import { NextResponse } from "next/server";
import { posts } from "./posts"; // Importing the posts array from the local file

export async function GET(request: Request) {
  //return NextResponse.json({ name: "John Doe" }); // Example response with a JSON object
  return NextResponse.json(posts); // Return the posts array as a JSON response
}

//It is better to create different folder for search params
// //This example for search parameters // This function handles the GET request to fetch posts
// //Based on search parameters. It returns the posts that match the search query.
//For search params can create different folder, like api/posts/search/route.ts

// export async function GET(request: Request) {
////user can pass query params or not in this method

//   const { searchParams } = new URL(request.url); // Get the search parameters from the request URL, destructuring searchParams

//   ////https://localhost:3000/api/posts?q=Manchester
//   const query = searchParams.get("q"); // Get the 'q' parameter from the URL

//if user don't pass any query, it will return all posts as it is
//   let currentPosts = posts; // Start with all posts

// //if user pass query, it will filter the posts
//   if (query) {
//     // If there is a query, filter the posts based on the title or body
//     currentPosts = posts.filter(
//       (post) =>
//         post.title.toLowerCase().includes(query.toLowerCase()) ||
//         post.body.toLowerCase().includes(query.toLowerCase())
//     );
//   }
//   return NextResponse.json(currentPosts); // Return the filtered posts as JSON response
// }

////////////////////////////////////////////////////////////////////////////////////

export async function POST(request: Request) {
  const body = await request.json(); // Parse and get the JSON body of the request

  posts.push(body); // Add the new post to the posts array

  return NextResponse.json(body);
}
