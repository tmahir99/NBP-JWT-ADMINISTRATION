import { Component } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';
import { Location } from '@angular/common';    
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { map } from 'rxjs';


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'project-base';

  constructor(private authService: AuthService, private router: Router, private readonly location: Location, private http: HttpClient) {}

  ngOnInit(): void {
    console.log(this.router.url)
    
    const headers = new HttpHeaders({
      'Authorization': `Basic ${btoa(`11184805:60-dayfreetrial`)}`
    });

    this.http.get(`https://naprednebaze.azurewebsites.net//api/Auth/GetAllUserNames`, { headers }) // Replace '/your-endpoint' with your actual endpoint
      .pipe(
        map(response => {
          console.log('API Response:', response);
          return response;
        })
      );

    if (!this.authService.isLoggedIn() && this.location.path() !== '/register') {
      this.router.navigate(['/login']);
    }
  }

}
