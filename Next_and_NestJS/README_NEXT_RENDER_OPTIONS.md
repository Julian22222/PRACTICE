# Next.js Rendetring Options

- CSR - Client Side Rendering,
- RSC - React Server Component,
- SSR - Server-Side Rendering,
- SSG - Static Site Generation
- ISR - Incremental Static Regeneration

# 🎉 Component - on level Client and Server side Rendering

![pic02](https://github.com/Julian22222/PRACTICE/blob/main/Next_and_NestJS/IMG/next2.jpg)

- In Next.JS you can create server side and client side components
- Server side components will be running on the server, all rendering taking place on server then sending to the browser already made Page, only HTML will be sent
- Client side components will be created in the browser, code is not rendered on the server, will receive HTML, CSS and code.
- In your app you can use both components
- page.tsx file can be - as a server side component (by default) or client side component. Also, other componets can be server or client side.
- Next.JS has a Rule when to use each component :
  - If you receive data from the server ( --> fetch('http://jsonplaceholder.type') ), or just showing something on the page - in this occasion use server components!!!
  - by default, components are server side components
  - if you work with user (if you use useState or other web hooks) - in this occasion use client side components!!! (check example in --> app/myhome/page.tsx). Without client side component it will show an error. This component will be proccessed in browser
- If you use client component add -> 'use client'
- async/await can be used only in server side components
- If you want to use async/await on the Client Component - "use clinet" --> You must place it within an event handler or a hook (like useEffect or inside a state update function), not the component's main body.

```JS
//❌ Bad example, can't use async/await and fetch this way
export default async function ProductsPage() {
  const data = await fetchData();
  //some code
}

/////////////
"use client";

export function SignInForm({}: Props) {
  const res = await signIn("credentials", {...}
  //some code
}
```

```JS
//✅ CORRECT Example with event handler
"use client";

import { useState } from 'react';
import { signIn } from 'next-auth/react'; // Assuming you're using next-auth

export function SignInForm({}: Props) {
  const [loading, setLoading] = useState(false);

  const handleSubmit = async (event) => {  // You must wrap async/await in a function, like an event handler
    event.preventDefault();
    setLoading(true);

    const res = await signIn("credentials", { ... });    // ✅ CORRECT: `async/await` is used inside a nested function (the event handler).

    setLoading(false);
    // ... handle response
  };

  return <button onClick={handleSubmit} disabled={loading}>Sign In</button>;
}
```

```JS
//✅ CORRECT Example with useEffect, example from posts/[id]/PostClient.tsx
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

  //some code here

   useEffect(() => {
    const fetchData = async () => {    //use async/await inside useEffect
      const data = await getPost(id);
      setPost(data); // Set the post data in state
    };


   fetchData(); // Fetch post data
  }, []);

    return (
    <div>
      {/* some code here */}
    </div>
  );
}
```

```JS
//✅ CORRECT Example with useEffect, Take fetching outside of "Client Component"
//Example from posts2/page.tsx

"use client";

export default function Posts2({}: Props) {
  //some code here


  useEffect(() => {
    getAllPosts()   //get fetched data, from another file that is imported
      .then(setPosts)
      .finally(() => {
        setUpdatedPosts(false); // Reset updatedPosts to false after fetching
        setLoading(false); // Set loading to false after data is fetched
      });
  }, [updatedPosts]); // Adding posts as a dependency to re-fetch if posts change

return(<div>//some code</div>);
```

```JS
////✅ CORRECT Example use async/await and fetch inside a state update function
//Example:

// app/actions.ts
//This is Server Action file - data fetching happens on the server
'use server';

export async function getData() {
  const getData = fetch("https://.....");

  return getData.json();
}


//Then in your client component:

'use client';

import { useActionState } from 'react';
import { getData } from '../actions';

export default function MyComponent() {
  const [state, formAction] = useActionState(async () => { //use useActionState hook
    const data = await getData();   // calls server
    return data;
  }, null);

  return (
    <form action={formAction}>
      <button type="submit">Get Data</button>
      {state && <p>Data: {state}</p>}
      {/* or the same code
       {state && <p>Data from server: {JSON.stringify(state)}</p>} */}
    </form>
  );
}

//When the form is submitted (by clicking the button), the formAction runs, which executes the Server Action (getData) on the server.
//The result is then returned and updates the state in the client component.
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
✅ This correctly triggers the server action on form submission.
```

🧩 What happens when the user clicks the button:

- The form is submitted → triggers the formAction function.
- formAction runs the async code (on the server).
- The server calls getData() and returns some data.
- The returned data is stored in state.
- The component re-renders with the new state, so the data appears on screen.

🔍 Quick analogy

Think of useActionState like a combo of:

```JS
const [result, setResult] = useState(null);

async function handleSubmit() {
  const res = await fetchDataFromServer();
  setResult(res);
}
```

…but it’s built specifically for Next.js Server Actions, and it automatically handles the server ↔️ client communication for you.

✅ In short

useActionState helps you:

Trigger a server action (via a form).
Automatically store the returned value.
Re-render your component with the new state.

🧠 What useActionState is

useActionState is a React hook provided by Next.js / React Server Actions that helps you:

Call a server action (a 'use server' function) from the client (usually through a form submission).
Keep track of the latest result (called the “state”) from that action.
It’s like combining:

useState (to store data),
and a form action handler (to trigger a server call).

- To avoid errors with async/await and -> use client and useState, fetching data, etc. ,separare your app on small components and then you can add client side or server side server where you need. Also, you can insert client side components into server side components.
- Metadata block can't be used in 'use client' file, Metadata block is server-only. Metadata block must run on the server!!! - to solve this problem we need to split out file into 2 different components - with 'use client' -client component file and server page file. See app/posts/[id]/page.tsx

```JS
//❌ Can't use Metadata in Client Component
//This example will show an error!!!
//page.tsx file

"use client"; // This file is a client component, allowing it to use hooks like useState, useEffect, etc.

export const metadata: Metadata = {
  title: "Post Page",
  description: "Front-end insights, styled like X.com",
};

export default function Post(){
  //some code
  return (<div>Hello World</div>)
}
```

### ✅ Advantages of server side components

- data loading on the server (SSR, ISR, SSG)

- (SSR) - Server-Side Rendering -> one request === 1 responce (usual approach), will cause browser delays if you have 1000 users on your website, server always will ask to increase resources because server power of the cloud will not be enough
- (ISR) - Incremental Static Regeneration -> static (with data update)
- (SSG) - Static Site Generation -> static (without data update)
- access to back-end utils and back-end,
- great security on back end server, uses sensetive data (access token, api keys, etc.)
- make light weight on client side and moving all heavy tasks on server
- great when use heavy dependencies

### ✅ Advantages of Client side components

- use states and effects (hooks, useState, useRef, useEffect, usePathname, etc. )
- use client events (onClick, onSubmit, onChange, onmouseover, etc.)
- use browser API (local storage, etc.)
- use custom brouser hooks
- class components (doesn't work on server side)

```JS
//see --> /app/posts2/page.tsx
//If you use "use client" you can make fetch data only in useEffect or event handler (see above Notes)
//otherwise fetch must be server component

export async function getPost(id: string | null) {
  const result = fetch(`https://jsonplaceholder.typicode.com/posts/${id}`).then(
    (res) => res.json()
  );



"use client";
import { getAllPosts } from "@/services/getPosts";

export default function Posts2({}: Props) {
 const [posts, setPosts] = useState<Post[]>([]);
  const [error, setError] = useState<string | null>(null);
  const [loading, setLoading] = useState<boolean>(true);
  const [updatedPosts, setUpdatedPosts] = useState<boolean>(false);

useEffect(() => {
    getAllPosts()  //getAllPosts fetchin data from another file and imported here
    // getPost(id)
      .then(setPosts)
      .finally(() => {
        setUpdatedPosts(false); // Reset updatedPosts to false after fetching
        setLoading(false); // Set loading to false after data is fetched
      });
  }, [updatedPosts]);

return(<div>....</div>)
}
```

- If you use different libraries like - font editors, google maps, etc, -> then it is better to use Client component
- On Server side don't use SSR but use ISR or SSG, use when security is needed-authorization

# NEXT JS

all component by default are server side components

```JS
'use client'  //if you want to use server Component - "use server" is not mandatory to write
//'use client'  <--this derective, define functions that will perform on browser

import styles from "./Products.module.css"; // Importing CSS module for styling

export function Products() {

  return (
    <div>
      <h1 className={styles.products}>Hello from Products</h1>
    </div>
  );
}
```

# Client and server-side components

![pic01](https://github.com/Julian22222/PRACTICE/blob/main/Next_and_NestJS/IMG/next1.JPG)

You can use Client Component in a Server Component if you need

- Server component → wraps → Client component
- Client component ❌ cannot import server components.
- Client component Runs code in the browser

Server Component is by default

- Can fetch data
- can use async await
- File system access with ‘fs’ library
- can use metadata on the page

Client

- can use hooks(useState, useEffect,etc.), onClick, onChange, onSubmit, etc
- error.tsx file must have "use client"
- if client side component inserted to another client side component (then we don't need to write "use client" on the top of the file that was inserted) --> see posts2 page.tsx file and PostSearch component, both are client side components, but you don't need to write "use client" in the PostSearch component

###### You can use Client Component in a Server Component

- We can insert Client component into Server component,
- But you can't use server component in the Client component

- Server Component fetches the data (e.g., from an API).
- It passes the fetched data as a prop to the Client Component.
- The Client Component receives that data as an initial value and can manage it with useState.

```JS
//server side component
import Items from './components/Items';

async function fetchData() {
  const res = await fetch('https://jsonplaceholder.typicode.com/todos/1');
  return res.json();
}

export default async function HomePage() {
  const data = await fetchData();

  return (
    <div>
      <h1>Welcome</h1>
      <Items data={data} /> {/* Pass fetched data as prop to client component*/}
    </div>
  );
}


//Client component
'use client';

import { useState } from 'react';

export default function Items({ data }: { data: any }) {
  const [item, setItem] = useState(data); // You can now update or manipulate `item` with setItem

  return (
    <div>
      <p>ID: {item.id}</p>
      <p>Title: {item.title}</p>
      {/* Example button to update state */}
      <button onClick={() => setItem({ ...item, title: 'Updated title!' })}>
        Update Title
      </button>
    </div>
  );
}
```

- You can't use server functions on the client side. Server functions (marked with 'use server') are only executed on the server. You cannot directly call or run server functions on the client side because they may contain sensitive logic (like environment variables) that should never be exposed to the browser.
- separate server side and client side components in different files
- In the new Next.js, you can define server actions (functions marked with 'use server') which run only on the server.

### Ability to call 'use server' function (server action) inside a 'use client'

You cannot directly call a 'use server' function (server action) inside a 'use client' component using a normal import and await.

```JS
//❌  Bad example!!!
//file1.ts
'use server'; // This directive makes the function a server action

//This marks getApiToken as a server action — it can only run on the server.
export const getApiToken = () => {
  console.log(process.env.TOKEN);
  return process.env.TOKEN;
};



//file2.ts
How to call server functions from client components?
You cannot call server functions directly on the client. Instead, Next.js provides ways to invoke server actions from client components, typically through form submissions or special server action APIs.
Here’s a simple way to call server actions:

'use client';

//When you import getApiToken in a client component, the function reference itself doesn’t exist in the browser — it’s stripped out during build.
import { getApiToken } from './path/to/serverActions';

export default function MyComponent() {
  async function handleClick() {
    const token = await getApiToken();  // ❌ Not allowed
    console.log('Token from server:', token);
  }

//It will throw a Next.js build error, something like:
//“Server Actions cannot be called directly from the client.”


  return <button onClick={handleClick}>Get Token</button>;
}

//But! This only works if getApiToken is exported as a server action, which must be used inside a server component or via specific mechanisms Next.js provides.
```

🧩 Correct ways to handle this

- Option 1: Use API routes or server functions via fetch

If you want to fetch data from the server when a user clicks a button, use an API route or a route handler.

```JS
//Example:
//can use fetch data with async/await in "use client" in useEffect or with event handler (see notes above)

// app/api/get-token/route.ts    <-- use Handlers API to fetch data then you can await it in client side
export async function GET() {
  const token = process.env.TOKEN;
  return Response.json({ token });
}


//Then in your client component:
'use client';

export default function MyComponent() {
  async function handleClick() {
    const res = await fetch('/api/get-token');
    const data = await res.json();
    console.log('Token from server:', data.token);
  }

  return <button onClick={handleClick}>Get Token</button>;
}


✅ This works — the client makes a network request to the server.
```

- Option 2: Use a Server Action properly (form-based or via useActionState)

Server Actions are meant to be triggered by form submissions, not arbitrary button clicks (at least, not directly).

Using useActionState (form-based trigger)
If you want to call your 'use server' function as a Server Action, it must be bound to a <form>

```JS
//Example:

// app/actions.ts
//This is Server Action file - data fetching happens on the server
'use server';

export async function getData() {
  const getData = fetch("https://.....");

  return getData.json();
}


//Then in your client component:

'use client';

import { useActionState } from 'react';
import { getData } from '../actions';

export default function MyComponent() {
  const [state, formAction] = useActionState(async () => { //use useActionState hook
    const data = await getData();   // calls server
    return data;
  }, null);

  return (
    <form action={formAction}>
      <button type="submit">Get Data</button>
      {state && <p>Data: {state}</p>}
      {/* or the same code
       {state && <p>Data from server: {JSON.stringify(state)}</p>} */}
    </form>
  );
}

//When the form is submitted (by clicking the button), the formAction runs, which executes the Server Action (getData) on the server.
//The result is then returned and updates the state in the client component.
//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

✅ This correctly triggers the server action on form submission.
```

🧩 What happens when the user clicks the button:

- The form is submitted → triggers the formAction function.
- formAction runs the async code (on the server).
- The server calls getData() and returns some data.
- The returned data is stored in state.
- The component re-renders with the new state, so the data appears on screen.

🔍 Quick analogy

Think of useActionState like a combo of:

```JS
const [result, setResult] = useState(null);

async function handleSubmit() {
  const res = await fetchDataFromServer();
  setResult(res);
}
```

…but it’s built specifically for Next.js Server Actions, and it automatically handles the server ↔️ client communication for you.

✅ In short

useActionState helps you:

Trigger a server action (via a form).
Automatically store the returned value.
Re-render your component with the new state.

🧠 What useActionState is

useActionState is a React hook provided by Next.js / React Server Actions that helps you:

Call a server action (a 'use server' function) from the client (usually through a form submission).
Keep track of the latest result (called the “state”) from that action.
It’s like combining:

useState (to store data),
and a form action handler (to trigger a server call).

```JS
//explanation of useActionState hook

const [state, formAction] = useActionState(async () => {
  const data = await getData();
  return data;
}, null);

//useActionState(...)  <--A hook that connects your form to a server action and keeps track of what it returns.
//async () => { const data = await getData(); return data; }  <--This is the function that runs on the server when the form is submitted. It calls getData() (a 'use server' function) and returns the result.
//null  <-- This is the initial state — what state should be before any form submission.
//[state, formAction]  <--  This hook returns two things: - state: the latest data returned by your action (e.g., the server’s response). - formAction: a special function that you attach to your <form>’s action attribute. When the form is submitted, that server action is triggered.


```

```JS
//how to define is it server or client component?

//use this in any component -->
console.log("SERVER check:", typeof window === "undefined");
  //"SERVER check: true" — this means your page is correctly a Server Component

```

- Check examples on api/products/page.tsx file

If you use REST API use fetch("http://.....")

# 👍 Rendering Strategies (different options)

To check and understand what Rendering option has each our Route in your application, we need to type in terminal-->

```JS
npm run build

//Next.js will build your application and, during this process, it will output a summary table in the terminal showing which rendering mode each route uses.This table usually shows:
// - Static Generation (SSG) — pages generated at build time.
// - Server-side Rendering (SSR) — pages rendered on each request.
// - ISR (Incremental Static Regeneration) — static pages with on-demand regeneration.
// - Edge Rendering (if you’re using edge middleware or edge functions).
// - And any client-side rendered parts if relevant.

//after building we will get number of Routes that were generated and next to them we will see - its rendering option. See picture below. This is super useful to confirm that your routes have the expected rendering strategy.

//we have: (signs meaning in the bottom of the picture)
//static rendering
//server rendering

//but can have other rendering options if any of our Routes will have other rendering option in our application
```

![pic03](https://github.com/Julian22222/PRACTICE/blob/main/Next_and_NestJS/IMG/next3.jpg)

- CSR - ( Client side rendering )
  -Tipical for SPA applications, Rendering is occuring on Client side, bad for SEO optimization

- SSR - ( Server Side Rendering )
  -Rendering is occuring on the server and send already build page to client, also send separate JS to the client which will build in to HTML page - (hydration). Good for SEO. This approach is deprecated (is used only when we don't use app(routing) main folder)

- RSC - ( React Server Components )
  -New approach, when we have app main directory with all our routing. Rendering is occuring on the server and send already build page to client. It uses striming process (giving data by pieces) without hydration.Good for SEO. Striming HTML static in first request and further navigation.

- SSG - ( Static Site Generation )
  -HTML rendering takes place on the server side during build (npm run build) and send static HTML to the client. During runtime no excess/extra load on the server side. Server keeps static data and send it to client side when needed. No hydration. To use SSG option you need to add it manually by using getStaticParams (in app router api), by default Next.js use SSR and RSC or CSR
  If you want to use dynamic data (when you add, edit or delite something in your app it will not show up, to see changes you need to - npm run build, again)

- ISR - ( Incremental Static Regeneration )
  -Allows to rerender static page by timer trigger or event trigger. It contains combination of SSG and SSR/RSC approaches.

  Revalidate uses to rerender the page using one of the triggers

[--> Revalidating Data notes <--](https://nextjs.org/docs/14/app/building-your-application/data-fetching/fetching-caching-and-revalidating)

```JS
//For example if we have this Route--> posts/[id]/page.tsx
//we can make SSG (Static Site generation) from SSR (Server side rendering), if articles or post,etc. not changing, don't want to update

//This is Static Site Generation (SSG) with Incremental Static Regeneration (ISR).

export async function generateStaticParams(){
  const posts: any[] = await getAllPosts();  //getting posts from the server by fetch in different file

  return posts.map(post => ({
    slug: post.id.toString();  //slug <-- is unchangable-always use word-slug, if -> post.id is a number type ->> toString() - must be a string as in URL, id must much with dynamic folder -> [id]
  }))
}
```

```JS
//full example
//This is Static Site Generation (SSG) with Incremental Static Regeneration (ISR).

import { getAllPosts, getPostById } from "@/services/getPosts";  //get function from other file
import { Metadata } from "next";
import { revalidatePath } from "next/cache";
import Link from "next/link";
import { redirect } from "next/navigation";

type Props = {
  params: {
    id: string;
  };
};

//// generateStaticParams() - generating static paths at build time. It tells Next.js to pre-render pages for these paths (static generation).
export async function generateStaticParams() {
  const posts: any[] = await getAllPosts();

  return posts.map((post) => ({
    slug: post.id.toString(),
  }));
}

export async function generateMetadata({ //async func.,which runs at build time or on-demand during regeneration.
  params: { id },
}: Props): Promise<Metadata> {
  const post = await getPostById(id);

  return {
    title: post.title,
  };
}

async function removePost(id: string) {
  "use server";
  await fetch(`http://localhost:3300/posts/${id}`, {
    method: "DELETE",
    headers: {
      "Content-Type": "application/json",
    },
  });

  //This triggers a revalidation of the cached static page, which aligns with ISR — after a mutation (deletion), it refreshes the static content.
  revalidatePath("/blog");
  redirect("/blog");
}


//async server component fetching post data (getPostById(id)) — this fetch happens at build time for each statically generated page.
export default async function Post({ params: { id } }: Props) {
  const post = await getPostById(id);

  return (
    <>
      <h1>{post.title}</h1>
      <p>{post.body}</p>

      <form action={removePost.bind(null, id)}>
        <input type="submit" value="delete post" />
      </form>

      <Link href={`/blog/${id}/edit`}>Edit</Link>
    </>
  );
}


//from /services/getPosts file
// export const getAllPosts = async () => {
//   const response = await fetch("http://localhost:3300/posts");

//   if (!response.ok) throw new Error("Unable to fetch posts.");

//   return response.json();
// };
```

Summary

- You use generateStaticParams() to generate static pages for each post at build time → SSG.
- You fetch data in async functions that run at build time (or regeneration) → static props.
- You manually trigger revalidation (revalidatePath) → ISR (on-demand regeneration of static pages).
- No getServerSideProps or similar that forces rendering on every request.

# 👀 Data loading using different options (SSR, ISR, SSG)

```JS
//SSG option, (Server-Side Generation) data loading.

import type {Metadata} from 'next'
import {Products} from './Products'

export const metadata: Metadata = {
  title: 'Products'
}

const fetchData = async()=>{  //async request to the server
  const response = await fetch('https://api.example.com/products') //will keep the post in cache, by default is --> cache: 'no-store'
  const data = await response.json()
  return data
}

export default async function Page(){
  const data = await fetchData()

  if(!data){ //can make some checks, on a server component
    //do some logic
    redirect('/404')
  }

  return <Products />
}
```

```JS
How to control caching behavior?
You can pass the cache option to fetch:

- cache: 'no-store' — disables caching; fetches fresh data every time (like SSR behavior).
- cache: 'force-cache' — caches the response indefinitely (default in server components).
- next: { revalidate: number } — set a time in seconds for how often the cache should be invalidated (ISR TTL).

////////////////

const response = await fetch(http://localhost:3300/posts, {
  cache: "no-store", // disables caching, always fetch fresh data
});
```

[ --> Route Segment Config <--](https://nextjs.org/docs/app/api-reference/file-conventions/route-segment-config)

```JS
//to optimize your code - use static options (ISR, SSG)

