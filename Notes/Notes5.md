# 🔥 Authentication Method Comparison

1. Password Hashing (Manual)

- The Logic: You capture a plain-text password, use a library like bcrypt to salt and hash it, and store only the hash in your database.
- Verification: During login, you hash the input and compare it to the stored hash using bcrypt.compare().
- Next.js/Nest.js Role: Nest.js handles the hashing logic in a service, while Next.js manages the login form.

2. JWT Authentication (The Standard)

- The Logic: After a successful password check, Nest.js issues a signed token (JWT).
- Statelessness: The server doesn't "remember" you; it just verifies the signature on each incoming request.
- Next.js Integration: Tokens are often sent in the Authorization header (Bearer <token>) from your Next.js frontend to protected Nest.js endpoints.

3. Managed Authentication (Auth0)

- The Logic: Outsources the entire identity flow (login page, user storage, social logins) to Auth0.
- Speed: Eliminates the need to build "Forgot Password" or "MFA" flows manually.
- Workflow: Next.js redirects to Auth0 for login; Auth0 returns a JWT that your Nest.js backend validates using a dedicated Passport strategy

```JS
✅1: Password Hashing Authentication
    -Store hashed passwords (bcrypt / argon2)
    -Server-managed sessions
    -Requires login endpoint
    -Secure if implemented properly
    -Manual scaling & security responsibility

✅2: JWT Authentication
    -Stateless authentication
    -Token stored on client (cookies/localStorage)
    -Fast & scalable
    -Requires token validation middleware
    -Risk if tokens are exposed

✅3: Managed Authentication (Auth0)
    -Third-party identity provider
    -Supports OAuth, social login, SSO
    -Built-in security features
    -Minimal backend auth logic
    -Vendor dependency & cost
```

# 🔥 How to Store passwords

❌ DON'T store passwords as plain text in Database

## ✅ USE HASH password

```JS
//Example:
//Use bcrypt:

//npm install bcrypt
//npm install -D @types/bcrypt

const bcrypt = require('bcrypt');

const password = "mySecret123"

const hashedPassword = await bcrypt.hash(password, 10);
//bcrypt.hash(...) → converts it into a hashed (encrypted) version that you store in your database
//await → because hashing is asynchronous (it takes time)

// So instead of storing "mySecret123", you store something like:
// $2b$10$K8vJ9... (long hashed string)

// bcrypt.hash(password, 10); -->10 means = The 10 is called the salt rounds (also known as cost factor). It controls how strong and slow the hashing process is. 10 - is default number and commonly used.
// 10 is a good balance between security and performance, which is why many frameworks and examples use it as default.
//Higher number: ✅ More secure (harder to brute-force), ❌ Slower to generate hash
//Lower number: ❌ Less secure, ✅ Faster hashing
```

# 🔥 JWT

JWT is Useful and helps you:

- keep users logged in
- protect routes
- identify users securely
- build authentication systems for APIs

✅ 1. Install needed dependencies for your Back-end (Nest.JS) - in Nest.js folder

```JS
npm install @nestjs/jwt @nestjs/passport passport passport-jwt
npm install -D @types/passport-jwt
```

✅ 2. Add JWT module in NestJS

