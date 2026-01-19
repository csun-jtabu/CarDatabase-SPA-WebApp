import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CarMakeCreate } from './add-make/car-make-create';
import { MakeData } from '../make/make-data';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment';
import { CarModelCreate } from './add-model/car-model-create';
import { ModelData } from '../model/model-data';

@Injectable({
  providedIn: 'root'
})
export class AdminControlService {
  
  constructor(private http: HttpClient, private router: Router) {}

  addMake(carMake: CarMakeCreate): Observable<MakeData> {
    return this.http.post<MakeData>(environment.apiUrl + "api/CarMakes", carMake);
  }

  deleteMake(id: number): Observable<MakeData> {
    return this.http.delete<MakeData>(`${environment.apiUrl}api/CarMakes/${id}`);
  }

  addModel(carModel: CarModelCreate): Observable<ModelData> {
    return this.http.post<ModelData>(environment.apiUrl + "api/CarModels", carModel);
  }

  deleteModel(id: number): Observable<ModelData> {
    return this.http.delete<ModelData>(`${environment.apiUrl}api/CarModels/${id}`);
  }
  
}
