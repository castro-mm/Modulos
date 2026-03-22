import { computed, inject, Injectable, signal } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Router } from '@angular/router';
import { environment } from '../../../environments/environment';
import { UserProfile } from '../models/user-profile.model';
import { TokenPayload } from '../models/token-payload.model';
import { Login } from '../models/login.model';
import { firstValueFrom } from 'rxjs';
import { ApiResponse } from '@/core/types/api-response.type';
import { Register } from '../models/register.model';
import { ChangePassword } from '../models/change-password.model';
import { ForgotPassword } from '../models/forgot-password.model';
import { ResetPassword } from '../models/reset-password.model';

const TOKEN_KEY: string = 'auth_token';
const REMEMBER_KEY: string = 'auth_remember';
const LAST_ACCESS_KEY: string = 'auth_last_access';

@Injectable({
    providedIn: 'root'
})
export class AuthService {
    private apiUrl: string = `${environment.url}/account`
    
    private http = inject(HttpClient);
    private router = inject(Router);

    // Estado reativo
    private _currentUser = signal<UserProfile | null>(null);
    private _token = signal<string | null>(this.getStoredToken());
    private _lastAccess = signal<string | null>(this.getStoredLastAccess());

    // Signals publicos (computed / readonly)
    private currentUser = this._currentUser.asReadonly();
    readonly token = this._token.asReadonly();
    readonly isAuthenticated = computed(() => !!this._token() && !this.isTokenExpired());
    readonly isAdmin = computed(() => this.getUserRole() === 'Admin');
    readonly userName = computed(() => this.currentUser()?.nomeCompleto ?? this.getClaimFromToken('name') ?? '');
    readonly userEmail = computed(() => this.currentUser()?.email ?? this.getClaimFromToken('email') ?? '');
    readonly userRole = computed(() => this.currentUser()?.role ?? this.getUserRole() ?? '');
    readonly userPhoto = computed(() => this.currentUser()?.fotoUrl ?? null);
    readonly lastAccess = this._lastAccess.asReadonly();

    constructor() {
        if(this.isAuthenticated()) {
            this.loadCurrentUser();
        }
    }

    // ----------------------------------------------------------------------------------------------------------------------------------
    // -- Métodos públicos
    // ----------------------------------------------------------------------------------------------------------------------------------

    async login(credentials: Login, rememberMe: boolean): Promise<ApiResponse> {
        const response$ = this.http.post<ApiResponse>(`${this.apiUrl}/login`, credentials);
        const response = await firstValueFrom(response$);

        if (response.result?.isSuccessful && response.result.data?.token) {
            // Salvar o acesso anterior como "último acesso" antes de atualizar
            const currentAccessTime = localStorage.getItem(LAST_ACCESS_KEY + '_current');
            if (currentAccessTime) {
                localStorage.setItem(LAST_ACCESS_KEY, currentAccessTime);
            }
            // Registrar o momento do acesso atual
            localStorage.setItem(LAST_ACCESS_KEY + '_current', new Date().toISOString());
            this._lastAccess.set(localStorage.getItem(LAST_ACCESS_KEY));

            this.setToken(response.result.data.token, rememberMe);
            await this.loadCurrentUser();
        }
        return response;
    }

    async register(user: Register): Promise<ApiResponse> {
        const response$ = this.http.post<ApiResponse>(`${this.apiUrl}/register`, user);
        return await firstValueFrom(response$);
    }

    async logout(): Promise<void> {
        localStorage.removeItem(TOKEN_KEY);
        localStorage.removeItem(REMEMBER_KEY);
        sessionStorage.removeItem(TOKEN_KEY);

        this.setToken(null, false);
        this._currentUser.set(null);
        this._lastAccess.set(null);
        await this.router.navigate(['/secure/login']);
    }
    
    // ----------------------------------------------------------------------------------------------------------------------------------
    // -- Senha
    // ----------------------------------------------------------------------------------------------------------------------------------

