import { Entity } from "@/core/models/entity.model";
import { Arquivo } from "./arquivo.model";

export class ArquivoDoRegistroDaConta extends Entity {
    registroDaContaId: number = 0;
    arquivoId: number = 0;
    modalidadeDoArquivo: number = 0;
    arquivo: Arquivo = null!;

    constructor(registroDaContaId: number, arquivoId: number, modalidadeDoArquivo: number, init?: Partial<ArquivoDoRegistroDaConta>) {
        super();
        this.registroDaContaId = registroDaContaId;
        this.arquivoId = arquivoId;
        this.modalidadeDoArquivo = modalidadeDoArquivo;
        Object.assign(this, init);
    }
}