import type {Metadata} from 'next'
import {Products} from './Products'

export const metadata: Metadata = {
  title: 'Products'
}

const fetchData = async()=>{  //async request to the server
  const response = await fetch('https://api.example.com/products',{
    cache: 'force-cache'  //for static only, SSG option - data will be loaded once only when building application(npm run build)

    // cache: 'no-cache'  <-- this willl be SSR option, can be used with revalidate. depends on HTTP caching headers, not Next.js’s cache

    //cache: 'no-store' <-- SSR, no cache, Always fetches fresh data from the origin server. This is true SSR — runs on every request, no caching
  })
  const data = await response.json()
  return data
}

export default async function Page(){
  const data = await fetchData()

  if(!data){ //can make some checks, on a server component
    //do some logic
    redirect('/404')
  }

  return <Products />
}
```

##### ISR and revalidate

- It is has Time-based trigger
- Granularity -> Per-page or per-fetch request.

Use this option: when Data changes on a predictable schedule

```JS
//ISR option
// revalidate option is Good when the data changes predictably (e.g., every few minutes). Based on time

import type {Metadata} from 'next'
import {Products} from './Products'

export const metadata: Metadata = {
  title: 'Products'
}

const fetchData = async()=>{  //async request to the server
  const response = await fetch('https://api.example.com/products',{
    cache: 'force-cache',
    next:{
      revalidate: 60 //will update data every 60sec( if you change item name, price, etc)
    }

  })
  const data = await response.json()
  return data
}

