import { Component } from '@angular/core';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
})
export class RegisterComponent {

  user: any = {
    firstName: '',
    lastName: '',
    userName: '',
    email: '',
    password: ''
  };

  constructor(private authService: AuthService) { }

  onSubmit() {
    this.authService.register(this.user).subscribe(
      (response) => {
        console.log('Registration successful:', response);
        // Optionally, redirect the user to another page after successful registration
      },
      (error) => {
        console.error('Registration failed:', error);
        // Handle registration error, e.g., display an error message to the user
      }
    );
  }
}
