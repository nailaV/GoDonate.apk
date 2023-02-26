import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import {HomeComponent} from "./home/home.component";
import {AboutComponent} from "./about/about.component";
import {LogInComponent} from "./log-in/log-in.component";
import {StoriesComponent} from "./stories/stories.component";
import {UserProfileComponent} from "./user-profile/user-profile.component";
import {RegistrationComponent} from "./registration/registration.component";
import {StoryDetailsComponent} from "./story-details/story-details.component";
import {DonationComponent} from "./donation/donation.component";
import {AdministratorComponent} from "./administrator/administrator.component";
import {ProvjeraAktivacija} from "./guard/Guard";
import {AdminAktivacija} from "./guard/AdminGuard";
import {Aktivacija} from "./guard/HomeAboutLogIn";

const routes: Routes = [
  {path: '', component:HomeComponent,canActivate:[Aktivacija]},
  {path: 'about', component:AboutComponent,canActivate:[Aktivacija]},
  {path: 'logIn', component:LogInComponent,canActivate:[Aktivacija]},
  {path: 'stories', component: StoriesComponent, canActivate:[ProvjeraAktivacija]},
  {path: 'userProfile', component: UserProfileComponent, canActivate:[ProvjeraAktivacija]},
  {path: 'registration', component: RegistrationComponent, canActivate:[ProvjeraAktivacija]},
  {path: 'storyDetails/:storyId', component: StoryDetailsComponent, canActivate:[ProvjeraAktivacija]},
  {path:'donation', component: DonationComponent, canActivate:[ProvjeraAktivacija]},
  {path:'admin',component:AdministratorComponent, canActivate:[AdminAktivacija]}
  ]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers:[ProvjeraAktivacija,AdminAktivacija,Aktivacija]
})
export class AppRoutingModule { }
