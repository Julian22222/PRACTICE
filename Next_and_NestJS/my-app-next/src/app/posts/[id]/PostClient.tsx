"use client"; // This file is a client component, allowing it to use hooks like useState, useEffect, etc.

import { useEffect, useState } from "react";
import { singlePost } from "@/shared/types/post";
import { Post_element } from "@/components/Post_element";

async function getPost(id: string | null) {
  const result = fetch(`https://jsonplaceholder.typicode.com/posts/${id}`).then(
    (res) => res.json()
  );
  return result;
}

interface Props {
  params: { id: string };
}

export default function PostClient({ params }: Props) {
  const [id, setId] = useState<string | null>(null); // State to hold the post ID
  const [post, setPost] = useState<singlePost | null>(null); // State to hold the post data

  useEffect(() => {
    // Unwrap params
    params.then((resolvedParams) => {
      setId(resolvedParams.id); // Set the ID in state
    });
  }, [params]);

  // useEffect(() => {
  //   setId(params.id);
  // }, [params]);

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

  // useEffect(() => {
  //   if (id) {
  //     getPost(id).then((data) => setPost(data));
  //   }
  // }, [id]);

  return (
    <div>
      <h2 className="post-header">Post Page</h2>
      <Post_element id={id} post={post} />
    </div>
  );
}
