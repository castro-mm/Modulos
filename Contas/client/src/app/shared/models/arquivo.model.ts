import { Entity } from "@/core/models/entity.model";

export class Arquivo extends Entity {
    nome: string = '';
    extensao: string = '';
    tamanho: number = 0;
    tipo: string = '';
    dados: Uint8Array = new Uint8Array();
    dataDaUltimaModificacao: Date = new Date();    
}