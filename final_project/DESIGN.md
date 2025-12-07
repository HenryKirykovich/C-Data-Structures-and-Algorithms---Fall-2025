# Project Design & Rationale

This document explains the design of the **Work Order Manager** console app: core entities, data structures, performance considerations, and intentional tradeoffs.

---

## Data Model & Entities

**Core entities:**

- **`WorkOrder`** – one job for a client, including:
	- identity (`OrderNumber`), work date, description, hours worked
	- client details (name, email, phone, address, city)
	- financial fields (materials, total `PaymentAmount`, flags for paid/tax included)
	- bookkeeping fields (payment method, invoice date, payment date)

- **`Contractor`** – information about the business issuing invoices:
	- legal/DBA name, owner name
	- mailing address
	- license number
	- contact info (phone, email)

- **`WorkOrderRepository`** – not a data entity itself, but the in-memory container for all work orders plus logic to load/save from `orders.txt`.

**Identifiers (keys) and why they're chosen:**

- **Primary key for orders: `OrderNumber : int`**
	- Easy to read over the phone or in a text message.
	- Used as the invoice number and printed on all outputs.
	- Maps naturally to `Dictionary<int, WorkOrder>` for O(1) lookups.
- **No composite or string keys** for work orders:
	- Client names and addresses are not guaranteed unique or stable.
	- Integers avoid cultural issues around casing, accents, or whitespace.

---

## Data Structures — Choices & Justification

Below are the main in-memory structures and why they were chosen.

### Structure #1 – `Dictionary<int, WorkOrder>`

**Chosen Data Structure:**

- `Dictionary<int, WorkOrder>` (index of all work orders by order number).

**Purpose / Role in App:**

- Drives all **by-id operations**:
	- search by order number
	- update an order
	- delete an order
	- generate an invoice for a specific order
- Acts as the authoritative in-memory index that backs the JSON `orders.txt` file.

**Why it fits:**

- Typical access pattern is: *given order number → fetch order*.
- `Dictionary` provides expected **O(1)** average time for add / lookup / remove.
- Keys are primitive `int` values; hashing is cheap and collisions are rare.
- The number of orders in this educational app is modest (hundreds, maybe low thousands), so memory overhead is negligible.

**Alternatives considered:**

- `List<WorkOrder>` only:
	- Would require a linear scan for each by-id lookup (`O(n)`), which is unnecessary when order numbers are unique.
- `SortedDictionary<int, WorkOrder>`:
	- Ordered keys are not required for this index because we sort views over `List<WorkOrder>` instead.
	- Extra tree complexity is not needed here.

---

### Structure #2 – `List<WorkOrder>`

**Chosen Data Structure:**

- `List<WorkOrder>` for all listing, searching, and reporting operations.

**Purpose / Role in App:**

- Powers **user-facing views and reports**:
	- main "List all work orders" flow with different sort orders
	- search results by client, city, and date range
	- reporting for unpaid balances and totals by client
- Serves as the common input for LINQ filters (`Where`), groupings (`GroupBy`), and sorts (`OrderBy` / `OrderByDescending`).

**Why it fits:**

- Natural structure for **ordered sequences** displayed to the user.
- `List<T>` provides:
	- `O(1)` indexed access
	- `O(n log n)` sorting using the built-in sort, which is fine for small/medium data sizes
- Plays well with LINQ to express filters and grouping logic concisely.

**Alternatives considered:**

- `LinkedList<WorkOrder>`:
	- Provides no benefit here; random access and sorting would be worse.
- `ObservableCollection<WorkOrder>`:
	- Useful for UI data binding, but this is a console app with no UI framework.

---

### Structure #3 – Derived collections for distinct values

**Chosen Data Structure:**

- Temporary `List<string>` / `HashSet<string>` for distinct client names and cities.

**Purpose / Role in App:**

- Help drive **user prompts** when adding or editing work orders:
	- show a list of existing clients
	- show a list of existing cities
- Encourage consistency in spelling (`"Seattle"` vs `"Seatle"`).

**Why it fits:**

- Distinct lists are relatively small and can be recomputed from all orders in memory.
- `HashSet<string>` helps de-duplicate values cheaply; `List<string>` gives an ordered display.

