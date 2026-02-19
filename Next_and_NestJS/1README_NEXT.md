# ⭐ NEXT.JS (Advantages of Next.js)

- It is a Front-End framework for static(client side) and server side rendering of React applications
- Next.JS has ability to add API endpoints and Server Actions (both help to work with Database without Back-End) but usually it is not used to don't mix Front-end and Back-end. For Back-end we need to use Nest.JS.
- API endpoints and Server Actions are shown in separate file -> README-NEXT_B-END.md file
- It is made from React JS, It is a cover over React JS
- Next JS allows to render the page on the server side and client side (browser)
- Next JS allows to create big, scalable applications
- Next JS has performance and simplicity

Next.js support of:

- CSR - Client Side Rendering,
- RSC - React Server Component,
- SSR - Server-Side Rendering,
- SSG - Static Site Generation
- ISR - Incremental Static Regeneration

Therefore it is flexible to adjust for any tasks.

- Next JS has build-in Routing and SEO optimization, API endpoints and Server Actions (server logic inside component)
- Next JS saves a lot of time and resources, cashing and optimization(images, styles, fonts, scripts) reduce load on a server, reduce hosts.
- Also, Next JS increasing development and hosting applications
- It has great code separation (=== fast loading of your website)
- Very convenient Routing. Routing use folders for Routing. To create a page, we create the folder in app folder and create page.tsx file
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

////If you pass some params (customer_id) -> to child component, child component can be either server or client side component, to get id from URL for example
//Never use useGlobal() or context hooks inside the server component.

# 🔥 Start Next JS project

```JS
//download VScode extension below for Next JS
Next.js 14/15 and React snippets

sci +Tab //new component with interface

sc + Tab //new component

rafce + Tab //standard React new component

```

```JS
npx create-next-app my-app-name //<-- can use this by default or -->

npx create-next-app@13.4 .  //dot in the end means -create app in current folder, next version -13.4

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

npm init -y //create package.json
npm i next react react-dom //download dependencies

scripts:
"dev": "next dev"
//npm run dev  - start Next JS app

//Then create folder - pages (in the Root folder of the project)
//Inside "pages" folder--> index.js //home page -> localhost:3000
//users.js  --> this component will be available using this URL -> localhost:3000/users
```

# ⚠️ Avoid error before pushing Nest.JS and/or Next.JS projects to GitHub

- after you create Next.js project it creates hiden .git folder. It causes .git ,which can cause error when you push the code to GitHub. And if you want to push first time your project it can cause error when you push the code to GitHub. Therefore you need to delete hidden .git folder. After command - "npx create-next-app my-app-name" ,NEXT JS automatically creates its own .git folder inside your project folder. Now you have a .git folder inside .git folder
- after creating new NEST.JS --> ninja-api folder using NEST CLI - it creates hiden .git folder, it causes .git which can cause error when you push the code to GitHub. Your main folder Next_AND_NEST folder has a .git folder. After command - "nest new ninja-api" ,NEST CLI automatically creates its own .git folder inside ninja-api folder. Now you have a .git folder inside .git folder, this is called nested Git repo, and Git doesn't like it. VS Code and GitHub are confused because ninja-api is not part of the main repository remote tracking, ninja-api has no remote link.To solve this error ->
  - open termimal and navigate to your NEST JS or/and Next JS project folder
  - type command: ls -a (list all files, including hidden files)
  - rm -rf .git (delete .git folder inside Nest.JS)

If you don't delete .git inside your Nest.Js project and push the code to GitHub it will create Git folder with arrow on GitHub --> "Git submodule", it is not a regular folder, you can't open this folder

Next.js also has its own .git hidden file

```JS
//If you pushed your code by mistake and you have git folder insed of the git folder
//Remove the submodule reference from your main repo:

git rm --cached bankapp  //bankapp  <-- folder to remove Git submodule
rm -rf .git/modules/bankapp

git commit -m "Remove bankapp submodule"
git add bankapp
git commit -m "Add bankapp as regular folder"
git push origin main

```

