import { Component } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { ModelData } from '../../model/model-data';
import { AdminControlService } from '../admin-control-service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-delete-model',
  imports: [ReactiveFormsModule],
  templateUrl: './delete-model.html',
  styleUrl: './delete-model.scss'
})
export class DeleteModel {

  form!:FormGroup;
  submitted = false;
  errorMessage: string | null = null;
  deletedModel: ModelData | null = null;

  constructor(private adminService: AdminControlService, private router: Router) {}

  // Initializes the form for add make component (form containing make and origin fields).
  ngOnInit(): void {
      this.form = new FormGroup({
        'modelId': new FormControl<number | null>(null, [Validators.required, Validators.min(1), Validators.pattern(/^\d+$/)])
      });
  }

  // Submits the add make form and calls the 
  onSubmit(): void {
    this.submitted = false;
    this.errorMessage = null;
    const modelId = this.form.controls['modelId'].value!;
    
    if (this.form.invalid) 
    {
      this.errorMessage = 'Form is invalid. Please correct the errors and try again.';
      return;
    }    

    this.adminService.deleteModel(modelId).subscribe({
        next: result => 
        {
          console.log(result);
          this.deletedModel = result;
          this.submitted = true;
          this.form.reset();  
        },
        error: result => 
        {
          if (result.status === 403) 
          {
            this.errorMessage = 'Entry not submitted. Error 403. You do not have permission to perform this action.';
          } 
          else if (result.status === 404) 
          {
            this.errorMessage = 'Car model not found.';
          } else 
          {
            this.errorMessage = `Error occurred! Status code: ${result.status}`;
          }
          this.form.reset(); 
        }
    });
  }
}
