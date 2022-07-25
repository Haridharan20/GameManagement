import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { WeaponService } from 'src/app/services/weapon.service';

@Component({
  selector: 'app-create-component',
  templateUrl: './create-component.component.html',
  styleUrls: ['./create-component.component.css'],
})
export class CreateComponentComponent implements OnInit {
  createForm!: FormGroup;
  constructor(
    private fb: FormBuilder,
    private weaponService: WeaponService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.createForm = this.fb.group({
      name: [null, Validators.required],
      price: [null, Validators.required],
    });
  }

  onSubmit() {
    const { name, price } = this.createForm.value;
    console.log(name, price);
    this.weaponService.createWeapon(name, price).subscribe({
      next: (res: any) => {
        alert('Weapon created successfully');
        this.router.navigate(['']);
      },
      error: (err: any) => {
        alert(err.error);
      },
    });
  }
}
