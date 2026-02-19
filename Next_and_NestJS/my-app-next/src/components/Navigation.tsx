"use client"; //if we use usePathname

import { useSession, signIn, signOut } from "next-auth/react";
import Link from "next/link";
import { usePathname } from "next/navigation"; // Importing usePathname to get the current path

type NavLink = {
  href: string;
  label: string;
};

interface Props {
  navLinks: NavLink[];
}

export function Navigation({ navLinks }: Props) {
  const pathname = usePathname(); // Using usePathname to get the current URL path where the user is currently located

  const session = useSession(); //it is from NextAuth library. we can use session data to show/hide links based on auth status. it will help to show is the user Authorized or not.
  console.log("Session data:", session); // Log session data to see its structure and contents

  return (
    <nav className="flex gap-6 text-white/80">
      {navLinks.map((link) => {
        const isActive = pathname === link.href; // Check if the current path matches the link's href

        return (
          <Link
            key={link.href}
            href={link.href}
            className={isActive ? "text-white font-bold" : ""}
          >
            {link.label}
          </Link>
        );
      })}

      {/* If session data exists, we will show another Link button in Navigation menu to our Profile page */}
      {session?.data && ( //if session data exists, it means user is logged in
        <Link href={"/profile"}>Profile</Link>
      )}

      {/* If session data exists - show Sign Out button otherwise show SignIn button */}
      {session?.data ? (
        <Link
          href={"#"}
          onClick={() => {
            signOut({ callbackUrl: "/" }); // Redirect to home page after sign out, signOut function is build-in from next-auth/react
          }}
        >
          Sign Out
        </Link>
      ) : (
        // <Link href={"/api/auth/signin"}>Sign In</Link>
        <Link href="/signin">Sign In</Link> //if you have your own custom sign in page at /signin
      )}
    </nav>
  );
}
