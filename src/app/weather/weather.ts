import { HttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { WeatherData } from '../weather-data';

@Component({
  selector: 'app-weather',
  imports: [],
  templateUrl: './weather.html',
  styleUrl: './weather.scss'
})
export class Weather {
  forecasts: WeatherData[] = [];

  constructor(http: HttpClient) 
  {
    http.get<WeatherData[]>('https://localhost:7260/weatherforecast').subscribe(result => {
      this.forecasts = result;
    });

  }
}