# 📤 Deployment

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

# 🗂️ Structure and Components of the Next JS app (Main locations of different files)

```JS
node_modules // folder where you can find all libraries and dependencies (normal and dev dependencies)
public //static folder, path to these files will be static. Here we keep images, other static files
src //main folder, here we keep all main files. src folder allow us to make good application folder structure. Inside src we have app folder, components, etc.
next-env.d.ts  //declerative file for TypeScript from Next.JS, allowing to add important types
next.config.ts  //Next.js application configuration.all default Next settings, add output: "export", this command will run automatically -when we build our app and allow to create static website to host it
package-lock.json  //the same as package.json. This file additionally contains certain library versions. It needs if you have 2 different versions of your project, one is local and one is on production.
tsconfig.json  //settings of TypeScript



src/app //folder app needs to correctly organise Routing and pages
src/app/page.tsx //main page
src/app/(home)  //folder (home) - is not a route for a page, (use brackets if not a Route), without brackets - the Route will be --> /home, with brackets will be --> /
src/middleware.ts //Most of the time this is used in Authorization(adjust Roles, close page for certain Roles). middleware it is common function that will run on certain condition, condition or conditions are indicated in middleware.ts file --> in config block. middleware allow to give access for some pages and forbid access for some pages
src/app/layout.tsx //layouts, common design for pages. applies to current folder and folders and files inside that folder.

global.css //global css file, you must import/connect this file in current layout.tsx file
anyname.css //local css file for certain page, import/connect in current layout.tsx file
```

```JS
//correct component structure

-app/
  └── products/
          └── page.tsx //file can have metadata, it is a server side component. page.tsx most often is responsible for server side.
          └── Products.tsx  //use client, and hooks are used in this client component. import this component to page.tsx file
```

# 📍 Routing

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
//create route parameter which has dynamic value in the URL --> products/:id (for example, like in expres js)
//we create a folder with the name in squire brackets, [id] -> will have dynamic value in the URL

src/app/products/[id]/page.tsx
src/app/products/[category]/[item]/page.tsx  //for example URL-> /products/tv/lg, /products/phone/nokia
```

3. Routing exception

```JS
//if you use round brackets in folder name-> (public)
//this folder will not be as a part of your Routing

//folder (home) is not a part of the Route
src/app/(home)/products/tv/page.tsx  // URL --> /products/tv
```

# 🪝 Next.JS useful Hooks (hooks are used with "use client")

Hooks always are used in Client side.

Next.js has the same main hooks as React.js -> useState, useEffect, usePathname, useParams, useSearchPramas, useMemo, useRef, Custom Hooks, etc. No -> useNavigate hook (React.js has this hook)

🟢 Next.js Hooks (App Router):

- usePathname <- Read current URL path (import { usePathname } from "next/navigation";)
- useParams <- Read dynamic route params
- useSearchParams <- Read query params
- useRouter <- Navigate (push, replace, back)
- useSelectedLayoutSegment <- Active route segmen
- useSelectedLayoutSegments <- Nested route segments

🟣 Next.js Server Utilities (not hooks)
These replace hooks on the server.

- redirect() <- Server-side redirect
- notFound() <- Trigger 404
- headers() <- Read request headers
- cookies() <- Read/write cookies

Used in:
Server Components
Server Actions
Route Handlers

1. useRouter(); <--return an object. Is used to redirect user to some page after some action(for example LogIn), Or you can use redirect navigation - to navigate the user to another page.

useRouter() in NEXT.JS it is -> alternative to useNavigate hook in React.js (the same)

Difference between useRouter() hook and redirect() navigation:

- useRouter() is for client-side navigation
- redirect() is for server-side control flow

Decision rule (easy) what to use - useRouter() hook or redirect() navigation (Ask yourself one question):
Does the user need to click something?

- Yes → useRouter()
- No (logic decides) → redirect()

🔹 useRouter() → user-driven, client-side navigation
🔹 redirect() → logic-driven, server-side control
🔹 Prefer redirect() whenever possible
🔹 Use useRouter() when the UI triggers navigation

```JS
//example 1
//useRouter() — Client-side navigation

