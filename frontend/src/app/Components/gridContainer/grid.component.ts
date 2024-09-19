import { ApiService } from './../../Services/fake-data-service.service';
import { Component } from '@angular/core';
import {
  ActionDisplayType,
  DataGridConfig,
} from '../../types/data-grid-config';
import { ActionType } from '../../types/action-config';
import { DataGridComponent } from '../data-grid/data-grid.component';
import { DataGridService } from '../../Services/data-grid.service';

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
    private ApiService: ApiService
  ) {}
  resetGrid() {
    this.dataGridService.emitResetSingal();
  }
  isAdmin() {
    return true;
  }
  edit(entity: any) {
    alert('edit' + JSON.stringify(entity));
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
        header: 'headers.enTitle',
        field: 'firstName',
        sortable: true,
      },
      {
        header: 'headers.arTitle',
        field: 'lastName',
        sortable: true,
      },
      {
        header: 'headers.Description',
        field: 'email',
        isMultiLang: true,
        sortable: true,
      },
      {
        header: 'headers.Price',
        field: 'mobile',
        sortable: true,
      },
      {
        header: 'headers.Stock',
        field: 'municipalNo',
        sortable: true,
      },
      {
        header: 'headers.AddedOn',
        field: 'nationalId',
        sortable: true,
      },
      {
        header: 'headers.enCategoryName',
        field: 'maritalStatus',
        sortable: false,
      },
      {
        header: 'headers.arCategoryName',
        field: 'address',
      },
    ],
    actions: [
      {
        name: 'buttons.Edit',
        callback: (entity) => this.edit(entity),
        enabled: this.isAdmin(),
        type: ActionType.Single,
        actionDisplayType: ActionDisplayType.HEADER,
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
