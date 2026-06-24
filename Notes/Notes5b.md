Beforehand - you need to create JWT + Http-Only cookie

# This allows to reload the page and still have the "Loged In" / "Active" user

The most used and popular method here is to use JWT + Http-Only cookie with Context (useState) where you keep "Loged In" user in state. When you refresh the page -> it automatically assigns "Loged In" user from cookies to useState->

```JS
const [user, setUser] = useState(LogedIn user);
//if cookie has a userData it will assign it when you refresh the page
```

#### If you store authentication in an HTTP-only cookie: JWT Cookie

then:

- Refresh page ✅
- New tab ✅
- Browser restart (if persistent cookie) ✅
- More secure than localStorage ✅

```JS
//A typical flow of JWT + HTTP-only cookie

HTTP-only cookie
    ↓
NestJS validates JWT
    ↓
GET /auth/me
    ↓
returns user profile (Current user)
    ↓
store in Zustand/Context
```

If your JWT is stored in an HTTP-only cookie, the cookie survives page refreshes, browser restarts (depending on expiration), and navigation.

If you keep "Loged In" user in React global state (Context, Zustand, Redux, useState, etc.) without -> initial user assigning from cookie ->

```JS
const [user, setUser] = useState(LogedIn user);
//if cookie has a userData it will assign it when you refresh the page
```

than the "Loged in" user exists only in memory, so it disappears on a full page refresh.

```JS
//A typical HTTP-only JWT cookie setup

HTTP-only JWT cookie
        ↓
Server reads cookie
        ↓
Determines current user/admin
        ↓
Returns user data
```

# How to get the "LogedIn" / "Active" user from JWT and Http-Only cookies in Next.js?

- Usually Developers use - Server side components to get "LogedIn" user from cookies. It is GOOD PRACTICE to get "LogedIn" user from cookies using server side option. (server side option + Context.) See example below.
- And rarely developers use code to get "LogedIn" user from client components from cookies. (Check an example below)

Do I need create separate files to get "Active User" from cookies, from server and client components? Yes, that's usually the cleanest approach.

```JS
src/
├─ lib/
│   ├─ auth/
│   │   ├─ server.ts  //use this file if you want to get userData from server component. It is GOOD PRACTICE to use this option + Context
│   │   ├─ client.ts
```

1. ✅ This option works only in Server side components, to get the "Active user" - using JWT and HTTP-only cookie.

```JS
//server.ts

//getting "LogedIn" user from cookies in server side component
//this code work only in server side components
import { cookies } from "next/headers";

export const loadUser = async () => {
  const cookieStore = await cookies();

  const meRes = await fetch(
    `${process.env.NEXT_PUBLIC_BACK_END_URL}/users/me`,
    {
      headers: {
        Cookie: cookieStore.toString(),
      },
      cache: "no-store",
    },
  );

  if (!meRes.ok) {
    return null; // better for redirects
  }

  return meRes.json();
};
```

2. ❌ This option works only in client side componets to get the "Active user" - using JWT and HTTP-only cookie. This option is NOT GOOD to use. Not Good Practice.

```JS
///client.ts

//this code works only in client side components
//getting "LogedIn" user from cookies in client side component
//Should i keep both loadUser for client isde and server side components

export const loadUser = async () => {
  const meRes = await fetch(
    `${process.env.NEXT_PUBLIC_BACK_END_URL}/users/me`,
    {
      credentials: "include",
      cache: "no-store",
    },
  );

  if (!meRes.ok) {
    throw new Error("Failed to fetch user");
  }

  const userData = await meRes.json();

  console.log("auth file - meRes status:", meRes.status);
  console.log("auth file - userData", userData);

  console.log("User data in loadUser RETURN:", userData);
  return userData;
};
```

- If you use this option, for client side component. Then every client component where you need to get "active user" must have:

```JS
useEffect(() => {
  loadUser().then(setUser);
}, []);

// Problems of this option:
// -Multiple /users/me requests
// -Repeated code everywhere
// -Loading states everywhere
// -Not ideal

// This is generally not the preferred approach.
```

