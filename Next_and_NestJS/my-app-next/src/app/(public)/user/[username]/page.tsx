import type { Metadata } from "next";

export const metadata: Metadata = {
  title: "User Page",
  description: "User profile page",
};

interface UserPageProps {
  params: {
    username: string | number; // The username can be a string or a number, depending on how you want to handle it
  };
}

export default function UserPage({ params }: UserPageProps) {
  //to get the params from the URL we use params
  return (
    <div>
      <h1>User Page</h1>
      <p>{params.username}</p>
      {/* // Displaying the username from the URL, it must be the same dynamuc name - username, as we used in folder [username] */}
    </div>
  );
}
