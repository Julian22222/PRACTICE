# SQL Structure of user's table in Real Database table, Production

add this field for user table

```JS
is_email_verified BOOLEAN DEFAULT FALSE,
is_phone_verified BOOLEAN DEFAULT FALSE,
last_login TIMESTAMP,
failed_login_attempts INT DEFAULT 0,
account_locked_until TIMESTAMP
```

```JS
🔐 1. is_email_verified
👉 Purpose:

User must confirm their email before using account
When it’s used:
    -After registration → send verification link
    -On login:
if (!user.is_email_verified) {
  throw new Error('Verify your email first');
}

// 👉 Highly recommended ✅
```

```JS
📱 2. is_phone_verified

👉 Purpose:
    -SMS verification (OTP)

Useful for:
    -2FA (very important in banking)
    -password recovery
👉 Optional now, important later
```

```JS
🕒 3. last_login
👉 Tracks:
    -last time user logged in

Useful for:
    -security monitoring
    -showing “last login” in UI
    -detecting suspicious activity

👉 Low effort, good value ✅
```

```JS
🚨 4. failed_login_attempts

👉 Counts wrong password attempts

Example logic:
if (passwordWrong) {
  failed_login_attempts += 1;
}
```

```JS
🔒 5. account_locked_until

👉 Works with failed attempts:

// Example:
if (failed_login_attempts >= 5) {
  account_locked_until = NOW() + INTERVAL '15 minutes';
}

On login:
if (now < account_locked_until) {
  throw new Error('Account temporarily locked');
}

// 👉 This is real security, not just theory
// 👉 Strongly recommended ✅
```
