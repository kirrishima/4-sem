interface ButtonProps {
  title: string;
  onClick: () => void;
  disabled?: boolean;
}

const Button = ({ title, onClick, disabled = false }: ButtonProps) => {
  return (
    <button
      onClick={onClick}
      disabled={disabled}
      style={{
        backgroundColor: disabled ? "#5c9e9e" : "#61d6f8",
        border: "none",
        padding: "10px 20px",
        margin: "5px",
        borderRadius: "5px",
        color: "black",
        fontSize: "16px",
        cursor: disabled ? "not-allowed" : "pointer",
      }}
    >
      {title}
    </button>
  );
};

export default Button;
