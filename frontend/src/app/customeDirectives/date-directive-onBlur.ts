import { formatDate } from '@angular/common';
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
 * @Input minYear: Minimum allowed Date  -> Default 1/1/1900
 * @Input maxYear: Maximum allowed Date  -> Default Current Date
 * @Input autoComplete: Automatically pads incomplete date entries (default: true)
 * @Input addValidation: Adds validation in the controller (default: true)
 */
@Directive({
  selector: '[DateParser]',
  standalone: true,
})
export class DateParserDirective {
  lastValue: string = ''; // Stores the last valid value of the input

  // Input properties for customization
  @Input() min: Date = new Date(1900, 0, 1);
  @Input() max: Date = new Date();
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
    const value = this.el.nativeElement.value;
    const converted = this.DateConvertion(value);
    this.el.nativeElement.value = converted;
    this.addValueToControl(converted);
  }

  /**
   * Main function to convert input into a valid date format (DD/MM/YYYY).
   * It applies auto-completion and validation.
   *
   * @param value String representation of the date input.
   * @returns A formatted or corrected date string.
   */
  private DateConvertion(value: string): string {
    let paddedDate = '';
    // Speacial Treatment for Date Containing Slash to handle Edit/Delete Operations
    if (value.includes('/')) {
      paddedDate = this.formatDateContainingSlash(value);
    } // Main Logic for Date Conversion
    else {
      paddedDate = this.PadintoDate(value.replace(/\D/g, ''));
      console.log('Padded Date', paddedDate);
    }
    // After Completing the Date Check if the Date is Valid it should be 8 length
    if (paddedDate.length !== 8) {
      if (this.addValidation) {
        this.addValidationErrors();
      }
      // If the Date is Invalid Return Invalid Date
      return 'Invalid Date';
    }
    // If Auto Complete Pad the Rest with '0' and Then Correct the Date
    if (this.autoComplete) {
      return this.AutoCompleteDate(paddedDate);
    }

    // If the Date is Valid Remove any exisitng Validation Error from last trails
    if (this.addValidation) {
      this.removeValidationErrors();
    }
    // Correct the Values well to be Proper Date Format and in Range Handle 30/2 etc and be within min/max Date
    paddedDate = this.DateCorrection(paddedDate);
    // Format the Date to be in DD/MM/YYYY
    const formattedDate = paddedDate.replace(
      /(\d{2})(\d{2})(\d{4})/,
      '$1/$2/$3'
    );
    // After this Check if the Date is Valid to handle unexpected values and return the Date if so..
    return this.isDateValid(paddedDate.replace(/\D/g, ''))
      ? formattedDate
      : 'Invalid Date';
  }

  /**
   * Auto-completes the input date to ensure a valid DD/MM/YYYY format.
   *
   * @param value Raw date input string.
   * @returns Auto-completed and corrected date in DD/MM/YYYY format.
   */
  private AutoCompleteDate(value: string): string {
    const corrected = this.PadintoDate(value);
    const paddedDate = corrected.padEnd(8, '0'); // Pad remaining digits with '0'
    return this.DateCorrection(paddedDate);
  }

  /**
   * Pads incomplete date input into DDMMYYYY format.
   *
   * @param value Raw date string.
   * @returns Padded date string in DDMMYYYY format.
   */
  private PadintoDate(value: string): string {
    if (value.length <= 4) {
      return this.formatDateWithoutYear(value);
    } else {
      return this.formatDateWithYear(value);
    }
  }

  private formatDateWithYear(value: string): string {
    //Length 6 dmyyyyy 122000 or 252000
    if (value.length == 6) {
      value = '0' + value.slice(0, 1) + '0' + value.slice(1);
    }
    if (value.length == 7) {
      //Length 7 dmyyyyy 312000 or 2522000
      // if first 2 is less than month max day
      if (
        parseInt(value.slice(0, 2)) <= this.getMonthMaxDay(parseInt(value[2]))
      ) {
        return value.slice(0, 2) + '0' + value.slice(2); // Month Need 0
      } else {
        value = '0' + value.slice(0, 1) + value.slice(1); // Day Need 0
        return value;
      }
    }
    return value;
  }
  private formatDateWithoutYear(value: string): string {
    let day = '';
    let month = '';
    let year = '';

    if (value.length == 1) {
      day = value;
      value = day.padStart(2, '0');
    }
    if (value.length == 2) {
      if (parseInt(value) <= this.getMonthMaxDay()) {
        day = value;
        value = day.padStart(2, '0');
      } else {
        day = value.slice(0, 1);
        month = value.slice(1);
        value = day.padStart(2, '0') + month.padStart(2, '0');
      }
    }
    if (value.length == 3) {
      if (
        parseInt(value.slice(0, 2)) <= this.getMonthMaxDay(parseInt(value[2]))
      ) {
        day = value.slice(0, 2);
        month = value.slice(2);
        value = day.padStart(2, '0') + month.padStart(2, '0');
      } else {
        day = value.slice(0, 1);
        month = value.slice(1, 3);
        value = day.padStart(2, '0') + month.padStart(2, '0');
      }
    }
    if (value.length > 3) {
      day = value.slice(0, 2);
      month = value.slice(2, 4);
      year = value.slice(4);
      value =
        day.padStart(2, '0') + month.padStart(2, '0') + year.padStart(4, '0');
    }
    console.log(value);
    return value;
  }
  private getMonthMaxDay(month: number = new Date().getMonth() + 1): number {
    let MonthDays = [31, 29, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    return MonthDays[month - 1];
  }

  private formatDateContainingSlash(value: string): string {
    const segments = value.split('/');
    console.log(
      segments.map((segment: string) => segment.padStart(2, '0')).join('')
    );
    return segments.map((segment: string) => segment.padStart(2, '0')).join('');
  }

  // #endregion

  // #region Date Correction
  /**
   * Corrects and formats a date string into DD/MM/YYYY, ensuring day, month, and year values are valid.
   * Handles leap years and ensures day/month values fall within acceptable ranges.
   *
   * @param value Raw date string in DDMMYYYY format.
   * @returns Corrected date string in DD/MM/YYYY format.
   */
  private DateCorrection(value: string): string {
    // if not Valid Date return Invalid Date
    console.log('Date Correction', value);

    let MonthDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    let Day = parseInt(value.substring(0, 2)) || new Date().getDate();
    let Month = parseInt(value.substring(2, 4)) || new Date().getMonth() + 1;
    let Year = parseInt(value.substring(4, 8)) || new Date().getFullYear();

    if ((Year % 4 === 0 && Year % 100 !== 0) || Year % 400 === 0) {
      MonthDays[1] = 29; // Adjust for leap year
    }
    if (Day > MonthDays[Month - 1] || Month > 12) {
      return 'Invalid Date';
    }

    Month = Math.max(1, Math.min(12, Month)); // Ensure month is valid
    Day = Math.max(1, Math.min(MonthDays[Month - 1], Day)); // Ensure day is valid for the given month
    let currentDate = new Date(Year, Month - 1, Day);
    // Insure Max Year and Min Year
    if (currentDate > this.max) {
      currentDate = this.max;
    }
    if (currentDate < this.min) {
      currentDate = this.min;
    }
    Day = currentDate.getDate();
    Month = currentDate.getMonth() + 1;
    Year = currentDate.getFullYear();

    return `${Day.toString().padStart(2, '0')}/${Month.toString().padStart(
      2,
      '0'
    )}/${Year}`;
  }
  // #endregion

  /**
   * Triggered on each input change.
   * Sanitizes input, ensuring only numeric values and slashes ('/') are allowed,
   * and restricts to valid date format.
   *
   * @param event Input event.
   */
  // #region Interactions

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
    // handle to limit of 8 digits
    if (sanitizedValue.replace(/\D/g, '').length > 8) {
      this.el.nativeElement.value = this.lastValue;
      return;
    }
    this.lastValue = sanitizedValue;
    this.el.nativeElement.value = sanitizedValue;
  }

  // #endregion

  // #region click
  @HostListener('click')
  onClick() {
    // this.el.nativeElement.select();
    // remove the invalid date error
    if (this.el.nativeElement.value == 'Invalid Date') {
      this.el.nativeElement.value = '';
    }
  }
  // #endregion

  // #region DateValidtor
  // DateValidtor Using BuiltIn DateFormat
  isDateValid(date: string) {
    // Check if the date is in the format DD/MM/YYYY
    if (date.length !== 8) {
      return false;
    }
    const day = parseInt(date.substring(0, 2), 10);
    const month = parseInt(date.substring(2, 4), 10) - 1;
    const year = parseInt(date.substring(4, 8), 10);

    const parsedDate = new Date(year, month, day);
    return (
      parsedDate.getFullYear() === year &&
      parsedDate.getMonth() === month &&
      parsedDate.getDate() === day
    );
  }
  // #endregion

  //#region Control Handling
  /**
   * Adds a validation error to the control if the entered date is invalid.
   */
  private addValidationErrors() {
    this.control.control?.setErrors({ invalidDate: true });
  }
  /**
   * Adds the value to the control
   */
  private addValueToControl(value: string) {
    this.control.control?.setValue(value);
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
  // #endregion

  //#region oldwork
  private formatDate(value: string): string {
    // Handle d/m format if given only d and m
    // length 2 ex : 42 or 12

    // Length 3 ex:111
    if (value[0] > '3' && value.length == 3) {
      return (value = '0' + value.slice(0, 1) + value.slice(1));
    }
    //Length 6 dmyyyyy 122000 or 252000
    if (value[0] <= '3' && value.length == 6) {
      value = '0' + value.slice(0, 1) + '0' + value.slice(1);
    }

    if (value[0] > '3') {
      value = '0' + value.slice(1);
    }
    if (value[2] == '1' && value.length == 7) {
      value = value.slice(0, 2) + '0' + value.slice(2);
    }
    if (value[2] > '1') {
      value = value.slice(0, 2) + '0' + value.slice(2);
    }
    console.log(value);
    return value;
  }
  //#endregion
}