"use client";

import { useRouter } from "next/navigation";

const router = useRouter();
router.push("/dashboard");
```

When to use useRouter

Use it when:

Navigation is triggered by user interaction
You’re in a Client Component
You want SPA-like transitions
Typical examples

Button click
Form submit handled on client
Wizard / multi-step UI
Modal close → navigate

```JS
<button onClick={() => router.push("/login")}>
  Login
</button>
```

```JS
//example 2
//redirect() — Server-side navigation
//it is A server function, not a hook.

import { redirect } from "next/navigation";

redirect("/login");
```

Where "redirect" runs?

- Server Components
- Server Actions
- Route Handlers (app/api/\*)
- Middleware
- When to use redirect

Use it when:

- Redirect decision happens before rendering
- Based on auth, cookies, headers, DB
- You want zero UI flash
- You want SEO-friendly redirects
- Typical examples

Protecting routes

- Auth checks
- Role-based access
- After server form submission

```JS
// Server Component
if (!user) {
  redirect("/login");
}
```

```JS
Feature              |    useRouter()     |    	redirect()
                     |                    |
Type                 |    React hook      |  Server function
Runs on              |   Client only      |  Server only
Triggered by         |  User interaction  |  Logic / conditions
Causes re-render     |         Yes        |  No (halts render)
SEO friendly         |         ❌         |         ✅
UI flash risk        |        ⚠️ Yes      |         ❌
Can read cookies/DB  |         ❌         |         ✅
```

Why redirect() feels “magical”

redirect() throws an internal exception that:

- Stops rendering immediately
- Sends a redirect response
- Never shows the page
- That’s why it must run on the server.

```JS
//Real-world examples
//✅ Auth-protected page (BEST PRACTICE)
// app/dashboard/page.js (Server Component)

if (!session) {
  redirect("/login");
}

////////////////////////////
//❌ Don’t do this
"use client";

useEffect(() => {
  if (!session) router.push("/login");
}, []);

//Why ❌:
//- UI flashes
//- Slower
//- Worse UX

////////////////////////////
//✅ After client form submit
"use client";

const onSubmit = async () => {
  await saveData();
  router.push("/success");
};

///////////////////////////////
//✅ After server action
export async function createPost() {
  // save to DB
  redirect("/posts");
}
```

```JS
//useRouter() – Next.js
import { useRouter } from "next/navigation";

const router = useRouter();

router.push("/dashboard");
router.replace("/login");
router.back();
```

```JS
//App Router (app/)
import { useRouter } from "next/navigation";

const router = useRouter();

router.push("/dashboard");
```

```JS
import { redirect } from "next/navigation";

redirect("/login");
//Or:
import { notFound } from "next/navigation";

notFound();

//This is something React Router cannot do.
```

```JS
//see  --> /components/SigninForm.tsx

//import useRouter from next/navidation

//often used hooks - push and replace -> redirect user to new URL, with option to return back, and no return back option
//refresh - refresh current browser page
'use client'  //<--this derective, define component or function that will run on server or browser side.

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

replace("/products")  //will redirect user to this URL, without option to return back. Can't return to previous page.

  return (
    <div>
      <h1 className={styles.products}>Hello from Products</h1>
      <Image src="/globe.svg" alt="Next.js Logo" width={100} height={100} />
    </div>
  );
}
```

```JS
redirect("/pathToRedirect") ////can be used --> in server-side only in Next.js.

router.push("/pathToRedirect") //can be used --> in client-side only for navigation.

//////////////////
//Client side example:
import { useRouter } from "next/navigation";

const router = useRouter();
router.push("/user-page");
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

- searchParams does let you read variables from the URL query string
- This hook is used only for query strings — the part of a URL after the ?

