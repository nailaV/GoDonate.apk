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

const routes: Routes = [
  {path: '', component:HomeComponent},
  {path: 'about', component:AboutComponent},
  {path: 'logIn', component:LogInComponent},
  {path: 'stories', component: StoriesComponent},
  {path: 'userProfile', component: UserProfileComponent},
  {path: 'registration', component: RegistrationComponent},
  {path: 'storyDetails/:storyId', component: StoryDetailsComponent},
  {path:'donation', component: DonationComponent},
  {path:'admin',component:AdministratorComponent}
  ]

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
