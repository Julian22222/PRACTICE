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

- JWT in HttpOnly Cookies (Access token + Refresh token pattern) ✅ the most common and recommended approach for web apps. Common for:

      - Next.js + NestJS
      - SaaS web apps

- JWT in Authorization Header. very common for:

      - Mobile apps
      - Public APIs
      - Microservices
      - SPA apps where you control token storage

📍 JWT is Useful and helps you:

- keep users logged in
- protect routes
- identify users securely
- build authentication systems for APIs

#### ⭐ JWT + HttpOnly Cookies

✅ 1. Install needed dependencies for your Back-end (Nest.JS) - in Nest.js folder

```JS
npm install @nestjs/jwt @nestjs/passport passport passport-jwt
npm install -D @types/passport-jwt
```

✅ 2. Add JWT module in NestJS

```JS
cd to bank-api/src folder -> nest g resource auth //This create auth folder with module, controllers and services + dto + test files.
```

```JS
//auth.module.ts

import { Module } from '@nestjs/common'; //<--Imports the Module decorator from NestJS. Then we can use Module in this file
import { JwtModule } from '@nestjs/jwt';  //<-- Imports NestJS JWT support. JwtModule helps - create JWT tokens, verify JWT tokens, manage authentication. JWT = JSON Web Token.
import { PassportModule } from '@nestjs/passport';  //<-- Imports Passport integration for NestJS.Passport is a popular authentication library.It helps with: login systems, JWT authentication, guards/strategies

@Module({   //module configuration <--is used to create a NestJS module. A module helps organize related code together. @Module() it is an object with settings
  imports: [   //<--imports means: “Which modules does this module need?” -  PassportModule,
    PassportModule,
    DatabaseModule,
    JwtModule.register({   //<-- JwtModule.register - Configures the JWT module. /// .register() means: “Set up JWT settings.”
      secret: process.env.JWT_SECRET,  //<- Defines the secret key used to sign JWT tokens.JWT tokens are encrypted/signed using this secret. If no environment variable exists -use 'dev_secret' as fallback./ //// 'dev_secret' <-- Never use this in production, Use a strong secret in .env
      signOptions: { expiresIn: '1h' },  //access tokken will expire after 1h. The JWT token becomes invalid after 1 hour. Examples: '1h' → 1 hour, '7d' → 7 days, '15m' → 15 minutes
    }),
  ],
  controllers: [AuthController],
  providers: [JwtStrategy, AuthService],
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

✅ 4. auth/login controller (Nest.JS)

```JS
//auth.controller.ts

import { Body, Controller, Get, Post, Req, Res } from '@nestjs/common';
import { AuthService } from './auth.service';
import { LoginDto } from './dto/login.dto';
import { Response, Request } from 'express';  //Express response object.

@Controller('auth')
export class AuthController {
  constructor(private readonly authService: AuthService) {}

//other methods...

 @Post('login')
  async login(
    @Body() loginData: LoginDto,
    @Res({ passthrough: true }) res: Response,  //<--@Res play importan role here
  ) {
    return this.authService.login(loginData, res);
  }
}

//@Res({ passthrough: true }) res: Response - is a NestJS feature that gives you access to the underlying Express Response object without taking over the entire response handling.
//@Res() is a parameter decorator that injects the Express Response object into your controller method.
// Response from (@Res() res: Response) -> gives you methods like:
res.status(200);
res.json(data);
res.send(data);
res.cookie(...);  //allow set the cookie
res.clearCookie(...);  //tells the browser to delete the cookie from Next.js
res.redirect(...);

//If you use->  login(@Res() res: Response) //without { passthrough: true } -> you have to return response using
res.json(...)
//or
res.send("...")
//or any others res. methods from Response

//@Res({ passthrough: true }) -> Nest lets you modify the response (cookies, headers, status code, etc.) while still allowing you to return a value normally ->
return this.authService.login(loginData, res)

----------------------------------------
//Example:
@Post('login')
login(@Res() res: Response) {  //Without { passthrough: true }, Nest is waiting to return the data by using -> res. /YOU MUST use -> res.
  // res.cookie('access_token', token);

  return {  //In this case the client request will typically hang because no response is ever completed.
    message: 'Logged in',
  };
}