✅ How searchParams works (App Router: /app directory)

In server components, page.tsx, layout.js, and server actions, you can access URL query parameters using searchParams.

```JS
//Example: /app/products/page.tsx
//Example in server component

export default function ProductsPage({ searchParams }) {

  // URL: /products?category=shoes&page=2
  const category = searchParams.category; // "shoes"
  const page = searchParams.page;         // "2"

  return (
    <div>
      <h1>Category: {category}</h1>
      <p>Page: {page}</p>
    </div>
  );
}

//⚠️ Important Notes
//searchParams only works in the App Router, not the old /pages directory.
//It is available automatically as a prop to page.js and layout.js.
//It always returns strings (or undefined).
```

```JS
//Example:  to read those values
//profile?name=John&age=25
//Works only for query parameters (after the ?).

//example in a client component - Client components cannot receive searchParams directly.
//Therefore --> import { useSearchParams } from "next/navigation";

'use client'

import { useSearchParams } from 'next/navigation'


export default function ProfilePage() {
  const searchParams = useSearchParams()
  const name = searchParams.get('name')      // "John"
  const age = searchParams.get('age')        // "25"

  return (
    <div>
      <h1>Name: {name}</h1>
      <p>Age: {age}</p>
    </div>
  )
}
```

```JS
//components/GoogleButton.tsx

"use client";  //<--this derective, define component or function that will run on server or browser side.

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

4. useParams() <-- to get values from dynamic variables, from [] folder.

This hook is used for dynamic route segments — the part of a URL defined by square brackets in your folder structure.

Works only for dynamic route segments (from [folderName]).

```JS
//Example folder:
app/
 └── user/
       └── [username]/
                └── page.tsx

Example URL:
/user/alice
To get the dynamic segment:

'use client'

import { useParams } from 'next/navigation'

export default function UserPage() {
  const params = useParams<{ username: string }>()
  return <h1>Welcome, {params.username}!</h1>
}


///////////////////////////////////////////////


//if you want to get dynamic variable from [] folder, that we made in app folder
const params = useParams<{username: string}>()  //username is in squere brackets and it is dynamic value, which is a string data type, it is used to get value from []dynamic folder

params.username
```

# ✔️ Next.JS Form - appeared in Next.JS 15, we can use Form tag (with big letter)

[ --> Forms <--](https://nextjs.org/docs/app/api-reference/components/form)

The <Form> component you’re using comes from next/form, which is specific to Next.js 14+ App Router. It is not part of React or standard React libraries

✅ In Next.js: <Form> works and has Next.js-specific features like Server Actions and client/server transitions.
❌ In plain React (CRA, Vite, etc.): <Form> does not exist, so you cannot use it. You would just use a normal HTML <form> tag instead.

```JS
//<form> submission:
User -> Browser -> Full page reload -> Server

//<Form> submission:
User -> Browser -> Client-side navigation -> Server Action -> Partial page update
```

Practical takeaway:

- Use <form> if:
  - You are in plain React or want simple GET/POST forms.
  - You don’t need Server Actions or client-side navigation.
- Use <Form> if:
  - You’re in Next.js App Router.
  - You want to use Server Actions without writing API routes.
  - You want smoother client-side UX without page reloads.

```JS
//See --> app/posts3/page.tsx file

import Form from 'next/form'

//How to use "GET" parametrs
export default async function SearchPage({  // searchParams is value from "GET" parametrs that we submit by using <Form>
  searchParams,
}: {
  searchParams: Promise<{ [key: string]: string | string[] | undefined }>  //searchParams - has Promise type, it can be - string, string[], undefined
}) {

  const results = (await searchParams).query  //query <-- it is our parametr name, from --> <input name="query" />, if we use different name,for example -title. It will be --> await getSearchResults((await searchParams).title)

  //use some logic with results variable

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

    <NewComponent />
  )
}
```

```JS
//Form also propose useFormStatus, special hook to work with submission form status

