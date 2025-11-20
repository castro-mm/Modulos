import { Params } from "@/core/models/params.model";

export class RegistroDaContaParams extends Params {
    mes: number = 0;
    ano: number = 0;
    credorId?: number;
    pagadorId?: number;
    status?: number;
}