import { StatusCode } from "../objects/enums";

/**
 * @author Marcelo M. de Castro
 * @summary Tipo de resposta enviada pela api
 * @description Define a estrutura padrão para respostas da API, incluindo código de status, mensagens, dados retornados e informações adicionais como timestamp e traceId. 
 * @param {StatusCode} statusCode - O código de status da resposta.
 * @param {string} [message] - Uma mensagem descritiva sobre a resposta.
 * @param {string} [details] - Detalhes adicionais sobre a resposta.
 * @param {any} [data] - Os dados retornados pela API.
 * @param {Date} [timeStamp] - A data e hora em que a resposta foi gerada.
 * @param {string} [apiPath] - O caminho da API que gerou a resposta.
 * @param {string} [traceId] - Um identificador único para rastreamento da requisição.
 * @returns {ApiResponse} Um objeto representando a resposta da API.
 */
export type ApiResponse = {
    statusCode: StatusCode;
    message?: string;
    details?: string;
    result?: Result;
    timeStamp?: Date;
    apiPath?: string;
    traceId?: string;
}

export type Result = {
    isSuccessful: boolean;
    data?: any;
    errors?: ErrorResult[];
    message?: string;
}

export type ErrorResult = {
    code: string;
    message: string;
}
