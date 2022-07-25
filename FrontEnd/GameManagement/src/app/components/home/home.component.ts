import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { WeaponService } from 'src/app/services/weapon.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.css'],
})
export class HomeComponent implements OnInit {
  weapons!: [any];
  constructor(
    public service: AuthService,
    private weaponService: WeaponService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.weaponService.getWeapons().subscribe({
      next: (result: any) => {
        this.weapons = result;
      },
      error: (err: any) => {
        alert(err.error);
      },
    });
  }
  onBuy(id: any) {
    console.log(id);
    this.router.navigate([`/buy/${id}`]);
  }

  onEdit(id: any) {
    console.log(id);
    this.router.navigate([`/edit/${id}`]);
  }
  onDelete(id: any) {
    this.weaponService.deleteWeapon(id).subscribe({
      next: (res: any) => {
        this.ngOnInit();
        alert(res.message);
      },
      error: (err: any) => {
        alert('Error Occured');
      },
    });
  }
}
