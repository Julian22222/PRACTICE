import { AuthOptions } from "next-auth";
import Google from "next-auth/providers/google";
// import Facebook from "next-auth/providers/facebook";

export const authConfig: AuthOptions = {
  // AuthOptions is an interface/data type provided by next-auth to define the configuration options for authentication.
  // You can add authentication providers here, e.g., Google, Facebook, GitHub, LinkedIn, Twiter etc.
  providers: [
    Google({
      clientId: process.env.GOOGLE_CLIENT_ID!, //this values we get from Google Developer Console. from cloud.google.com
      clientSecret: process.env.GOOGLE_CLIENT_SECRET!, //this values we get from Google Developer Console. from cloud.google.com

      //   clientId: process.env.GOOGLE_CLIENT_ID as string, <-- can be writen as well this way
    }),
  ],
  debug: true, // Enable debug mode for detailed logging during development. Set to false in production.
};
