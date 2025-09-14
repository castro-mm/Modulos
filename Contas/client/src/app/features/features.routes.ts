import { Routes } from "@angular/router";

export default [
    { 
        path: 'segmento-do-credor', 
        loadComponent: () => import('./segmento-do-credor/segmento-do-credor.component').then(m => m.SegmentoDoCredorComponent) 
    }
] as Routes;