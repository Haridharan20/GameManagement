import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-signup',
  templateUrl: './signup.component.html',
  styleUrls: ['./signup.component.css'],
})
export class SignupComponent implements OnInit {
  registerForm!: FormGroup;
  constructor(private fb: FormBuilder, public authService: AuthService) {}

  ngOnInit(): void {
    this.registerForm = this.fb.group({
      name: [null, Validators.required],
      email: [null, Validators.required],
      password: [null, Validators.required],
    });
  }

  onSubmit() {
    const { name, email, password } = this.registerForm.value;
    this.authService.Register(name, email, password).subscribe({
      next: (res: any) => {
        console.log(res);
      },
      error: (err: any) => {
        alert(err.error.message);
      },
    });
  }
}
