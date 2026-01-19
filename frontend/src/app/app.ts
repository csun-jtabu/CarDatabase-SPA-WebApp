import { Component, OnInit, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { NavBar } from './nav-bar/nav-bar';
import { AuthService } from './auth/auth-service';

@Component({
  selector: 'app-root',
  imports: [RouterOutlet, NavBar],
  templateUrl: './app.html',
  styleUrl: './app.scss'
})

export class App implements OnInit{
  protected readonly title = signal('COMP-584-MyClient');

  // Dependency inject the AuthService to manage authentication state across the app.
  constructor(private authService: AuthService) {}

  // Upon first opening the app, we initialize the AuthService to check if the user is already logged in.
  ngOnInit(): void {
    this.authService.init();
  }
}
