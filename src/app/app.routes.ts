import { Routes } from '@angular/router';
import { City } from './city/city';
import { Country } from './country/country';
import { Home } from './home/home';
import { Weather } from './weather/weather';


export const routes: Routes = [
  {path: '', component: Home, pathMatch: 'full'},
  {path: 'city', component: City},
  {path: 'country', component: Country},
  {path: 'weather', component: Weather},
];
