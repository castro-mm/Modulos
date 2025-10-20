import { Entity } from "@/core/models/entity.model";

export class Pagador extends Entity {
    nome: string = '';
    cpf: number = 0;
    email: string = '';
}