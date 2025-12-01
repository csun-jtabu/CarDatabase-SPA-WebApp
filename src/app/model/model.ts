import { Component } from '@angular/core';
import { ModelData } from './model-data';
import { Observable } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { RouterLink } from '@angular/router';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-model',
  imports: [AsyncPipe],
  templateUrl: './model.html',
  styleUrl: './model.scss'
})
export class Model {
  models$: Observable<ModelData[]>;

  constructor(private http: HttpClient) 
  {
    this.models$ = http.get<ModelData[]>(environment.apiUrl + "api/CarModels");
  }
}
