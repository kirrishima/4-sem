import React, { useState } from "react";
import { Link } from "react-router-dom";
import { TextInput, PasswordInput } from "./InputComponents";
import { StandardButton } from "./Buttons";
import "./App.css";

// Определяем интерфейс для пользователя
interface User {
  name: string;
  email: string;
  password: string;
}

// Хранение зарегистрированных пользователей в оперативной памяти
const registeredUsers: User[] = [];

export default function SignUp() {
  const [name, setName] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [submitted, setSubmitted] = useState(false);

  // Регулярные выражения для валидации
  const namePattern = /^[A-Za-zА-Яа-яЁё\s]{2,50}$/;
  const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;
  const passwordPattern = /^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?!.*\s).{8,}$/;

  // Универсальная функция-обработчик для сабмита формы. Принимает событие формы либо клика по кнопке.
  const handleSubmit = (e: React.FormEvent<HTMLFormElement> | React.MouseEvent<HTMLButtonElement, MouseEvent>) => {
    e.preventDefault();
    setSubmitted(true);

    // Проверка обязательных полей
    if (!name.trim() || !email.trim() || !password.trim() || !confirmPassword.trim()) {
      alert("Пожалуйста, заполните все обязательные поля.");
      return;
    }
    if (!namePattern.test(name)) {
      alert("Имя должно содержать только буквы и пробелы (2–50 символов).");
      return;
    }
    if (!emailPattern.test(email)) {
      alert("Введите корректный адрес электронной почты.");
      return;
    }
    if (!passwordPattern.test(password)) {
      alert(
        "Пароль должен быть не менее 8 символов, содержать заглавную и строчную буквы, цифру и не содержать пробелов."
      );
      return;
    }
    if (password !== confirmPassword) {
      alert("Пароли не совпадают.");
      return;
    }
    // Проверка уникальности почты
    if (registeredUsers.find((user) => user.email === email)) {
      alert("Пользователь с такой почтой уже зарегистрирован.");
      return;
    }
    if (registeredUsers.find((user) => user.name === name)) {
      alert("Пользователь с таким именем пользователя уже зарегистрирован.");
      return;
    }
    // Если все проверки пройдены, сохраняем пользователя
    registeredUsers.push({ name, email, password });
    alert("Регистрация прошла успешно!");
    // Можно здесь добавить дополнительную логику, например редирект пользователя
    handleReset();
  };

  const handleReset = () => {
    setName("");
    setEmail("");
    setPassword("");
    setConfirmPassword("");
    setSubmitted(false);
  };

  const titleStyle: React.CSSProperties = {
    textAlign: "center",
    marginBottom: "24px",
    color: "#4a6cf7",
  };

  const linksContainerStyle: React.CSSProperties = {
    display: "flex",
    justifyContent: "center",
    marginTop: "16px",
  };

  return (
    <div className="form-container">
      <h2 style={titleStyle}>Регистрация</h2>
      <form onSubmit={handleSubmit}>
        <TextInput
          label="Имя"
          value={name}
          onChange={setName}
          placeholder="Введите имя"
          validationPattern={namePattern}
          errorMessage="Имя должно содержать только буквы и пробелы (2–50 символов)"
          required
          forceTouched={submitted}
        />
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
          validationPattern={passwordPattern}
          errorMessage="Пароль должен быть не менее 8 символов, содержать заглавную и строчную буквы, цифру"
          required
          forceTouched={submitted}
        />
        <PasswordInput
          label="Подтверждение пароля"
          value={confirmPassword}
          onChange={setConfirmPassword}
          placeholder="Подтвердите пароль"
          validationPattern={new RegExp(`^${password}$`)}
          errorMessage="Пароли не совпадают"
          required
          forceTouched={submitted}
        />
        <div
          style={{
            display: "flex",
            justifyContent: "stretch",
            gap: 10,
            flexDirection: "column",
            marginTop: "20px",
          }}
        >
          <StandardButton value="Зарегистрироваться" onClick={() => handleSubmit} color="primary" />
          <StandardButton value="Сбросить" onClick={handleReset} color="danger" />
        </div>
      </form>
      <div style={linksContainerStyle}>
        <Link to="/sign-in">Уже зарегистрированы? Войти</Link>
      </div>
    </div>
  );
}
