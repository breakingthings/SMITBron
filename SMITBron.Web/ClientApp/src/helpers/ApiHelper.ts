export const Kama = {};
import { SwaggerResponse } from "../APIClient";
import { MainContext, IMainContext } from "../context/MainContext";

const timeTakenHeader = "X-Request-Timetaken";

export const WrapApi = <T>(
  apiCall: Promise<SwaggerResponse<T> | undefined>,
  context: IMainContext,
  showLoading: boolean = true
): Promise<T | null> => {
  return new Promise((resolve, reject) => {
    if (showLoading) {
      context.setLoading(true);
    }

    apiCall
      .then((x) => {
        if (
          x &&
          x.headers[timeTakenHeader] &&
          Number(x.headers[timeTakenHeader])
        ) {
          context.setLastFetchTime({
            call: x.headers["X-Request-Path"],
            milliSeconds: Number(x.headers[timeTakenHeader]),
          });
        }

        if (showLoading) {
          context.setLoading(false);
        }

        resolve(x === undefined ? null : x.result);
      })
      .catch((reason) => {
        if (showLoading) {
          context.setLoading(false);
        }

        if (reason.response && reason.response[0]) {
          context.showSnack(reason.response[0].error, "error");
        } else {
          context.showSnack("unknown error", "error");
        }
        reject(null);
      });
  });
};
