import { createStore } from 'redux';
import { todosReducer } from './reducer';

export const store = createStore(
    todosReducer
);

export type RootState = typeof store.getState;
export type AppDispatch = typeof store.dispatch;
