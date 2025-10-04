import { Injectable, Signal, WritableSignal, signal } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class TitleService {
    private readonly _pageTitle: WritableSignal<string> = signal('Ana Sayfa');
    public readonly pageTitle: Signal<string> = this._pageTitle.asReadonly();

    constructor() { }

    setPageTitle(newTitle: string): void {
        this._pageTitle.set(newTitle);
    }
}
