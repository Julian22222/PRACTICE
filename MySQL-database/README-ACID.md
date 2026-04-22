# 💡 What is a transaction?

- A database transaction ensures: 👉 All operations succeed OR none of them are saved
- This is called: 🔑 ACID principle (specifically “Atomicity”)

### Example

```JS
//Bank/bank-api/user.service.ts file -  line 89

My create() method does multiple dependent operations:

-Insert user
-Create account
-Create transaction (initial £200)
-Insert welcome message

//Right now, each step runs independently.
//my remove() method also needs a transaction
-----------------------------------------------------------

❌ What can go wrong (real scenario)

Imagine this sequence:
✅ User created
✅ Account created
❌ Transaction fails (e.g. DB error)

// 👉 Result:

// User exists
// Account exists
// BUT no initial balance

// Your system is now inconsistent.

---------------------------------------------------------------

Another bad scenario:
✅ User created
❌ Account creation fails

// 👉 Result:
// User exists with NO account → broken business logic
```

```JS
✅ What should happen instead
With a transaction:

Start transaction
→ Create user
→ Create account
→ Create transaction
→ Insert message
If ALL succeed → COMMIT ✅
If ANY fails → ROLLBACK ❌ (undo everything)

---------------------------------------
❌ Without transaction

User ✔
Account ✔
Transaction ❌
→ Broken system

----------------------------------------
✅ With transaction

User ✔
Account ✔
Transaction ❌
→ EVERYTHING rolled back
→ System stays clean
```

# 🔧 How to implement it (PostgreSQL + pg Pool)

You must use a client, not this.pool.query() directly.

```JS
//✅ Correct implementation

async create(createUserDto: CreateUserDto): Promise<UserResponseDto> {
  const client = await this.pool.connect();

  try {
    await client.query('BEGIN');

    // 1. Create user
    const newUser = await client.query(
      `INSERT INTO customers (...) VALUES (...) RETURNING *`,
      [...]
    );

    const user = newUser.rows[0];

    // 2. Create account
    const newAccount = await this.accountsService.create(
      { customer_id: user.customer_id, ... },
      client, // 👈 pass client!
    );

    // 3. Create transaction
    await this.transactionsService.create(
      { account_id: newAccount.account_id, ... },
      client,
    );

    // 4. Insert message
    await client.query(
      `INSERT INTO messages (...) VALUES (...)`,
      [...]
    );

    await client.query('COMMIT');

    return this.mapToUserResponse(user);

  } catch (error) {
    await client.query('ROLLBACK'); // 🔴 VERY IMPORTANT
    throw error;
  } finally {
    client.release(); // 🔴 ALWAYS release
  }
}


// ⚠️ VERY important detail (most people miss this)
// Your other services:
// - accountsService
// - transactionsService

// 👉 MUST also use the same client

---------------------------------------------

// ❌ Wrong
await this.accountsService.create(data); // uses pool internally
//👉 This breaks the transaction!


// ✅ Correct
// Pass the client:
await this.accountsService.create(data, client);

//Then inside AccountsService:
async create(data, client: PoolClient) {
  return client.query(...);
}
```

# 🔥 Why this matters (real-world impact)

Without transactions:

- ata corruption
- Impossible debugging
- Financial inconsistencies (VERY BAD in banking apps)

With transactions:

- Data integrity guaranteed
- Safer system
- Easier to reason about failures

# 🧠 Rule of thumb

👉 If multiple DB operations depend on each other → use a transaction

#### ✨ Final takeaway

Your current code works… until something fails.

👉 Transactions turn your code from “works most of the time” into “always correct”.
