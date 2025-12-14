import { Routes } from '@angular/router';
import { Home } from './home/home';
import { Model } from './model/model';
import { Make } from './make/make';
import { CarMakeModelCount } from './car-make-model-count/car-make-model-count';
import { Login } from './auth/login';


export const routes: Routes = [
  {path: '', component: Home, pathMatch: 'full'},
  {path: 'model', component: Model},
  {path: 'make', component: Make},
  {path: 'car-make-model-count/:id', component: CarMakeModelCount},
  {path: 'login', component: Login}
];
