import { Role, Status } from "../../enum";

export class User {
    constructor() {

    }

    public Id: number;
    public UserName: string;

    public Name: string;
    public Email: string;

    public ListRoles: Role[];
    public Status: Status;
}
