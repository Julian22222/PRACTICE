import "./Styles/App.css";
import "./Styles/CurrentUsers.css";
import "./Styles/IndividualWaitingUser.css";
import "./Styles/IndividualCurrentUser.css";
import "./Styles/Modal.css";
import "./Styles/Chat.css";
import WaitingList from "./Components/WaitingList";
import Currentusers from "./Components/CurrentUsers";
import Header from "./Components/Header";
import { Route, BrowserRouter, Routes } from "react-router-dom";
import IndividualWaitingUser from "./Components/IndividualWaitingUser";
import Services from "./Components/Services";
import Chat from "./Components/Chat";
import { useUsers } from "./hooks/users"; // custom hook to fetch users and current customers
import IndividualCurrentUser from "./Components/IndividualCurrentUser";

function App() {
  const { users, currentCustomers } = useUsers(); // destructure the hook to use the data it provides

  return (
    <div className="App">
      <BrowserRouter>
        <Header />
        <Routes>
          <Route
            path="/"
            element={<Currentusers customers={currentCustomers} />}
          />
          <Route
            path="/current-user/:car_id"
            element={<IndividualCurrentUser />}
          />
          <Route path="/waiting-list" element={<WaitingList users={users} />} />
          <Route path="/waiting-list/:id" element={<IndividualWaitingUser />} />
          <Route path="/services/" element={<Services />} />
          <Route path="/chat/" element={<Chat />} />
        </Routes>
      </BrowserRouter>

      {/* <Card
        variant={CardVariant.outlined}
        width="200px"
        height="200px"
        myFunc={(num: number) => console.log("Clicked", num)}
      >
        <button>Button</button>
        <p>Hello World!!</p>
      </Card> */}
    </div>
  );
}

export default App;
