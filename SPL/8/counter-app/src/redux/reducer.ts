import { CounterState, CounterAction, INCREMENT, DECREMENT, RESET } from "./types";

const initialState: CounterState = {
    count: 0,
};

export const counterReducer = (state = initialState, action: CounterAction): CounterState => {
    switch (action.type) {
        case INCREMENT:
            return { count: state.count + 1 };
        case DECREMENT:
            return { count: state.count - 1 };
        case RESET:
            return { count: 0 };
        default:
            return state;
    }
};
