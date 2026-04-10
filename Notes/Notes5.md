# How to Store passwords

❌ DON'T store passwords as plain text in Database

## use hash password

```JS
//Example:
//Use bcrypt:

import * as bcrypt from 'bcrypt';

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
