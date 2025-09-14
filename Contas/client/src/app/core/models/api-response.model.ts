import { StatusCode } from "../objects/enums";

export type ApiResponse = {
    statusCode: StatusCode;
    message?: string;
    details?: string;
    data?: any;
    timeStamp?: Date;
    apiPath?: string;
    traceId?: string;
}
