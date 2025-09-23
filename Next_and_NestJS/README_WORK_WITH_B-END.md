Next.js is one of the best front-end frameworks, it is better practice to don't add back-end to Next.js such as Server Actions and API Handlers. It is not good practice to mix back-end database, PRISMA etc. with front-end in Next.js.
If you will mix everithing in Next.js - Once your application will grow/expand, you will have more problems, and trash bin with all different files in one place.

It is better practice to keep Front-End in Next.js and Back-End in Nest.js

But it is good to know that Next.js have opportunities to work with Back-End in the Next.js without creating Back-End.

# Server Actions

NEXT JS allow to create Api Routes and Server Actions, but most of the time developers separate application to NEXT.JS (Front-END) and NEST.JS (BACK-END) and dont put all together Front-end and Back-end.

usually Server Actions are used with Forms, (from UI), See -> components/NewPostForm.tsx

What are Server Actions in Next.js?

- Allow interact with data without creating back-end api server.
- All queries are taking place in the Next.js server side

Server Actions are special functions that run only on the server when you call them from your React components. They let you do things like:

- Fetch data securely
- Write to a database
- Call APIs without exposing secrets
- Perform server-side logic directly from your UI code

Why use Server Actions?
Normally, when you want to update data or do some backend work, you create API routes and call them with fetch or axios. Server Actions let you skip the extra API layer and call server functions directly from your React components — making your code simpler and cleaner.

How do Server Actions work?

1. You define a function as a server action.
2. Call this function inside your React component.
3. The function runs on the server.
4. The client gets the updated UI or data after the action finishes.

Summary:

- Server Actions are functions that run on the server.
- You can call them from React components without creating separate API routes.
- Useful for secure operations like DB access or secret API calls.
- Makes your app code simpler and cleaner.

Real Example:

1.  npm install mysql2
2.  Define the Server Action to insert a user into the database

```JS
// app/actions.js
'use server';

import mysql from 'mysql2/promise';

// Create a connection pool (reuse connections)
const pool = mysql.createPool({
  host: process.env.DB_HOST,
  user: process.env.DB_USER,
  password: process.env.DB_PASSWORD,
  database: process.env.DB_NAME,
});


export async function createUser(name, email) {
  //Direct query to Database without back-end server with api
  //Can use ORM approach (Prisma) or other interactions with databases (Firebase, etc)
  const sql = 'INSERT INTO users (name, email) VALUES (?, ?)';
  const [result] = await pool.execute(sql, [name, email]);
  return { id: result.insertId, name, email };
}
```

3. React Client Component calling this Server Action

```JS
'use client';

import { createUser } from '../actions';

export default function UserForm() {
  async function handleSubmit(e) {
    e.preventDefault();
    const form = e.target;
    const name = form.name.value;
    const email = form.email.value;

    try {
      const user = await createUser(name, email);
      alert(`User created with ID: ${user.id}`);
      form.reset();
    } catch (error) {
      alert('Failed to create user: ' + error.message);
    }
  }

  return (
    <form onSubmit={handleSubmit}>
      <input name="name" placeholder="Name" required />
      <input name="email" placeholder="Email" type="email" required />
      <button type="submit">Create User</button>
    </form>
  );
}

// Notes:
// The Server Action createUser connects directly to MySQL using mysql2/promise.
// You don’t create an API route manually.
// The function runs securely on the server, so your database credentials never leak to the client.
// The client calls the function like a normal async function, but under the hood Next.js runs it on the server.


// Important:
// In Next.js, only variables prefixed with NEXT_PUBLIC_ are exposed to the browser.
// Since database credentials should stay secret, use environment variables without NEXT_PUBLIC_ prefix.
// Server Actions run on the server, so they can safely access these env vars.
// The .env.local file is loaded automatically by Next.js during development and build.
```

Simple example 2:

```JS
//Step 1: Server Action with validation and DB logic

// app/actions.js (or inside a server component file)
// app/actions.js
'use server';

const fakeDB = []; // Mock database

export async function createPost({ title, content }) {
  // Simple validation
  if (!title || title.length < 5) {
    throw new Error('Title must be at least 5 characters.');
  }
  if (!content || content.length < 20) {
    throw new Error('Content must be at least 20 characters.');
  }

  // Simulate saving to DB
  const newPost = {
    id: Date.now(),
    title,
    content,
    createdAt: new Date().toISOString(),
  };
  fakeDB.push(newPost);

  return newPost;

}
```

