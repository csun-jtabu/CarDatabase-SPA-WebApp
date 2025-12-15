import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { CarMakeCreate } from './add-make/car-make-create';
import { MakeData } from '../make/make-data';
import { Observable } from 'rxjs';
import { environment } from '../../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class AdminControlService {
  
  constructor(private http: HttpClient, private router: Router) {}

  addMake(carMake: CarMakeCreate): Observable<MakeData> {
    return this.http.post<MakeData>(environment.apiUrl + "api/CarMakes", carMake);
  }
  
}
