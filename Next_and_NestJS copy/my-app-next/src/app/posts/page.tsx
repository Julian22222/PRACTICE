import Link from "next/link";
import "./posts.css";
import { singlePost } from "@/shared/types/post";
import { getAllPosts } from "@/services/getPosts"; // can import Importing the function to fetch posts from services, then we need to use useEffect to call this function and set the data in state, but here we are using async/await directly in the component.
//by importing a function we need to use promises-> .then

async function fechData() {
  const response = await fetch(
    "https://jsonplaceholder.typicode.com/posts?_limit=10"
  );
  const result = await response.json();

  if (!result || result.length === 0) {
    throw new Error("No posts found"); // Handle error if no posts are found, will send this message to Error page- error.tsx
    //throw new Error("Failed to fetch posts"); // Handle error if fetch failss
  }

  return result;
}

interface Props {}

export default async function page({}: Props) {
  const data = await fechData(); // Fetching data from the API

  // console.log(data);

  return (
    <div>
      <h1 className="posts-header">Posts</h1>
      <ul>
        {data.map((post: singlePost) => (
          <li key={post.id} className="post-element">
            <h2>{post.title}</h2>
            <p>{post.body}</p>
            <Link href={`/posts/${post.id}`} className="posts-link">
              Read More
            </Link>
          </li>
        ))}
      </ul>
    </div>
  );
}