export default async function Page(){
  const products = await fetchData()

  if(!products){ //can make some checks, on a server component
    //do some logic
    redirect('/404')
  }

  return (
  <>
    <h1>Product List</h1>
    <Products products={products }/>  //passing products to client component
  </>
  )
}
```

```JS
//can use this code in one of this levels  --> layout.tsx | page.tsx | route.ts
//
export const revalidate = 60;

export default async function Blog(){
  const response = await fetch('https://api.example.com/products');
}

//it is the same as
const response = await fetch('https://api.example.com/products',{
    cache: 'force-cache', //this cache: force-cache' is defined by default, not needed to write this line.
    next:{
      revalidate: 60 //will update data every 60 sec( if you change item name, price, etc)
    }
})

//////////another option to write ISR and revalidate

// Revalidate every 60 seconds
export default async function Page() {
  const res = await fetch('https://api.example.com/posts', {
    //No need cache: 'force-cache', because it is defined by default
    next: { revalidate: 60 } // 60 seconds
  });
  const data = await res.json();
  return <div>{data.title}</div>;
}
```

##### ISR and tags

- tags are cache keys (labels) that you can attach to a fetch request or page.
- They let you manually invalidate cached data on demand (not just based on time).
- tags give you fine-grained control over when and what to invalidate data. (via revalidateTag)
- Tags Granularity -> Groups of requests/pages by a shared tag can be updated manually, and Developer-controlled (triggered by code).
- Ideal for:
  - Admin dashboards (e.g., after publishing a new blog post, you call revalidateTag('posts')).
  - E-commerce sites (invalidate product data after a price update).

How it works:

1. Tag your fetch calls or pages:

```JS
const res = await fetch('https://api.example.com/posts', {
  next: { tags: ['posts'] }
});

