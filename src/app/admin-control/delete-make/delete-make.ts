import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { AdminControlService } from '../admin-control-service';
import { Router } from '@angular/router';
import { MakeData } from '../../make/make-data';

@Component({
  selector: 'app-delete-make',
  imports: [ReactiveFormsModule],
  templateUrl: './delete-make.html',
  styleUrl: './delete-make.scss'
})
export class DeleteMake implements OnInit {

  form!:FormGroup;
  submitted = false;
  errorMessage: string | null = null;
  deletedMake: MakeData | null = null;

  constructor(private adminService: AdminControlService, private router: Router) {}

  // Initializes the form for add make component (form containing make and origin fields).
  ngOnInit(): void {
      this.form = new FormGroup({
        'makeId': new FormControl<number | null>(null, [Validators.required, Validators.min(1), Validators.pattern(/^\d+$/)])
    });
  }

  // Submits the add make form and calls the 
  onSubmit(): void {
    this.submitted = false;
    this.errorMessage = null;
    const makeId = this.form.controls['makeId'].value!;
    
    if (this.form.invalid) 
    {
      this.errorMessage = 'Form is invalid. Please correct the errors and try again.';
      return;
    }    

    this.adminService.deleteMake(makeId).subscribe({
        next: result => 
        {
          console.log(result);
          this.deletedMake = result;
          this.submitted = true;
          this.form.reset();  
        },
        error: result => 
        {
          if (result.status === 403) 
          {
            this.errorMessage = 'Entry not submitted. Error 403. You do not have permission to perform this action.';
          }
          else if (result.status === 400) 
          {
            this.errorMessage = 'Cannot delete this make. Delete all associated models first.';
          } 
          else if (result.status === 404) {
            this.errorMessage = 'Car make not found.'; 
          }
          else 
          {
            this.errorMessage = `Error occurred! Status code: ${result.status}`;
          }
          this.form.reset(); 
        }
    });
  }
}
