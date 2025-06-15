import { useState } from 'react';

interface ButtonProps {
  title: string;
  onClickHandler: () => void;
  disabled?: boolean;
}

const Button =  function (
  {
  title,
  onClickHandler,
  disabled = false,
} : ButtonProps) {
  return (
    <button
      onClick={onClickHandler}
      disabled={disabled}
      style={{
        backgroundColor: disabled ? "#5c9e9e" : "#61d6f8",
        border: 'none',
        padding: '10px 20px',
        margin: '5px',
        borderRadius: '5px',
        color: 'black',
        fontSize: '16px',
        cursor: disabled ? 'not-allowed' : 'pointer'
      }}
    >
      {title}
    </button>
  );
};


const Counter = function () {
  const [count, setCount] = useState<number>(0);

  const handleIncrease = () => {
    setCount((prev) => prev + 1);
  };

  const handleReset = () => {
    setCount(0);
  };

  return (
    <div style={{ textAlign: 'center',background: 'black', color: count >= 5 ? "#d6153b" : "#63d6f7", width: 'fit-content', margin: 'auto' }}>
      <h1>{count}</h1>
      <div>
        <Button
          title="Increase"
          onClickHandler={handleIncrease}
          disabled={count >= 5}
        />
        <Button
          title="Reset"
          onClickHandler={handleReset}
          disabled={count === 0}
        />
      </div>
    </div>
  );
};

const App: React.FC = () => {
  return (
    <div className="App">
      <Counter />
    </div>
  );
};

export default App;