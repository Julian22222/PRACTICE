import { match } from "assert";
import type { NextRequest } from "next/server";
import { NextResponse } from "next/server";

// Middleware to handle requests globally
export function middleware(request: NextRequest) {
  // Example: Log the request URL
  console.log("Request URL:", request.url);

  //access to our cookies
  //cookies.get("token"); // Example: Get a cookie named "token"

  //   const pathname = request.url; // Get the pathname from the request URL, where the user is located at the moment
  const pathname = request.url.includes("/about") ? "about" : "dashboard"; // Check if the pathname includes "/about"

  //const isAbout = request.url.includes("/about") // Check if the pathname is "about"
  //   if (isAbout) {
  //     //if user is on /about page
  //     if (true) {
  //       //if user is authorized, we can redirect him to some page
  //       return NextResponse.next(); //we can allow him to continue to the page
  //     }else return NextResponse.redirect(new URL("/login", request.url)); //if user is not authorized, we can redirect him to login page or /home page
  //   }

  return NextResponse.redirect(new URL("/home", request.url));

  // Example: Redirect to a specific page if the request is for a specific path
  //   if (request.nextUrl.pathname === "/old-path") {
  //     return NextResponse.redirect(new URL("/new-path", request.url));
  //   }

  // Continue with the request
  //   return NextResponse.next();
}

export const config = {
  // Apply middleware to all paths
  // matcher: ["/:path*"], // This will match all paths
  matcher: "/about/:path*", // This will match all paths except API routes, static files, and favicon
};
