export interface UserProfile {
    id: string;
    nomeCompleto: string;
    email: string;
    role: string;
    isActive: boolean;
    emailConfirmed: boolean;
    fotoUrl: string | null;
    dataDeCriacao: Date;
    dataDeAtualizacao: Date;
}