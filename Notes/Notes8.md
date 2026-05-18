# Express.js and NEST.JS ERROR HADLING

## 👉 NEST.JS ERROR HANDLING

- NestJS handles exceptions automatically
- NEST.JS is built on top of Express/Fastify and already has: exception filters, async exception handling, automatic promise rejection handling
- avoid unnecessary try/catch blocks in NEST.JS
- Don't use try/catch blocks in NEST.JS if you just returning DB/API result, or simply use - BadRequestException, NotFoundException, ConflictException, UnauthorizedException, InternalServerErrorException

### Use TRY/CATCH block in NEST.js if:

- transform the error into a custom exception -> (BadRequestException, NotFoundException, ConflictException, UnauthorizedException, InternalServerErrorException)
- add custom logging
- recover from the error
- or return a fallback value.
- NestJS already has a built-in exception layer, so unhandled errors will bubble up automatically

### try/catch is mostly unnecessary in NEST.JS unless you are:

- transforming database errors
- adding logging
- wrapping unknown errors into Nest exceptions

```JS
Use try/catch when you want to:
    -transform errors
    -handle specific errors
    -Logging errors
    -Recovering/retrying (if DB query fails retry a query)
    -return custom exceptions

Skip try/catch when you just want errors to propagate naturally.


//For example, this is completely fine:
async findOne(id: number): Promise<UserResponseDto> {
  const result = await this.pool.query(
    'SELECT * FROM customers WHERE customer_id = $1',
    [id],
  );

  if (!result.rows.length) {
    throw new NotFoundException('User not found');
  }

  return result.rows[0];
}

//No try/catch needed.
//If pool.query() fails, NestJS will automatically return a 500 error. (if no reult -> will automatically throw an error - 500)

///////////////////////////////////////////////////

//Use try/catch only when needed:
async create(dto: CreateUserDto) {
  try {
    const result = await this.pool.query(...);

    return result.rows[0];

  } catch (error: any) {
    if (error.code === '23505') {
      throw new ConflictException('Email already exists');  //catching specific error
    }

    throw new InternalServerErrorException();
  }
}

//This is useful because you're converting a PostgreSQL error into a meaningful HTTP exception.
```

Try to use NestJS exceptions

```JS
//For example:
throw new NotFoundException('User not found');

//returns:

{
  "statusCode": 404,
  "message": "User not found",
  "error": "Not Found"
}

//if you known business errors → throw one of Nest exceptions
```

```JS
//Use specific exceptions when you know the situation:
//throw meaningful Nest exceptions only when you understand the failure

✅BadRequestException
→ invalid input

✅NotFoundException
→ entity does not exist

✅ConflictException
→ duplicate email / unique constraint

✅UnauthorizedException
→ auth problems

✅InternalServerErrorException
→ unexpected DB/server issue
```

```JS
//🔥 A good pattern is catching database-specific errors and converting them:

try {
  return await this.pool.query(query, values);
} catch (error: any) {       //if you need to catch specific error.code
  if (error.code === '23505') {
    throw new ConflictException('Email already exists');
  }

  throw new InternalServerErrorException();
}

```

```JS
//❌ Bad PRACTICE - using TRY/CATCH block here

  async findOne(id: number): Promise<UserResponseDto> {

try {
    const userFromDB = await this.pool.query(
      `SELECT customer_id, first_name, last_name,
        email, phone, customer_address, dob, created_at FROM customers WHERE customer_id = $1`,
      [id],
    );

    if (!userFromDB.rows.length) {
      throw new NotFoundException('User not found');
    }

    const data = userFromDB.rows[0];

    const userDataNoPassword: UserResponseDto = {
      customer_id: data.customer_id,
      first_name: data.first_name,
      last_name: data.last_name,
      email: data.email,
      phone: data.phone,
      customer_address: data.customer_address,
      dob: data.dob,
      created_at: data.created_at,
    };

    return userDataNoPassword;
    }catch (error) {            //HERE don't neew catch block
        throw new InternalServerErrorException(error.message);  //error.message may expose internal DB details to clients.
    }
  }
```

# throw error in NEST.JS

```JS
throw error;  //<- in NEST.JS
//error is not a NestJS HTTP exception (NotFoundException, BadRequestException, etc.),


//if you use - "throw error" in Nest it will usually return:
{
  "statusCode": 500,
  "message": "Internal server error"
}

///Example:
try {
  await this.pool.query(query);
} catch (error) {
  throw error;
}

//If pool.query() throws a PostgreSQL error, Nest treats it as an unknown server error → HTTP 500
```

## 👉 EXPRESS.JS

- In express.js i need to use try/catch blocks all the time when you make a query to Database
- in plain Express.js you usually need try/catch more often than in NestJS. Because Express does not automatically handle async errors well unless you add middleware or wrappers.

```JS
//Example in Express:

app.get('/users', async (req, res) => {
  const result = await pool.query('SELECT * FROM users');

  res.json(result.rows);
});

//If pool.query() throws:
// -the request may hang
// -or Express may crash
// -or the error won't reach your error middleware correctly
// So developers usually do:
app.get('/users', async (req, res, next) => {

  try {
    const result = await pool.query('SELECT * FROM users');
    res.json(result.rows);

  } catch (error) {
    next(error);
  }
});

//or use async wrappers.

```
