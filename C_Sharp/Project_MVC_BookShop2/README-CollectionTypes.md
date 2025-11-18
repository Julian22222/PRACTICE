# IEnumerable<T>, ICollection<T>, IList<T>, and List<T>

# IEnumerable

What it is?

- The most basic read-only sequence of items.
- Supports iteration only (foreach), LINQ queries.
- No Add/Remove, no indexing, no count modification.

Where you use it

✔ Return data from services / repositories when the caller should not modify the collection.
✔ Pass data to views (e.g., IEnumerable<Book> for a list page).
✔ When using LINQ to Entities (EF Core), because it can stream results.

✅ Pros

- Best abstraction for read-only collections.
- Lazy evaluation possible → can defer database execution.

❌ Cons

- No random access by index.
- Can't modify the collection.

# ICollection

What it is

- A more powerful contract: includes:
  - Add, Remove, Count, Contains.

Where you use it

✔ Navigation properties with EF Core, e.g.:

public ICollection<Book> Books { get; set; }

Because EF Core uses it to:

- track relationships
- add/remove items
- load related data.

✔ When you want the caller to be able to modify the collection.

✅ Pros

- Balanced, used heavily by EF Core.
- Signals: "This is a collection you can modify."

❌ Cons

- No indexing like a list.

# IList

What it is

- Extends ICollection<T> and adds indexers (this[int index]).
- Ordered, index-based access.

Where you use it

✔ When you need indexed operations, e.g.:

- modifying items at specific positions
- reordering items
- UI lists where index matters

But in modern .NET, IList is used less often.

✅ Pros

Allows item access by index.
More specific than ICollection.

❌ Cons

Over-abstraction — most code does not need indexing.

# List

What it is

- A concrete implementation of both ICollection<T> and IList<T>.
- Most commonly used concrete list type.

Where you use it

✔ Inside your method or class when you need a real list implementation.
✔ When you need fast indexed access or want to modify items freely.
✔ When EF Core materializes queries (returns List<T> by default).

✅ Pros

- Flexible and fast.
- Full-featured collection with indexing.

❌ Cons

- Exposes more capabilities than often needed → not ideal for public API returns.

# 🔥 Quick Summary Table

```C#
Type          |  Mutable?  |  Indexed? |  Typical Use

IEnumerable   |   ❌ No    |  ❌ No    |    Read-only data to views or services
ICollection   |   ✔ Yes    |  ❌ No    |    EF Core navigation properties, modifiable collections
IList         |   ✔ Yes    |  ✔ Yes    |    When list ordering + indexing matters
List          |   ✔ Yes    |  ✔ Yes    |    Concrete implementation inside business logic
```

🧭 When to Use What (Real ASP.NET Core Guidance)

→ RETURN TYPES in Controllers / Services

- Use IEnumerable
  Because controllers and views normally read only.

public IEnumerable<Book> GetAllBooks()
→ EF Core ENTITY Navigation Properties

- Use ICollection

public ICollection<Book> Books { get; set; }
→ INTERNAL METHOD LOGIC

- Use List

var books = new List<Book>();
→ APIs Where Order or Index Is Important

- Use IList

###### Rare, but possible for sortable UI components.

🧨 What NOT to Do

Avoid returning List from repositories/services unless you need to guarantee the concrete type:

public List<Book> GetBooks() ❌ // too concrete

Because it exposes unnecessary mutability and implementation details.

##### 🏁 Final Practical Rule of Thumb

- Default to IEnumerable<T> for results.
- Use ICollection<T> on EF Core navigation properties.
- Use List<T> internally inside methods.
- Use IList<T> only when index-based operations are required.