### 🔥 Good Practice to get "LogedIn" user from cookies using server side option together with Context

- You can get "LogedIn" user data from server component or client component
- But Good Practice is to get "LogedIn" user data from server

### Choose one of the Option below. The most common option is Option 1

- Server loads user once + Context shares it

1. 🔥 Option 1. This is Commonly used approach.

Advantages of this approach:

- ✅ No prop drilling (when you fetch LogedIn / Active user in server component (page.tsx for example) and then pass it as a prop)
- ✅ LogedIn / Active User available everywhere
- ✅ LogedIn / Active User fetched only once, No additional API request.
- ✅ Common in large applications

```JS
//Recommended architecture:

HTTP-only JWT cookie
        ↓
GET /auth/me
        ↓
Current user
        ↓
Store in Zustand/Context


// Use global state as a cache

// Many production apps do this:

// 1.JWT stays in HTTP-only cookie.
// 2.On app startup, fetch /auth/me.
// 3.Store the result in Zustand/Context.
// 4.After refresh, -> fetch /auth/me again and -> repopulate the store (assign the data to useState).

Refresh page
    ↓
Cookie still exists
    ↓
Fetch /auth/me
    ↓
Receive user
    ↓
Fill Zustand store/ Context
     ↓
Use store everywhere


// On refresh:

// Refresh
//   ↓
// Store is empty
//   ↓
// Fetch /auth/me again
//   ↓
// Restore store (restore useState - assigning user from cookies)


Login
  ↓
Cookie set
  ↓
Fetch /auth/me
  ↓
Save user in store
  ↓
Use store everywhere
```

- It uses global state as a cache

Create AuthProvider and Pass it into Context:

- use this option -> Good when many client components need the user.

#### 🛣️ First -> Create separate file to Fetch "Active user" using server component

```JS
//auth-server.ts file
//this work only in server side components
import { cookies } from "next/headers";

export const loadUser = async () => {
  const cookieStore = await cookies();

  const meRes = await fetch(
    `${process.env.NEXT_PUBLIC_BACK_END_URL}/users/me`,
    {
      headers: {
        Cookie: cookieStore.toString(),
      },
      cache: "no-store",
    },
  );

  if (!meRes.ok) {
    return null; // better for redirects
  }

  return meRes.json();
};
```

#### Then create these files below

```JS
//AuthProvider.tsx
"use client";

import { useState } from "react";
import { AuthContext } from "./AuthContext";
import { IUserWithAccount } from "@/src/shared/types/userWithAccount.interface";

export function AuthProvider({
  initialUser,
  children,
}: {
  initialUser: IUserWithAccount | null;
  children: React.ReactNode;
}) {
  //after reloading the page initialUser will be set in useState from cookie
  const [user, setUser] = useState(initialUser);

  return (
    <AuthContext.Provider value={{ user, setUser }}>
      {children}
    </AuthContext.Provider>
  );
}

////////
//AuthContext.tsx

"use client";

import { IUserWithAccount } from "@/src/shared/types/userWithAccount.interface";
import { createContext, useContext } from "react";

type AuthContextType = {
  user: IUserWithAccount | null;
  setUser: React.Dispatch<React.SetStateAction<IUserWithAccount | null>>;
};

// export const AuthContext = createContext<any>(null);
export const AuthContext = createContext<AuthContextType | null>(null);

export const useAuth = () => {
  const context = useContext(AuthContext);

  if (!context) {
    throw new Error("useAuth must be used inside AuthProvider");
  }

  return context;
};
```

```JS
//app/(user)/layout.tsx

import { loadUser } from "../actions/auth-server";
import { AuthProvider } from "./AuthProvider";

export default async function UserLayout({
  children,
}: {
  children: React.ReactNode;
}) {
  let user = null;

  try {
    user = await loadUser(); //server side approach to get "LogedIn" / "Active" user
  } catch {
    redirect("/login");
  }

  return <AuthProvider initialUser={user}>{children}</AuthProvider>;
}

// 👉 This runs ONCE on the server
// 👉 Gets user from HTTP-only cookie
// 👉 Passes it into React tree
```

