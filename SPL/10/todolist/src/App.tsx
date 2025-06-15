import React from "react";
import TodoForm from "./components/TodoForm";
import TodoList from "./components/TodoList";

const App = () => (
  <div className="app-container">
    <h1>Список дел</h1>
    <TodoForm />
    <TodoList />
  </div>
);

export default App;
