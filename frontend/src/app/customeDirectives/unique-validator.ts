import {
  Directive,
  ElementRef,
  HostListener,
  Input,
} from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { NgControl } from '@angular/forms';

@Directive({
  selector: '[UniqueValidator]',
  standalone: true,
})
export class UniqueValidatorDirective {
  @Input() propertyName: string = ''; // The property to validate (e.g., Email, Mobile)
  @Input() userId: string = "0"; // ID of the user (for edit scenarios)

  constructor(
    private el: ElementRef,
    private http: HttpClient,
    private control: NgControl
  ) {}

  @HostListener('blur') async onBlur() {
    const value = this.el.nativeElement.value;
    if (!value || !this.propertyName) {
      return;
    }

    const params = new HttpParams()
      .set('propertyName', this.propertyName)
      .set('value', value)
      .set('id', this.userId.toString());

    // Call the uniqueness API
    this.http
      .get<boolean>('https://localhost:7237/api/Users/IsUnique', { params })
      .subscribe({
        next: (isUnique) => {
          if (!isUnique) {
            // If not unique, show an error
            this.control.control?.setErrors({ notUnique: true });
          } else {
            // Clear only unique error if value is unique
            const errors = this.control.control?.errors;
            if (errors) {
              delete errors['notUnique'];
              this.control.control?.setErrors({ ...errors });
            }
          
          }
        },
        error: () => {
            console.log('Unexpected error');
        },
      });
  }
}
