import { Base } from '../base.model';

export class Company extends Base {
    constructor() {
        super();

        this.Name = '';
    }

    public DealerId: number;
    public Name: string;
    public Enable: boolean;
    public EnableStr: string;
}
