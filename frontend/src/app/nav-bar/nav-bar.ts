import { Component, OnDestroy, OnInit } from '@angular/core';
import { RouterLink } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatIconModule } from '@angular/material/icon';
import { MatButtonModule } from '@angular/material/button';
import { AuthService } from '../auth/auth-service';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-nav-bar',
  imports: [RouterLink, MatToolbarModule, MatIconModule, MatButtonModule],
  templateUrl: './nav-bar.html',
  styleUrl: './nav-bar.scss'
})

export class NavBar implements OnInit, OnDestroy{

  private destroy = new Subject();
  isLoggedIn! : boolean;

  constructor(public authService: AuthService) { 
      authService.authStatus.pipe(takeUntil(this.destroy)).subscribe(result => {
      this.isLoggedIn! = result;
    });
  }
  
  ngOnInit(): void {
    this.isLoggedIn! = this.authService.isLoggedIn();
  }
  ngOnDestroy(): void {
    this.destroy.next(true);
    this.destroy.complete();
  }

}
