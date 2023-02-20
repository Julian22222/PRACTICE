import "./App.css";
import { useState } from "react";
import Header from "./components/Header";
import Input from "./components/Input";
import RemoveBtn from "./components/RemoveBtn";

function App() {
  const [list, setList] = useState([
    "studying 5 hours",
    "gym at 8pm",
    " cooking",
    "call mom ",
  ]);

  console.log(list);

  return (
    <div className="App">
      <Header />
      <Input setList={setList} list={list} />
      <ul>
        {list.map((el) => {
          return (
            <li key={el}>
              {el}
              <RemoveBtn setList={setList} item={el} />
            </li>
          );
        })}
      </ul>
    </div>
  );
}

export default App;
