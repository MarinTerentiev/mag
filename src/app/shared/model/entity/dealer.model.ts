import { Base } from '../base.model';

export class Dealer extends Base {
    constructor() {
        super();
        this.Amount = null;
        this.Name = '';
    }

    public UserId: number;
    public Name: string;

    public Enable: boolean;
    public EnableStr: string;
    public Amount: number;
}