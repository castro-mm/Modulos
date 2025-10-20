import { InjectionToken } from "@angular/core";
import { Entity } from "../models/entity.model";
import { EntityService } from "./entity.service";

/**
 * @author Marcelo M. de Castro
 * @summary Token de injeção para o serviço genérico de dados.
 * @description Este token pode ser utilizado para injetar o serviço genérico de dados em componentes ou outros serviços.
 * Ele é útil quando se deseja utilizar o serviço de forma genérica, sem depender de uma implementação específica.
 * Exemplo de uso:
 * ```typescript
 * constructor(@Inject(DATA_SERVICE_TOKEN) private dataService: EntityService<Entity>) { }
 * ```
 */
export const DATA_SERVICE_TOKEN = new InjectionToken<EntityService<Entity>>('DATA_SERVICE_TOKEN');