import { AxiosRequestConfig } from "axios";

export class ApiBase {

    constructor() { }

    transformOptions = (options: AxiosRequestConfig) => {
        //options.withCredentials = true;
        return Promise.resolve(options);
    }

    getBaseUrl = (url: string) => {
        return '';

    }
}