**Alternatives considered:**

- Maintaining separate persistent tables for cities/clients:
	- Overkill for a single-file project.
	- Harder to keep in sync and serialize.

---

## Comparers & String Handling

**Comparer choices:**

- **For keys:**
	- `OrderNumber` is an `int`, so the default numeric comparer is used.

- **For display sorting and search:**
	- When ordering by `ClientName` or `City`, the app uses `StringComparison.OrdinalIgnoreCase`.
	- When searching by client or city (`Contains`), the same case-insensitive comparison is used.

**Normalization rules:**

- User input for names, cities, and addresses is:
	- `Trim`med to remove leading/trailing whitespace.
	- Stored as entered (no forced upper/lower case), but compared using case-insensitive rules.
- This keeps the original spelling on screen and invoices, while making searches more forgiving.

**Bad key examples avoided:**

- **Client name as a key:**
	- Not used as the primary key because clients may share names or change how they want their name printed.
- **City as a key for tax:**
	- City is used as a lookup input but never as a unique identifier for work orders.
	- The app is careful not to assume that city values are globally unique.

---

## Performance Considerations

**Expected data scale:**

- Target scale is on the order of **dozens to low hundreds** of work orders for course testing.
- With some growth, the design still works comfortably into the low thousands.

**Performance bottlenecks identified:**

- All operations are in-memory and file-backed; there is **no remote latency**.
- The slowest operations are full scans over the list for reporting (e.g., grouping or filtering):
	- For this scale, `O(n)` passes are acceptable.
	- JSON load/save happens only at startup and when quitting, not on every operation.

**Big-O analysis of core operations:**

- **Add:**
	- Insert into dictionary and list: average **O(1)** for dictionary add, **O(1)** amortized for list append.
- **Search:**
	- By order number: **O(1)** via dictionary.
	- By client/city/date range: **O(n)** scan over list.
- **List:**
	- Show all orders: **O(n log n)** for sorting using in-memory `List<T>.Sort`.
- **Update:**
	- Lookup by id (**O(1)**) then in-place mutation.
- **Delete:**
	- Dictionary remove: **O(1)** average.
	- Removing from list by index: **O(n)** in worst case due to shifting, but still acceptable at this data size.

---

## Design Tradeoffs & Decisions

**Key design decisions:**

- **File-based JSON storage instead of a database:**
	- Keeps the project easy to run (`orders.txt` lives next to the executable).
	- Lets students open and inspect data without any tools beyond a text editor.

- **In-memory processing with simple collections:**
	- All search and reporting logic uses LINQ on lists.
	- This focuses on core C# collection skills rather than external frameworks.

- **Single `WorkOrder` model with flags:**
	- Fields like `IsPaid` and `IsSalesTaxIncluded` are simple booleans instead of nested state machines.
	- Clear enough for classroom use and portfolio demonstration.

- **HTML invoice rendering isolated in `InvoiceRenderer`:**
	- Keeps UI/formatting concerns out of `Program.cs`.
	- Makes it easier to evolve invoice layout later without touching core logic.

**Tradeoffs made:**

- **Simplicity over maximum performance:**
	- For realistic small-business data sizes, the cost of a few `O(n)` scans is trivial.
	- The payoff is very readable code that clearly shows intent (filter, sort, group).

- **Educational tax handling vs production accuracy:**
	- The app stores real combined tax rates by city and shows them on invoices.
	- It does not attempt to compute exact tax lines or handle edge cases (returns, partial payments, rule changes).

- **Console UI instead of GUI:**
	- Faster to build and easier to grade.
	- Real-world users would probably prefer a web or desktop UI, but that would distract from data-structure learning goals.

**What you would do differently with more time:**

- Introduce automated tests for repository, search, and invoice rendering.
- Replace the JSON file with a small database (e.g., SQLite) and add richer queries.
- Extract a service layer between `Program` and the repository to further separate concerns.
- Enhance invoice rendering to accurately break out subtotal, tax amount, and total using robust, tested tax rules.

This design favors clear, idiomatic C# collections and a realistic small-business workflow while staying within the scope of a data-structures-focused course project.
