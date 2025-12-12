/**
 * @author Marcelo M. de Castro
 * @summary Enumeração dos códigos de status HTTP.
 * @description Esta enumeração define os principais códigos de status HTTP utilizados nas respostas da API, facilitando a interpretação dos resultados das requisições.
 * @enum {StatusCode}
 * @returns {StatusCode} O código de status HTTP correspondente.
 */
export enum StatusCode {
    OK = 200,
    Created = 201,
    NoContent = 204,
    BadRequest = 400,
    Unauthorized = 401,
    Forbidden = 403,
    NotFound = 404,
    InternalServerError = 500,
    ServiceUnavailable = 503
}