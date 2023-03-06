import "./App.css";
import ListHeader from "./components/ListHeader";
import ListItem from "./components/ListItem";
import { useEffect, useState } from "react";

function App() {
  const userEmail = "julian@test.com";

  const [tasks, setTasks] = useState(null);

  const getData = async () => {
    try {
      const response = await fetch(`http://localhost:9090/todos/${userEmail}`);
      const json = await response.json();
      setTasks(json);
    } catch (err) {
      console.log(err);
    }
  };

  useEffect(() => {
    getData();
  }, []);

  console.log(tasks);

  // Sort by date
  // tasks?.sort  -> if tasks exists we will sort them
  const sortedTasks = tasks?.sort(
    (a, b) => new Date(a.date) - new Date(b.date)
  );

  return (
    <div className="app">
      <ListHeader listName={" 🏝  Holiday tick list"} getData={getData} />
      {sortedTasks?.map((task) => (
        <ListItem key={task.id} task={task} getData={getData} />
      ))}
    </div>
  );
}

export default App;
