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

export enum StatusDaConta {
    Pendente = 0,
    Paga = 1,
    Vencida = 2,
    Cancelada = 3
}

export enum TipoDeArquivo {
    BoletoBancario = 1,
    ComprovanteDePagamento = 2,
    Outro = 3
}