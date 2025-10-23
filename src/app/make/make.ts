import { Component } from '@angular/core';
import { MakeData } from './make-data';
import { HttpClient } from '@angular/common/http';
import { RouterLink } from '@angular/router';
import { environment } from '../../environments/environment';

@Component({
  selector: 'app-make',
  imports: [RouterLink],
  templateUrl: './make.html',
  styleUrl: './make.scss'
})
export class Make {
  makes: MakeData[] = [];

  constructor(http: HttpClient) 
  {
    http.get<MakeData[]>(environment.apiUrl + "api/CarMakes").subscribe(result => {
      this.makes = result;
    });

  }
}
