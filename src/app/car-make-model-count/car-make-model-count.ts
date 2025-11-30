import { Component, OnInit } from '@angular/core';
import { ModelData } from './model-data';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ActivatedRoute, RouterLink } from '@angular/router';
import { Observable } from 'rxjs';
import { AsyncPipe } from '@angular/common';

@Component({
  selector: 'app-car-make-model-count',
  imports: [RouterLink, AsyncPipe],
  templateUrl: './car-make-model-count.html',
  styleUrl: './car-make-model-count.scss'
})
export class CarMakeModelCount implements OnInit {
  makeModelCount$!: Observable<ModelData>;

  constructor(private http: HttpClient, private activatedRoute: ActivatedRoute) 
  {

  }
  ngOnInit(): void {
    let idParam = this.activatedRoute.snapshot.paramMap.get('id');

    this.makeModelCount$ = this.http.get<ModelData>(`${environment.apiUrl}api/CarMakes/modelcount/${idParam}`);
  }
}