Then -> Then anywhere in client component: every page inside: app/(user)/ <-- i have access to "user"

```JS
//✅ useAuth() can ONLY be used in Client Components.
//"use client";

//👉 Reads from Context
const { user } = useAuth();

//or
const { user, setUser } = useAuth();
//to setUser(with new data, when edit the user data) or when logout setUser(null)

return <div>{user?.email}</div>;

// ⚠️ Important limitation
// If user changes (logout/login), you must update:
//setUser(newUser)
//Because Context does NOT automatically sync with cookie.


// ❌ You CANNOT use useAuth() in:
// - layout.tsx (Server Component)
// - page.tsx (Server Component by default)
// - server actions
// - server components in general

// Because React Context (useContext) does not exist on the server.

------------------------------

// Advantages of this option:

// ✅ No prop drilling
// ✅ User available everywhere
// ✅ User fetched only once
// ✅ No additional API request.
// ✅ Common in large applications
```

2. ✅ Option 2 to get "Active user".

Advantages of this approach, of passing Active User as a prop:

- ✅ No extra client fetch (no client-side loading)
- ✅ User comes directly from HTTP-only cookie
- ✅ Better performance (data is available immediately)
- ✅ Better security
- ✅ Works great with Next.js App Router
- ✅ No hydration issues

This is usually the first choice when only a few components need the user.

##### In Server Component: in page.tsx you get the "LogedIn" / "Active" user

- fetching the user in a Server Component and passing it down as props.
- You will run many times -> the command (const activeUser = await loadUser();) to get the Active user when "LogedIn" user data needed
- Use this option Good when only a few components need "Active user".

#### 🛣️ First -> Create separate file to Fetch "Active user" using server component

```JS
//auth-server.ts file
//this work only in server side components
import { cookies } from "next/headers";

export const loadUser = async () => {
  const cookieStore = await cookies();

  const meRes = await fetch(
    `${process.env.NEXT_PUBLIC_BACK_END_URL}/users/me`,
    {
      headers: {
        Cookie: cookieStore.toString(),
      },
      cache: "no-store",
    },
  );

  if (!meRes.ok) {
    return null; // better for redirects
  }

  return meRes.json();
};
```

#### Then create these files below

```JS
//you fetch the data in server component - page.tsx and then pass this user as a prop to children components
const activeUser = await loadUser();  //fetch user from server component

 if (!activeUser) {
    redirect("/login");
  }


 return (
    <AccountsDashboard
      activeUser={activeUser}
    />
  );


// Advantages of passing Active User as a prop:

// ✅ No extra client fetch (no client-side loading)
// ✅ User comes directly from HTTP-only cookie
// ✅ Better performance (data is available immediately)
// ✅ Better security
// ✅ Works great with Next.js App Router
// ✅ No hydration issues

// This is usually the first choice when only a few components need the user.
```

```JS
In this approach:

❌ No AuthProvider
❌ No useAuth()
✅ Just pass user as props
```

3. ❌ Option 3. Try to avoid to use this option

```JS
//client server
useEffect(() => {
  loadUser();
}, []);

// Disadvantages
// ❌ Extra network request
// ❌ Loading state needed
// ❌ Slower
// ❌ Can flash unauthenticated UI
// This is usually the least preferred option in App Router.
```

#### auth-server.ts is used in page.tsx and layout.tsx

