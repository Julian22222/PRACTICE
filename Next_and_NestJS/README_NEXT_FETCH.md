# Fetch data and assign to variables or to useState

⭐ If we use Context - to have global scope to some useStates -> to use Context in some certain component - we need make sure that component where we use Context is "use client"

✨ In most cases in Next.js you should:

- fetch data on the server, then ➜ pass the result to a client component
- page.tsx (server side component) <-- fetch the data and then pass it to another component(client component) -> to use Hooks or client interactions - onClick, onChange, onSubmit, etc.

🔥 Because server fetching is:

- faster (runs on the server, no waterfall)
- secure (no exposing secrets)
- SEO-friendly
- cached automatically

If you do fetching on the client side component, it loses all these benefits.

# ✔️ Recommended Project Structure

```JS
-app/
  └── layout.tsx
  └── page.tsx  //<--useally server component
  └── other.tsx  //<--if needed it can be "use client" component that is inserted to page.tsx

-components/
    └── Item.tsx            ← Server Component (fetching data)
    └── ItemClient.tsx      ← Client Component (useEffect, client interactions - onClick, onChange, onSubmit, etc.)

-services/
    └── fetchItem.ts        ← Helper function for server fetch
```

✔️ Pattern to Follow

1. Server Component (fetches data)

```JS
// app/components/Item.tsx  (Server Component)
import ItemClient from "./ItemClient";
import { getItem } from "@/services/fetchItem";

export default async function Item({ id }: { id: string }) {
  const item = await getItem(id); // Server data fetching
  return <ItemClient item={item} />;
}
```

```JS
//example from my-app-next/src/app/posts/page.tsx

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


export default async function page() {
  const data = await fechData(); // Fetching data from the external API

  return (
    <div>
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
```

2. Client Component (if needs useEffect)

```JS
// app/components/ItemClient.tsx
"use client";

import { useEffect } from "react";

export default function ItemClient({ item }) {
  useEffect(() => {
    console.log("Client-only effect, item:", item);
  }, []);

  return <div>{item.name}</div>;
}
```

```JS
//example from my-app-next/src/app/posts/[id]/PostClient.tsx

"use client";

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

  useEffect(() => {
    const fetchData = async () => {
      const data = await getPost(id);    //use asyn/await and fetch inside useEffect!!!
      setPost(data); // Set the post data in state
    };

    if (id) {
      fetchData(); // Fetch post data only if id is available
    }
  }, [id]);


  return (
    <div>
      <Post_element id={id} post={post} />
    </div>
  );
}
```

```JS
//example from my-app-next/src/app/posts2/page.tsx

"use client";
import { Posts } from "@/components/Posts";
import { PostSearch } from "@/components/PostSearch";
import { getAllPosts } from "@/services/getPosts";    //fetch from another file

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
      <PostSearch                    //passing posts data
        posts={posts}
        setPosts={setPosts}
        setUpdatedPosts={setUpdatedPosts}
      />
      {posts && posts.length > 0 ? (
        <Posts posts={posts} />     // //passing posts data
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



//another file
//from --> my-app-next/src/services/getPosts.ts
export const getAllPosts = async () => {
  const res = await fetch(
    "https://jsonplaceholder.typicode.com/posts?_limit=10"
  );

  if (!res.ok) {
    throw new Error("Unable to fetch posts.");
  }

  return res.json();
};

```

3. Server fetching utils

```JS
// services/fetchItem.ts

export async function getItem(id: string) {
  const res = await fetch(`https://api.example.com/item/${id}`, {
    cache: "no-store",
  });

  return res.json();
}
```

4. Using in a page

```JS
// app/page.tsx

import Item from "./components/Item";

export default function Page() {
  return <Item id="123" />;
}
```

# Next.js allow to put fetch in the client component

✔️ When to put fetching in the Client Component?

Only when:

- You need user authentication tokens in the browser
- The fetch must happen after user interaction
- You need real-time polling inside useEffect
- Otherwise, server fetching is always better.

✔️ When this version (fetch inside useEffect) is correct

Using this:

```JS
"use client"

useEffect(() => {
  fetchAllUsers().then(setAllUsers);
}, []);
```

is correct ONLY IF:

🔸 The data must be fetched on the client

Examples:

- You use localStorage, sessionStorage, or browser-only state.
- You need user tokens that only exist on the client.
- You want real-time updates after page load.
- You’re building a login form that validates credentials on the client (like your case).
- If useGlobal() stores global client state in React context →
  ➡️ Then fetching inside useEffect is fine and expected.

So your login page is not wrong.

# Examples with server side component and then passing data to "use client"

Clear, modern way to structure a Next.js (App Router) project when:

- You want server-side data fetching (recommended for performance/SEO).
- You also have a component that needs useEffect (client-only logic).

```JS
//Example 1
✅ //server side

export default async function page({}: Props) {

  const data = await fetch("http://localhost:3001/myposts");  //fetch data and map
  const myposts = await data.json();

  return (
    <>
      <h1>Local Server Page</h1>

      <ul>
        {myposts.map((post: Mypost) => (
          <li key={post.id}>
            <h2>{post.title}</h2>
          </li>
        ))}
      </ul>
    </>
  );
}
```

```JS
//Example 2
✅ //Server side

async function fetchAllUsers() {
  const res = await fetch("http://localhost:3005/users"); //fetch the data and pass it to another component --> "use client"
  return res.json();
}

export default async function LoginPage() {
  const users = await fetchAllUsers();
  // console.log("Fetched All Users from useEffect:", users);

  return (
    <div>

      {/* passing fetched data to "use client" component -> there we can use useState to assign this data */}
      <LoginForm users={users} />
    </div>);
}
```

# Example with "use client" and useEffect + fetch(data)

```JS
👀 //you can fetch data inside useEffect in "use clieent" component but it is not good practice
//✅ This example works, but
//❌ It is not the recommended way in Next.js App Router unless you must fetch on the client.

