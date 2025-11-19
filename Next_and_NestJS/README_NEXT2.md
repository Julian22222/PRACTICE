# Fetch data and assign to variables or to useState

⭐ If we use Context - to have global scope to some useStates -> all variables that we want to use from Context in other components, must be --> "use client"

always you need to use - Server side to fetch the data and then pass it to another component -> to use Hooks or client interactions - onClick, etc.

```JS
//It will work but it is Not Good example!!!!
//you can fetch data inside useEffect in "use clieent" component but it is not good practice

"use client";
import Header from "@/src/components/Header";
import Link from "next/link";
import React, { useEffect } from "react";
import "./login.css";
import { User } from "../../shared/types/user.interface";
import { redirect } from "next/navigation"; // Importing redirect function for navigation
import { useRouter } from "next/navigation";
import { useGlobal } from "../Context"; //IMPORT GLOBAL CONTEXT, Global UseState

async function fetchAllUsers() {
  const res = await fetch("http://localhost:3005/users");
  return res.json();
}

export default function LoginPage() {
  const { allUsers, setAllUsers, activeUser, setActiveUser } = useGlobal();

  const router = useRouter();

  const [formInput, setFormInput] = React.useState({ email: "", password: "" });
  const [loginError, setLoginError] = React.useState(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const users = await fetchAllUsers();
        console.log("Fetched All Users from useEffect:", users);
        setAllUsers(users);
      } catch (error) {
        console.error("Error fetching users:", error);
      }
    };

    fetchData();
    //the same code-->
    // fetchAllUsers().then((data) => {
    //   console.log("Fetched users:", data);
    //   setAllUsers(data);
    // });
    // console.log("Active allUsers from UseEffect :", allUsers);
  }, []);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();
    console.log("Form submitted");
    console.log("Form Input:", formInput);

    console.log("All Users from hadleSubmit:", allUsers);

    allUsers.map((user) => {
      if (
        user.email === formInput.email &&
        user.password === formInput.password
      ) {
        console.log("User found:", user);

        setActiveUser(user);
        console.log("All Users:", allUsers);
        console.log("Active User before redirect:", activeUser);
        console.log("Active User set to:", user);

        router.push("/user-page");

      } else {
        console.log("User not found");
        setLoginError(true);
      }
    });
  };

  ////////////////////////////////////////////////

  return (
    <div
      style={{
        fontFamily: "Arial, sans-serif",
        backgroundColor: "#f8f8f8",
        minHeight: "100vh",
      }}
    >
      {/* Header */}
      <Header />

      {/* Main Section */}
      <main className="main-login-container">
        <div className="login-box">
          {/* Title */}
          <h2 className="login-title">Log on to your account</h2>

          {/* Username */}
          <form onSubmit={handleSubmit}>
            <label style={labelStyle}>User ID</label>
            <input
              type="text"
              placeholder="Enter your User ID"
              value={formInput.email}
              name="email"
              onChange={(e) =>
                setFormInput({ ...formInput, email: e.target.value })
              }
            />

            {/* Password */}
            <label style={labelStyle}>Password</label>
            <input
              type="password"
              placeholder="Enter your Password"
              value={formInput.password}
              name="password"
              onChange={(e) =>
                setFormInput({ ...formInput, password: e.target.value })
              }
            />

            {/* Remember Me */}
            <div className="remember-me">
              <input
                type="checkbox"
                id="remember"
                style={{ marginRight: "8px" }}
              />
              <label
                htmlFor="remember"
                style={{ fontSize: "14px", color: "#333" }}
              >
                Remember my User ID
              </label>
            </div>

            <input type="submit" value="Log on" className="login-button" />
          </form>

          {loginError && (
            <div>
              Invalid User ID or Password
            </div>
          )}
    </div>
  );
}

```

```JS
//Example 1
//server side

export default async function page({}: Props) {

  const data = await fetch("http://localhost:3001/myposts");  //fetch data and map
  const myposts = await data.json();

  return (
    <>
      <h1>Local Server Page</h1>

      <ul>
        {myposts.map((post: Mypost) => (
          <li key={post.id}>
            <h2>{post.title}</h2>
          </li>
        ))}
      </ul>
    </>
  );
}
```

```JS
//Example 2
//Server side

async function fetchAllUsers() {
  const res = await fetch("http://localhost:3005/users"); //fetch the data and pass it to another component --> "use client"
  return res.json();
}

export default async function LoginPage() {
  const users = await fetchAllUsers();
  // console.log("Fetched All Users from useEffect:", users);

  return (
    <div>

      {/* passing fetched data to "use client" component -> there we can use useState to assign this data */}
      <LoginForm users={users} />
    </div>);
}

```
