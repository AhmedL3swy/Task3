// import { Directive, ElementRef, HostListener, Input } from '@angular/core';
// import { NgControl } from '@angular/forms';
// import { min } from 'rxjs';
// @Directive({
//   selector: '[DateParser]',
//   standalone: true,
// })
// export class DateParserDirective {
//   constructor(private el: ElementRef, private control: NgControl) {

//   }

//   // Input Min and Max Year
//   @Input() minYear: number = 1200;
//   @Input() maxYear: number = new Date().getFullYear();
//   lastInput: string = '';
//   moreThanOne: boolean = false;
//   segmentIndex: number = 0; // 0 for day, 1 for month, 2 for year

//   @HostListener('select', ['$event'])
//   onSelect(event: Event) {
//     const input = event.target as HTMLInputElement;
//     // console.log(event);
//     // If More than 1 character selected, then select all to the next /
//     if (
//       input.selectionEnd !== null &&
//       input.selectionStart !== null &&
//       input.selectionEnd - input.selectionStart > 1
//     ) {
//       this.moreThanOne = true;
//       return;
//     }
//   }

//   // Main Functionality

//   @HostListener('input', ['$event']) onInput(event: any) {
//     const value = this.el.nativeElement.value;
//     if (value.length > 10) {
//       this.el.nativeElement.value = this.lastInput;
//       return;
//     }
//     const parsedDate = this.parseDate(value);
//     // Access the input key via event.key
//     const inputKey = event.data;

//     if (this.moreThanOne) {
//       this.moreThanOne = false;
//       this.el.nativeElement.value = this.lastInput;
//       return;
//     }
//     //Handle Deleting by Clearing the Value
//     if (this.isDeleting(value)) {
//       this.lastInput = '';
//       this.el.nativeElement.value = inputKey;
//       return;
//     }

//     // Save Last Input
//     this.lastInput = parsedDate;
//     this.el.nativeElement.value = parsedDate;
//     this.control.control?.setValue(parsedDate);
//   }

//   // HostListener for backspace and delete
//   @HostListener('keydown', ['$event'])
//   keydown(event: KeyboardEvent) {
//     const value = this.el.nativeElement.value;
//     if (event.key === 'Backspace' || event.key === 'Delete') {
//       event.preventDefault();
//       this.lastInput = this.el.nativeElement.value = value.substring(
//         0,
//         value.length - 1
//       );
//     }
//     // if key is /
//     if (event.key === '/') {
//       event.preventDefault();
//       // if the last character is / then return
//       if (value[value.length - 1] === '/') {
//         return;
//       }
//       this.el.nativeElement.value = value + '/';
//     }
//   }
//   // Validation Functionality
//   @HostListener('blur')
//   onBlur() {
//     let value = this.el.nativeElement.value;
//     const parsed = value.replace(/\D/g, '');
//     const nparsed = this.DateCorrection(parsed);
//     this.el.nativeElement.value = nparsed;
//     // set the control value
//     this.control.control?.setValue(nparsed);
//     // Set Error to Controller
//     // if (this.isInvalidDate(value)) {
//     //   this.control.control?.setErrors({ invalidDate: true });
//     //   return;
//     // } else {
//     //   const errors = this.control.control?.errors;
//     //   if (errors) {
//     //     delete errors['invalidDate'];
//     //     this.control.control?.setErrors({ ...errors });
//     //   }
//     // }
//   }
//   isDeleting(value: string): boolean {
//     if (this.lastInput == '') return false;
//     return (
//       value.replace(/\D/g, '').length < this.lastInput.replace(/\D/g, '').length
//     );
//   }
//   parseDate(date: string): string {
//     if (!date || date === '') {
//       return date;
//     }
//     const value = date.replace(/\D/g, '');
//     return this.dateParser(value);
//   }