```JS
//create separate folder src/auth and create file ->
//auth.module.ts

import { Module } from '@nestjs/common'; //<--Imports the Module decorator from NestJS. Then we can use Module in this file
import { JwtModule } from '@nestjs/jwt';  //<-- Imports NestJS JWT support. JwtModule helps - create JWT tokens, verify JWT tokens, manage authentication. JWT = JSON Web Token.
import { PassportModule } from '@nestjs/passport';  //<-- Imports Passport integration for NestJS.Passport is a popular authentication library.It helps with: login systems, JWT authentication, guards/strategies

@Module({   //module configuration <--is used to create a NestJS module. A module helps organize related code together. @Module() it is an object with settings
  imports: [   //<--imports means: “Which modules does this module need?” -  PassportModule,
    PassportModule,
    JwtModule.register({   //<-- JwtModule.register - Configures the JWT module. /// .register() means: “Set up JWT settings.”
      secret: process.env.JWT_SECRET || 'dev_secret',  //<- Defines the secret key used to sign JWT tokens.JWT tokens are encrypted/signed using this secret. If no environment variable exists -use 'dev_secret' as fallback./ //// 'dev_secret' <-- Never use this in production, Use a strong secret in .env
      signOptions: { expiresIn: '1h' },  //access tokken will expire after 1h. The JWT token becomes invalid after 1 hour. Examples: '1h' → 1 hour, '7d' → 7 days, '15m' → 15 minutes
    }),
  ],
  exports: [JwtModule],   //<-- Makes JwtModule available to other modules. Without this line: other modules cannot use JWT features from this module.
})
export class AuthModule {}  //<--Creates and exports the module class.Other modules can now import AuthModule

//////////////////////////////////////////////////////////////////////////////////
📍// This module:
//     -enables JWT authentication
//     -enables Passport authentication
//     -configures JWT secret and expiration
//     -shares JWT functionality with other modules
////////////////////////////////////////////////////////////////////////////////////////////////////

✅ //Example .env file
//This is the secret key used to create and verify JWT tokens.Think of it like a private password that only your server knows.
👉//Why JWT Needs a Secret-> When the server creates a JWT token, it signs the token using the secret.
// using JWT secret -> server can detect fake tokens, modified tokens, hackers editing payload data
//When creating token: jwtService.sign(payload) <--NestJS internally uses: payload + secret (to generate a cryptographic signature.)
//Example Token Structure: JWT looks like -> xxxxx.yyyyy.zzzzz  ///// Token parts: Header, Payload, Signature.
//The signature is generated using: payload data and your secret
JWT_SECRET=my_super_secret_key


📍//Example:
// User logs in
//    ↓
// Server creates token
//    ↓
// Token signed with secret key
//    ↓
// Token sent to client


// Later, when the client sends the token back:
// Client sends JWT
//    ↓
// Server checks signature using same secret
//    ↓
// If valid → user is trusted
// If invalid → reject request


📍// If someone knows your JWT secret:
// -they can create fake valid tokens
// -pretend to be any user
// -bypass authentication

🔥// So your secret must be:
// -long
// -random
// -private

🔥// Good Secret Example:
// JWT_SECRET=8fK2!xPq9Lm#Zr7@uYw3VbN1

❌// Bad Secret Examples:
// JWT_SECRET=123456
// JWT_SECRET=password
// JWT_SECRET=secret
```

✅ 3. Update your login service (Nest.JS)

```JS
import { JwtService } from '@nestjs/jwt';
import { Injectable, UnauthorizedException } from '@nestjs/common';
import * as bcrypt from 'bcrypt';

@Injectable()
export class UsersService {
  constructor(
    private readonly jwtService: JwtService,
    private readonly pool: any,
  ) {}


 //// 🔐 Modify login to return token

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


//Once Password is matching you can create JWT payload
  // 👇 JWT payload (keep it small!) <-- you can get access to this data from anywhere -> sub and email
  //The payload is the data stored inside the JWT token. Also JWT contains extra JWT metadata like: expiration time, issued time.
  //after login, this information becomes accessible: in Next.js, in browser cookies, in localStorage/sessionStorage (if you store token there), after decoding the token
  //JWT Payload Is NOT Secret, Anyone who has the token can decode and read the payload.
  ❌//NEVER store: password, credit card, sensitive private data
  const payload = {
    sub: user.customer_id,  //sub means: subject. It is a standard JWT field representing: “Who owns this token?” - Usually:user id, customer id, account id
    email: user.email,
    role: user.role,
  };
  //Then NEXT.JS can know: is admin?, which user logged in?, what UI to show?

  const token = this.jwtService.sign(payload);

  return {
    access_token: token,   //<-- return tokken and user's data
    user: {
      customer_id: user.customer_id,
      first_name: user.first_name,
      last_name: user.last_name,
      email: user.email,
      phone: user.phone,
      customer_address: user.customer_address,
      dob: user.dob,
      created_at: user.created_at,
    },
  };
}

///////////////////
👉 // Why Payload Is Useful:
//   Frontend often needs:
//         -current user id
//         -email
//         -role
//         -permissions
// without making another database request.

```

✅ 4. Update Next.js LoginForm

```JS
//Now your backend returns:
{
  access_token,
  user
}
```

