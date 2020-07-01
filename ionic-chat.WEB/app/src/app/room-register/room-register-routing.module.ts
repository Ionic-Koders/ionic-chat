import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule, PreloadAllModules } from '@angular/router';
import { RoomRegisterComponent } from './room-register.component';
import { RoomDashboardComponent } from './components/room-dashboard/room-dashboard.component';
import { RoomComponent } from './components/room/room.component';



const routes: Routes = [
  {
    path: '',
    redirectTo: 'room-dashboard',
    pathMatch: 'full',
  },
  {
    path: '',
    component: RoomRegisterComponent,
    children: [
      {
        path: 'room-dashboard',
        component: RoomDashboardComponent
      },
      {
        path: 'room',
        component: RoomComponent
      },
    ]
  },
];

@NgModule({
  imports: [
    RouterModule.forChild(routes)
  ],
  exports: [RouterModule]
})
export class RoomRegisterRoutingModule { }