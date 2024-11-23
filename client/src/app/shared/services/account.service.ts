import { HttpClient } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { User } from '../models/user';
import { tap } from 'rxjs';
import { ValidationResult } from '../models/validationResult';
import { environment } from '../../../environments/environment.development';

@Injectable({
    providedIn: 'root',
})
export class AccountService {
    apiUrl: string = environment.apiUrl+'account/';
    currentUser = signal<User | null>(null);

    constructor(private http: HttpClient) { }

    register() {

    }
    
    login(auth: any) {
        return this.http.post(this.apiUrl+'login', auth)
            .pipe(
                tap((result: any) => {
                    const validationResult: ValidationResult = result as ValidationResult;
                    if (validationResult.statusCode == 200) {
                        this.setCurrentUser(validationResult.data[0]);
                    }
                    return result;
                })
            )
    }

    setCurrentUser(user: User) {
        user.roles = [];

        const roles = JSON.parse(atob(user.token.split('.')[1]));
        Array.isArray(roles) ? user.roles = roles : user.roles.push(roles);

        localStorage.setItem('user', JSON.stringify(user));
        this.currentUser.set(user);        
    }


    logout() {
        localStorage.removeItem('user');
        this.currentUser.set(null);        
    }
}