```JS
//Update your frontend:
try {
      const res = await fetch(
        `${process.env.NEXT_PUBLIC_BACK_END_URL}/users/login`,
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
      localStorage.setItem("token", access_token);

      setActiveUser(user);



const data = await res.json();
const { access_token, user } = data;  //destructurisation of the response from Back-end

// store token (simple version)
localStorage.setItem("token", access_token);  //<-- assign to localStorage to be able to use it

setActiveUser(user);
}
```

✅ 5. Send JWT in future requests

```JS
//Whenever you call protected routes: (routes thta only loged in users can access)

const token = localStorage.getItem("token");

await fetch("/api/some-protected-route", {
  headers: {
    Authorization: `Bearer ${token}`,
  },
});
```

# 🧩 Can Next.js decode payload directly?

```JS
//YES.
npm install jwt-decode


// Client component ("use client")
import { jwtDecode } from "jwt-decode";

const token = localStorage.getItem("token");

if (token) {
  const decoded = jwtDecode(token);

  console.log(decoded);
}

//⚠️ But decoding ≠ validating
//Anyone can decode JWT.
//Only backend can truly verify it using secret key.
```

🧩 Even if you think you don’t have protected routes, most apps eventually have them, like:

- /users/profile
- /accounts
- /transactions
- /admin/\*

Those almost always require authentication.

```JS
1. Login (once)
// You send:
// {
//   "email": "test@email.com",
//   "password": "123"
// }

//Backend responds:
//{
//   "access_token": "eyJhbGciOiJIUzI1NiIs..."
// }

2. Store the token (frontend)

// In your Next.js app:
//localStorage.setItem("token", access_token);

3. Future request
//When you call ANY protected API, you must include the token:
//❌ Without JWT (server will reject you)
//fetch("/users/profile");

//✅ With JWT (server recognizes you)
// const token = localStorage.getItem("token");

// fetch("/users/profile", {
//   headers: {
//     Authorization: `Bearer ${token}`,
//   },
// });
```

✅ 6. Add JWT validation (Guard)

if no guard -> anyone can --> GET /users/profile (without login)

👉 JWT Guard is what blocks that.

🧠 So JwtStrategy = “How to read and validate JWT”

```JS
//Create:
jwt.strategy.ts


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

✅ 7. Protect routes with Guard

🧩 What is a Guard?

A Guard is like a security gatekeeper: “Should this request be allowed through?”

```JS
//use @UseGuards(AuthGuard('jwt')) in controllers, but only on routes you want to protect
//You put it directly above a controller route (or controller class).

import { UseGuards } from '@nestjs/common';
import { AuthGuard } from '@nestjs/passport';

//“Only allow this endpoint if the user sends a valid JWT token.”
//If no token or invalid token → ❌ request is blocked (401 Unauthorized)
@UseGuards(AuthGuard('jwt'))
@Get('profile')
getProfile(@Request() req) {
  return req.user;
}
```

❌ DO NOT protect:

- login
- register
- public products
- public pages

✅ DO protect:

- profile
- accounts
- transactions
- admin routes
- user-specific data

```JS
///When someone calls:
// GET /users/profile
// Authorization: Bearer valid_token

Step-by-step:
✔ Step 1: Guard runs

AuthGuard('jwt') triggers JwtStrategy

✔ Step 2: Token is checked
valid → continue
invalid → ❌ 401 Unauthorized
✔ Step 3: Request continues

If valid:
req.user = decodedJWT;

✔ Step 4: Controller runs
return req.user;

//////🚨 If token is missing or wrong. NestJS automatically returns:
//{
//   "statusCode": 401,
//   "message": "Unauthorized"
// }

// Controller NEVER runs.
```

🔥 Important improvements (recommended)
❌ Don’t store JWT in localStorage (better option)

Use:

- httpOnly cookies (best practice)

```JS
Example JWT Flow:
1.User logs in
2.Server checks email/password
3.Server creates JWT token
4.Token returned to frontend
5.Frontend sends token in requests
6.Backend verifies token

//////////////////////////////////
🔁 Full flow

1. Login

✔ get JWT

2. Request protected route
Authorization: Bearer token

3. Guard checks token

✔ valid → allow
❌ invalid → block

