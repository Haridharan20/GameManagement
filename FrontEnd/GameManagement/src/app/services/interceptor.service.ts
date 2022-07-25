import {
  HttpEvent,
  HttpHandler,
  HttpHeaders,
  HttpInterceptor,
  HttpParams,
  HttpRequest,
} from '@angular/common/http';
import { Injectable } from '@angular/core';
import { finalize, Observable } from 'rxjs';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root',
})
export class InterceptorService implements HttpInterceptor {
  constructor(private authService: AuthService) {}
  intercept(
    req: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    this.authService.isLoading.next(true);
    let token = this.authService.getToken();
    const modifiedReq = req.clone({
      setHeaders: {
        Authorization: `bearer ${token}`,
      },
    });
    return next.handle(modifiedReq).pipe(
      finalize(() => {
        this.authService.isLoading.next(false);
      })
    );
  }
}
