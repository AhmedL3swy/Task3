import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import {
  FormBuilder,
  FormGroup,
  FormsModule,
  ReactiveFormsModule,
  Validators,
} from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../Services/fake-data-service.service';

@Component({
  selector: 'app-add-user',
  templateUrl: './add-user.component.html',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  styleUrls: ['./add-user.component.scss'],
})
export class AddUserComponent implements OnInit {
  userForm!: FormGroup;
  maritalStatusOptions = [
    { id: 1, value: 'Single' },
    { id: 2, value: 'Married' },
    { id: 3, value: 'Divorced' },
    { id: 4, value: 'Widower' },
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
      firstName: ['', [Validators.required]],
      lastName: ['', [Validators.required]],
      email: ['', [Validators.required, Validators.email]],
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
  }

  // Load user details for editing
  loadUserDetails(userId: string): void {
    this.dataService.getEntityById(`${this.apiUrl}/GetUser`, userId).subscribe(
      (user) => {
        this.userForm.patchValue(user);
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
    }
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
