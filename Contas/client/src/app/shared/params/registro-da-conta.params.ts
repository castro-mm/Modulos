import { Params } from "@/core/models/params.model";

/**
 * @author Marcelo M. de Castro
 * @summary Parâmetros de consulta para Registro da Conta
 * @version 1.0
 */
export class RegistroDaContaParams extends Params {
    mes: number = 0;
    ano: number = 0;
    credorId?: number;
    pagadorId?: number;
    status?: number;
}