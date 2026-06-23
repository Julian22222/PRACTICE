# How to keep active user without using global useStete -> that allow to reload the page and still have the active user

### JWT stays in HTTP-only cookie

when user is loged in we don't need to assign the user to global State - useState. Then:

- When user Refresh page ❌ loses state
- When user Open new tab ❌ loses state
- When user make Browser restart ❌ loses state

because React state only lives in memory.

Therefore we need to use HTTP-only cookie and JWT

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

If your JWT is stored in an HTTP-only cookie, the cookie survives page refreshes, browser restarts (depending on expiration), and navigation. A React global state (Context, Zustand, Redux, useState, etc.) exists only in memory, so it disappears on a full page refresh.

A typical setup looks like this:

```JS
HTTP-only JWT cookie
        ↓
Server reads cookie
        ↓
Determines current user/admin
        ↓
Returns user data
```

# You can get user data from server component or client component

in most cases in Next.js App Router, getting the "active user" in a Server Component is the better approach.

```JS
//server component
export default async function AccountsPage() {
  const user = await loadUser();

  if (!user) {
    redirect("/login");
  }

  const accounts = await fetchAccounts(user.id);

  return (
    <AccountsDashboard
      user={user}       //passing user as a prop to other children components
      accounts={accounts}
    />
  );
}

// This is often preferable because:

// -no client-side loading
// -no hydration issues
// -data is available immediately
// -more secure
```

Best practice summary

✅ Best (recommended default)

- Server Component fetches user
- Pass user as prop

⚡ Also good (for UI convenience)

- Context/Zustand for client components

❌ Avoid

- Fetching /me in every client component
- Storing JWT in localStorage
- Duplicating auth logic everywhere

# Final rule of thumb

- If a page depends on the user → fetch it on the server
- If a component just displays user → pass it as prop
- If UI needs global access → use Context as cache

#### Why Server Component auth is better

1. No “loading user” flicker

If you fetch in a client component:

- page renders
- then fetch /users/me
- then UI updates

So you get a small “unauthenticated → authenticated” transition.

Server-side:

- user is already known before render
- UI is correct immediately

2. No extra client request

Client approach:

- page loads → /users/me request → state update

Server approach:

- page request → user included → HTML already ready

Faster and cleaner.

3. Better security model

Server components:

- read HTTP-only cookie directly
- never expose token to browser JS
- no client-side auth logic needed

This is the most secure pattern in Next.js.

4. Better architecture separation

```JS
        Layer	                  Responsibility

Server Component	            Auth, data fetching
Client Component	            UI, interactions
```

# Do you need to create separate cookies for user and admin?

Usually no

user and admin can both use the same authentication cookie.

# Do I need separate files for server and client cookie access?

Yes, that's usually the cleanest approach.

```JS
src/
├─ lib/
│   ├─ auth/
│   │   ├─ server.ts
│   │   ├─ client.ts
```

```JS
//server.ts

import { cookies } from "next/headers";

export async function getToken() {
  const cookieStore = await cookies();
  return cookieStore.get("access_token")?.value;
}




///client.ts
export async function getCurrentUser() {
  const res = await fetch("/api/auth/me");
  return res.json();
}


Then:

// Server Component
import { getToken } from "@/lib/auth/server";



// Client Component
import { getCurrentUser } from "@/lib/auth/client";

```

# For a modern Next.js App Router + NestJS application, Option 2 (global state as a cache) is usually the best developer experience, while the cookie/JWT remains the source of truth.

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
// 4.After refresh, fetch /auth/me again and repopulate the store.
```

```JS
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
// Restore store


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

# create app/(user)/layout.tsx

# can use the same cookies for user and admin

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

--------------------------------------------------------------------------------


Yes, you're understanding the pattern correctly. However, there are a few refinements I'd make.

What you're describing

//app/(user)/layout.tsx
import { getCurrentUserServer } from "@/lib/auth/server";
import { AuthProvider } from "@/providers/auth-provider";

export default async function UserLayout({
  children,
}: {
  children: React.ReactNode;
}) {

  const user = await getCurrentUserServer();

  return (
    <AuthProvider initialUser={user}>
      {children}
    </AuthProvider>
  );

}

----------------------------

AuthProvider
"use client";

import { createContext, useState } from "react";

export function AuthProvider({
  initialUser,
  children,
}: {
  initialUser: User | null;
  children: React.ReactNode;
}) {

  const [user, setUser] = useState(initialUser);

  return (

    <AuthContext.Provider value={{ user, setUser }}>
      {children}
    </AuthContext.Provider>
  );
}

--------------------------------------

Client component
"use client";

const { user } = useAuth();

return <div>{user?.email}</div>;

This works and is a valid pattern.

-----------------------------------------

But I'd avoid duplicating providers

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

# How to get the active user in Next.js without global useState?

1. ✅ This option works only in Server side components, to get the "active user" - using JWT and HTTP-only cookie. It is GOOD PRACTICE to use this option + Context.

```JS
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

2. ❌ This option works only in client side componets to get the "active user" - using JWT and HTTP-only cookie. This option is NOT GOOD to use. Not Good Practice.

```JS
//this code can be used only in client side components

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

### 🔥 Good Practice to use this Server side option together with Context

- Server loads user once + Context shares it

Create separate file to Fetch active user from server component

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

### Then you need to choose one of the Option below. The most common option is Option 2

1. Option 1 to get "active user"

In Server Component: in page.tsx

- Getting the user in a Server Component and passing it down as props.
- Use this option Good when only a few components need "active user".

```JS
//you fetch the data in server component - page.tsx and then pass this user as a prop to children components
const activeUser = await loadUser();  //fetch user from server component

 return (
    <AccountsDashboard
      userAllAccounts_withBalance={userAllAccounts_withBalance}
      activeUser={activeUser}
    />
  );


// Advantages of passing activeUser as a prop:

// ✅ No extra client fetch
// ✅ User comes directly from HTTP-only cookie
// ✅ Better performance
// ✅ Better security
// ✅ Works great with Next.js App Router

// This is usually the first choice when only a few components need the user.
```

```JS
In this approach:

❌ No AuthProvider
❌ No useAuth()
✅ Just pass user as props
```

2. 🔥 Option 2. This is Commonly used approach.

Create AuthProvider and Pass it into Context:

- use this option -> Good when many client components need the user.

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
    user = await loadUser(); //server approach tp get user
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

//No additional API request.

// Advantages

// ✅ No prop drilling
// ✅ User available everywhere
// ✅ User fetched only once
// ✅ Common in large applications
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
