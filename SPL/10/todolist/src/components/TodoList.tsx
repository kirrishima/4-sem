import React from "react";
import { useSelector } from "react-redux";
import TodoItem from "./TodoItem";
import { Todo } from "../types";

const TodoList = () => {
  const todos = useSelector((state: any) => state.todos.todos);

  return (
    <ul className="todo-list">
      {todos.map((todo: Todo) => (
        <TodoItem key={todo.id} todo={todo} />
      ))}
    </ul>
  );
};

export default TodoList;
