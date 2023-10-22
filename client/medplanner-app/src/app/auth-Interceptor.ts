import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent } from "@angular/common/http";
import { Observable } from "rxjs";

export class AuthInterceptor implements HttpInterceptor {
    constructor() { }

    intercept(
        req: HttpRequest<any>,
        next: HttpHandler
    ): Observable<HttpEvent<any>> {
        const currentUser = localStorage.getItem('currentUser');

        if (!currentUser) {
            return next.handle(req);
        }

        const user = JSON.parse(currentUser);
        const authToken = user.token;

        const authReq = req.clone({
            setHeaders: {
                Authorization: `Bearer ${authToken}`,
            },
        });

        return next.handle(authReq);
    }
}