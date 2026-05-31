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
GET /users/me (credentials: include)
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
- JWT (access + refresh)
- HttpOnly cookies
- /me endpoint
- guards read cookies

//Frontend
- fetch(..., { credentials: 'include' })
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
├── jwt.strategy.ts   ← HERE (cookie extraction)
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

Then You HAVE to use this:

1. Best option - cleanest architecture (cleanest option to use )

- Since you're using NestJS JWT authentication.
This is actually one of the biggest advantages of JWT authentication.

- Instead of calling from Next.js fetching:
      -/users/me first and then
      -/accounts/user/:id/accounts-balance,
you could use the JWT payload directly in the Back-end.
Your Server Component in NEXT.JS doesn't need: userId, customer_id, or any user info in the fetch URL

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

////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

//Below options are not needed, NOT IMPORTANT BECAUSE localStorage can be used in NEXT.JS in Client component

# JWT and localStorage data

### You can get the data from localStorage in Next.js only from "use client"

❌ you cannot access localStorage in Next.js Server Components.

```JS
//only from "use client"
"use client";

const user = localStorage.getItem("user");

/////////////////////
// ❌ Server Components
// run on the server (Node.js)
// no browser APIs
// no window
// no localStorage

// So this will NOT work:
localStorage.getItem("user")
```

```JS
| Type             | Runs where | Can use localStorage? |
| ---------------- | ---------- | --------------------- |
| Server Component | server     | ❌ No                  |
| Client Component | browser    | ✅ Yes                 |
```

#### The flow of login process

```JS
//when user login in NEXT>JS -> the data comes to NEST.JS auth/auth.service.ts
//this is auth/auth.service.ts file

import { JwtService } from '@nestjs/jwt';
const bcrypt = require('bcrypt');

import { Inject, Injectable, UnauthorizedException } from '@nestjs/common';
import { PG_POOL } from '../database/database.module';
import { Pool } from 'pg';
import { UserResponseDto } from './dto/response-user.dto';
import { LoginDto } from './dto/login.dto';

@Injectable()
export class AuthService {
  constructor(
    @Inject(PG_POOL) private readonly pool: Pool,
    private readonly jwtService: JwtService,
  ) {}

  async login(loginData: LoginDto) {
    const { email, password } = loginData;

    const result = await this.pool.query(
      `SELECT * FROM customers WHERE email = $1`,
      [email],
    );

    const user = result.rows[0];

    if (!user) {
      throw new UnauthorizedException('Invalid email or password');
    }

    const isMatch = await bcrypt.compare(password, user.password);

    if (!isMatch) {
      throw new UnauthorizedException('Invalid email or password');
    }

    // 👇 JWT payload (keep it small!)
    const payload = {
      sub: user.customer_id,
      email: user.email,
      // user: user.role
    };

    // 🔐 Access Token
    const accessToken = this.jwtService.sign(payload, {
      secret: process.env.JWT_SECRET,
      expiresIn: '15m',
    });

    // 🔄 Refresh Token
    const refreshToken = this.jwtService.sign(payload, {
      secret: process.env.JWT_REFRESH_SECRET,
      expiresIn: '7d',
    });

    const userResponse: UserResponseDto = {
      //returning user data with no password
      customer_id: user.customer_id,
      first_name: user.first_name,
      last_name: user.last_name,
      email: user.email,
      phone: user.phone,
      customer_address: user.customer_address,
      dob: user.dob,
      created_at: user.created_at,
    };

    // Return response with both tokens and user data /
    //RETURNING THIS DATA WITH SUCCESSFUL LOGIN
    return {
      access_token: accessToken,
      refresh_token: refreshToken,
      userResponse,
    };
  }
}
```

