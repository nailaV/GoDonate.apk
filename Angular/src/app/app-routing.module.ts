import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {AboutComponent} from "./about/about.component";
import {LogInComponent} from "./log-in/log-in.component";
import {StoriesComponent} from "./stories/stories.component";
import {UserProfileComponent} from "./user-profile/user-profile.component";
import {RegistrationComponent} from "./registration/registration.component";

const routes: Routes = [
  {path: '', component:HomeComponent},
  {path: 'about', component:AboutComponent},
  {path: 'logIn', component:LogInComponent},
  {path: 'stories', component: StoriesComponent},
  {path: 'userProfile', component: UserProfileComponent},
  {path: 'registration', component: RegistrationComponent}
  ]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
