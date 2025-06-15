import React, { useState } from "react";
import { Link } from "react-router-dom";
import { TextInput } from "./InputComponents";
import { StandardButton } from "./Buttons";
import "./App.css";

export default function ResetPassword() {
  const [email, setEmail] = useState("");
  const [submitted, setSubmitted] = useState(false);

  const emailPattern = /^[^\s@]+@[^\s@]+\.[^\s@]+$/;

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    setSubmitted(true);

    if (!email.trim()) {
      alert("Пожалуйста, заполните обязательное поле.");
      return;
    }
    if (!emailPattern.test(email)) {
      alert("Введите корректный адрес электронной почты.");
      return;
    }
    // Для теста: сообщаем новый пароль пользователю
    const newPassword = "newPass123";
    alert(`Запрос на восстановление пароля отправлен. Ваш новый пароль: ${newPassword}`);
  };

  const titleStyle: React.CSSProperties = {
    textAlign: "center",
    marginBottom: "24px",
    color: "#4a6cf7",
  };

  const linksContainerStyle: React.CSSProperties = {
    display: "flex",
    justifyContent: "center",
    flexDirection: "column",
    marginTop: "16px",
  };

  return (
    <div className="form-container">
      <h2 style={titleStyle}>Восстановление пароля</h2>
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
        <div style={{ marginTop: "20px" }}>
          <StandardButton
            value="Восстановить пароль"
            onClick={() => handleSubmit({ preventDefault: () => {} } as React.FormEvent)}
            color="primary"
          />
        </div>
      </form>
      <div style={linksContainerStyle}>
        <Link to="/sign-in">Вернуться ко входу</Link>
      </div>
    </div>
  );
}
