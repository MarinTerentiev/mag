import { UserExists } from "./user-exists.model";

export class DealerExists extends UserExists {
    constructor(userName: string, email: string, name: string) {
        super(userName, email);

        this.Name = name;
    }
    
    public Name: string;
}
