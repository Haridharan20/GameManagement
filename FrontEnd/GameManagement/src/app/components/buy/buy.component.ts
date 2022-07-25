import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params, Router } from '@angular/router';
import { WeaponService } from 'src/app/services/weapon.service';

@Component({
  selector: 'app-buy',
  templateUrl: './buy.component.html',
  styleUrls: ['./buy.component.css'],
})
export class BuyComponent implements OnInit {
  id!: number;
  weapon!: any;
  constructor(
    private route: ActivatedRoute,
    private weaponService: WeaponService,
    private router: Router
  ) {
    this.route.params.subscribe({
      next: (result: Params) => {
        this.id = +result['id'];
      },
    });
  }

  ngOnInit(): void {
    this.weaponService.getWeapon(this.id).subscribe({
      next: (result: any) => {
        console.log(result);
        this.weapon = result;
      },
    });
  }

  onBuy(price: number) {
    this.weaponService.buyWeapon(this.id, price).subscribe({
      next: (result: any) => {
        alert('Purchased Successfully');
        this.router.navigate(['/myweapons']);
      },
      error: (err: any) => {
        alert(err.error);
      },
    });
  }
}