4. Controller runs only if valid
```

# real authentication systems there are usually two tokens

1. Access Token
2. Refresh Token

Access token is used to access protected APIs.

- valid → allow request
- expired → reject request

```JS
// Problem Without Refresh Token
// If access token expires after 1 hour:
expiresIn: '1h'
// then user must:
// -login again every hour
// Bad user experience.
```

Solution → Refresh Token

```JS
Login
 ├── Access Token (short life)
 └── Refresh Token (long life)

// Example:
// -access token → 15 minutes
// -refresh token → 7 days
```

Real Authentication Flow

```JS
📍// Step 1 — Login
// User logs in:
// POST /login

// Server returns:
// {
//   "access_token": "...",
//   "refresh_token": "..."
// }
```

```JS
📍//Step 2 — Use Access Token
// Frontend sends access token:
// Authorization: Bearer access_token
```

```JS
📍// Step 3 — Access Token Expires
// Server returns:
// 401 Unauthorized
// because token expired.
```

```JS
📍//Step 4 — Use Refresh Token
// Frontend automatically sends refresh token:
// POST /refresh

// Server checks refresh token.
// If valid:
// -creates NEW access token
// -user stays logged in
```

```JS
//Step 5 — New Access Token
// Server returns:
// {
//   "access_token": "new_token"
// }
// User continues normally.
```

```JS
//access the data

//⚡ Final important distinction
| Location       | Access payload how?               |
| -------------- | --------------------------------- |
| NestJS backend | `req.user`                        |
| Next.js client | decode token OR use state/context |
| Next.js server | cookies/session usually           |

///////////////////////////////////

🚨 Important for Next.js
"use client"

// Can access:
// -localStorage
// -browser APIs
// -JWT from localStorage


"use server"
// Cannot access:
// -localStorage
// -browser state

Because it runs on server.

/////////////////////////////////////////////

// 🧠 Quick comparison
| Feature              | use client | use server     |
| -------------------- | ---------- | -------------- |
| localStorage         | ✅          | ❌              |
| decode JWT           | ✅          | ✅              |
| browser token access | ✅          | ❌              |
| secure validation    | ❌          | ✅ backend only |

```

```JS
| Token         | Purpose                  | Lifetime |
| ------------- | ------------------------ | -------- |
| Access Token  | Access APIs              | Short    |
| Refresh Token | Create new access tokens | Long     |
```

# Where Refresh Token Is Created

Usually inside login service too.

```JS
//Example
const accessToken = this.jwtService.sign(payload, {
  secret: process.env.JWT_SECRET,
  expiresIn: '15m',
});

const refreshToken = this.jwtService.sign(payload, {
  secret: process.env.JWT_REFRESH_SECRET,
  expiresIn: '7d',
});

//Notice:
// different secret
// longer expiration
```

### Why Different Secret?

Security.

If access token secret leaks:

- refresh tokens still protected.

### Typical Login Response

```JS
return {
  access_token: accessToken,
  refresh_token: refreshToken,
  user: {
    customer_id: user.customer_id,
    email: user.email,
  },
};
```

# Where Refresh Token Is Stored

```JS
Usually:
-HttpOnly cookie (BEST)
- NOT localStorage

Why?
-safer against XSS attacks
```

### Access Token Storage

```JS
Common options:
-memory
-cookie
-sometimes localStorage
```

# Refresh Endpoint Example

```JS
//Example NestJS route

