# How to Store passwords

❌ DON'T store passwords as plain text in Database

## use hash password

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

# JWT

✅ 1. Install needed dependencies for your Back-end (Nest.JS) - in Nest.js folder

```JS
npm install @nestjs/jwt @nestjs/passport passport passport-jwt
npm install -D @types/passport-jwt
```

✅ 2. Add JWT module in NestJS

```JS
//auth.module.ts

import { Module } from '@nestjs/common';
import { JwtModule } from '@nestjs/jwt';
import { PassportModule } from '@nestjs/passport';
import { UsersService } from './users.service';
import { UsersController } from './users.controller';

@Module({
  imports: [
    PassportModule,
    JwtModule.register({
      secret: process.env.JWT_SECRET || 'dev_secret',
      signOptions: { expiresIn: '1h' },
    }),
  ],
  controllers: [UsersController],
  providers: [UsersService],
  exports: [JwtModule],
})
export class UsersModule {}
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
  const payload = {
    sub: user.customer_id,
    email: user.email,
  };

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
