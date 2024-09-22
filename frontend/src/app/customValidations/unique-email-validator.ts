import {
  AbstractControl,
  ValidationErrors,
  AsyncValidatorFn,
} from '@angular/forms';
import { Observable, of } from 'rxjs';
import { debounceTime, map } from 'rxjs/operators';
import { ApiService } from '../Services/fake-data-service.service';

export function emailAsyncValidator(userService: ApiService): AsyncValidatorFn {
  return (control: AbstractControl): Observable<ValidationErrors | null> => {
    // if it has errors, return without calling the API
    if (control.errors) {
      return of(null);
    }
    // get the userId from the root Form Group // for Edit scenarios
    const userId = control.root.get('id');
    return userService
      .isEmailUnique(
        'https://localhost:7237/api/Users/IsUnique',
        control.value,
        userId?.value || '0'
      )
      .pipe(
        // debounceTime(300),
        map(
         (isUnique) => 
            {
            if (!isUnique) {
                // return the error if the email is not unique
                return { emailTaken: true };
                }
            // return null if the email is unique
            return null;
         }
        )
      );
  };
}
