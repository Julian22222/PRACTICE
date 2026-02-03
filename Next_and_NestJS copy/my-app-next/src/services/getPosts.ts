export const getAllPosts = async () => {
  const res = await fetch(
    "https://jsonplaceholder.typicode.com/posts?_limit=10"
  );

  if (!res.ok) {
    throw new Error("Unable to fetch posts.");
  }

  return res.json();
};

export const getPostsBySearch = async (search: string) => {
  const res = await fetch(
    `https://jsonplaceholder.typicode.com/posts?q=${search}`
  );

  if (!res.ok) {
    throw new Error("Unable to fetch posts by search.");
  }

  return res.json();
};