```JS
//Step 2: React Client Component to use the Server Action

'use client';

import { useState } from 'react';
import { createPost } from '../actions';

export default function BlogForm() {
  const [error, setError] = useState('');
  const [success, setSuccess] = useState(null);
  const [loading, setLoading] = useState(false);

  async function handleSubmit(e) {
    e.preventDefault();
    setError('');
    setSuccess(null);
    setLoading(true);

    const formData = new FormData(e.target);
    const title = formData.get('title');
    const content = formData.get('content');

    try {
      const post = await createPost({ title, content });
      setSuccess(post);
      e.target.reset();
    } catch (err) {
      setError(err.message);
    } finally {
      setLoading(false);
    }
  }

  return (
    <div>
      <h2>Create a New Blog Post</h2>
      <form onSubmit={handleSubmit}>
        <div>
          <label>Title:</label><br />
          <input name="title" type="text" />
        </div>
        <div>
          <label>Content:</label><br />
          <textarea name="content" rows="5" />
        </div>
        <button type="submit" disabled={loading}>
          {loading ? 'Saving...' : 'Create Post'}
        </button>
      </form>

      {error && <p style={{ color: 'red' }}>Error: {error}</p>}
      {success && (
        <div style={{ marginTop: '20px', color: 'green' }}>
          <h3>Post created successfully!</h3>
          <p><strong>{success.title}</strong></p>
          <p>{success.content}</p>
          <small>Created at: {success.createdAt}</small>
        </div>
      )}
    </div>
  );
}

//Explanation:
The client calls createPost server action.
Server validates the input and throws errors if invalid.
If valid, server "saves" post to the fake database.
Server returns the new post object.
Client updates UI with success or error messages.
All heavy logic happens on the server, making the client clean and secure.

//Why this is better than API routes?
{/* You write less boilerplate (no need for fetch or API endpoints).
You get automatic server/client boundaries.
You avoid manually handling request/response objects.
Stronger type safety and better developer experience. */}
```

##### If you want Component to behave the same way from different pages

```JS
//components/NewPostForm.tsx
import { blogPosts } from "@/shared/data/blogposts.data";
import { redirect } from "next/navigation";

async function createPost(data: FormData) {
"use server";

 const { title, body } = Object.fromEntries(data);

//make DB request, call an API, or perform any other server-side logic here
  // we can use PRISMA or direct DB queries here- using SQL query, or call an external API

  ///////////////////////////////////////////////////////////////////////////////////////
  //example of making a POST request to an API to create a new post
  //   const response = await fetch("https://jsonplaceholder.typicode.com/posts", {
  //     method: "POST",
  //     headers: {
  //       "Content-Type": "application/json",
  //     },
  //     body: JSON.stringify({
  //       title,
  //       body,
  //       userId: 1, //hardcoded userId, in real app we will get it from the session
  //     }),
  //   });

  //   const post = await response.json(); //getting the newly created post from the response
  ///////////////////////////////////////////////////////////////////////////////////////

   blogPosts.push({
    userId: 1,
    id: blogPosts.length + 1,
    title: title as string,
    body: body as string,
  });

  const post = blogPosts[blogPosts.length - 1]; //getting the newly created post from the response

  redirect(`/blog/${post.id}`); //redirecting user to the posts page after creating a new post
}

//NewPostForm component dont receive any props from parent component -> NewPostForm(){...}
export default function NewPostForm(){
  return (
    // form submission will be handled by the createPost function - which is a Server Action
    <form
      action={createPost} //handling form submission using the createPost - it is server action
      className="form"
      style={{
        border: "1px solid white",
        display: "flex",
        justifyContent: "center",
        flexDirection: "column",
        gap: "10px",
        padding: "10px",
        borderRadius: "5px",
        maxWidth: "400px",
        margin: "0 auto",
      }}
    >
      <input type="text" placeholder="title" required name="title" />
      <textarea placeholder="body" required name="body" />
      <div>
        <input type="submit" value="Add post" />
      </div>
    </form>
  );
}



//blog/page.tsc
export default function page({}: Props) {
  return (
      <div>
        <a>Add new post</a>
       <NewPostForm /> //not passing any props
       .....
      </div>)}
```

# Handlers API

Route Handlers - allow to create endpoints for API requests in Next.js

- /app/api is app directory structure. The file path inside this folder corresponds to the URL path of the API endpoint.

API Routes can have different data return options:

- You only need a database if your API handler needs to persist or fetch data.
- You can create API handlers that return static data or perform operations without any database.

- To create API routes inside /app directory, we create directory /api inside app directory. /api folder can have other directories inside where you create route.ts file.
- If route.ts file is located /app/api/posts , then URL request will be /api/posts
- route.ts file MUST export an object with function and with one of methods: GET, POST, DELETE, PUT, PATCH, etc.
- In one folder can't be route.ts and page.ts files!!!

Next.js is one of the best front-end frameworks, it is better practice to don't add back-end to Next.js. It is not good practice to mix back-end database, PRISMA etc. with front-end in Next.js.
If you will mix everithing in Next.js - Once your application will grow/expand, you will have more problems, and trash bin with all different files in one place.

