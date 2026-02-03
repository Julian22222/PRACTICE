import "./App.css";
import ListHeader from "./components/ListHeader";
import ListItem from "./components/ListItem";
import Auth from "./components/Auth";
import { useEffect, useState } from "react";

function App() {
  // show ligin/register form if =true, eslse show todo list
  // it will dicktate will we see Auth component or not
  const [showAuth, setShowAuth] = useState(true);

  // remember the LogedIn email for user, when you Log In
  const [activeUser, setActiveUser] = useState(null);

  // const userEmail = "julian@test.com";

  const userEmail = activeUser;

  // list of tasks that are shown
  const [tasks, setTasks] = useState(null);

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
    getData();
  }, [activeUser]);

  // console.log(tasks);

  // Sort by date
  // tasks?.sort  -> if tasks exists we will sort them
  const sortedTasks = tasks?.sort(
    (a, b) => new Date(a.date) - new Date(b.date)
  );

  return (
    <div className="app">
      {/* Login - Sign Up form  */}
      {showAuth ? (
        <Auth setShowAuth={setShowAuth} setActiveUser={setActiveUser} />
      ) : (
        <div className="list-container-full">
          {/* //////////////////////////////////////////////////////////////////////////////// */}
          <ListHeader
            listName={" 🏝  To do list"}
            getData={getData}
            activeUser={activeUser}
            setActiveUser={setActiveUser}
            setShowAuth={setShowAuth}
          />
          <p className="user-email">Welcome back {userEmail}</p>
          {sortedTasks?.map((task) => (
            <ListItem key={task.id} task={task} getData={getData} />
          ))}
          {/* ///////////////////////////////////////////////////////////////////////////////////// */}

          <p className="copyright">Your Daily Diary</p>
        </div>
      )}
    </div>
  );
}

export default App;
