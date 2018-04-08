export class UserExists {
    constructor(userName: string, email: string) {
        this.UserName = userName;
        this.Email = email;
    }

    public UserName: string;
    public Email: string;
}