//See --> components/SearchButton

"use client";  //<--this derective, define component or function that will run on server or browser side.
import { useFormStatus } from "react-dom";

export default function SearchButton() {
  const status = useFormStatus(); // it is special hook to get the form submission status, it has pending value, if pending = true when submitting the form or false when not submitting

  return (
    <button type="submit">{status.pending ? "Searching..." : "Search"}</button> // Change button text based on form submission status. If form is being submitted (pending = true), show "Searching...", otherwise show "Search"
  );
}
```

```JS
//different ways how you can use "actions" in <Form> tags

<Form action="/search"> (string URL)  //<-- after submit the form - Browser goes to /search?… with params appended

<Form action={serverAction}> (Server Action)  //<-- after submiting the form - Calls the action on the server; navigation/update depends on what the action does
```

Also, Form has --> Suspense tag and use function

# ➡️ Midleware

Expanded configuration and optimization

- create middleware.ts in src folder
- middleware it is common function that will run on certain condition, condition or conditions are indicated in middleware.ts file --> in config block.
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
    matcher: '/about/:path*'  //when user use this path it will invoke function above
    // match: ['/about/:path*', 'dashboard/:path*']
}

//middleware file most often is used for authorization, Page is accessible only for Admin, etc
```

# 🪹 Environment Variables

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

//❗ These are never exposed to the browser unless you manually pass them down.
```

2. Client side variables (and server side. Available on both)

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


❗ Important security note:
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

# Link component

```JS
Always use <Link> tags instead of <a>
```

```JS
<Link className="posts-link" to="/products">
    Click Here
</Link>  //<--will address user to somhere without page loading,


<a href="https;//domainName">Click Here</a>  //<--anker will adress user to somewhere with page loading
```

# 🔗 Link tags VS. anker tags

- when use `<a>` tags, usually it uses to connect web pages togther, but by default it will upload new page --> which defeats one of the purposes of React and single page applications.
- If you need a full page reload (e.g., for external links or to reset the entire application state), use the standard HTML `<a>` tag instead of `<Link>`.

```JS
<a href="/your-path">Click here</a>
```

- Therefore we use `<Link>` to don't upload entire page. For example using `<Link>` tags are useful in NavBar component, NavBar component will be outside `<Routes>` and inside `<BrowserRoutes>`
- `<Link>` component is designed specifically to prevent full page refreshes. It enables client-side routing, which updates the URL and changes the view without fetching a new HTML document from the server, making your app feel faster and more seamless.

```JS
<nav>
    <Link to="/" > Home </Link>
    <Link to="/about" > About </Link>
</nav>
```

- Alos, we can wrap <Link> tag aroun any quantity child elements-->

```JS
<ul>
    <li>
        <Link to="/topics/1" >
            <h2> Topic Id 1 </h2>
            <img src="http://....." alt="topic1" />
            <p>Topics 1's tag link </p>
        </Link>
    </li>
</ul>
```

# Layout

- allow to change visual part, SEO, titles, descriptions
- at least we need to have one layout.tsx file in the app folder/ root of our application, also we can have many layouts. Next.JS don't automatically add HTML and body tags, so we need to add them manually in the RootLayout component, at least we need to have 1 RootLayout component in the app directory.
- We can have many layouts in our app
- if we have root layout all pages will have the same template
- layout.tsx file has a scope. Can use on different levels/ in different folders. Effect on that current folder and children folders and files
- layouts and pages are server components by default in Next.js 13+
- if you add "use cleint " to the layout, all its children will become client components too, which is not desired (not recommended).
- If your layout or provider imports any "use client" modules incorrectly, or the page imports a client-only module in the wrong way, you may get the params Promise error after navigation.

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
- ❗ error page MUST have 'use client'
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

- ❗ use Big letter --> <Image .../>
- Next.JS <Image ../> by default has loading="lazy"

```JS
//In Next.js, when you use the built-in <Image /> component from next/image, images are lazy-loaded by default. This means they won't load until they're close to entering the viewport, which improves performance.

