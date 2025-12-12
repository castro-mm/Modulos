import { Params } from "@/core/models/params.model";

/**
 * @author Marcelo M. de Castro
 * @summary Parâmetros de consulta para Credor
 * @version 1.0
 */
export class CredorParams extends Params {
    segmentoDoCredorId: number = 0;
    razaoSocial: string = '';
    nomeFantasia: string = '';
    cnpj: number = 0;
}
