import { useAppDispatch, useAppSelector } from "../redux/hooks";
import { increment, decrement, reset } from "../redux/actions";
import Button from "./Button";

const Counter = () => {
  const count = useAppSelector((state) => state.count);
  const dispatch = useAppDispatch();

  return (
    <div
      style={{
        textAlign: "center",
        background: "black",
        color: count >= 5 ? "#d6153b" : "#63d6f7",
        width: "fit-content",
        margin: "auto",
        padding: "20px",
        borderRadius: "10px",
      }}
    >
      <h1>{count}</h1>
      <div>
        <Button title="+" onClick={() => dispatch(increment())} disabled={count >= 5} />
        <Button title="–" onClick={() => dispatch(decrement())} disabled={count <= 0} />
        <Button title="Reset" onClick={() => dispatch(reset())} disabled={count === 0} />
      </div>
    </div>
  );
};

export default Counter;