@Post('refresh')
refresh(@Body() body) {
  return this.authService.refreshToken(body.refresh_token);
}
```

Refresh Service Example

```JS
async refreshToken(refreshToken: string) {
  try {
    const payload = this.jwtService.verify(refreshToken, {
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

    return {
      access_token: newAccessToken,
    };
  } catch {
    throw new UnauthorizedException();
  }
}
```

# Important Security Practice

Many real apps also:

- save refresh token in database
- hash refresh token
- revoke tokens on logout

This prevents stolen refresh tokens from being reused.

```JS
// Access Token -> expiresIn: '15m'
// Used for:
// -protected APIs
// -authorization
// Short lifetime for security.

// Refresh Token
// expiresIn: '7d'
// Used to:
// -create new access tokens
// -keep user logged in

// usually for refresh tokken you create new endpoint. -> /refresh
// POST /refresh
//Frontend sends refresh token → backend returns new access token.
```

```JS
| Token         | Recommended Storage |
| ------------- | ------------------- |
| Access Token  | memory or cookie    |
| Refresh Token | HttpOnly cookie     |
```

```JS
Why /refresh Route Is Needed
Your access token expires quickly: expiresIn: '15m'

After 15 minutes: -> 401 Unauthorized
Without /refresh: user must login again
With /refresh: frontend sends refresh token, backend creates new access token, user stays logged in
```

```JS
LOGIN
  ↓
Receive:
- access_token
- refresh_token
  ↓
Use access token for API calls
  ↓
Access token expires
  ↓
POST /refresh
  ↓
Receive new access token
  ↓
Continue normally
```

```JS
//Example Controller Route
//Usually in your controller:

@Post('refresh')
refreshToken(@Body() body: any) {
  return this.usersService.refreshToken(
    body.refresh_token,
  );
}

//Example Frontend Request (Next.js)
await fetch('http://localhost:3000/refresh', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify({
    refresh_token,
  }),
});

//Example Service Method
//Inside UsersService:
async refreshToken(refreshToken: string) {
  try {
    // Verify refresh token
    const payload = this.jwtService.verify(
      refreshToken,
      {
        secret: process.env.JWT_REFRESH_SECRET,
      },
    );

    // Create new access token
    const newAccessToken = this.jwtService.sign(
      {
        sub: payload.sub,
        email: payload.email,
        role: payload.role,
      },
      {
        secret: process.env.JWT_SECRET,
        expiresIn: '15m',
      },
    );

    return {
      access_token: newAccessToken,
    };
  } catch (error) {
    throw new UnauthorizedException(
      'Invalid refresh token',
    );
  }
}
```

# Important Difference

```JS
//Login Route
POST /login
// Purpose:
// -check email/password
// -create BOTH tokens

// Refresh Route
POST /refresh
// Purpose:
// -verify refresh token
// -create NEW access token ONLY
// No password needed.

Very Important Security Note
In production:

-refresh token should usually be stored in HttpOnly cookie
-not localStorage

And often:
-saved in database
-hashed
-revoked on logout
```

# Where and when use REFRESH

```JS
// Example Frontend Request (Next.js)

await fetch('http://localhost:3000/refresh', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify({
    refresh_token,
  }),
});
```

- You use this request when the access token expires.
- Usually automatically in Next.js frontend.

### When /refresh Is Called

```JS
User logs in
   ↓
Frontend stores:
- access_token
- refresh_token

   ↓
User uses app normally

   ↓
Access token expires (15m)

   ↓
API request fails with 401

   ↓
Frontend calls /refresh

   ↓
Backend returns new access token

   ↓
Retry original request
```

### Most Common Place To Use It

🔥 Usually inside:

- API helper
- fetch wrapper
- axios interceptor

❌ NOT directly inside every component.

```JS
//Example Scenario

// You call protected API:
GET /profile
//with expired access token.

// Backend returns:
401 Unauthorized

//Then frontend automatically does:
await fetch('http://localhost:3000/refresh', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify({
    refresh_token,
  }),
});

//to get new access token.
```

### Typical Next.js Structure

```JS
//Example:

src/
├── app/
├── components/
├── lib/
│   └── apiFetch.ts
└── services/

////////////////////////////////////////
//Example api.ts
//This is where refresh logic usually lives


//This is where refresh logic usually lives.
// For refresh tokken and access tokken generation and validation.

//Then Use It Everywhere, For example for profile route -> apiFetch('/profile')
//apiFetch('/profile') <-- this function - attach access token automatically, detect 401 Unauthorized, call /refresh, retry request.
//thi s will automatically add the access token to the header and retry once if the access token is expired.

