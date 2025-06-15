import {
    ADD_TODO,
    TOGGLE_TODO,
    EDIT_TODO,
    DELETE_TODO,
    TodoActionTypes,
  } from '../types';
    
  export const addTodo = (text: string): TodoActionTypes => ({
    type: ADD_TODO,
    payload: { text },
  });
  
  export const toggleTodo = (id: number): TodoActionTypes => ({
    type: TOGGLE_TODO,
    payload: { id },
  });
  
  export const editTodo = (id: number, text: string): TodoActionTypes => ({
    type: EDIT_TODO,
    payload: { id, text },
  });
  
  export const deleteTodo = (id: number): TodoActionTypes => ({
    type: DELETE_TODO,
    payload: { id },
  });
  