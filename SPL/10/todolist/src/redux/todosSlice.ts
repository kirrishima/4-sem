import { createSlice, PayloadAction } from '@reduxjs/toolkit';
import { Todo } from '../types';

interface TodosState {
    todos: Todo[];
}

const initialState: TodosState = {
    todos: [],
};

export const todosSlice = createSlice({
    name: 'todos',
    initialState,
    reducers: {
        addTodo: {
            reducer(state, action: PayloadAction<Todo>) {
                state.todos.push(action.payload);
            },
            prepare(text: string) {
                return {
                    payload: {
                        id: Date.now(),
                        text,
                        completed: false,
                    } as Todo,
                };
            },
        },
        toggleTodo(state, action: PayloadAction<number>) {
            const todo = state.todos.find(t => t.id === action.payload);
            if (todo) {
                todo.completed = !todo.completed;
            }
        },
        editTodo(state, action: PayloadAction<{ id: number; text: string }>) {
            const todo = state.todos.find(t => t.id === action.payload.id);
            if (todo) {
                todo.text = action.payload.text;
            }
        },
        deleteTodo(state, action: PayloadAction<number>) {
            state.todos = state.todos.filter(t => t.id !== action.payload);
        },
    },
});

export const { addTodo, toggleTodo, editTodo, deleteTodo } = todosSlice.actions;
export const todosReducer = todosSlice.reducer;
 