```

2. Later, invalidate the tag:

```JS
import { revalidateTag } from 'next/cache'

export async function POST() {
  await revalidateTag('posts');
  return Response.json({ revalidated: true });
}
//This clears the cache for all data/pages that used the tag posts
```

```JS
//If you pass some params -> to child component, child component can be server or client side component, to get id from URL for example

//Another example with tags

//1. Tag the Product Fetch
//In app/products/[id]/page.tsx:
import React from 'react';

export default async function ProductPage({ params }: { params: { id: string } }) {
  const res = await fetch(`https://api.example.com/products/${params.id}`, {
    // Tag cache with the product ID
    next: { tags: [`product-${params.id}`] }
  });

  const product = await res.json();

  return (
    <div>
      <h1>{product.name}</h1>
      <p>${product.price}</p>
      <p>{product.stock} in stock</p>
    </div>
  );
}
///Each product page is cached with a unique tag like product-123



//2. Revalidate a Tag When Data Changes
//In app/api/admin/update-product/route.ts:

import { revalidateTag } from 'next/cache';
import { NextResponse } from 'next/server';

export async function POST(req: Request) {
  const body = await req.json();
  const { id, price, stock } = body;

  // ✅ Update the product in your database
  await updateProductInDB(id, { price, stock });

  // ✅ Immediately invalidate the cache for this product
  await revalidateTag(`product-${id}`);

  return NextResponse.json({ revalidated: true });
}

