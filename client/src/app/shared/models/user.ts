export interface User {
    nome: string;
    email: string;
    token: string;
    roles: string | string[];
    isAuthenticated: boolean;
}