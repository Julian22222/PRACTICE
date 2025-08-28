# Authentication for the Web.

Authentication for your application. We will use Auth.js library (before known as NextAuth library). Free and open source

[https://authjs.dev/](https://authjs.dev/)

- this library support not only Next.js but others too
- this library allows to create Login page, can use Google account to Login

1. Need to install

```JS
npm i next-auth
```

2. Auth.js works through special api routes, therefore we need to create new api route for authentication - app/api/auth/[dynamicRoute]/route.ts

- in our case - [...nextauth] <--dynamic route

3. Create config/auth.ts file <-- configuration for Authorization

- In onfig/auth.ts we have a function, export const authConfig: AuthOptions = {..} <- It Must contain different providers
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
http://localhost:3000/api/auth/signin
```

//7:03
