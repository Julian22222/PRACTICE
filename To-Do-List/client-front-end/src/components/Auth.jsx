// import { useCookies } from "react-cookie";
import { useState } from "react";

const Auth = () => {
  // const [cookies, setCookie, removeCookie] = useCookies(null);

  // if you loged In - isLogin is true
  const [isLogIn, setIsLogin] = useState(true);

  // use email,password and confirm password to login and signup
  const [email, setEmail] = useState(null);
  const [password, setPassword] = useState(null);
  const [confirmPassword, setConfirmPassword] = useState(null);

  // console.log(email, password, confirmPassword);

  const [error, setError] = useState(null);

  const viewLogin = (status) => {
    setError(null);
    setIsLogin(status);
  };

  const handleSubmit = async (e, endpoint) => {
    e.preventDefault();

    // if we are on a Sign up page not on login page and password not
    // equal to confirm password we set an error in useState with msg
    if (!isLogIn && password !== confirmPassword) {
      setError("Make sure passwords match!");
      return;
    }

    if (endpoint === "signup") {
      const response = await fetch(`${process.env.REACT_APP_SERVERURL}/users`, {
        method: "POST",
        headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ email, password }),
      });

      const data = await response.json();
      console.log(data);
      // if (data.detail) {
      //   setError(data.detail);
      // } else {
      //   setCookie("Email", data.email);
      //   setCookie("AuthToken", data.token);

      //   window.location.reload();
      // }
    }
  };

  return (
    <div className="auth-container">
      <div className="auth-container-box">
        <form>
          <h2>{isLogIn ? "Please log in" : "Please sign up!"}</h2>
          <input
            type="email"
            placeholder="email"
            onChange={(e) => setEmail(e.target.value)}
          />
          <input
            type="password"
            placeholder="password"
            onChange={(e) => setPassword(e.target.value)}
          />

          {/* if isLogin is not true show input confirm password */}
          {!isLogIn && (
            <input
              type="password"
              placeholder="confirm password"
              onChange={(e) => setConfirmPassword(e.target.value)}
            />
          )}
          <input
            type="submit"
            className="create"
            onClick={(e) => handleSubmit(e, isLogIn ? "login" : "signup")}
          />

          {/* if error does exist then show error */}
          {error && <p>{error}</p>}
        </form>

        <div className="auth-options">
          <button
            onClick={() => viewLogin(false)}
            //
            // if we are not on login the background will be white otherwise style -dark background
            style={{
              backgroundColour: !isLogIn
                ? "rgb(255,255,255)"
                : "rgb(188,188,188)",
            }}
          >
            Sign Up
          </button>
          <button
            onClick={() => viewLogin(true)}
            style={{
              backgroundColour: isLogIn
                ? "rgb(255,255,255)"
                : "rgb(188,188,188)",
            }}
          >
            Login
          </button>
        </div>
      </div>
    </div>
  );
};

export default Auth;
