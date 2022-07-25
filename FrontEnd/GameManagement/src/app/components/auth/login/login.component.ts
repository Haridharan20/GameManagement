import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css'],
})
export class LoginComponent implements OnInit {
  loginForm!: FormGroup;
  constructor(
    private fb: FormBuilder,
    public authService: AuthService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loginForm = this.fb.group({
      username: [null, [Validators.required]],
      password: [null, [Validators.required]],
    });
  }

  onSubmit() {
    const { username: email, password } = this.loginForm.value;
    this.authService.Login(email, password).subscribe({
      next: (result: any) => {
        console.log(result);
        this.authService.setToken(result.message, result.userId);
        alert('Login Successfully');
        this.router.navigate(['/home']);
      },
      error: (err: any) => {
        alert('user ' + err.error.message);
      },
    });
  }
}
