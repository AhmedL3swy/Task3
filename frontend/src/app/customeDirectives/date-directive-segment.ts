// import { Directive, ElementRef, HostListener, Input } from '@angular/core';
// import { NgControl } from '@angular/forms';

// @Directive({
//   selector: '[DateParser2]',
//   standalone: true,
// })
// export class DateParserDirective2 {
//   constructor(private el: ElementRef, private control: NgControl) {
//     this.el.nativeElement.style.userSelect = 'none'; // Prevent text selection across segments
//   }

//   // Input for Min and Max Year
//   @Input() minYear: number = 1200;
//   @Input() maxYear: number = new Date().getFullYear();
//   lastInput: string = '';
//   segmentIndex: number = 0; // 0 for day, 1 for month, 2 for year

//   // Handle segment selection and input focus
//   @HostListener('select', ['$event'])
//   onSelect(event: Event) {
//     const input = this.el.nativeElement;
//     const selectionStart = input.selectionStart || 0;

//     if (selectionStart < 3) {
//       this.segmentIndex = 0; // Day segment
//     } else if (selectionStart >= 3 && selectionStart < 6) {
//       this.segmentIndex = 1; // Month segment
//     } else {
//       this.segmentIndex = 2; // Year segment
//     }
//     this.preventCrossSelection(); // Restrict selection to a single segment
//   }

//   // Prevent cross-selection (allow only one segment to be selected)
//   private preventCrossSelection() {
//     const input = this.el.nativeElement;
//     if (this.segmentIndex === 0 && input.selectionEnd > 2) {
//       input.selectionStart = 0;
//       input.selectionEnd = 2; // Limit selection to the day segment
//     } else if (this.segmentIndex === 1 && input.selectionEnd > 5) {
//       input.selectionStart = 3;
//       input.selectionEnd = 5; // Limit selection to the month segment
//     } else if (this.segmentIndex === 2 && input.selectionEnd > 10) {
//       input.selectionStart = 6;
//       input.selectionEnd = 10; // Limit selection to the year segment
//     }
//   }

//   // Handle input event for each segment
//   @HostListener('input', ['$event'])
//   onInput(event: any) {
//     const input = this.el.nativeElement.value;
//     const parsedDate = this.parseDate(input);

//     // Update the value based on the active segment
//     this.el.nativeElement.value = parsedDate;
//     this.lastInput = parsedDate;
//   }

//   // Handle backspace and delete keys
//   @HostListener('keydown', ['$event'])
//   keydown(event: KeyboardEvent) {
//     const value = this.el.nativeElement.value;
//     if (event.key === 'Backspace' || event.key === 'Delete') {
//       event.preventDefault();
//       this.handleBackspaceDelete(value);
//     }

//     // Handle slashes to navigate between segments
//     if (event.key === '/') {
//       event.preventDefault();
//       this.navigateSegments();
//     }
//   }

//   private handleBackspaceDelete(value: string) {
//     // Handle deletion in the current segment
//     const segment = this.getSegmentValue();
//     if (segment) {
//       const newValue = segment.slice(0, -1); // Remove last character
//       this.setSegmentValue(newValue);
//     }
//   }

//   private navigateSegments() {
//     // Navigate to the next segment on '/' key press
//     if (this.segmentIndex < 2) {
//       this.segmentIndex++;
//     } else {
//       this.segmentIndex = 0; // Loop back to the day segment
//     }
//     this.focusSegment();
//   }

//   // Parse date based on the segment index and value
//   private parseDate(value: string): string {
//     const numericValue = value.replace(/\D/g, '');
//     return this.formatDate(numericValue);
//   }

//   // Format the date value (Day/Month/Year)
//   private formatDate(value: string): string {
//     if (value.length <= 2) {
//       return value.padStart(2, '0') + '/';
//     } else if (value.length <= 4) {
//       return (
//         value.slice(0, 2).padStart(2, '0') +
//         '/' +
//         value.slice(2).padStart(2, '0') +
//         '/'
//       );
//     } else {
//       return (
//         value.slice(0, 2).padStart(2, '0') +
//         '/' +
//         value.slice(2, 4).padStart(2, '0') +
//         '/' +
//         value.slice(4).padStart(4, '0')
//       );
//     }
//   }

//   // Get the value for the current segment
//   private getSegmentValue(): string {
//     const value = this.el.nativeElement.value;
//     const segments = value.split('/');
//     return segments[this.segmentIndex] || '';
//   }

//   // Set the value for the current segment
//   private setSegmentValue(newValue: string) {
//     const value = this.el.nativeElement.value;
//     const segments = value.split('/');
//     segments[this.segmentIndex] = newValue;
//     this.el.nativeElement.value = segments.join('/');
//   }

//   // Focus on the current segment for input
//   private focusSegment() {
//     const input = this.el.nativeElement;
//     if (this.segmentIndex === 0) {
//       input.setSelectionRange(0, 2); // Focus on day
//     } else if (this.segmentIndex === 1) {
//       input.setSelectionRange(3, 5); // Focus on month
//     } else if (this.segmentIndex === 2) {
//       input.setSelectionRange(6, 10); // Focus on year
//     }
//   }
// }
