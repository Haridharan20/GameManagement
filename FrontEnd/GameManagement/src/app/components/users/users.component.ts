import { Component, OnInit } from '@angular/core';
import { AuthService } from 'src/app/services/auth.service';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css'],
})
export class UsersComponent implements OnInit {
  users!: [any];
  constructor(public service: AuthService) {}

  ngOnInit(): void {
    console.log('demo');
    this.service.getUsers().subscribe({
      next: (res: any) => {
        this.users = res;
      },
    });
  }

  onChange(id: number, admin: number): any {
    let currUser = localStorage.getItem('uid') || '';
    if (currUser != '') {
      if (Number(currUser) == id) {
        alert('Action not performed by You');
        return false;
      }
    }
    let val = admin == 1 ? 0 : 1;
    this.service.changeAdmin(id, val).subscribe({
      next: (res: any) => {
        console.log(res);
        this.ngOnInit();
      },
      error: (err: any) => {
        alert(err.error);
      },
    });
  }
}
