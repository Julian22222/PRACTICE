"use client"; // This file is a client component, allowing it to use hooks like useState, useEffect, etc.

import { Posts } from "@/components/Posts";
import { PostSearch } from "@/components/PostSearch";
import { getAllPosts } from "@/services/getPosts";
import { Metadata } from "next";
import Link from "next/link";
import { useEffect, useState } from "react";

interface Props {}

export default function Posts2({}: Props) {
  type Post = {
    id: number;
    title: string;
    body: string;
  };

  const [posts, setPosts] = useState<Post[]>([]); // State to hold the posts data
  const [error, setError] = useState<string | null>(null); // State to hold any error message
  const [loading, setLoading] = useState<boolean>(true); // State to manage loading state
  const [updatedPosts, setUpdatedPosts] = useState<boolean>(false); //change useEffect to re-fetch posts when this state changes

  // useEffect(() => {
  //   getAllPosts().then((data) => {
  //     setPosts(data); // Set the posts data in state
  //     setLoading(false); // Set loading to false after data is fetched
  //   });
  // }, []); // Empty dependency array means this effect runs once when the component mounts

  //the same code as above
  useEffect(() => {
    getAllPosts()
      .then(setPosts)
      .finally(() => {
        setUpdatedPosts(false); // Reset updatedPosts to false after fetching
        setLoading(false); // Set loading to false after data is fetched
      });
  }, [updatedPosts]); // Adding posts as a dependency to re-fetch if posts change

  return (
    <div>
      <h1 className="posts-header">Posts2</h1>
      <PostSearch
        posts={posts}
        setPosts={setPosts}
        setUpdatedPosts={setUpdatedPosts}
      />
      {posts && posts.length > 0 ? (
        <Posts posts={posts} />
      ) : loading ? (
        <p>Loading...</p>
      ) : error ? (
        <p>Error: {error}</p>
      ) : (
        "No posts found."
      )}
    </div>
  );
}
