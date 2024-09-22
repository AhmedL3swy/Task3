import { Routes } from '@angular/router';
import { EmptyComponent } from './Components/empty/empty.component';
import { GridContainerComponent } from './Components/gridContainer/grid.component';
import { AddUserComponent } from './Components/add-user/add-user.component';

export const routes: Routes = [
  { path: 'empty', component: EmptyComponent },
  { path: 'users', component: GridContainerComponent },
  { path: 'add', component: AddUserComponent },
  { path: 'edit/:id', component: AddUserComponent },
  { path: '**', redirectTo: 'users', pathMatch: 'full' },
];
