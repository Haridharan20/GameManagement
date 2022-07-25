import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { LoginComponent } from './components/auth/login/login.component';
import { SignupComponent } from './components/auth/signup/signup.component';
import { BuyComponent } from './components/buy/buy.component';
import { CreateComponentComponent } from './components/create-component/create-component.component';
import { EditComponentComponent } from './components/edit-component/edit-component.component';
import { HomeComponent } from './components/home/home.component';
import { MyWeaponsComponent } from './components/my-weapons/my-weapons.component';
import { UsersComponent } from './components/users/users.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: '/home',
    pathMatch: 'full',
  },
  {
    path: 'home',
    component: HomeComponent,
  },
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'signup',
    component: SignupComponent,
  },
  {
    path: 'buy/:id',
    component: BuyComponent,
  },
  {
    path: 'edit/:id',
    component: EditComponentComponent,
  },
  {
    path: 'users',
    component: UsersComponent,
  },
  {
    path: 'create',
    component: CreateComponentComponent,
  },
  {
    path: 'myweapons',
    component: MyWeaponsComponent,
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
