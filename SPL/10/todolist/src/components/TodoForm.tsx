import React, { useState, useEffect } from "react";
import { useSelector, useDispatch } from "react-redux";
import { addTodo, editTodo } from "../redux/todosSlice";

interface TodoFormProps {
  editMode?: boolean;
  currentText?: string;
  id?: number;
  onFinishEdit?: () => void;
}

const TodoForm = ({ editMode = false, currentText = "", id, onFinishEdit }: TodoFormProps) => {
  const [text, setText] = useState(currentText);
  const dispatch = useDispatch();

  useEffect(() => {
    setText(currentText);
  }, [currentText]);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    if (!text.trim()) return;

    if (editMode && id !== undefined) {
      dispatch(editTodo({ id, text }));
      onFinishEdit?.();
    } else {
      dispatch(addTodo(text));
      setText("");
    }
  };

  return (
    <form onSubmit={handleSubmit} className="todo-form">
      <input
        value={text}
        onChange={(e) => setText(e.target.value)}
        className="todo-input"
        placeholder="Новая задача..."
      />
      <button type="submit" className="todo-button">
        {editMode ? "Сохранить" : "Добавить"}
      </button>
    </form>
  );
};

export default TodoForm;
