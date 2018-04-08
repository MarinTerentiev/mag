import { ModalTempType } from '../../enum';

export class ModalTempConfig {
    constructor(message: string, type: ModalTempType) {
        this.Message = message;
        this.Type = ModalTempType[type];
        this.Time = 3000;
    }

    public Message: string;
    public Type: string;
    public Time: number;
}
