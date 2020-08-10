



export interface ChartDataRequest {
    //
    dataPointIds: number[];
    //
    includeNulls?: boolean;
    //
    yearMin?: number;
    //
    yearMax?: number;
    //
    movingAveragePeriod?: number;
}