export async function apiFetch(
  url: string,
  options: RequestInit = {},
) {
  let accessToken =
    localStorage.getItem('access_token');

  // First request
  let response = await fetch(url, {
    ...options,
    headers: {
      ...options.headers,
      Authorization: `Bearer ${accessToken}`,
    },
  });

  // If token expired
  // Access token expired
  if (response.status === 401) {
    const refreshToken =
      localStorage.getItem('refresh_token');

    // Request new access token
    const refreshResponse = await fetch(
      'http://localhost:3000/refresh',
      {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify({
          refresh_token: refreshToken,
        }),
      },
    );

    // Get new token
    const refreshData =
      await refreshResponse.json();

    accessToken =
      refreshData.access_token;

    // Save new access token
    localStorage.setItem(
      'access_token',
      accessToken,
    );

    // Retry original request
    response = await fetch(url, {
      ...options,
      headers: {
        ...options.headers,
        Authorization: `Bearer ${accessToken}`,
      },
    });
  }

  return response;
}
```

Then Use It Everywhere (use this logic everywhere)

```JS
❌ // Instead of doing:
fetch('/profile')

//////////////////////////
// you use:
apiFetch('/profile')  //<-- for example with profile endpoint, the same with other endpoints
```

# 🔥 Where you USE apiFetch('/profile')

🔥 You use it in React components or hooks.

```JS
//Example: React Page
//Basically you make data fetch as normal but you use a function -> apiFetch('RouteToBackEndData')

import { useEffect, useState } from 'react';
import { apiFetch } from '@/lib/apiFetch';

export default function ProfilePage() {
  const [user, setUser] = useState(null);

  useEffect(() => {
    async function loadProfile() {
      const res = await apiFetch('/users/1');

      const data = await res.json();

      setUser(data);
    }

    loadProfile();
  }, []);

  if (!user) return <p>Loading...</p>;

  return (
    <div>
      <h1>{user.first_name}</h1>
      <p>{user.email}</p>
    </div>
  );
}

//////////////////////////////////////
// Without apiFetch
// Every component would need to:
// get token
// add Authorization header
// detect 401
// call refresh
// save new token
// retry request

// That becomes repetitive.

////////////////////////////////////////////////////////////////////
// What happens step by step

Component loads
   ↓
apiFetch('/users/1')
   ↓
Sends request with access token
   ↓
Backend checks token
   ↓
If valid → returns data
If expired → 401
   ↓
apiFetch calls /refresh
   ↓
Gets new access token
   ↓
Retries original request
   ↓
UI gets data

//////////////////////////////////////////////
// Another Example
// Create/register a user:

await apiFetch('/users', {
  method: 'POST',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify(formData),
});


//Another Example
//Update profile:
await apiFetch('/users/1', {
  method: 'PATCH',
  headers: {
    'Content-Type': 'application/json',
  },
  body: JSON.stringify(data),
});

///////////
// Think of apiFetch as a Wrapper

Component
    ↓
apiFetch()
    ↓
Adds JWT automatically
    ↓
Calls NestJS API
    ↓
Refreshes token if needed
    ↓
Returns response
```

# ❌ Where you should NOT use apiFetch

- ❌ inside NestJS (backend)
- ❌ inside services in backend
- ❌ inside controllers

It is ONLY for frontend (Next.js / React).

#### Why This Is Powerful

- User never notices token expiration.
- Everything refreshes automatically.

### Better Production Approach

Instead of localStorage:

- store refresh token in HttpOnly cookie

Then browser sends it automatically.

More secure.

### Simple Beginner Mental Model

```JS
Access token expired?
        ↓
Frontend silently calls /refresh
        ↓
Gets new access token
        ↓
Retries failed request
```

# Better pattern (real apps)

Instead of calling /profile directly, you usually do:

```JS
apiFetch('/users/me')

//Why?
//Because backend already knows user from JWT:
req.user.sub
//So no need to send user id.
```

# Clean architecture summary

```JS
// Backend (NestJS)
- /users
- /auth/login
- /auth/refresh
//can put user registration -> to auth folder or can be in users folder
//change password doesn't apply to "auth" therefore change password methods can be in "users" folder/- using in users.controller.ts and users.service.ts files

//Frontend (Next.js)
- apiFetch()
- components call API
- refresh handled automatically

/////////////////////////////////////////////////////

// Simple Mental Model

React component
   ↓
apiFetch('/users/me')
   ↓
handles token + refresh automatically
   ↓
returns data
```

# ✨✨✨✨✨✨ For authentication tokens, cookies are generally considered more secure than localStorage

Keep userData in NEXT.JS is better in HttpOnly cookies but NOT in useState

Storing non-sensitive user data in localStorage is usually fine:

```JS
{
  "customer_id": 1,
  "first_name": "John",
  "email": "john@gmail.com"
}