But Next.js has ability to work with back-end.

- allow to control and adjust server side.
- You create any database and you can work with that Database from Next.Js Framework. You don't need to install express.js, node.js, or other additional frameworks, etc.
- Next.js applicationYou allow to connect to your Database and get needed values from that database. We use --> app/api folder and route.ts file

```JS
//YOU MUST create folder api inside app folder and file name MUST be - route.ts

//IF we have folders location -> app/api/data - the URL to get data will be --> /api/data + request method(GET,POST, PUT, PATCH, DELETE, etc.)

//example, function name MUST have HTTP methods -->
export async function GET(req: Request){....}  //function name - GET, receive "req" with data type- Request

SEE --> app/api/posts/route.ts file
SEE --> app/api/posts/[]/route.ts
SEE --> app/api/movies/route.ts  //work with Database

IF we have folders location -> app/api - the URL to get data will be --> /api + request method(GET,POST, PUT, PATCH, DELETE, etc.)

Next.js offers 2 additional objects that we can import -> NextRequest and NextResponse (the same as Request and Response in global level)
```

- api folder must be inside app folder. api folder outside of app folder will not be read at all.
- The logic is the same as folder app, where you can add additional folders which will effect on final URL address of our api, and instead of page.tsx we use route.ts
- in route.ts you just use URL address with HTTP method and returning the response
- route.ts and page.tsx Must be in different folders!!! route.ts MUST be in api folder!

```JS
//URL to our local data, if your application is running on port 3001
http://localhost:3001/api/data

//this is endpoint that we can use to get data from Next.js

```

If we need to DELETE some object using API Routes, there is 2 options:

```JS
//option 1
//check app/api/data/route.ts file

export async function DELETE(request: Request) {
//   const { searchParams } = new URL(request.url); // Get the search parameters from the request URL

//   //https://localhost:3000/api/posts?q=Manchester
//   const query = searchParams.get("q"); // Get the 'q' parameter from the URL

//the same as code above
  const url = new URL(request.url);
  const id = url.searchParams.get("id");   // Get the 'id' parameter from the URL

if (!id) {
    return new Response("ID is required", { status: 400 });
  }

  const response = await fetch(
    `https://jsonplaceholder.typicode.com/posts/${id}`,
    {
      method: "DELETE",
    }
  );

  if (!response.ok) {
    return new Response("Failed to delete data", { status: response.status });
  }

  return new Response("Data deleted successfully");
  //return NextResponse.json({message: "Data deleted successfully"});
}
```

```JS
//option 2, create folder [id] and create route.ts file
//check app/api/posts/[id]/route.ts file

export async function DELETE(
  request: Request,
  { params }: { params: { id: string } }
) {
  //get id from params to delete specific post

  const id = params.id;

  const response = await fetch(
    `https://jsonplaceholder.typicode.com/posts/${id}`,
    {
      method: "DELETE",
    }
  );

  if (response.ok) {
    return new Response(id, { status: 204 }); // returnn 204 status code for successful deletion with id of deleted post
  } else {
    return new Response("Failed to delete the post", { status: 500 });
  }
}
```

- Can use Next.js helpers --> redirect, headers, cookies

```JS
//If we need to delete the post by its ID and we don't need to return anything but we need to redirect user to another page
import { NextResponse } from 'next/server'
import { headers, cookies } from 'next/headers'  //Next JS helpers, can check cookies and headers not mandatory
import { redirect } from 'next/navigation'  //Next JS helpers

export async function DELETE(
  request: Request,
  { params }: { params: { id: string } }
) {
  //get id from params to delete specific post

  const id = params.id;

  const response = await fetch(
    `https://jsonplaceholder.typicode.com/posts/${id}`,
    {
      method: "DELETE",
    }
  );

  if (response.ok) {
    redirect('/home')  //redirect to home page, when post is deleted
  } else {
    return new Response("Failed to delete the post", { status: 500 });
  }
}
```

# API secret keys

- check app/api/movies/route.ts file
- this file needs API KEY, we can keep secret variables in env.local file in the root of our project
- or we can keep secret variables in .env file, which must present in the root of our project.
- then use as usual - process.env.variableName, to get the value
- then to get movies we need to use URL - https://localhost:3000/api/movies

# API requests

```JS
// GET query params

export async function GET(req: Request) {
  const { searchParams } = new URL(req.url);

  const query = searchParams.get("q");

  // some logic

  return NextResponse.json(currentPosts);
}

/////////////////////////////////////////

// GET body request

export async function POST(req: Request) {
  const body = await req.json();

  console.log(body);

  return NextResponse.json({ message: "done" });
}

//////////////////////////////////////////

// GET URL params

export async function DELETE(
  req: Request,
  { params }: { params: { id: string } }
) {
  const id = params?.id;

  // some logic for delete post by id

  return NextResponse.json({ id });
}

