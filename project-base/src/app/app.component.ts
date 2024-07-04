import { Component } from '@angular/core';
import { AuthService } from './auth.service';
import { Router } from '@angular/router';
import { Location } from '@angular/common';    


@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
})
export class AppComponent {
  title = 'project-base';

  constructor(private authService: AuthService, private router: Router, private readonly location: Location) {}

  ngOnInit(): void {
    console.log(this.router.url)

    if (!this.authService.isLoggedIn() && this.location.path() !== '/register') {
      this.router.navigate(['/login']);
    }
  }

}
