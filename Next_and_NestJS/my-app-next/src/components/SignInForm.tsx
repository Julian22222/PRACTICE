"use client";

import { useRouter } from "next/navigation";
import { signIn } from "next-auth/react";

interface Props {}

export function SignInForm({}: Props) {
  const router = useRouter();

  const handleSubmit = async (e: React.FormEvent<HTMLFormElement>) => {
    e.preventDefault();

    //to handle form submission, without create a useState for each input field
    const formData = new FormData(e.currentTarget); //e.currentTarget is the form element/ we are creating a new FormData object from the form element

    //signIn is build-in method from next-auth/react
    //we define the provider as "credentials" because we are using Credentials Provider in config/auth.ts, line 36, and credentials contains email and password fields from the config/auth.ts line 20-33
    //therefore we need to pass email and password to the signIn method, credentials contains email and password fields in the config/auth.ts line 20-33
    const res = await signIn("credentials", {
      //signIn is a built-in NextAuth function that handles logging in a user.
      email: formData.get("email"), //email value will be taken from the form input field with name="email"
      password: formData.get("password"),
      redirect: false, //in case of error will redirect user to build-in signin form, if it is set to true. In this case we set it to false, so we can handle the error ourselves
      // callbackUrl: "/profile", //after successful sign in, we will redirect user to /profile page
      callbackUrl: "/profile", //after successful sign in, we will redirect user to /profile page. // 👈 important: tell NextAuth where to go
    });

    if (res?.error) {
      // ❌ login failed
      console.log("Error signing in:", res.error);
      console.log("res:", res);
    } else if (res?.url) {
      //   router.push("/profile"); //redirect user to /profile page
      // ✅ if login was successful
      router.push(res?.url);
    } // Ensure this closing brace is here
  };

  return (
    <div className="page-container">
      <form className="form-container" onSubmit={handleSubmit}>
        <div className="email-container">
          <label style={{ marginRight: "5px" }} htmlFor="email">
            Email:
          </label>
          <input style={{ width: "100%" }} type="email" name="email" required />
        </div>

        <div className="pas-container">
          <label htmlFor="password">Password:</label>
          <input
            style={{ width: "100%" }}
            type="password"
            name="password"
            required
          />
        </div>
        <div
          style={{
            display: "flex",
            justifyContent: "center",
            backgroundColor: "green",
            borderRadius: "10px",
            padding: "5px",
            margin: "10px",
          }}
        >
          <button type="submit">Sign In </button>
        </div>
      </form>
    </div>
  );
}
