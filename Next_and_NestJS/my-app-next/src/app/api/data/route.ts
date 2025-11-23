// export async function GET(request: Request) {
//   //GET request handler for the API route
//   return new Response("Hello, World!");
// }

import { NextResponse } from "next/server";

// export async function POST(request: Request) {
//   return new Response(request.body);
// }

////////////////////////////////////////////////////////

export async function GET(request: Request) {
  //receiing a request from the client with the GET method
  //here you can receive an object from Database, and return it as a response
  const data = {
    id: 1,
    title: "Sample Data",
    message: "Hello, World!",
    timestamp: new Date().toISOString(),
  };

  return new Response(JSON.stringify(data));

  // //or can use NextResponse to return a JSON response
  // return NextResponse.json(data, {
  //   status: 200, // HTTP status code
  //   headers: {
  //     "Content-Type": "application/json", // Set the content type to JSON
  //     "Cache-Control": "no-cache", // Disable caching for this response
  //   },
  // });
}

///////////////////////////////////////////////////////////////////////////////////

// export async function GET(request: Request) {
//   //GET request handler for the API route
//   // Handle GET request
//   const response = await fetch(
//     "https://jsonplaceholder.typicode.com/posts?_limit=10"
//   );
//   const data = await response.json();

//   return new Response(JSON.stringify(data), {
//     headers: { "Content-Type": "application/json" },
//   });
// }

export async function POST(request: Request) {
  //POST request handler for the API route
  // Handle POST request
  const body = await request.json(); // Parse the JSON body of the request

  const response = await fetch("https://jsonplaceholder.typicode.com/posts", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify(body), // Send the parsed body as JSON
  });

  const data = await response.json();

  return new Response(JSON.stringify(data), {
    headers: { "Content-Type": "application/json" },
  });
}

export async function PUT(request: Request) {
  //PUT request handler for the API route
  // Handle PUT request
  const body = await request.json(); // Parse the JSON body of the request

  const response = await fetch(
    `https://jsonplaceholder.typicode.com/posts/${body.id}`,
    {
      method: "PUT",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify(body), // Send the parsed body as JSON
    }
  );

  const data = await response.json();

  return new Response(JSON.stringify(data), {
    headers: { "Content-Type": "application/json" },
  });
}

export async function DELETE(request: Request) {
  //DELETE request handler for the API route
  // Handle DELETE request
  const url = new URL(request.url);
  const id = url.searchParams.get("id"); // Get the ID from the query parameters

  if (!id) {
    return new Response("ID is required", { status: 400 });
    //return NextResponse.json({ error: 'ID is required' }, { status: 400 })
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
// This is a simple API route example for handling CRUD operations
