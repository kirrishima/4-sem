import React from "react";
import Colors from "../Colors";

export enum ButtonVariant {
  Primary = "primary",
  Secondary = "secondary",
  Tertiary = "tertiary",
}

export interface ButtonProps {
  text: string;
  appearance: ButtonVariant;
}

export default function Button({ text, appearance }: ButtonProps) {
  let backgroundColor: string;
  let color: string;
  let border: string = "none";
  switch (appearance) {
    case ButtonVariant.Primary:
      backgroundColor = Colors.Primary;
      color = Colors.Background;
      break;
    case ButtonVariant.Secondary:
      backgroundColor = Colors.Secondary;
      color = Colors.Background;
      break;
    case ButtonVariant.Tertiary:
      backgroundColor = Colors.UIBackground;
      color = Colors.Text;
      border = `${Colors.Text} 1px solid`;
      break;
  }

  const baseStyle: React.CSSProperties = {
    width: 330,
    padding: "12px 20px",
    border: border,
    borderRadius: 6,
    height: 50,
    cursor: "pointer",
    fontSize: 16,
    fontWeight: 500,
    backgroundColor,
    color,
    transition: "opacity 0.2s ease",
    display: "inline-block",
    textAlign: "center",
  };

  return (
    <button
      style={baseStyle}
      onMouseEnter={(e) => (e.currentTarget.style.opacity = "0.85")}
      onMouseLeave={(e) => (e.currentTarget.style.opacity = "1")}
    >
      {text}
    </button>
  );
}
