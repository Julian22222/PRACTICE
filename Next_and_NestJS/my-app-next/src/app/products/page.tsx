import type { Metadata } from "next";
import { Products } from "./Products"; // Importing the Products component
import { redirect } from "next/navigation"; // Importing redirect function for navigation

//metadata for the products page
export const metadata: Metadata = {
  title: "Products",
  description: "A page displaying products",
  keywords: "products, items, shop",
  authors: [{ name: "Your Name", url: "https://yourwebsite.com" }],
  robots: {
    index: true, // Allow search engines to index this page
    follow: true, // Allow search engines to follow links on this page
  },
};

//Data loading using SSR, ISR, or SSG
//SSR - Server-Side Rendering
//ISR - Incremental Static Regeneration
//SSG - Static Site Generation

//request
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

///////////////////////////////////////////////////////////////////
// SSG option
// const fetchData = async () => {
//   const response = await fetch("https://jsonplaceholder.typicode.com/posts", {
//     // cache: "no-cache", // This is  SSR
//     cache: "force-cache", // This is SSG, Using cache to avoid fetching data on every request, data loading only once
//   });
//   const data = await response.json();
//   return data;
// };

///////////////////////////////////////////////////////////
// ISR option
// const fetchData = async () => {
//   const response = await fetch("https://jsonplaceholder.typicode.com/posts", {
//     cache: "force-cache",
//     next:{
//         revalidate: 3600, // This is ISR, data will be revalidated every 1 hour, data will be udated every hour
//         }
//   });
//   const data = await response.json();
//   return data;
// };
