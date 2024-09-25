import { Directive, ElementRef, HostListener, Input } from '@angular/core';
import { NgControl } from '@angular/forms';

@Directive({
  selector: '[DateParser3]',
  standalone: true,
})
export class DateParserDirective3 {
  // Input Min and Max Year
  @Input() minYear: number = 1200;
  @Input() maxYear: number = new Date().getFullYear();

  constructor(private el: ElementRef, private control: NgControl) {}

  // On blur, convert the input value to a valid date format or show "Invalid Date"
  @HostListener('blur')
  onBlur() {
    let value = this.el.nativeElement.value;
    const parsed = value.replace(/\D/g, ''); // Remove all non-digit characters
    const converted = this.DateConvertion(parsed);
    this.el.nativeElement.value = converted;
    this.control.control?.setValue(converted);
  }

  // Main function to convert input to a valid date format
  private DateConvertion(value: string): string {
    // Handle invalid lengths

    // Pad and format the date into DD/MM/YYYY
    const paddedDate = this.PadintoDate(value);
    if (paddedDate.length !== 8) {
      return 'Invalid Date';
    }
    

    // Extract day, month, year from padded date
    const correctedDate = this.DateCorrection(paddedDate.replace(/\D/g, ''));

    // Return the date formatted as DD/MM/YYYY
    return correctedDate;
  }

  // Pad the Date into the format of DD/MM/YYYY, handling cases like '551999' -> '05/05/1999'
  private PadintoDate(value: string): string {
    if (value.length < 8) {
      let day, month, year;
      if (value[0] > '3') {
        // add 0 to the value
        value = '0' + value;
      }
      if (value[2] > '1') {
        // add 0 to the value
        value = value.slice(0, 2) + '0' + value.slice(2);
      }
      day = value.slice(0, 2);
      month = value.slice(2, 4);
      year = value.slice(4);
      return day + month + year;
    } else {
      return value;
    }
  }
  private DateCorrection(value: string): string {
    // Take Full Length of DD/MM/YYYY and correct the date Days of the month and handle leap year
    const MonthDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
    let Day = parseInt(value.substring(0, 2)) || new Date().getDate();
    let Month = parseInt(value.substring(2, 4)) || new Date().getMonth() + 1;
    let Year = parseInt(value.substring(4, 8)) || new Date().getFullYear();

    // Ensure Year is within the min and max range
    Year = Math.max(this.minYear, Math.min(this.maxYear, Year));

    // Leap year adjustment
    if ((Year % 4 === 0 && Year % 100 !== 0) || Year % 400 === 0) {
      MonthDays[1] = 29;
    }

    // Ensure Month is within 1-12
    Month = Math.max(1, Math.min(12, Month));

    // Ensure Day is within the valid range for the given month
    Day = Math.max(1, Math.min(MonthDays[Month - 1], Day));

    // Return the corrected date formatted as DD/MM/YYYY
    return `${Day.toString().padStart(2, '0')}/${Month.toString().padStart(2, '0')}/${Year}`;
  }
}