//Example 1
"use client";
import Header from "@/src/components/Header";
import Link from "next/link";
import React, { useEffect } from "react";
import "./login.css";
import { User } from "../../shared/types/user.interface";
import { redirect } from "next/navigation"; // Importing redirect function for navigation
import { useRouter } from "next/navigation";
import { useGlobal } from "../Context"; //IMPORT GLOBAL CONTEXT, Global UseState

async function fetchAllUsers() {
  const res = await fetch("http://localhost:3005/users");
  return res.json();
}

export default function LoginPage() {
  const { allUsers, setAllUsers, activeUser, setActiveUser } = useGlobal();  //get useState variables from Context

  const router = useRouter();

  const [formInput, setFormInput] = React.useState({ email: "", password: "" });
  const [loginError, setLoginError] = React.useState(false);

  useEffect(() => {
    const fetchData = async () => {
      try {
        const users = await fetchAllUsers();
        setAllUsers(users);
      } catch (error) {
        console.error("Error fetching users:", error);
      }
    };

    fetchData();
    //the same code-->
    // fetchAllUsers().then((data) => {
    //   console.log("Fetched users:", data);
    //   setAllUsers(data);
    // });
    // console.log("Active allUsers from UseEffect :", allUsers);
  }, []);

  const handleSubmit = (e: React.FormEvent) => {
    e.preventDefault();

    allUsers.map((user) => {
      if (
        user.email === formInput.email &&
        user.password === formInput.password
      ) {
        setActiveUser(user);
        router.push("/user-page");
      } else {
        console.log("User not found");
        setLoginError(true);
      }
    });
  };

  ////////////////////////////////////////////////

  return (
    <div
      style={{
        fontFamily: "Arial, sans-serif",
        backgroundColor: "#f8f8f8",
        minHeight: "100vh",
      }}
    >
      {/* Header */}
      <Header />

      {/* Main Section */}
      <main className="main-login-container">
        <div className="login-box">
          {/* Title */}
          <h2 className="login-title">Log on to your account</h2>

          {/* Username */}
          <form onSubmit={handleSubmit}>
            <label style={labelStyle}>User ID</label>
            <input
              type="text"
              placeholder="Enter your User ID"
              value={formInput.email}
              name="email"
              onChange={(e) =>
                setFormInput({ ...formInput, email: e.target.value })
              }
            />

            {/* Password */}
            <label style={labelStyle}>Password</label>
            <input
              type="password"
              placeholder="Enter your Password"
              value={formInput.password}
              name="password"
              onChange={(e) =>
                setFormInput({ ...formInput, password: e.target.value })
              }
            />

            {/* Remember Me */}
            <div className="remember-me">
              <input
                type="checkbox"
                id="remember"
                style={{ marginRight: "8px" }}
              />
              <label
                htmlFor="remember"
                style={{ fontSize: "14px", color: "#333" }}
              >
                Remember my User ID
              </label>
            </div>

            <input type="submit" value="Log on" className="login-button" />
          </form>

          {loginError && (
            <div>
              Invalid User ID or Password
            </div>
          )}
    </div>
  );
}
```

```JS
//Example 2

"use client";

import { useEffect, useState } from "react";

type Mypost = { id: number; title: string };

export default function Page() {
  const [myposts, setMyposts] = useState<Mypost[]>([]);

  const fetchPosts = async () => {     ////////////use async in client component but it is not in main function
    const res = await fetch(http://localhost:3001/myposts);
    const data = await res.json();
    setMyposts(data);
  };

  useEffect(() => {
    fetchPosts();
  }, []);

  const handleAdd = async () => {
    await fetch(http://localhost:3001/myposts, {
      method: "POST",
      body: JSON.stringify({ title: "New Post" }),
      headers: { "Content-Type": "application/json" },
    });
    fetchPosts(); // refresh list
  };

  return (
    <>
      <h1>Local Server Page</h1>
      <button onClick={handleAdd}>Add Post</button>
      <ul>
        {myposts.map((post) => (
          <li key={post.id}>{post.title}</li>
        ))}
      </ul>
    </>
  );
}
```

# If you want to use fetch in “use client” à use asunc await function inside useEffect !!!!

```JS
//Example 1

"use client";

async function getPost(id: string | null) {
  const result = fetch(`https://jsonplaceholder.typicode.com/posts/${id}`).then(
    (res) => res.json()
  );
  return result;
}

//some code…

export default function PostClient({ params }: Props) {

  useEffect(() => {
    const fetchData = async () => {
      const data = await getPost(id);
      setPost(data); // Set the post data in state
    };

   if (id) {
      fetchData(); // Fetch post data only if id is available
    }
  }, [id]);

retur(
//some HTML code
)
}
```

```JS
Example 2:

//src/services/getPosts.ts separate file where you fetch data

export const getAllPosts = async () => {
  const res = await fetch(
    https://jsonplaceholder.typicode.com/posts?_limit=10
  );

  if (!res.ok) {
    throw new Error("Unable to fetch posts.");
  }
  return res.json();
};



//src/app/posts2/page.tsx file

"use client";

import { getAllPosts } from "@/services/getPosts";  //import fetched data

export default function Posts2({}: Props) {

//some code

useEffect(() => {
    getAllPosts()   //ß use here imported feched data
      .then(setPosts)
      .finally(() => {
        setUpdatedPosts(false); // Reset updatedPosts to false after fetching
        setLoading(false); // Set loading to false after data is fetched
      });
  }, [updatedPosts]);

return (<>some HTML</>)
}
```
