import { DestroyRef, inject } from '@angular/core';
import { Observable } from 'rxjs';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

export function abortOnDestroy<T>(source: Observable<T>): Observable<T> {
    const destroyRef = inject(DestroyRef);
    return source.pipe(takeUntilDestroyed(destroyRef));
}
