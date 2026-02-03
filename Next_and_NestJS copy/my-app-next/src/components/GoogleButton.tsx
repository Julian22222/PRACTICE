"use client";

import { signIn } from "next-auth/react";
import { useSearchParams } from "next/navigation";

interface Props {}

export function GoogleButton({}: Props) {
  const searchParams = useSearchParams(); //hook Must be used in a Client side Component. It returns the URLSearchParams object of the current URL
  const callbackUrl = searchParams?.get("callbackUrl") || "/profile"; //if there is no callbackUrl in the URL, we will redirect user to /profile page after successful sign in

  return (
    <div className="google-page">
      <div className="signIn-google-btn">
        <button
          onClick={() => signIn("google", { callbackUrl })} //signIn is build-In method from next-auth/react, inside we pass the name of the provider we want to use - google
          //callbackUrl is optional, it will redirect user to the specified URL after successful sign in
        >
          <img
            src="/googleProvider-logo.png"
            alt="Google Logo"
            style={{ width: "50px" }}
          />
          {/* Sign In with Google */}
        </button>
      </div>
    </div>
  );
}
