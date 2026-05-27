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

//////////////////////////////////////////////////////////////////////////////////
📍// This module:
//     -enables JWT authentication
//     -enables Passport authentication
//     -registers controllers
//     -registers services
//     -configures JWT secret and expiration
//     -shares JWT functionality with other modules
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
