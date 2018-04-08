export class AnswerExists {
    constructor(exist: boolean, message: string) {
        this.Exist = exist;
        this.Message = message;
    }

    public Exist: boolean;
    public Message: string;
}
