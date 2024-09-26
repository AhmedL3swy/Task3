import { Directive, ElementRef, HostListener, Input } from '@angular/core';
import { NgControl } from '@angular/forms';

/**
 * Directive that automatically formats and validates date input fields.
 *
 * Features:
 * - Enforces a date format of DD/MM/YYYY
 * - Auto-completes dates with missing day/month values
 * - Validates that the entered date is within a specified year range
 * - Ensures no more than two slashes ("/") and only numeric input
 *
 * Usage:
 *
 * `<input type="text" DateParser3 [minYear]="1900" [maxYear]="2024" [autoComplete]="true" [addValidation]="true">`
 *
 * @Input minYear: Minimum allowed year for the date (default: 1900)
 * @Input maxYear: Maximum allowed year for the date (default: current year)
 * @Input autoComplete: Automatically pads incomplete date entries (default: false)
 * @Input addValidation: Adds validation for date format and valid range (default: false)
 */
@Directive({
  selector: '[DateParser3]',
  standalone: true,
})
export class DateParserDirective3 {
  lastValue: string = ''; // Stores the last valid value of the input

  // Input properties for customization
  @Input() minYear: number = 1900;
  @Input() maxYear: number = new Date().getFullYear();
  @Input() autoComplete: boolean = true;
  @Input() addValidation: boolean = true;

  constructor(private el: ElementRef, private control: NgControl) {}

  // #region Validation and Formatting

  /**
   * Triggered on blur (when input loses focus).
   * Attempts to convert the entered value into a valid date format.
   */
  @HostListener('blur')
  onBlur() {

    let value = this.el.nativeElement.value;
    const parsed = value.replace(/\D/g, ''); // Removes non-numeric characters
    const converted = this.DateConvertion(parsed);
    this.el.nativeElement.value = converted;
    // this.control.control?.setValue(converted);
  }

  /**
   * Main function to convert input into a valid date format (DD/MM/YYYY).
   * It applies auto-completion and validation.
   *
   * @param value String representation of the date input.
   * @returns A formatted or corrected date string.
   */
  private DateConvertion(value: string): string {
    if (this.autoComplete) {
      return this.AutoCompleteDate(value);
    }
    const paddedDate = this.PadintoDate(value);

    if (paddedDate.length !== 8) {
      if (this.addValidation)
        {this.addValidationErrors();} 
      return 'Invalid Date';
    }
    if (this.addValidation) {this.removeValidationErrors();}
    return this.DateCorrection(paddedDate);
  }

  /**
   * Adds a validation error to the control if the entered date is invalid.
   */
  private addValidationErrors() {
    this.control.control?.setErrors({ invalidDate: true });
  }

  /**
   * Removes the 'invalidDate' validation error from the control.
   */
  private removeValidationErrors() {
    if (this.control.control?.errors?.['invalidDate']) {
      const errors = this.control.control.errors;
      delete this.control.control.errors['invalidDate'];
      this.control.control?.setErrors({ ...errors });
    }
  }

  /**
   * Auto-completes the input date to ensure a valid DD/MM/YYYY format.
   *
   * @param value Raw date input string.
   * @returns Auto-completed and corrected date in DD/MM/YYYY format.
   */
  private AutoCompleteDate(value: string): string {
    if (value[0] > '3') {
      value = '0' + value; // Pad day with 0 if the first digit is greater than 3
    }
    if (value[2] > '1') {
      value = value.slice(0, 2) + '0' + value.slice(2); // Pad month with 0 if the first digit is greater than 1
    }
    const paddedDate = value.padEnd(8, '0'); // Pad remaining digits with '0'
    return this.DateCorrection(paddedDate);
  }

  /**
   * Pads incomplete date input into DDMMYYYY format.
   *
   * @param value Raw date string.
   * @returns Padded date string in DDMMYYYY format.
   */
  private PadintoDate(value: string): string {
    if (value.length < 8) {
      if (value[0] > '3') {
        value = '0' + value; // Pad day if needed
      }
      if (value[2] > '1') {
        value = value.slice(0, 2) + '0' + value.slice(2); // Pad month if needed
      }
      let day = value.slice(0, 2);
      let month = value.slice(2, 4);
      let year = value.slice(4);
      return day + month + year;
    } else {
      return value;
    }
  }

  /**
   * Corrects and formats a date string into DD/MM/YYYY, ensuring day, month, and year values are valid.
   * Handles leap years and ensures day/month values fall within acceptable ranges.
   *
   * @param value Raw date string in DDMMYYYY format.
   * @returns Corrected date string in DD/MM/YYYY format.
   */
  private DateCorrection(value: string): string {
    let MonthDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    let Day = parseInt(value.substring(0, 2)) || new Date().getDate();
    let Month = parseInt(value.substring(2, 4)) || new Date().getMonth() + 1;
    let Year = parseInt(value.substring(4, 8)) || new Date().getFullYear();

    Year = Math.max(this.minYear, Math.min(this.maxYear, Year)); // Ensure year is within the allowed range

    if ((Year % 4 === 0 && Year % 100 !== 0) || Year % 400 === 0) {
      MonthDays[1] = 29; // Adjust for leap year
    }

    Month = Math.max(1, Math.min(12, Month)); // Ensure month is valid
    Day = Math.max(1, Math.min(MonthDays[Month - 1], Day)); // Ensure day is valid for the given month

    return `${Day.toString().padStart(2, '0')}/${Month.toString().padStart(
      2,
      '0'
    )}/${Year}`;
  }

  // #region Interactions

  /**
   * Triggered on each input change.
   * Sanitizes input, ensuring only numeric values and slashes ('/') are allowed,
   * and restricts to valid date format.
   *
   * @param event Input event.
   */
  @HostListener('input', ['$event'])
  onInput(event: any) {
    const value = this.el.nativeElement.value;

    // Sanitize input to allow only numbers and slashes
    const sanitizedValue = value
      .replace(/[^0-9/]/g, '') // Remove non-numeric and non-slash characters
      .replace(/(\/{2,})/g, '/') // Replace multiple slashes with a single slash
      .replace(/^[/]/, ''); // Remove leading slashes

    // Restrict slashes to format of DD/MM/YYYY
    if (event.data == '/') {
      const segments = sanitizedValue.split('/');
      if (segments.length === 1 && segments[0].length > 2) {
        this.el.nativeElement.value = this.lastValue;
        return;
      } else if (
        segments.length > 3 ||
        segments.some((segment: string) => segment.length > 2)
      ) {
        this.el.nativeElement.value = this.lastValue;
        return;
      }
    }
    this.lastValue = sanitizedValue;
    this.el.nativeElement.value = sanitizedValue;
  }

  // #endregion
}
