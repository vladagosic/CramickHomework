import { HttpInterceptorFn } from '@angular/common/http';
import { inject } from '@angular/core';
import { AuthenticationService } from '../shared/services/authentication.service';

export const TokenInterceptor: HttpInterceptorFn = (request, next) => {
  const authenticationService = inject(AuthenticationService);
  if (authenticationService.isLoggedIn()) {
    const newRequest = request.clone({
      setHeaders: {
        Authorization: `Bearer ${authenticationService.getToken()}`,
      },
    });
    return next(newRequest);
  }

  return next(request);
};
