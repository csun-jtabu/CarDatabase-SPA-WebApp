import { Routes } from '@angular/router';
import { Home } from './home/home';
import { Model } from './model/model';
import { Make } from './make/make';
import { CarMakeModelCount } from './car-make-model-count/car-make-model-count';
import { Login } from './auth/login';
import { AddMake } from './admin-control/add-make/add-make';
import { DeleteMake } from './admin-control/delete-make/delete-make';


export const routes: Routes = [
  {path: '', component: Home, pathMatch: 'full'},
  {path: 'model', component: Model},
  {path: 'make', component: Make},
  {path: 'car-make-model-count/:id', component: CarMakeModelCount},
  {path: 'login', component: Login},
  {path: 'add-make', component: AddMake},
  {path: 'delete-make', component: DeleteMake}
];
