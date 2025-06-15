import React, { useState } from "react";
import { Link } from "react-router-dom";
import { TextInput, PasswordInput } from "./InputComponents";
import { StandardButton } from "./Buttons";
import "./App.css";

export default function SignIn() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [submitted, setSubmitted] = useState(false);

  const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setSubmitted(true);

    if (!email.trim() || !password.trim()) {
      alert("Пожалуйста, заполните все обязательные поля.");
      return;
    }
    if (!emailPattern.test(email)) {
      alert("Введите корректный адрес электронной почты.");
      return;
    }
    // Для тестовой авторизации используем test@test.com и test
    if (email === "test@test.com" && password === "test") {
      alert("Авторизация прошла успешно!");
    } else {
      alert("Неверный адрес электронной почты или пароль.");
    }
  };

  const titleStyle: React.CSSProperties = {
    textAlign: "center",
    marginBottom: "24px",
    color: "#4a6cf7",
  };

  const linksContainerStyle: React.CSSProperties = {
    display: "flex",
    justifyContent: "stretch",
    flexDirection: "column",
    gap: 10,
    marginTop: "16px",
  };

  return (
    <div className="form-container">
      <h2 style={titleStyle}>Вход в систему</h2>
      <form onSubmit={handleSubmit}>
        <TextInput
          label="Электронная почта"
          value={email}
          onChange={setEmail}
          placeholder="Введите адрес электронной почты"
          validationPattern={emailPattern}
          errorMessage="Введите корректный адрес электронной почты"
          required
          forceTouched={submitted}
        />
        <PasswordInput
          label="Пароль"
          value={password}
          onChange={setPassword}
          placeholder="Введите пароль"
          errorMessage="Введите пароль"
          required
          forceTouched={submitted}
        />
        <div style={{ marginTop: "20px" }}>
          <StandardButton
            value="Войти"
            onClick={() => handleSubmit({ preventDefault: () => {} } as React.FormEvent)}
            color="primary"
          />
        </div>
      </form>
      <div style={linksContainerStyle}>
        <Link to="/sign-up">Еще не зарегистрированы? Регистрация</Link>
        <Link to="/reset-password">Забыли пароль?</Link>
      </div>
    </div>
  );
}
