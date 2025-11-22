# Fetching data in Server Actions not in Component function

Generally it is better to interact with your database through Server Actions (or Route Handlers) rather than directly from a component.

```JS
//Example -> src/components/NewPostForm.tsx
// ❌ It is working example, 🔥 but it is better to interact with your database using Server Actions
//❌ Client component + fetch is not recommended

export default function NewPostForm({
  onSuccess,
}: {
  onSuccess: (id?: number) => Promise<void> | void;
}) {

async function createPost(data: FormData) {
"use server";

//getting form data values
const { title, body } = Object.fromEntries(data); //converting form data to an object, and destructuring the object to get title and body values

//example of making a POST request to an API to create a new post
const response = await fetch("https://jsonplaceholder.typicode.com/posts", {
  method: "POST",
  headers: {
    "Content-Type": "application/json",
  },
  body: JSON.stringify({
    title,c
    body,
    userId: 1, //hardcoded userId, in real app we will get it from the session
  }),
});

const post = await response.json(); //getting the newly created post from the response

return (
    // form submission will be handled by the createPost function - which is a Server Action
    <form
      action={createPost} //handling form submission using the createPost - it is server action
    >
      <input type="text" placeholder="title" required name="title" />
      <textarea placeholder="body" required name="body" />
      <div>
        <input type="submit" value="Add post" />
      </div>
    </form>
  );
}
```

### ✅ Why you should use a Server Action to interact with your DB

✔ 1. No secrets or DB logic must ever run in the client

If you do this inside a React component (client-side):

- Anyone can see network calls
- Anyone can hit your API with whatever data they want
- You must secure and validate the API route separately
- You expose your app to more attack surface

⭐⭐⭐ Server actions must – contain a “use server”. You can use server action in separate function or in separate file

##### 🔥 //Good Example 1 - Server Action in separate file

```JS
//app/actions.ts
"use server";  //adding this directive to indicate that this is a server component

export async function createPost(data) {
  // Safe: runs only on server
  await db.query("INSERT ...");
}

//This logic is completely hidden from the user.
//No fetch, no API route, no exposure.
```

##### 🔥 //Good Example 2 - Server action in separate function in the same file

```JS
//See --> /components/NewPostForm.tsx

export default function NewPostForm({
  onSuccess,
}: {
  onSuccess: (id?: number) => Promise<void> | void;
}) {

async function createPost(data: FormData) { //Server Actions in separate function
//we use form data to get the form input values, we receive the "data" from the form submission,

"use server";

const { title, body } = Object.fromEntries(data);

const response = await fetch("https://jsonplaceholder.typicode.com/posts", {
        method: "POST",
        headers: {
          "Content-Type": "application/json",
        },
        body: JSON.stringify({
          title,c
          body,
          userId: 1, //hardcoded userId, in real app we will get it from the session
        }),
      });

      const post = await response.json(); //getting the newly created post from the response

  await onSuccess?.(post.id); //calling the onSuccess function passed as a prop from the parent component, and passing the newly created post id to it
}

  return (
    // form submission will be handled by the createPost function - which is a Server Action
    <form
      action={createPost} //handling form submission using the createPost - it is server action
    >
      <input type="text" placeholder="title" required name="title" />
      <textarea placeholder="body" required name="body" />
      <div>
        <input type="submit" value="Add post" />
      </div>
    </form>
  );
}
```

```JS
//Example 3 -  <Form action="/posts3">
//See -> src/app/posts3/page.tsx

Type Post = {
  id: number;
  title: string;
  body: string;
};

const baseUrl = "https://jsonplaceholder.typicode.com/posts?_limit=10";

export default async function PostPage({
  //will get the search params from the URL
  searchParams,
}: {
  searchParams: Promise<{ [key: string]: string | string[] | undefined }>;
}) {
  // get title parametr value from search params, if not present set it to an empty string
  //if we have ?title=something in the URL, we will get that value here
  //title will be used to filter the posts, if we have title in the search params
  //title comes from Form--> <input type="text" name="title" />
  const title = (await searchParams).title || ""; // Get the title from search params, if parametrs not present set it to an empty string

  console.log("Title from search params:", title);

  const url = title ? `${baseUrl}&title_like=${title}` : baseUrl; //if title is present in the search params, we add title_like query param to the URL to filter the posts by title. If not, we just use the baseUrl
  //title_like is a query param that jsonplaceholder API supports to filter posts by title
  //title_like does a partial match, so if we search for "sunt" it will return all posts that have "sunt" in the title
  //if we want exact match, we can use title=somevalue, but it will return only one post that matches the title exactly
  //title_like is case insensitive
  //title_like is a query param that jsonplaceholder API supports to filter posts by title

  const data: Promise<Post[]> = fetch(
    url //if title is present in the search params, we add title_like query param to the URL to filter the posts by title. If not, we just use the
    // baseUrl
    //filtering posts by title using title_like query param
    //if title is present in the search params, we add title_like query param to the URL to filter the posts by title. If not, we just use the baseUrl to get all posts
  ).then((res) => res.json());

  return (
    <>
      <h1>Here we use new approach with Form tag from Next.js 15</h1>

      {/* action="/posts3" <-- we stay on the same page, we don't do redirect to different page after Form submission*/}
      {/* by submission this Form we make URL ->  https://jsonplaceholder.typicode.com/posts +?title=somevalue*/}
      <Form action="/posts3">
        <input type="text" name="title" placeholder="Enter your name" />
        {/* <button type="submit">Search</button>  //<-- usual button tag*/}
        <SearchButton />
      </Form>

      <PostsComponent data={data} />
    </>
  );
}
```

