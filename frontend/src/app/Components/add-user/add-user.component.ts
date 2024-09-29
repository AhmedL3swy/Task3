import { min } from 'rxjs';
import { CommonModule } from '@angular/common';
import {
  Component,
  OnInit,
  ViewChild,
  ElementRef,
  viewChild,
} from '@angular/core';
import { formatDate } from '@angular/common';

import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../Services/fake-data-service.service';
import { HttpParams } from '@angular/common/http';
import { UniqueValidatorDirective } from '../../customeDirectives/unique-validator';
import { emailAsyncValidator } from '../../customValidations/unique-email-validator';
import { DateParserDirective } from '../../customeDirectives/date-directive-onBlur';
import moment from 'moment';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    ReactiveFormsModule,
    UniqueValidatorDirective,
    DateParserDirective,
  ],
  styleUrls: ['./add-user.component.scss'],
})
export class AddUserComponent implements OnInit {
  userForm!: FormGroup;
  @ViewChild('birthDate') birthDate!: ElementRef;
  @ViewChild('secondDate') secondDate!: ElementRef;
  birthDatee: string | null = null;
  minDate = new Date(2000, 0, 1);
  maxDate = new Date(2030,0,1);

  onDateChange(event: any) {
    // Format the input value to 'dd/MM/yyyy'
  }
  maritalStatusOptions: any = [
    { id: 1, name: 'Single' },
    { id: 2, name: 'Married' },
    { id: 3, name: 'Divorced' },
    { id: 4, name: 'Widower' },
  ];
  isEditMode = false;
  userId: string | null = null;
  apiUrl = 'https://localhost:7237/api/Users';

  constructor(
    private fb: FormBuilder,
    private dataService: ApiService,
    private route: ActivatedRoute,
    private router: Router
  ) {}

  ngOnInit(): void {
    // Initialize form
    this.userForm = this.fb.group({
      id: [this.userId],
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      birthDate: ['', [Validators.required]],
      email: [
        '',
        {
          validators: [Validators.required, Validators.email],
          asyncValidators: [emailAsyncValidator(this.dataService)],
          updateOn: 'blur',
        },
      ],
      mobile: ['', [Validators.required, Validators.pattern(/^05\d{8}$/)]],
      municipalNo: [
        '',
        [Validators.required, Validators.pattern(/^\d{6,10}$/)],
      ],
      nationalId: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      maritalStatusId: [null, [Validators.required]],
      address: [''],
    });

    // Check if we are in edit mode (if userId is in the route)
    this.userId = this.route.snapshot.paramMap.get('id');
    if (this.userId) {
      this.isEditMode = true;
      this.loadUserDetails(this.userId);
    }

    // Clear server-side errors when the user types
    this.userForm.valueChanges.subscribe(() => {
      this.clearServerErrors();
    });
    // OverRide Maritual Status Dict from   'https://localhost:7237/api/MaritalStatus/GetMaritalStatuses' \ id,name
    this.dataService
      .getData(
        'https://localhost:7237/api/MaritalStatus/GetMaritalStatuses',
        new HttpParams()
      )
      .subscribe(
        (res) => {
          this.maritalStatusOptions = res;
        },
        (err) => {
          console.error('Error loading marital status options', err);
        }
      );
  }

  // Load user details for editing
  loadUserDetails(userId: string): void {
    this.dataService.getEntityById(`${this.apiUrl}/GetUser`, userId).subscribe(
      (user: any) => {
        this.userForm.patchValue(user);
        this.userId = user.id;

        const formattedDate = formatDate(
          this.userForm.get('birthDate')?.value,
          'dd/MM/yyyy',
          'en-US'
        );
        this.userForm.get('birthDate')?.setValue(formattedDate);
      },
      (err) => {
        console.error('Error loading user details', err);
      }
    );
  }

  // Clears specific server-side errors when the field is touched or typed in
  clearServerErrors() {
    Object.keys(this.userForm.controls).forEach((key) => {
      if (this.userForm.get(key)?.dirty) {
        const control = this.userForm.get(key);
        if (control && control.hasError('serverError')) {
          control.setErrors(null);
        }
      }
    });
  }

  // Submit the form for adding or editing
  onSubmit(): void {
    if (this.userForm.valid) {
      const user = this.userForm.value;
      if (this.isEditMode) {
        // Update existing user
        this.dataService
          .updateEntity(`${this.apiUrl}/EditUser/${this.userId}`, user)
          .subscribe(
            (res) => {
              console.log('User updated successfully', res);
              this.router.navigate(['/users']);
            },
            (err) => {
              this.handleServerErrors(err.error);
            }
          );
      } else {
        // Add new user
        this.dataService.addEntity(`${this.apiUrl}/AddUser`, user).subscribe(
          (res) => {
            console.log('User added successfully', res);
            this.router.navigate(['/users']);
          },
          (err) => {
            this.handleServerErrors(err.error);
          }
        );
      }
    } else {
      this.userForm.markAllAsTouched();
    }
  }
  checkDate() {
    // log form values
    console.log(this.userForm.value);
    // const birthDate = this.birthDate.nativeElement.value;
    //const secondDate = this.secondDate.nativeElement.value;
    // const date = new Date(secondDate);
    // const momentDate = moment(date);
    // const formattedDate = momentDate.format('DD/MM/YYYY');
    // let angularFormattedDate = '';
    // if (!isNaN(Date.parse(secondDate))) {
    //   angularFormattedDate = formatDate(secondDate, 'dd/MM/yyyy', 'en-US');
    // }

    // console.log('Moment.js formatted date:', formattedDate);
    // console.log('birthDate:', birthDate);
    // console.log('SecondDate:', angularFormattedDate);
  }

  // Adds server-side validation errors to the form controls
  handleServerErrors(errors: any) {
    this.userForm.markAsPristine();
    Object.keys(errors).forEach((key) => {
      const ckey = key.charAt(0).toLowerCase() + key.slice(1);
      if (this.userForm.get(ckey)) {
        this.userForm
          .get(ckey)
          ?.setErrors({ serverError: errors[key]?.join(' ') });
      }
    });
  }
  //back to grid
  back() {
    this.router.navigate(['/users']);
  }
  get f() {
    return this.userForm.controls;
  }
}