//   private dateParser(value: string): string {
//     if (value.length <= 2) {
//       value = this.padDay(value);
//       return value.replace(/^(\d{0,2})/, '$1/');
//     }
//     if (value.length <= 4) {
//       value =
//         this.padDay(value.substring(0, 2)) + this.padMonth(value.substring(2));
//       return value.replace(/^(\d{0,2})(\d{0,2})/, '$1/$2/');
//     }
//     if (value.length <= 8) {
//       value =
//         this.padDay(value.substring(0, 2)) +
//         this.padMonth(value.substring(2, 4)) +
//         this.padYear(value.substring(4));
//       return value.replace(/^(\d{0,2})(\d{0,2})(\d{0,4})/, '$1/$2/$3');
//     }
//     return this.DateCorrection(value);
//   }
//   private padDay(value: string): string {
//     if (value[0] > '3') {
//       return '0' + value[0];
//     }
//     if (value[0] === '3' && value[1] > '1') {
//       return '0' + value[0] + '0' + value[1];
//     }
//     return value;
//   }
//   private padMonth(value: string): string {
//     if (value[0] > '1') {
//       return '0' + value[0];
//     }
//     if (value[0] === '1' && value[1] > '2') {
//       return '0' + value[0] + this.padYear(value[1]);
//     }
//     return value;
//   }
//   private padYear(value: string): string {
//     // allow only from 1900 to this year
//     const currentYear = this.maxYear;
//     const minYear = this.minYear;
//     // check by parsing to int
//     const year = parseInt(value);
//     // if year not full 4 digits return as its
//     if (value.length < 4) {
//       return value;
//     }
//     // if total less than 1900 or greater return the min or max by parsing into int
//     if (year < minYear) {
//       return minYear.toString();
//     }
//     if (year > currentYear) {
//       return currentYear.toString();
//     }
//     return value;
//   }

//   private DateCorrection(value: string): string {
//     // Take Full Length of DD/MM/YYYY and correct the date Days of the month and handle leap year
//     var MonthDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
//     var Day = Math.max(
//       1,
//       parseInt(value.substring(0, 2)) || new Date().getDate()
//     );
//     var Month = Math.max(
//       1,
//       Math.min(12, parseInt(value.substring(2, 4)) || new Date().getMonth() + 1)
//     );
//     var Year = Math.max(
//       this.minYear,
//       Math.min(
//         this.maxYear,
//         parseInt(value.substring(4, 8)) || new Date().getFullYear()
//       )
//     );

//     // Leap Days Fix
//     if (Year > this.minYear) {
//       if (this.isLeapYear(Year)) {
//         MonthDays[1] = 29;
//       }
//     }
//     // Pervent Day to be more than its month
//     if (Day > MonthDays[Month - 1]) {
//       Day = MonthDays[Month - 1];
//     }

//     return (
//       this.padDay(Day.toString().padStart(2, '0')) +
//       '/' +
//       this.padMonth(Month.toString().padStart(2, '0')) +
//       '/' +
//       this.padYear(Year.toString())
//     );
//   }
//   private isLeapYear(year: number): boolean {
//     return (year % 4 === 0 && year % 100 !== 0) || year % 400 === 0;
//   }

//   isInvalidDate(value: string): boolean {
//     // Split and Proper Pad the Splited Values
//     let parts = value.split('/');
//     let Day = parts[0];
//     let Month = parts[1];
//     let Year = parts[2];
//     if (
//       isNaN(parseInt(Day)) ||
//       parseInt(Day) == 0 ||
//       isNaN(parseInt(Month)) ||
//       parseInt(Month) == 0 ||
//       isNaN(parseInt(Year)) ||
//       parseInt(Year) == 0 ||
//       value.length < 10
//     ) {
//       return true;
//     }
//     return false;
//   }
//   //   @HostListener('ngModelChange', ['$event'])
//   //   onModelChange(event: any) {
//   //     this.onInputChange(event, false);
//   //   }
//   //   @HostListener('keydown.backspace', ['$event'])
//   //   keydownBackspace(event: any) {
//   //     this.onInputChange(event?.target?.value, true);
//   //   }

