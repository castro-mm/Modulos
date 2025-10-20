import { EntityService } from "@/core/services/entity.service";
import { Injectable } from "@angular/core";
import { Pagador } from "../models/pagador.model";

@Injectable({
    providedIn: 'root'
})
export class PagadorService extends EntityService<Pagador> {
    constructor() {
        super('pagador');
    }
}