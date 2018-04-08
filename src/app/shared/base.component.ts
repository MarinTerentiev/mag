declare var window: any;

export class BaseComponent {

    updateMaterial(): void {
        setTimeout(function () {
            window.onComponentShow();
            window.upgradeDom();
        }, 50);
    }

    public getCurrency(): string {
        return '$';
    }
}