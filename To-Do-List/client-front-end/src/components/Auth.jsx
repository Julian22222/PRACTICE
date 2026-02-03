import { useEffect, useState } from "react";

const Auth = ({ setShowAuth, setActiveUser }) => {
  // if you loged In - isLogin is true
  const [isLogIn, setIsLogin] = useState(true);
  const [loading, setLoading] = useState(false);

  // use email,password and confirm password to login and signup
  const [email, setEmail] = useState(null);
  const [password, setPassword] = useState(null);
  const [confirmPassword, setConfirmPassword] = useState(null);

  // all users data list
  const [allUsersList, setAllUsersList] = useState([]);

  // console.log(email, password, confirmPassword);

  //if password and confirm password doesn't mach
  const [error, setError] = useState(null);

  // if email exists in the database it will show error
  {
    /* if registration successful show msg */
  }
  const [emailErrorRegistration, setEmailErrorRegistration] = useState(null);

  const viewLogin = (status) => {
    setError(null);
    setIsLogin(status);
  };

  useEffect(() => {
    fetch(`${process.env.REACT_APP_SERVERURL}/users`)
      .then((allUsers) => allUsers.json())
      .then((data) => {
        setAllUsersList(data);
        console.log(allUsersList);
      })
      .catch((err) => {
        console.log(err);
      });
  }, [allUsersList]);

  /////////////////////////////////////////////////////////Sign UP

  const handleSignUp = async (e) => {
    e.preventDefault();
    // console.log(email);
    // console.log(password);

    // if we are on a Sign up page and password not
    // equal to confirm password we set an error in useState with msg
    if (!isLogIn && password !== confirmPassword) {
      setError("Make sure passwords match!");
      setTimeout(() => {
        setError(null);
      }, 2000);
    }

    // if the entered email not contains in the users List than add it to the database
    if (
      !allUsersList.find((el) => {
        return el.u_email === email;
      })
    ) {
      try {
        const response = await fetch(
          `${process.env.REACT_APP_SERVERURL}/users`,
          {
            method: "POST",
            headers: { "Content-Type": "application/json" },
            body: JSON.stringify({ email, password }),
          }
        );

        const data = await response.json();
        console.log(data);
      } catch (err) {
        console.error(err);
      }

      setEmailErrorRegistration("Registration is successful, Please Log In.");

      setTimeout(() => {
        setEmailErrorRegistration(null);
      }, 3000);
    } else {
      setEmailErrorRegistration(
        "Entered email already exists in the database ,please enter different email"
      );

      setTimeout(() => {
        setEmailErrorRegistration(null);
      }, 3000);
    }
  };

  /////////////////////////////////////////////////////////Log In

  const handleSubmit = async (e) => {
    // e.preventDefault();
    // setLoading(true);

    // const findExistUser = await allUsersList.find((el) => {
    //   return el.u_email === email;
    // });

    // setActiveUser(findExistUser.u_email);

    // setLoading(false);
    // setShowAuth(false);
    // console.log(showAuth);

    e.preventDefault();

    setLoading(true); // Start loading

    try {
      // Fetch the user from the server to check if email exists

      const response = await fetch(`${process.env.REACT_APP_SERVERURL}/users`);

      const allUsers = await response.json();

      // Find the existing user based on the email

      const findExistUser = allUsers.find((el) => el.u_email === email);

      if (findExistUser) {
        // Set the active user when found

        setActiveUser(findExistUser.u_email);
      } else {
        // Handle the case when the user is not found

        setError("User not found");
      }
    } catch (error) {
      console.error("Error fetching users:", error);

      setError("An error occurred, please try again later.");
    } finally {
      setLoading(false); // Stop loading after the request is complete, whether successful or failed). This guarantees the loading state is updated properly after the async operation.
    }

    // Hide the authentication modal after a successful login

    setShowAuth(false);
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
            value={email}
          />

          <input
            type="password"
            placeholder="password"
            onChange={(e) => setPassword(e.target.value)}
            value={password}
          />

          {/* if isLogin is not true show input confirm password */}
          {!isLogIn && (
            <input
              type="password"
              placeholder="confirm password"
              onChange={(e) => setConfirmPassword(e.target.value)}
            />
          )}

          <div className="submit-btn-container">
            {/* ///////////////////////////////// */}
            {/* if Login =true -> btn LogIn ,else SignUp */}
            {isLogIn ? (
              <div>
                <button
                  type="submit"
                  className="submit-btn"
                  onClick={(e) => handleSubmit(e)}
                >
                  Log In
                </button>
              </div>
            ) : (
              <div>
                <button
                  type="submit"
                  className="submit-btn"
                  onClick={handleSignUp}
                >
                  Sign Up
                </button>
              </div>
            )}
          </div>

          {loading && <div>Loading...</div>}

          {/* if password not equal to confirmed password show error */}
          {error && <p style={{ color: "red" }}>{error}</p>}

          {/* if the same email exists in database -> show msg */}
          {/* if registration successful show msg */}
          {emailErrorRegistration ? (
            <p style={{ color: "red" }}>{emailErrorRegistration}</p>
          ) : null}
        </form>

        <div className="auth-options">
          <button
            onClick={() => viewLogin(false)}
            //
            // if we are not on login the background will be white otherwise style -dark background
            // style={{
            //   backgroundColour: !isLogIn
            //     ? "rgb(255,255,255)"
            //     : "rgb(188,188,188)",
            // }}
            className="submit-btn-bottom-line"
          >
            Sign Up
          </button>
          <button
            onClick={() => viewLogin(true)}
            // style={{
            //   backgroundColour: isLogIn
            //     ? "rgb(255,255,255)"
            //     : "rgb(188,188,188)",
            // }}
            className="submit-btn-bottom-line"
          >
            Login
          </button>
        </div>
      </div>
    </div>
  );
};

export default Auth;
