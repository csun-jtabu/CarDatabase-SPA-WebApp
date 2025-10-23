import { Routes } from '@angular/router';
import { City } from './city/city';
import { Country } from './country/country';
import { Home } from './home/home';
import { Weather } from './weather/weather';
import { Model } from './model/model';
import { Make } from './make/make';
import { CarMakeModelCount } from './car-make-model-count/car-make-model-count';


export const routes: Routes = [
  {path: '', component: Home, pathMatch: 'full'},
  {path: 'city', component: City},
  {path: 'country', component: Country},
  {path: 'model', component: Model},
  {path: 'make', component: Make},
  {path: 'weather', component: Weather},
  {path: 'car-make-model-count/:id', component: CarMakeModelCount}
];
