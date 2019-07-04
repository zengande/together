import { Reducer } from 'redux';
import { IGlobalSettings } from 'src/types/IGlobalSettings';

const initialState: IGlobalSettings = {
    title: 'together'
}

export const reducer: Reducer<IGlobalSettings> = (state = initialState, action) => {
    return state;
}