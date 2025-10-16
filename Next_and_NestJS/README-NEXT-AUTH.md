# Authentication for the Web.

Authentication for your application. We will use Auth.js library (before known as NextAuth library). Free and open source

[https://authjs.dev/](https://authjs.dev/)

- this library support not only Next.js but others.
- For React JS is better to use Authentication library -> Auth0
- this library allows to create Login page, can use Google account to Login

1. Need to install

```JS
npm i next-auth
```

2. Auth.js works through special api routes, therefore we need to create new api route for authentication - app/api/auth/[dynamicRoute]/route.ts

- in our case - [...nextauth] <--dynamic route

3. Create config/auth.ts file <-- configuration for Authorization

- In config/auth.ts we have a function, export const authConfig: AuthOptions = {..} <- It Must contain different providers
- any provider represent a function that needs to be invoked, passing some settings inside.
- different providers have different settings

```JS
//If we use Google provider - we have at least 2 settings that needs to be field- clientId and clientSecret. they needed to make Google work for authentication.

export const authConfig: AuthOptions = {
  // You can add authentication providers here, e.g., Google, Facebook, GitHub, LinkedIn, Twiter etc.
  providers: [
    Google({
      clientId: process.env.GOOGLE_CLIENT_ID || "",  //Google settings --> clientId
      clientSecret: process.env.GOOGLE_CLIENT_SECRET || "",  //Google settings --> clientSecret
    }),
  ],
};
```

```JS
//.env file

//Auth Providers (Credentials). For Google Provider
GOOGLE_CLIENT_ID = 'Google Client ID Secret'
GOOGLE_CLIENT_SECRET = 'Google Clernt Secret'

// Auth config, Must be declared. Use these variables/keys!!!
//This variables will be used by NextAuth library, these will help to create some keys and tokens
NEXTAUTH_SECRET = 'supersecret'  //value can be anything
NEXTAUTH_URL = 'http://localhost:3000' //should be URL address of our app
```

- Where we can find these Google settings?

![pic03](https://github.com/Julian22222/PRACTICE/blob/main/Next_and_NestJS/IMG/pic3.jpg)

Go to --> [cloud.google.com](cloud.google.com) and press GET started for Free. If you are Authorized/registred, click on my-> Console (see pic above)

![pic04](https://github.com/Julian22222/PRACTICE/blob/main/Next_and_NestJS/IMG/pic4.jpg)

- then search and choose APIs & services (step 1 on pic above), create new project if you don't have any or you want to create new project - click on step 2. And go to Credentials(left side menu bar, step 3)

![pic05](https://github.com/Julian22222/PRACTICE/blob/main/Next_and_NestJS/IMG/pic5.jpg)

- Create new credentials (see pic above)--> Create credentials (step 1) -> OAuth client ID (step 2, see pic above)

![pic06](https://github.com/Julian22222/PRACTICE/blob/main/Next_and_NestJS/IMG/pic6.jpg)

- then we add type - Web application (step 1)
- add the name (step 2)
- adding 2 URL links:
  While we are working on localhost, we add the root URl of our web site (step 3)

  - this will create a URL route --> http://localhost:3000

  Then if we use Auth.js library, we provide another URL link (step 4)

  - http://localhost:3000/api/auth/callback/google //google <- is the name of provider we use, if we have different provider than change it, --> http://localhost:3000/api/auth/callback/github //this is for github

Then click -> Create button, it will give Client ID and Client secret, copy these 2 values and paste them to --> .env.local, then use them in config/auth.ts

which will allow to sign in with Google account, also can use other providers to sign in with other providers

- Then we import this config/auth.ts file into app/api/auth/[...nextauth]/route.ts file (line 5)

```JS
const handler = NextAuth(authConfig);
```

After we installed Auth.js library and created config with at least 1 provider, our app automatically created new signIn page -> with route:

```JS
http://localhost:3000/api/auth/signin  //<--signIn with Google account
```

This provide bandle of helpers depending with what componet do you work. Do you work with Client component (will have different helpers) or with Server components have different helpers.

- Then in Navigation component, where we have our app navigation between app different pages. There we add some logic showing is the user Authorized or not.

```JS
//useSession(); can be used only in Client component!!!
"use client"

const session = useSession();  //get this method from next-auth library, it will help to show is the user Authorized or not.
console.log("Session data in Navigation:", session); // Log session data to see its structure and contents. This object has data, status and update keys. We need data keys

//useSession(); --> must be wrapped in a <SessionProvider />  //works as context in React
```

- Therefore we create new component - Providers.tsx
- Then we use Providers component in our main root app folder - layout.tsx file, and wrap all our app with Providers component

```JS
return (
    <html lang="en">
      <body>
        <Providers>
          <Header />
          {children}
        </Providers>
      </body>
    </html>
  );
```

- then in Navigation.tsx file, we can use session from NextAuth library to track is the user LogedIn or not

- NextAuth library provide some functions to use --> SignIn and SignOut

```JS
import { useSession, signIn, signOut } from "next-auth/react";
```

```JS
 {/* If session data exists, we will show another Link button in Navigation menu to our Profile page */}
      {session?.data && ( //if session data exists, it means user is logged in
        <Link href={"/profile"}>Profile</Link>
      )}

      {/* If session data exists - show Sign Out button otherwise show SignIn button */}
      {session?.data ? (
        <Link
          href={"#"}
          onClick={() => {
            signOut({ callbackUrl: "/" }); // Redirect to home page after sign out, signOut function is build-in from next-auth/react
          }}
        >
          Sign Out
        </Link>
      ) : (
        <Link href={"/api/auth/signin"}>Sign In</Link>
      )}
```

- Then we can create Profile page (it will be server side component)

To use session data in server component we need to write -->

```JS
//useSession(); can be used only in Client side component!!!
//const session = useSession();  //This example won't work in server side component. session helps to show is the user Authorized, ans user's data or not authorized.

//we have another helper to use in the server side component
const session = await getServerSession(authConfig);  //have authConfig atribute, the same config from our file config/auth.ts
```

Profile Page Must be closed from users if the user is not loged In

- if user will use URL- http://localhost:3000/profile , without login, it will be still available.
- We need to make this route private, not available for not logedIn users -> http://localhost:3000/profile
- Therefore we need to create file -middleware.ts in the root of our application - in src folder
- when we work with NextAuth library we can use alredy build in middleware. We could write the code, to make route private by ourself but it easier to use buildIn settings from NextAuth library

```JS
import withAuth from "next-auth/middleware"; // Middleware that checks if the user is authenticated/authorized. If they aren't, they will be redirected to the login page. Otherwise, continue.
//withAuth - imports a helper from NextAuth that automatically protects certain routes (pages). It checks if a user is logged in (has a session)

//see below how to create your own signIn page and style it how you want
//this option is used Build-in option to sign In
export default withAuth({
  pages: {
    signIn: "/api/auth/signin", // This tells NextAuth where to send users if they are not logged in. In this case, it sends them to /api/auth/signin (the built-in NextAuth sign-in page).
    //You could change this to /signin if you create your own custom login page
  },
});


  //This allow to create private routes
export const config = {
  // matcher: ["/:path*", '/profile'], // This will match all paths in the brackets. '/profile' - static path, '/:path*' - dynamic path
  //matcher <-- is a property that defines which paths the middleware should apply to, to be private routes
  // matcher: "/about/:path*", // This will match all paths except API routes, static files, and favicon
  matcher: ["/profile", "/protected/:path*"], //making /profile and /protected - private routes, so only authorized users can access them, if user not logedIn he can't use URL - /profile and will be redirected to /api/auth/signin URl to signIn. Also, we will have callbackURL on the actual URL and after succesfull logIn, user will be redirected to /profile (page that was provate before login)
};
```

- when user will use URL - http://localhost:3000/profile without login. user will be redirected to /api/auth/signin to sign In. Also, URL will have callbackURL data, witch will redirect succesfully logedIn user to clicked page before loged in page.

# Add MextAuth.JS build-In sign In Form together with other signIn providers, on the same page

- In config/auth.ts file we can add

```JS
import { AuthOptions, User } from "next-auth";
import Credentials from "next-auth/providers/credentials";  //allow to make signIn form (email, password, etc.) without any SignIn providers

export const authConfig: AuthOptions = {
    providers: [
    //some code to connect Providers- Goggle, Facebook, GitHub, etc.
  }),

  Credentials({     //credentials has at least 2 fields: credentials and authorize
      credentials: {
        //Here we declere form fields that user need to fill to login, these fields will be shown on the sign in page automatically
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
        console.log("Credentials:", credentials);

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

         //authorize method Must return a User type
          return userWithoutPassword as User; // ensure id is a string //returning a user object without password

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
  debug: true, // Enable debug mode for detailed logging during development. Set to false in production.
};

```

- Then we can use session in client side rendering, example for client side

```JS
//components/Navigation.tsx file

"use client"; //if we use usePathname
import { useSession, signIn, signOut } from "next-auth/react";

export function Navigation({ navLinks }: Props) {
  const pathname = usePathname(); // Using usePathname to get the current URL path where the user is currently located

  const session = useSession(); //it is from NextAuth library. we can use session data to show/hide links based on auth status. it will help to show is the user Authorized or not.
  console.log("Session data:", session); // Log session data to see its structure and contents

  .....some code

}
```

- If we use server side rendering

```JS
//app/profile.page.tsx

import { authConfig } from "@/config/auth";
import { getServerSession } from "next-auth/next";

export default async function Profile({}: Props) {
  const session = await getServerSession(authConfig); //it is from NextAuth library. we can use session data to show/hide links based on auth status. it will help to show is the user Authorized or not.
  //receive authConfig from config/auth.ts

  //some code
}

```

# Make your own form signIn

- Not always we will use already build-in form. Can can make our own form and style it as we want.
- Therefore, we need to create separate page for our own signIn Form --> /app/signin/page.tsx (it will be server component)
- and make separate components as button for SignIn form and Form to fill --> components/GoogleButton.tsx
- We change component/Navigation.tsx line 54

```JS
//from build-in signIn Form
<Link href={"/api/auth/signin"}>Sign In</Link>

//our page for Sign-in
<Link href={"/signin"}>Sign In</Link>
```

- Also, in config/auth.ts file we need to declere that we will use our own Sign-In page by changing our settings in there

```JS
export const authConfig: AuthOptions = {
  // AuthOptions is an interface/data type provided by next-auth to define the configuration options for authentication.
  // You can add authentication providers here, e.g., Google, Facebook, GitHub, LinkedIn, Twiter etc.
  providers: [
    Google({
      clientId: process.env.GOOGLE_CLIENT_ID!, //this values we get from Google Developer Console. from cloud.google.com
      clientSecret: process.env.GOOGLE_CLIENT_SECRET!, //this values we get from Google Developer Console. from cloud.google.com

      //   clientId: process.env.GOOGLE_CLIENT_ID as string, <-- can be writen as well this way
    }),
    Credentials({
      //credentials has at least 2 fields: credentials and authorize
      credentials: {//some credentials here, (build in sign in Form from NextAuth library)
      },
      async authorize(credentials) {
        //return data after build-in sign-In Form
        }
      },
    }),
  ],
  pages: {  //We add this object, to tell that we have our own sign in page
    signIn: '/signin'
  },
  debug: true, // Enable debug mode for detailed logging during development. Set to false in production.
};
```

- Then create component --> SignInForm.tsx (to make this component as client side component and import to signin page, which is server side component)
