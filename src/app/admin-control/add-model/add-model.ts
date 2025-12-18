import { Component, OnInit } from '@angular/core';
import { AbstractControl, FormControl, FormGroup, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { AdminControlService } from '../admin-control-service';
import { Router } from '@angular/router';
import { CarModelCreate } from './car-model-create';
import { MakeData } from '../../make/make-data';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { HttpClient } from '@angular/common/http';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-add-model',
  imports: [ReactiveFormsModule, AsyncPipe],
  templateUrl: './add-model.html',
  styleUrl: './add-model.scss'
})
export class AddModel implements OnInit {

  form!:FormGroup;
  submitted = false;
  errorMessage: string | null = null;
  makes$ : Observable<MakeData[]>;

  constructor(private adminService: AdminControlService, private router: Router, private http: HttpClient)
  {
    this.makes$ = http.get<MakeData[]>(environment.apiUrl + "api/CarMakes");
  }

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

  // Initializes the form for add model components 
  // with validation rules like minimum values and required fields
  ngOnInit(): void {
      this.form = new FormGroup({
        'makeId': new FormControl<number | null>(null, [Validators.required, Validators.min(1), Validators.pattern(/^\d+$/)]),
        'model': new FormControl<string>('', [Validators.required, Validators.maxLength(50), this.noWhitespaceValidator]),
        'mpg': new FormControl<number | null>(null, [Validators.required, Validators.min(0.01)]),
        'cylinders': new FormControl<number | null>(null, [Validators.required, Validators.min(1), Validators.pattern(/^\d+$/)]),
        'displacement': new FormControl<number | null>(null, [Validators.required, Validators.min(0.01)]),
        'horsepower': new FormControl<number | null>(null, [Validators.required, Validators.min(1), Validators.pattern(/^\d+$/)]),
        'weight': new FormControl<number | null>(null, [Validators.required, Validators.min(1), Validators.pattern(/^\d+$/)]),
        'acceleration': new FormControl<number | null>(null, [Validators.required, Validators.min(0.01)]),
        'modelYear': new FormControl<number | null>(null, [Validators.required, Validators.min(1886), Validators.pattern(/^\d+$/)])
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
    
    let carModelDetails = <CarModelCreate> {
      makeId: this.form.controls['makeId'].value!,
      model: this.form.controls['model'].value!,
      mpg: this.form.controls['mpg'].value!,
      cylinders: this.form.controls['cylinders'].value!,
      displacement: this.form.controls['displacement'].value!,
      horsepower: this.form.controls['horsepower'].value!,
      weight: this.form.controls['weight'].value!,
      acceleration: this.form.controls['acceleration'].value!,
      modelYear: this.form.controls['modelYear'].value!
    };

    this.adminService.addModel(carModelDetails).subscribe({
        next: result => {
          console.log(result);
          this.submitted = true;
          this.form.reset();  
        },
        error: result => {
          if (result.status === 403) 
          {
            this.errorMessage = 'Entry not submitted. Error 403. You do not have permission to perform this action.';
          }
          else if (result.status === 400)
          {
            this.errorMessage = 'Entry not submitted. Error 400. Bad Request - Please check the data you have entered. Make sure Make ID exists.';
          } 
          else 
          {
            this.errorMessage = `Error occurred! Status code: ${result.status}`;
          }
          this.form.reset(); 
    }});
  }
}
