import React, { useState } from "react";
import { useSelector, useDispatch } from "react-redux";
import { toggleTodo, deleteTodo } from "../redux/todosSlice";
import TodoForm from "./TodoForm";
import { Todo } from "../types";

interface TodoItemProps {
  todo: Todo;
}

const TodoItem = ({ todo }: TodoItemProps) => {
  const dispatch = useDispatch();
  const [isEditing, setIsEditing] = useState(false);

  if (isEditing) {
    return <TodoForm editMode currentText={todo.text} id={todo.id} onFinishEdit={() => setIsEditing(false)} />;
  }

  return (
    <li className="todo-item">
      <div className="todo-left">
        <input type="checkbox" checked={todo.completed} onChange={() => dispatch(toggleTodo(todo.id))} />
        <span className={todo.completed ? "todo-text completed" : "todo-text"}>{todo.text}</span>
      </div>
      <div className="todo-actions">
        {!todo.completed && (
          <button onClick={() => setIsEditing(true)} className="edit-btn">
            Редактировать
          </button>
        )}
        <button onClick={() => dispatch(deleteTodo(todo.id))} className="delete-btn">
          Удалить
        </button>
      </div>
    </li>
  );
};

export default TodoItem;
