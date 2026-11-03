# What is json-server?

It creates local fake REST API

json-server is a simple and powerful Node.js package that allows you to create a full fake REST API quickly and easily by using a simple JSON file as your database.

# How does it work?

- You create a JSON file (e.g., db.json) that contains the data you want to serve.
- You run json-server pointing to that JSON file.
- It automatically generates RESTful routes (GET, POST, PUT, PATCH, DELETE) based on the data in that JSON file.
- You can then interact with this fake API just like a real backend.

# Why use json-server?

1. Quick prototyping and development
   When building front-end applications, you often need an API to work with. Instead of waiting for backend development, you can mock a full API instantly with json-server.
2. No backend setup required
   No need to write server-side code or set up databases. Just create a JSON file and get a REST API.
3. Works for testing and demos
   Perfect for demos, tutorials, and testing API interactions without affecting production data.
4. Supports full CRUD operations
   You can create, read, update, and delete resources using HTTP methods, just like a real REST API.

# Work with json-server

1.

```JS
npm install json-server
```

2. Make a db.json file in the root of your project.
   Example:

```JS
//This acts like your database.

{
  "posts": [
    { "id": 1, "title": "Hello World", "author": "Alice" },
    { "id": 2, "title": "Next.js with JSON Server", "author": "Bob" }
  ],
  "comments": [
    { "id": 1, "body": "Great post!", "postId": 1 }
  ]
}

```

3. Add a Script in package.json

```JS
//In package.json add a script to run json-server:
//This will start the fake API on http://localhost:3001

"scripts": {
  "dev": "next dev",
  "json-server": "json-server --watch db.json --port 3001"
}

//json-server --watch db.json  //<-- will assign any port
//npm run json-server  <-- will start the local server as well

//--watch → Watches for changes in db.json.
//--port 3001 → API will run on
//http://localhost:3001/posts
//http://localhost:3001/comments
```

4. Run the Server

Running Both Next.js & JSON Server Together
In two terminals:

Terminal 1: npm run dev → starts Next.js

Terminal 2: npm run json-server → starts JSON server

5. Fetch Data in Next.js (TypeScript Example)

get your posts in the page.tsx

```JS
"use client"; // if you’re using the app router

import { useEffect, useState } from "react";

interface Post {
  id: number;
  title: string;
  author: string;
}

export default function HomePage() {
  const [posts, setPosts] = useState<Post[]>([]);

  useEffect(() => {
    fetch("http://localhost:3001/posts")
      .then((res) => res.json())
      .then((data: Post[]) => setPosts(data));
  }, []);

  return (
    <div>
      <h1>Posts from JSON Server</h1>
      <ul>
        {posts.map((post) => (
          <li key={post.id}>
            {post.title} — {post.author}
          </li>
        ))}
      </ul>
    </div>
  );
}


```

6. Or you can use API in Next.js

```JS
//Example (pages/api/posts.ts):

import type { NextApiRequest, NextApiResponse } from "next";

export default async function handler(
  req: NextApiRequest,
  res: NextApiResponse
) {
  const response = await fetch("http://localhost:3001/posts");
  const data = await response.json();
  res.status(200).json(data);
}
```

7. Once running, you’ll get endpoints like:

GET all posts: http://localhost:3001/posts
GET single post: http://localhost:3001/posts/1
POST new post: http://localhost:3001/posts
GET /posts — list all posts
GET /posts/1 — get post with id 1
POST /posts — add a new post
PUT /posts/1 — update post with id 1
DELETE /posts/1 — delete post with id 1
PATCH/PUT/DELETE also work.

# Summary

json-server is an easy way to create a mock REST API using just a JSON file. It’s great for front-end developers who want a backend-like service without having to build one.
