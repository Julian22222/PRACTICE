# USE JWT , HttpOnly cookies and @Get('me') to GET user data from back-end without -> user_id

- JWT is ALWAYS required for /me
- You don't need to return any token from back-end as a response!!! Because with HttpOnly cookies, frontend should NEVER handle tokens.

```JS
//from service in NEST.JS
//❌ Don't need to return tokens with HttpOnly cookies

  return {
      access_token: accessToken,
      refresh_token: refreshToken,
      userResponse,
    };
```

```JS
// Real-world flow (recommended)

Login
  ↓
NestJS creates JWT
  ↓
JWT stored in HttpOnly cookie

Frontend requests:
  ↓
GET /users/me //   headers: { Cookie: cookieStore.toString()} cache: "no-store",
// In client component (credentials: include)
  ↓
NestJS reads cookie
  ↓
JWT validated
  ↓
Returns current user
```

```JS
✅ Best Practice Stack
1. JWT (authentication mechanism)
-This is the actual “proof” of login
-Contains user identity (sub, email, role)
-Signed by backend

👉 JWT = who the user is

2. HttpOnly Cookies (storage layer)
-Store JWT securely in browser
-Not accessible from JavaScript
-Automatically sent with requests

👉 Cookies = how the token is stored & sent

3. @Get('me') (user identity endpoint)
-Reads JWT from request
-Returns current user data
-No need for frontend to pass user ID

👉 /me = who is logged in right now?
```

@Get('me') uses JWT on the backend

```JS
//Correct architecture
//Backend (NestJS)

@Get('me')
@UseGuards(JwtAuthGuard)
getCurrentUser(@Req() req) {
  return this.usersService.findOne(req.user.sub);
}


//This works only if:
//- request includes JWT token
//- JWT is validated by backend

Flow:
Cookie → JWT Guard → req.user → DB → response
```

```JS
//Backend sets:
res.cookie('access_token', token, {
  httpOnly: true,
  secure: true,
  sameSite: 'lax',
});
//Browser handles everything automatically.


//Frontend:
await fetch('/users/me', {
  credentials: 'include',
});

//Works with JWT
// ✔ yes
// ✔ more secure
// ✔ no token handling in JS


//⚠️ Important note
//You still use JWT, just not manually in frontend.

//JWT is still:
// -created by backend
// -verified by backend
// -used in guards
```

```JS
// src/auth/jwt.strategy.ts file

import { Injectable } from '@nestjs/common';
import { PassportStrategy } from '@nestjs/passport';
import { ExtractJwt, Strategy } from 'passport-jwt';

@Injectable()
export class JwtStrategy extends PassportStrategy(Strategy) {    //<-- This tells NestJS:"Use JWT authentication."
  constructor() {
    super({
      jwtFromRequest: ExtractJwt.fromExtractors([   //<--This tells Passport: "Find the JWT inside the access_token cookie." Example request: Cookie: access_token=eyJhbGciOi...
        (req) => {
          return req?.cookies?.access_token;
        },
      ]),
      secretOrKey: process.env.JWT_SECRET,   //Uses the same secret that was used when creating the access token:
    });
  }

  async validate(payload: any) {   //validate()
    return payload; // this becomes req.user
  }
}

///////////////////////////////////////////////////////
What happens when someone calls /auth/me

// // Request:
// GET /auth/me
// Cookie: access_token=eyJhbGciOi...
            ↓
//JwtStrategy extracts:
// req.cookies.access_token
            ↓
//Passport verifies the JWT using:
// secretOrKey: process.env.JWT_SECRET
            ↓
//validate() runs:
// async validate(payload: any) {
//   return payload;
// }
            ↓
//NestJS sets:
// req.user = payload;

// If your JWT payload was:
// const payload = {
//   sub: user.customer_id,
//   email: user.email,
// };

//then later:
//req.user

//will contain:
// {
//   "sub": 1,
//   "email": "john@gmail.com",
//   "iat": 1710000000,
//   "exp": 1710000900
// }
```

```JS
//src/auth/authservice.ts

async refreshToken(req: any, res: Response) {
  try {
    const token = req.cookies.refresh_token;

    if (!token) {
      throw new UnauthorizedException('No refresh token');
    }

    const payload = this.jwtService.verify(token, {
      secret: process.env.JWT_REFRESH_SECRET,
    });

    const newAccessToken = this.jwtService.sign(
      {
        sub: payload.sub,
        email: payload.email,
      },
      {
        secret: process.env.JWT_SECRET,
        expiresIn: '15m',
      },
    );

    res.cookie('access_token', newAccessToken, {
      httpOnly: true,
      secure: false,
      sameSite: 'lax',
      maxAge: 15 * 60 * 1000,
    });

    return { message: 'token refreshed' };
  } catch {
    throw new UnauthorizedException('Invalid refresh token');
  }
}
```

```JS
main.ts file
//NestJS cannot read cookies by default.

// npm install cookie-parser  <- in NEST.JS folder


import * as cookieParser from 'cookie-parser';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);

  app.use(cookieParser());

  await app.listen(3000);
}
bootstrap();
```

