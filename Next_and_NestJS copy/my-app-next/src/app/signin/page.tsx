import { GoogleButton } from "@/components/GoogleButton";
import { SignInForm } from "@/components/SignInForm";
import "./signin.css";

interface Props {}

export default function Signin({}: Props) {
  return (
    <div>
      <h1 className="signIn-header">SignIn</h1>
      <br />
      <GoogleButton />
      <div style={{ display: "flex", justifyContent: "center" }}>or</div>
      <SignInForm />
    </div>
  );
}
