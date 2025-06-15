import React, { useState } from "react";

// Общие стили
const commonLabelStyle: React.CSSProperties = {
  display: "block",
  marginBottom: "8px",
  fontWeight: 500,
  color: "#333333",
  textAlign: "left",
};

const commonErrorStyle: React.CSSProperties = {
  color: "#e74c3c",
  fontSize: "14px",
  marginBottom: "16px",
};

const baseInputStyle: React.CSSProperties = {
  display: "block",
  width: "100%",
  padding: "10px 12px",
  marginBottom: "4px",
  borderRadius: "4px",
  fontSize: "16px",
  transition: "all 0.3s ease",
  boxSizing: "border-box",
};

// TextInput Component с поддержкой required и forceTouched
type TextInputProps = {
  label: string;
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
  validationPattern?: RegExp;
  errorMessage?: string;
  required?: boolean;
  forceTouched?: boolean; // если true — показываем ошибку, даже если поле не было затронуто
};

export function TextInput({
  label,
  value,
  onChange,
  placeholder = "",
  validationPattern,
  errorMessage,
  required = false,
  forceTouched = false,
}: TextInputProps) {
  const [touched, setTouched] = useState(false);
  const [isValid, setIsValid] = useState(true);

  // Определяем, нужно ли показать ошибку
  const hasBeenTouched = touched || forceTouched;

  // Функция валидации учитывает обязательное заполнение
  const validateInput = (text: string) => {
    if (required && text.trim() === "") {
      setIsValid(false);
      return false;
    }
    if (validationPattern) {
      const valid = validationPattern.test(text);
      setIsValid(valid);
      return valid;
    }
    setIsValid(true);
    return true;
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newValue = e.target.value;
    onChange(newValue);
    validateInput(newValue);
  };

  const handleBlur = () => {
    setTouched(true);
    validateInput(value);
  };

  const inputStyle: React.CSSProperties = {
    ...baseInputStyle,
    border: `1px solid ${hasBeenTouched && !isValid ? "#e74c3c" : "#ddd"}`,
  };

  // Если поле пустое и required — используем дефолтное сообщение об ошибке
  const displayErrorMessage =
    hasBeenTouched && !isValid ? errorMessage || (required && value.trim() === "" && "это поле обязательно") : "";

  return (
    <div style={{ marginBottom: "16px" }}>
      <label style={commonLabelStyle}>{label}</label>
      <input
        style={inputStyle}
        type="text"
        value={value}
        onChange={handleChange}
        onBlur={handleBlur}
        placeholder={placeholder}
        required={required}
      />
      {displayErrorMessage && <div style={commonErrorStyle}>{displayErrorMessage}</div>}
    </div>
  );
}

// PasswordInput Component с поддержкой required и forceTouched
type PasswordInputProps = {
  label: string;
  value: string;
  onChange: (value: string) => void;
  placeholder?: string;
  validationPattern?: RegExp;
  errorMessage?: string;
  required?: boolean;
  forceTouched?: boolean;
};

export function PasswordInput({
  label,
  value,
  onChange,
  placeholder = "",
  validationPattern,
  errorMessage,
  required = false,
  forceTouched = false,
}: PasswordInputProps) {
  const [showPassword, setShowPassword] = useState(false);
  const [touched, setTouched] = useState(false);
  const [isValid, setIsValid] = useState(true);

  const hasBeenTouched = touched || forceTouched;

  const validateInput = (text: string) => {
    if (required && text.trim() === "") {
      setIsValid(false);
      return false;
    }
    if (validationPattern) {
      const valid = validationPattern.test(text);
      setIsValid(valid);
      return valid;
    }
    setIsValid(true);
    return true;
  };

  const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
    const newValue = e.target.value;
    onChange(newValue);
    validateInput(newValue);
  };

  const handleBlur = () => {
    setTouched(true);
    validateInput(value);
  };

  const toggleShowPassword = () => {
    setShowPassword(!showPassword);
  };

  const inputGroupStyle: React.CSSProperties = {
    position: "relative",
    marginBottom: "16px",
  };

  const inputStyle: React.CSSProperties = {
    ...baseInputStyle,
    border: `1px solid ${hasBeenTouched && !isValid ? "#e74c3c" : "#ddd"}`,
  };

  const toggleButtonStyle: React.CSSProperties = {
    position: "absolute",
    right: "12px",
    top: "50%",
    transform: "translateY(-50%)",
    background: "none",
    border: "none",
    cursor: "pointer",
    color: "#6c757d",
  };

  const displayErrorMessage =
    hasBeenTouched && !isValid ? errorMessage || (required && value.trim() === "" && "это поле обязательно") : "";

  return (
    <div style={{ marginBottom: "16px" }}>
      <label style={commonLabelStyle}>{label}</label>
      <div style={inputGroupStyle}>
        <input
          style={inputStyle}
          type={showPassword ? "text" : "password"}
          value={value}
          onChange={handleChange}
          onBlur={handleBlur}
          placeholder={placeholder}
          required={required}
        />
        <button
          type="button"
          style={toggleButtonStyle}
          onMouseDown={(e) => e.preventDefault()}
          onClick={toggleShowPassword}
        >
          {showPassword ? "⛔" : "👀"}
        </button>
      </div>
      {displayErrorMessage && <div style={commonErrorStyle}>{displayErrorMessage}</div>}
    </div>
  );
}
