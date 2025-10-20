import { Params } from "@/core/models/params.model";

export class CredorParams extends Params {
    segmentoDoCredorId: number = 0;
    razaoSocial: string = '';
    nomeFantasia: string = '';
    cnpj: number = 0;
}
