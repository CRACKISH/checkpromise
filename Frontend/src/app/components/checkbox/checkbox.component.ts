import { Component, Input } from '@angular/core';
import { PromiseStatus } from '../../models/promise-data.model';

@Component({
  selector: 'app-checkbox',
  templateUrl: './checkbox.component.html',
  styleUrls: ['./checkbox.component.scss']
})
export class CheckboxComponent {

  @Input()
  public status: PromiseStatus;

  public getStyleClass(): string {
    let result = '';
    switch (this.status) {
      case PromiseStatus.Done:
        result = 'checked';
        break;
      case PromiseStatus.NotPerformed:
        result = 'crossed';
        break;
      case PromiseStatus.Nothing:
      default:
        result = '';
    }
    return result;
  }

}