//As soon as an admin updates the product in the dashboard:
//revalidateTag('product-123') clears the cache for that product.
//Next request to /products/123 fetches fresh data from the API.


//3. Optional: Add a Fallback Timer
//You can also combine tags with a revalidate fallback:
next: { revalidate: 3600, tags: [`product-${params.id}`] }

//Product data will refresh:
//Immediately when an admin calls revalidateTag.
//Or automatically every hour if no manual update occurs.

```

You can combine 2 methods.

# use revalidatePath() method to update data from database, if you posted, edited or delited something

```JS
//see --> blog/[id]/page.tsx

revalidatePath("/blog"); // Revalidating the /posts path to reflect the deleted post
redirect("/blog"); // Redirecting to the /posts page after deletion

//revalidatePath and redirect are used in in Next.js Server Actions or Route Handlers when you perform a mutation (like creating, updating, or deleting data) and want to control how the UI updates afterward.
//Use revalidatePath(path) when you have changed data and you need to ensure the Next.js cache for a specific page is updated, so the next visitor (or the current user) sees the new data
//This is essential after any server action that changes data that is displayed on another page
//very common to use both together in a Server Action
// Use redirect(url) when you want to navigate the user to a different page immediately after a server action is completed. This is useful for improving user experience by taking them to a relevant page after an operation, such as going back to the list view after deleting an item.
//Revalidate: Clear the cache for the page(s) that display the changed data.
//// Redirect: Navigate the user to a different page after the action is complete.
```

# ISR tags vs. revalidatePath()

- next: { tags: [...] } + revalidateTag() → ✅ Best for updating data
  (modern, granular ISR)

- revalidatePath('/path') → 🧱 Use when entire route must re-render

- You cannot use revalidatePath() just to update a single data fetch — it refreshes the whole page.

```JS
//ISR -  incremental static regeneration

