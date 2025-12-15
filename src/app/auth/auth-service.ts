import { Injectable, OnInit } from '@angular/core';
import { LoginRequest } from './login-request';
import { LoginResponse } from './login-response';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment.development';
import { Router } from '@angular/router';


@Injectable({
  providedIn: 'root'
})

export class AuthService {
  
  // This is the jwt token key that will be stored in the browser's local storage upon successful login.
  private token = 'auth_token';
  
  private _authStatus = new BehaviorSubject<boolean>(false);
  public authStatus = this._authStatus.asObservable();

  // We import HttpClient to make making HTTP requests in this component. Using dependency injection 
  // We also import Router to redirect users upon logout.
  constructor(private http: HttpClient, private router: Router) {}

  // This initializes the authentication service and checks if the user is already logged in upon service creation.
  init()
  {
    if(this.isLoggedIn())
    {
      this.setAuthStatus(true);
    }
  }

  setAuthStatus(isLoggedIn: boolean) {
    this._authStatus.next(isLoggedIn);
  }

  // This method performs the login operation by sending a POST request to the backend API with the provided login credentials
  login(loginRequest: LoginRequest): Observable<LoginResponse> {
    return this.http.post<LoginResponse>(environment.apiUrl + "api/Admin", loginRequest)
    .pipe(tap(response => {
      if(response.success)
      {
        localStorage.setItem(this.token, response.token);
        this.setAuthStatus(true);
      }
      
    }));
  }

  getToken(): string | null {
    return localStorage.getItem(this.token);
  }

  // This method checks if the JWT token is expired by decoding its payload and comparing the expiration time with the current time.
  isTokenExpired(token: string): boolean {
    const payload = JSON.parse(atob(token.split('.')[1]));
    return Date.now() >= payload.exp * 1000;
  }

  // This method checks if the user is currently logged in by verifying the presence of the JWT token in local storage.
  isLoggedIn(): boolean {
    const token = this.getToken();

    if (!token)
    {
      return false;
    } 
      
    // Token is expired, remove it from storage
    if (this.isTokenExpired(token)) {
      localStorage.removeItem(this.token);
      this.setAuthStatus(false);
      return false;
    }

    return true;
  }

  // This method logs out the user by removing the JWT token from local storage.
  // We also make it so that the user is then redirected to the login page after logging out.
  logout(): void {
    localStorage.removeItem(this.token);
    this.setAuthStatus(false);
    this.router.navigate(['/login']); // redirect after logout
  }


}
