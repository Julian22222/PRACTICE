import { useState } from "react";

const Input = (props) => {
  const [inputWord, setInputWord] = useState("");

  const hadleAdd = () => {
    props.setList([inputWord, ...props.list]);
    setInputWord("");
  };

  console.log(inputWord);

  return (
    <>
      <input
        value={inputWord}
        onChange={(event) => {
          setInputWord(event.target.value);
        }}
      ></input>
      <button onClick={hadleAdd}>Add</button>
    </>
  );
};

export default Input;