//storing data in localStorage is not considered best practice for production applications
// GOOD PRACTICE is to use HttpOnly cookies
```

```JS
Better Approach
Use HttpOnly cookies.
Example login response:

Set-Cookie: access_token=...
Set-Cookie: refresh_token=...

with flags like:
HttpOnly
Secure
SameSite=Lax
//or
SameSite=Strict
```

# Why HttpOnly Cookies Are Better

JavaScript cannot read them.

```JS
//This fails:
document.cookie
//for HttpOnly cookies.

//This also fails:
localStorage.getItem('access_token')
//because the token isn't in localStorage.

So if malicious JavaScript runs on your page, it cannot directly steal the token.
```

## Typical Modern Setup

```JS
//➡️ Backend (NestJS)

//Login:
POST /auth/login

// it returns:
Set-Cookie:
- access_token
- refresh_token

//instead of:
{
  "access_token": "...",
  "refresh_token": "..."
}

/////////////////////////

//➡️ Frontend (Next.js)

// You don't store tokens manually.
// Requests simply include:

fetch('/users/me', {
  credentials: 'include',
});
//The browser automatically sends the cookies.
```

### What Should Be Stored Where?

```JS
// Good in Cookies
access_token
refresh_token

//Okay in localStorage
theme
language
sidebar state

//It is better to don't use user's data in React State / Context, better to keep it in HttpOnly cookie
user.first_name
user.email
user.role

//Avoid:
localStorage.setItem(
  'access_token',
  token
);
//especially for refresh tokens.
```

```JS
// A common production architecture looks like:

NestJS Login
    ↓
Sets HttpOnly Cookies
    ↓
Next.js calls API
    ↓
Browser automatically sends cookies
    ↓
NestJS validates JWT
    ↓
Returns user data
```

//////////////////////////////////////////////////////////////////////////////////////////////////////

```JS
//auth.service.ts file

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

    // Return response with both tokens and user data
    return {
      access_token: accessToken,
      refresh_token: refreshToken,
      userResponse,
    };
  }
}

//here we return
// {
//   "access_token": "...",
// refresh_token:"...",
//   "userResponse": {
//     "customer_id": 1,
//     "first_name": "John",
//     "email": "john@gmail.com"
//   }
// }

// and save in NExt.js Login page:
// localStorage.setItem(
//   'user',
//   JSON.stringify(user)
// );

//This works, but the data can become stale.
// Example:
// User logs in
//  ↓
// localStorage stores email
//  ↓
// Admin changes email in database
//  ↓
// localStorage still has old email
```

# Better Approach

Store only authentication information (JWT cookie).

Then ask the backend:

```JS
GET /users/me
//when you need the current user.
```

```JS
//Example Flow

//Login
POST /auth/login

//Backend:
Set-Cookie: access_token=...

//Frontend:
No need to save user object

//Get Current User
const response = await fetch(
  'http://localhost:3000/users/me',
  {
    credentials: 'include',
  }
);

const user = await response.json();


//Backend:
{
  "customer_id": 1,
  "first_name": "John",
  "email": "john@gmail.com"
}
```

### How Does NestJS Know Who I Am?

Remember your JWT payload:

```JS
const payload = {
  sub: user.customer_id,
  email: user.email,
  role: user.role,
};

//When the JWT is verified, NestJS gets:
req.user

// which might look like:
// {
//   "sub": 1,
//   "email": "john@gmail.com",
//   "role": "customer"
// }

////////////////////////////
// Example Controller
// Using a JWT guard:

@Get('me')
@UseGuards(JwtAuthGuard)
getCurrentUser(@Req() req) {
  return this.usersService.findOne(
    req.user.sub,
  );
}


///////////////////////
// What Happens

GET /users/me
      ↓
JWT Guard verifies token
      ↓
req.user.sub = 1
      ↓
SELECT * FROM customers
WHERE customer_id = 1
      ↓
Return user data
```

#### Why /me Is Better Than /users/:id

```JS
// Instead of:
GET /users/1

//you do:
GET /users/me

//Benefits:
-frontend doesn't need to know user ID
-less chance of requesting another user's data
-cleaner API
-can fetch data in NEXT.js using server components for system performance
```
