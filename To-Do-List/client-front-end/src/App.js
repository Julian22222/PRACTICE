import "./App.css";
import ListHeader from "./components/ListHeader";
import ListItem from "./components/ListItem";
import Auth from "./components/Auth";
import { useEffect, useState } from "react";
// import { useCookies } from "react-cookie";

function App() {
  // const [cookies, setCookie, removeCookie] = useCookies(null);
  // const userEmail = cookies.Email;
  // const authToken = cookies.AuthToken;

  const userEmail = "julian@test.com";

  // list of tasks that are shown
  const [tasks, setTasks] = useState(null);

  // it will dicktate will we see Auth component or not
  // const authToken = false;

  const loginExists = async () => {
    try {
      const response = await fetch(`${process.env.REACT_APP_SERVERURL}/users`);

      const json = await response.json();
      console.log(json.rows);
    } catch (err) {
      console.error(err);
    }
  };

  console.log(loginExists());

  const getData = async () => {
    try {
      const response = await fetch(
        `${process.env.REACT_APP_SERVERURL}/todos/${userEmail}`
      );
      const json = await response.json();
      setTasks(json);
    } catch (err) {
      console.error(err);
    }
  };

  useEffect(() => {
    // if (authToken) {
    getData();
    // }
  }, []);

  // console.log(tasks);

  // Sort by date
  // tasks?.sort  -> if tasks exists we will sort them
  const sortedTasks = tasks?.sort(
    (a, b) => new Date(a.date) - new Date(b.date)
  );

  return (
    <div className="app">
      {/* if no authToken exists we will show Auth component */}
      {/* {!authToken && <Auth />} */}
      {/* if authTokken is true we will see everithin in here */}

      {/* Login - Sign Up form  */}
      {/* <Auth /> */}

      {/* //////////////////////////////////////////////////////////////////////////////////// */}
      {/* //////////////////////////////////////////////////////////////////////////////// */}
      <ListHeader listName={" 🏝  To do list"} getData={getData} />
      <p className="user-email">Welcome back {userEmail}</p>
      {sortedTasks?.map((task) => (
        <ListItem key={task.id} task={task} getData={getData} />
      ))}
      {/* ///////////////////////////////////////////////////////////////////////////////////// */}
      {/* /////////////////////////////////////////////////////////////////////////////////// */}
      <p className="copyright">Your Daily Diary</p>
    </div>
  );
}

export default App;
