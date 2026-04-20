# ON DELETE CASCADE (used rarely)

```JS
    const createAccountsTable = await pool.query(`
        CREATE TABLE accounts (
        account_id SERIAL PRIMARY KEY,
        customer_id INTEGER REFERENCES customers(customer_id) ON DELETE CASCADE,
        account_type VARCHAR(20) NOT NULL,
        account_nr VARCHAR(30) NOT NULL UNIQUE,
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        );
    `);

    const createTransactionsTable = await pool.query(`
        CREATE TABLE transactions (
        transaction_id SERIAL PRIMARY KEY,
        account_id INTEGER REFERENCES accounts(account_id) ON DELETE CASCADE,
        customer_id INTEGER REFERENCES customers(customer_id) ON DELETE CASCADE,
        money_amount DECIMAL(10, 2) NOT NULL DEFAULT 200.00,
        balance DECIMAL(10, 2) NOT NULL DEFAULT 200.00,
        details TEXT,
        transaction_date TIMESTAMP DEFAULT CURRENT_TIMESTAMP
      );`);

    const createAdminsTable = await pool.query(`
        CREATE TABLE admins (
        admin_id SERIAL PRIMARY KEY,
        admin_name VARCHAR(50) NOT NULL,
        email VARCHAR(100) NOT NULL UNIQUE,
        password VARCHAR(255) NOT NULL,
        phone VARCHAR(15) NOT NULL,
        customer_address VARCHAR(255) NOT NULL,
        dob DATE NOT NULL,
        created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
        );
    `);

    const createMessagesTable = await pool.query(`
        CREATE TABLE messages (
        message_id SERIAL PRIMARY KEY,
        customer_id INTEGER REFERENCES customers(customer_id) ON DELETE CASCADE,
        msg_subject VARCHAR(255) NOT NULL,
        msg_status VARCHAR(50) NOT NULL,
        msg_body TEXT NOT NULL,
        msg_created_by VARCHAR(50) NOT NULL,
        sent_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
      );`);
```

In PostgreSQL (psql), ON DELETE CASCADE is not part of a DELETE query itself—it’s something you define when creating a foreign key constraint between tables.

What it does? - ON DELETE CASCADE tells PostgreSQL:

When a row in the parent table is deleted, automatically delete all related rows in the child table.

```JS
Simple example

Tables:

customers (parent)
orders (child)
CREATE TABLE customers (
    id SERIAL PRIMARY KEY,
    name TEXT
);


CREATE TABLE orders (
    id SERIAL PRIMARY KEY,
    customer_id INT REFERENCES customers(id) ON DELETE CASCADE
);
```

What happens in practice

```JS
If you run:
DELETE FROM customers WHERE id = 1;

// Then PostgreSQL will automatically:
// Delete the customer with id = 1
// Also delete all rows in orders where customer_id = 1
// You don’t need to manually delete from orders.
```

```JS
Why it's useful

Without ON DELETE CASCADE:

You’d get an error if child rows exist (foreign key violation), or
You’d have to manually delete child rows first.
With it:
- The database keeps everything consistent automatically.
```

Important caution

It’s powerful but can be dangerous:

One delete can remove a lot of related data
Cascades can chain across multiple tables
So always be sure you really want dependent data gone.

```JS
//Quick analogy
Think of it like:
Delete a user → automatically delete all their posts, comments, etc.
```

# Example with 2 or more tables using ON DELETE CASCADE

```JS
-table1 → parent of table2
-table2 → parent of table3
-table2 and table3 both use ON DELETE CASCADE

/////////
//So conceptually:
table1
  ↓
table2 (ON DELETE CASCADE from table1)
  ↓
table3 (ON DELETE CASCADE from table2)
```

### What happens if you delete from table2?

```JS
If you run:
DELETE FROM table2 WHERE id = X;

// Result:
// The row in table2 is deleted ✅
// PostgreSQL sees that table3 depends on table2 with ON DELETE CASCADE
// It automatically deletes all rows in table3 that reference that row ❌

// What does NOT happen
// Nothing happens to table1 ❗
// Because cascade only works downward (parent → child), not upward.

// So the chain behaves like this:
Deleting from middle table (table2):
table1   ← stays untouched
table2   ← deleted (your query)
table3   ← automatically deleted (cascade)


// Extra clarity: direction matters
// Cascades only go one way:
// Deleting table1 → deletes table2 → deletes table3 (full chain)
// Deleting table2 → deletes table3 only
// Deleting table3 → only deletes table3


// Quick mental model
// Think of it like a tree:
// table1
// └── table2
//       └── table3

// Cut table2 branch → everything below it (table3) falls off
// The root (table1) is unaffected
```
