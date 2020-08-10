



export interface ScatterSeries<TKey, TValue> {
    //
    name: string;
    //
    id?: TKey;
    //
    data?: TValue[];
}


export interface LineSeries {
    //
    name: string;
    //
    data: number[];
}
