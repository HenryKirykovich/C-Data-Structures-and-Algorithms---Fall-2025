# Work Order Manager (Final Project)

Console-based C# .NET application to manage work orders for a small home-improvement / handyman business. It tracks jobs, clients, payments, unpaid balances, and generates text/HTML invoices.

---

## What I Built (Overview)

**Problem this solves:**  
Small contractors and handymen often track jobs, amounts, and payments in ad-hoc spreadsheets or paper notes. This app centralizes work orders, keeps track of who paid and who still owes money, and provides simple reports for invoicing and tax preparation.

**Core features:**

- Add, list, search, update, and delete work orders
- Track client info, city, description, hours, materials, and payment amount
- Mark orders as paid/unpaid and see unpaid balances by client
- Generate text and HTML invoices for any order
- Search by date ranges (including unpaid-only) with totals for the result set
- Basic statistics by city and overall payments/hours

## How to Run

**Requirements:**

- .NET SDK 9.0 (or compatible installed SDK)
- Windows, macOS, or Linux with a terminal

**Build & Run:**

```powershell
cd "C:\Users\info\Desktop\C-Data-Structures-and-Algorithms---Fall-2025\final_project"
dotnet run
```

**Sample data:**

- `orders.txt` in the `final_project` folder contains sample work orders used by the app.

---

## Using the App (Quick Start)

**Typical workflow:**

1. Start the app with `dotnet run` from the `final_project` folder.
2. Use menu option `1` to add work orders or use existing sample data in `orders.txt`.
3. Use options `2`–`3` to list and search work orders (by client, city, or date range).
4. Use options `7` and `8` to generate invoices and see unpaid balances.

**Input tips:**

- Most string searches (client name, city) are case-insensitive and work with partial matches.
- When asked for an order number or numeric input, invalid input will be rejected with a clear message.
- In many numeric prompts (order number for search/update/delete, invoice generation) you can type `q` to cancel and return safely to the menu.

---

## Data Structures (Brief Summary)

> Full rationale goes in **DESIGN.md**. Here, list only what you used and the feature it powers.

**Data structures used:**

- `Dictionary<int, WorkOrder>` → fast lookup of work orders by order number.
- `List<WorkOrder>` → ordered views for listing, sorting, and searching.
- `List<string>` → distinct clients and cities for re-use when adding new orders.

---

## Manual Testing Summary

> No unit tests required. Show how you verified correctness with 3–5 test scenarios.

**Test scenarios:**

**Scenario 1: Generate invoice for existing order**

- Steps: Start app → option `7` → enter known order number → open generated HTML invoice.
- Expected result: Invoice files appear in `invoices/` with correct client, amounts, and tax-rate information.

**Scenario 2: Find all unpaid balances by client**

- Steps: Start app → option `8`.
- Expected result: Table of clients with unpaid orders, their total due amounts, and involved order numbers.

**Scenario 3: Search unpaid orders for a quarter**

- Steps: Start app → option `3` (Search) → option `5` (By date range, unpaid only) → enter quarter dates.
- Expected result: List of only unpaid orders in that period and total due amount at the bottom.

**Scenario 4: Search all orders for a quarter (gross total)**

- Steps: Start app → option `3` → option `4` (By date range) → enter quarter dates.
- Expected result: All orders in that period with a total at the bottom that matches expected gross revenue.

**Scenario 5: Emergency exit on input**

- Steps: Start app → choose update/delete/invoice options → at order number prompt type `q`.
- Expected result: Operation cancels cleanly and returns to menu without crashing.

---

## Known Limitations

**Limitations and edge cases:**

- Designed as a single-user console app with local JSON file storage (`orders.txt`).
- Tax rates are configured as static combined rates per city and displayed for reference only; they are not a certified tax-calculation engine.

## Comparers & String Handling

**Keys comparer:**

- For lookups and grouping by client or city, `StringComparison.OrdinalIgnoreCase` is used so that `"seattle"` and `"Seattle"` are treated as the same client/city.

**Normalization:**

- Trims user input for names and cities before storing.
- Uses partial, case-insensitive `Contains` for search to make lookups forgiving.

---

## Credits & AI Disclosure

**Resources:**

- Official .NET documentation for `List<T>`, `Dictionary<TKey,TValue>`, and LINQ.
- Washington State Department of Revenue tables for combined city tax rates (used as reference values).

- **AI usage (if any):**  
  Used GitHub Copilot Chat to help design the menu flow, implement search/sort logic, unpaid-balance reports, and invoice HTML layout. All generated code was reviewed, adjusted to class requirements, and tested manually via the scenarios above.

  ***

## Challenges and Solutions

**Biggest challenge faced:**

- Balancing real-world business needs (invoices, unpaid balances, tax awareness) with the constraints of a simple console app and course requirements.

**How you solved it:**

- Iteratively added features (search, invoices, reports) on top of a clean `WorkOrderRepository` and kept data structures simple (`List`, `Dictionary`). Used manual test scenarios and sample data to validate behavior.

**Most confusing concept:**

- Designing flexible search and reporting (by client, city, date ranges, and unpaid only) while avoiding code duplication and keeping sorting/total logic centralized.

## Code Quality

**What you're most proud of in your implementation:**

- The search/reporting flows: unpaid-by-client report, date-range search with totals, and invoice generation that feel realistic for a small business.

**What you would improve if you had more time:**

- Move storage from a JSON file to a real database.
- Add a GUI or web front-end.
- Implement more robust, officially-correct tax calculations with effective dating and changing rates.

## Real-World Applications

**How this relates to real-world systems:**

- Similar to lightweight job, invoicing, or CRM systems used by small contractors, but built with basic .NET collections and console UI.

**What you learned about data structures and algorithms:**

- How simple data structures (`List`, `Dictionary`) plus good key choices (order number, client, city) can support practical features like sorting, searching, grouping, and reporting without overcomplication.

## Submission Checklist

- [ ] Public GitHub repository link submitted
- [ ] README.md completed (this file)
- [ ] DESIGN.md completed
- [ ] Source code included and builds successfully
- [ ] (Optional) Slide deck or 5–10 minute demo video link (unlisted)

**Demo Video Link (optional):**
