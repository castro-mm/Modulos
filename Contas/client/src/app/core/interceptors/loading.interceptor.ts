import { HttpEvent, HttpHandler, HttpHandlerFn, HttpInterceptor, HttpInterceptorFn, HttpRequest } from "@angular/common/http";
import { finalize, Observable } from "rxjs";
import { LoadingService } from "../services/loading.service";
import { inject } from "@angular/core";

export class LoadingInterceptor implements HttpInterceptor {
    intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        const loadingService = inject(LoadingService);
        loadingService.show();

        return next.handle(req)
            .pipe(
                finalize(() => loadingService.hide()
            )
        );
    }
}

export const loadingInterceptor: HttpInterceptorFn = (req: HttpRequest<any>, next: HttpHandlerFn): Observable<HttpEvent<any>> => {
    const loadingService = inject(LoadingService);
    loadingService.show();

    return next(req)
        .pipe(
            finalize(() => loadingService.hide()
        )
    );
};