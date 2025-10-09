import { authConfig } from "@/config/auth"; // Importing the authentication configuration file
import NextAuth from "next-auth/next";

//we import authConfig from config/auth.ts
const handler = NextAuth(authConfig); // Creating a NextAuth handler with the provided configuration. NextAuth must have AuthOptions data type config.

// console.log("GOOGLE_CLIENT_ID:", process.env.GOOGLE_CLIENT_ID);

export { handler as GET, handler as POST }; // Exporting the handler as both GET and POST methods. This is necessary for Next.js API routes to handle different HTTP methods.
