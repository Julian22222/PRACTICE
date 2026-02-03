import type { Metadata } from "next";
import { Tweets } from "@/shared/data/tweets.data";

export const metadata: Metadata = {
  title: "User Page",
  description: "User profile page",
};

async function fetchData(username: string) {
  // const res = await fetch(
  //   "https://jsonplaceholder.typicode.com/users/${username}",
  //   {
  //     next: {
  //       revalidate: 60, // Revalidate the data every 60 seconds
  //     },
  //   }
  // );
  // return res.json();

  const res = Tweets.find((tweet) => tweet.author === username);
  console.log("Fetched user data:", res); // Logging the fetched user data for debugging
  return res || null; // Return the user data if found, otherwise return null
}

type UserPageProps = {
  params: {
    // username is a dynamic parameter in the URL, defined by the folder name [username]
    //username - is because or [username] folder name, it must be the same as we used in folder name
    username: string; // The username can be a string or a number, depending on how you want to handle it
  };
};

export default async function UserPage({ params }: UserPageProps) {
  //to get the params from the URL we use params
  console.log("UserPage params:", params); // Logging the params to see what we get

  const modifiedParams = {
    ...params,
    username: params.username.replace("%20", " "),
  }; // Example of modifying params if needed

  // const userData = await fetchData(params.username); // Fetching user data based on the username from the URL

  const userData = await fetchData(modifiedParams.username);

  return (
    <div>
      <h1>User Page</h1>
      <p>{params.username}</p>
      {/* // Displaying the username from the URL, it must be the same dynamic name - username, as we used in folder [username] */}
      {userData ? (
        <div>
          <h2>User Details</h2>
          <p>Author: {userData.author}</p>
          <p>Text: {userData.text}</p>
        </div>
      ) : (
        <p>User not found</p>
      )}
    </div>
  );
}
