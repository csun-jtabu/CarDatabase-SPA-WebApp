import { Component } from '@angular/core';
import { ModelData } from './model-data';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-car-make-model-count',
  imports: [],
  templateUrl: './car-make-model-count.html',
  styleUrl: './car-make-model-count.scss'
})
export class CarMakeModelCount {
  make!: ModelData;

  constructor(http: HttpClient) 
  {
    http.get<ModelData>(environment.apiUrl + "api/CarMakes/modelcount/26").subscribe(result => {
      this.make = result;
    });

  }
}