```JS
//then in NEXT.JS you get this returned data and assign to localStorage

"use client";

import { useGlobal } from "../../Context";
import React from "react";
import { useRouter } from "next/navigation";
import { IAccount } from "../../../../../shared/types/account.interface";
import Link from "next/link";

interface Props {
  AllUsersAccounts: IAccount[];
}

export default function LoginForm({ AllUsersAccounts }: Props) {
  const { setActiveUser, setUserAccountType, setCurrUserAllAccounts } =
    useGlobal();

  const router = useRouter();

  const [formInput, setFormInput] = React.useState({ email: "", password: "" });
  const [loginError, setLoginError] = React.useState(false);

  const handleSubmit = async (e: React.FormEvent) => {
    e.preventDefault();

    try {
      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACK_END_URL}/auth/login`,
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(formInput),
        },
      );

      if (!res.ok) {
        throw new Error();
      }

      setLoginError(false);

      const user = await res.json();

      const { access_token } = user;

      // store token (simple version)
      localStorage.setItem("token", access_token);   /////////////////<----loacalStorage assign access_token -> to tokken
      localStorage.setItem("user", JSON.stringify(user));   /////////////////<----loacalStorage assign JSON.stringify(user) -> to user

      setActiveUser(user);

      const userAccounts = AllUsersAccounts.filter(
        (account: IAccount) =>
          account.customer_id === user.userResponse.customer_id,
      );

      setUserAccountType(userAccounts[0].account_type);
      setCurrUserAllAccounts(userAccounts);

      router.push("/user-page");
    } catch (error) {
      setLoginError(true);
    }
  };

  const userString = localStorage.getItem("user");   /////////////////<----loacalStorage get the data of "user"
  const test = userString ? JSON.parse(userString) : null;    /////////////////<----loacalStorage data assigning to get user id later

  console.log(localStorage.getItem("token"), "!!token in login form");  /////////////////<----loacalStorage get the assigned data from "token"
  console.log(localStorage.getItem("user"), "!!user in login form");   /////////////////<----loacalStorage get the assigned data from "user"
  console.log(test?.userResponse?.customer_id, "!!user in login form");   /////////////////<----loacalStorage getting the id of the user

  return (
    <main className="d-flex justify-content-center py-5">
      <div className="card shadow-sm p-4 w-100" style={{ maxWidth: "420px" }}>
        <h2 className="text-center text-success mb-4">
          Log on to your account
        </h2>

        <form onSubmit={handleSubmit}>
          {/* User ID */}
          <label className="form-label fw-bold">User ID</label>
          <input
            type="text"
            className="form-control mb-3"
            placeholder="Enter your User ID"
            value={formInput.email}
            onChange={(e) =>
              setFormInput({ ...formInput, email: e.target.value })
            }
          />

          {/* Password */}
          <label className="form-label fw-bold">Password</label>
          <input
            type="password"
            className="form-control mb-3"
            placeholder="Enter your Password"
            value={formInput.password}
            onChange={(e) =>
              setFormInput({ ...formInput, password: e.target.value })
            }
          />

          {/* Remember */}
          <div className="form-check mb-3">
            <input className="form-check-input" type="checkbox" id="remember" />
            <label className="form-check-label" htmlFor="remember">
              Remember my User ID
            </label>
          </div>

          {/* Submit */}
          <button
            className="btn w-100 fw-bold text-white"
            style={{ backgroundColor: "#006a4d" }}
          >
            Log on
          </button>
        </form>

        {/* Error */}
        {loginError && (
          <div className="text-danger text-center mt-3 fw-bold">
            Invalid User Email or Password
          </div>
        )}

        {/* Links */}
        <div className="text-center mt-3">
          <Link
            href="/registration"
            className="text-success fw-bold text-decoration-none"
          >
            Register for Internet Banking
          </Link>

          <div>
            <Link
              href="#"
              className="text-success fw-bold text-decoration-none"
            >
              Forgotten your login details?
            </Link>
          </div>
        </div>

        {/* Admin */}
        <button
          type="button"
          className="btn w-100 mt-3 fw-bold text-white"
          style={{ backgroundColor: "#a45d16" }}
          onClick={() => router.push("/admin-login")}
        >
          Login as Admin
        </button>

        {/* Divider */}
        <hr className="my-4" />

        {/* Security text */}
        <small className="text-muted">
          <strong>Security notice:</strong> We’ll never ask you to move money or
          share your login details. If you receive a suspicious message, contact
          us immediately.
        </small>
      </div>
    </main>
  );
}
```

```JS
auth/jwt.strategy.ts file

import { Injectable } from '@nestjs/common';
import { PassportStrategy } from '@nestjs/passport';
import { ExtractJwt, Strategy } from 'passport-jwt';

@Injectable()
export class JwtStrategy extends PassportStrategy(Strategy) {
  constructor() {
    super({
      jwtFromRequest: ExtractJwt.fromAuthHeaderAsBearerToken(),
      secretOrKey: process.env.JWT_SECRET || 'dev_secret',
    });
  }

  validate(payload: any) {
    return {
      userId: payload.sub,
      email: payload.email,
    };
  }
}
```

# BEST OPTION for a server component in NEXT.JS is to Use cookies instead of localStorage

❌ you CAN'T USE cookies in "use client"

Store JWT in httpOnly cookie from backend.

```JS
// Then in Server Components:

import { cookies } from "next/headers";

const token = cookies().get("token");
//✔ Works in server components
//✔ Secure (best practice)
//✔ No XSS risk

// This only works in:
// Server Components
// Route Handlers (app/api/...)
// Server Actions

// ❌ It does NOT work in:
// "use client" components
```

# 🚨 Important security insight

If you store JWT in:

❌ localStorage

- vulnerable to XSS attacks

⚠️ normal cookies

- readable by JS (less secure)

✅ HttpOnly cookies (best)

- safest
- used by banks, fintech apps

```JS
1. 🍪 HttpOnly cookies (BEST – secure)

Set by backend or server
- NOT accessible in JavaScript
- NOT accessible via document.cookie

// Used for auth (JWT)
// Example:
Set-Cookie: token=abc123; HttpOnly; Path=/

// Access:
// -Server Components ✔
// -API routes ✔
// -Middleware ✔
// -Client JS ❌

// 👉 This is the most secure option.
```
