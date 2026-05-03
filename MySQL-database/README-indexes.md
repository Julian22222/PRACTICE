# SQL indexes

- SQL Indexing important for performance
- Use indexes if you have a lot of rows in your table – it makes SQL find rows faster.
- SQL indexes doesn’t matter with 10 rows, but makes or breaks your app at scale.

# 🧠 What is an index (in simple terms)?

A database index is like the index in a book.

- Without index → PostgreSQL scans the entire table
- With index → PostgreSQL jumps directly to the rows you need

```JS
// For example you have 2 tables:

CREATE TABLE conversations (
    conversation_id SERIAL PRIMARY KEY,
    customer_id INT REFERENCES customers(customer_id) ON DELETE CASCADE,
    assigned_admin_id INT REFERENCES admins(admin_id),
    status VARCHAR(50) DEFAULT 'open', -- open, closed, pending
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
)

CREATE TABLE chat_messages (
    message_id SERIAL PRIMARY KEY,
    conversation_id INT REFERENCES conversations(conversation_id) ON DELETE CASCADE,
    sender_type VARCHAR(20) NOT NULL, -- 'customer' or 'admin'
    sender_id INT NOT NULL, -- either customer_id or admin_id
    message TEXT NOT NULL,
    is_read BOOLEAN DEFAULT FALSE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP
);

//🔥 before you make a query as usual you need to create INDEX
//👉 You don’t change your queries at all.
//👉 You just create the index once, and PostgreSQL decides when to use it.

//add this to your seed.ts file or yourDatabase.sql file
CREATE INDEX idx_conversation_customer ON conversations(customer_id);
// 👉 This helps queries like:
// SELECT * FROM conversations WHERE customer_id = 1;

// Without index:
    // -PostgreSQL checks every row in conversations
    // -Time grows linearly (slow with large data)

// With index:
    // -Uses a B-tree lookup
    // -Finds matching rows in milliseconds


CREATE INDEX idx_messages_conversation_created ON chat_messages(conversation_id, created_at);
//PostgreSQL can: Filter by conversation_id, Already have results sorted by created_at . → avoids extra sorting step → faster

//👉 This helps:
// SELECT * FROM chat_messages WHERE conversation_id = 10 ORDER BY created_at;

// This is critical, because:
    // -Every time you open a chat → you load messages
    // -This query will run constantly


// If admins fetch open chats:
// SELECT * FROM conversations WHERE status = 'open';
// 👉 Add:
CREATE INDEX idx_conversation_status ON conversations(status);
```

```JS
//expanation of
CREATE INDEX idx_conversation_customer ON conversations(customer_id);

👉//CREATE INDEX - you tell PSQL -> “Build a data structure that helps me find rows faster.”
//Without an index, PostgreSQL scans the whole table (full table scan) to find matching rows. With an index, it can jump directly to relevant rows.

👉//idx_conversation_customer <- This is just the name of the index. It can be anything you want (as long as it's unique in the schema)
//Better to use clear naming convention, like: idx_<table>_<column>.
//Example: idx_conversations_customer_id

👉//ON conversations -  you tell PSQL -> “Create this index for the conversations table.” So the index belongs to that table.

👉//(customer_id) <- This is the column being indexed. PostgreSQL builds a structure (usually a B-tree) that maps: customer_id → rows in the table

//////////////////////////////////
//So when you run:
//SELECT * FROM conversations WHERE customer_id = 42;

// Instead of scanning every row, PostgreSQL can:
// -Look up 42 in the index
// -Jump straight to matching rows
```

```JS
// Mental model (simple)
// Think of an index like a book index:

customer_id	   |   row location
     1	       |    row 5
     1	       |    row 20
     2	       |    row 8

//Instead of flipping every page (full scan), you go straight to the entries.
```

```JS
// 🧠 Rule of thumb
// Add an index when:
// - You use WHERE on a column
// - You use JOIN on a column
// - You use ORDER BY


// 🚨 Common beginner mistake.
// Don't add indexes everywhere

// Too many indexes:
// -slow writes
// -waste memory
// 👉 Index only what you query frequently
```

# ⚡ Real-world impact

```JS
Imagine:
- 1 million messages
- 50k conversations

Without index:
Opening a chat could take: 200ms → 2 seconds 😬

With index:
~1–10ms 🚀
```

```JS
//better to use - ORDER BY -> it gives performance (Filters by conversation_id, Already sorted by created_at, Avoids extra sorting step)
SELECT * FROM chat_messages WHERE conversation_id = 10 ORDER BY created_at;
// 👉 Best index for that:
// CREATE INDEX idx_messages_conversation_date ON chat_messages(conversation_id, created_at);
```

# ⚠️ Important trade-offs

Indexes are not “free”.

❌ Downsides:

- Slower INSERT, UPDATE, DELETE (index must be updated)
- Takes more disk space/ more storage

✅ But for your app:

- Chat = read-heavy
- So indexes are 100% worth it