//This will work ok
//  res.json({
  //   message: 'Logged in',
  // });
```

✅ 4. auth/login service (Nest.JS)

```JS
//auth.service.ts

import { JwtService } from '@nestjs/jwt';
import { Injectable, UnauthorizedException } from '@nestjs/common';
const bcrypt = require('bcrypt');

@Injectable()
export class AuthService {
   constructor(
    @Inject(PG_POOL) private readonly pool: Pool,
    private readonly jwtService: JwtService,     //<-- import JwtService
    private configService: ConfigService,     //<-- import ConfigService, that helps to read secrets from .env file
  ) {}


 //// 🔐 Login + return token using -< res.cookie
 async login(loginData: LoginDto, res: Response) {  //
    const { email, password } = loginData;

   const result = await this.pool.query(
      `SELECT * FROM customers WHERE email = $1`,
      [email],
    );

  const user = result.rows[0];

  if (!user) {
    throw new UnauthorizedException('Invalid email or password');
  }

  const isMatch = await bcrypt.compare(password, user.password);  //compare passwords

  if (!isMatch) {
    throw new UnauthorizedException('Invalid email or password');
  }


//Once Password is matching you can create JWT payload
  // 👇 JWT payload (keep it small!) <-- you can get access to this data from anywhere in Nest.js (usually is used in Nest.js controller to get logedIn user Id)-> sub and email
  //The payload is the data stored inside the JWT token. Also JWT contains extra JWT metadata like: expiration time, issued time.
  //after login, this information becomes accessible: in Next.js, in browser cookies (if you store token there), after decoding the token
  //JWT Payload Is NOT Secret, Anyone who has the token can decode and read the payload.
  ❌//NEVER store: password, credit card, sensitive private data in payload
  const payload = {
    sub: user.customer_id,  //sub means: subject. It is a standard JWT field representing: “Who owns this token?” - Usually:user id, customer id, account id
    email: user.email,
    role: user.role,
  };
  //Then NEXT.JS can know: is admin?, which user logged in?, what UI to show?


    // 🔐 Create Access Token
    const accessToken = this.jwtService.sign(payload, {
      // secret: process.env.JWT_SECRET,
      secret: this.configService.get<string>('JWT_SECRET'),
      expiresIn: '15m', //expires in 15min
    });


  // 🔄 Create Refresh Token
    const refreshToken = this.jwtService.sign(payload, {
      // secret: process.env.JWT_REFRESH_SECRET,
      secret: this.configService.get<string>('JWT_REFRESH_SECRET'),
      expiresIn: '7d',
    });


 // 🍪 STORE BOTH IN HTTPONLY COOKIES
    res.cookie('access_token', accessToken, {
      httpOnly: true,
      secure: false, // true in production (HTTPS)
      sameSite: 'lax',
      maxAge: 15 * 60 * 1000,
    });

    res.cookie('refresh_token', refreshToken, {
      httpOnly: true,
      secure: false, // true in production (HTTPS)
      sameSite: 'lax',
      maxAge: 7 * 24 * 60 * 60 * 1000, // 7 days
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

  return userResponse;
}

///////////////////
👉 // Why Payload Is Useful:
//   Frontend often needs:
//         -current user id
//         -email
//         -role
//         -permissions
// without making additional database request.

```

✅ 5. Now Next.js LoginForm return userResponse + res.cookie(accessToken) + res.cookie(refreshToken)

```JS
//LoginForm - Next.js:
try {
      const loginRes = await fetch(
        `${process.env.NEXT_PUBLIC_BACK_END_URL}/auth/login`,
        {
          method: "POST",
          headers: { "Content-Type": "application/json" },
          body: JSON.stringify(formInput),
        },
      );

      const response = await loginRes.json();  //this returns - userResponse

      if (!loginRes.ok) {
        console.error("Login failed:", await loginRes.text());
        setLoginError(response.message || "Failed to login");
        return;
      }

      //other code ..
}
```

✅ 6. Send JWT in future requests

- if you want to use payload data in Nest.js methods

```JS
//Whenever you call protected routes: (routes that only logedIn users can access)

//Example to get transactions of LogedIn user in server component
  try {
    const res = await fetch(
      `${process.env.NEXT_PUBLIC_BACK_END_URL}/transactions/my`,  //Nest will use UserId from payload
      {
        headers: {
          //JwtAuthGuard requires a JWT, If your JWT is stored in a cookie, then req.user. Needs this code
          Cookie: cookieStore.toString(),
        },
        cache: "no-store",
        next: { tags: ["transactions"] },
      },
    );

    const data = await res.json();
    if (!res.ok) {
      throw new Error(data.message || "Failed to fetch transactions");
    }

    return data;
  } catch (error) {
    console.error(error);
    throw new Error("Unexpected error occured");
  }
};
```

# 🧩 Can Next.js decode payload directly?

```JS
//⚠️ But decoding ≠ validating
//Anyone can decode JWT.
//Only backend can truly verify it using secret key.
```

🧩 Even if you think you don’t have protected routes, most apps eventually have them

```JS
//When you Setup JWT + HttpOnly Cookies

//In Controller you can protect some routes
@Patch('password')
@UseGuards(JwtAuthGuard)   //<-- means only loged in user can access this route.
userPasswordUpdate(...){
  return ...
}


- /users/profile
- /accounts
- /transactions
- /admin/
```

✅ Add JWT validation (Guard)

if no guard -> anyone can --> GET /users/profile (without login)

👉 JWT Guard is what blocks that.

🧠 So JwtStrategy = “How to read and validate JWT”

```JS
//Create:
jwt.strategy.ts  //strategy.ts file needs to validate each request goes with access token, and Nest.js can validate that user access token


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


//////🚨 If token is missing or wrong. NestJS automatically returns:
//{
//   "statusCode": 401,
//   "message": "Unauthorized"
// }

// And Controller NEVER runs.
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

#### httpOnly cookies (best practice)

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

# Real authentication systems there are usually two tokens

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

🚨

### Why needed JWT secret and JWT refresh secret ?

Security.

If access token secret leaks:

- refresh tokens still protected.

# 🔄 How Refresh token works

1. Create Refresh Endpoint - in controller

```JS
//Example Nest.JS route - auth.controller.ts

@Controller('auth')
export class AuthController {
  constructor(private readonly authService: AuthService) {}

//some code..

//create new endpoint. -> /refresh
//// POST /refresh
//Frontend sends refresh token → backend returns new access token.
//when access token expire we will POST credentials (access token to /refresh route)
  @Post('refresh')
  refreshToken(@Req() req: Request, @Res({ passthrough: true }) res: Response) {
    return this.authService.refreshToken(req, res);
  }
```

2. Create Refresh Service

```JS
//NEST.JS -> auth.service.ts

@Injectable()
export class AuthService {
  constructor(
    @Inject(PG_POOL) private readonly pool: Pool,
    private readonly jwtService: JwtService,
    private configService: ConfigService,
  ) {}

//some other code ...


//refresh token should come from COOKIE, not from body or header for better security
  async refreshToken(req: Request, res: Response) {
    try {
      const refreshToken = req.cookies.refresh_token;

      if (!refreshToken) {
        throw new UnauthorizedException('No refresh token');
      }

      // Verify refresh token
      const payload = this.jwtService.verify(refreshToken, {
        // secret: process.env.JWT_REFRESH_SECRET,
        secret: this.configService.get<string>('JWT_REFRESH_SECRET'),  //getting secrets from .env or AWS Parameter Store
      });


      // Create new access token
      const accessToken = this.jwtService.sign(
        {
          sub: payload.sub,
          // email: payload.email,
        },
        {
          // secret: process.env.JWT_SECRET,
          secret: this.configService.get<string>('JWT_SECRET'),  //getting secrets from .env or AWS Parameter Store
          expiresIn: '15m',  // Access Token -> expiresIn: '15m'
        },
      );

       //Create new access token
      res.cookie('access_token', accessToken, {
        httpOnly: true,
        secure: false,
        sameSite: 'lax',
        maxAge: 15 * 60 * 1000,   // Access Token -> expiresIn: '15m'
      });


      return { message: 'token refreshed' };
    } catch {
      throw new UnauthorizedException('Invalid refresh token');
    }
  }
```

3. Check main.ts file - make sure you have cookie-parser.

```JS
//main.ts

import cookieParser from 'cookie-parser';

async function bootstrap() {
  const app = await NestFactory.create(AppModule);

  app.use(cookieParser());  //Without that, req.cookies will be undefined. (req.cookies.access_token, req.cookies.refresh_token)

  app.enableCors({
    origin: 'http://localhost:3000',
    credentials: true,
  });

  await app.listen(3005);
}

bootstrap();
```

```JS
//Next.js fetch must include credentials
//when you do fetch always - When calling NestJS from Next.js:
//add credentials

fetch(`${API_URL}/customers`, {
  //Every request that needs authentication needs:
  credentials: "include",  //<--//add credentials
});

//This logic is already inside src/lib/apiFetch.ts file (see step 4)
```

4. Create src/lib/apiFetch.ts (logic to create new acccess token using refresh token)

```JS


//❌ NOT directly inside every component.
//🔥 The best practice is to create a central API wrapper/ fetch wrapper.
//create -> src/lib/api.ts

src/
├── app/
├── components/
├── lib/
│   └── apiFetch.ts
└── services/

//src/lib/api.ts - This is where refresh logic usually lives
//For refresh tokken and access tokken generation and validation.
const API_URL = process.env.NEXT_PUBLIC_BACKEND_URL;

export async function apiFetch(
  endpoint: string,
  options: RequestInit = {}
) {
  let response = await fetch(`${API_URL}${endpoint}`, {
    ...options,
    credentials: "include",
  });

  // Access token expired
  if (response.status === 401) {
    const refreshResponse = await fetch(
      `${API_URL}/auth/refresh`,
      {
        method: "POST",
        credentials: "include",
      }
    );

    if (refreshResponse.ok) {
      // retry original request
      response = await fetch(
        `${API_URL}${endpoint}`,
        {
          ...options,
          credentials: "include",
        }
      );

    }
  }

  return response;
}

----------------------------------------

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

5. Change you Next.js -> fetch methods to apiFetch("/someRoute")

```JS

//Then insted of common way of doing everywhere ->
// ❌ Instead of doing: fetch(`${URL}/someRoute`)
//use: ->
//the same with any endpoint
apiFetch("/someRoute") //this function - attach access token automatically, detect 401 Unauthorized, call /refresh, retry request.
//this will automatically add the access token to the header and retry once if the access token is expired.


//only login request from Next.js should use the same common logic->
await fetch(
 `${BACKEND_URL}/auth/login`, //<-- when user logIn
 {
   method:"POST",
   headers:{
     "Content-Type":"application/json"
   },
   credentials:"include",
   body:JSON.stringify({
     email,
     password
   })
 }
);
```

6. Check auth/jwt.strategy.ts file

```JS
//auth/jwt.strategy.ts

@Injectable()
export class JwtStrategy extends PassportStrategy(Strategy) {

 constructor(
   private configService: ConfigService
 ) {

 super({
   jwtFromRequest: ExtractJwt.fromExtractors([
     (req)=> req.cookies.access_token
   ]),
   secretOrKey:
     configService.get<string>("JWT_SECRET"),
 });

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
```

```JS
//FLOW of ACCESS and REFRESH tokens

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

# Where and when use REFRESH

```JS

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

```JS
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

```JS
// A common production architecture looks like:

NestJS Login
    ↓
Sets HttpOnly Cookies
    ↓
Next.js calls API  //fetch request + cookie
    ↓
Browser automatically sends cookies //Each request From Next.js sends to Nest.js with cookie
    ↓
NestJS validates JWT //in auth/jwt.strategy.ts
    ↓
Returns user data
```

//////////////////////////////////////////////////////////////////////////////////////////////////////

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

//Get Current User in client component
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