```JS
//See --> src/app/blog/[id]/page.tsx

async function deletePost(id: string) {
"use server"; // Marking the function to be a server action
// ..some code to interact with Database

}

export default function Post({ params }: { params: { id: string } }) {
  const singlePost = blogPosts?.find((post) => post.id === Number(params.id)); // Find the post with the matching ID from the static data

  return(
<>
<form action={deletePost.bind(null, params.id)}>
  {/* Binding the deletePost function to the form action, bind method allow you to pass correct post id*/}
  {/* null - is passing context in bind method,we don't have any context therefore it is null */}
  {/* params.id - is a string  */}
  <input type="submit" value="Delete Post" />
</form>
}
</>
```

✔ 2. Cleaner and faster — no extra API hop

```JS
//Traditional client flow:

Component → fetch → API Route → DB
```

```JS
//Server Action flow:

Component → Server Action → DB
```

You remove an entire HTTP round-trip.

✔ 3. No need for useEffect or fetch() for mutations

```JS
//Your form can directly submit to the server action:


<form action={createPost}>   //<--use Server Actions
  <input name="title" />
  <input name="body" />
  <button type="submit">Create</button>
</form>

//The server action receives the values on the server and interacts with your database
```

🚦 So when should you not use Server Actions?

Use a normal API route only when:

❗ You must call it from a fully client-side interaction

(e.g., fetching in useEffect, polling, dynamic search-as-you-type)

❗ Your call must be accessible by external clients

(e.g., mobile app, webhook)

Otherwise, Server Actions should be your default choice.

#### API endpoints are exposed — but that’s normal and safe when done correctly

i should not use Server Actions, when I must call it from a fully client-side interaction (e.g., fetching in useEffect, polling, dynamic search-as-you-type)

When you must fetch from client-side code (useEffect, search-as-you-type, polling, pagination), you must expose a public API endpoint because the browser needs something to call.

This is not insecure as long as the API is properly protected.

🛡 How to stay secure when exposing API routes

1. Authenticate the user (required)

Protect the endpoint with NextAuth, Clerk, Auth.js, JWT, or cookies.

```JS
//Example:
export async function POST(req: Request) {
  const session = await auth();
  if (!session) {
    return new Response("Unauthorized", { status: 401 });
  }

  // only authenticated users reach this part
}
```

2. Validate the incoming data

```JS
//Never trust body data: always give certain rules

const { search } = await req.json();
if (typeof search !== "string" || search.length > 100) {
  return new Response("Invalid input", { status: 400 });
}
```

3. Apply authorization rules

```JS
//Example: only allow users to modify their own posts.

if (session.user.id !== post.userId) {
  return new Response("Forbidden", { status: 403 });
}
```

4. Never expose raw SQL or sensitive logic

The API handler is the only place that touches the DB:

```JS
await db.query("SELECT * FROM posts WHERE title LIKE ?", [`%${search}%`]);

//This is safe.
```

🔍 Why Server Actions cannot replace client-side fetch

Server Actions require:

- A form submission, or
- A server component

But when you are doing:

- search-as-you-type (debounced)
- infinite scroll
- dashboard auto-refresh
- polling
- loading data after page load
- user-type → real-time suggestions
- WebSockets-style updates

# Practical List what to use in each scenario in Next.js App Router — Server Actions vs. Server Components vs. API Routes.

✅ 1. Fetching Data (READ)

**A. Static or server-rendered page loading data**

Use **Server Components**
✔ Best performance
✔ Runs only on server
✔ No exposure
✔ No client fetch needed

