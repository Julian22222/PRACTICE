# PSQL column_names Notes

You can use camelCase column names, but snake_case is the convention for PostgreSQL, and there's a real reason behind it.

## Why it feels like a rule in PostgreSQL

PostgreSQL folds unquoted identifiers to lowercase. So if you create a column as userId, Postgres stores it as userid. To preserve the camelCase, the column must be created with double quotes ("userId"), and then every raw query must quote it forever:

```JS
//✅ Works
SELECT "userId" FROM orders;

//❌ Fails: column "userid" does not exist
SELECT userId FROM orders;
```

That's the pain point. It's legal, just annoying, and easy to get wrong in hand-written SQL, migrations, and BI tools.

### What your ORM does

- TypeORM with synchronize or migrations will create "userId" (quoted) by default if your entity property is userId. It works fine through the ORM, but raw queries need quotes.
- Prisma lets you keep camelCase in the model and map to snake_case in the DB with @map("user_id").
- Sequelize has an underscored: true option.

## Best practice: camelCase in code, snake_case in the DB

With TypeORM, you can keep your entity properties camelCase and map explicitly:

```JS
@Column({ name: 'user_id' })
userId: number;
```

Or apply it globally with a naming strategy, so you don't repeat yourself:

```JS
import { SnakeNamingStrategy } from 'typeorm-naming-strategies';

TypeOrmModule.forRoot({
  // ...
  namingStrategy: new SnakeNamingStrategy(),
});
```

With that, userId in your entity automatically becomes user_id in the table.

### Other databases

MySQL column names are case-insensitive, so camelCase causes no quoting issues there (though table names can be case-sensitive depending on the OS). SQL Server is similar. Still, snake_case is the safe, portable convention.

So: not required, but strongly recommended for Postgres. If you're starting a project, set up the naming strategy once and you'll never think about it again.
