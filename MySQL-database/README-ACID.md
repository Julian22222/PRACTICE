# ACID = rules for safe transactions. Transactions = how you group operations (like creating data and adding data to other tables)

ACID is a set of properties that guarantee reliable database transactions. It stands for:

```JS
1. Atomicity
This is the part you’re thinking of. A transaction is all or nothing.

If you:
-create a user in one table
-create a profile in another
…and the second step fails, the first one is rolled back. No partial data is saved.

2. Consistency
After a transaction completes, the database must remain valid according to its rules:

-constraints (e.g., foreign keys)
-data types
-business rules
If something would break consistency, the transaction fails.

3. Isolation
Transactions don’t interfere with each other.
If two users are writing data at the same time, the database ensures they don’t corrupt each other’s results (depending on isolation level).

4. Durability
Once a transaction is committed, it stays saved—even if the server crashes right after.
```

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

  const client = await this.pool.connect();   /////-👉 Get a dedicated database connection (client). This is important: a transaction must use one single connection.

  try {                                    /////👉 Start a block where you’ll run your queries. If anything fails, execution jumps to catch.
    await client.query('BEGIN');           ////👉 Tell the database: “Start a transaction now.” From this point:- nothing is permanently saved yet, - changes are temporary until you commit

    // 1. Create user
    const newUser = await client.query(`INSERT INTO customers (...) VALUES (...) RETURNING *`,[...]);  ///👉 Try to insert a user (but not saved permanently yet).

    const user = newUser.rows[0];

    // 2. Create account
    const newAccount = await this.accountsService.create(newAccountData, client);   //👉 Try to insert a account (but not saved permanently yet). / 👈 pass client! to the accountsService

    // 3. Create transaction
    await this.transactionsService.create(newTransactionData,client);     //👉 Try to insert a transaction (but not saved permanently yet). / 👈 pass client! to the transactionsService

    // 4. Insert message
    await this.adminService.sendMessageToUser(newMsgData, client);          ///👉 Try to insert a message (but not saved permanently yet). / 👈 pass client! to the adminService

    await client.query('COMMIT');  //Add this before returning  👉 Tell the database: “Everything worked — save all changes permanently.” Now all inserts become real and visible.

    return .....;

  } catch (error) {                 //👉 If ANY query above fails, execution comes here.
    await client.query('ROLLBACK'); // 🔴 VERY IMPORTANT, 👉 Tell the database: “if Something fails (if any query fails)— undo everything.” All previous inserts are completely erased. Database stays clean
    throw error;
  } finally {                       //👉 This runs no matter what (success or failure).
    client.release(); // 🔴 ALWAYS release, 👉 Give the connection back to the pool. If you forget this → your app can run out of connections.
  }
}

-----
//in accountsService, transactionsService, adminService add this logic to handle - "client" argument
import { PoolClient, Pool } from 'pg';
import { PG_POOL } from '../database/database.module';

export class AccountsService {
  constructor(
    @Inject(PG_POOL) private readonly pool: Pool,
    private readonly adminService: AdminService,
    private readonly transactionsService: TransactionsService,
  ) {}

  async create(createAccountDto: CreateAccountDto, client?: PoolClient) {  //client - must be "PoolClient" data type
    const executor = client ?? this.pool;   //if client is not existing use - this.pool, else use client

    try {
      const result = await executor.query(`INSERT INTO accounts.....`,[...]);
    //some code
    }
    //some code
  }
}

// ⚠️ VERY important detail (most people miss this)
// Your other services:
// - accountsService
// - transactionsService
// - adminService

// 👉 MUST also use the same client

-------------------------------------------------------------------
if you are using raw SQL you need to write ->
in await client.query('BEGIN'); - you need to write 'BEGIN', and
in await client.query('COMMIT'); - you need to write 'COMMIT', and
in await client.query('ROLLBACK'); - you need to write 'ROLLBACK'

These are actual SQL commands:
'BEGIN' → start transaction
'COMMIT' → save changes
'ROLLBACK' → undo changes
So with client.query(...), you must write them.


💡 Simple mental model

BEGIN → start recording actions
queries → do work
COMMIT → save everything
ROLLBACK → pretend nothing ever happened


// ⚠️ But there’s an alternative (important)
// If you use a library/ORM, it usually handles this for you. – (Prisma, TypeORM, etc)

// Example (cleaner approach):
// await this.db.transaction(async (client) => {
//   await client.query(`INSERT INTO users ...`);
//   await client.query(`INSERT INTO accounts ...`);
// });

// 👉 Behind the scenes it automatically does:
// BEGIN
// COMMIT (if success)
// ROLLBACK (if error)
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

```JS
//real example from Bank/bank-api/src/users/users.service.ts
//When we create a user -> it creates new account -> add new transaction -> add new welcome message

async create(createUserDto: CreateUserDto): Promise<UserResponseDto> {

  const client = await this.pool.connect(); //connection to your DB, It should be one single connection for all queries if you use ACID

  //some code
  try {
    await client.query('BEGIN');

    const newUser = await client.query(`INSERT INTO customers...`,[...]);
    //some code

    newAccount = await this.accountsService.create(accObj, client); //use accountsService in usersService.ts file. I pass data to create new account + client

    //some code

     const newTransaction = await this.transactionsService.create(transObj,client);  //use transactionsService in usersService.ts file. I pass data to create new transaction + client

    //some code

    const createNewMsg = await this.adminService.sendMessageToUser(newMsg,client);  //use adminService in usersService.ts file. I pass data to create new message + client

    await client.query('COMMIT');  //<-- MUST be before you return

    return new_user;
    } catch (error) {
      await client.query('ROLLBACK');
      console.error('Error creating user:', error);
      throw error;
    } finally {
      client.release();
    }
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
