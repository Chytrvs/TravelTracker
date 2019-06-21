import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { FlightComponent } from './flight/flight.component';
import { AuthGuard } from './_guards/auth.guard';
import { MapComponent } from './map/map.component';

const routes: Routes = [
  {path: 'home', component: HomeComponent},
  {path: 'flight', component: FlightComponent, canActivate:[AuthGuard]},
  {path: 'map', component: MapComponent, canActivate:[AuthGuard]},
  {path:'**', redirectTo:'home', pathMatch:'full'}
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
