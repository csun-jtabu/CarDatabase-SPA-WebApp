import { Component } from '@angular/core';
import { MakeData } from './make-data';
import { HttpClient } from '@angular/common/http';
import { RouterLink } from '@angular/router';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-make',
  imports: [RouterLink, AsyncPipe],
  templateUrl: './make.html',
  styleUrl: './make.scss'
})
export class Make {
  makes$: Observable<MakeData[]>;

  constructor(private http: HttpClient) 
  {
    this.makes$ = http.get<MakeData[]>(environment.apiUrl + "api/CarMakes");
  }
}
