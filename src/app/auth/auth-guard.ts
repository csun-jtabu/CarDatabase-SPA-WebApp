import { ActivatedRouteSnapshot, CanActivateFn, Router, RouterStateSnapshot } from '@angular/router';
import { AuthService } from './auth-service';
import { inject } from '@angular/core';

export const authGuard: CanActivateFn = (next: ActivatedRouteSnapshot, state: RouterStateSnapshot) => {
  const authService:AuthService = inject(AuthService);
  const router:Router = inject(Router);
  
  // This is to handle anonymous user access to certain routes

  // If the user is authenticated, we allow access to the route
  if (authService.isLoggedIn()) {
    return true;
  }
  
  // If the user isn't authenticated, we redirect them to the login page
  return router.createUrlTree(['/login'], {
    queryParams: {returnUrl: state.url}
  });
};