const res = await fetch('https://api.example.com/posts', {
  next: { tags: ['posts'] }
});

// - You are opting in to ISR (Incremental Static Regeneration) behavior.
// - The next: { tags: ['posts'] } option associates that cached fetch with a cache tag named "posts".
//- Later, you can trigger revalidation (refresh) for that tag — and Next.js will re-fetch the data and update the static cache.
```

# ⚙️ revalidateTag('posts') vs revalidatePath('/some-page')

1. revalidateTag('posts')

- Use this when you want to invalidate data associated with a certain tag.
- Typically used after a data mutation (e.g., creating or updating a post).

```JS
//Example:
import { revalidateTag } from 'next/cache';

export async function POST(req: Request) {
  const newPost = await createPostInDB(await req.json());

  // Invalidate all fetches that used tag 'posts'
  revalidateTag('posts');

  return Response.json({ success: true });
}

// ✅ Effect:
//Next.js will re-fetch the API call that had next: { tags: ['posts'] } the next time it’s requested.
```

2. revalidatePath('/blog')

- Use this when you want to rebuild a static route or page (ISR for a specific path).
- This revalidates the entire route, not just a specific data fetch.

```JS
//Example:
import { revalidatePath } from 'next/cache';

export async function POST(req: Request) {
  await updateBlogPostInDB(await req.json());

  // Rebuild /blog page statically
  revalidatePath('/blog');
}

