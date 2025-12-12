import { Params } from "@/core/models/params.model";

/**
 * @author Marcelo M. de Castro
 * @summary Parametro de consulta do Pagador
 * @version 1.0
 */
export class PagadorParams extends Params {
    nome: string = '';
    cpf: number = 0;
    email: string = '';
}