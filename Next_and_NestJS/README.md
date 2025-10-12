# NEXT.JS (Advantages of Next.js)

- It is a Front-End framework for static(client side) and server side rendering of React applications
- Next.JS has ability to add API endpoints, but usually it is not used to don't mix Front-end and Back-end. For Back-end we need to use Nest.JS
- It is made from React JS, It is a cover over React JS
- Next JS allows to render the page on the server side and client side (browser)
- Next JS allows to create big, scalable applications
- Next JS has performance and simplicity

Next.js support of:
-CSR - client side rendering,
-RSC - react server component,
-SSR - server side rendering,
-SSG - static site generation,
-ISR - incremental site generation.

Therefore it is flexible to adjust for any tasks.

- Next JS has build-in Routing and Seo optimization, Server Actions (server logic inside component)
- Next JS saves a lot of time and resources, cashing and optimization(images, styles, fonts, scripts) reduce load on a server, reduce hosts.
- Also, Next JS increasing development and hosting applications
- It has great code separation (=== fast loading of your website)
- Very convenient Routing. Routing use foldersfor Routing. To create a page, we create the folder in app folder and create page.tsx file
- Automatic optimization of images, styles, fonts and scripts
- Optimization of building an application (building is very fast)
- by default next is running on webpack -->

```JS
//package.json file

"scripts":{
  "build": "next build"  //running on webpack by default
}

//Vercel company that is developing Next.js
//made their own bulder -> turbopack
"build": "next build --turbopack"
```

- global styles and google fonts we add in layout.tsx file. Google fonts can be found on their google website

# Start Next JS project

```JS
//download VScode extension for Next JS
Next.js 14/15 and React snippets

sci +Tab //new component with interface

sc + Tab //new component

rafce + Tab //standard React new component

```

```JS
npx create-next-app my-app-name //<-- can use this by default or -->

px create-next-app@13.4 .  //dot in the end means -create app in current folder, next version -13.4

npx create-next-app@latest my-app-name
//install some packages -->
//TypeScript -yes
//ESLint - no ,(if you work in a team -yes)
//App Router -yes (convenient way of Routing)
//Turbopack -yes
//alias - no (don't customize default version) (used to use imports instead of doing this --> import logo from '../../../assets/logo.svg'), it allows to use -> import logo from '@assets/logo.svg'

//use bun + next js
bunx create-next-app@latest my-app-name
//bun dev - start app
//bunx → Bun’s equivalent of npx (used to run packages without installing them globally
//create-next-app@latest → Ensures you get the latest version of Next.js.
```

```JS
//Can create Front-End app manually

npm init -y //create package.josn
npm i next react react-dom //download dependencies

scripts:
"dev": "next dev"
//npm run dev  - start Next JS app

//Then create folder - pages (in the Root folder of the project)
//Inside "pages" folder--> index.js //home page -> localhost:3000
//users.js  --> this component will be available using this URL -> localhost:3000/users
```

# Deployment

Next.JS has it own hosting website --> can be hosted on vercel.com

