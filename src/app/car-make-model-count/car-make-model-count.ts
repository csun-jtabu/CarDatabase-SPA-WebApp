import { Component, OnInit } from '@angular/core';
import { ModelData } from './model-data';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ActivatedRoute, RouterLink } from '@angular/router';

@Component({
  selector: 'app-car-make-model-count',
  imports: [RouterLink],
  templateUrl: './car-make-model-count.html',
  styleUrl: './car-make-model-count.scss'
})
export class CarMakeModelCount implements OnInit {
  makeModelCount!: ModelData;

  constructor(private http: HttpClient, private activatedRoute: ActivatedRoute) 
  {

  }
  ngOnInit(): void {
    let idParam = this.activatedRoute.snapshot.paramMap.get('id');

    this.http.get<ModelData>(`${environment.apiUrl}api/CarMakes/modelcount/${idParam}`).subscribe(result => {
      this.makeModelCount = result;
    });
  }
}
