import { useSelector } from "react-redux";
import TodoItem from "./TodoItem";
import { TodosState } from "../types";

const TodoList = () => {
  const todos = useSelector((state: TodosState) => state.todos);

  return (
    <ul className="todo-list">
      {todos.map((todo) => (
        <TodoItem key={todo.id} todo={todo} />
      ))}
    </ul>
  );
};

export default TodoList;
