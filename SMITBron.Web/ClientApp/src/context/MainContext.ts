import * as React from "react";

export interface IFetchTime {
    call: string;
    milliSeconds: number;
};

export interface IMainContext {
    showSnack: (content: string, type: "error"|"info"|"warning"|"success") => void;
    setLoading: (loading: boolean) => void;
    setLastFetchTime: (time: IFetchTime) => void;
}

export const MainContext = React.createContext<IMainContext>({
    showSnack: (content, type) => { },
    setLoading: (loading) => { },
    setLastFetchTime: (time) => {}
});


export const MainProvider = MainContext.Provider;

export const MainConsumer = MainContext.Consumer;