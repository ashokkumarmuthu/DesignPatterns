# Builder Pattern Challenge: Fluent SQL & Analytical Query Builder 🔍

### Scenario
You are developing an Object-Relational Mapping (ORM) framework or dynamic report reporting service. Database queries can become extremely complex with dynamic conditions, table joins, group-by aggregates, pagination, and sorting:
```sql
SELECT u.Id, u.Name, COUNT(o.Id) AS TotalOrders 
FROM Users u 
INNER JOIN Orders o ON u.Id = o.UserId 
WHERE u.IsActive = 1 AND o.OrderDate >= '2026-01-01' 
GROUP BY u.Id, u.Name 
HAVING COUNT(o.Id) > 5 
ORDER BY TotalOrders DESC 
LIMIT 20 OFFSET 0;
```

Constructing such SQL query strings manually using raw string manipulation leads to SQL injection vulnerabilities, syntax errors, and messy conditional logic.

---

### What it teaches:
* **The Builder Design Pattern:** Constructing complex objects step-by-step using a fluent, method-chaining API.
* **Separation of Construction from Representation:** Building clean SQL statements or parameterized queries safely.

---

### Core Requirements
1. **Product Model (`SqlQuery`):**
   - Stores TargetTable, SelectedColumns, Joins, WhereConditions, GroupByColumns, HavingConditions, OrderByColumn, IsDescending, Limit, Offset.
   - Provides `ToExecutableSql()` which renders the formatted SQL string.
2. **Builder (`SqlQueryBuilder`):**
   - Fluent API methods: `Select()`, `From()`, `InnerJoin()`, `Where()`, `GroupBy()`, `Having()`, `OrderBy()`, `Paginate()`.
   - `Build()` validates table presence and constructs the final `SqlQuery` instance.