//   //   onInputChange(event: any, backspace: any) {
//   //     let newVal = event.replace(/\D/g, '');
//   //     if (backspace && newVal.length <= 6) {
//   //       newVal = newVal.substring(0, newVal.length - 1);
//   //     }
//   //     if (newVal.length === 0) {
//   //       newVal = '';
//   //     } else if (newVal.length <= 3) {
//   //       newVal = newVal.replace(/^(\d{0,2})/, '$1/');
//   //     } else if (newVal.length <= 4) {
//   //       newVal = newVal.replace(/^(\d{0,2})(\d{0,2})/, '$1/$2/');
//   //     } else {
//   //       newVal = newVal.substring(0, 8);
//   //       newVal = newVal.replace(/^(\d{0,2})(\d{0,2})(\d{0,4})/, '$1/$2/$3');
//   //     }
//   //     newVal = this.DateCorrection(newVal.replace(/\D/g, ''));
//   //     console.log('value in directive: ' + newVal);
//   //     if (this.control?.valueAccessor) {
//   //       this.control.valueAccessor.writeValue(newVal);
//   //     }
//   //   }

//   // @HostListener('select', ['$event'])
//   // onSelect(event: any) {
//   //   const input = this.el.nativeElement.value;
//   //   const selectionStart = this.el.nativeElement.selectionStart;
//   //   if (selectionStart < 3) {
//   //     this.segmentIndex = 0; // Select day
//   //   } else if (selectionStart < 6) {
//   //     this.segmentIndex = 1; // Select month
//   //   } else {
//   //     this.segmentIndex = 2; // Select year
//   //   }
//   //   this.preventCrossSelection();
//   // }
//   // private preventCrossSelection() {
//   //   const input = this.el.nativeElement;
//   //   if (this.segmentIndex === 0 && input.selectionEnd > 2) {
//   //     input.selectionStart = 0;
//   //     input.selectionEnd = 2;
//   //   } else if (this.segmentIndex === 1 && input.selectionEnd > 5) {
//   //     input.selectionStart = 3;
//   //     input.selectionEnd = 5;
//   //   } else if (this.segmentIndex === 2 && input.selectionEnd > 10) {
//   //     input.selectionStart = 6;
//   //     input.selectionEnd = 10;
//   //   }
//   // }
//   // if (this.moreThanOne) {
//   //   // Parse the input Key only in the segment with padding while leaving the others
//   //   // if (this.segmentIndex === 0) {
//   //   //   // Make Correction with replacing the first 2 characters of lastinput with padded inputKey
//   //   //   // split by / and replace the first segment with padded inputKey
//   //   //   const newVal = this.lastInput.split('/')[0].replace(
//   //   //     /(\d{0,2})/,
//   //   //    this.padDay(inputKey)
//   //   //   );
//   //   //   this.el.nativeElement.value = this.DateCorrection(newVal);
//   //   // } else if (this.segmentIndex === 1) {
//   //   //   this.el.nativeElement.value = this.padMonth(inputKey);
//   //   // } else {
//   //   //   this.el.nativeElement.value = this.padYear(inputKey);
//   //   // }
//   // }
//   // @HostListener('input', ['$event'])
//   // onInput(event: any) {
//   //   const input = this.el.nativeElement.value.replace(/\D/g, '');
//   //   if (input.length <= 2) {
//   //     this.segmentIndex = 0; // Day segment
//   //   } else if (input.length <= 4) {
//   //     this.segmentIndex = 1; // Month segment
//   //   } else {
//   //     this.segmentIndex = 2; // Year segment
//   //   }
//   //   const parsedDate = this.parseDate(input);
//   //   this.el.nativeElement.value = parsedDate;
//   // }
//   // private parseDate(value: string): string {
//   //   if (!value || value === '' ) {
//   //     return value;
//   //   }
//   //   if (value.length <= 2) {
//   //     return value.replace(/^(\d{0,2})/, '$1/');
//   //   }
//   //   if (value.length <= 4) {
//   //     return value.replace(/^(\d{0,2})(\d{0,2})/, '$1/$2/');
//   //   }
//   //   return value.substring(0, 8).replace(/^(\d{0,2})(\d{0,2})(\d{0,4})/, '$1/$2/$3');
//   // }
//   // Allow click to activate element
// }