[ --> youTube <--](https://www.youtube.com/watch?v=Cez-ecgXvNU&list=PLiZoB8JBsdzlgeYHZDJ_orG0vy8JiEhKr&index=16)

- Versel is a Next.js hosting
- Vercel does offer a free tier (called the Hobby plan) where you can host a Next.js application without paying.
- Free tier gives some limitations in storage and functions

Check what you need to do for hosting on different providers.-->

[ --> Deploying Next.JS <-- ](https://nextjs.org/docs/app/getting-started/deploying)

Next.JS can be hosted on GitHub pages (static export), as a Docker container and as a Node.js server (Virtual hosting with your own domen)

- To host NEXT.JS to GitHub pages you need make some changes in the next.config.ts file and there is some limitations, doesn't support some features. use link above to check that.

```JS
//next.config.ts file
import type { NextConfig } from 'next';

const nextConfig: NextConfig = {
  output: 'export',
  basePath: process.env.PAGES_BASE_PATH, //path to correct project folder in GitHub, path after -> https://github.com/Julian22222/everythin in here put in basePath
};

export default nextConfig;
```

```JS
also add to all dynamic componens (such as individual item page)--> export async function generateStaticParams(){
  //need to get an array of all ellements in your Database
}
```

# Structure and Components of the Next JS app (Main locations of different files)

```JS
node_modules // folder where you can find all libraries and dependencies (normal and dev dependencies)
public //static folder, path to these files will be static. Here we keep images, other static files
src //main folder, here we keep all main files. src folder allow us to make good application folder structure, Insded src we have app folder, components, etc.
next-env.d.ts  //declerative file for TypeScript from Next.JS, allowing to add important types
next.config.ts  //Next.js application configuration.all default Next settings, add output: "export", this command will run automatically -when we build our app and allow to create staic website to host it
package-lock.json  //the same as package.json. This file additionally contains sertain library versions. It needs if you have 2 different versions of your project, one is local and one is on production.
tsconfig.json  //settings of TypeScript



src/app //folder app needs to correctly organize Routing and pages
src/app/page.tsx //main page
src/app/(home)  //folder (home) - is not a route for a page, (use brackets if not a Route), without brackets - the Route will be --> /home, with brackets will be --> /
src/app/layout.tsx //layouts, common design for pages. applies to current folder and folders and files inside that folder.

global.css //global css file, you must import/connect this file in current layout.tsx file
anyname.css //local css file for certain page, import/connect in current layout.tsx file
```

```JS
//correct component structure

-app
  -products
    -page.tsx //file can have metadata and hooks, it is a server side. page.tsx most often is responsible for server side.
    -Products.tsx  //use client, it is client component. import this component to page.tsx file
```

# Routing

1. URL depends from folders nesting

```JS
//for this URL -> domain.com/products
//We create new folder in app folder--> and create a file with this name-> page.tsx
src/app/products/page.tsx

//in page.tsx file
export default function Page(){
return (<div>Products</div>)
}
```

2. URL with dynamic parametrs

```JS
//for this URL -> domain.com/products/tv
//We create new products folder in app folder and inside products folder we create tv folder--> and then we create a file with this name-> page.tsx
src/app/products/tv/page.tsx
```

```JS
//create route parameter which has dynamic value in the URL --> products/:id (for example)
//we create a folder with the name in squire brackets, [id] -> will have dynamic value in the URL

src/app/products/[id]/page.tsx
src/app/products/[category]/[item]  //for example URL-> /products/tv/lg, /products/phone/nokia
```

3. Routing exception

```JS
//if you use round brackets in folder name-> (public)
//this folder will not be as a part of your Routing

//folder (home) is not a part of the Route
src/app/(home)/products/tv/page.tsx  // URL --> /products/tv
```

# Next.JS useful Hooks (hooks are used with "use client")

Hooks always are used in Client side.

we use - useState, useEffect

1. useRouter(); <--return an object. Is used to redirect user to some page after some action(for example LogIn)

```JS
//see  --> /components/SigninForm.tsx

//import useRouter from next/navidation

//often used hooks - push and replace -> redirect user to new URL, with option to return back, and no return back option
//refresh - refresh current browser page
'use client'

import { useRouter } from "next/navigation";
import styles from "./Products.module.css"; // Importing CSS module for styling
import Image from "next/image"; // Importing Image component from Next.js

export function Products() {
// const {back} = useRouter();  //navigation back, return to previous page
const {push} = useRouter(); // Using useRouter hook for navigation, redirect user to some page

if(/if the signin was successful){
  push("/products")  //will redirect user to this URL, with option to return back
}else{
  console.log(/some error)
}

  return (
    <div>
      <h1 className={styles.products}>Hello from Products</h1>
      <Image src="/globe.svg" alt="Next.js Logo" width={100} height={100} />
    </div>
  );
}
```

```JS
'use client'
import { useRouter } from "next/navigation";
import styles from "./Products.module.css"; // Importing CSS module for styling
import Image from "next/image"; // Importing Image component from Next.js

export function Products() {

const {replace} = useRouter(); // Using useRouter hook for navigation
//replace hook is the same as push, but it clear a browser history and you can't return back in the browser, is used when redirecting after authoriation , can't return back to authorization (can't return and won't be any error)

replace("/products")  //will redirect user to this URL, with option to return back

  return (
    <div>
      <h1 className={styles.products}>Hello from Products</h1>
      <Image src="/globe.svg" alt="Next.js Logo" width={100} height={100} />
    </div>
  );
}
```

2. usePathname(); <-- return a variable, get the current URL path

```JS
//see --> components/Navigation

"use client"; //if we use usePathname, it should be in -> use client

import Link from "next/link";
import { usePathname } from "next/navigation"; // Importing usePathname to get the current path

type NavLink = {
  href: string;
  label: string;
};

interface Props {
  navLinks: NavLink[];
}

export function Navigation({ navLinks }: Props) {{
const pathname = usePathname();  //show URL address of your current page, used to show active element in the menu bar, or do some logic- if it is this page -> do this code...
// Using usePathname to get the current URL path where the user is currently located

  return (
    <nav className="flex gap-6 text-white/80">
      {navLinks.map((link) => {
        const isActive = pathname === link.href; // Check if the current path matches the link's href

        return (
          <Link
            key={link.href}
            href={link.href}
            className={isActive ? "text-white font-bold" : ""}
          >
            {link.label}
          </Link>
        );
      })}
      }
    </nav>
  );
}
```

3. useSearchParams(); <-- it is working only for query requests, help to get some values from URL

```JS
'use client'

const params = useSearchParams()

//if you want to get dynamic variable from [] folder, that we made in app folder
const params = useParams<{username: string}>()  //username is in squere brackets and it is dynamic value, which is a string data type, it is used to get value from []dynamic folder

params.username
```

```JS
//components/GoogleButton.tsx

"use client";

import { signIn } from "next-auth/react";
import { useSearchParams } from "next/navigation";

interface Props {}

export function GoogleButton({}: Props) {
  const searchParams = useSearchParams(); //hook Must be used in a Client side Component. It returns the URLSearchParams object of the current URL
  const callbackUrl = searchParams?.get("callbackUrl") || "/profile"; //if there is no callbackUrl in the URL, we will redirect user to /profile page after successful sign in

  return (
    <div className="google-page">
      <div className="signIn-google-btn">
        <button
          onClick={() => signIn("google", { callbackUrl })} //signIn is build-In method from next-auth/react, inside we pass the name of the provider we want to use - google
          //callbackUrl is optional, it will redirect user to the specified URL after successful sign in
        >
          Sign In with Google
        </button>
      </div>
    </div>
  );
}
```

# Next.JS Form - appeared in Next.JS 15, we can use Form tag (with big letter)

[ --> Forms <--](https://nextjs.org/docs/app/api-reference/components/form)

```JS
//See --> app/posts3/page.tsx file

import Form from 'next/form'

export default function Page() {
  return (
    //action="..." <--can be passed Server Actions or string.
    //If we put a string - it is URL route, if we put the same page URL there -it will stay on the same page after Form submission and we will get "GET" parametrs, or if it is different URL route - it will redirect user to another page after Form submission and again will get "GET" parametrs.
    <Form action="/search">
      {/* On submission, the input value will be appended to
          the URL, e.g. /search?query=abc */}
      <input name="query" />
      {/* //add "GET" parametrs using input tags by using name="parametrs", if we have name="query" -->  /search?query=abc , abc - it is a value from user Form input and it will be asigned to query key*/}
      <button type="submit">Submit</button>
    </Form>
  )
}

////////////////////////////////////////
//How to use "GET" parametrs

import { getSearchResults } from '@/lib/search'

export default async function SearchPage({
  searchParams,
}: {
  searchParams: Promise<{ [key: string]: string | string[] | undefined }>  //searchParams - has Promise1 type, it can be - string, string[], undefined
}) {
  const results = await getSearchResults((await searchParams).query)  //query <-- it is our parametr name, from --> <input name="query" />, if we use different name,for example for example -title. It will be --> await getSearchResults((await searchParams).title)

  return <div>...</div>
}
```

```JS
//Form also propose useFormStatus, special hook to work with submission form status

//See --> components/SearchButton

"use client";
import { useFormStatus } from "react-dom";

export default function SearchButton() {
  const status = useFormStatus(); // it is spe1cial hook to get the form submission status, it has pending value, if pending = true when submitting the form or false when not submitting

  return (
    <button type="submit">{status.pending ? "Searching..." : "Search"}</button> // Change button text based on form submission status. If form is being submitted (pending = true), show "Searching...", otherwise show "Search"
  );
}
```

# Component - on level Client and Server side Rendering

![pic02](https://github.com/Julian22222/PRACTICE/blob/main/Next_and_NestJS/IMG/next2.jpg)

- In Next.JS you can create server side and client side components
- Server side components will be running on the server, all rendering taking place on server then sending to the browser already made Page, only HTML will be sent
- Client side components will be created in the browser, code is not rendered on the server, will receive HTML, CSS and code.
- in your app you can use both components
- page.tsx file can be - as a server side component (by default) or client side component. Also, other componets can be server or client side.
- Next.JS has a Rule when to use each component :
  - If you receive data from the server ( --> fetch('http://jsonplaceholder.type') ), or just showing something on the page - in this occasion use server components!!!
  - by default, components are server side components
  - if you work with user (if you use useState or other web hooks) - in this occasion use client side components!!! (check example in --> app/myhome/page.tsx). Whithout client side component it will show an error. This component will be proccessed in browser
- If you use client component add -> 'use client'
- async await main function can be used only in server side components

```JS
//main function with async can be only in server side component
export default async function ProductsPage() {
  const data = await fetchData();
  //some code
}

/////////////
"use client";

export function SignInForm({}: Props) {
  //async can be inside main block in client side
  const res = await signIn("credentials", {...}
  //some code
}
```

- To avoid errors with async await and -> use client and useState, etc. ,separare your app on small components and then you can add client side server or client side server where you need. Also, you can insert client side components into server side components
- Metadata block can't be used in 'use client' file, Metadata block is server-only. Metadata block must run on the server!!! - to solve this problem we need to split out file into 2 different components - with 'use client' -client component file and server page file. See app/posts/[id]/page.tsx

```JS
//page.tsx file, this example will show an error!!!

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

### advantages of server side components

- data loading on the server (SSR, ISR, SSG)

- (SSR) - server side rendering -> one request === 1 responce (usual approach), will cause browser delays if you have 1000 users on your website, server always will ask to increase resources because server power of the cloud will not be enough
- (ISR) - incremental site generation -> static (with data update)
- (SSG) - static site generation -> static (without data update)

```JS
//app/posts

//it must be server component

async function getPost(id: string | null) {
  const result = fetch(`https://jsonplaceholder.typicode.com/posts/${id}`).then(
    (res) => res.json()
  );

  //if fetch is done in other file then was imported then you can use client side
  //app/posts2
```

- access to back-end utils and back-end,
- great security on back end server, uses sensetive data (access token, api keys, etc.)
- make light weight on client side and moving all heavy tasks on server
- great when use heavy dependencies

### advantages of Client side components

- use states and effects (hooks, useState, useRef, useEffect, usePathname, etc. )
- use client events (onclick,onchange, onmouseover, etc.)
- use browser API (local storage, etc.)
- use custom brouser hooks
- class components (doesn't work on server side)

If you use different libraries like - font editors, google maps, etc, -> then it is better to use Client component

On Server side don't use SSR but use ISR or SSG, use when security is needed-authorization

# NEXT JS

all component by default are server side components

```JS
'use client'  //use server is not mandatory to write
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

Using Client Component in a Server Component if you need

Server component → wraps → Client component
Client component ❌ cannot import server components.
Client component Runs code in the browser

Server (by default)

- Can fetch data
- can use async await
- File system access with ‘fs’ library
- can use metadata on the page

Client

- can use hooks(useState, useEffect,etc.), onClick, etc
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

```JS
//how to define is it server or client component?

//for example, we have some function
export const getApiToken = () =>{
    'use server'  //<--this function will run only on server side
//this is server action, it will be performing only on the server

    console.log(process.env.TOKEN)
}

```

- Check examples on api/products/page.tsx file

If you use REST API use fetch("http://.....")

# Rendering Strategies (different options)

To check and understand what Rendering option has each our Route in your application, we need to type in terminal-->

```JS
npm run build

//after building we will number of Routes that were generated and next to them - its rendering option. See picture below

//we have: (signs meaning in the bottom of the picture)
//static rendering
//server rendering

//but can have other rendering options if any of our Routes will have other rendering option in our application
```

![pic03](https://github.com/Julian22222/PRACTICE/blob/main/Next_and_NestJS/IMG/next3.jpg)

- CSR - Client side rendering
  Tipical for SPA applications, Rendering is occuring on Client side, bad for SEO optimization

- SSR - Server side rendering
  Rendering is occuring on the server and send already build page to client, also send separate JS to the client which will build in to HTML page - (hydration). Good for SEO. This approach is deprecated (is used only when we don't use app(routing) main folder)

- RSC - React Server components
  New approach, when we have app main directory with all our routing. Rendering is occuring on the server and send already build page to client. It uses striming process (giving data by pieces) without hydration.Good for SEO. Striming HTML static in first request and further navigation.

- SSG - Static Site generation
  HTML rendering takes place on the server side during build (npm run build) and send static HTML to the client. During runtime no excess/extra load on the server side. Server keeps static data and send it to client side when needed. No hydration. Needs to be added by using getStaticParams (in app router api), by default Next.js use SSR and RSC or CSR
  If you want to use dynamic data (when you add, edit or delite something in your app it will not show up, to see changes you need to - npm run build, again)

- ISR - Incremental Static Regeneration
  Allows to rerender static page by timer trigger or event trigger. It contains combination of SSG and SSR/RSC approaches.

  Revalidate uses to rerender the page using one of the triggers

[--> Revalidating Data notes <--](https://nextjs.org/docs/14/app/building-your-application/data-fetching/fetching-caching-and-revalidating)

```JS
//For example if we have this Route--> posts/[id]/page.tsx
//we can make SSG (Static Site generation) from SSR (Server side rendering), if articles or post,etc. not changing, don't want to update

export async function generateStaticParams(){
  const posts: any[] = await getAllPosts();  //getting posts from the server by fetch in different file

  return posts.map(post => ({
    slug: post.id.toString();  //slug <-- is unchangable-always use word-slug, if -> post.id is a number type ->> toString() - must be a string as in URL, id must much with dynamic folder -> [id]
  }))
}
```

```JS
//full example

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

export async function generateStaticParams() {
  const posts: any[] = await getAllPosts();

  return posts.map((post) => ({
    slug: post.id.toString(),
  }));
}

export async function generateMetadata({
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

  revalidatePath("/blog");
  redirect("/blog");
}

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

# Data loading using different options (SSR, ISR, SSG)

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
- Granularity -> Per-page or per-fetch request
- Use case this option: when Data changes on a predictable schedule

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
  const data = await fetchData()

  if(!data){ //can make some checks, on a server component
    //do some logic
    redirect('/404')
  }

  return <Products />
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

```

# Link component

```JS
<Link href="/products">Click Here</Link>  //<--will address user to somhere without page loading,

<a></a>  //<--anker will adress user to somewhere with page loading
```

# Layout

- allow to change visual part, SEO, titles, descriptions
- at least we need to have one layout.tsx file in the app folder/ root of our application, also we can have many layouts. Next.JS don't automatically add HTML and body tags, so we need to add them manually in the RootLayout component, at least we need to have 1 RootLayout component in the app directory.
- We can have many layouts in our app
- if we have root layout all pages will have the same template
- layout.tsx file has a scope. Can use on different levels/ in different folders. Effect on that current folder and children folders and files

# How to use Styles

1. use CSS, we can use css as normal

```JS
//page.tsx file
import "./posts.css";
//some code


//posts.css file
//css code here
```

1. use CSS, using modules

- We need to import module styles
- model styles allows as generate unique classes for specific page, will work only for certain page.
- check app/products/Products.module.css and app/products/Products.tsx

```JS
import styles from "./Products.module.css"; // Importing CSS module for styling

export function Products() {
  return (
    <div>
      <h1 className={styles.products}>Products</h1>  //specify here the class name using styles
    </div>
  );
}
```

```JS
//Products.module.css file
.products {
  background-color: aqua;
}
```

2. Can use SCSS

install in terminal

```JS
npm install -D sass
```

```JS
//rename css file to Products.module.scss

.products {
  background-color: aqua;
}
```

```JS
import styles from "./Products.module.scss"; // Importing SCSS module for styling

export function Products() {
  return (
    <div>
      <h1 className={styles.products}>Products</h1>
    </div>
  );
}
```

# How to use Fonts

check layout.tsx file

```JS
//you don't need to donload anything and add to your project, you just take any font from google fonts
import {Inter} from "next/font/google";

import {Metal} from "next/font/google";
```

# 404 Not Found

- Create file not-found.tsx in the app folder Root, and style that file

```JS
//See --> products/page.tsx

const fetchData = async () => {
  const response = await fetch("https://jsonplaceholder.typicode.com/posts");
  const data = await response.json();
  return data;
};

//this example is using SSR, but you can use ISR or SSG as well
//this approach is not optimized for performance, as it fetches data on every request
//one request === one response,
export default async function ProductsPage() {
  const data = await fetchData(); // Fetching data from the API

  //server-side comonent advantage, can check here, if data is empty or not
  if (!data || data.length === 0) redirect("/404"); // Redirect to not-found page if no data is found

  return (
    <div>
      <h1>Products Page</h1>
      <p>This is the products page.</p>
      {/* <Products data={data}/>   //<--passing data to our component*/}
      <Products />
    </div>
  );
}
```

# Loading page

- we can create preloading page, if data is loading we can show Loading ... message or any other GIF files, spinners
- create loading.tsx file, has a scope. Can use on different levels/ in different folders. Effect on that current folder and children folders and files
- must have name -> loading.tsx
- check app/posts/loading.tsx file

# Error page

- if there an error occured we can through an error message and show the specific page
- error page MUST have 'use client'
- create error.tsx file, has a scope. Can use on different levels/ in different folders. Effect on that current folder and children folders and files
- check app/posts/error.tsx file //will work only on posts id pages
- error message is comming from app/posts/page.tsx -->

```JS
async function fechData() {
  const response = await fetch(
    "https://jsonplaceholder.typicode.com/posts?_limit=10"
  );
  const result = await response.json();

  if (!result || result.length === 0) {
    throw new Error("No posts found"); // Handle error if no posts are found, will send this message to Error page- error.tsx
  }

  return result;
}
```

```JS
//error.tsx file

"use client"; // This file is a client component, allowing it to use hooks like useState, useEffect, etc.

export default function ErrorWrapper({ error }: { error: Error }) {
  //receive error as a prop
  return (
    <div>
      <h2 className="post-header">Error Page</h2>
      <p>Something went wrong. Please try again later.</p>
      <p>Error details: {error.message}</p>  //will show - Error details: No posts found
    </div>
  );
}
```

- also, we need through an error in the posts/[id]/page.tsx (check line 26-28 in app/posts/[id]/page.tsx)

# How to insert Images to Next.JS using unique image component

use Big letter --> <Image .../>

```JS
//src="/nameOfTheFile" <-- if file is located in public folder
 <Image src="/globe.svg" alt="Next.js Logo" width={100} height={100} />
```

# Work with Images in Next.js

- Build in Image component

[ --> Image component props <--](https://nextjs.org/docs/pages/api-reference/components/image)

it allows to make website optimization, with loading images in certain way.
The Next.js Image component extends the HTML <img> element for automatic image optimization.

have different ways how to optimase images:

- can make images - lazy loading, when images loads only when it shown to user on the screen, by scrolling page down images will load -> loading="lazy"
- use different image formats
- etc.

```JS
import type {ImageProps} from '@chakra-ui/next-js';  //data types if we use TypeScript
import NextImage from 'next/image'; //import component from next, NextImage - can be any name

//when we import image in React.JS without framework usually it is a string
import Img from '@/assets/hero.jpg';

//In this case it is an object with certain properties

<Image src={Img} alt="Hero image" />  //2 mandatory, required Props
//by default all image have lazy loading

<Image src={Img} alt="Hero image" priority/> //priority - this image will load first, don't use on all image tags, used only on main image
```

```JS
//not all the images can be loaded locally, some images can be loaded from other servers. In this case we need to adjust the next configurations in --> next.config.ts file and add object with key = images

const nextConfig: NextConfig = {

  images:{
    remotePatterns: [
      //formats: ['image/avif', 'image/web'],
      //deviceSizes: [375, 640, 750, 828, 1080, 1200, 1920]

      //if we use one server where we get Images we have one object in here
      {
        protocol: 'https',
        hostname: 'i.ytimg.com',
        port: '',
        pathname: '/vi/**', //folder direction where the image is
      },
    ]
  }

}

//if we don't use formats it will have default values, can assign some images formats
```

# Meta data

SEO team is responsible for Metadata, they will tell what data you need to insert there.
Metadata is information about your web page that helps browsers and search engines understand what your page is about. This includes things like the page title, description, keywords, and more.

You typically set metadata per page. In Next.js, each page (in the /pages or /app directory) can have its own metadata. This keeps things organized and ensures SEO is focused per page.

Here are some key properties you want to include for SEO in the metadata object:

- title — The page title shown in browser tabs and search results.
- description — A short summary of the page, shown in search results.
- keywords — Keywords related to the page (less important nowadays but still used sometimes).
- robots — Controls if search engines should index or follow the page (index, follow is default).
- openGraph — Metadata for social media sharing (Facebook, LinkedIn, etc.).
- twitter — Metadata for Twitter cards.
- etc.

Metadata

- this block is needed for correct SEO optimization
- usually is connected inside layout.tsx file or in needed file -page.tsx (check app/products/page.tsx)
- main layout will make the same title and description, if you dont have other metadata blocks on the page.tsx or another layout.tsx in folder where you have page.tsx

```JS
//object of metadata
export const metadata: Metadata = {
  title: "X App",
  description: "Front-end insights, styled like X.com",
};
```

```JS
//Example:
export const metadata = {
  title: 'My Awesome Page',
  description: 'This page is about my awesome product.',
  keywords: 'product, awesome, nextjs',
  robots: 'index, follow',
  openGraph: {
    title: 'My Awesome Page',
    description: 'This page is about my awesome product.',
    url: 'https://example.com/page',
    siteName: 'MySite',
    images: [
      {
        url: 'https://example.com/image.png',
        width: 800,
        height: 600,
      },
    ],
    locale: 'en_US',
    type: 'website',
  },
  twitter: {
    card: 'summary_large_image',
    title: 'My Awesome Page',
    description: 'This page is about my awesome product.',
    images: ['https://example.com/image.png'],
  },
}
```

- also can be added other metadata values

```JS
export const metadata: Metadata = {
  title: "X App",
  description: "Front-end insights, styled like X.com",
  keywords: "next.js", "practice", //this meta tag, will help to find your page in search engines
  author: "nameOfTheAuthor",
  //and other tags

  //if you want to add more metadata tags, click --> ctr + space, to see all metadata tags available to add
};
```

- If you want to show dynamic page title -> show post id + other info (as i page title)

```JS
//dynamic metadata title, see example on app/posts/[id]/page.tsx file
//function MUST have this name --> generateMetadata
export async function generateMetadata({params, searchParams}){  //receive 2 parameters as our component in the page.tsx file
  const post = await getPost(params.id)
  return {  //return metadata object
    title: post.title,
    description: post.body,
    //can add other metadata tags if you want
  }
}
```

```JS
//example of dynamic params
// app/blog/[slug]/page.tsx

export async function generateMetadata({ params }) {
  const post = await getPost(params.slug)
  return {
    title: post.title,
    description: post.excerpt,
  }
}
```

You can keep Metadata in layout.tsx file or in page.tsx

-In layout.tsx file - default/shared metadata. The metadata is shared across many pages. You're setting defaults (like a default title or site name). You're setting static metadata that doesn’t change between pages.

-page.tsx file - specific or dynamic metadata. The metadata needs to change depending on the page content (like blog post titles, product names, etc).

You can use Both at the same time - Use layout for base, and page to override as needed

# SEO Optimization

```JS
//static for SEO, important tags
export const metadata: Metadata = {
  title: "Products", //heading of current page
  description: "A page displaying products",
  applicationName:  //for Web application, PWA-Progressive Web Application
  icons:
  manifest:  //important for PWA
  openGraph: //for social media
  robots //can adjust your web app to don't be accessible from search engines
  verification
};
```

```JS
//add dynamic for SEO, for some specific product

export async function generateMetadata({
    params,  //get dynamic data from []folder
}: {
    params: {username}
}): Pomise<Metadata> {
    const product = await getData(username)
    return {title; product.title}
}
```

/////////////////////////////////////////////////////////////////////////////////////

To pass props in any component we can use Redux, Context, Zustand library and SWR

# Midleware

Expanded configuration and optimization

- create middleware.ts is src folder
- middleware it is common function that will run on certain condition, conndition is in config block, in the file.
- Most of the time this is used in Authorization(adjust Roles, close page for certain Roles)
- middleware allow to give access for some pages and forbid access for some pages

```JS
//middleware.ts file

// file has some function that will run after specific triger

import type {NextRequest} from 'next/server'
import {NextResponse} from 'next/server'

//this function will run
export function middleware(request: NextRequest){

const pathname = request.url  //get current URL, where the user is located at the moment


    return NextResponse.redirect(now URL('/home', request.url))
}

export const config = {  //our condition is here to run middleware function, if you use this URL path then function will be invoked
    matcher: '/about/:path*'  //when user on this path it will run function above
    // match: ['/about/:path*', 'dashboard/:path*']
}

//middleware file most often is used for authorization, Page is accessible only for Admin, etc
```

# Environment Variables

```JS
Also, unlike client-side variables, these do not need the NEXT_PUBLIC_ prefix (since they are server-only).
```

1. Server-only variables

- Stored in .env, .env.local, .env.development, etc.
- Accessible only in server code: API routes, getServerSideProps, middleware, or any code that runs on the Node.js side.

```JS
//Example

//.env file
GOOGLE_CLIENT_ID=your-secret-id
GOOGLE_CLIENT_SECRET=your-secret-secret


//ts file
// Safe: runs only on server
const id = process.env.GOOGLE_CLIENT_ID;

//These are never exposed to the browser unless you manually pass them down.
```

2.

```JS
process.env.NEXT_PUBLIC_BASE_URL

NEXT_PUBLIC_ is a special prefix for environment variables in Next.js that makes those variables available in both the server and client-side code.

Why use NEXT_PUBLIC_?

By default, environment variables in Next.js are only available server-side.
If you want to expose an environment variable to your browser (client-side) code, you need to prefix it with NEXT_PUBLIC_.
```

```JS
   Client-side variables (with NEXT_PUBLIC_)
```

```JS
- Must be prefixed with NEXT_PUBLIC_ in your .env file.
- Next.js will inline them into your frontend bundle at build time.
- Accessible in any client component or browser-side JS.
```

```JS
//Example:
//.env file
# Only server-side
DATABASE_PASSWORD=supersecret123

# Available client and server
NEXT_PUBLIC_API_BASE_URL=https://api.example.com


Usage in code:
//ts file
// server or client code
const apiBaseUrl = process.env.NEXT_PUBLIC_API_BASE_URL


Important security note:
Do NOT expose sensitive info (like passwords, API secrets, private keys) with NEXT_PUBLIC_.
Anything prefixed with NEXT_PUBLIC_ is bundled into the client JavaScript and visible to anyone.


In your case:
//.env file
NEXT_PUBLIC_BASE_URL=https://yourdomain.com
```

```JS
//Example

//.env file
NEXT_PUBLIC_API_BASE_URL=https://api.example.com

//ts file
// Safe in client code (browser can see this)
const apiUrl = process.env.NEXT_PUBLIC_API_BASE_URL;

//Use only for values you’re comfortable exposing publicly (like non-secret API URLs, feature flags, analytics keys, etc.).
```

###### Why this distinction matters?

```JS
Something like a Google OAuth client secret must stay server-only → keep it in GOOGLE_CLIENT_SECRET (no prefix).

Something like a frontend base URL or public Stripe publishable key → needs to be visible to the browser, so you add NEXT_PUBLIC_.
```

###### Quick rule of thumb

```JS
Sensitive? (passwords, API secrets, private keys, database URLs) → plain .env, no NEXT_PUBLIC_*.

Needed in browser? (public keys, feature toggles, app base URL) → use NEXT_PUBLIC_.
```

you can absolutely keep both server-only and client-side variables in the same .env file.

```JS
//How it works
//Next.js automatically loads environment variables from .env, .env.local, etc.
//Variables without NEXT_PUBLIC_ → available only on the server.
//Variables with NEXT_PUBLIC_ → injected into both the server and the client bundle.

//Example .env.local:
# Server-only (never exposed to browser)
DATABASE_URL=postgres://user:password@host:5432/db
GOOGLE_CLIENT_SECRET=super-secret

# Client-side (safe to expose)
NEXT_PUBLIC_API_URL=https://api.example.com
NEXT_PUBLIC_FEATURE_X=true

//In code
//Server side:
//ts file
console.log(process.env.DATABASE_URL); // works
console.log(process.env.NEXT_PUBLIC_API_URL); // works too

//Client side (React component):
//tsx file

export default function Home() {
  console.log(process.env.NEXT_PUBLIC_API_URL); // works
  console.log(process.env.DATABASE_URL); // ❌ undefined
  return <div>API: {process.env.NEXT_PUBLIC_API_URL}</div>;
}




//Rule of thumb
//Keep everything in one .env file if you want simplicity.
//Use NEXT_PUBLIC_ prefix only for values you intend to leak to the browser.
//Sensitive secrets should never have the prefix.
```
