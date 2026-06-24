# InventoryHub — Activity 4 Reflection

## Overview

This capstone project built **InventoryHub**, a full-stack inventory management application using **Blazor WebAssembly** (front-end) and an **ASP.NET Core Minimal API** (back-end). Across four activities, the focus moved from initial integration, through debugging, JSON structuring, and finally performance optimization.

---

## How Copilot Assisted Each Phase

### Activity 1: Integration Code

Copilot helped scaffold the connection between the Blazor client and the Minimal API. It generated:

- `HttpClient` registration in `Program.cs` with the correct API base address
- The `FetchProducts.razor` component with `OnInitializedAsync` lifecycle logic
- JSON deserialization using `GetFromJsonAsync` / `JsonSerializer`

This established the foundation: the front-end could call `/api/products` and render a product list.

### Activity 2: Debugging

When integration broke, Copilot was useful for diagnosing three common full-stack issues:

| Issue | Symptom | Fix |
|-------|---------|-----|
| Incorrect API route | 404 errors | Updated endpoint to `/api/productlist` |
| CORS restrictions | Browser blocked cross-origin requests | Added `UseCors` with `AllowAnyOrigin` |
| Malformed / mismatched JSON | Blank names, `$0` prices | Added `try/catch` and case-insensitive deserialization |

Copilot accelerated root-cause analysis by suggesting CORS middleware configuration and explicit JSON error handling patterns.

### Activity 3: JSON Structuring

Copilot helped design a standardized API response with nested objects:

```json
{
  "id": 1,
  "name": "Laptop",
  "price": 1200.5,
  "stock": 25,
  "category": { "id": 101, "name": "Electronics" }
}
```

It also suggested matching `Product` and `Category` model classes on the client so deserialization remained type-safe.

### Activity 4: Performance Optimization

Copilot guided refactoring toward maintainable, efficient patterns:

**Front-end**
- Extracted `ProductService` to centralize API calls
- Added `ProductCache` (singleton) to avoid redundant HTTP requests when revisiting the Products page
- Moved models into `Models/Product.cs` to remove duplication

**Back-end**
- Introduced `IMemoryCache` with a 5-minute expiration for `/api/productlist`
- Moved static product data into `ProductCatalog` so the endpoint handler stays lean

---

## Challenges and How Copilot Helped

### 1. CORS middleware startup failure

The inline `UseCors` policy from the activity instructions caused a startup crash because `AddCors()` was not registered in services. Copilot identified the missing `ICorsService` dependency and suggested adding `builder.Services.AddCors()`.

### 2. camelCase vs PascalCase deserialization

The API returned `name` and `price`, but the client expected `Name` and `Price`. Products rendered as `- $0`. Copilot recommended `PropertyNameCaseInsensitive = true` in `JsonSerializerOptions`, which immediately fixed the display.

### 3. Redundant API calls on navigation

Each visit to `/fetchproducts` triggered a new HTTP request. Copilot suggested a cached service pattern — a singleton `ProductCache` checked before calling the API — which reduces network traffic during normal browsing.

---

## What I Learned About Using Copilot Effectively

1. **Be specific about context.** Prompts that mention the framework (Blazor WASM), endpoint path, and error message produce more accurate suggestions than generic "fix my API" requests.

2. **Iterate, don't accept blindly.** Copilot's first answer for CORS omitted `AddCors()`. Verifying against compiler/runtime errors caught this quickly.

3. **Use Copilot for patterns, not architecture decisions.** It excels at boilerplate (DI registration, `try/catch`, model classes) while you retain control over caching strategy and project structure.

4. **Pair Copilot with testing.** After each change, running both apps and checking the browser Network tab confirmed that optimizations worked and did not introduce regressions.

5. **Full-stack awareness matters.** A front-end fix (case-insensitive JSON) and a back-end fix (CORS) are often needed together. Copilot is most effective when you describe the full request/response flow.

---

## Performance Testing Notes

To measure improvements:

1. Open browser DevTools → **Network** tab
2. Load `/fetchproducts` — note the `/api/productlist` request
3. Navigate away (e.g., Home) and return to Products
4. **Before optimization:** a new API call on every visit
5. **After optimization:** cached data is served from `ProductCache` with no additional network request (unless **Refresh** is clicked)

On the server, `IMemoryCache` avoids rebuilding the product array on every request within the 5-minute cache window.

---

## Conclusion

Copilot served as a collaborative accelerator throughout InventoryHub — from wiring up `HttpClient` to structuring nested JSON and applying caching patterns. The most valuable workflow was: describe the problem clearly, apply Copilot's suggestion, build and test, then refine. This mirrors effective full-stack development practice, with AI assisting repetitive and diagnostic tasks while the developer validates correctness and design.
