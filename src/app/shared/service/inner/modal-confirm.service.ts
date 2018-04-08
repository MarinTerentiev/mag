import { Injectable } from '@angular/core';
import { ModalTempConfig } from '../../model/inner/modal-temp-config.model';
import { ModalConfirmConfig } from '../../model/inner/modal-confirm-config.model';
declare var window: any;

@Injectable()
export class ModalConfirmService {
    public modalConfirmConfig: ModalConfirmConfig = new ModalConfirmConfig('', '', '');

    constructor() { }

    init(config: ModalConfirmConfig): void {
        this.modalConfirmConfig = config;
    }

    open(): void {
        window.showModal('#modal-confirm');
    }

    close(): void {
        window.hideModal('#modal-confirm');
    }
}