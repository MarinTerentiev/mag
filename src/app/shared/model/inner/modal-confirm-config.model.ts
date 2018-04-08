export class ModalConfirmConfig {
    constructor(header: string, message: string, handler: any) {
        this.Header = header;
        this.Message = message;
        this.handlers.push(handler);
    }

    public Header: string;
    public Message: string;
    private handlers = [];

    ok(): void {
        this.handlers.forEach(x => { x.ok(x.context, this); });
    }
    cancel(): void {
        console.log(this.handlers);
        this.handlers.forEach(x => { x.cancel(x.context, this); });
    }
}
