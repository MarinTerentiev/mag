import { Injectable } from '@angular/core';
import { ModalTempConfig } from '../../model/inner/modal-temp-config.model';
import { ModalTempType } from '../../enum';

@Injectable()
export class ModalTempService {
    public isShow: boolean;
    public config: ModalTempConfig;

    constructor() {
        this.isShow = false;
    }

    show(modalTempConfig: ModalTempConfig): void {
        this.isShow = true;
        this.config = modalTempConfig;

        const that = this;
        setTimeout(function(){
            that.isShow = false;
        }, modalTempConfig.Time);
    }

    showSuccess(): void {
        let config = new ModalTempConfig('Success', ModalTempType.success);
        this.show(config);
    }
    showDanger(): void {
        let config = new ModalTempConfig('Error', ModalTempType.danger);
    }
}
