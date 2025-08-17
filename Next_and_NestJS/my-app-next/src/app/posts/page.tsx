import Link from "next/link";
import "./posts.css";
import { singlePost } from "@/shared/types/post";

async function fechData() {
  const response = await fetch(
    "https://jsonplaceholder.typicode.com/posts?_limit=10"
  );
  const result = await response.json();

  if (!result || result.length === 0) {
    throw new Error("No posts found"); // Handle error if no posts are found, will send this message to Error page- error.tsx
  }

  return result;
}

interface Props {}

export default async function page({}: Props) {
  const data = await fechData(); // Fetching data from the API

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