```JS
🔥 FINAL BEST PRACTICE ARCHITECTURE

// Backend:
- JWT (access + refresh tokens)
- HttpOnly cookies
- /me endpoint
- guards read cookies

//Frontend
- fetch(..., {
      headers: {
        Cookie: cookieStore.toString(),
      },
      cache: "no-store",
    },) //in server component
- apiFetch() optional (only for retry logic)
- no token storage at all


//🧭 Mental model
Login → server sets cookies
Frontend → never sees tokens
/me → backend reads cookie → returns user
Refresh → backend updates cookie
```

```JS
🔁 How everything works together

// Step 1: login
NestJS sets cookie:
access_token=eyJ...

//Step 2: request /me
Browser automatically sends cookie

//Step 3: JwtStrategy runs
req.cookies.access_token
//gets token

//Step 4: validate()
req.user = payload

//Step 5: controller
req.user.sub
```

```JS
//🧭 Where each file goes

src/auth/
├── jwt.strategy.ts   //← HERE (cookie extraction) and avalidation
├── jwt-auth.guard.ts
├── auth.service.ts
├── auth.controller.ts

//💡 Simple mental model
jwt.strategy.ts = HOW to read token
jwt.guard.ts    = WHEN to protect routes
/me endpoint    = WHAT user is logged in
```

# Important

```JS

- You need to keep page.tsx in NEXT.JS --> as server component ("use server") for website PERFORMANCE, if you need to make a fetch with user_id from the page.tsx.

For example:  await fetch(
      `${process.env.NEXT_PUBLIC_BACK_END_URL}/accounts/user/${customer_id}/accounts-balance`, //<-- if you need to use customer_id in the request to Back-End
      {
        cache: "no-store",
        next: { tags: ["thisUser-WithBalance"] },
      },
    );

⚠️ Then You HAVE to use this:

1. Best option - cleanest architecture (cleanest option to use )

- Since you're using NestJS JWT authentication.
This is actually one of the biggest advantages of JWT authentication.

- Instead of calling from Next.js fetching:
      -/users/transactions/:id
      -/accounts/user/:id/accounts-balance,
you could use the JWT payload directly in the Back-end.
Your Server Component in NEXT.JS doesn't need: LogedIn userId, LogedIn customer_id, or any LogedIn user info in the fetch URL

- Use JWT userData from payload: //can assign any data from user data to payload
 const payload = {
      sub: user.customer_id,
      email: user.email,
      // user: user.role
    };

//The backend gets the user ID from: req.user.sub (after validating the JWT and extracting the payload) and then uses that to fetch the correct accounts for that user.
- Then in NEST.JS controller put:

// @Get('my-accounts')     /<-- No need custcustomer ID, to make a request for certain user, custcustomer ID comes from JWT payload
// @UseGuards(JwtAuthGuard)
// getMyAccounts(@Req() req) {
//   return this.usersService.findOne(req.user.sub);   //<-- req.user.sub - this is custcustomer ID from JWT payload
// }

- then call this method from NEXT.JS
//GET /accounts/my-accounts     //<-- No custcustomer ID needed at all in the route

//NEXT.JS component
const cookieStore = await cookies();

 const response = await fetch(
    `${process.env.NEXT_PUBLIC_BACK_END_URL}/accounts/my-accounts-balance`,
    {
      headers: {
        Cookie: cookieStore.toString(),  //<--needs to include this, forward the cookie from Browser to Next.js Server Component
      },
      cache: "no-store",
    },
  );

//That's typically cleaner and more secure because the client never chooses which customer ID to request. The backend derives it from the authenticated JWT.





2. Works ONLY In a Client Component

This option works in the browser, because Server Components don't have access to the browser's cookies automatically.

This option effectively only useful for browser/client-side usage.

// fetch(`${process.env.NEXT_PUBLIC_BACK_END_URL}/users/me`, {
//   credentials: "include",
// });

// - credentials: "include" tells the browser:"Send my cookies with this request."
// - When this code runs in a Client Component, the browser has access to the user's cookies and sends them.

export const loadUserClient = async () => {
  const res = await fetch(
    `${process.env.NEXT_PUBLIC_BACK_END_URL}/users/me`,
    {
      credentials: "include",  //credentials: "include" only works in the browser.
    },
  );

  return res.json();
};

3. Works ONLY In a Server Component

// - The code runs on the Next.js server, not in the user's browser.
// - There is no browser cookie jar attached to that fetch.

// So:
// credentials: "include"
// doesn't magically send the user's cookies to NestJS.

// That's why in Server component /users/me returns:

// {
// "message": "Unauthorized",
// "statusCode": 401
// }

// to make it to work in Server component -> Use cookies() from Next.js:

import { cookies } from "next/headers";

export const loadUserServer = async () => {
  const cookieStore = await cookies();

  const res = await fetch(
    `${process.env.NEXT_PUBLIC_BACK_END_URL}/users/me`,
    {
      headers: {
        Cookie: cookieStore.toString(),
      },
      cache: "no-store",
    },
  );

  return res.json();
};

```
