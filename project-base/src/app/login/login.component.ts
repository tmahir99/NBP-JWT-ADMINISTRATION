import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from '../auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
})
export class LoginComponent {
  loginForm: FormGroup;

  constructor(
    private fb: FormBuilder,
    private authService: AuthService,
    private router: Router
  ) {
    this.loginForm = this.fb.group({
      username: ['', Validators.required],
      password: ['', Validators.required],
    });
  }

  onSubmit() {
    if (this.loginForm.valid) {
      const { username, password } = this.loginForm.value;
      this.authService.login(username, password).subscribe(
        (response) => {
          this.authService.storeToken(response.message);
          this.authService.storeUserName(response.firstName);
          this.authService.storeUserSurname(response.lastName);
          this.authService.storeUserUsername(response.userName);
          this.authService.storeUserRoles(response.roles);

          this.router.navigate(['/home']);
        },
        (error) => {
          console.error('Login error', error);
        }
      );
    }
  }
}
