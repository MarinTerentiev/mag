import { Component } from '@angular/core';
import { BaseComponent } from '../../../shared/base.component';
import { ModalTempService } from '../../../shared/service/inner/modal-temp.service';

@Component({
    selector: 'app-modal-temp',
    templateUrl: './modal-temp.component.html',
    styleUrls: ['./modal-temp.component.less']
})
export class ModalTempComponent extends BaseComponent {
    constructor (
        public modalTempService: ModalTempService
    ) {
        super();
    }
}
