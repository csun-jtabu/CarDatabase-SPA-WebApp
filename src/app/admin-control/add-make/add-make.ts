import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { AdminControlService } from '../admin-control-service';
import { Router } from '@angular/router';
import { CarMakeCreate } from './car-make-create';

@Component({
  selector: 'app-add-make',
  imports: [ReactiveFormsModule],
  templateUrl: './add-make.html',
  styleUrl: './add-make.scss'
})
export class AddMake implements OnInit {
  
  form!:FormGroup;
  submitted = false;
  errorMessage: string | null = null;

  constructor(private adminService: AdminControlService, private router: Router)
  {}

  // Validator method to check for whitespace-only input
  noWhitespaceValidator(control: AbstractControl): ValidationErrors | null {
    let value = control.value || '';
    let isValid = false;
    
    if (value.trim().length > 0) 
    {
      isValid = true;
    }

    if (isValid) 
    {
      return null;
    } else 
    {  
      return { whitespace: true };
    }
  }

  // Initializes the form for add make component (form containing make and origin fields).
  ngOnInit(): void {
      this.form = new FormGroup({
        'make': new FormControl<string>('', [Validators.required, Validators.maxLength(30), this.noWhitespaceValidator]),
        'origin': new FormControl<string>('', [Validators.required, Validators.maxLength(30), this.noWhitespaceValidator])
    });
  }

  // Submits the add make form and calls the 
  onSubmit(): void {
    this.submitted = false;
    this.errorMessage = null;
    
    if (this.form.invalid) {
      this.errorMessage = 'Form is invalid. Please correct the errors and try again.';
      return;
    }
    
    let carMakeDetails = <CarMakeCreate> {
      make: this.form.controls['make'].value,
      origin: this.form.controls['origin'].value
    };

    this.adminService.addMake(carMakeDetails).subscribe({
        next: result => {
          console.log(result);
          // this.router.navigate(['/admin/makes']);
          this.submitted = true;
          this.form.reset();  
        },
        error: result => {
          if (result.status === 403) 
          {
            this.errorMessage = 'Entry not submitted. Error 403. You do not have permission to perform this action.';
          } else 
          {
            this.errorMessage = `Error occurred! Status code: ${result.status}`;
          }
          this.form.reset(); 
    }});
  }

}
