import { AuthOptions, User } from "next-auth"; //authorize method must return a User object or null
import Google from "next-auth/providers/google";
// import Facebook from "next-auth/providers/facebook";
import Credentials from "next-auth/providers/credentials";
import { users } from "@/shared/data/users.data";

//this authConfig method is for authentication configuration, will be used in api/auth/[...nextauth]/route.ts file and in app/profile/page.tsx file to get the session data.
export const authConfig: AuthOptions = {
  // AuthOptions is an interface/data type provided by next-auth to define the configuration options for authentication.
  // You can add authentication providers here, e.g., Google, Facebook, GitHub, LinkedIn, Twiter etc.
  providers: [
    Google({
      clientId: process.env.GOOGLE_CLIENT_ID!, //this values we get from Google Developer Console. from cloud.google.com
      clientSecret: process.env.GOOGLE_CLIENT_SECRET!, //this values we get from Google Developer Console. from cloud.google.com

      //   clientId: process.env.GOOGLE_CLIENT_ID as string, <-- can be writen as well this way

      authorization: {
        params: {
          prompt: "select_account", // 👈 force Google to always show account chooser. Google defaults to prompt=none or reuses the last account → skipping the chooser.
          access_type: "offline", // optional: useful if you want refresh tokens
          response_type: "code",
          ////Without prompt: "select_account", Google assumes you want to log back in with the last-used account → no chooser. With it, you always get the account chooser.
        },
      },
    }),
    Credentials({
      //credentials has at least 2 fields: credentials and authorize
      credentials: {
        //this is the form fields that user needs to fill to login
        //these fields will be shown on the sign in page automatically
        email: {
          label: "email",
          type: "text",
          placeholder: "insert your email",
          required: true,
        },
        password: {
          label: "password",
          type: "password",
          placeholder: "insert your password",
          required: true,
        },
      },
      async authorize(credentials) {
        //authorize method to verify user credentials(email and password)
        console.log("Credentials:", credentials);

        //authorize method Must return a User type
        if (!credentials?.email || !credentials?.password) {
          return null; //if fields (email or/amd password) is/are missing - return null
        }

        //if credentials are valid, we will return a user object
        //here we can interact with database or external API to verify the credentials. We can make a request to our database or an external service to check if the provided credentials are valid.
        //In this example, we will just hardcode a user object for demonstration purposes. from shared/data/users.data.ts file
        const currentUser = users.find(
          (user) => user.email === credentials.email
        ); //if user exists return the user object, finding user with matching email

        if (currentUser && currentUser.password === credentials.password) {
          //checking if password matches, password should be hashed and salted in real world applications
          //if user is found return the user object

          const { password, ...userWithoutPassword } = currentUser; //destructuring to exclude password from the user object

          return userWithoutPassword as User; // ensure id is a string //returning a user object without password,
          //authorize method Must return a User type

          //or simple example
          //   return {
          //     id: currentUser.id.toString(),
          //     email: currentUser.email,
          //   }; //returning a user object with id, email, name, and role properties
        }

        return null; //if user is not authenticated return null, if credential is not valid, fields are not matching to existing user
      }, //this function is called when user submits the login form. we will write the logic to authenticate the user here. it can be with database or external API
      //authorize method receives credentials and req as parameters. and checks if the credentials are valid and returns a user object if they are valid, otherwise returns null
    }),
  ],
  pages: {
    //We add this object, to tell that we have our own sign in page,
    //By default, NextAuth provides its own sign-in page at /api/auth/signin. But if you want to create your own custom sign-in page, you can specify its path here.
    //signIn property specifies the path to your custom sign-in page. There are other properties as well like signOut, error, verifyRequest, newUser etc.
    signIn: "/signin",
  },
  debug: true, // Enable debug mode for detailed logging during development. Set to false in production.
};
