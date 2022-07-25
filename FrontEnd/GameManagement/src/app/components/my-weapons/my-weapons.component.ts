import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Params } from '@angular/router';
import { AuthService } from 'src/app/services/auth.service';
import { UserServiceService } from 'src/app/services/user-service.service';

@Component({
  selector: 'app-my-weapons',
  templateUrl: './my-weapons.component.html',
  styleUrls: ['./my-weapons.component.css'],
})
export class MyWeaponsComponent implements OnInit {
  id!: any;
  myWeapons!: [any];

  constructor(
    public service: AuthService,
    private userService: UserServiceService
  ) {}

  ngOnInit(): void {
    this.id = localStorage.getItem('uid');
    this.userService.GetMyWeapons(this.id).subscribe({
      next: (res: any) => {
        console.log(res);
        this.myWeapons = res.message;
      },
      error: (err: any) => {
        alert(err.error);
      },
    });
  }
}
