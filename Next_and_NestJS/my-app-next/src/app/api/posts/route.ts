import { NextResponse } from "next/server";
import { posts } from "./posts"; // Importing the posts array from the local file

export async function GET() {
  return NextResponse.json(posts);
}

// //This example for search parameters // This function handles the GET request to fetch posts
// //Based on search parameters. It returns the posts that match the search query.
//For search params can create different folder, like api/posts/search/route.ts
// export async function GET(request: Request) {
//   const { searchParams } = new URL(request.url); // Get the search parameters from the request URL

//   //https://localhost:3000/api/posts?q=Manchester
//   const query = searchParams.get("q"); // Get the 'q' parameter from the URL

//   let currentPosts = posts; // Start with all posts

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
