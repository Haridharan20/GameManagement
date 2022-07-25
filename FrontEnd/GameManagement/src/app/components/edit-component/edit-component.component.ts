import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { WeaponService } from 'src/app/services/weapon.service';

@Component({
  selector: 'app-edit-component',
  templateUrl: './edit-component.component.html',
  styleUrls: ['./edit-component.component.css'],
})
export class EditComponentComponent implements OnInit {
  editForm!: FormGroup;
  id!: number;
  name!: any;
  price!: any;
  constructor(
    public service: AuthService,
    private route: ActivatedRoute,
    private weaponService: WeaponService,
    private router: Router
  ) {
    this.route.params.subscribe({
      next: (param: Params) => {
        this.id = +param['id'];
      },
    });
  }

  ngOnInit(): void {
    this.weaponService.getWeapon(this.id).subscribe({
      next: (res: any) => {
        this.name = res.name;
        this.price = res.price;
      },
    });
  }

  onSubmit() {
    this.weaponService.editWeapon(this.name, this.price, this.id).subscribe({
      next: (res: any) => {
        alert(res.message);
        this.router.navigate(['']);
      },
      error: (err: any) => {
        alert(err.error.message);
      },
    });
  }
}
