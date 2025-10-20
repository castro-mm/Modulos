import { Params } from "@/core/models/params.model";

export class PagadorParams extends Params {
    nome: string = '';
    cpf: number = 0;
    email: string = '';
}