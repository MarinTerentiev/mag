import { Injectable } from '@angular/core';

@Injectable()
export class LoaderService {
	private loaderStatus: boolean = false; 
	private count = 0;

	public getState(): boolean {
		return this.loaderStatus;
	}

	public start(): void {
		this.loaderStatus = true;
		this.count++;
	}

	public stop(): void {
		this.count--;
		if (this.count < 1) {
			this.loaderStatus = false;
		}
	}
}