```JS

Instead of:

app/
├── (user)/layout.tsx
├── (admin)/layout.tsx


both containing:
<AuthProvider initialUser={...}>

I would usually have one AuthProvider at the highest level where authentication is needed.

For example:

app/
├── layout.tsx
├── (user)/
├── (admin)/

Root layout

const currentUser = await getCurrentUserServer();



<AuthProvider initialUser={currentUser}>
  {children}
</AuthProvider>

Now both (user) and (admin) can access:
const { user } = useAuth();
and check:
user?.role === "admin"

--------------------------------------------------------------------

Even better with Zustand

Instead of:

const [user, setUser] = useState(initialUser);
many teams use Zustand:

type AuthStore = {
  user: User | null;
  setUser: (user: User | null) => void;
};

Then hydrate it once:
"use client";

useEffect(() => {
  useAuthStore.setState({
    user: initialUser,
  });
}, [initialUser]);

Then anywhere:

const user = useAuthStore((state) => state.user);

No Context Provider nesting, no prop drilling.

---------------------------------

For route protection

Your layouts are actually a great place to enforce authorization.

app/(admin)/layout.tsx
const user = await getCurrentUserServer();

if (!user) {
  redirect("/login");
}


if (user.role !== "admin") {
  redirect("/unauthorized");
}



return children;

app/(user)/layout.tsx

const user = await getCurrentUserServer();



if (!user) {

  redirect("/login");

}

return children;

This is one of the strongest reasons to have separate layouts.

------------------------------------------------------

What I would do in your banking app

app/
├── layout.tsx
├── (user)/
│   └── layout.tsx
├── (admin)/
│   └── layout.tsx

Root layout
-Load current authenticated user once.
-Initialize AuthProvider/Zustand.

User layout
-Verify user is logged in.

Admin layout
-Verify user is logged in.
-Verify role === admin.

Client components

const user = useAuthStore((state) => state.user);

This is a very common production architecture for Next.js App Router applications and scales well as the project grows.
```

# Do you need to create separate cookies for user and admin?

Usually no

user and admin can both use the same authentication cookie.

# Do I need separate files to get cookie from server and client components?

# Do i need to create separate cookies for user and admin?

```JS
Not necessarily. Having separate database tables and separate NestJS services does not automatically mean you need separate cookies.
The key question is:

Can an authenticated session belong to either a User or an Admin, and can you determine which one from the JWT?

If yes, a single cookie is usually enough.

For example:
{
  sub: "42",
  role: "admin"
}

or

{
  sub: "123",
  role: "user"
}

Then in your NestJS auth logic:

async validate(payload: JwtPayload) {
  if (payload.role === "admin") {
    return this.adminService.findById(payload.sub);
  }

  return this.userService.findById(payload.sub);
}

The cookie is the same:

access_token=...
The JWT payload tells your backend which table/service to use.

---------------------------------------------------------------

When separate cookies make sense

Separate cookies are useful when you have truly separate authentication systems.

For example:
Admin Portal
  admin_access_token

Customer Portal
  user_access_token

or

admin_token
user_token
This might be appropriate if:

-Admins and users log in independently.
-A browser can be logged in as both at the same time.
-Admin routes and user routes are essentially different applications.

Example:
/admin
/customer
and you want:

Admin A logged in
Customer B logged in

in the same browser session simultaneously.
A single cookie cannot easily represent two active identities at once.

------------------------------------------------------------------------------

For your folder structure
You mentioned:

Bank/
├── bank-api     (NestJS)
└── bankapp      (Next.js)
    ├── (user)
    └── (admin)

This alone doesn't require separate cookies.
A common implementation is:

{
  sub: "123",
  role: "admin"
}

and

{
  sub: "456",
  role: "user"
}

with one cookie:

access_token

Then your guards check the role:
@Roles("admin")
@Controller("admin")

@Roles("user")
@Controller("user")

and your JWT strategy loads the correct entity based on role.

---------------------------------------------------------

What I would recommend

If:
-Users table and Admins table are separate.
-Only one account is logged in at a time.
-(admin) and (user) are just role-specific sections of the same application.

Use:
1 HTP-only access token cookie
1 HTTP-only refresh token cookie

JWT payload:
{
  sub: "123",
  role: "admin", // or "user"
  type: "admin"  // optional
}

Then route to the proper service:

switch (payload.role) {
  case "admin":
    return this.adminService.findById(payload.sub);

  case "user":
    return this.userService.findById(payload.sub);
}

Only introduce separate cookies if you explicitly need two simultaneous sessions (e.g., logged in as an admin and as a customer in the same browser at the same time). For most applications, including many banking systems, a single authenticated session with a role/type claim is simpler and easier to maintain.

```
