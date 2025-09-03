import { Component } from '@angular/core';
import { FormBuilder, Validators, FormGroup } from '@angular/forms';
import { UserService, UserLoginDto } from '../../services/user.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  standalone: false
})
export class LoginComponent {

  loginForm!: FormGroup;
  loading = false;
  showPassword = false;

  constructor(
    private fb: FormBuilder,
    private userService: UserService
  ) {
    this.loginForm = this.fb.group({
      tc: ['', [Validators.required, Validators.pattern(/^\d{11}$/)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]]
    });
  }

  onSubmit(): void {
    if (this.loginForm.invalid) {
      this.loginForm.markAllAsTouched();
      return;
    }

    const loginDto: UserLoginDto = this.loginForm.value as UserLoginDto;
    this.loading = true;

    this.userService.login(loginDto).subscribe({
      next: (res) => {
        alert(`${res.message}\nTC: ${res.tc}\nEmail: ${res.email}`); 
        this.loading = false;
      },
      error: (err) => {
        const errorMessage =
          typeof err.error === 'string'
            ? err.error
            : err.error?.message || 'Sunucu hatası';
        alert('Hata oluştu: ' + errorMessage);
        this.loading = false;
      }
    });
  }

  togglePasswordVisibility() {
    this.showPassword = !this.showPassword;
  }
}
