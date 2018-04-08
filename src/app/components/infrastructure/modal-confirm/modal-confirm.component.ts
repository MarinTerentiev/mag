import { Component } from "@angular/core";
import { BaseComponent } from "../../../shared/base.component";
import { ModalConfirmService } from "../../../shared/service/inner/modal-confirm.service";


@Component({
    selector: 'app-modal-confirm',
    templateUrl: './modal-confirm.component.html',
    styleUrls: ['./modal-confirm.component.less']
})
export class ModalConfirmComponent extends BaseComponent {
    
    constructor (
        public modalConfirmService: ModalConfirmService
    ) {
        super();
    }

    
}