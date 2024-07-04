import { Component } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { AuthService } from '../auth.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css']

})
export class HomeComponent {

  userRole: string = this.authService.getUserRoles()?.toString() || '';
  response: string = '';
  userFullName: string = this.authService.getUserUsername()?.toString() || '';


  constructor(
    private authService: AuthService,
    private http: HttpClient,
    private router: Router
  ) { }

}