//////////////////////////////////////////////

// Build-in function
import { headers, cookies } from "next/headers";

export async function GET(req: Request) {
  const headersList = headers();
  const cookiesList = cookies();

  const type = headersList.get("Content-Type");
  const Cookie_1 = cookiesList.get("Cookie_1")?.value;

  return NextResponse.json({});
}

//////////////////////////////////////////////

import { redirect } from "next/navigation";

export async function GET(request: Request) {
  redirect("https://nextjs.org/");
}
```

# Work with Database

Example: Next.js API handler with MongoDB

1. Setup Mongoose connection helper (to avoid multiple connections)

Create a file lib/mongodb.js:

```JS
import mongoose from "mongoose";

const MONGODB_URI = process.env.MONGODB_URI;

if (!MONGODB_URI) {
  throw new Error("Please define the MONGODB_URI environment variable");
}

/**
* Global is used here to maintain a cached connection across hot reloads in development.
*/

let cached = global.mongoose;

if (!cached) {
  cached = global.mongoose = { conn: null, promise: null };
}

async function connectToDatabase() {
  if (cached.conn) {
    return cached.conn;
  }

  if (!cached.promise) {
    cached.promise = mongoose.connect(MONGODB_URI).then((mongoose) => {
      return mongoose;
    });
  }

  cached.conn = await cached.promise;
  return cached.conn;
}

export default connectToDatabase;

```

2. Define a simple Mongoose model (e.g., for users)

Create models/User.js:

```JS
import mongoose from "mongoose";

const UserSchema = new mongoose.Schema({
  name: String,
  email: String,
});

export default mongoose.models.User || mongoose.model("User", UserSchema);
```

3. Create API handler that connects to the DB and fetches users

Create /pages/api/users.js:

```JS
import connectToDatabase from "../../lib/mongodb";
import User from "../../models/User";

export default async function handler(req, res) {
  await connectToDatabase();

  if (req.method === "GET") {
    // Fetch all users
    const users = await User.find({});
    res.status(200).json(users);
  } else if (req.method === "POST") {
    // Create new user
    const { name, email } = req.body;

    if (!name || !email) {
      return res.status(400).json({ error: "Missing name or email" });
    }

    const newUser = new User({ name, email });
    await newUser.save();

    res.status(201).json(newUser);

  } else {
    res.setHeader("Allow", ["GET", "POST"]);
    res.status(405).end(`Method ${req.method} Not Allowed`);
  }
}
```

4. Environment variable

Add your MongoDB connection string to .env.local:

```JS
MONGODB_URI=mongodb+srv://username:password@cluster0.mongodb.net/mydb?retryWrites=true&w=majority
```

Summary

GET /api/users — returns all users from MongoDB.
POST /api/users — creates a new user in MongoDB.

# Example: Next.js API handler with PostgreSQL

1. Install the pg package

```JS
npm install pg
```

2. Create a database connection helper

Create lib/postgres.js:

```JS
import { Pool } from "pg";

const pool = new Pool({
  connectionString: process.env.DATABASE_URL, // your PostgreSQL connection string
});

export default pool;
```

3. Create an API handler to query PostgreSQL

Create /pages/api/users.js:

```JS
import pool from "../../lib/postgres";

export default async function handler(req, res) {
  if (req.method === "GET") {
    try {
      const { rows } = await pool.query("SELECT * FROM users");
      res.status(200).json(rows);
    } catch (error) {
      console.error("Error fetching users:", error);
      res.status(500).json({ error: "Internal Server Error" });
    }
  } else if (req.method === "POST") {

    const { name, email } = req.body;

    if (!name || !email) {
      return res.status(400).json({ error: "Missing name or email" });
    }

    try {
      const query =
        "INSERT INTO users (name, email) VALUES ($1, $2) RETURNING *";
      const values = [name, email];

      const { rows } = await pool.query(query, values);

      res.status(201).json(rows[0]);

    } catch (error) {
      console.error("Error creating user:", error);
      res.status(500).json({ error: "Internal Server Error" });
    }
  } else {
    res.setHeader("Allow", ["GET", "POST"]);
    res.status(405).end(`Method ${req.method} Not Allowed`);
  }
}
```

4. Set environment variable

In your .env.local:

```JS
DATABASE_URL=postgresql://username:password@host:port/database
```

5. Example users table schema for PostgreSQL

Run this in your PostgreSQL to create the users table:

```JS

CREATE TABLE users (
  id SERIAL PRIMARY KEY,
  name VARCHAR(100) NOT NULL,
  email VARCHAR(100) NOT NULL UNIQUE
);
```

How it works:

GET /api/users — returns all users from the PostgreSQL database.
POST /api/users — creates a new user with name and email.