// ✅ Effect:
//Next.js re-renders the /blog route at build-time (server-side) using fresh data.
```

🧩 Difference Summary

```JS
Feature           |      next: { tags: [...] }                 |     revalidatePath()

Scope             |   Data-level (fetches with specific tags)  |   Route-level (entire page or layout)
Usage             |   Associate fetches with cache tags        |   Force a specific page re-render
Trigger           |   Call revalidateTag('tagName')            |   Call revalidatePath('/some-path')
Granularity       |   Fine-grained (specific data)             |   Coarse-grained (whole path)
Common use case   |   When data changes (e.g., new post)       |   When page content structure changes
```

🔁 Which Is Better for Updating Data?

✅ Most popular / recommended pattern (App Router):

- Use fetch(..., { next: { tags: [...] } }) to cache API responses.
- Then, use revalidateTag('tag') when you mutate the data.

This is more granular, scalable, and faster than revalidatePath, since it doesn’t rebuild entire pages — only the data source cache is refreshed.

```JS
//Example of modern best practice:

// Fetch data (in Server Component)
const res = await fetch('https://api.example.com/posts', {
  next: { tags: ['posts'] }
});

 // Mutate data (in Server Action)
'use server';

import { revalidateTag } from 'next/cache';

export async function createPost(formData: FormData) {
  await createPostInDB(formData);
  revalidateTag('posts');
}
```

- next: { tags: [...] } + revalidateTag() → ✅ Best for updating data
  (modern, granular ISR)
- revalidatePath('/path') → 🧱 Use when entire route must re-render
- You cannot use revalidatePath() just to update a single data fetch — it refreshes the whole page.

# Example of next: { tags: [...] } and revalidateTag()

```JS
//Directory structure:

