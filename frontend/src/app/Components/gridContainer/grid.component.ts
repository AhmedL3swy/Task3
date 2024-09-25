import { ApiService } from './../../Services/fake-data-service.service';
import { Component } from '@angular/core';
import {
  ActionDisplayType,
  DataGridConfig,
} from '../../types/data-grid-config';
import { ActionType } from '../../types/action-config';
import { DataGridComponent } from '../data-grid/data-grid.component';
import { DataGridService } from '../../Services/data-grid.service';
import { Router } from '@angular/router';

@Component({
  selector: 'app-grid',
  standalone: true,
  imports: [DataGridComponent],
  templateUrl: './grid.component.html',
  styleUrl: './grid.component.scss',
})
export class GridContainerComponent {
  constructor(
    private dataGridService: DataGridService,
    private ApiService: ApiService,
    private router: Router
  ) {}
  resetGrid() {
    this.dataGridService.emitResetSingal();
  }
  isAdmin() {
    return true;
  }
  Add() {
    this.router.navigate(['/add']);
  }
  edit(entity: any) {
    this.router.navigate(['/edit', entity.id]);
  }
  delete(entity: any) {
    alert(' delete logic' + JSON.stringify(entity));
  }
  bulkDelete(entities: any[]) {
    alert('bulk delete logic' + JSON.stringify(entities));
    console.log(entities);
  }

  dataGird: DataGridConfig = {
    dataApi: 'https://localhost:7237/api/Users/Search',
    columns: [
      {
        header: 'headers.firstName',
        field: 'firstName',
        sortable: true,
      },
      {
        header: 'headers.lastName',
        field: 'lastName',
        sortable: true,
      },
      {
        header: 'headers.birthDate',
        field: 'birthDate',
        sortable: true,
      },
      {
        header: 'headers.email',
        field: 'email',
        sortable: false,
      },
      {
        header: 'headers.phone',
        field: 'mobile',
        sortable: false,
      },
      {
        header: 'headers.municipalNo',
        field: 'municipalNo',
        sortable: false,
      },
      {
        header: 'headers.nationalId',
        field: 'nationalId',
        sortable: false,
      },
      {
        header: 'headers.maritalStatus',
        field: 'maritalStatus',
        sortable: false,
      },
      {
        header: 'headers.address',
        field: 'address',
      },
    ],
    actions: [
      {
        name: 'buttons.Edit',
        callback: (entity) => this.edit(entity),
        enabled: this.isAdmin(),
        type: ActionType.Single,
        actionDisplayType: ActionDisplayType.ROW,
      },
      {
        name: 'buttons.Delete',
        callback: (entity) => this.delete(entity),
        enabled: this.isAdmin(),
        type: ActionType.Single,
        actionDisplayType: ActionDisplayType.ROW,
      },
      {
        name: 'buttons.BDelete',
        callback: (entities) => this.bulkDelete(entities),
        enabled: this.isAdmin(),
        type: ActionType.Multi,
      },
    ],
  };
}
