import React, { useState, useEffect, KeyboardEvent } from 'react';
import Display from './Display';
import Button from './Button';
import History from './History';

export default function Calculator() {
  const [input, setInput] = useState<string>('');
  const [result, setResult] = useState<string>('');
  const [history, setHistory] = useState<string[]>([]);
  const [theme, setTheme] = useState<'light' | 'dark'>('light');

  const handleButtonClick = (value: string) => {
    if (value === 'C') {
      setInput('');
      setResult('');
    } else if (value === '⌫') {
      setInput(input.slice(0, -1));
    } else if (value === '=') {
      calculateResult();
    } else {
      // Предотвращение некорректного ввода, например, лишних нулей в начале
      if (input === '' && value === '0') return;
      setInput(input + value);
    }
  };

  const calculateResult = () => {
    // Если ввод пуст или равен "0", не делаем ничего
    if (input.trim() === '' || input.trim() === '0') {
      return;
    }
    try {
      // Проверка деления на ноль
      if (/\/0(?!\.)/.test(input)) {
        setResult('Ошибка: Деление на ноль');
        return;
      }
      // Для лабораторной работы можно использовать eval,
      // но в продакшене рекомендуется использовать специализированный парсер.
      const evalResult = eval(input);
      console.log(evalResult);
      setResult(evalResult.toString());
      setHistory([`${input} = ${evalResult}`, ...history]);
      setInput('');
    } catch (error) {
      setResult('Ошибка');
    }
  };

  // Обработка событий клавиатуры
  useEffect(() => {
    const handleKeyDownWindow = (e: KeyboardEvent) => {
      if (/^[0-9+\-*/.]$/.test(e.key)) {
        setInput(prev => prev + e.key);
      } else if (e.key === 'Backspace') {
        setInput(prev => prev.slice(0, -1));
      } else if (e.key === 'Enter') {
        calculateResult();
      }
    };

    window.addEventListener('keydown', handleKeyDownWindow as any);
    return () => window.removeEventListener('keydown', handleKeyDownWindow as any);
  }, [input, history]);

  const toggleTheme = () => {
    setTheme(prev => (prev === 'light' ? 'dark' : 'light'));
  };

  return (
    <div className={`calculator-container ${theme}`} tabIndex={0}>
      <button onClick={toggleTheme} className="theme-toggle">
        {theme === 'light' ? 'Тёмная тема' : 'Светлая тема'}
      </button>
      <Display input={input} result={result} />
      <div className="buttons-container">
        {[
          '7', '8', '9', '/', '⌫',
          '4', '5', '6', '*', 'C',
          '1', '2', '3', '-', '=',
          '0', '.', '+'
        ].map((btn) => (
          <Button value={btn} onClick={handleButtonClick} />
        ))}
      </div>
      <History history={history} />
    </div>
  );
}
