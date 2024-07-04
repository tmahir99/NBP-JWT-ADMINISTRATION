import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpHandler,
  HttpRequest,
  HttpErrorResponse
} from '@angular/common/http';
import { catchError } from 'rxjs/operators';
import { throwError } from 'rxjs';
import { MatSnackBar, MatSnackBarConfig } from '@angular/material/snack-bar';

@Injectable()
export class HttpErrorInterceptor implements HttpInterceptor {

  constructor(private snackBar: MatSnackBar) {}

  intercept(request: HttpRequest<any>, next: HttpHandler) {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = '';
        if (error.error instanceof ErrorEvent) {
          // Client-side network error
          errorMessage = `Network Error: ${error.error.message}`;
        } else {
          // Server-side error
          if (error.status === 0) {
            errorMessage = 'Server Unreachable: Please check your internet connection';
          } else {
            switch (error.status) {
              case 400:
                errorMessage = 'Bad Request: The request was invalid';
                break;
              case 401:
                errorMessage = 'Unauthorized: You are not authorized to access the whole page.';
                break;
              case 403:
                errorMessage = 'Forbidden: You dont have the authorization to view the whole page';
                break;
              case 404:
                errorMessage = 'Not Found: The requested resource does not exist';
                break;
              case 500:
                errorMessage = 'Internal Server Error: Something went wrong on the server';
                break;
            case 415:
                errorMessage = 'Unsupported media type.';
                break;
              default:
                errorMessage = `Error: ${error.message}`;
                break;
            }
          }
        }

        this.showSnackBar(errorMessage, 'Error');
        return throwError(errorMessage);
      })
    );
  }

  private showSnackBar(message: string, action: string) {
    const config = new MatSnackBarConfig();
    config.duration = 5000; // 5 seconds
    config.horizontalPosition = 'right';
    config.verticalPosition = 'bottom';
    config.panelClass = ['error-snackbar']; // Custom CSS class for styling

    this.snackBar.open(message, action, config);
  }
}