    async confirmEmail(email: string, token: string): Promise<ApiResponse> {
        const params = new HttpParams().set('email', email).set('token', token);
        const response$ = this.http.get<ApiResponse>(`${this.apiUrl}/confirm-email`, { params });
        return await firstValueFrom(response$);
    }

    async changePassword(data: ChangePassword): Promise<ApiResponse> {
        const response$ = this.http.put<ApiResponse>(`${this.apiUrl}/change-password`, data);
        return await firstValueFrom(response$);
    }

    async forgotPassword(data: ForgotPassword): Promise<ApiResponse> {
        const response$ = this.http.post<ApiResponse>(`${this.apiUrl}/forgot-password`, data);
        return await firstValueFrom(response$);
    }

    async resetPassword(data: ResetPassword): Promise<ApiResponse> {
        const response$ = this.http.post<ApiResponse>(`${this.apiUrl}/reset-password`, data);
        return await firstValueFrom(response$);
    }
    
    // ----------------------------------------------------------------------------------------------------------------------------------
    // -- Usuário Logado
    // ----------------------------------------------------------------------------------------------------------------------------------

    async loadCurrentUser(): Promise<void> {
        if (!this.isAuthenticated()) {
            this._currentUser.set(null);
            return;
        }

        try {
            const response$ = this.http.get<ApiResponse>(`${this.apiUrl}/me`);
            const response = await firstValueFrom(response$);
    
            if (response.result?.isSuccessful && response.result.data) {
                this._currentUser.set(response.result.data as UserProfile);
            } else {
                this._currentUser.set(null);
            }    
        } catch {
            this._currentUser.set(null);
        }
    }

    async updatePhoto(fotoUrl: string): Promise<ApiResponse> {
        const response$ = this.http.put<ApiResponse>(`${this.apiUrl}/me/photo`, { fotoUrl });
        const response = await firstValueFrom(response$);

        if (response.result?.isSuccessful) {
            await this.loadCurrentUser();
        }

        return response;
    }

    async removePhoto(): Promise<ApiResponse> {
        const response$ = this.http.delete<ApiResponse>(`${this.apiUrl}/me/photo`);
        const response = await firstValueFrom(response$);

        if (response.result?.isSuccessful) {
            await this.loadCurrentUser();
        }

        return response;
    }

    // ----------------------------------------------------------------------------------------------------------------------------------
    // -- Gestão de Usuários
    // ----------------------------------------------------------------------------------------------------------------------------------

    async getAllUsers(): Promise<ApiResponse> {
        const response$ = this.http.get<ApiResponse>(`${this.apiUrl}/users`);
        return await firstValueFrom(response$);
    }

    async getUserById(userId: number): Promise<ApiResponse> {
        const response$ = this.http.get<ApiResponse>(`${this.apiUrl}/users/${userId}`);
        return await firstValueFrom(response$);
    }

    async updateUser(userId: number, data: { nomeCompleto: string; email: string; role: string; isActive: boolean }): Promise<ApiResponse> {
        const response$ = this.http.put<ApiResponse>(`${this.apiUrl}/users/${userId}`, data);
        return await firstValueFrom(response$);
    }

    async updateUserRole(userId: number, newRole: string): Promise<ApiResponse> {
        const response$ = this.http.put<ApiResponse>(`${this.apiUrl}/users/${userId}/role`, { role: newRole });
        return await firstValueFrom(response$);
    }

    async deactivateUser(userId: number): Promise<ApiResponse> {
        const response$ = this.http.put<ApiResponse>(`${this.apiUrl}/users/${userId}/deactivate`, {});
        return await firstValueFrom(response$);
    }

    async activateUser(userId: number): Promise<ApiResponse> {
        const response$ = this.http.put<ApiResponse>(`${this.apiUrl}/users/${userId}/activate`, {});
        return await firstValueFrom(response$);
    }

    async deleteUser(userId: number): Promise<ApiResponse> {
        const response$ = this.http.delete<ApiResponse>(`${this.apiUrl}/users/${userId}`);
        return await firstValueFrom(response$);
    }