**Example:** product list, blog posts, user dashboard

```tsx
// Server Component
export default async function Page() {
  const posts = await db.query("SELECT * FROM posts");
  return <PostList posts={posts} />;
}
```

---

**B. Client needs to load data AFTER render**

(useEffect, infinite scroll, search-as-you-type)

Use **API Route (Route Handler)**
❌ Cannot use Server Action (because it does not work from client)
✔ Requires authentication
✔ Must validate inputs

**Example:** live user search, infinite load, filtering on client typing

```tsx
useEffect(() => {
  fetch("/api/search?q=" + query);
}, [query]);
```

---

🚀 **2. Mutations (WRITE: create, update, delete)**

**A. Triggered by a form or a button inside a server component**

Use **Server Actions (BEST)**
✔ Never exposed
✔ No API required
✔ No fetch
✔ More secure
✔ Simpler code

**Example:** add post, update profile, delete item

```tsx
<form action={createPost}>
  <input name="title" />
  <button type="submit">Save</button>
</form>
```

---

**B. Triggered by client-side event (useEffect, polling, dynamic actions)**

Use **API Route**
✔ Browser must be able to call it
✔ Authenticate + validate
❌ Server Actions cannot run here

**Example:**

- auto-save draft every 2 seconds
- upvote button without page refresh
- like/unlike a post
- toast UI triggered from client code

```tsx
await fetch("/api/upvote", { method: "POST", body: ... })
```

---

🔄 **3. Mixed: Read + Write in UI**

### **A. Page loads data (server), but also mutates**

- Load data: **Server Component**
- Mutate: **Server Action**

**Example:** dashboard that loads tasks, and form adds a task.

---

**B. Page loads data dynamically on client and mutates**

Both from client → **API Routes for both read + write**

**Example:** chat app, live search + send message

---

🔐 **4. Authentication-required operations**

Use this rule:

### **If action requires login and doesn’t need client-side fetch → Server Action**

### **If login-required action needs to run from browser JS → API Route**

---

🎯 **5. Real-time or frequent updates**

Server Actions do NOT work → use API Route.

Examples requiring **API Routes**:

- Polling every X seconds
- Realtime notifications
- WebSocket/sse connections
- Search suggestions as user types

---

💾 **6. Heavy database operations**

### If running on initial page load → **Server Component**

### If running on user submission → **Server Action**

### If triggered by client-side JS → **API Route**

---

# 📌 **Ultimate Cheat Sheet (Summary)**

| Scenario                              | Use                                  | Why                                |
| ------------------------------------- | ------------------------------------ | ---------------------------------- |
| Load data on page (SSR)               | **Server Component**                 | Fast, secure, no API               |
| Submit a form (create/update/delete)  | **Server Action**                    | Best for mutations                 |
| Dynamic data loaded from client       | **API Route**                        | Browser must fetch                 |
| Live search / infinite scroll         | **API Route**                        | Needs client fetch                 |
| Auto-save draft                       | **API Route**                        | Periodic client calls              |
| Load + mutate in same route           | **Server Component + Server Action** | Clean split                        |
| External clients need access          | **API Route**                        | Public endpoint                    |
| Sensitive DB writes without client JS | **Server Action**                    | Most secure                        |
| Use button click handled in client    | **API Route**                        | SA cannot be called from client JS |
| External webhook                      | **API Route**                        | Must be reachable                  |

---

# ⭐ Final Rules to Remember

### ✓ **Reads → Server Components (when possible)**

### ✓ **Writes → Server Actions (when possible)**

### ✓ **Client-side fetch → API Routes (always)**

### ✓ **API Routes must be authenticated + validated**

### ✓ **Server Actions never run in client → safe**

# Interact with your Database in Server Actions and API Handlers but without Back-end (with no Nest JS) - adding all in Next.JS

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

#### interactions with Database without Back-End in a Server Action

```JS
//example

export async function createUser(name, email) {
  //Direct query to Database without back-end server with api
  //Can use ORM approach (Prisma) or other interactions with databases (Firebase, etc)

  const sql = 'INSERT INTO users (name, email) VALUES (?, ?)';
  const [result] = await pool.execute(sql, [name, email]);
  return { id: result.insertId, name, email };
}

//Fetch() – is used for Calls an external API or route. For External data from another backend, microservices, or REST APIs
//To interact directly to your own database – use SQL queries



//Example of fetch in Server actions:

"use server";

export async function sendNotification(email: string) {
  await fetch(https://api.mailservice.com/send, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, message: "Welcome!" }),
  });
}
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
