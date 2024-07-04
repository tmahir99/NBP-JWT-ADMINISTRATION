import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']

})
export class HomeComponent implements OnInit {

  userRole: string = this.authService.getUserRoles()?.toString() || '';
  response: string = '';
  userFullName: string = this.authService.getUserUsername()?.toString() || '';


  constructor(
    private authService: AuthService,
    private http: HttpClient,
    private router: Router
  ) { }

  ngOnInit(): void {
    this.getUserRole();
  }


  getUserRole() {
    const token = this.authService.getToken(); // Retrieve token from AuthService

    if (token) {
      const headers = new HttpHeaders({
        'Content-Type': 'application/json',
        'Authorization': `Bearer ${token}` // Set bearer token in request headers
      });


      this.http.get<any>('https://localhost:7082/api/Auth/GetAllUsers', { headers: headers })
        .subscribe(
          (response) => {
            console.log(response)
            this.response = response;
          },
          (error) => {
            console.error('Error fetching user role:', error);
          }
        );
    } else {
      console.error('Token not found.');
      this.router.navigate(['/login']);
    }
  }

}

