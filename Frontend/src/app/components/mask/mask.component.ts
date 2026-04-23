import { Component, Input } from '@angular/core';

@Component({
    selector: 'app-mask',
    templateUrl: './mask.component.html',
    styleUrls: ['./mask.component.scss'],
    standalone: false
})
export class MaskComponent {

  @Input()
  public visible: boolean;

}
