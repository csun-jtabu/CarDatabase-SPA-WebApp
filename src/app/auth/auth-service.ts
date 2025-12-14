import { Injectable } from '@angular/core';
import { LoginRequest } from './login-request';
import { LoginResponse } from './login-response';
import { Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';


@Injectable({
  providedIn: 'root'
})

export class AuthService {
  
  // This is the jwt token key that will be stored in the browser's local storage upon successful login.
  private token = 'auth_token';
  
  // We import HttpClient to make making HTTP requests in this component. Using dependency injection 
  constructor(private http: HttpClient) {}

  // This method performs the login operation by sending a POST request to the backend API with the provided login credentials
  login(loginRequest: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(environment.apiUrl + "api/Admin", loginRequest)
    .pipe(tap(response => {
      if(response.success)
      {
        localStorage.setItem(this.token, response.token);
      }
      
    }));
  }

  // This method checks if the user is currently logged in by verifying the presence of the JWT token in local storage.
  isLoggedIn(): boolean {
    return localStorage.getItem(this.token) != null;
  }


  // This method logs out the user by removing the JWT token from local storage.
  logout(): void {
    localStorage.removeItem(this.token);
  }
}
