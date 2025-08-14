```jS
"use client"; // This file is a client component, allowing it to use hooks like useState, useEffect, etc.

import { useEffect, useState } from "react";
import { singlePost } from "@/shared/types/post";
import Link from "next/link";

async function getPost(id: string | null) {
  const result = fetch(`https://jsonplaceholder.typicode.com/posts/${id}`).then(
    (res) => res.json()
  );

  return result;
}

// params is a Promise — so we call .then() in a useEffect to resolve it.
// We store id in state — so it triggers the second useEffect for fetching.
// No async function component — avoids the original Next.js error

interface Props {
  params: Promise<{
    id: string;
  }>;
}

export default function Post({ params }: Props) {
  //{params} getting the params (id of the post) from the URL
  //   const id = { useParams };

  const [id, setId] = useState<string | null>(null); // State to hold the post ID
  const [post, setPost] = useState<singlePost | null>(null); // State to hold the post data

  useEffect(() => {
    // Unwrap params
    params.then((resolvedParams) => {
      setId(resolvedParams.id); // Set the ID in state
    });
  }, [params]);

  // console.log("Post ID:", id); // Log the post ID to the console

  useEffect(() => {
    const fetchData = async () => {
      const data = await getPost(id);
      setPost(data); // Set the post data in state
    };

    if (id) {
      fetchData(); // Fetch post data only if id is available
    }
  }, [id]);

  return (
    <div>
      <h2>Post Page</h2>
      <Link href="/posts">Back</Link>
      <br />
      <br />
      {id && <p>{id}</p>}
      {/* Displaying the post ID from the URL, name id should be the same as we have in -->[id] */}

      {post && (
        <>
          <p>{post.title}</p>
          <p>{post.body}</p>
        </>
      )}
    </div>
  );
}

```