    async adminResetPassword(userId: number): Promise<ApiResponse> {
        const response$ = this.http.post<ApiResponse>(`${this.apiUrl}/users/${userId}/admin-reset-password`, {});
        return await firstValueFrom(response$);
    }

    // ----------------------------------------------------------------------------------------------------------------------------------
    // -- Gestão de Roles
    // ----------------------------------------------------------------------------------------------------------------------------------

    async getAllRoles(): Promise<ApiResponse> {
        const response$ = this.http.get<ApiResponse>(`${this.apiUrl}/roles`);
        return await firstValueFrom(response$);
    }

    async getRoleById(roleId: number): Promise<ApiResponse> {
        const response$ = this.http.get<ApiResponse>(`${this.apiUrl}/roles/${roleId}`);
        return await firstValueFrom(response$);
    }

    async createRole(name: string): Promise<ApiResponse> {
        const response$ = this.http.post<ApiResponse>(`${this.apiUrl}/roles`, { name });
        return await firstValueFrom(response$);
    }

    async updateRole(roleId: number, name: string): Promise<ApiResponse> {
        const response$ = this.http.put<ApiResponse>(`${this.apiUrl}/roles/${roleId}`, { id: roleId, name });
        return await firstValueFrom(response$);
    }

    async deleteRole(roleId: number): Promise<ApiResponse> {
        const response$ = this.http.delete<ApiResponse>(`${this.apiUrl}/roles/${roleId}`);
        return await firstValueFrom(response$);
    }

    // ----------------------------------------------------------------------------------------------------------------------------------
    // -- Token
    // ----------------------------------------------------------------------------------------------------------------------------------

    getToken(): string | null {
        return this._token();
    }

    getUserId(): number {
        const payload = this.decodeToken();
        if (!payload) return 0;

        return parseInt(payload.sub, 10) || 0;
    }

    getUserRole(): string {
        const payload = this.decodeToken();
        if (!payload) return '';

        const roleClaim = payload['http://schemas.microsoft.com/ws/2008/06/identity/claims/role'];
        if (Array.isArray(roleClaim)) {
            return roleClaim[0] || '';
        }
        return roleClaim || '';
    } 

    isTokenExpired(): boolean {
        const payload = this.decodeToken();
        if (!payload) return true;

        const currentTime = Math.floor(Date.now() / 1000);
        return payload.exp < currentTime;
    }

    // ----------------------------------------------------------------------------------------------------------------------------------
    // -- Métodos privados
    // ----------------------------------------------------------------------------------------------------------------------------------

    private setToken(token: string | null, rememberMe: boolean): void {
        if (token === null) {
            this._token.set(null);
            localStorage.removeItem(TOKEN_KEY);
            localStorage.removeItem(REMEMBER_KEY);
            return;   
        }

        localStorage.setItem(TOKEN_KEY, token);
        if (rememberMe) {
            localStorage.setItem(REMEMBER_KEY, 'true');
        } 
        this._token.set(token);        
    }

    private getStoredToken(): string | null {
        return localStorage.getItem(TOKEN_KEY) ?? sessionStorage.getItem(TOKEN_KEY);
    }

    private getStoredLastAccess(): string | null {
        return localStorage.getItem(LAST_ACCESS_KEY);
    }

    private decodeToken(): TokenPayload | null {
        const token = this._token();
        if (!token) return null;

        try {
            const payload = JSON.parse(atob(token.split('.')[1])) as TokenPayload;
            return payload;
        } catch {
            return null;
        }
    }

    private getClaimFromToken(claim: 'name' | 'email'): string | null {
        const payload = this.decodeToken();
        if (!payload) return null;
        
        const claimMap: Record<string, string> = {
            name: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name',
            email: 'http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress'
        };        

        const claimValue = (payload as unknown as Record<string, unknown>)[claimMap[claim]] as string;
        return claimValue || null;
    }
}
