import { useState } from "react";

type Post = {
  id: number;
  title: string;
  body: string;
};

interface Props {
  posts: Post[]; // Props to receive the posts data
  setPosts: React.Dispatch<React.SetStateAction<Post[]>>; // Function to update the posts state
  setUpdatedPosts: React.Dispatch<React.SetStateAction<boolean>>; // Function to trigger re-fetching of posts
}

export function PostSearch({ posts, setPosts, setUpdatedPosts }: Props) {
  const [search, setSearch] = useState<string>(""); // State to hold the search input value
  const [error, setError] = useState<string | null>(null);

  const handleSearch = (e: { preventDefault: () => void }) => {
    e.preventDefault(); // Prevent the default form submission behavior

    const filteredPosts = posts.filter((post) =>
      post.title.toLowerCase().includes(search.toLowerCase())
    );

    console.log(search);

    if (filteredPosts.length === 0) {
      setError("No posts found with that title.");
    } else {
      setError(null);
      setPosts(filteredPosts);
    }
  };

  const getAllPosts = () => {
    setPosts(posts); // Reset the posts to the original list
    setSearch(""); // Clear the search input
    setError(null); // Clear any error message
    setUpdatedPosts(true); // Trigger re-fetching of posts
  };

  return (
    <div>
      <form onSubmit={handleSearch} className="posts-search">
        <input
          type="search"
          value={search}
          onChange={(e) => setSearch(e.target.value)}
          placeholder="Search posts by title"
          className="search-input"
        />
        {search && (
          <button className="x-search" onClick={getAllPosts}>
            X
          </button>
        )}
        <button type="submit" className="search-button">
          Search
        </button>
      </form>
    </div>
  );
}