app/
  -page.tsx
  -actions.ts

lib/
  -api.ts
```

1. lib/api.ts — Fetching Data with a Tag

We fetch blog posts from an external API (or your DB).
We assign the cache tag 'posts' to this fetch.

```JS
// lib/api.ts
export async function getPosts() {

  const res = await fetch('https://api.example.com/posts', {
    // ISR: associate this fetch with the 'posts' tag
    next: { tags: ['posts'] },
  });

  if (!res.ok) {
    throw new Error('Failed to fetch posts');
  }

  return res.json();
}

// 🧠 Now any cached response from this fetch is tied to the tag 'posts'.
```

2. app/page.tsx — Displaying Posts (Server Component)

We use our getPosts() function to show the posts list.

```JS
// app/page.tsx
import { getPosts } from '@/lib/api';
import { createPost } from './actions';

export default async function HomePage() {
  const posts = await getPosts();

  return (
    <main>
      <h1>Blog Posts</h1>

      <form action={createPost}>
        <input type="text" name="title" placeholder="New post title" required />
        <button type="submit">Add Post</button>
      </form>

      <ul>
        {posts.map((post: any) => (
          <li key={post.id}>{post.title}</li>
        ))}
      </ul>
    </main>
  );
}
```

3. app/actions.ts — Mutating Data + Revalidating Tag

We define a Server Action that:

    1.Creates a new post.
    2.Calls revalidateTag('posts') to refresh the data cache.

```JS
// app/actions.ts
'use server';

import { revalidateTag } from 'next/cache';

export async function createPost(formData: FormData) {
  const title = formData.get('title') as string;

  // Imagine this creates a post in your DB or API
  await fetch('https://api.example.com/posts', {
    method: 'POST',
    headers: { 'Content-Type': 'application/json' },
    body: JSON.stringify({ title }),
  });

  // Invalidate all fetches tagged with 'posts'
  revalidateTag('posts');
}
```

🧠 How It Works

1. On first load, getPosts() runs and caches the response, tagged as 'posts'.
2. When a new post is created via the form:

- The server action createPost() runs.
- It creates the new post in your backend.
- Then revalidateTag('posts') tells Next.js:
  👉 “Next time someone calls a fetch with { tags: ['posts'] }, refetch it fresh.”

3. The next request to your home page triggers a new fetch to the API (fresh data).

✅ Benefits

- No full page rebuilds (like revalidatePath()).
- Efficient and scalable — you can revalidate only the data that changed.
- Works seamlessly with Server Actions and Server Components.
- Perfect for dashboards, blogs, feeds, or lists.
