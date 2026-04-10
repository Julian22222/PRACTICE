# Generating Random numbers for id, account number, identity number, random numbers

# 1. Math.random()

```JS
//Example:

let account;

 for (let i = 0; i < 7; i++) {
          account += Math.floor(Math.random() * 10);
}

//account - will have 6 digit number

// 👉 Problems of this method:
// - Not unique
// - Predictable
// - Not safe
```

# 2. randomInt()

```JS
//Example:

import { randomInt } from 'crypto';

const num = randomInt(0, 10) //generates a number in this range: 0, 1, 2, 3, 4, 5, 6, 7, 8, 9. 👉 10 is NOT included
//num will have 1 digit number

// randomInt(min, max)
// min → it is smallest possible value: 0, (inclusive)
// max → it is largest possible value: 10, (exclusive)

const otp = randomInt(100000, 1000000); // generates a random 6-digit number using Node.js’s secure random generator.
// randomInt(min, max)
// min → it is smallest possible value: 100000, (inclusive)
// max → it is largest possible value: 999999, (exclusive)

const otp = randomInt(100000, 1000000).toString();  //can use as a string
```

This method:

- ensures fixed length (6 digits in our example)
- Generates a secure random number
- avoids smaller numbers like 1234
- uses crypto-secure randomness (hard to predict)

Usially this method is used to generate:

- OTP (stands for One-Time Password)
- Commonly used for verification codes
- Logging into accounts (banks, email, apps)
- Two-factor authentication (2FA)
- Password reset verification
- Confirming sensitive actions (payments, changing email, etc.)

```JS
✅ Pros:
- Simple and short
- User-friendly (easy to read/type)
- Good for OTPs or small codes

❌ Cons:
- Not guaranteed unique
- You must check database for duplicates
- Collisions become likely at scale
- Easier to guess (especially short ranges)
```

```JS
// 🧠 How it works (simple flow)
// 1. You try to log in or perform an action
// 2. The system generates an OTP (e.g. 482901)
// 3. It is sent to you via:
//     - SMS
//     - Email
//     - Authenticator app
// 4. You enter the OTP
// 5. The system checks if it matches
// 6. If correct → access is granted
// 7. After use or expiration → it becomes invalid


// ⏳ Key characteristics
//  - One-time use only → cannot be reused
//  - Time-limited → usually expires in 30 seconds to 10 minutes
//  - Random → hard to guess
//  - Secure → adds an extra layer of protection
```

### Comparison

```JS
| Feature     | `randomInt` | `Math.random()`   |
| ----------- | ----------- | ---------------   |
| Secure      | ✅ Yes       | ❌ No            |
| Uniform     | ✅ Yes       | ⚠️ Slight bias   |
| Crypto-safe | ✅ Yes       | ❌ No            |
```

# 3. UUID (stands for Universally Unique Identifier)

```JS
//example:
550e8400-e29b-41d4-a716-446655440000
```

```JS
✅ Pros:
- Practically guaranteed unique
- Works well in distributed systems
- No need to check database for duplicates
- Hard to guess → more secure for public IDs
- Standard in backend systems (NestJS, PostgreSQL, etc.)

❌ Cons:
- Long and not user-friendly
- Hard to read or type manually
- Not “pretty” for customer-facing account numbers
```

```JS
//Example:
import { randomUUID } from 'crypto';

const userId = randomUUID();
```
