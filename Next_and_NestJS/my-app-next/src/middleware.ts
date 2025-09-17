import type { NextRequest } from "next/server";
import { NextResponse } from "next/server";
import withAuth from "next-auth/middleware"; // Middleware that checks if the user is authenticated/authorized. If they aren't, they will be redirected to the login page. Otherwise, continue.
//withAuth - imports a helper from NextAuth that automatically protects certain routes (pages). It checks if a user is logged in (has a session)

// //Middleware to handle requests globally
// export function middleware(request: NextRequest) {
//   // Example: Log the request URL
//   console.log("Request URL:", request.url);

//   //access to our cookies
//   //cookies.get("token"); // Example: Get a cookie named "token"

//   //   const pathname = request.url; // Get the pathname from the request URL, where the user is located at the moment
//   const pathname = request.url.includes("/about") ? "about" : "dashboard"; // Check if the pathname includes "/about"

//   //const isAbout = request.url.includes("/about") // Check if the pathname is "about"
//   //   if (isAbout) {
//   //     //if user is on /about page
//   //     if (true) {
//   //       //if user is authorized, we can redirect him to some page
//   //       return NextResponse.next(); //we can allow him to continue to the page
//   //     }else return NextResponse.redirect(new URL("/login", request.url)); //if user is not authorized, we can redirect him to login page or /home page
//   //   }

//   return NextResponse.redirect(new URL("/api/auth/signin", request.url)); //will redirect user to sign in page always when he tries to access /profile page, (mentione in config below, line 44)

//   // Example: Redirect to a specific page if the request is for a specific path
//   //   if (request.nextUrl.pathname === "/old-path") {
//   //     return NextResponse.redirect(new URL("/new-path", request.url));
//   //   }

//   // Continue with the request
//   //   return NextResponse.next();
// }

///////////////////////////////////////////////////////////////////////////////

//If no session, it redirects to /api/auth/signin (or your custom login page).
//If session exists, it allows the request to go through and your /profile page will see the session data.
//This sets up the middleware that will run before your page loads. If the user is logged in → let them see the page. If not → redirect them to the login page.
export default withAuth({
  pages: {
    signIn: "/signin", // This tells NextAuth where to send users if they are not logged in. In this case, it sends them to /api/auth/signin (the built-in NextAuth sign-in page).
    //You could change this to /signin if you create your own custom login page
  },
});

export const config = {
  //matcher - allow to create private routes
  // Apply middleware to all paths,
  // matcher: ["/:path*", '/profile'], // This will match all paths in the brackets. '/profile' - static path, '/:path*' - dynamic path
  //matcher <-- is a property that defines which paths the middleware should apply to, to be private routes
  // matcher: "/about/:path*", // This will match all paths except API routes, static files, and favicon
  matcher: ["/profile", "/protected/:path*"], //making /profile and /protected private routes, so only authorized users can access them
  //The matcher tells Next.js which routes should use this middleware.
  // /profile → protected route
  // /protected/:path* → anything under /protected/... is also protected

  //If you go to /profile and you are not logged in, you’ll be redirected to /api/auth/signin.
  //If you are logged in, you’ll see the page.
};
