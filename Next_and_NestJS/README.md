# NEXT.JS (Advantages of Next.js)

- It is a Front-End framework for static(client side) and server side rendering of React applications
- Next.JS has ability to add API endpoints, but usually it is not used to don't mix Front-end and Back-end. For Back-end we need to use Nest.JS
- It is made from React JS, It is a cover over React JS
- Next JS allows to render the page on the server side and client side (browser)
- Next JS allows to create big, scalable applications
- Next JS has performance and simplicity

support of CSR - client side rendering,
RSC - react server component,
SSR - server side rendering,
SSG - static site generation,
ISR - incremental site generation.

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
bunx xreate-next-app@latest my-app-name
//bun dev - start app
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

can be hosted on vercel.com

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
//components/SigninForm.tsx

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

# Component- level Client and Server side Rendering

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

- To avoid errors with async await and -> use client and useSatte, etc. ,separare your app on small components and then you can add client side server or client side server where you need. Also, you can insert client side components into server side components
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

  SSR- one request === 1 responce (usual approach), will cause browser delays if you have 1000 users on your website, server always will ask to increase resources because server power of the cloud will not be enough
  ISR - static (with data update)
  SSG static (without data update)

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

To check and see Rendering option for each our Route in your application, we need to type -->

```JS
npm run build

//static rendering
//server rendering
```

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
//SSG example

export async function generateStaticParams(){
  const posts: any[] = await getAllPosts();  //getting posts from the server by fetch in different file

  return posts.map(post => ({
    slug: post.id.toString();  //slug is unchangable, toString() - must be a string as in URL, id must much with dynamic folder -> [id]
  }))
}

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
  const response = await fetch('https://api.example.com/products') //will keep the post in cache, by default is --> cache: 'force-cache'
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

```JS
//ISR option

import type {Metadata} from 'next'
import {Products} from './Products'

export const metadata: Metadata = {
  title: 'Products'
}

const fetchData = async()=>{  //async request to the server
  const response = await fetch('https://api.example.com/products',{
    cache: 'force-cache',
    next:{
      revalidate: 200 //will update data every 0,2sec( if you change item name, price, etc)
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
export const revalidate = 200;

export default async function Blog(){
  const response = await fetch('https://api.example.com/products');
}

//it is the same as
const response = await fetch('https://api.example.com/products',{
    cache: 'force-cache',
    next:{
      revalidate: 200 //will update data every 0,2sec( if you change item name, price, etc)
    }
```

# Link component

```JS
<Link href="/products">Click Here</Link>  //<--will address user to somhere without page loading,

<a></a>  //<--anker will adress user to somewhere with page loading
```

# Layout

- allow to change visual part, seo, titles, descriptions
- at least we need to have one layout.tsx file in the app folder/ root of our application, also we can have many layouts. Next automatically don't add HTML and body tags, so we need to add them manually in the RootLayout component, at least we need to have 1 RootLayout component in the app directory.
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

3. how to use fonts

check layout.tsx file

```JS
//you don't need to donload anything and add to your project, you just take any font from google fonts
import {Inter} from "next/font/google";

import {Metal} from "next/font/google";
```

# 404 Not Found

- create file not-found.tsx in the app folder Root, and style that file

# Loading page

- we can create preloading page, if data is loading we can show Loading ... message or any other GIF files, spinner
- create loading.tsx file, has a scope. Can use on different levels/ in different folders. Effect on that current folder and children folders and files
- must have name -> loading.tsx
- check app//posts/loading.tsx file

# Error page

- if there an error occured we can through an error message and show the specific page
- error page MUST have 'use client'
- create error.tsx file, has a scope. Can use on different levels/ in different folders. Effect on that current folder and children folders and files
- check app//posts/error.tsx file //will work only on posts id pages

```JS
//error.tsx file

"use client"; // This file is a client component, allowing it to use hooks like useState, useEffect, etc.

export default function ErrorWrapper({ error }: { error: Error }) {
  //receive error as a prop
  return (
    <div>
      <h2 className="post-header">Error Page</h2>
      <p>Something went wrong. Please try again later.</p>
      <p>Error details: {error.message}</p>
    </div>
  );
}
```

- also, we need through an error in the posts/[id]/page.tsx (check line 26-28 in app/posts/[id]/page.tsx)

# How to insert Images to Next.JS using unique image component

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

title — The page title shown in browser tabs and search results.
description — A short summary of the page, shown in search results.
keywords — Keywords related to the page (less important nowadays but still used sometimes).
robots — Controls if search engines should index or follow the page (index, follow is default).
openGraph — Metadata for social media sharing (Facebook, LinkedIn, etc.).
twitter — Metadata for Twitter cards.

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

# Server Actions -

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

##### If you want Component to behave the same wasy from different pages

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

To create API routes inside /app directory, we create directory /api inside app directory. /api folder can have other directories inside where you create route.ts file.

If route.ts file is located /app/api/posts , then URL request will be /api/posts

route.ts file MUST export an object with function and with one of methods: GET, POST, DELETE, PUT, PATCH, etc.

Next.js is one of the best front-end frameworks, it is better practice to don't add back-end to Next.js. It is not good practice to mix back-end database, PRISMA etc. with front-end in Next.js.
If you will mix everithing in Next.js - Once your application will grow/expand, you will have more problems, and trash bin with all different files in one place.

But Next.js has ability to work with back-end.

- allow to control and adjust server side.
- You create any database and you can work with that Database from Next.Js Framework. You don't need to install express.js, node.js, or other additional frameworks, etc.
- Next.js applicationYou allow to connect to your Database and get needed values from that database. We use --> app/api folder and route.ts file

```JS
YOU MUST create folder api inside app folder and file name MUST be - route.ts

IF we have folders location -> app/api/data - the URL to get data will be --> /api/data + request method(GET,POST, PUT, PATCH, DELETE, etc.)

IF we have folders location -> app/api - the URL to get data will be --> /api + request method(GET,POST, PUT, PATCH, DELETE, etc.)
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

- Can use Next.js helpers

```JS
//If we need to delete the post by its ID and we don't need to return anything but we need to redirect user to another page
import { NextResponse } from 'next/server'
import { headers, cookies } from 'next/headers'  //Next JS helpers, can check cookies and headers not mandatory
import { redirect } from 'next/headers'  //Next JS helpers

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
