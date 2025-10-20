import { Entity } from "@/core/models/entity.model";

export class Credor extends Entity {
    segmentoDoCredorId: number = 0;
    razaoSocial: string = '';
    nomeFantasia: string = '';
    cnpj: number = 0;
}