import { Component } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { UserService, UserRegisterDto } from '../../services/user.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-register',
  templateUrl: './register.component.html',
  standalone: false
})
export class RegisterComponent {
  registerForm: FormGroup;
  isSubmitting: boolean = false;

  constructor(private fb: FormBuilder, private userService: UserService, private router:Router) {
    this.registerForm = this.fb.group({
      tc: ['', [Validators.required, Validators.pattern(/^\d{11}$/)]],
      email: ['', [Validators.required, Validators.email]],
      password: ['', [Validators.required, Validators.minLength(6)]],
      rePassword: ['', Validators.required]
    });
  }

  onSubmit(): void {
    if (this.registerForm.invalid) {
      this.registerForm.markAllAsTouched();
      return;
    }

    const user: UserRegisterDto = this.registerForm.value;

    if (user.password !== user.rePassword) {
      alert('Şifreler uyuşmuyor!');
      return;
    }

    this.isSubmitting = true;
    this.registerForm.disable();

    this.userService.register(user).subscribe({
      next: () => {
        alert('Kayıt başarılı!');
        this.registerForm.reset();
        this.router.navigate(['/login']);
      },
      error: err => {
        alert('Hata oluştu: ' + (err.error?.message || err.message));
      },
      complete: () => {
        this.isSubmitting = false;
        this.registerForm.enable();
      }
    });
  }



 showPassword = false;

togglePasswordVisibility() {
  this.showPassword = !this.showPassword;
}
  

showRePassword = false;

toggleRePasswordVisibility() {
  this.showRePassword = !this.showRePassword;
}





}
