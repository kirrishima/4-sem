import { TodosState, TodoActionTypes, ADD_TODO, TOGGLE_TODO, EDIT_TODO, DELETE_TODO } from '../types';

const initialState: TodosState = {
    todos: [],
};

export function todosReducer(state = initialState, action: TodoActionTypes): TodosState {
    switch (action.type) {
        case ADD_TODO:
            const newTodo = {
                id: Date.now(),
                text: action.payload.text,
                completed: false,
            };
            return { todos: [...state.todos, newTodo] };

        case TOGGLE_TODO:
            return {
                todos: state.todos.map(todo =>
                    todo.id === action.payload.id ? { ...todo, completed: !todo.completed } : todo
                ),
            };

        case EDIT_TODO:
            return {
                todos: state.todos.map(todo =>
                    todo.id === action.payload.id ? { ...todo, text: action.payload.text } : todo
                ),
            };

        case DELETE_TODO:
            return {
                todos: state.todos.filter(todo => todo.id !== action.payload.id),
            };

        default:
            return state;
    }
}
