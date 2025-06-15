
// Standard Button Component
type StandardButtonProps = {
  value: string;
  onClick: (value: string) => void;
  color?: "primary" | "secondary" | "success" | "danger";
};

export function StandardButton({ value, onClick, color = "primary" }: StandardButtonProps) {
  // Color styles mapping
  const colorMap = {
    primary: {
      main: "#4a6cf7",
      hover: "#3a5be0",
      text: "#ffffff",
    },
    secondary: {
      main: "#6c757d",
      hover: "#5a6268",
      text: "#ffffff",
    },
    success: {
      main: "#2ecc71",
      hover: "#27ae60",
      text: "#ffffff",
    },
    danger: {
      main: "#e74c3c",
      hover: "#c0392b",
      text: "#ffffff",
    },
  };

  const buttonStyle: React.CSSProperties = {
    background: colorMap[color].main,
    border: "none",
    color: colorMap[color].text,
    padding: "10px 20px",
    fontSize: "16px",
    fontWeight: 500,
    cursor: "pointer",
    borderRadius: "4px",
    transition: "all 0.3s ease",
  };

  return (
    <button
      style={buttonStyle}
      onMouseDown={(e) => e.preventDefault()}
      onClick={() => onClick(value)}
      onMouseEnter={(e) => {
        e.currentTarget.style.background = colorMap[color].hover;
      }}
      onMouseLeave={(e) => {
        e.currentTarget.style.background = colorMap[color].main;
      }}
    >
      {value}
    </button>
  );
}