So your code:

<Image src="/globe.svg" alt="Next.js Logo" width={100} height={100} />

//will automatically have lazy loading behavior without you needing to add loading="lazy" explicitly.

//If you want to disable lazy loading (e.g., for above-the-fold images), you can add:

<Image src="/globe.svg" alt="Next.js Logo" width={100} height={100} loading="eager" />

//But by default, it’s lazy.
```

```JS
//src="/nameOfTheFile" <-- if file is located in public folder
 <Image src="/globe.svg" alt="Next.js Logo" width={100} height={100} />
```

# Work with Images in Next.js

In Next.js, using the built-in component -->

```JS
<Image src="/...." alt="...."/>
```

is generally better than using a plain HTML tag -->

```JS
<img src="/...." alt="...."/>
```

because "Image" provides automatic performance optimizations, better user experience, and built-in best practices with almost no extra effort.

✅ Why "Image" tag is better than "img"

1. Automatic image optimization

Next.js optimizes images on-demand:

- Generates multiple sizes for different devices (responsive images)
- Serves modern formats like WebP when supported
- Compresses images automatically
- Delivers correctly sized images based on device viewport

With "img" tag , you would have to handle all this manually.

2. Built-in lazy loading

"Image" tag - automatically lazy-loads images that are off-screen, improving:

- Page load speed
- Core Web Vitals (especially LCP)

"img" tag - requires loading="lazy" and still won’t handle all optimizations.

3. Prevents layout shift (CLS)

"Image" tag - reserves space based on width/height or aspect-ratio, preventing layout jumps.

"img" tag - can cause layout shift unless you manually set width/height or CSS aspect ratios.

4. Integrated with Next.js CDN

Images are cached and served from the Next.js Image Optimization CDN (or your configured loader).

"img" tag - has no optimization pipeline and serves raw image files.

5. Responsive image support

Simple API for responsive behavior:

```JS
<Image
  src="/hero.png"
  width={800}
  height={600}
  sizes="(max-width: 768px) 100vw, 800px"
  alt="Hero image"
/>
```

Next.js generates images for each size automatically.

With "img" you'd have to manually create and provide srcset and multiple image versions.

6. Better caching

Next.js handles immutable caching, etags, and revalidation automatically.

"img" tag - depends entirely on how the server sets headers.

7. Built-in security checks

Next.js ensures:

- Safe external domains (via next.config.js)
- Avoiding untrusted or oversized image downloads

"img" provides no such protections.

❓ When should you NOT use "Image " ?

Use "img" if:

- You need to display raw SVG (not rasterized)
- You need extremely custom behavior that "Image" can’t support (rare)
- You’re dealing with content inside MDX/Markdown (unless using next-mdx-remote + custom components)

```JS
Feature                      |    <Image />      |   	<img>
                             |                   |
Automatic optimization       |     ✅ Yes        |    ❌ No
Lazy loading                 |      Auto         |  	Manual
Prevents layout shift        |      Auto         |   	Manual
Responsive sizes             |      Auto         |    Manual
CDN optimization             |      Yes          |    No
Modern formats (WebP/AVIF)   |      Yes          |    No
Caching + compression        |   	  Auto         |    Manual
Easiest developer experience |      Yes          |    No
```

➡️ Use "Image" whenever possible.
It makes your site faster, improves SEO, and reduces manual work drastically.

- Build in Image component

[ --> Image component props <--](https://nextjs.org/docs/pages/api-reference/components/image)

it allows to make website optimization, with loading images in certain way.
The Next.js Image component extends the HTML "img" tag element for automatic image optimization.

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

Metadata in Next.js (including generateMetadata) can be used only in Server Components, because metadata is resolved on the server at build/request time.

Metadata can be defined in any of the following, as long as they are server components:

Metadata can be used in :

- layout.ts
- page.tsx
- generateMetadata() inside any page or layout

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
