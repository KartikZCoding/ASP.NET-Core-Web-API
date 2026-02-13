# 🌐 Introduction to ASP.NET Core Web API

A comprehensive guide to understanding Web APIs, their evolution, and practical implementation using ASP.NET Core.

---

## 📖 Table of Contents

1. [Introduction](#1-introduction)
2. [Evolution of Web API](#2-evolution-of-web-api)
3. [What is Web API?](#3-what-is-web-api)
4. [Why Web API?](#4-why-web-api)
5. [Web API Request & Response](#5-web-api-request--response)
6. [HTTP Verbs](#6-http-verbs)
7. [Creating Your First Endpoint](#7-creating-your-first-endpoint)
8. [Why and How to Use DTOs](#8-why-and-how-to-use-dtos)
9. [HttpPost – Creating Resources](#9-httppost--creating-resources)
10. [CreatedAtRoute – Proper POST Response](#10-createdatroute--proper-post-response)
11. [HttpPut – Updating Resources](#11-httpput--updating-resources)
12. [HttpPatch – Partial Updates](#12-httppatch--partial-updates)
13. [Model Validation – Preventing Invalid Data](#13-model-validation--preventing-invalid-data)
14. [Built-in Validation Attributes](#14-built-in-validation-attributes)
15. [Custom Validation Attributes](#15-custom-validation-attributes)
16. [Dependency Injection in Web API](#16-dependency-injection-in-web-api)
17. [Built-in Logger in Web API](#17-built-in-logger-in-web-api)
18. [Serilog – Advanced Logging](#18-serilog--advanced-logging)
19. [Entity Framework Core](#19-entity-framework-core)
    19.1. [Creating Foreign Keys in EF Core](#191-creating-foreign-keys-in-ef-core)
    19.2. [Entity Framework Database First Approach](#192-entity-framework-database-first-approach)
20. [AutoMapper – Simplifying Object Mapping](#20-automapper--simplifying-object-mapping)
21. [Repository Design Pattern](#21-repository-design-pattern)
22. [Generic Repository Pattern (Advanced)](#22-generic-repository-pattern-advanced)
23. [Security in Web API](#23-security-in-web-api)
24. [CORS – Cross-Origin Resource Sharing](#24-cors--cross-origin-resource-sharing)
25. [CORS Scenarios](#25-cors-scenarios)
26. [Enabling CORS in Web API](#26-enabling-cors-in-web-api)
27. [JWT – JSON Web Tokens](#27-jwt--json-web-tokens)
28. [JWT Authentication in Swagger UI](#28-jwt-authentication-in-swagger-ui)
29. [Why We Need Issuer & Audience in JWT](#29-why-we-need-issuer--audience-in-jwt)
30. [Common API Response](#30-common-api-response)
31. [Role-Based Authentication – Database Setup](#31-role-based-authentication--database-setup)
    31.1. [Creating User-Role Mapping in EF](#311-creating-user-role-mapping-in-ef)
    31.2. [Creating UserType Table in EF](#312-creating-usertype-table-in-ef)
    31.3. [Creating RoleDTO](#313-creating-roledto)
    31.4. [Creating Role Creation Endpoint](#314-creating-role-creation-endpoint)
    31.5. [Creating Role List Endpoint](#315-creating-role-list-endpoint)
    31.6. [Creating Role Update Endpoint](#316-creating-role-update-endpoint)
    31.7. [Creating Role Delete Endpoint](#317-creating-role-delete-endpoint)
32. [Role Privilege CRUD – Managing Permissions per Role](#32-role-privilege-crud--managing-permissions-per-role)
    32.1. [RolePrivilege Entity & DTO](#321-roleprivilege-entity--dto)
    32.2. [Creating RolePrivilege Endpoint](#322-creating-roleprivilege-endpoint)
    32.3. [Getting All RolePrivileges](#323-getting-all-roleprivileges)
    32.4. [Getting RolePrivileges by RoleId](#324-getting-roleprivileges-by-roleid)
    32.5. [Getting RolePrivilege by Id & Name](#325-getting-roleprivilege-by-id--name)
    32.6. [Updating a RolePrivilege](#326-updating-a-roleprivilege)
    32.7. [Deleting a RolePrivilege](#327-deleting-a-roleprivilege)

---

## 1. Introduction

Welcome to the world of **Web APIs**! 🚀

A **Web API (Application Programming Interface)** is a set of rules and protocols that allows different software applications to communicate with each other over the internet. Think of it as a waiter in a restaurant – it takes your order (request), communicates with the kitchen (server), and brings back your food (response).

In this guide, you'll learn:

- How APIs evolved over time
- The difference between traditional web apps and Web APIs
- How to build your own API endpoints

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 2. Evolution of Web API

### 🕰️ The SOAP Era (Before REST)

In the early days, applications communicated using **SOAP (Simple Object Access Protocol)**:

```
┌─────────────────┐         WSDL/Proxy           ┌─────────────────┐
│   .NET App      │  ────────────────────────▶   │   Java App      │
│                 │                               │                 │
│  ┌───────────┐  │      XML Serialization       │  ┌───────────┐  │
│  │   Data    │──┼─────────────────────────────▶│──│   Data    │  │
│  └───────────┘  │                               │  └───────────┘  │
│                 │      XML Deserialization      │                 │
└─────────────────┘  ◀────────────────────────────└─────────────────┘
```

**How SOAP Works:**

1. A **.NET application** creates data
2. Uses **WSDL (Web Services Description Language)** to define the service contract
3. Data is **serialized into XML** format
4. Sent over the network using SOAP protocol
5. **Java application** receives and **deserializes** the XML back to objects

> ⚠️ **Problem with SOAP:** XML is verbose, heavy, and slow. Every piece of data carries a lot of extra tags!

---

### 🚀 The REST Revolution

**REST (Representational State Transfer)** emerged as a simpler, lighter alternative:

```
┌─────────────────┐                              ┌─────────────────┐
│   Any Client    │         HTTP + JSON          │   Web API       │
│ (Mobile/Web/IoT)│  ◀─────────────────────────▶ │   Server        │
└─────────────────┘                              └─────────────────┘
```

**Why REST Won:**
| Feature | SOAP | REST |
|---------|------|------|
| Data Format | XML only | JSON (mainly), XML, others |
| Weight | Heavy | Lightweight |
| Speed | Slower | Faster |
| Complexity | Complex | Simple |
| Readability | Difficult | Easy |

**JSON Example:**

```json
{
  "id": 1,
  "studentName": "Kartik",
  "email": "Kartik123@gmail.com"
}
```

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 3. What is Web API?

### 🏗️ Traditional Web Application Architecture

```
┌────────────────────────────────────────────────────────────┐
│                    TRADITIONAL WEB APP                      │
│                                                             │
│   ┌─────────┐     ┌─────────┐     ┌─────────┐     ┌──────┐ │
│   │   UI    │────▶│   BLL   │────▶│   DAL   │────▶│  DB  │ │
│   │ (Views) │◀────│(Business│◀────│ (Data   │◀────│      │ │
│   │         │     │ Logic)  │     │ Access) │     │      │ │
│   └─────────┘     └─────────┘     └─────────┘     └──────┘ │
│        │                                                    │
│        ▼                                                    │
│   ┌─────────────────┐                                      │
│   │   HTML Page     │  ◀─── Rendered to Web Browser        │
│   └─────────────────┘                                      │
└────────────────────────────────────────────────────────────┘
```

**Components:**

- **UI (User Interface):** Razor Views, HTML pages
- **BLL (Business Logic Layer):** Rules, calculations, validations
- **DAL (Data Access Layer):** Database operations
- **DB (Database):** Data storage

> 🔴 **Problem:** The UI is tightly coupled with the backend. If you want a mobile app, you'd need to rebuild everything!

---

### 🌐 Web API Architecture

```
                    ┌─────────────────────────────────────────┐
                    │              WEB API                     │
                    │                                          │
                    │   ┌─────────┐     ┌─────────┐     ┌────┐│
                    │   │   BLL   │────▶│   DAL   │────▶│ DB ││
                    │   │(Business│◀────│ (Data   │◀────│    ││
                    │   │ Logic)  │     │ Access) │     │    ││
                    │   └────┬────┘     └─────────┘     └────┘│
                    │        │                                 │
                    └────────┼─────────────────────────────────┘
                             │
              ┌──────────────┼──────────────┐
              │              │              │
              ▼              ▼              ▼
        ┌──────────┐   ┌──────────┐   ┌──────────┐
        │  React   │   │  Mobile  │   │  Angular │
        │   App    │   │   App    │   │   App    │
        └──────────┘   └──────────┘   └──────────┘
```

**Key Difference:** Web API exposes only **BLL** and **DAL** – no UI! This allows multiple frontends to consume the same backend.

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 4. Why Web API?

### 🤔 The Problem with Traditional Apps

```
Traditional App:
┌──────────────────────────────────────────┐
│  Web Browser  ◀──────▶  ASP.NET MVC App  │
└──────────────────────────────────────────┘
        ❌ Can't reuse for mobile!
        ❌ Can't reuse for desktop!
        ❌ Tightly coupled UI
```

### ✅ The Web API Solution

```
Web API Approach:
┌──────────────────┐
│   Web Browser    │────┐
└──────────────────┘    │
                        │
┌──────────────────┐    │      ┌────────────────┐
│   Mobile App     │────┼─────▶│    WEB API     │
└──────────────────┘    │      │   (Backend)    │
                        │      └────────────────┘
┌──────────────────┐    │
│   Desktop App    │────┘
└──────────────────┘
```

### 💡 Benefits of Web API:

| Benefit                | Description                              |
| ---------------------- | ---------------------------------------- |
| **Reusability**        | One backend, multiple frontends          |
| **Flexibility**        | Change UI without touching backend       |
| **Scalability**        | Scale frontend and backend independently |
| **Technology Freedom** | Use React, Angular, Flutter – anything!  |
| **Mobile Ready**       | Same API for web and mobile apps         |

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 5. Web API Request & Response

### 📤 HTTP Request Structure

Every API request has three main parts:

```
┌────────────────────────────────────────────────────────┐
│                     HTTP REQUEST                        │
├────────────────────────────────────────────────────────┤
│  VERB      │  GET, POST, PUT, DELETE                   │
├────────────────────────────────────────────────────────┤
│  HEADERS   │  Content-Type: application/json           │
│            │  Authorization: Bearer token123           │
│            │  Accept: application/json                 │
├────────────────────────────────────────────────────────┤
│  CONTENT   │  { "name": "Kartik", "email": "..." }    │
│  (Body)    │                                           │
└────────────────────────────────────────────────────────┘
```

---

### 📥 HTTP Response Structure

```
┌────────────────────────────────────────────────────────┐
│                     HTTP RESPONSE                       │
├────────────────────────────────────────────────────────┤
│  STATUS    │  200 OK, 404 Not Found, 500 Error         │
├────────────────────────────────────────────────────────┤
│  HEADERS   │  Content-Type: application/json           │
│            │  Cache-Control: no-cache                  │
├────────────────────────────────────────────────────────┤
│  CONTENT   │  { "id": 1, "studentName": "Kartik" }    │
│  (Body)    │                                           │
└────────────────────────────────────────────────────────┘
```

### 📊 Common HTTP Status Codes

| Code  | Meaning               | Usage                         |
| ----- | --------------------- | ----------------------------- |
| `200` | OK                    | Request successful            |
| `201` | Created               | Resource created successfully |
| `400` | Bad Request           | Invalid request data          |
| `404` | Not Found             | Resource doesn't exist        |
| `500` | Internal Server Error | Server-side error             |

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 6. HTTP Verbs

HTTP verbs (methods) define what action to perform on a resource:

### 🔵 GET – Retrieve Data

```http
GET /api/student/all
```

> Fetches all students or a specific student. **Safe & Idempotent.**

---

### 🟢 POST – Create Data

```http
POST /api/student
Content-Type: application/json

{
    "studentName": "New Student",
    "email": "new@email.com"
}
```

> Creates a new resource. **Not idempotent.**

---

### 🟡 PUT – Update Data

```http
PUT /api/student/1
Content-Type: application/json

{
    "id": 1,
    "studentName": "Updated Name"
}
```

> Updates an existing resource completely. **Idempotent.**

---

### 🔴 DELETE – Remove Data

```http
DELETE /api/student/1
```

> Removes a resource. **Idempotent.**

---

### 📋 HTTP Verbs Summary

| Verb   | Action | Safe?  | Idempotent? |
| ------ | ------ | ------ | ----------- |
| GET    | Read   | ✅ Yes | ✅ Yes      |
| POST   | Create | ❌ No  | ❌ No       |
| PUT    | Update | ❌ No  | ✅ Yes      |
| DELETE | Delete | ❌ No  | ✅ Yes      |

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 7. Creating Your First Endpoint

Let's look at a real implementation from this project!

### 📁 Project Structure

```
CollegeApp/
├── Controllers/
│   └── StudentController.cs    ◀── API Endpoints
├── Model/
│   ├── Student.cs              ◀── Data Model
│   └── CollegeRepository.cs    ◀── Data Storage
└── Program.cs
```

---

### 📦 Student Model

```csharp
namespace ASPNETCoreWebAPI.Model
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
```

---

### 💾 College Repository (Data Layer)

```csharp
namespace ASPNETCoreWebAPI.Model
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>
        {
            new Student
            {
                Id = 1,
                StudentName = "Kartik",
                Email = "Kartik123@gmail.com",
                Address = "Hyd, India"
            },
            new Student
            {
                Id = 2,
                StudentName = "Aryan",
                Email = "Aryan123@gmail.com",
                Address = "Banglore, India"
            }
        };
    }
}
```

---

### 🎮 Student Controller (API Endpoints)

```csharp
using ASPNETCoreWebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // GET: api/student/all
        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        public ActionResult<IEnumerable<Student>> GetStudents()
        {
            return Ok(CollegeRepository.Students);
        }

        // GET: api/student/{id}
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        public ActionResult<Student> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students
                .Where(s => s.Id == id).FirstOrDefault();

            if (student == null)
                return NotFound($"The student with id {id} not found!.");

            return Ok(student);
        }

        // GET: api/student/{name}
        [HttpGet("{name:alpha}", Name = "GetStudentByName")]
        public ActionResult<Student> GetStudentByName(string name)
        {
            if (string.IsNullOrEmpty(name))
                return BadRequest();

            var student = CollegeRepository.Students
                .Where(s => s.StudentName == name).FirstOrDefault();

            if (student == null)
                return NotFound($"The student with name {name} not found!.");

            return Ok(student);
        }

        // DELETE: api/student/{id}
        [HttpDelete("{id}", Name = "DeleteStudentById")]
        public ActionResult<bool> DeleteStudent(int id)
        {
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students
                .Where(s => s.Id == id).FirstOrDefault();

            if (student == null)
                return NotFound($"The student with id {id} not found!.");

            CollegeRepository.Students.Remove(student);
            return Ok(true);
        }
    }
}
```

---

### 🧪 Testing Your Endpoints

| Endpoint                  | Method | Description          |
| ------------------------- | ------ | -------------------- |
| `GET /api/student/all`    | GET    | Get all students     |
| `GET /api/student/1`      | GET    | Get student by ID    |
| `GET /api/student/Kartik` | GET    | Get student by name  |
| `DELETE /api/student/1`   | DELETE | Delete student by ID |

---

### 🎯 Key Takeaways

1. **`[ApiController]`** – Marks the class as a Web API controller
2. **`[Route("api/[controller]")]`** – Defines the base URL route
3. **`[HttpGet]`, `[HttpPost]`, etc.** – Maps methods to HTTP verbs
4. **`ActionResult<T>`** – Allows returning data with HTTP status codes
5. **Response Helpers:**
   - `Ok()` – Returns 200 with data
   - `BadRequest()` – Returns 400
   - `NotFound()` – Returns 404

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 8. Why and How to Use DTOs

### 🤔 What is a DTO?

**DTO (Data Transfer Object)** is a design pattern used to transfer data between layers of an application. It's a simple object that carries data without any business logic.

---

### ❌ The Problem: Returning Database Entities Directly

When working with an **in-memory repository** or any database, you might be tempted to return database entities directly:

```csharp
// ❌ BAD PRACTICE: Returning database entity directly
public ActionResult<Student> GetStudentById(int id)
{
    var student = CollegeRepository.Students.Find(s => s.Id == id);
    return Ok(student);  // Exposing database entity!
}
```

> ⚠️ **Why is this bad?**
>
> - Exposes internal database structure to clients
> - Can leak sensitive fields (passwords, internal IDs)
> - Tightly couples API responses to database schema
> - Hard to add calculated fields or transform data

---

### ✅ The Solution: Use DTOs

```
┌─────────────────────────────────────────────────────────────────────────┐
│                        DATA FLOW WITH DTOs                               │
│                                                                          │
│   ┌────────┐      ┌─────────┐      ┌─────────┐      ┌──────────────┐   │
│   │   DB   │─────▶│   DAL   │─────▶│   BLL   │─────▶│  Controller  │   │
│   │        │      │         │      │         │      │              │   │
│   │ Student│      │ Student │      │ Student │      │  StudentDTO  │   │
│   │ Entity │      │ Entity  │      │ → DTO   │      │  (Response)  │   │
│   └────────┘      └─────────┘      └─────────┘      └──────────────┘   │
│                                         │                    │          │
│                                         ▼                    ▼          │
│                                  ┌─────────────────────────────────┐   │
│                                  │ Transform + Add Calculated Data  │   │
│                                  └─────────────────────────────────┘   │
└─────────────────────────────────────────────────────────────────────────┘
```

---

Imagine a shopping cart system:

**Step 1: DAL Layer reads from Database**

```csharp
// Database Entity (what's stored in DB)
public class OrderItem
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
}
```

**Step 2: BLL Layer calculates and creates DTO**

```csharp
// DTO (what's sent to UI)
public class OrderItemDTO
{
    public int Id { get; set; }
    public string Name { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    // ✅ Calculated fields added by BLL
    public decimal TotalPrice { get; set; }      // Quantity × UnitPrice
    public decimal TaxAmount { get; set; }       // Calculated tax
    public decimal TaxPercentage { get; set; }   // Tax %
}
```

**Step 3: Controller returns DTO to UI**

```csharp
public ActionResult<OrderItemDTO> GetOrderItem(int id)
{
    var item = _repository.GetById(id);  // Get entity from DB

    var dto = new OrderItemDTO
    {
        Id = item.Id,
        Name = item.Name,
        Quantity = item.Quantity,
        UnitPrice = item.UnitPrice,
        TotalPrice = item.Quantity * item.UnitPrice,
        TaxAmount = (item.Quantity * item.UnitPrice) * 0.18m,
        TaxPercentage = 18
    };

    return Ok(dto);  // ✅ Return DTO, not entity!
}
```

---

### 📁 Updated Project Structure

```
CollegeApp/
├── Controllers/
│   └── StudentController.cs    ◀── Returns StudentDTO
├── Model/
│   ├── Student.cs              ◀── Database Entity
│   ├── StudentDTO.cs           ◀── Data Transfer Object (NEW!)
│   └── CollegeRepository.cs    ◀── Data Storage
└── Program.cs
```

---

### 📦 StudentDTO Model

```csharp
namespace ASPNETCoreWebAPI.Model
{
    public class StudentDTO
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }
}
```

---

### � Updated Student Controller (Using DTOs)

```csharp
using ASPNETCoreWebAPI.Model;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        // GET: api/student/all
        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        public ActionResult<IEnumerable<StudentDTO>> GetStudents()
        {
            // Using LINQ to transform Entity → DTO
            var students = CollegeRepository.Students.Select(s => new StudentDTO()
            {
                Id = s.Id,
                StudentName = s.StudentName,
                Email = s.Email,
                Address = s.Address
            });

            return Ok(students);
        }

        // GET: api/student/{id}
        [HttpGet]
        [Route("{id:int}", Name = "GetStudentById")]
        public ActionResult<StudentDTO> GetStudentById(int id)
        {
            if (id <= 0)
                return BadRequest();

            var student = CollegeRepository.Students
                .Where(s => s.Id == id).FirstOrDefault();

            if (student == null)
                return NotFound($"The student with id {id} not found!.");

            // Create DTO from entity
            var studentDTO = new StudentDTO
            {
                Id = student.Id,
                StudentName = student.StudentName,
                Email = student.Email,
                Address = student.Address
            };

            return Ok(studentDTO);
        }
    }
}
```

---

### 💡 Benefits of Using DTOs

| Benefit             | Description                                           |
| ------------------- | ----------------------------------------------------- |
| **Security**        | Hide sensitive database fields from API responses     |
| **Flexibility**     | Shape data for specific UI needs                      |
| **Decoupling**      | API responses independent of database schema          |
| **Calculated Data** | Add computed fields (totals, percentages) in BLL      |
| **Versioning**      | Maintain different DTO versions for API compatibility |

---

### 🎯 Key Takeaways for DTOs

1. **Never return database entities directly** – Always use DTOs
2. **Create DTOs in the Model folder** – Keep them separate from entities
3. **Transform in BLL/Controller** – Convert Entity → DTO before returning
4. **Add calculated fields** – DTOs can include computed values
5. **Use LINQ for transformation** – Efficient way to map collections

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 9. HttpPost – Creating Resources

### 🤔 What is HttpPost?

**`[HttpPost]`** is an HTTP verb attribute in ASP.NET Core Web API used to **create new resources**. When a client wants to add new data (like creating a new student), it sends a POST request with the data in the request body.

### Why Use HttpPost?

| Purpose                  | Description                                         |
| ------------------------ | --------------------------------------------------- |
| **Create Data**          | Add new records to your database/repository         |
| **Send Complex Data**    | Request body can contain JSON objects               |
| **Non-Idempotent**       | Each call creates a new resource                    |
| **Secure Data Transfer** | Data is in body, not URL (safer for sensitive info) |

### When to Use HttpPost?

- ✅ Creating a new user account
- ✅ Submitting a form
- ✅ Adding a new product to inventory
- ✅ Creating a new student record

---

### 📦 Example from This Project

**StudentController.cs – CreateStudent Method:**

```csharp
[HttpPost]
[Route("Create")]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
{
    if (model == null)
        return BadRequest();

    int newId = CollegeRepository.Students.LastOrDefault().Id + 1;

    Student student = new Student
    {
        Id = newId,
        StudentName = model.StudentName,
        Email = model.Email,
        Address = model.Address
    };

    CollegeRepository.Students.Add(student);

    model.Id = student.Id;

    return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
}
```

### 🔑 Key Points

1. **`[HttpPost]`** – Marks the method to handle POST requests
2. **`[FromBody]`** – Tells ASP.NET Core to read data from request body
3. **`StudentDTO model`** – The DTO object containing student data from client
4. **Returns `201 Created`** – Standard response for successful creation

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 10. CreatedAtRoute – Proper POST Response

### 🤔 What is CreatedAtRoute?

**`CreatedAtRoute()`** is a helper method that returns a **201 Created** response along with:

- A **Location header** pointing to the newly created resource
- The **created object** in the response body

---

### Why Do We Need CreatedAtRoute?

When you create a new resource, the client needs to know:

1. **Was it successful?** → Status code 201
2. **What is the new resource ID?** → Response body
3. **Where can I find it?** → Location header

```
HTTP/1.1 201 Created
Location: https://localhost:7001/api/Student/3
Content-Type: application/json

{
    "id": 3,
    "studentName": "New Student",
    "email": "new@email.com",
    "address": "Delhi, India"
}
```

---

### 📐 How CreatedAtRoute Works

```
┌─────────────────────────────────────────────────────────────────┐
│                    CreatedAtRoute Flow                          │
│                                                                 │
│  Client POST Request                                            │
│       │                                                         │
│       ▼                                                         │
│  ┌─────────────────────┐                                       │
│  │  CreateStudent()    │                                       │
│  │  [HttpPost]         │                                       │
│  └──────────┬──────────┘                                       │
│             │                                                   │
│             ▼                                                   │
│  ┌─────────────────────────────────────────────────────────┐   │
│  │  CreatedAtRoute("GetStudentById", new { id = 3 }, dto)  │   │
│  └─────────────────────────────────────────────────────────┘   │
│             │                                                   │
│             ▼                                                   │
│  ┌─────────────────────┐    Generates URL using                │
│  │  GetStudentById     │◀── route name and parameters          │
│  │  [HttpGet("{id}")]  │                                       │
│  │  Name = "..."       │                                       │
│  └─────────────────────┘                                       │
│             │                                                   │
│             ▼                                                   │
│  Response: 201 Created + Location: /api/Student/3              │
└─────────────────────────────────────────────────────────────────┘
```

---

### 📦 Example from This Project

**Step 1: Define a Named Route (for GET)**

```csharp
[HttpGet]
[Route("{id:int}", Name = "GetStudentById")]  // ◀── Named route
public ActionResult<StudentDTO> GetStudentById(int id)
{
    // ... get student by id
    return Ok(studentDTO);
}
```

**Step 2: Use CreatedAtRoute in POST**

```csharp
[HttpPost]
[Route("Create")]
public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
{
    // ... create student logic

    // Returns 201 with Location header pointing to GetStudentById
    return CreatedAtRoute("GetStudentById", new { id = model.Id }, model);
}
```

### 🔑 CreatedAtRoute Parameters

| Parameter        | Description                       | Example                 |
| ---------------- | --------------------------------- | ----------------------- |
| **Route Name**   | Name of the GET route to link to  | `"GetStudentById"`      |
| **Route Values** | Parameters for the route URL      | `new { id = model.Id }` |
| **Value**        | Object to return in response body | `model` (StudentDTO)    |

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 11. HttpPut – Updating Resources

### 🤔 What is HttpPut?

**`[HttpPut]`** is an HTTP verb attribute in ASP.NET Core Web API used to **update existing resources**. When a client wants to modify existing data (like updating a student's information), it sends a PUT request with the complete updated data in the request body.

### Why Use HttpPut?

| Purpose                  | Description                                         |
| ------------------------ | --------------------------------------------------- |
| **Update Data**          | Modify existing records in your database/repository |
| **Full Resource Update** | Replaces the entire resource with new data          |
| **Idempotent**           | Same request multiple times = same result           |
| **Secure Data Transfer** | Data is in body, not URL (safer for sensitive info) |

### When to Use HttpPut?

- ✅ Updating a user profile
- ✅ Modifying a student's information
- ✅ Changing product details
- ✅ Updating existing records completely

> 💡 **PUT vs PATCH**: PUT replaces the entire resource, while PATCH applies partial modifications. This project uses PUT for complete updates.

---

### 📦 Example from This Project

**StudentController.cs – UpdateStudent Method:**

```csharp
[HttpPut]
[Route("Update")]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public ActionResult UpdateStudent([FromBody] StudentDTO model)
{
    if (model == null || model.Id <= 0)
        return BadRequest();

    var existingStudent = CollegeRepository.Students.Where(s => s.Id == model.Id).FirstOrDefault();

    if (existingStudent == null)
        return NotFound();

    existingStudent.StudentName = model.StudentName;
    existingStudent.Email = model.Email;
    existingStudent.Address = model.Address;

    return NoContent();
}
```

---

### 🔑 Key Points

1. **`[HttpPut]`** – Marks the method to handle PUT requests
2. **`[FromBody]`** – Reads the updated data from request body
3. **`model.Id <= 0`** – Validates that the ID is provided and valid
4. **`FirstOrDefault()`** – Finds the existing record to update
5. **Returns `204 NoContent`** – Standard response for successful update

---

### 📐 How HttpPut Works

```
┌─────────────────────────────────────────────────────────────────┐
│                    HttpPut Update Flow                           │
│                                                                  │
│  Client PUT Request                                              │
│  PUT /api/student/Update                                         │
│  Body: { "id": 1, "studentName": "Updated Name", ... }          │
│       │                                                          │
│       ▼                                                          │
│  ┌─────────────────────┐                                        │
│  │  Validation Check   │                                        │
│  │  - Is model null?   │                                        │
│  │  - Is Id <= 0?      │                                        │
│  └──────────┬──────────┘                                        │
│             │ ✅ Valid                                           │
│             ▼                                                    │
│  ┌─────────────────────┐                                        │
│  │  Find Existing      │                                        │
│  │  Student by Id      │                                        │
│  └──────────┬──────────┘                                        │
│             │                                                    │
│    ┌────────┴────────┐                                          │
│    │                 │                                          │
│    ▼                 ▼                                          │
│  Found            Not Found                                     │
│    │                 │                                          │
│    ▼                 ▼                                          │
│  Update          404 NotFound                                   │
│  Properties                                                     │
│    │                                                            │
│    ▼                                                            │
│  204 NoContent                                                  │
└─────────────────────────────────────────────────────────────────┘
```

> 💡 **Why 204 NoContent?** After a successful update, the client already knows the data they sent. Returning the updated object would be redundant, so 204 is more efficient.

---

### ⚡ PUT vs POST Comparison

| Feature        | POST (Create)               | PUT (Update)                 |
| -------------- | --------------------------- | ---------------------------- |
| **Purpose**    | Create new resource         | Update existing resource     |
| **Idempotent** | ❌ No                       | ✅ Yes                       |
| **ID in Body** | ❌ Not required (generated) | ✅ Required (to find record) |
| **Response**   | `201 Created` with Location | `204 NoContent` or `200 OK`  |
| **Behavior**   | Adds new record each time   | Same result for same request |

---

### 🎯 Best Practices for PUT

1. **Always validate the ID** – Check if `Id <= 0` or `Id == null`
2. **Check if resource exists** – Return `404` if not found
3. **Use DTOs** – Never accept/return database entities
4. **Return `204 NoContent`** – Most efficient for updates
5. **Handle validation** – Use model validation attributes
6. **Use `[ProducesResponseType]`** – Document all possible responses
7. **Make it idempotent** – Same request should produce same result

---

### 📋 Complete CRUD Operations

Now you've learned all major CRUD operations:

| Operation  | HTTP Verb | Endpoint                   | Description             |
| ---------- | --------- | -------------------------- | ----------------------- |
| **C**reate | POST      | `POST /api/student/Create` | Create new student      |
| **R**ead   | GET       | `GET /api/student/All`     | Get all students        |
| **R**ead   | GET       | `GET /api/student/{id}`    | Get student by ID       |
| **U**pdate | PUT       | `PUT /api/student/Update`  | Update existing student |
| **D**elete | DELETE    | `DELETE /api/student/{id}` | Delete student          |

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 12. HttpPatch – Partial Updates

### 🤔 What is HttpPatch?

**`[HttpPatch]`** is an HTTP verb attribute in ASP.NET Core Web API used to **partially update existing resources**. Unlike PUT which requires sending the entire object, PATCH allows you to send only the fields you want to update.

---

### ❌ The Problem with HttpPut

When using `[HttpPut]`, you must send **all fields** even if you want to update just one:

```json
// Want to update ONLY the email? Still need to send everything!
PUT /api/Student/Update
{
    "id": 1,
    "studentName": "Kartik",           // ❌ Redundant
    "email": "newemail@gmail.com",     // ✅ Only this changed!
    "address": "Hyd, India"            // ❌ Redundant
}
```

**Drawbacks of HttpPut:**

| Issue               | Description                                          |
| ------------------- | ---------------------------------------------------- |
| **Bandwidth Waste** | Sending unnecessary data over the network            |
| **Performance**     | Larger payload = slower requests                     |
| **Error-Prone**     | Client must know all field values to avoid data loss |
| **Inefficient**     | Updating one field requires full object              |

---

### ✅ The Solution: HttpPatch

With `[HttpPatch]`, send **only the fields you want to update**:

```json
// Update ONLY the email - much more efficient!
PATCH /api/Student/UpdatePartial/1
[
    {
        "op": "replace",
        "path": "/email",
        "value": "newemail@gmail.com"
    }
]
```

**Benefits of HttpPatch:**

| Benefit            | Description                                      |
| ------------------ | ------------------------------------------------ |
| **Efficient**      | Send only changed fields                         |
| **Less Bandwidth** | Smaller payload size                             |
| **Flexible**       | Update one or multiple fields                    |
| **Safe**           | No risk of accidentally overwriting other fields |

---

### 📦 Required NuGet Packages

To use `[HttpPatch]` in ASP.NET Core, you need two libraries:

#### 1. **Microsoft.AspNetCore.JsonPatch**

Provides JSON Patch functionality according to RFC 6902 standard.

```powershell
Install-Package Microsoft.AspNetCore.JsonPatch
```

#### 2. **Microsoft.AspNetCore.Mvc.NewtonsoftJson**

Adds Newtonsoft.Json support for handling JSON Patch documents.

```powershell
Install-Package Microsoft.AspNetCore.Mvc.NewtonsoftJson
```

**Add to Program.cs:**

```csharp
builder.Services.AddControllers()
    .AddNewtonsoftJson();  // ◀── Required for JSON Patch
```

---

### 📐 How HttpPatch Works

```
┌─────────────────────────────────────────────────────────────────┐
│                    HttpPatch Update Flow                         │
│                                                                  │
│  Client PATCH Request                                            │
│  PATCH /api/student/UpdatePartial/1                              │
│  Body: [{ "op": "replace", "path": "/email", "value": "..." }] │
│       │                                                          │
│       ▼                                                          │
│  ┌─────────────────────┐                                        │
│  │  Find Student by Id │                                        │
│  └──────────┬──────────┘                                        │
│             │                                                    │
│    ┌────────┴────────┐                                          │
│    │                 │                                          │
│    ▼                 ▼                                          │
│  Found            Not Found                                     │
│    │                 │                                          │
│    ▼                 ▼                                          │
│  Apply Patch     404 NotFound                                   │
│  Operations                                                     │
│  (Only changed                                                  │
│   fields)                                                       │
│    │                                                            │
│    ▼                                                            │
│  Validate                                                       │
│  ModelState                                                     │
│    │                                                            │
│    ▼                                                            │
│  204 NoContent                                                  │
└─────────────────────────────────────────────────────────────────┘
```

---

### 🎮 Example Implementation

**StudentController.cs – UpdatePartialStudent Method:**

```csharp
using Microsoft.AspNetCore.JsonPatch;

[HttpPatch]
[Route("UpdatePartial/{id}")]
[ProducesResponseType(StatusCodes.Status204NoContent)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public ActionResult UpdatePartialStudent(int id, [FromBody] JsonPatchDocument<StudentDTO> patchDocument)
{
    if (patchDocument == null || id <= 0)
        return BadRequest();

    // Find existing student
    var existingStudent = CollegeRepository.Students
        .Where(s => s.Id == id).FirstOrDefault();

    if (existingStudent == null)
        return NotFound();

    // Convert to DTO
    var studentDTO = new StudentDTO
    {
        Id = existingStudent.Id,
        StudentName = existingStudent.StudentName,
        Email = existingStudent.Email,
        Address = existingStudent.Address
    };

    // Apply patch operations to DTO
    patchDocument.ApplyTo(studentDTO, ModelState);

    // Validate after applying patch
    if (!ModelState.IsValid)
        return BadRequest(ModelState);

    // Update entity with patched values
    existingStudent.StudentName = studentDTO.StudentName;
    existingStudent.Email = studentDTO.Email;
    existingStudent.Address = studentDTO.Address;

    return NoContent();
}
```

---

### 🔑 Key Points

1. **`JsonPatchDocument<StudentDTO>`** – Represents the patch operations
2. **`ApplyTo()`** – Applies patch operations to the DTO
3. **`ModelState`** – Validates the patched data
4. **Field-level updates** – Only specified fields are modified
5. **Returns `204 NoContent`** – Standard response for successful update

---

### 📊 JSON Patch Operation Types

| Operation | Purpose                      | Example                             |
| --------- | ---------------------------- | ----------------------------------- |
| `add`     | Add or update value          | Add new property or update existing |
| `remove`  | Remove property              | Set property to null/default        |
| `replace` | Replace existing value       | Update field value                  |
| `copy`    | Copy value from another path | Copy one field to another           |
| `move`    | Move value to another path   | Move data between fields            |
| `test`    | Test if value matches        | Verify before applying changes      |

---

### 🧪 Testing the PATCH Endpoint

**Request Example 1: Update only email**

```http
PATCH /api/Student/UpdatePartial/1
Content-Type: application/json-patch+json

[
    {
        "op": "replace",
        "path": "/email",
        "value": "kartik.new@gmail.com"
    }
]
```

**Request Example 2: Update multiple fields**

```http
PATCH /api/Student/UpdatePartial/1
Content-Type: application/json-patch+json

[
    {
        "op": "replace",
        "path": "/studentName",
        "value": "Kartik Updated"
    },
    {
        "op": "replace",
        "path": "/address",
        "value": "Bangalore, India"
    }
]
```

**Successful Response:**

```http
HTTP/1.1 204 No Content
```

---

### ⚡ PUT vs PATCH Comparison

| Feature          | PUT (Full Update)       | PATCH (Partial Update)           |
| ---------------- | ----------------------- | -------------------------------- |
| **Purpose**      | Replace entire resource | Update specific fields           |
| **Payload**      | Must send all fields    | Send only changed fields         |
| **Efficiency**   | ❌ Less efficient       | ✅ More efficient                |
| **Bandwidth**    | ❌ Higher               | ✅ Lower                         |
| **Use Case**     | Complete replacement    | Field-level updates              |
| **Idempotent**   | ✅ Yes                  | ⚠️ Depends on operations         |
| **Request Body** | Full object (JSON)      | Array of operations (JSON Patch) |
| **Content-Type** | `application/json`      | `application/json-patch+json`    |

---

### 🎯 When to Use PUT vs PATCH?

**Use PUT when:**

- ✅ Updating the entire resource
- ✅ You have all field values available
- ✅ Simpler implementation needed
- ✅ Client sends complete object anyway

**Use PATCH when:**

- ✅ Updating only specific fields
- ✅ Optimizing bandwidth usage
- ✅ Mobile apps or slow networks
- ✅ User edits individual fields (e.g., profile updates)

---

### 🎯 Best Practices for PATCH

1. **Use `application/json-patch+json` Content-Type** – Standard for JSON Patch
2. **Validate after ApplyTo()** – Always check ModelState
3. **Handle invalid paths gracefully** – Return proper error messages
4. **Document operations** – Specify which operations are supported
5. **Use DTOs** – Never patch entity models directly
6. **Return `204 NoContent`** – Consistent with PUT behavior
7. **Add NewtonsoftJson** – Required for JSON Patch support

---

### 💡 Real-World Example

**Scenario:** User wants to update only their email in a profile page.

**❌ With PUT (Inefficient):**

```json
PUT /api/student/Update
{
    "id": 1,
    "studentName": "Kartik",
    "email": "newemail@gmail.com",  // Only this changed!
    "address": "Hyd, India",
    "phone": "1234567890",
    "dateOfBirth": "2000-01-01"
    // ... 20 more fields
}
// Payload: ~500 bytes
```

**✅ With PATCH (Efficient):**

```json
PATCH /api/student/UpdatePartial/1
[
    {
        "op": "replace",
        "path": "/email",
        "value": "newemail@gmail.com"
    }
]
// Payload: ~80 bytes (85% reduction!)
```

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 13. Model Validation – Preventing Invalid Data

### 🤔 What is Model Validation?

**Model Validation** ensures that the data received from clients meets your business rules **before processing**. It prevents users from creating incomplete or invalid records.

---

### Why Use Model Validation?

```
Without Validation:                    With Validation:
┌─────────────────────┐              ┌─────────────────────┐
│ POST /api/student   │              │ POST /api/student   │
│ { }  ← Empty!       │              │ { }  ← Empty!       │
└──────────┬──────────┘              └──────────┬──────────┘
           │                                    │
           ▼                                    ▼
┌─────────────────────┐              ┌─────────────────────┐
│   CREATES EMPTY     │              │   400 BAD REQUEST   │
│   STUDENT RECORD!   │  😱          │   "Name required"   │  ✅
└─────────────────────┘              └─────────────────────┘
```

### When to Use Model Validation?

- ✅ Ensuring required fields are filled
- ✅ Validating email format
- ✅ Checking string length limits
- ✅ Validating date ranges
- ✅ Confirming password matches

---

### 📦 How ASP.NET Core Handles Validation

When you use **`[ApiController]`** attribute, ASP.NET Core **automatically validates** the model and returns **400 Bad Request** if validation fails!

```csharp
[Route("api/[controller]")]
[ApiController]  // ◀── Enables automatic model validation
public class StudentController : ControllerBase
{
    [HttpPost]
    public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
    {
        // No need to check ModelState.IsValid manually!
        // ASP.NET Core automatically returns 400 if validation fails

        // ... create student logic
    }
}
```

> 💡 **Without `[ApiController]`**, you would need to manually check:
>
> ```csharp
> if (!ModelState.IsValid)
>     return BadRequest(ModelState);
> ```

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 14. Built-in Validation Attributes

ASP.NET Core provides many **built-in validation attributes** that you can apply to DTO properties to enforce rules.

### 📋 Common Built-in Validation Attributes

| Attribute             | Purpose                      | Example                            |
| --------------------- | ---------------------------- | ---------------------------------- |
| `[Required]`          | Field cannot be null/empty   | `[Required]`                       |
| `[EmailAddress]`      | Must be valid email format   | `[EmailAddress]`                   |
| `[StringLength]`      | Max (and min) string length  | `[StringLength(30)]`               |
| `[Range]`             | Value must be within range   | `[Range(10, 20)]`                  |
| `[Compare]`           | Must match another property  | `[Compare(nameof(Password))]`      |
| `[ValidateNever]`     | Skip validation for property | `[ValidateNever]`                  |
| `[RegularExpression]` | Must match regex pattern     | `[RegularExpression(@"^\d{10}$")]` |
| `[MinLength]`         | Minimum length               | `[MinLength(5)]`                   |
| `[MaxLength]`         | Maximum length               | `[MaxLength(100)]`                 |
| `[Phone]`             | Valid phone number format    | `[Phone]`                          |
| `[Url]`               | Valid URL format             | `[Url]`                            |
| `[CreditCard]`        | Valid credit card number     | `[CreditCard]`                     |

---

### 📦 Example from This Project – StudentDTO.cs

```csharp
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreWebAPI.Model
{
    public class StudentDTO
    {
        [ValidateNever]
        public int Id { get; set; }

        [Required(ErrorMessage = "Student name is required.")]
        [StringLength(30)]
        public string StudentName { get; set; }

        [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
        public string Email { get; set; }

        [Range(10, 20)]
        public int Age { get; set; }

        [Required]
        public string Address { get; set; }

        public string Password { get; set; }

        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
    }
}
```

---

### 🔍 Attribute Details

#### 1. `[Required]` – Prevents Empty Values

```csharp
[Required]
public string StudentName { get; set; }

[Required(ErrorMessage = "Student name is required.")]  // Custom message
public string StudentName { get; set; }
```

#### 2. `[EmailAddress]` – Validates Email Format

```csharp
[EmailAddress]
public string Email { get; set; }

// Invalid: "kartik", "kartik@", "@gmail.com"
// Valid: "kartik@gmail.com"
```

#### 3. `[StringLength]` – Controls String Length

```csharp
[StringLength(30)]  // Max 30 characters
public string StudentName { get; set; }

[StringLength(30, MinimumLength = 3)]  // Between 3-30 characters
public string StudentName { get; set; }
```

#### 4. `[Range]` – Validates Numeric Range

```csharp
[Range(10, 20)]  // Age must be between 10 and 20
public int Age { get; set; }

[Range(0.01, 10000.00)]  // For decimal values
public decimal Price { get; set; }
```

#### 5. `[Compare]` – Compares Two Properties

```csharp
public string Password { get; set; }

[Compare(nameof(Password))]  // Must match Password property
public string ConfirmPassword { get; set; }
```

#### 6. `[ValidateNever]` – Skip Validation

```csharp
[ValidateNever]  // Don't validate this property
public int Id { get; set; }
```

> 💡 Use `[ValidateNever]` for auto-generated fields like IDs

---

### 🧪 Validation Error Response

When validation fails, ASP.NET Core returns:

```json
{
  "type": "https://tools.ietf.org/html/rfc7231#section-6.5.1",
  "title": "One or more validation errors occurred.",
  "status": 400,
  "errors": {
    "StudentName": ["Student name is required."],
    "Email": ["Please enter a valid email address."],
    "Age": ["The field Age must be between 10 and 20."]
  }
}
```

---

## 15. Custom Validation Attributes

### 🤔 When Built-in Attributes Aren't Enough

Sometimes you need validation logic that built-in attributes don't provide:

- ✅ Date must be in the future
- ✅ End date must be after start date
- ✅ Custom business rules
- ✅ Complex conditional validation

---

### 📐 How to Create Custom Validation

**Step 1: Create a class that inherits from `ValidationAttribute`**
**Step 2: Override the `IsValid` method**
**Step 3: Apply the attribute to your DTO property**

---

### 📦 Example from This Project – DateCheckAttribute

**Validators/DateCheckAttribute.cs:**

```csharp
using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreWebAPI.Validators
{
    public class DateCheckAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            var date = (DateTime?)value;

            if (date < DateTime.Now)
            {
                return new ValidationResult("The date must be greater than or equal to today.");
            }

            return ValidationResult.Success;
        }
    }
}
```

**Model/StudentDTO.cs – Using the Custom Attribute:**

```csharp
using ASPNETCoreWebAPI.Validators;  // ◀── Import custom validators

namespace ASPNETCoreWebAPI.Model
{
    public class StudentDTO
    {
        public int Id { get; set; }

        [Required]
        public string StudentName { get; set; }

        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Address { get; set; }

        [DateCheck]  // ◀── Custom validation attribute!
        public DateTime AdmissionDate { get; set; }
    }
}
```

---

### 🔍 How Custom Validation Works

```
┌─────────────────────────────────────────────────────────────────────┐
│                  Custom Validation Flow                              │
│                                                                      │
│  POST Request with AdmissionDate = "2024-01-01" (past date)         │
│       │                                                              │
│       ▼                                                              │
│  ┌─────────────────────────────────────────────┐                    │
│  │  ASP.NET Core Model Binder                   │                    │
│  │  Finds [DateCheck] attribute on property     │                    │
│  └──────────────────────┬──────────────────────┘                    │
│                         │                                            │
│                         ▼                                            │
│  ┌─────────────────────────────────────────────┐                    │
│  │  DateCheckAttribute.IsValid()               │                    │
│  │  - Receives: date = "2024-01-01"            │                    │
│  │  - Checks: date < DateTime.Now?             │                    │
│  │  - Result: YES, it's in the past!           │                    │
│  └──────────────────────┬──────────────────────┘                    │
│                         │                                            │
│                         ▼                                            │
│  ┌─────────────────────────────────────────────┐                    │
│  │  Returns ValidationResult with error        │                    │
│  │  "The date must be greater than or equal    │                    │
│  │   to today."                                │                    │
│  └──────────────────────┬──────────────────────┘                    │
│                         │                                            │
│                         ▼                                            │
│  400 Bad Request with validation error message                      │
└─────────────────────────────────────────────────────────────────────┘
```

---

### 🔑 Key Parts of Custom Validation

| Component                           | Purpose                                                   |
| ----------------------------------- | --------------------------------------------------------- |
| **`ValidationAttribute`**           | Base class to inherit from                                |
| **`IsValid()`**                     | Method to override with your logic                        |
| **`object? value`**                 | The value of the property being validated                 |
| **`ValidationContext`**             | Access to the entire object for cross-property validation |
| **`ValidationResult.Success`**      | Return when validation passes                             |
| **`new ValidationResult("error")`** | Return when validation fails with message                 |

---

### 📝 Alternative: Manual Validation in Controller

Instead of custom attributes, you can validate manually in the controller:

```csharp
[HttpPost]
[Route("Create")]
public ActionResult<StudentDTO> CreateStudent([FromBody] StudentDTO model)
{
    if (model == null)
        return BadRequest();

    // Manual validation
    if (model.AdmissionDate < DateTime.Now)
    {
        ModelState.AddModelError("Admission Error",
            "Admission date must be greater than or equal to today's date.");
        return BadRequest(ModelState);
    }

    // ... create student logic
}
```

> 💡 **Custom attributes are better** because they're reusable across multiple models and keep validation logic separate from controllers!

---

### 📋 Summary: Validation Approach Comparison

| Approach                 | Use Case                                    | Reusability | Location           |
| ------------------------ | ------------------------------------------- | ----------- | ------------------ |
| **Built-in Attributes**  | Common validations (required, email, range) | ✅ High     | DTO Properties     |
| **Custom Attributes**    | Complex/business-specific rules             | ✅ High     | Validators folder  |
| **Manual in Controller** | One-time specific checks                    | ❌ Low      | Controller methods |

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 16. Dependency Injection in Web API

### 🤔 What is Dependency Injection?

**Dependency Injection (DI)** is a design pattern that implements **Inversion of Control (IoC)** for managing dependencies between classes. Instead of a class creating its own dependencies, they are "injected" from outside.

Think of it like ordering food at a restaurant – you don't go to the kitchen to cook; the waiter **injects** the food to your table!

---

### ❌ The Problem: Tightly Coupled Code

Without Dependency Injection, each controller creates its own instance of the service:

```
┌─────────────────────────────────────────────────────────────────┐
│              TIGHTLY COUPLED (Without DI)                      │
│                                                                 │
│  StudentController           DemoController                     │
│  ┌──────────────────┐       ┌──────────────────┐             │
│  │ _logger =       │       │ _logger =       │             │
│  │ new LogToDB()   │       │ new LogToDB()   │             │
│  └────────┬─────────┘       └────────┬─────────┘             │
│           │                         │                          │
│           ▼                         ▼                          │
│  ┌──────────────────┐       ┌──────────────────┐             │
│  │     LogToDB     │       │     LogToDB     │  ❌ Multiple │
│  │    Instance 1   │       │    Instance 2   │     Instances│
│  └──────────────────┘       └──────────────────┘             │
│                                                                 │
│  ❌ Each controller creates its own instance                     │
│  ❌ To change logger, must modify ALL controllers                │
│  ❌ Hard to test (can't mock dependencies)                       │
└─────────────────────────────────────────────────────────────────┘
```

**Example: Tightly Coupled Code (BAD ❌)**

```csharp
public class DemoController : ControllerBase
{
    private readonly IMyLogger _myLogger;

    // ❌ Tightly Coupled - Creates its own instance
    public DemoController()
    {
        _myLogger = new LogToDB();  // Hardcoded dependency!
    }
}
```

**Problems with Tightly Coupled Code:**

| Issue                  | Description                                                    |
| ---------------------- | -------------------------------------------------------------- |
| **Multiple Instances** | Each controller creates its own logger instance                |
| **Hard to Change**     | Need to change code in ALL controllers to switch logger        |
| **Not Testable**       | Can't mock dependencies for unit testing                       |
| **Violates SOLID**     | Breaks Single Responsibility & Dependency Inversion principles |

---

### ✅ The Solution: Loosely Coupled with DI

With Dependency Injection, a single registration point manages all instances:

```
┌─────────────────────────────────────────────────────────────────┐
│              LOOSELY COUPLED (With DI)                         │
│                                                                 │
│                   Program.cs (DI Container)                     │
│                  ┌─────────────────────────┐                      │
│                  │ builder.Services       │                      │
│                  │   .AddScoped<IMyLogger,│                      │
│                  │    LogToMemoryServer>();│                     │
│                  └────────────┬────────────┘                      │
│                              │                                  │
│              ┌───────────────┴───────────────┐                │
│              │               │               │                │
│              ▼               ▼               ▼                │
│  StudentController   DemoController   OtherController       │
│  ┌───────────────┐   ┌───────────────┐   ┌───────────────┐  │
│  │ IMyLogger    │   │ IMyLogger    │   │ IMyLogger    │  │
│  │ (Injected)   │   │ (Injected)   │   │ (Injected)   │  │
│  └───────────────┘   └───────────────┘   └───────────────┘  │
│                                                                 │
│  ✅ One registration point for all controllers                   │
│  ✅ Change logger in ONE place (Program.cs)                      │
│  ✅ Easy to test with mock implementations                       │
└─────────────────────────────────────────────────────────────────┘
```

---

### 🤔 Why Use Dependency Injection?

| Benefit                    | Description                                      |
| -------------------------- | ------------------------------------------------ |
| **Loose Coupling**         | Classes don't create their own dependencies      |
| **Single Point of Change** | Change implementation in one place (Program.cs)  |
| **Testability**            | Easy to inject mock implementations for testing  |
| **Maintainability**        | Easier to modify and extend the application      |
| **Reusability**            | Same interface can have multiple implementations |
| **SOLID Principles**       | Follows Dependency Inversion Principle           |

---

### 📦 When and Where to Use DI?

**When to Use:**

- ✅ When a class needs external services (logging, database, email, etc.)
- ✅ When you want to swap implementations easily
- ✅ When writing unit tests with mock objects
- ✅ When following SOLID principles

**Where to Register:**

- ✅ In `Program.cs` using `builder.Services`
- ✅ Before `builder.Build()` is called

---

### � Types of Dependency Injection

There are **three main ways** to inject dependencies into a class:

```
┌─────────────────────────────────────────────────────────────────┐
│                    DI INJECTION TYPES                           │
│                                                                 │
│  ┌─────────────────┐  ┌─────────────────┐  ┌─────────────────┐ │
│  │   Constructor   │  │    Property     │  │     Method      │ │
│  │    Injection    │  │    Injection    │  │    Injection    │ │
│  │   ⭐ Preferred  │  │   Optional DI   │  │   Runtime DI    │ │
│  └─────────────────┘  └─────────────────┘  └─────────────────┘ │
│         │                     │                     │          │
│         ▼                     ▼                     ▼          │
│  Via Constructor      Via Property Setter   Via Method Param   │
│  Parameters           (public set)          (action methods)   │
└─────────────────────────────────────────────────────────────────┘
```

---

#### 1️⃣ Constructor Injection (⭐ Recommended)

Dependencies are provided through the **class constructor**. This is the **most common and preferred** approach in ASP.NET Core.

```csharp
public class DemoController : ControllerBase
{
    private readonly IMyLogger _myLogger;

    // ✅ Constructor Injection - Dependencies injected via constructor
    public DemoController(IMyLogger myLogger)
    {
        _myLogger = myLogger;  // Dependency is injected here
    }

    [HttpGet]
    public ActionResult Index()
    {
        _myLogger.Log("Index method called");
        return Ok();
    }
}
```

**Advantages:**

- ✅ Dependencies are **required** (enforced at compile time)
- ✅ **Immutable** - dependencies set once, can't be changed
- ✅ Easy to see **all dependencies** in one place
- ✅ **Testable** - easy to mock dependencies

---

#### 2️⃣ Property Injection (Setter Injection)

Dependencies are provided through **public properties**. Used when dependencies are **optional**.

```csharp
public class DemoController : ControllerBase
{
    // ✅ Property Injection - Dependency can be set via property
    public IMyLogger? MyLogger { get; set; }

    [HttpGet]
    public ActionResult Index()
    {
        // Must check for null since it's optional
        MyLogger?.Log("Index method called");
        return Ok();
    }
}
```

**Advantages:**

- ✅ Good for **optional** dependencies
- ✅ Can be changed at runtime

**Disadvantages:**

- ❌ Dependency might be null (must handle)
- ❌ Not enforced at compile time
- ❌ ASP.NET Core DI doesn't natively support property injection

---

#### 3️⃣ Method Injection

Dependencies are provided through **method parameters**. Used when dependency is only needed for a **specific method**.

```csharp
public class DemoController : ControllerBase
{
    // ✅ Method Injection - Dependency passed to specific method
    [HttpGet]
    public ActionResult Index([FromServices] IMyLogger myLogger)
    {
        myLogger.Log("Index method called");
        return Ok();
    }

    [HttpGet("other")]
    public ActionResult Other()
    {
        // This method doesn't need IMyLogger
        return Ok("No logging here");
    }
}
```

**Advantages:**

- ✅ Dependency only injected **when needed**
- ✅ Different methods can use **different services**
- ✅ Good for **rarely used** dependencies

**Disadvantages:**

- ❌ Can make method signatures complex
- ❌ Less clear dependencies at class level

---

#### 📊 Comparison: DI Injection Types

| Aspect              | Constructor Injection | Property Injection | Method Injection  |
| ------------------- | --------------------- | ------------------ | ----------------- |
| **Preferred**       | ⭐ Yes (Most used)    | Sometimes          | Rarely            |
| **Dependency**      | Required              | Optional           | Per-method        |
| **Null Safety**     | ✅ Guaranteed         | ❌ May be null     | ✅ Guaranteed     |
| **Visibility**      | All in constructor    | Scattered          | Per method        |
| **Immutability**    | ✅ Yes                | ❌ No              | N/A               |
| **ASP.NET Support** | ✅ Native             | ❌ Manual          | ✅ [FromServices] |
| **Use Case**        | Most services         | Optional services  | Specific actions  |

---

#### 🎯 Which Type Should You Use?

```
┌─────────────────────────────────────────────────────────────────┐
│                    DECISION GUIDE                               │
│                                                                 │
│  Is the dependency required for the class to work?             │
│       │                                                         │
│       ├── YES ──▶ Use Constructor Injection ⭐                  │
│       │                                                         │
│       └── NO ──▶ Is it needed for only one method?             │
│                        │                                        │
│                        ├── YES ──▶ Use Method Injection         │
│                        │           [FromServices]               │
│                        │                                        │
│                        └── NO ──▶ Use Property Injection        │
│                                   (Optional dependency)         │
└─────────────────────────────────────────────────────────────────┘
```

> 💡 **Best Practice:** Use **Constructor Injection** for 95% of cases. It's the cleanest, safest, and most testable approach!

---

### �📊 DI Lifetime Types (Scopes)

ASP.NET Core provides three service lifetimes:

| Lifetime      | Method             | Description                         | Use Case                        |
| ------------- | ------------------ | ----------------------------------- | ------------------------------- |
| **Singleton** | `AddSingleton<>()` | One instance for entire application | Configuration, Caching          |
| **Scoped**    | `AddScoped<>()`    | One instance per HTTP request       | Database Context, Logging       |
| **Transient** | `AddTransient<>()` | New instance every time requested   | Lightweight, stateless services |

```csharp
// Singleton: Same instance throughout the app
builder.Services.AddSingleton<IMyLogger, LogToFile>();

// Scoped: New instance per HTTP request
builder.Services.AddScoped<IMyLogger, LogToDB>();

// Transient: New instance every time DI container is asked
builder.Services.AddTransient<IMyLogger, LogToMemoryServer>();
```

---

### 🎮 Example from This Project

**Step 1: Create the Interface**

```csharp
// MyLogging/IMyLogger.cs
namespace ASPNETCoreWebAPI.MyLogging
{
    public interface IMyLogger
    {
        void Log(string message);
    }
}
```

**Step 2: Create Multiple Implementations**

```csharp
// MyLogging/LogToDB.cs
namespace ASPNETCoreWebAPI.MyLogging
{
    public class LogToDB : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("LogToDB");
        }
    }
}

// MyLogging/LogToFile.cs
namespace ASPNETCoreWebAPI.MyLogging
{
    public class LogToFile : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("LogToFile");
        }
    }
}

// MyLogging/LogToMemoryServer.cs
namespace ASPNETCoreWebAPI.MyLogging
{
    public class LogToMemoryServer : IMyLogger
    {
        public void Log(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("LogToMemoryServer");
        }
    }
}
```

**Step 3: Register in Program.cs (DI Container)**

```csharp
// Program.cs
using ASPNETCoreWebAPI.MyLogging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ✅ Register DI - One place to change implementation!
builder.Services.AddScoped<IMyLogger, LogToMemoryServer>();

var app = builder.Build();
```

> 💡 **To switch logger**: Just change `LogToMemoryServer` to `LogToDB` or `LogToFile` in Program.cs. No controller changes needed!

**Step 4: Inject in Controller (Loosely Coupled)**

```csharp
// Controllers/DemoController.cs
using ASPNETCoreWebAPI.MyLogging;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly IMyLogger _myLogger;

        // ✅ Loosely Coupled - Dependency is injected
        public DemoController(IMyLogger myLogger)
        {
            _myLogger = myLogger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _myLogger.Log("Index method started");
            return Ok();
        }
    }
}
```

---

### 🔑 Tightly Coupled vs Loosely Coupled

| Aspect                | Tightly Coupled ❌            | Loosely Coupled ✅             |
| --------------------- | ----------------------------- | ------------------------------ |
| **Instance Creation** | `new LogToDB()` in controller | DI Container provides instance |
| **Change Logger**     | Modify ALL controllers        | Change ONE line in Program.cs  |
| **Testing**           | Hard to mock                  | Easy to inject mocks           |
| **Code Location**     | Scattered across controllers  | Centralized in Program.cs      |
| **Maintenance**       | Difficult                     | Easy                           |
| **Flexibility**       | Low                           | High                           |

---

### 📐 How DI Works in ASP.NET Core

```
┌─────────────────────────────────────────────────────────────────┐
│                    DI Container Flow                           │
│                                                                 │
│  1️⃣ Application Starts                                         │
│      │                                                          │
│      ▼                                                          │
│  ┌──────────────────────────────────────────────────────────┐  │
│  │  Program.cs                                                │  │
│  │  builder.Services.AddScoped<IMyLogger, LogToMemoryServer>()│  │
│  └───────────────────────────┬──────────────────────────────┘  │
│                              │                                  │
│  2️⃣ HTTP Request Comes In    │                                  │
│      │                        │                                  │
│      ▼                        ▼                                  │
│  ┌──────────────────┐  ┌─────────────────────────────┐  │
│  │  DI Container    │  │  Looks for IMyLogger         │  │
│  │  resolves        │──▶  Finds: LogToMemoryServer    │  │
│  │  dependencies    │  │  Creates instance            │  │
│  └────────┬─────────┘  └─────────────┬───────────────┘  │
│           │                          │                          │
│  3️⃣ Injects into Controller         │                          │
│           │                          │                          │
│           ▼                          ▼                          │
│  ┌──────────────────────────────────────────────┐             │
│  │  DemoController(IMyLogger myLogger)              │             │
│  │  {                                               │             │
│  │      _myLogger = myLogger; // Injected!          │             │
│  │  }                                               │             │
│  └──────────────────────────────────────────────┘             │
│                                                                 │
│  4️⃣ Controller uses the injected service                       │
└─────────────────────────────────────────────────────────────────┘
```

---

### 📁 Project Structure with DI

```
CollegeApp/
├── Controllers/
│   ├── DemoController.cs       ◀── Injects IMyLogger
│   └── StudentController.cs    ◀── Can also inject IMyLogger
├── MyLogging/
│   ├── IMyLogger.cs            ◀── Interface (Contract)
│   ├── LogToDB.cs              ◀── Implementation 1
│   ├── LogToFile.cs            ◀── Implementation 2
│   └── LogToMemoryServer.cs    ◀── Implementation 3 (Currently used)
└── Program.cs                  ◀── DI Registration (Central location)
```

---

### 💡 Real-World Benefit

**Scenario:** You want to switch from logging to MemoryServer to logging to Database.

**❌ Without DI (Tightly Coupled):**

```csharp
// Must change EVERY controller file!
// DemoController.cs
_myLogger = new LogToDB();  // Was: new LogToMemoryServer()

// StudentController.cs
_myLogger = new LogToDB();  // Was: new LogToMemoryServer()

// ... and ALL other controllers
```

**✅ With DI (Loosely Coupled):**

```csharp
// Change ONLY Program.cs - ONE line!
builder.Services.AddScoped<IMyLogger, LogToDB>();  // Was: LogToMemoryServer

// ✅ All controllers automatically use the new implementation!
// ✅ No controller code changes needed!
```

---

### 🎯 Best Practices for DI

1. **Program interfaces, not implementations** – Depend on `IMyLogger`, not `LogToDB`
2. **Use constructor injection** – Preferred way to inject dependencies
3. **Choose correct lifetime** – Scoped for request-based, Singleton for app-wide
4. **Keep services focused** – Each service should do one thing well
5. **Register at startup** – All registrations in Program.cs before `Build()`
6. **Avoid Service Locator pattern** – Let DI container do the work

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 17. Built-in Logger in Web API

### 🤔 What is Built-in Logger?

ASP.NET Core provides a **built-in logging framework** through the `ILogger<T>` interface. It's automatically available via Dependency Injection – no extra packages needed!

---

### 📊 Log Levels in Web API

ASP.NET Core uses **7 log levels** to categorize messages by severity:

```
┌─────────────────────────────────────────────────────────────────┐
│                    LOG LEVELS HIERARCHY                        │
│                                                                 │
│  Level 0  ┌────────────┐  Most detailed, verbose output           │
│  TRACE    │   Trace    │  ◀ Development debugging                │
│          └────────────┘                                          │
│              │                                                   │
│  Level 1  ┌────────────┐  Debugging information                   │
│  DEBUG    │   Debug    │  ◀ Development only                     │
│          └────────────┘                                          │
│              │                                                   │
│  Level 2  ┌────────────┐  General flow information                │
│  INFO     │Information │  ◀ Production friendly                  │
│          └────────────┘                                          │
│              │                                                   │
│  Level 3  ┌────────────┐  Unexpected but handled issues           │
│  WARNING  │  Warning   │  ◀ Something to watch                   │
│          └────────────┘                                          │
│              │                                                   │
│  Level 4  ┌────────────┐  Error occurred, operation failed        │
│  ERROR    │   Error    │  ◀ Needs attention                      │
│          └────────────┘                                          │
│              │                                                   │
│  Level 5  ┌────────────┐  System crash, requires immediate fix     │
│ CRITICAL  │  Critical  │  ◀ Highest priority                     │
│          └────────────┘                                          │
│              │                                                   │
│  Level 6  ┌────────────┐  Logging disabled                        │
│  NONE     │   None     │  ◀ No logs at all                       │
│          └────────────┘                                          │
└─────────────────────────────────────────────────────────────────┘
```

| Level           | Value | Method             | Usage                                         |
| --------------- | ----- | ------------------ | --------------------------------------------- |
| **Trace**       | 0     | `LogTrace()`       | Most detailed logs, debugging internals       |
| **Debug**       | 1     | `LogDebug()`       | Development debugging information             |
| **Information** | 2     | `LogInformation()` | General application flow                      |
| **Warning**     | 3     | `LogWarning()`     | Unexpected events that don't stop execution   |
| **Error**       | 4     | `LogError()`       | Errors that stop current operation            |
| **Critical**    | 5     | `LogCritical()`    | System failures requiring immediate attention |
| **None**        | 6     | N/A                | Disables logging completely                   |

---

### � Example from This Project

**Using ILogger in Controller:**

```csharp
// Controllers/DemoController.cs
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        private readonly ILogger<DemoController> _logger;

        // ✅ Built-in ILogger injected via constructor
        public DemoController(ILogger<DemoController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public ActionResult Index()
        {
            _logger.LogTrace("Log message from Trace method");
            _logger.LogDebug("Log message from Debug method");
            _logger.LogInformation("Log message from Information method");
            _logger.LogWarning("Log message from Warning method");
            _logger.LogError("Log message from Error method");
            _logger.LogCritical("Log message from Critical method");

            return Ok();
        }
    }
}
```

---

### ⚙️ Configuring Log Levels in appsettings.json

Configure which log levels to display for different providers:

```json
// appsettings.json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    },
    "Console": {
      "LogLevel": {
        "Default": "Error",
        "Microsoft.Hosting": "Trace"
      }
    },
    "Debug": {
      "LogLevel": {
        "Default": "Trace",
        "Microsoft.Hosting": "Error"
      }
    }
  },
  "AllowedHosts": "*"
}
```

**Configuration Explained:**

| Setting                             | Meaning                                            |
| ----------------------------------- | -------------------------------------------------- |
| `"Default": "Information"`          | Show Information+ for all categories               |
| `"Microsoft.AspNetCore": "Warning"` | Show only Warning+ for ASP.NET Core framework logs |
| `"Console": { ... }`                | Specific settings for console logging provider     |
| `"Debug": { ... }`                  | Specific settings for debug output window          |

> 💡 **Note:** Setting a level shows that level AND all levels above it. E.g., `"Warning"` shows Warning, Error, and Critical.

---

### 🔑 Key Points

1. **Use `ILogger<T>`** – T is your class name (e.g., `ILogger<DemoController>`)
2. **Inject via constructor** – ASP.NET Core DI provides it automatically
3. **Choose appropriate level** – Don't use Error for informational messages
4. **Configure per environment** – Use `appsettings.Development.json` for dev settings
5. **Filter by category** – Control logs from specific namespaces

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 18. Serilog – Advanced Logging

### 🤔 What is Serilog?

**Serilog** is a third-party logging library for .NET that provides **structured logging** with support for multiple output destinations (called "sinks") like files, databases, and cloud services.

---

### 📦 Installation

```bash
dotnet add package Serilog.AspNetCore
dotnet add package Serilog.Sinks.File
```

---

### ⚙️ Configuration in Program.cs

```csharp
using Serilog;

// Configure Serilog
Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("Log/log.txt", rollingInterval: RollingInterval.Minute)
    .CreateLogger();

// Use Serilog along with built-in logger
builder.Logging.AddSerilog();

// OR override built-in logger completely
// builder.Host.UseSerilog();
```

---

### � Key Points

| Method                         | Behavior                            |
| ------------------------------ | ----------------------------------- |
| `builder.Logging.AddSerilog()` | Adds Serilog alongside built-in     |
| `builder.Host.UseSerilog()`    | Replaces built-in logger completely |

| RollingInterval | Description                   |
| --------------- | ----------------------------- |
| `Minute`        | New file every minute         |
| `Day`           | New file daily (⭐ most used) |
| `Infinite`      | Single file, no rolling       |

> 💡 Serilog integrates with `ILogger<T>`, so you use it the same way as the built-in logger in your controllers!

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 19. Entity Framework Core

### 🤔 What is Entity Framework Core?

**Entity Framework Core (EF Core)** is an **ORM (Object-Relational Mapper)** for .NET. It lets you work with databases using C# objects instead of writing raw SQL queries.

---

### 📚 What is ORM (Object-Relational Mapping)?

**ORM** is a technique that connects your C# code to a database. Instead of writing SQL queries manually, you work with C# classes and objects.

**Without ORM (Traditional Way):**

```csharp
// You write raw SQL queries
string sql = "SELECT * FROM Students WHERE Id = 1";
SqlCommand cmd = new SqlCommand(sql, connection);
// Then manually convert results to C# objects... 😓
```

**With ORM (EF Core Way):**

```csharp
// You write C# code, EF Core handles SQL for you!
var student = _dbContext.Students.Where(s => s.Id == 1).FirstOrDefault();
// EF Core automatically converts database rows to C# objects! 😊
```

---

### ❓ Why Use Entity Framework Core?

| Problem Without EF Core               | Solution With EF Core                  |
| ------------------------------------- | -------------------------------------- |
| Write manual SQL queries              | Use LINQ (C# code) instead             |
| Manually map database rows to objects | Automatic mapping to C# objects        |
| Database changes require code updates | Migrations handle schema changes       |
| Hard to switch databases              | Just change the provider (SQL → MySQL) |
| SQL injection risks                   | Parameterized queries by default       |

**Benefits:**

- ✅ **No SQL knowledge required** – Write C# code, EF Core generates SQL
- ✅ **Type-safe queries** – Compile-time error checking
- ✅ **Faster development** – Less boilerplate code
- ✅ **Easy maintenance** – Changes in one place (C# model)
- ✅ **Cross-database support** – SQL Server, MySQL, PostgreSQL, SQLite

---

### 🔄 How EF Core Works

```
┌─────────────────────────────────────────────────────────────────────────┐
│                         EF CORE WORKFLOW                                │
│                                                                         │
│   Your App          EF Core              Database                       │
│   ────────          ───────              ────────                       │
│                                                                         │
│   LINQ Query   ──►  Translates to   ──►  SQL Query                     │
│   (C# Code)         SQL                  Executes                       │
│                                                                         │
│   C# Objects   ◄──  Converts to     ◄──  Result Data                   │
│   (Student)         Objects              (Rows)                         │
└─────────────────────────────────────────────────────────────────────────┘
```

**Example:**

```csharp
// You write LINQ (C# code)
var students = _dbContext.Students.Where(s => s.Id == 1).FirstOrDefault();

// EF Core translates to SQL
// SELECT * FROM Students WHERE Id = 1
```

---

### 📊 Two Approaches in EF Core

| Approach          | Description                               | When to Use                |
| ----------------- | ----------------------------------------- | -------------------------- |
| **Code First** ⭐ | Create C# classes → EF generates database | New projects, full control |
| **DB First**      | Create database → EF generates C# classes | Existing databases         |

---

### 🏗️ Code First Approach (Used in This Project)

**Code First** means you write C# code first, and EF Core creates the database for you.

**Think of it like this:**

- You design your house (C# classes) on paper first
- Then the builder (EF Core) constructs the actual house (database)

**Step-by-Step Process:**

```
┌─────────────────────────────────────────────────────────────────────────┐
│  CODE FIRST WORKFLOW                                                    │
│                                                                         │
│  Step 1: Create Entity Model (C# Class)                                 │
│          └── Student.cs (defines table structure)                       │
│                                                                         │
│  Step 2: Create DbContext                                               │
│          └── CollegeDBContext.cs (database session manager)             │
│                                                                         │
│  Step 3: Configure Connection String                                    │
│          └── appsettings.json (where is your database?)                 │
│                                                                         │
│  Step 4: Register DbContext                                             │
│          └── Program.cs (tell ASP.NET Core about your context)          │
│                                                                         │
│  Step 5: Create Migration                                               │
│          └── Add-Migration InitialSetup (creates migration files)       │
│                                                                         │
│  Step 6: Update Database                                                │
│          └── Update-Database (creates actual tables in SQL Server)      │
└─────────────────────────────────────────────────────────────────────────┘
```

**Why Code First?**

- ✅ Full control over your database design
- ✅ Version control for database changes (migrations)
- ✅ Easy to modify – change C# class, run migration
- ✅ No need to manually create tables in SQL Server
- ✅ Perfect for new projects

---

### 🔄 DB First Approach (Alternative)

**DB First** means the database already exists, and EF Core generates C# classes from it.

```
┌─────────────────────────────────────────────────────────────────────────┐
│  DB FIRST WORKFLOW                                                      │
│                                                                         │
│  Step 1: Design Database in SQL Server                                  │
│          └── Create tables, relationships manually                      │
│                                                                         │
│  Step 2: Scaffold (Generate C# code from DB)                            │
│          └── dotnet ef dbcontext scaffold "ConnectionString" Provider   │
│                                                                         │
│  Step 3: Use Generated Classes                                          │
│          └── EF creates DbContext and entity models for you             │
└─────────────────────────────────────────────────────────────────────────┘
```

**When to use DB First?**

- ✅ Working with an existing database
- ✅ Database designed by a DBA team
- ✅ Legacy systems migration

---

### 📦 Required NuGet Packages

```bash
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Tools
```

---

### 🔗 Connection String

Add connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "CollegeAppDBConnection": "Data Source=localhost; Initial Catalog=CollegeAppDB; Integrated Security=True; Trust Server Certificate=True"
  }
}
```

| Part                       | Meaning                           |
| -------------------------- | --------------------------------- |
| `Data Source`              | Server name (localhost for local) |
| `Initial Catalog`          | Database name                     |
| `Integrated Security=True` | Use Windows authentication        |
| `Trust Server Certificate` | Trust the SSL certificate         |

---

### 🏗️ EF Core Structure

```
┌─────────────────────────────────────────────────────────────────────────┐
│  EF CORE STRUCTURE                                                      │
│                                                                         │
│  Each database needs its own DbContext:                                 │
│                                                                         │
│  ┌──────────────────────┐          ┌──────────────────────┐            │
│  │   CollegeDBContext   │  ◄────►  │    CollegeAppDB      │            │
│  │   ─────────────────  │          │    (SQL Server)      │            │
│  │  DbSet<Student>      │  ◄────►  │    Students Table    │            │
│  │  DbSet<Course>       │  ◄────►  │    Courses Table     │            │
│  └──────────────────────┘          └──────────────────────┘            │
└─────────────────────────────────────────────────────────────────────────┘
```

---

### 📝 Step 1: Create Entity Model

```csharp
// Data/Student.cs
namespace ASPNETCoreWebAPI.Data
{
    public class Student
    {
        public int Id { get; set; }
        public string StudentName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public DateTime DOB { get; set; }
    }
}
```

---

### 📝 Step 2: Create DbContext

```csharp
// Data/CollegeDBContext.cs
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreWebAPI.Data
{
    public class CollegeDBContext : DbContext
    {
        public CollegeDBContext(DbContextOptions<CollegeDBContext> options) : base(options)
        {
        }

        // Each DbSet = One Table in Database
        public DbSet<Student> Students { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Apply entity configurations
            modelBuilder.ApplyConfiguration(new StudentConfig());
        }
    }
}
```

---

### 📝 Step 3: Create Entity Configuration (Optional but Recommended)

Separate configuration keeps DbContext clean:

```csharp
// Data/Config/StudentConfig.cs
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ASPNETCoreWebAPI.Data.Config
{
    public class StudentConfig : IEntityTypeConfiguration<Student>
    {
        public void Configure(EntityTypeBuilder<Student> builder)
        {
            // Table name
            builder.ToTable("Students");

            // Primary key
            builder.HasKey(t => t.Id);

            // Auto-increment
            builder.Property(x => x.Id).UseIdentityColumn();

            // Column constraints
            builder.Property(n => n.StudentName).IsRequired().HasMaxLength(250);
            builder.Property(n => n.Address).IsRequired(false).HasMaxLength(500);
            builder.Property(n => n.Email).IsRequired().HasMaxLength(250);

            // Seed default data
            builder.HasData(new List<Student>()
            {
                new Student
                {
                    Id = 1,
                    StudentName = "Kartik",
                    Email = "Kartik123@gmail.com",
                    Address = "Hyd, India",
                    DOB = new DateTime(2005, 08, 03)
                },
                new Student
                {
                    Id = 2,
                    StudentName = "Aryan",
                    Email = "Aryan123@gmail.com",
                    Address = "Banglore, India",
                    DOB = new DateTime(2004, 09, 03)
                }
            });
        }
    }
}
```

---

### 📝 Step 4: Register DbContext in Program.cs

```csharp
// Program.cs
using Microsoft.EntityFrameworkCore;

builder.Services.AddDbContext<CollegeDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("CollegeAppDBConnection"));
});
```

---

### 🔄 What is Migration?

**Migration** is a way to update your database schema when your C# models change. Think of it as version control for your database!

```
┌─────────────────────────────────────────────────────────────────────────┐
│  MIGRATION WORKFLOW                                                     │
│                                                                         │
│  1. Change C# Model    ──►  Add new property to Student.cs              │
│  2. Add Migration      ──►  Creates migration file with changes         │
│  3. Update Database    ──►  Applies changes to actual database          │
└─────────────────────────────────────────────────────────────────────────┘
```

---

### ⚡ Migration Commands

Run these in **Package Manager Console** (Visual Studio) or **Terminal**:

| Command                                | Description                            |
| -------------------------------------- | -------------------------------------- |
| `Add-Migration InitialDBSetup`         | Create first migration                 |
| `Add-Migration AddDataToStudentsTable` | Create migration with seed data        |
| `Update-Database`                      | Apply migrations to database           |
| `Remove-Migration`                     | Remove last migration (if not applied) |

**Example Workflow:**

```bash
# Step 1: Create initial migration
Add-Migration InitialDBSetup

# Step 2: Apply to database (creates tables)
Update-Database

# Step 3: Later, add seed data
Add-Migration AddDataToStudentsTable
Update-Database

# Step 4: Modify schema (add constraints)
Add-Migration ModifyStudentsSchema
Update-Database
```

---

### 📁 Project Structure with EF Core

```
ASPNETCoreWebAPI/
├── Controllers/
│   └── StudentController.cs    ◀── Uses DbContext for CRUD
├── Data/
│   ├── CollegeDBContext.cs     ◀── Database context
│   ├── Student.cs              ◀── Entity model
│   └── Config/
│       └── StudentConfig.cs    ◀── Entity configuration
├── Migrations/
│   ├── 20260204075146_InitialDBSetup.cs
│   ├── 20260204083931_AddDataToStudentsTable.cs
│   ├── 20260204085406_ModifyStudentsSchema.cs
│   └── CollegeDBContextModelSnapshot.cs
├── appsettings.json            ◀── Connection string
└── Program.cs                  ◀── DbContext registration
```

---

### 🎮 CRUD Operations with EF Core

#### **Create (INSERT)**

```csharp
Student student = new Student
{
    StudentName = model.StudentName,
    Email = model.Email,
    Address = model.Address,
    DOB = model.DOB
};

_dbContext.Students.Add(student);    // Add to DbSet
_dbContext.SaveChanges();            // Execute INSERT
```

#### **Read (SELECT)**

```csharp
// Get all students
var students = _dbContext.Students.ToList();

// Get by ID
var student = _dbContext.Students.Where(s => s.Id == id).FirstOrDefault();

// Get by name
var student = _dbContext.Students.Where(s => s.StudentName == name).FirstOrDefault();
```

#### **Update (UPDATE)**

```csharp
var existingStudent = _dbContext.Students.Where(s => s.Id == model.Id).FirstOrDefault();

existingStudent.StudentName = model.StudentName;
existingStudent.Email = model.Email;
existingStudent.Address = model.Address;

_dbContext.SaveChanges();    // Execute UPDATE
```

#### **Delete (DELETE)**

```csharp
var student = _dbContext.Students.Where(s => s.Id == id).FirstOrDefault();

_dbContext.Students.Remove(student);    // Mark for deletion
_dbContext.SaveChanges();               // Execute DELETE
```

---

### 🔑 Key Points

| Concept                  | Description                            |
| ------------------------ | -------------------------------------- |
| **DbContext**            | Represents a session with the database |
| **DbSet<T>**             | Represents a table in the database     |
| **SaveChanges()**        | Commits all changes to the database    |
| **Migration**            | Version control for database schema    |
| **Entity Configuration** | Define table schema using Fluent API   |
| **HasData()**            | Seed default data into tables          |

> 💡 **Tip:** Always call `SaveChanges()` after Add, Update, or Remove operations!

⬆️ [Back to Table of Contents](#-table-of-contents)

---

### 19.1. Creating Foreign Keys in EF Core

**Foreign Keys** create relationships between tables. In EF Core Code First, you define them using **navigation properties** and **Fluent API**.

#### **Step 1: Define Navigation Properties**

📁 **Data/Student.cs** (Child Entity)

```csharp
public class Student
{
    public int Id { get; set; }
    public string StudentName { get; set; }
    public string Email { get; set; }

    // Foreign Key Property
    public int? DepartmentId { get; set; }

    // Navigation Property
    public virtual Department? Department { get; set; }
}
```

📁 **Data/Department.cs** (Parent Entity)

```csharp
public class Department
{
    public int Id { get; set; }
    public string DepartmentName { get; set; }

    // Collection Navigation Property
    public virtual ICollection<Student> Students { get; set; }
}
```

**Key Points:**

- `DepartmentId` = Foreign key property (nullable for optional relationship)
- `Department` = Navigation property to parent
- `Students` = Collection navigation property in parent
- `virtual` keyword enables lazy loading

---

#### **Step 2: Configure with Fluent API**

📁 **Data/Config/StudentConfig.cs**

```csharp
public class StudentConfig : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        // Configure Foreign Key Relationship
        builder.HasOne(s => s.Department)           // Student has one Department
               .WithMany(d => d.Students)           // Department has many Students
               .HasForeignKey(s => s.DepartmentId)  // Foreign key column
               .HasConstraintName("FK_Student_Department"); // Constraint name in DB
    }
}
```

**Relationship Breakdown:**

- `HasOne()` → Student has ONE Department
- `WithMany()` → Department has MANY Students
- `HasForeignKey()` → Specifies the FK column
- `HasConstraintName()` → Custom constraint name (optional)

---

#### **Database Result**

After migration, SQL Server creates:

```sql
ALTER TABLE Students
ADD CONSTRAINT FK_Student_Department
FOREIGN KEY (DepartmentId) REFERENCES Departments(Id);
```

---

#### **Relationship Types**

| Code Pattern           | Relationship Type  |
| ---------------------- | ------------------ |
| `HasOne().WithMany()`  | One-to-Many (1:N)  |
| `HasOne().WithOne()`   | One-to-One (1:1)   |
| `HasMany().WithMany()` | Many-to-Many (N:M) |

---

#### **🎯 Quick Tips**

1. **Nullable FK** → `int?` for optional relationships
2. **Required FK** → `int` for mandatory relationships
3. **Use `virtual`** → Enables lazy loading
4. **OnDelete Behavior:**
   ```csharp
   .OnDelete(DeleteBehavior.Cascade)  // Delete students when department deleted
   .OnDelete(DeleteBehavior.Restrict) // Prevent deletion if students exist
   ```

⬆️ [Back to Table of Contents](#-table-of-contents)

---

### 19.2. Entity Framework Database First Approach

Previously, you learned the **Code First** approach where you create C# classes first, and EF Core generates the database for you. Now let's learn the **Database First** approach, where an existing database is used to generate C# entity classes automatically!

---

#### 🤔 What is Database First Approach?

**Database First** is a development approach where:

1. ✅ You start with an **existing database** (e.g., Northwind, AdventureWorks)
2. ✅ You **scaffold** (reverse-engineer) the database to generate C# entity classes
3. ✅ EF Core automatically creates:
   - Entity classes (one for each table)
   - DbContext class
   - Relationships (foreign keys, navigation properties)

```
┌──────────────────────────────────────────────────────────────────┐
│                  DATABASE FIRST WORKFLOW                         │
│                                                                  │
│   ┌─────────────────┐          Scaffold           ┌───────────┐ │
│   │  Existing DB    │ ────────────────────────▶   │  Entities │ │
│   │  (Northwind)    │                             │  Classes  │ │
│   │                 │                             │           │ │
│   │  - Customers    │    Reverse Engineering      │  Customer │ │
│   │  - Orders       │    (Scaffold-DbContext)     │  Order    │ │
│   │  - Products     │                             │  Product  │ │
│   │  - etc.         │                             │  etc.     │ │
│   └─────────────────┘                             └───────────┘ │
│           │                                            │         │
│           │                                            │         │
│           ▼                                            ▼         │
│   ┌──────────────┐                         ┌────────────────┐   │
│   │   Tables     │  ◀───── Use in API ────│   DbContext    │   │
│   │   Views      │                         │   (Generated)  │   │
│   └──────────────┘                         └────────────────┘   │
└──────────────────────────────────────────────────────────────────┘
```

---

#### 🆚 Code First vs Database First

| Aspect              | Code First                     | Database First               |
| ------------------- | ------------------------------ | ---------------------------- |
| **Starting Point**  | C# Classes                     | Existing Database            |
| **Direction**       | Code → Database                | Database → Code              |
| **Control**         | Full control over schema       | Database schema already set  |
| **Use Case**        | New projects                   | Legacy databases             |
| **Migrations**      | ✅ Yes (manage schema changes) | ❌ No (manual DB changes)    |
| **Team Preference** | Developer-centric              | DBA-centric                  |
| **Example**         | New College App                | Microsoft Northwind Database |

---

#### 📚 Real-World Example: Microsoft Northwind Database

For this tutorial, we'll use the classic **Northwind** database from Microsoft, which contains sample data for a trading company with customers, orders, products, and employees.

---

### 🚀 Step-by-Step Implementation

#### 📝 Step 1: Get the Northwind Database

1. Download the Northwind database script from [Microsoft's GitHub repository](https://github.com/microsoft/sql-server-samples/tree/master/samples/databases/northwind-pubs)
2. Open **SQL Server Management Studio (SSMS)**
3. Connect to your SQL Server instance (`localhost`)
4. Execute the Northwind SQL script to create the database

> 💡 **Tip:** You can use any existing database you have access to for this approach!

---

#### 📝 Step 2: Get Connection String from Visual Studio

**Using Server Explorer:**

1. Open Visual Studio → **Server Explorer**
2. Right-click **Data Connections** → **Add Connection**
3. Connect to your SQL Server:
   - **Server name:** `localhost`
   - **Database:** `Northwind`
   - **Authentication:** Windows Authentication (or SQL Server Auth)
4. After connection, right-click the database → **Properties**
5. Copy the **Connection String** value:

```
Data Source=localhost;Initial Catalog=Northwind;Integrated Security=True;Trust Server Certificate=True
```

> 💡 **Trust Server Certificate=True** is needed for localhost development to bypass SSL certificate validation.

---

#### 📝 Step 3: Scaffold the Database (Reverse Engineering)

Now comes the magic! Use EF Core CLI to automatically generate entity classes from your database.

**Run this command in Package Manager Console:**

```powershell
Scaffold-DbContext "Data Source=localhost;Initial Catalog=Northwind;Integrated Security=True;Trust Server Certificate=True" Microsoft.EntityFrameworkCore.SqlServer -OutputDir EFDBFirst
```

**What this command does:**

| Part                                      | Explanation                                  |
| ----------------------------------------- | -------------------------------------------- |
| `Scaffold-DbContext`                      | EF Core command to reverse-engineer database |
| `"Data Source=..."`                       | Connection string to your database           |
| `Microsoft.EntityFrameworkCore.SqlServer` | EF Core provider for SQL Server              |
| `-OutputDir EFDBFirst`                    | Output folder for generated files            |

**Generated Files:**

```
EFDBFirst/
├── NorthwindContext.cs          ← DbContext class
├── Customer.cs                  ← Entity classes (one per table)
├── Order.cs
├── Product.cs
├── Employee.cs
├── Category.cs
├── OrderDetail.cs
└── ... (all tables/views become classes!)
```

---

#### 📦 Generated Entity Class Example

**Customer.cs** (Auto-generated from `Customers` table):

```csharp
using System;
using System.Collections.Generic;

namespace ASPNETCoreWebAPI.EFDBFirst;

public partial class Customer
{
    public string CustomerId { get; set; } = null!;

    public string CompanyName { get; set; } = null!;

    public string? ContactName { get; set; }

    public string? ContactTitle { get; set; }

    public string? Address { get; set; }

    public string? City { get; set; }

    public string? Region { get; set; }

    public string? PostalCode { get; set; }

    public string? Country { get; set; }

    public string? Phone { get; set; }

    public string? Fax { get; set; }

    // ✅ Navigation Property - Automatically created!
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<CustomerDemographic> CustomerTypes { get; set; } = new List<CustomerDemographic>();
}
```

**Key Features:**

- ✅ Properties match table columns exactly
- ✅ `virtual` keyword enables lazy loading
- ✅ Navigation properties for relationships (Orders collection)
- ✅ `= null!` suppresses nullable warnings for required fields

---

#### 🗄️ Generated DbContext Example

**NorthwindContext.cs** (Partial snippet - auto-generated):

```csharp
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace ASPNETCoreWebAPI.EFDBFirst;

public partial class NorthwindContext : DbContext
{
    public NorthwindContext()
    {
    }

    public NorthwindContext(DbContextOptions<NorthwindContext> options)
        : base(options)
    {
    }

    // ✅ DbSets for all tables
    public virtual DbSet<Customer> Customers { get; set; }
    public virtual DbSet<Order> Orders { get; set; }
    public virtual DbSet<Product> Products { get; set; }
    public virtual DbSet<Employee> Employees { get; set; }
    // ... all other tables

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // ✅ All table configurations (foreign keys, constraints)
        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasIndex(e => e.City, "City");
            entity.Property(e => e.CustomerId).HasMaxLength(5).IsFixedLength();
            entity.Property(e => e.CompanyName).HasMaxLength(40);
            // ... more configurations
        });

        // OnModelCreatingPartial(modelBuilder); allows custom code
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
```

**Key Features:**

- ✅ All DbSets automatically created
- ✅ Relationships and foreign keys configured
- ✅ Column constraints (max length, required, etc.)
- ✅ `partial` class allows you to extend without modifying generated code

---

#### 📝 Step 4: Register Connection String in appsettings.json

Add your connection string to `appsettings.json`:

```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "CollegeAppDBConnection": "Data Source=localhost;Initial Catalog=CollegeAppDB;Integrated Security=True;Trust Server Certificate=True",
    "EFDBFirstDBConnection": "Data Source=localhost;Initial Catalog=Northwind;Integrated Security=True;Trust Server Certificate=True"
  }
}
```

> 💡 **Why separate connection strings?** You can have multiple databases in one project!

---

#### 📝 Step 5: Register DbContext in Program.cs

Register the auto-generated `NorthwindContext` in `Program.cs`:

```csharp
using ASPNETCoreWebAPI.EFDBFirst;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Register Northwind DbContext for Database First
builder.Services.AddDbContext<NorthwindContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("EFDBFirstDBConnection"));
});

// ... other services

var app = builder.Build();

// ... rest of the configuration
```

**What this does:**

- Registers `NorthwindContext` in Dependency Injection
- Reads connection string from `appsettings.json`
- Makes DbContext available to controllers via constructor injection

---

#### 📝 Step 6: Use DbContext in Controller

Now you can use the auto-generated entities and DbContext in your API!

**DemoController.cs:**

```csharp
using ASPNETCoreWebAPI.EFDBFirst;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DemoController : ControllerBase
    {
        // ✅ Inject NorthwindContext via Dependency Injection
        private readonly NorthwindContext _dbContext;
        private readonly ILogger<DemoController> _logger;

        public DemoController(ILogger<DemoController> logger, NorthwindContext northwindContext)
        {
            _logger = logger;
            _dbContext = northwindContext;
        }

        // ✅ GET: api/Demo/customers
        [HttpGet("customers", Name = "GetCustomerData")]
        public IEnumerable<dynamic> Get()
        {
            // Query the database using auto-generated entities!
            return _dbContext.Customers.ToList();
        }

        // Example: Get orders for a specific customer
        [HttpGet("customers/{id}/orders")]
        public IEnumerable<dynamic> GetCustomerOrders(string id)
        {
            return _dbContext.Orders
                .Where(o => o.CustomerId == id)
                .ToList();
        }
    }
}
```

**Key Points:**

- ✅ Inject `NorthwindContext` in constructor
- ✅ Use `_dbContext.Customers`, `_dbContext.Orders`, etc.
- ✅ LINQ queries work automatically!
- ✅ Navigation properties allow easy relationship traversal

---

#### 📝 Step 7: Test Your API

**Run the application and test:**

```http
GET https://localhost:7052/api/Demo/customers
```

**Sample Response:**

```json
[
  {\r
    "customerId": "ALFKI",\r
    "companyName": "Alfreds Futterkiste",\r
    "contactName": "Maria Anders",\r
    "contactTitle": "Sales Representative",\r
    "address": "Obere Str. 57",\r
    "city": "Berlin",\r
    "region": null,\r
    "postalCode": "12209",\r
    "country": "Germany",\r
    "phone": "030-0074321",\r
    "fax": "030-0076545"\r
  },\r
  {\r
    "customerId": "ANATR",\r
    "companyName": "Ana Trujillo Emparedados y helados",\r
    "contactName": "Ana Trujillo",\r
    "contactTitle": "Owner",\r
    "address": "Avda. de la Constitución 2222",\r
    "city": "México D.F.",\r
    "region": null,\r
    "postalCode": "05021",\r
    "country": "Mexico",\r
    "phone": "(5) 555-4729",\r
    "fax": "(5) 555-3745"\r
  }\r
  // ... more customers\r
]
```

✅ **Success!** Your API is now reading from the Northwind database using auto-generated entities!

---

### 🎯 Key Takeaways

1. **Database First** = Database → Code (reverse of Code First)
2. **`Scaffold-DbContext`** command auto-generates:
   - Entity classes for all tables
   - DbContext with all configurations
   - Navigation properties for relationships
3. **Connection String** from Server Explorer → Properties
4. **Register DbContext** in `Program.cs` using dependency injection
5. **Use in Controller** via constructor injection
6. **Multiple DbContexts** possible (Code First + Database First in same project!)
7. **`partial` classes** allow extending generated code without modification

---

### 💡 Best Practices

| Practice                     | Recommendation                                 |
| ---------------------------- | ---------------------------------------------- |
| **Modifying Generated Code** | ❌ DON'T edit generated files directly         |
| **Extending Entities**       | ✅ Use `partial` classes in separate files     |
| **Database Changes**         | Re-run `Scaffold-DbContext` to regenerate      |
| **Version Control**          | ✅ Commit generated files to Git               |
| **Multiple Databases**       | ✅ Use different output folders (`-OutputDir`) |
| **Use DTOs**                 | ✅ Don't return entities directly to clients   |

---

### ⚠️ Common Issues & Solutions

#### Issue 1: "Conflicting method/path combination" in Swagger

**Problem:** Two methods in the same controller have the same route.

**Solution:**

```csharp
// ❌ BAD
[HttpGet]
public ActionResult Method1() { }

[HttpGet]
public ActionResult Method2() { }

// ✅ GOOD
[HttpGet("log")]
public ActionResult Method1() { }

[HttpGet("customers")]
public ActionResult Method2() { }
```

#### Issue 2: Connection String Issues

**Problem:** "Cannot open database" or "Login failed"

**Solutions:**

- ✅ Verify SQL Server is running
- ✅ Check database name in SSMS
- ✅ Try `Trust Server Certificate=True` for localhost
- ✅ Use Windows Authentication if SQL Auth fails

---

### 🔄 When to Use Database First?

**✅ Use Database First when:**

- Working with **legacy/existing databases**
- Database is designed by **DBAs** (Database Administrators)
- You need to integrate with **external databases**
- Database schema is **already stable**
- Multiple apps share the **same database**

**❌ Use Code First instead when:**

- Starting a **new project** from scratch
- You want **full control** over schema design
- You need **migration** support for schema changes
- You prefer **domain-driven design**

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 20. AutoMapper – Simplifying Object Mapping

### 🤔 What is AutoMapper?

**AutoMapper** is a library that helps you copy data from one object to another automatically. Instead of writing many lines of code to copy each property manually, AutoMapper does it for you in just one line!

---

### ❌ The Problem: Manual Object Mapping

In our `StudentController`, we need to copy data from `Student` (database entity) to `StudentDTO` (what we send to client). Without AutoMapper, we had to write code like this:

```csharp
// ❌ Manual mapping - Too many lines!
var studentDTO = new StudentDTO
{
    Id = student.Id,
    StudentName = student.StudentName,
    Email = student.Email,
    Address = student.Address,
    DOB = student.DOB
};
```

> ⚠️ **Problem:** For every property, we write one line. If you have 20 properties, that's 20 lines! And if you have many controllers, this becomes very messy.

---

### ✅ The Solution: AutoMapper

With AutoMapper, we replace all those lines with just **one line**:

```csharp
// ✅ With AutoMapper - Just one line!
var studentDTO = _mapper.Map<StudentDTO>(student);
```

AutoMapper automatically copies all matching properties from `Student` to `StudentDTO`!

---

### 📦 Installing AutoMapper

Add the AutoMapper NuGet package to your project:

```xml
<PackageReference Include="AutoMapper" Version="16.0.0" />
```

> 💡 **Note:** In AutoMapper 13+, the DI extension is included in the main package. No need to install `AutoMapper.Extensions.Microsoft.DependencyInjection` separately!

---

### ⚙️ Configuring AutoMapper

#### **Step 1: Create a Profile Class**

Create a configuration file that tells AutoMapper which classes to map:

📁 **Configurations/AutoMapperConfig.cs**

```csharp
using ASPNETCoreWebAPI.Data;
using ASPNETCoreWebAPI.Model;
using AutoMapper;

namespace ASPNETCoreWebAPI.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            // Basic mapping: StudentDTO <--> Student
            CreateMap<StudentDTO, Student>().ReverseMap();
        }
    }
}
```

**Key Points:**

- `Profile` – Base class from AutoMapper for configuration
- `CreateMap<Source, Destination>()` – Tells AutoMapper how to map
- `ReverseMap()` – Creates mapping in both directions (Student → StudentDTO AND StudentDTO → Student)

---

#### **Step 2: Register AutoMapper in Program.cs**

```csharp
// AutoMapper 13+ syntax
builder.Services.AddAutoMapper(cfg => { }, typeof(AutoMapperConfig));
```

---

### 🎮 Using AutoMapper in Controller

#### **Step 1: Inject IMapper**

```csharp
public class StudentController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly CollegeDBContext _dbContext;

    public StudentController(CollegeDBContext collegeDBContext, IMapper mapper)
    {
        _dbContext = collegeDBContext;
        _mapper = mapper;
    }
}
```

#### **Step 2: Use \_mapper.Map<T>() for Mapping**

**Single Object Mapping:**

```csharp
// Get student from database
var student = await _dbContext.Students.Where(s => s.Id == id).FirstOrDefaultAsync();

// Map to DTO using AutoMapper
var studentDTO = _mapper.Map<StudentDTO>(student);

return Ok(studentDTO);
```

**List Mapping:**

```csharp
// Get all students from database
var students = await _dbContext.Students.ToListAsync();

// Map entire list to DTOs automatically!
var studentDTOData = _mapper.Map<List<StudentDTO>>(students);

return Ok(studentDTOData);
```

---

### 🔄 Async Methods with Entity Framework Core

When using AutoMapper with EF Core, always use **async methods** for better performance:

| Sync Method        | Async Method            | Purpose           |
| ------------------ | ----------------------- | ----------------- |
| `ToList()`         | `ToListAsync()`         | Get all records   |
| `FirstOrDefault()` | `FirstOrDefaultAsync()` | Get single record |
| `Add()`            | `AddAsync()`            | Insert record     |
| `SaveChanges()`    | `SaveChangesAsync()`    | Commit changes    |

**Example:**

```csharp
[HttpGet]
[Route("All", Name = "GetAllStudents")]
public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsAsync()
{
    // Async database call
    var students = await _dbContext.Students.ToListAsync();

    // AutoMapper copies data
    var studentDTOData = _mapper.Map<List<StudentDTO>>(students);

    return Ok(studentDTOData);
}
```

---

### 🛠️ Advanced AutoMapper Features

#### **1. Mapping Different Property Names**

If your source and destination have different property names, use `ForMember`:

```csharp
// If StudentDTO has 'Name' but Student has 'StudentName'
CreateMap<StudentDTO, Student>()
    .ForMember(dest => dest.StudentName, opt => opt.MapFrom(src => src.Name))
    .ReverseMap()
    .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.StudentName));
```

---

#### **2. Ignoring Properties**

Sometimes you don't want to map certain properties:

```csharp
// Don't copy StudentName when mapping
CreateMap<StudentDTO, Student>()
    .ReverseMap()
    .ForMember(dest => dest.StudentName, opt => opt.Ignore());
```

---

#### **3. Transforming Property Values**

You can transform values during mapping using `ForMember` with `MapFrom`:

```csharp
// If Address is empty, set default value
CreateMap<StudentDTO, Student>()
    .ReverseMap()
    .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
        string.IsNullOrEmpty(src.Address) ? "No address found" : src.Address));
```

---

### 📁 Project Structure with AutoMapper

```
ASPNETCoreWebAPI/
├── Configurations/
│   └── AutoMapperConfig.cs     ◀── AutoMapper profile
├── Controllers/
│   └── StudentController.cs    ◀── Uses IMapper
├── Data/
│   └── Student.cs              ◀── Database entity
├── Model/
│   └── StudentDTO.cs           ◀── Data Transfer Object
└── Program.cs                  ◀── AddAutoMapper registration
```

---

### 📊 Before vs After AutoMapper

| Without AutoMapper            | With AutoMapper              |
| ----------------------------- | ---------------------------- |
| 5-10 lines per mapping        | 1 line per mapping           |
| Error-prone (miss properties) | Automatic & safe             |
| Hard to maintain              | Easy to maintain             |
| Repeated code everywhere      | Configure once, use anywhere |

---

### 🎯 Key Takeaways

1. **Install AutoMapper** – Just the main package (v13+)
2. **Create Profile** – Define mappings in a `Profile` class
3. **Register in DI** – Use `AddAutoMapper()` in Program.cs
4. **Inject IMapper** – Use constructor injection in controllers
5. **Use Map<T>()** – Single line replaces manual copying
6. **Advanced Features:**
   - `ForMember()` – Map different property names
   - `Ignore()` – Skip certain properties
   - `MapFrom()` – Transform values during mapping

> 💡 **Tip:** Always use async EF Core methods (`ToListAsync`, `FirstOrDefaultAsync`) with AutoMapper for better performance!

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 21. Repository Design Pattern

### 🤔 What is Repository Pattern?

**Repository Pattern** is an abstraction of the Data Access Layer. It hides the details of how exactly the data is saved or retrieved from the underlying data source (like a database).

Think of it as a **middleman** between your controller and the database. Instead of your controller directly talking to Entity Framework, it talks to the repository.

---

### ❌ The Problem: Direct Database Access in Controller

In our `StudentController`, we were directly using `DbContext` for database operations:

```csharp
// ❌ Bad Practice: Controller directly using DbContext
public class StudentController : ControllerBase
{
    private readonly CollegeDBContext _dbContext;

    public async Task<ActionResult> GetStudentsAsync()
    {
        var students = await _dbContext.Students.ToListAsync();  // Direct DB access!
        return Ok(students);
    }
}
```

> ⚠️ **Problems with this approach:**
>
> - Controller knows too much about database operations
> - Hard to test (need real database)
> - If database changes, controller code must change
> - Duplicate code across controllers

---

### ✅ The Solution: Repository Pattern

```
┌─────────────────────────────────────────────────────────────────────┐
│                    WITHOUT REPOSITORY PATTERN                     │
│                                                                    │
│   StudentController  ─────▶  Entity Framework  ─────▶  Database   │
│                                                                    │
└─────────────────────────────────────────────────────────────────────┘

┌─────────────────────────────────────────────────────────────────────┐
│                     WITH REPOSITORY PATTERN                        │
│                                                                    │
│   StudentController ─▶ StudentRepository ─▶ Entity Framework ─▶ DB │
│                            │                                       │
│                    (Abstraction Layer)                             │
└─────────────────────────────────────────────────────────────────────┘
```

---

### 📊 Repository Pattern Architecture

```
┌─────────────────────┐      ┌────────────────────┐      ┌────────────┐
│    WEB API App      │      │     Repository     │      │  Database  │
│    (Consumers)      │      │      Layer         │      │            │
├─────────────────────┤      ├────────────────────┤      ├────────────┤
│ StudentController   │ ───▶ │ StudentRepository  │ ───▶ │  Student   │
├─────────────────────┤      ├────────────────────┤      ├────────────┤
│ CourseController    │ ───▶ │ CourseRepository   │ ───▶ │  Course    │
└─────────────────────┘      └────────────────────┘      └────────────┘
```

> 💡 **Note:** If we have multiple tables, we create multiple repositories. But this can become too much! That's why we can create a **Generic Repository** for all tables (covered in advanced topics).

---

### 🛠️ Implementation Steps

#### **Step 1: Create the Interface (Contract)**

The interface defines WHAT operations the repository can do:

📁 **Data/Repository/IStudentRepository.cs**

```csharp
namespace ASPNETCoreWebAPI.Data.Repository
{
    public interface IStudentRepository
    {
        Task<List<Student>> GetAllAsync();
        Task<Student> GetByIdAsync(int id, bool useNoTracking = false);
        Task<Student> GetByNameAsync(string name);
        Task<int> CreateAsync(Student student);
        Task<int> UpdateAsync(Student student);
        Task<bool> DeleteAsync(Student student);
    }
}
```

**Key Points:**

- Interface defines the contract (what methods are available)
- All methods are async for better performance
- `useNoTracking` parameter helps with update operations

---

#### **Step 2: Implement the Repository**

The concrete class implements HOW the operations work:

📁 **Data/Repository/StudentRepository.cs**

```csharp
namespace ASPNETCoreWebAPI.Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;

        public StudentRepository(CollegeDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _dbContext.Students.AsNoTracking()
                    .Where(s => s.Id == id).FirstOrDefaultAsync();
            else
                return await _dbContext.Students
                    .Where(s => s.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Student> GetByNameAsync(string name)
        {
            return await _dbContext.Students
                .Where(s => s.StudentName.ToLower().Contains(name.ToLower()))
                .FirstOrDefaultAsync();
        }

        public async Task<int> CreateAsync(Student student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return student.Id;
        }

        public async Task<int> UpdateAsync(Student student)
        {
            _dbContext.Update(student);
            await _dbContext.SaveChangesAsync();
            return student.Id;
        }

        public async Task<bool> DeleteAsync(Student student)
        {
            _dbContext.Students.Remove(student);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
```

---

#### **Step 3: Register in Dependency Injection**

📁 **Program.cs**

```csharp
// Register repository
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
```

---

#### **Step 4: Use Repository in Controller**

📁 **Controllers/StudentController.cs**

```csharp
public class StudentController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;
    private readonly IMapper _mapper;

    public StudentController(IMapper mapper, IStudentRepository studentRepository)
    {
        _mapper = mapper;
        _studentRepository = studentRepository;
    }

    [HttpGet]
    [Route("All")]
    public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsAsync()
    {
        // Using repository instead of direct DbContext
        var students = await _studentRepository.GetAllAsync();
        var studentDTOData = _mapper.Map<List<StudentDTO>>(students);
        return Ok(studentDTOData);
    }

    [HttpPost]
    [Route("Create")]
    public async Task<ActionResult<StudentDTO>> CreateStudentAsync([FromBody] StudentDTO dto)
    {
        if (dto == null)
            return BadRequest();

        Student student = _mapper.Map<Student>(dto);
        var id = await _studentRepository.CreateAsync(student);
        dto.Id = id;

        return CreatedAtRoute("GetStudentById", new { id = dto.Id }, dto);
    }
}
```

---

### 📁 Project Structure with Repository Pattern

```
ASPNETCoreWebAPI/
├── Controllers/
│   └── StudentController.cs    ◀── Uses IStudentRepository
├── Data/
│   ├── CollegeDBContext.cs     ◀── Database context
│   ├── Student.cs              ◀── Entity model
│   └── Repository/
│       ├── IStudentRepository.cs   ◀── Interface (Contract)
│       └── StudentRepository.cs    ◀── Implementation
└── Program.cs                  ◀── DI Registration
```

---

### 📊 Before vs After Repository Pattern

| Without Repository                | With Repository                    |
| --------------------------------- | ---------------------------------- |
| Controller knows database details | Controller only knows interface    |
| Hard to unit test                 | Easy to mock and test              |
| Tight coupling                    | Loose coupling                     |
| Duplicate DB code                 | Reusable repository methods        |
| Change DB = Change controller     | Change DB = Only change repository |

---

### 🎯 Key Takeaways

1. **Repository = Abstraction Layer** – Hides database details from controllers
2. **Interface First** – Create `IStudentRepository` before implementation
3. **Dependency Injection** – Register `<interface, implementation>` in Program.cs
4. **Controller Uses Interface** – Inject `IStudentRepository`, not `StudentRepository`
5. **All CRUD in Repository** – GetAll, GetById, Create, Update, Delete
6. **Async Everything** – Use async methods for better performance

> 💡 **Tip:** For multiple tables, consider creating a **Generic Repository** to avoid duplicate code!

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 22. Generic Repository Pattern (Advanced)

### 🤔 What is Generic Repository Pattern?

**Generic Repository Pattern** (also called **Common Repository Pattern**) is an advanced version of the Repository Pattern that uses **C# Generics** to create a single repository that works for **ALL** database tables.

Instead of creating separate repositories for each table (StudentRepository, CourseRepository, DepartmentRepository, etc.), we create ONE generic repository that can handle any table!

---

### ❌ The Problem: Too Many Repositories

In our previous implementation, we created `StudentRepository` for the `Student` table:

```csharp
public class StudentRepository : IStudentRepository
{
    // CRUD methods for Student table
}
```

Now imagine we have more tables:

```
┌─────────────────────────────────────────────────────────────────┐
│                    DATABASE TABLES                              │
├─────────────────────────────────────────────────────────────────┤
│  Student Table  →  Need StudentRepository                       │
│  Course Table   →  Need CourseRepository                        │
│  Department Table → Need DepartmentRepository                   │
│  Teacher Table  →  Need TeacherRepository                       │
│  Fee Table      →  Need FeeRepository                           │
└─────────────────────────────────────────────────────────────────┘
```

> ⚠️ **Problems:**
>
> - Too many repository classes (one for each table)
> - Duplicate code everywhere (GetAll, GetById, Create, Update, Delete are same for all)
> - Hard to maintain (change in one means change in all)
> - More code = More bugs

---

### ✅ The Solution: Generic Repository

Create ONE repository that works for ALL tables using **C# Generics**:

```
┌─────────────────────────────────────────────────────────────────┐
│              GENERIC REPOSITORY (ONE FOR ALL)                   │
│                                                                 │
│  ICollegeRepository<T>  ──────┐                                │
│  CollegeRepository<T>         │                                │
│                               │                                │
│                               ├──▶ Works with Student          │
│                               ├──▶ Works with Course           │
│                               ├──▶ Works with Department       │
│                               ├──▶ Works with ANY table!       │
│                               └──▶ ...                         │
└─────────────────────────────────────────────────────────────────┘
```

---

### 🛠️ Implementation Steps

#### **Step 1: Create Generic Interface**

The generic interface uses `<T>` to work with any type:

📁 **Data/Repository/ICollegeRepository.cs**

```csharp
using System.Linq.Expressions;

namespace ASPNETCoreWebAPI.Data.Repository
{
    public interface ICollegeRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false);
        Task<T> CreateAsync(T dbrecord);
        Task<T> UpdateAsync(T dbrecord);
        Task<bool> DeleteAsync(T dbrecord);
    }
}
```

**Key Points:**

- `<T>` is a generic type parameter (can be Student, Course, any entity)
- `Expression<Func<T, bool>>` allows flexible filtering (like LINQ where clauses)
- Works for ANY database entity that is a class

---

#### **Step 2: Implement Generic Repository**

📁 **Data/Repository/CollegeRepository.cs**

```csharp
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ASPNETCoreWebAPI.Data.Repository
{
    public class CollegeRepository<T> : ICollegeRepository<T> where T : class
    {
        private readonly CollegeDBContext _dbContext;
        private DbSet<T> _dbset;

        public CollegeRepository(CollegeDBContext dbContext)
        {
            _dbContext = dbContext;
            _dbset = _dbContext.Set<T>();  // Get DbSet for any table
        }

        public async Task<List<T>> GetAllAsync()
        {
            return await _dbset.ToListAsync();
        }

        public async Task<T> GetAsync(Expression<Func<T, bool>> filter, bool useNoTracking = false)
        {
            if (useNoTracking)
                return await _dbset.AsNoTracking().Where(filter).FirstOrDefaultAsync();
            else
                return await _dbset.Where(filter).FirstOrDefaultAsync();
        }

        public async Task<T> CreateAsync(T dbrecord)
        {
            _dbset.Add(dbrecord);
            await _dbContext.SaveChangesAsync();
            return dbrecord;
        }

        public async Task<T> UpdateAsync(T dbrecord)
        {
            _dbContext.Update(dbrecord);
            await _dbContext.SaveChangesAsync();
            return dbrecord;
        }

        public async Task<bool> DeleteAsync(T dbrecord)
        {
            _dbset.Remove(dbrecord);
            await _dbContext.SaveChangesAsync();
            return true;
        }
    }
}
```

**Key Points:**

- `where T : class` ensures T must be a class (entity type)
- `_dbContext.Set<T>()` dynamically gets the DbSet for any entity
- Same code works for Student, Course, Department, etc.!

---

#### **Step 3: Register Generic Repository in DI**

📁 **Program.cs**

```csharp
// Register generic repository (works for ALL tables)
builder.Services.AddScoped(typeof(ICollegeRepository<>), typeof(CollegeRepository<>));
```

**Important:** Notice the `typeof(ICollegeRepository<>)` syntax – this registers the open generic type!

---

### 🔗 Inheriting Generic Repository in Table-Specific Repository

Sometimes you need **table-specific methods** along with common CRUD. For example, `GetStudentsByFeesStatus()` only makes sense for Student table.

**Solution:** Inherit from the generic repository and add custom methods:

#### **Updated IStudentRepository**

📁 **Data/Repository/IStudentRepository.cs**

```csharp
namespace ASPNETCoreWebAPI.Data.Repository
{
    // Inherit from generic interface + add custom methods
    public interface IStudentRepository : ICollegeRepository<Student>
    {
        Task<List<Student>> GetStudentsByFeesStatusAsync(int feesStatus);
    }
}
```

---

#### **Updated StudentRepository**

📁 **Data/Repository/StudentRepository.cs**

```csharp
namespace ASPNETCoreWebAPI.Data.Repository
{
    // Inherit from generic repository + implement custom methods
    public class StudentRepository : CollegeRepository<Student>, IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;

        public StudentRepository(CollegeDBContext dbContext) : base(dbContext)
        {
            _dbContext = dbContext;
        }

        // Custom method specific to Student
        public async Task<List<Student>> GetStudentsByFeesStatusAsync(int feesStatus)
        {
            return await _dbContext.Students
                .Where(s => s.FeesStatus == feesStatus)
                .ToListAsync();
        }
    }
}
```

**Key Points:**

- `StudentRepository` inherits from `CollegeRepository<Student>`
- Gets ALL common methods (GetAll, Create, Update, Delete) for FREE
- Can add table-specific methods like `GetStudentsByFeesStatusAsync`

---

### 📁 Complete Architecture

```
                    ┌─────────────────────────────────┐
                    │   ICollegeRepository<T>         │
                    │   (Generic Interface)           │
                    └─────────────┬───────────────────┘
                                  │
                   ┌──────────────┴──────────────┐
                   │                             │
          ┌────────▼────────┐         ┌─────────▼──────────┐
          │ IStudentRepo    │         │ ICourseRepo        │
          │ : ICollegeRepo  │         │ : ICollegeRepo     │
          │   <Student>     │         │   <Course>         │
          └────────┬────────┘         └─────────┬──────────┘
                   │                            │

          ┌────────▼────────────┐      ┌────────▼───────────┐
          │ StudentRepository   │      │ CourseRepository   │
          │ : CollegeRepository │      │ : CollegeRepository│
          │   <Student>         │      │   <Course>         │
          │                     │      │                    │
          │ + Custom Methods    │      │ + Custom Methods   │
          └─────────────────────┘      └────────────────────┘
```

---

### 🎮 Using Generic Repository in Controller

You can use the generic repository directly OR use the table-specific one:

#### **Option 1: Use Generic Repository Directly**

```csharp
public class CourseController : ControllerBase
{
    private readonly ICollegeRepository<Course> _courseRepository;

    public CourseController(ICollegeRepository<Course> courseRepository)
    {
        _courseRepository = courseRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllCourses()
    {
        var courses = await _courseRepository.GetAllAsync();
        return Ok(courses);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetCourseById(int id)
    {
        var course = await _courseRepository.GetAsync(c => c.Id == id);
        return Ok(course);
    }
}
```

---

#### **Option 2: Use Table-Specific Repository**

```csharp
public class StudentController : ControllerBase
{
    private readonly IStudentRepository _studentRepository;

    public StudentController(IStudentRepository studentRepository)
    {
        _studentRepository = studentRepository;
    }

    [HttpGet]
    public async Task<ActionResult> GetAllStudents()
    {
        // Using inherited generic method
        var students = await _studentRepository.GetAllAsync();
        return Ok(students);
    }

    [HttpGet("fees/{status}")]
    public async Task<ActionResult> GetStudentsByFees(int status)
    {
        // Using custom table-specific method
        var students = await _studentRepository.GetStudentsByFeesStatusAsync(status);
        return Ok(students);
    }
}
```

---

### 📊 Before vs After Generic Repository

| Table-Specific Repositories | Generic Repository            |
| --------------------------- | ----------------------------- |
| One repository per table    | ONE repository for all tables |
| 100+ lines per repository   | ~60 lines TOTAL               |
| Duplicate CRUD code         | Code reuse with generics      |
| Hard to maintain            | Easy to maintain              |
| 10 tables = 10 repositories | 10 tables = 1 repository      |
| Add table = Create new repo | Add table = Use existing repo |

---

### 💡 Expression Trees for Flexible Filtering

Notice the `GetAsync()` method uses `Expression<Func<T, bool>>`. This allows flexible filtering:

```csharp
// Get student by ID
var student = await _repository.GetAsync(s => s.Id == 5);

// Get student by email
var student = await _repository.GetAsync(s => s.Email == "test@email.com");

// Get student by name
var student = await _repository.GetAsync(s => s.StudentName == "Kartik");

// Complex filter
var student = await _repository.GetAsync(s => s.Id > 10 && s.FeesStatus == 1);
```

The filter is a **lambda expression** that works like a LINQ `Where` clause!

---

### 🎯 Key Takeaways

1. **Generic Repository** – ONE repository for ALL tables using `<T>`
2. **Code Reuse** – Eliminate duplicate CRUD code across repositories
3. **`where T : class`** – Constraint ensures T is an entity type
4. **`_dbContext.Set<T>()`** – Dynamically get DbSet for any table
5. **Inheritance** – Table-specific repos can inherit from generic + add custom methods
6. **Expression Trees** – Flexible filtering with `Expression<Func<T, bool>>`
7. **DI Registration** – Use `typeof(ICollegeRepository<>)` for open generics

> 💡 **Best Practice:** Use generic repository for common CRUD, inherit for table-specific logic!

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 23. Security in Web API

### 🔐 Why Security Matters

Till now, we've created Web API services and consumed them using **Postman** and **Swagger**. In real-world scenarios, APIs are consumed by applications.

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│                         WHO CONSUMES YOUR API?                                   │
├─────────────────────────────────────────────────────────────────────────────────┤
│                                                                                  │
│   ✅ Internal Applications        ✅ External Applications       ❌ Unwanted    │
│   ┌─────────────────────┐        ┌─────────────────────┐        ┌─────────────┐ │
│   │   Your Company's    │        │   Partner Apps      │        │   Hackers   │ │
│   │   Mobile App        │        │   Third-Party       │        │   Bots      │ │
│   │   Web Dashboard     │        │   Integrations      │        │   Scrapers  │ │
│   └─────────────────────┘        └─────────────────────┘        └─────────────┘ │
│                                                                                  │
│                              ❓ How to block unwanted access?                    │
│                              ❓ How to allow only authorized apps?               │
└─────────────────────────────────────────────────────────────────────────────────┘
```

---

### 🛡️ Stages of Web API Security

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│                           SECURITY LAYERS IN WEB API                             │
├─────────────────────────────────────────────────────────────────────────────────┤
│                                                                                  │
│   Stage 1: CORS              Stage 2: Authentication      Stage 3: Authorization│
│   ┌───────────────────┐      ┌───────────────────┐        ┌───────────────────┐ │
│   │ Which ORIGINS     │ ───▶ │ WHO are you?      │ ───▶   │ WHAT can you do?  │ │
│   │ can access?       │      │ (Identity)        │        │ (Permissions)     │ │
│   │                   │      │                   │        │                   │ │
│   │ Allow specific    │      │ JWT Token         │        │ Roles: Admin,     │ │
│   │ domains only      │      │ API Key           │        │ User, Guest       │ │
│   └───────────────────┘      └───────────────────┘        └───────────────────┘ │
│                                                                                  │
└─────────────────────────────────────────────────────────────────────────────────┘
```

| Stage                 | Purpose                                            | Example                         |
| --------------------- | -------------------------------------------------- | ------------------------------- |
| **1. CORS**           | Decide which origins (domains) can access your API | Allow only `collegeapp.com`     |
| **2. Authentication** | Verify identity of the user/application            | JWT Token, API Key              |
| **3. Authorization**  | Verify what actions the user can perform           | Admin can delete, User can read |

> ⚠️ **Important:** These stages work together. CORS alone is NOT enough for complete security!

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 24. CORS – Cross-Origin Resource Sharing

### 🤔 What is CORS?

**CORS (Cross-Origin Resource Sharing)** is a browser security feature that controls which websites can access your API.

> ⚠️ **Important:** CORS is **NOT a security feature** – it actually **relaxes security**! It allows a server to explicitly permit some cross-origin requests while rejecting others. An API is not safer by allowing CORS.

---

### 🌐 Understanding "Origin"

An **origin** is a combination of:

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│                              ORIGIN = SCHEMA + DOMAIN + PORT                     │
├─────────────────────────────────────────────────────────────────────────────────┤
│                                                                                  │
│   https://collegeapp.com:443/api/getallstudents                                  │
│   ──────  ───────────────  ───                                                   │
│   Schema     Domain        Port                                                  │
│                                                                                  │
│   Origin = https://collegeapp.com:443                                            │
│                                                                                  │
└─────────────────────────────────────────────────────────────────────────────────┘
```

---

### ✅ Same Origin Examples

These URLs have the **SAME origin** (same schema, domain, port):

| URL 1                                       | URL 2                                    | Same Origin? |
| ------------------------------------------- | ---------------------------------------- | ------------ |
| `https://collegeapp.com/api/getallstudents` | `https://collegeapp.com/angularhomepage` | ✅ Yes       |
| `https://collegeapp.com/api/students`       | `https://collegeapp.com/api/teachers`    | ✅ Yes       |

---

### ❌ Different Origin Examples

These URLs have **DIFFERENT origins**:

| URL 1                                 | URL 2                                      | Why Different?                   |
| ------------------------------------- | ------------------------------------------ | -------------------------------- |
| `https://collegeapp.com/api/students` | `https://collegeapp.net/api/students`      | Different domain extension       |
| `https://collegeapp.com/api/students` | `http://collegeapp.com/api/students`       | Different schema (https vs http) |
| `https://collegeapp.com/api/students` | `https://collegeapp.com:9000/api/students` | Different port                   |

---

### 🔄 How CORS Works

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│                              CORS REQUEST FLOW                                   │
├─────────────────────────────────────────────────────────────────────────────────┤
│                                                                                  │
│   WEB API SERVER                              CLIENT REQUESTS                    │
│   https://collegeapp.com/api/getstudents                                         │
│                                                                                  │
│        │                                                                         │
│        ├───────────▶ https://google.com          ❌ REJECT (different origin)   │
│        │                                                                         │
│        ├───────────▶ https://collegeapp.com      ✅ ACCEPT (same origin)        │
│        │                                                                         │
│        ├───────────▶ https://microsoft.com       ❌ REJECT (different origin)   │
│        │                                                                         │
│        └───────────▶ http://localhost:5173       ✅ ACCEPT (if CORS policy set) │
│                                                                                  │
└─────────────────────────────────────────────────────────────────────────────────┘
```

> 💡 **Key Insight:** By default, browsers block cross-origin requests. CORS policies tell the browser which origins to allow.

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 25. CORS Scenarios

There are three main CORS scenarios:

### 1️⃣ Simple Request

**Simple requests** are basic requests that don't trigger a preflight check.

**Requirements for Simple Request:**

- Methods: `GET`, `HEAD`, `POST` only
- Headers: Only basic headers like `Accept`, `Content-Type` (with limited values)
- Content-Type: Only `application/x-www-form-urlencoded`, `multipart/form-data`, or `text/plain`

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│                              SIMPLE REQUEST FLOW                                 │
├─────────────────────────────────────────────────────────────────────────────────┤
│                                                                                  │
│   CLIENT                                                   SERVER                │
│   (Browser)                                                (Web API)             │
│                                                                                  │
│      │                                                         │                 │
│      │  ─────────────────────────────────────────────────▶     │                 │
│      │  GET /api/getallstudents HTTP/1.1                       │                 │
│      │  Origin: https://collegeapp.com                         │                 │
│      │                                                         │                 │
│      │     ◀─────────────────────────────────────────────────  │                 │
│      │     HTTP/1.1 200 OK                                     │                 │
│      │     Access-Control-Allow-Origin: *                      │                 │
│      │     (or specific origin)                                │                 │
│      │                                                         │                 │
└─────────────────────────────────────────────────────────────────────────────────┘
```

> 💡 The server responds with `Access-Control-Allow-Origin` header. Use `*` for all origins or specify the exact origin like `https://collegeapp.com`.

---

### 2️⃣ Preflight Request

**Preflight requests** are sent by the browser before the actual request for "non-simple" requests.

**When Preflight is Required:**

- Methods: `PUT`, `DELETE`, `PATCH`, or custom methods
- Custom headers: `Authorization`, `X-Custom-Header`, etc.
- Content-Type: `application/json`

```
┌─────────────────────────────────────────────────────────────────────────────────┐
│                            PREFLIGHT REQUEST FLOW                                │
├─────────────────────────────────────────────────────────────────────────────────┤
│                                                                                  │
│   CLIENT                                                   SERVER                │
│   (Browser)                                                (Web API)             │
│                                                                                  │
│      │  STEP 1: Preflight (OPTIONS request)                    │                 │
│      │  ─────────────────────────────────────────────────▶     │                 │
│      │  OPTIONS /api/getallstudents HTTP/1.1                   │                 │
│      │  Origin: https://collegeapp.com                         │                 │
│      │  Access-Control-Request-Method: DELETE                  │                 │
│      │                                                         │                 │
│      │     ◀─────────────────────────────────────────────────  │                 │
│      │     HTTP/1.1 204 No Content                             │                 │
│      │     Access-Control-Allow-Origin: https://collegeapp.com │                 │
│      │     Access-Control-Allow-Methods: DELETE                │                 │
│      │                                                         │                 │
│      │  STEP 2: Actual Request (after preflight success)       │                 │
│      │  ─────────────────────────────────────────────────▶     │                 │
│      │  DELETE /api/student/1 HTTP/1.1                         │                 │
│      │  Origin: https://collegeapp.com                         │                 │
│      │                                                         │                 │
│      │     ◀─────────────────────────────────────────────────  │                 │
│      │     HTTP/1.1 200 OK                                     │                 │
│      │     Access-Control-Allow-Origin: https://collegeapp.com │                 │
│      │                                                         │                 │
└─────────────────────────────────────────────────────────────────────────────────┘
```

> 💡 The browser first sends an `OPTIONS` request to check if the server allows the actual request. If successful (204), then the actual request is sent.

---

### 3️⃣ Request with Credentials

When requests include cookies or authentication headers, special configuration is needed.

| Scenario          | CORS Configuration                                                            |
| ----------------- | ----------------------------------------------------------------------------- |
| Simple Request    | `Access-Control-Allow-Origin: *` or specific origin                           |
| Preflight Request | Same + `Access-Control-Allow-Methods`                                         |
| With Credentials  | Must use specific origin (not `*`) + `Access-Control-Allow-Credentials: true` |

---

### 📊 CORS Scenarios Summary

| Scenario          | HTTP Method              | Custom Headers? | Preflight? |
| ----------------- | ------------------------ | --------------- | ---------- |
| Simple Request    | GET, HEAD, POST          | No              | ❌ No      |
| Preflight Request | PUT, DELETE, PATCH, etc. | Yes             | ✅ Yes     |
| With Credentials  | Any                      | Any             | Depends    |

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 26. Enabling CORS in Web API

There are **three ways** to enable CORS in ASP.NET Core Web API:

1. Using middleware with Named or Default policy
2. Using endpoint routing
3. Using the `[EnableCors]` attribute

---

### 1️⃣ Using Middleware (Named & Default Policies)

**Step 1: Define CORS Policies in `Program.cs`**

```csharp
// Program.cs

// Add CORS services
builder.Services.AddCors(options =>
{
    // Named Policy: AllowAll - permits any origin
    options.AddPolicy("AllowAll", policy =>
    {
        // For all origins
        policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });

    // Named Policy: AllowOnlyLocalhost - permits only localhost
    options.AddPolicy("AllowOnlyLocalhost", policy =>
    {
        // For specific origin
        policy.WithOrigins("http://localhost:5173").AllowAnyHeader().AllowAnyMethod();
    });

    // Named Policy: AllowOnlyGoogle - permits Google domains
    options.AddPolicy("AllowOnlyGoogle", policy =>
    {
        // For specific origins
        policy.WithOrigins("http://google.com", "http://gmail.com", "http://drive.google.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

    // Named Policy: AllowOnlyMicrosoft - permits Microsoft domains
    options.AddPolicy("AllowOnlyMicrosoft", policy =>
    {
        // For specific origins
        policy.WithOrigins("http://outlook.com", "http://microsoft.com", "http://onedrive.com")
              .AllowAnyHeader()
              .AllowAnyMethod();
    });

    // Default Policy (uncomment to use)
    // options.AddDefaultPolicy(policy =>
    // {
    //     policy.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    // });
});
```

**Step 2: Apply CORS Middleware**

```csharp
// Program.cs - Configure middleware pipeline

var app = builder.Build();

app.UseHttpsRedirection();
app.UseRouting();

// Apply CORS middleware with named policy
app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
```

> ⚠️ **Order Matters:** `UseCors()` must be called after `UseRouting()` and before `UseAuthorization()`.

---

### 📌 Named vs Default Policy

| Feature             | Named Policy                             | Default Policy                    |
| ------------------- | ---------------------------------------- | --------------------------------- |
| Definition          | `AddPolicy("PolicyName", policy => ...)` | `AddDefaultPolicy(policy => ...)` |
| Usage in Middleware | `app.UseCors("PolicyName")`              | `app.UseCors()`                   |
| Attribute Usage     | `[EnableCors("PolicyName")]`             | `[EnableCors]`                    |
| Flexibility         | Multiple policies for different needs    | Single default for all            |
| Best For            | Different origins for different APIs     | Same origin rules for entire app  |

---

### 2️⃣ Using Endpoint Routing

You can apply different CORS policies to specific endpoints:

```csharp
// Program.cs

app.UseRouting();
app.UseCors("AllowAll");
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    // Specific endpoint with specific CORS policy
    endpoints.MapGet("api/testingendpoint",
        context => context.Response.WriteAsync("Test Response"))
        .RequireCors("AllowOnlyLocalhost");  // Only localhost can access

    // All controllers with AllowAll policy
    endpoints.MapControllers().RequireCors("AllowAll");

    // Another endpoint (inherits from middleware CORS)
    endpoints.MapGet("api/testingendpoint1",
        context => context.Response.WriteAsync("Test Response 1"));
});
```

> 💡 Use `RequireCors()` on endpoints to override the default middleware policy.

---

### 3️⃣ Using the `[EnableCors]` Attribute

Apply CORS policies at the controller or action level:

**Controller Level:**

```csharp
// StudentController.cs
using Microsoft.AspNetCore.Cors;

[Route("api/[controller]")]
[ApiController]
// With the [EnableCors] attribute.
[EnableCors(PolicyName = "AllowOnlyLocalhost")]
public class StudentController : ControllerBase
{
    // All actions in this controller will use "AllowOnlyLocalhost" policy
}
```

```csharp
// DemoController.cs
using Microsoft.AspNetCore.Cors;

[Route("api/[controller]")]
[ApiController]
// With the [EnableCors] attribute.
[EnableCors(PolicyName = "AllowOnlyGoogle")]
public class DemoController : ControllerBase
{
    // All actions use "AllowOnlyGoogle" policy
}
```

```csharp
// MicrosoftController.cs
using Microsoft.AspNetCore.Cors;

[Route("api/[controller]")]
[ApiController]
// With the [EnableCors] attribute.
[EnableCors(PolicyName = "AllowOnlyMicrosoft")]
public class MicrosoftController : ControllerBase
{
    // All actions use "AllowOnlyMicrosoft" policy
}
```

---

### 🚫 Using `[DisableCors]` Attribute

You can disable CORS for specific actions:

```csharp
// DemoController.cs
[Route("api/[controller]")]
[ApiController]
[EnableCors(PolicyName = "AllowOnlyGoogle")]
public class DemoController : ControllerBase
{
    [HttpGet("customers", Name = "GetCustomerData")]
    // With the [DisableCors] attribute - blocks all cross-origin requests
    [DisableCors]
    public IEnumerable<dynamic> Get()
    {
        return _dbContext.Customers.ToList();
    }
}
```

> 💡 `[DisableCors]` is useful when you want to block cross-origin access for sensitive endpoints while allowing it for others in the same controller.

---

### 🧪 Testing CORS with UI

This project includes a test UI in the `student-ui` folder (React + Vite) to verify CORS behavior.

**Frontend API Call:**

```javascript
// student-ui/src/api/studentApi.js
import axios from "axios";

const BASE_URL = "https://localhost:7234/api/testingendpoint";

export const getAllStudents = async () => {
  const response = await axios.get(`${BASE_URL}`, {
    headers: {
      "Content-Type": "application/json",
      Accept: "application/json",
    },
  });
  return response.data;
};
```

**How to Test:**

1. Run the Web API (`https://localhost:7234`)
2. Run the UI (`http://localhost:5173`)
3. Open browser DevTools → Network tab
4. Click "Get Students" button
5. Check request/response headers:
   - `Origin: http://localhost:5173`
   - `Access-Control-Allow-Origin: http://localhost:5173` (or `*`)

---

### 📊 CORS Methods Comparison

| Method               | Scope                      | Flexibility | Use Case                                |
| -------------------- | -------------------------- | ----------- | --------------------------------------- |
| **Middleware**       | Entire application         | Low         | Same policy for all endpoints           |
| **Endpoint Routing** | Specific endpoints         | Medium      | Different policies for different routes |
| **`[EnableCors]`**   | Controller or Action level | High        | Fine-grained control per controller     |
| **`[DisableCors]`**  | Action level               | High        | Block specific sensitive endpoints      |

---

### 🎯 Key Takeaways

1. **CORS is NOT security** – It relaxes browser restrictions, not adds protection
2. **Origin = Schema + Domain + Port** – All three must match for same-origin
3. **Simple vs Preflight** – PUT/DELETE/custom headers trigger preflight OPTIONS request
4. **Three ways to enable** – Middleware, Endpoint Routing, `[EnableCors]` attribute
5. **Named policies** – Define multiple policies for different origins/controllers
6. **`[DisableCors]`** – Block cross-origin access for sensitive endpoints
7. **Order matters** – `UseCors()` must come after `UseRouting()` and before `UseAuthorization()`

> 💡 **Best Practice:** Use CORS as the first layer, but always implement proper Authentication and Authorization for complete security!

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 27. JWT – JSON Web Tokens

### 🔐 What is Authentication and Authorization?

Before understanding JWT, let's understand two important security concepts:

**Authentication** – The process of verifying the identity of a user or system. It answers: **"Who are you?"**

**Authorization** – Defines what actions a user or system is allowed to perform. It answers: **"What can you do?"**

---

### 🏢 Real-World Example: College Web API

Imagine a College Web API with multiple modules:

```
┌────────────────────────────────────────────────────────────┐
│                     COLLEGE WEB API                         │
├────────────────────────────────────────────────────────────┤
│                                                             │
│   ┌─────────────┐   ┌─────────────┐   ┌─────────────┐      │
│   │   Student   │   │    Fees     │   │ Examination │      │
│   │   Module    │   │   Module    │   │   Module    │      │
│   └─────────────┘   └─────────────┘   └─────────────┘      │
│                                                             │
│   ┌─────────────┐                                          │
│   │ Attendance  │                                          │
│   │   Module    │                                          │
│   └─────────────┘                                          │
│                                                             │
└────────────────────────────────────────────────────────────┘
                           │
                           ▼
┌────────────────────────────────────────────────────────────┐
│                      USER ACCESS                            │
│                                                             │
│  Step 1: Authentication (Prove your identity)              │
│          ─────────────────────────────────────             │
│          User provides: Username + Password                 │
│          System verifies: "Yes, you are John"              │
│                                                             │
│  Step 2: Authorization (Check your permissions)            │
│          ─────────────────────────────────────             │
│          Based on Role: Admin, Student, Teacher            │
│          System allows: Access to specific modules          │
│                                                             │
└────────────────────────────────────────────────────────────┘
```

**How it works:**

1. User wants to access the API
2. First, user proves identity (Authentication using **username + password**)
3. Then, system checks what user can do (Authorization using **role**)
4. User gets access to allowed modules only

---

### 🎫 What is JWT?

**JWT (JSON Web Token)** is a popular mechanism for securing Web APIs by encoding information in a token that can be easily validated.

> JWT is an open, industry-standard **RFC 7519** method for representing claims securely between two parties.

**Key Points:**

- JWT contains **base64 encoded data** passed to clients
- It is **self-contained** – all user info is inside the token
- It can be **validated** without database calls
- It is **stateless** – server doesn't store session

---

### 🧩 JWT Structure – Three Parts

Every JWT token has three parts separated by dots (`.`):

```
xxxxx.yyyyy.zzzzz
  │      │      │
  │      │      └── Signature (Blue)
  │      └── Payload (Green)
  └── Header (Red)
```

---

#### 1️⃣ JWT Header

The header contains information about the token itself:

```json
{
  "alg": "HS256",
  "typ": "JWT"
}
```

| Field | Description                                    |
| ----- | ---------------------------------------------- |
| `alg` | Algorithm used to sign the token (e.g., HS256) |
| `typ` | Type of token (always "JWT")                   |

> 💡 The header is also known as the **JOSE header** (JSON Object Signing and Encryption).

---

#### 2️⃣ JWT Payload

The payload contains the user data and claims:

```json
{
  "id": "1234567",
  "name": "John Doe",
  "role": "admin"
}
```

**Common Claims:**
| Claim | Full Name | Description |
|-------|-----------|-------------|
| `sub` | Subject | Unique identifier for the user |
| `name` | Name | User's display name |
| `role` | Role | User's permission level |
| `iat` | Issued At | When token was created |
| `exp` | Expiration | When token expires |

> 💡 No claims are mandatory, but specific claims have definite meanings.

---

#### 3️⃣ JWT Signature

The signature ensures the token hasn't been tampered with:

```
HMACSHA256(
  base64UrlEncode(header) + "." + base64UrlEncode(payload),
  <YourSecretKey>
)
```

**Purpose of Signature:**

- Allows parties to verify the **authenticity** of the JWT
- Ensures data hasn't been **tampered with**
- Created using the header, payload, and a **secret key**

---

### 🔄 How JWT Token is Generated

```
┌──────────────────────────────────────────────────────────────────┐
│                    JWT TOKEN GENERATION PROCESS                   │
├──────────────────────────────────────────────────────────────────┤
│                                                                   │
│  ┌─────────────┐              ┌─────────────────────────┐        │
│  │   Header    │   ────────▶  │   base64UrlEncode()     │        │
│  │  (JSON)     │              │   → base64Header        │        │
│  └─────────────┘              └─────────────────────────┘        │
│                                          │                        │
│  ┌─────────────┐              ┌─────────────────────────┐        │
│  │   Payload   │   ────────▶  │   base64UrlEncode()     │        │
│  │  (JSON)     │              │   → base64Payload       │        │
│  └─────────────┘              └─────────────────────────┘        │
│                                          │                        │
│                                          ▼                        │
│              ┌───────────────────────────────────────┐           │
│              │  base64Header + "." + base64Payload   │           │
│              └───────────────────────────────────────┘           │
│                                          │                        │
│                                          ▼                        │
│              ┌───────────────────────────────────────┐           │
│              │  Sign with Algorithm + Secret Key     │           │
│              │  (e.g., HMACSHA256)                   │           │
│              └───────────────────────────────────────┘           │
│                                          │                        │
│                                          ▼                        │
│              ┌───────────────────────────────────────┐           │
│              │  base64UrlEncode(signature)           │           │
│              │  → base64Signature                    │           │
│              └───────────────────────────────────────┘           │
│                                          │                        │
│                                          ▼                        │
│  ┌───────────────────────────────────────────────────────────┐   │
│  │                      FINAL JWT TOKEN                       │   │
│  │  base64Header.base64Payload.base64Signature               │   │
│  └───────────────────────────────────────────────────────────┘   │
│                                                                   │
└──────────────────────────────────────────────────────────────────┘
```

---

### 📝 Example JWT Token

```
eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJzdWIiOiIxMjM0NTY3ODkwIiwibmFtZSI6IkpvaG4gRG9lIiwiaWF0IjoxNTE2MjM5MDIyfQ.SflKxwRJSMeKKF2QT4fwpMeJf36POk6yJV_adQssw5c
│                                      │                                                           │
└──────────── Header (Red) ────────────┴────────────── Payload (Green) ────────────────────────────┴────── Signature (Blue) ─────┘
```

**Breaking it down:**
| Part | Encoded Value | Decoded |
|------|---------------|---------|
| Header | `eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9` | `{"alg":"HS256","typ":"JWT"}` |
| Payload | `eyJzdWIiOiIxMjM0...` | `{"sub":"123","name":"John","iat":1516239022}` |
| Signature | `SflKxwRJSM...` | (Binary hash) |

---

### 🔢 JWT Algorithms

Common algorithms used for signing JWT tokens:

| JWA Spec | Algorithm                       | Description                      |
| -------- | ------------------------------- | -------------------------------- |
| HS256    | HMAC using SHA-256              | Symmetric key algorithm          |
| RS256    | RSASSA PKCS1 v1.5 using SHA-256 | Asymmetric RSA algorithm         |
| ES256    | ECDSA using P-256 and SHA-256   | Elliptic Curve algorithm         |
| PS256    | RSASSA-PSS + MGF1 with SHA-256  | RSA with probabilistic signature |

> 💡 These algorithms are available in 256, 384, and 512-bit formats (e.g., HS384, HS512).

**Algorithm Full Forms:**

| Short Form | Full Form                                  |
| ---------- | ------------------------------------------ |
| HMAC       | Keyed-Hash Message Authentication Code     |
| RSA        | Rivest, Shamir, Adleman                    |
| ECDSA      | Elliptic Curve Digital Signature Algorithm |
| SHA        | Secure Hash Algorithm                      |
| RSASSA     | RSA Signature Scheme with Appendix         |
| PKCS       | Public-Key Cryptography Standards          |

---

### 🌐 JWT.IO – Online JWT Debugger

[JWT.IO](https://jwt.io) is a helpful website to:

- **Decode** JWT tokens to see header and payload
- **Verify** signatures with your secret key
- **Create** new JWT tokens for testing
- **Debug** token issues quickly

> 💡 Use jwt.io during development to understand and debug your tokens!

---

### 📋 Pre-requisites for JWT in ASP.NET Core

Before implementing JWT authentication, you need:

#### 1️⃣ NuGet Package

Install the JWT Bearer authentication package:

```xml
<!-- ASPNETCoreWebAPI.csproj -->
<PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="10.0.2" />
```

Or install via Package Manager:

```powershell
Install-Package Microsoft.AspNetCore.Authentication.JwtBearer
```

---

#### 2️⃣ Secret Key in Configuration

Store your secret key securely in `appsettings.json`:

```json
// appsettings.json
{
  "JWTSecret": "This is secret key 3$%^&*()cauefuihUCHELAW HFE&&..."
}
```

> ⚠️ **Important:** In production, use environment variables or Azure Key Vault for secrets!

---

#### 3️⃣ Authorize Attribute

Use `[Authorize]` attribute to protect your controllers:

```csharp
using Microsoft.AspNetCore.Authorization;

[Authorize]  // Requires any authenticated user
public class StudentController : ControllerBase { }

[Authorize(Roles = "Superadmin, Admin")]  // Requires specific roles
public class StudentController : ControllerBase { }

[AllowAnonymous]  // Allows unauthenticated access to specific action
public async Task<ActionResult> PublicAction() { }
```

---

### ⚙️ Configure Web API to Use JWT

Here's how to configure JWT authentication in your Web API:

**Program.cs – JWT Configuration:**

```csharp
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Step 1: Read secret key from configuration
var key = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecret"));

// Step 2: Add Authentication Configuration
builder.Services.AddAuthentication(options =>
{
    // Set JWT Bearer as the default authentication scheme
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    // Save the token for later use
    options.SaveToken = true;

    // Configure token validation
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        // Validate the signing key
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),

        // For simplicity, we're not validating issuer and audience
        // In production, set these to true and configure valid values
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();

// ... other middleware

app.UseRouting();
app.UseCors("AllowAll");

// IMPORTANT: Add UseAuthentication() before UseAuthorization()
app.UseAuthentication();  // Validates JWT token
app.UseAuthorization();   // Checks user permissions

app.MapControllers();
app.Run();
```

---

### 🔒 Protecting Controllers with JWT

**StudentController.cs – Using Authorize Attribute:**

```csharp
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(Roles = "Superadmin, Admin")]  // 👈 Only these roles can access
    public class StudentController : ControllerBase
    {
        [HttpGet]
        [Route("All", Name = "GetAllStudents")]
        public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsAsync()
        {
            // Only authenticated users with Superadmin or Admin role can access
            var students = await _studentRepository.GetAllAsync();
            return Ok(students);
        }

        [HttpGet]
        [Route("Public")]
        [AllowAnonymous]  // 👈 Anyone can access this endpoint
        public ActionResult<string> GetPublicInfo()
        {
            return Ok("This is public information");
        }
    }
}
```

---

### 📊 JWT Authentication Flow

```
┌──────────────────────────────────────────────────────────────────┐
│                    JWT AUTHENTICATION FLOW                        │
├──────────────────────────────────────────────────────────────────┤
│                                                                   │
│  1. LOGIN REQUEST                                                 │
│  ─────────────────                                               │
│  ┌────────┐         POST /api/login           ┌──────────────┐   │
│  │ Client │  ──────────────────────────────▶  │   Web API    │   │
│  │        │   { username, password }          │              │   │
│  └────────┘                                   └──────────────┘   │
│                                                                   │
│  2. VALIDATE & GENERATE TOKEN                                     │
│  ────────────────────────────                                    │
│  ┌──────────────┐                                                │
│  │   Web API    │  ① Verify username/password                    │
│  │              │  ② Create JWT with user claims                 │
│  │              │  ③ Sign with secret key                        │
│  └──────────────┘                                                │
│                                                                   │
│  3. RETURN TOKEN                                                  │
│  ───────────────                                                 │
│  ┌──────────────┐    { "token": "eyJhbG..." }   ┌────────┐       │
│  │   Web API    │  ──────────────────────────▶  │ Client │       │
│  └──────────────┘                               └────────┘       │
│                                                                   │
│  4. API REQUEST WITH TOKEN                                        │
│  ─────────────────────────                                       │
│  ┌────────┐    GET /api/student/all            ┌──────────────┐  │
│  │ Client │  ───────────────────────────────▶  │   Web API    │  │
│  │        │   Authorization: Bearer eyJhbG...  │              │  │
│  └────────┘                                    └──────────────┘  │
│                                                                   │
│  5. VALIDATE TOKEN & RETURN DATA                                  │
│  ───────────────────────────────                                 │
│  ┌──────────────┐  ① Decode JWT                                  │
│  │   Web API    │  ② Verify signature                            │
│  │              │  ③ Check expiration                            │
│  │              │  ④ Validate role/claims                        │
│  │              │  ⑤ Return data if valid                        │
│  └──────────────┘                                                │
│                                                                   │
└──────────────────────────────────────────────────────────────────┘
```

---

### 🎯 Key Takeaways

1. **Authentication vs Authorization** – Auth verifies WHO you are, Authorization checks WHAT you can do
2. **JWT is self-contained** – All user info is encoded in the token itself
3. **Three parts** – Header (algorithm), Payload (user data), Signature (verification)
4. **Base64 encoded** – JWT is encoded, not encrypted (anyone can read the payload!)
5. **Signature validates integrity** – Ensures token hasn't been tampered
6. **Use `[Authorize]`** – Protect your endpoints with role-based authorization
7. **Store secrets securely** – Never hardcode secrets in code, use configuration
8. **Middleware order matters** – `UseAuthentication()` must come before `UseAuthorization()`

> ⚠️ **Security Note:** JWT payload is only encoded (Base64), not encrypted. Never store sensitive data like passwords in the payload!

---

### 🔧 Generating JWT Token in Web API

Now let's see how to **generate JWT tokens** in your Web API when a user logs in successfully.

#### Step 1: Create Login DTOs

First, create DTOs (Data Transfer Objects) for login request and response:

**LoginDTO.cs – Request Model:**

```csharp
// Model/LoginDTO.cs
using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreWebAPI.Model
{
    // This DTO receives username and password from the client
    public class LoginDTO
    {
        [Required]  // Username is mandatory
        public string Username { get; set; }

        [Required]  // Password is mandatory
        public string Password { get; set; }
    }
}
```

**LoginResponseDTO.cs – Response Model:**

```csharp
// Model/LoginResponseDTO.cs
namespace ASPNETCoreWebAPI.Model
{
    // This DTO sends the generated token back to the client
    public class LoginResponseDTO
    {
        public string Username { get; set; }  // Return username for display
        public string token { get; set; }      // The JWT token string
    }
}
```

---

#### Step 2: Create Login Controller

Now create a controller that handles login requests and generates JWT tokens:

**LoginController.cs – Complete Implementation:**

```csharp
// Controllers/LoginController.cs
using ASPNETCoreWebAPI.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ASPNETCoreWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]  // 👈 IMPORTANT: Allow unauthenticated access to login endpoint
    public class LoginController : ControllerBase
    {
        // Inject IConfiguration to read JWTSecret from appsettings.json
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost]
        public ActionResult Login(LoginDTO model)
        {
            // Step 1: Validate the model (check if username & password are provided)
            if (!ModelState.IsValid)
            {
                return BadRequest("Please provide username & password");
            }

            // Step 2: Create response object
            LoginResponseDTO response = new();

            // Step 3: Verify credentials (In real app, check against database)
            if (model.Username == "Kartik" && model.Password == "Kartik@123")
            {
                // ========== JWT TOKEN GENERATION STARTS HERE ==========

                // Step 4: Get the secret key from configuration
                var key = Encoding.ASCII.GetBytes(
                    _configuration.GetValue<string>("JWTSecret")
                );

                // Step 5: Create token handler
                var tokenHandler = new JwtSecurityTokenHandler();

                // Step 6: Create token descriptor with claims and settings
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    // Define the claims (user information) to include in token
                    Subject = new ClaimsIdentity(new Claim[]
                    {
                        // Add username claim - identifies WHO the user is
                        new Claim(ClaimTypes.Name, model.Username),

                        // Add role claim - defines WHAT the user can do
                        new Claim(ClaimTypes.Role, "Admin")
                    }),

                    // Set token expiration time (4 hours from now)
                    Expires = DateTime.Now.AddHours(4),

                    // Sign the token with our secret key using HMAC-SHA512
                    SigningCredentials = new SigningCredentials(
                        new SymmetricSecurityKey(key),
                        SecurityAlgorithms.HmacSha512Signature
                    )
                };

                // Step 7: Create the token
                var token = tokenHandler.CreateToken(tokenDescriptor);

                // Step 8: Convert token to string format
                response.token = tokenHandler.WriteToken(token);
                response.Username = model.Username;

                // ========== JWT TOKEN GENERATION ENDS HERE ==========
            }
            else
            {
                // Invalid credentials - return error message
                return Ok("Invalid username & password");
            }

            // Return the response with token
            return Ok(response);
        }
    }
}
```

---

#### 📊 Token Generation Flow Diagram

```
┌──────────────────────────────────────────────────────────────────┐
│                    JWT TOKEN GENERATION STEPS                     │
├──────────────────────────────────────────────────────────────────┤
│                                                                   │
│  1. Receive Login Request                                         │
│     ┌────────────────────────────────────────────┐               │
│     │  POST /api/Login                            │               │
│     │  { "username": "Kartik", "password": "..." }│               │
│     └────────────────────────────────────────────┘               │
│                           │                                       │
│                           ▼                                       │
│  2. Validate Credentials                                          │
│     ┌────────────────────────────────────────────┐               │
│     │  Check username & password against DB       │               │
│     │  (In this example: hardcoded values)        │               │
│     └────────────────────────────────────────────┘               │
│                           │                                       │
│              ┌────────────┴────────────┐                          │
│              ▼                         ▼                          │
│         ❌ Invalid                  ✅ Valid                      │
│     Return error message       Continue to Step 3                 │
│                                        │                          │
│                                        ▼                          │
│  3. Get Secret Key from Configuration                             │
│     ┌────────────────────────────────────────────┐               │
│     │  var key = Encoding.ASCII.GetBytes(         │               │
│     │      _configuration["JWTSecret"]            │               │
│     │  );                                         │               │
│     └────────────────────────────────────────────┘               │
│                           │                                       │
│                           ▼                                       │
│  4. Create Claims (User Info)                                     │
│     ┌────────────────────────────────────────────┐               │
│     │  ClaimTypes.Name → "Kartik" (Username)      │               │
│     │  ClaimTypes.Role → "Admin" (Role)           │               │
│     └────────────────────────────────────────────┘               │
│                           │                                       │
│                           ▼                                       │
│  5. Create Token Descriptor                                       │
│     ┌────────────────────────────────────────────┐               │
│     │  Subject: Claims                            │               │
│     │  Expires: DateTime.Now.AddHours(4)          │               │
│     │  SigningCredentials: HMAC-SHA512 + Key      │               │
│     └────────────────────────────────────────────┘               │
│                           │                                       │
│                           ▼                                       │
│  6. Generate & Return Token                                       │
│     ┌────────────────────────────────────────────┐               │
│     │  tokenHandler.CreateToken(tokenDescriptor)  │               │
│     │  tokenHandler.WriteToken(token)             │               │
│     │                                             │               │
│     │  Response: { "username": "Kartik",          │               │
│     │              "token": "eyJhbG..." }         │               │
│     └────────────────────────────────────────────┘               │
│                                                                   │
└──────────────────────────────────────────────────────────────────┘
```

---

#### 🔑 Key Classes Used for Token Generation

| Class                     | Namespace                         | Purpose                                              |
| ------------------------- | --------------------------------- | ---------------------------------------------------- |
| `JwtSecurityTokenHandler` | `System.IdentityModel.Tokens.Jwt` | Creates and writes JWT tokens                        |
| `SecurityTokenDescriptor` | `Microsoft.IdentityModel.Tokens`  | Describes token properties (claims, expiry, signing) |
| `ClaimsIdentity`          | `System.Security.Claims`          | Container for user claims                            |
| `Claim`                   | `System.Security.Claims`          | Individual piece of user information                 |
| `SymmetricSecurityKey`    | `Microsoft.IdentityModel.Tokens`  | The secret key for signing                           |
| `SigningCredentials`      | `Microsoft.IdentityModel.Tokens`  | Combines key + algorithm for signing                 |

---

### 🎬 JWT Authentication in Action

Now let's see how the **frontend (React UI)** interacts with the **backend (Web API)** to perform JWT authentication.

#### 📁 Project Structure

```
CollegeApp/
├── ASPNETCoreWebAPI/              ◀── Backend (Web API)
│   ├── Controllers/
│   │   ├── LoginController.cs     ◀── Generates JWT tokens
│   │   └── StudentController.cs   ◀── Protected with [Authorize]
│   └── Model/
│       ├── LoginDTO.cs
│       └── LoginResponseDTO.cs
│
└── student-ui/                    ◀── Frontend (React)
    └── src/
        ├── api/
        │   └── studentApi.js      ◀── API calls + token management
        ├── components/
        │   ├── Login.jsx          ◀── Login form UI
        │   └── StudentList.jsx    ◀── Displays student data
        └── App.jsx                ◀── Main application
```

---

#### Step 1: Frontend API Service

Create an API service that handles login and token management:

**studentApi.js – API Service with Token Management:**

```javascript
// student-ui/src/api/studentApi.js
import axios from "axios";

// Define API base URLs
const API_BASE = "https://localhost:7234/api";
const STUDENT_URL = `${API_BASE}/Student`;
const LOGIN_URL = `${API_BASE}/Login`;

// Set default headers for all axios requests
axios.defaults.headers.common["Content-Type"] = "application/json";
axios.defaults.headers.common["Accept"] = "application/json";

// ========== TOKEN MANAGEMENT FUNCTIONS ==========

/**
 * Set JWT token in axios default headers
 * This token will be sent with every subsequent request
 * @param {string} token - The JWT token received from login
 */
export const setToken = (token) => {
  if (token) {
    // Add Authorization header with Bearer token
    axios.defaults.headers.common["Authorization"] = `Bearer ${token}`;
  } else {
    // Remove Authorization header (for logout)
    delete axios.defaults.headers.common["Authorization"];
  }
};

/**
 * Get current token from headers
 * @returns {string|null} The current JWT token or null
 */
export const getToken = () => {
  return (
    axios.defaults.headers.common["Authorization"]?.replace("Bearer ", "") ||
    null
  );
};

// ========== API FUNCTIONS ==========

/**
 * Login function - calls the Login API
 * @param {string} username - User's username
 * @param {string} password - User's password
 * @returns {Object} Response containing username and token
 */
export const login = async (username, password) => {
  // POST request to /api/Login with credentials
  const response = await axios.post(LOGIN_URL, { username, password });
  return response.data;
};

/**
 * Get all students - requires valid JWT token
 * @returns {Array} List of all students
 */
export const getAllStudents = async () => {
  // GET request to /api/Student/All
  // Authorization header is automatically added by axios
  const response = await axios.get(`${STUDENT_URL}/All`);
  return response.data;
};
```

---

#### Step 2: Login Component

Create a React component for the login form:

**Login.jsx – Login Form Component:**

```jsx
// student-ui/src/components/Login.jsx
import { useState } from "react";
import { login, setToken } from "../api/studentApi";

function Login({ onLoginSuccess }) {
  // State variables for form inputs and UI
  const [username, setUsername] = useState("");
  const [password, setPassword] = useState("");
  const [loggedInUser, setLoggedInUser] = useState(null); // Currently logged in user
  const [error, setError] = useState(""); // Error message
  const [loading, setLoading] = useState(false); // Loading state

  /**
   * Handle login button click
   * Calls the API and stores the token
   */
  const handleLogin = async () => {
    setLoading(true); // Show loading indicator
    setError(""); // Clear previous errors

    try {
      // Step 1: Call login API with credentials
      const response = await login(username, password);
      console.log("Login response:", response);

      // Step 2: Check if we received a token
      if (response && response.token) {
        // Step 3: Store token in axios headers for future requests
        setToken(response.token);

        // Step 4: Update UI state
        setLoggedInUser(response.username);
        setError("");

        // Step 5: Notify parent component (optional)
        if (onLoginSuccess) {
          onLoginSuccess(response.username, response.token);
        }
      } else {
        // Handle invalid credentials response
        const errorMsg =
          typeof response === "string"
            ? response
            : "Invalid username & password";
        setError(errorMsg);
      }
    } catch (err) {
      console.error("Login error:", err);
      // Extract and display error message
      let errorMsg = "Login failed";
      if (err.response?.data) {
        errorMsg =
          typeof err.response.data === "string"
            ? err.response.data
            : JSON.stringify(err.response.data);
      } else if (err.message) {
        errorMsg = err.message;
      }
      setError(errorMsg);
    } finally {
      setLoading(false); // Hide loading indicator
    }
  };

  /**
   * Handle logout button click
   * Clears the token and resets state
   */
  const handleLogout = () => {
    setToken(null); // Remove token from headers
    setLoggedInUser(null); // Clear logged in user
    setUsername(""); // Clear form inputs
    setPassword("");
  };

  // Render login form or logged-in state
  return (
    <div className="login-container">
      {loggedInUser ? (
        // Show welcome message and logout button when logged in
        <div className="logged-in">
          <span className="welcome-text">
            Welcome, <strong>{loggedInUser}</strong>!
          </span>
          <button onClick={handleLogout} className="logout-btn">
            Logout
          </button>
        </div>
      ) : (
        // Show login form when not logged in
        <div className="login-form">
          <input
            type="text"
            placeholder="Username"
            value={username}
            onChange={(e) => setUsername(e.target.value)}
            className="login-input"
          />
          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={(e) => setPassword(e.target.value)}
            className="login-input"
          />
          <button
            onClick={handleLogin}
            disabled={loading}
            className="login-btn"
          >
            {loading ? "Logging in..." : "Login"}
          </button>
        </div>
      )}
      {/* Display error message if any */}
      {error && <p className="error">{error}</p>}
    </div>
  );
}

export default Login;
```

---

#### Step 3: Main Application

Integrate login component with your main app:

**App.jsx – Main Application:**

```jsx
// student-ui/src/App.jsx
import StudentList from "./components/StudentList";
import Login from "./components/Login";
import "./index.css";

function App() {
  return (
    <div className="container">
      <h2>Student API Test UI</h2>

      {/* Login component - handles authentication */}
      <Login />

      <hr className="divider" />

      {/* StudentList - fetches data using JWT token */}
      <StudentList />
    </div>
  );
}

export default App;
```

---

#### 📊 Complete Authentication Flow

```
┌──────────────────────────────────────────────────────────────────────────────┐
│                         JWT AUTHENTICATION IN ACTION                          │
├──────────────────────────────────────────────────────────────────────────────┤
│                                                                               │
│  ┌─────────────────────┐                        ┌─────────────────────┐      │
│  │     FRONTEND        │                        │      BACKEND        │      │
│  │    (React UI)       │                        │     (Web API)       │      │
│  └─────────────────────┘                        └─────────────────────┘      │
│                                                                               │
│  ═══════════════════════ STEP 1: USER LOGIN ════════════════════════════     │
│                                                                               │
│  ┌─────────────────────┐   POST /api/Login      ┌─────────────────────┐      │
│  │  User enters:       │   ─────────────────▶   │  LoginController    │      │
│  │  Username: Kartik   │   { username,          │                     │      │
│  │  Password: ****     │     password }         │  • Validates creds  │      │
│  │                     │                        │  • Generates JWT    │      │
│  │  login(u, p)        │                        │  • Returns token    │      │
│  └─────────────────────┘                        └─────────────────────┘      │
│                                                          │                    │
│                                                          ▼                    │
│  ┌─────────────────────┐   200 OK + Token       ┌─────────────────────┐      │
│  │  Receives response: │   ◀─────────────────   │  {                  │      │
│  │                     │                        │    username:"Kartik"│      │
│  │  setToken(token)    │                        │    token:"eyJ..."   │      │
│  │  (stores in axios)  │                        │  }                  │      │
│  └─────────────────────┘                        └─────────────────────┘      │
│                                                                               │
│  ══════════════ STEP 2: ACCESS PROTECTED RESOURCE ══════════════════════     │
│                                                                               │
│  ┌─────────────────────┐   GET /api/Student/All ┌─────────────────────┐      │
│  │  User clicks:       │   ─────────────────▶   │  StudentController  │      │
│  │  "Get Students"     │   Headers:             │  [Authorize]        │      │
│  │                     │   Authorization:       │                     │      │
│  │  getAllStudents()   │   Bearer eyJhbG...     │  • Validates token  │      │
│  └─────────────────────┘                        │  • Checks role      │      │
│                                                 │  • Returns data     │      │
│                                                 └─────────────────────┘      │
│                                                          │                    │
│              Token Valid?  ─────┬─────────────────       │                    │
│                                 │              │         ▼                    │
│                              ❌ NO           ✅ YES                           │
│                                 ▼              │  ┌─────────────────────┐      │
│                      ┌─────────────────┐       │  │  200 OK + Data      │      │
│                      │  401 Unauthorized│      │  │  [                  │      │
│                      │  Access Denied   │      │  │   {id:1, name:...}, │      │
│                      └─────────────────┘       │  │   {id:2, name:...}  │      │
│                                                │  │  ]                  │      │
│                                                │  └─────────────────────┘      │
│  ┌─────────────────────┐                       │                              │
│  │  Displays students  │◀──────────────────────┘                              │
│  │  in a table         │                                                      │
│  └─────────────────────┘                                                      │
│                                                                               │
│  ════════════════════ STEP 3: LOGOUT ═══════════════════════════════════     │
│                                                                               │
│  ┌─────────────────────┐                                                      │
│  │  User clicks:       │                                                      │
│  │  "Logout"           │                                                      │
│  │                     │                                                      │
│  │  setToken(null)     │  ◀── Removes Authorization header                   │
│  │  (clears token)     │      Future requests will be rejected               │
│  └─────────────────────┘                                                      │
│                                                                               │
└──────────────────────────────────────────────────────────────────────────────┘
```

---

#### 🧪 Testing the Authentication

1. **Start the Backend:** Run the Web API on `https://localhost:7234`
2. **Start the Frontend:** Run the React app on `http://localhost:5173`
3. **Test Login:**
   - Username: `Kartik`
   - Password: `Kartik@123`
4. **After successful login:** Click "Get Students" to fetch protected data
5. **Test Logout:** Click "Logout" and try to fetch students again (will fail with 401)

---

#### 💡 What Happens Behind the Scenes

| Step | Action                  | What Happens                                       |
| ---- | ----------------------- | -------------------------------------------------- |
| 1    | User enters credentials | `Login.jsx` captures username & password           |
| 2    | Click Login             | `studentApi.js` → `login()` → POST to `/api/Login` |
| 3    | Backend validates       | `LoginController` checks credentials               |
| 4    | Token generated         | JWT created with username & role claims            |
| 5    | Token returned          | Response: `{ username, token }`                    |
| 6    | Token stored            | `setToken(token)` adds to axios headers            |
| 7    | Access protected API    | GET `/api/Student/All` with `Bearer token`         |
| 8    | Backend validates token | Middleware checks signature, expiry, role          |
| 9    | Data returned           | Student list sent if token valid                   |
| 10   | Logout                  | `setToken(null)` removes Authorization header      |

---

### 🎯 Key Takeaways

1. **Authentication vs Authorization** – Auth verifies WHO you are, Authorization checks WHAT you can do
2. **JWT is self-contained** – All user info is encoded in the token itself
3. **Three parts** – Header (algorithm), Payload (user data), Signature (verification)
4. **Base64 encoded** – JWT is encoded, not encrypted (anyone can read the payload!)
5. **Signature validates integrity** – Ensures token hasn't been tampered
6. **Use `[Authorize]`** – Protect your endpoints with role-based authorization
7. **Store secrets securely** – Never hardcode secrets in code, use configuration
8. **Middleware order matters** – `UseAuthentication()` must come before `UseAuthorization()`
9. **Token generation** – Use `JwtSecurityTokenHandler` with claims and signing credentials
10. **Frontend token storage** – Store token in axios headers for automatic inclusion in requests

> ⚠️ **Security Note:** JWT payload is only encoded (Base64), not encrypted. Never store sensitive data like passwords in the payload!

---

### 🔐 Using Multiple JWT Authentications (Default vs Named JWT Policies)

In real-world applications, you may need to support **multiple authentication providers** (like Google, Microsoft, Local login) with different JWT tokens. ASP.NET Core allows you to configure **named JWT policies** to handle this scenario.

---

#### 🚦 Understanding 401 vs 403 Errors

Before diving into multiple policies, let's understand two important HTTP error codes:

| Error Code | Name         | Meaning                                  | When It Occurs                  |
| ---------- | ------------ | ---------------------------------------- | ------------------------------- |
| **401**    | Unauthorized | User is NOT authenticated                | Missing or invalid JWT token    |
| **403**    | Forbidden    | User IS authenticated but not authorized | User doesn't have required role |

##### Example: 403 Forbidden Error

```
Scenario: StudentController requires roles "Superadmin, Admin"
User logs in successfully with role "Admin" → Token generated ✅
User accesses /api/Student → Access granted ✅

Now, if we change the controller to require only "Superadmin":
[Authorize(Roles = "Superadmin")]  // Admin role removed

User with "Admin" role tries to access /api/Student
→ 403 Forbidden ❌ (User is authenticated but lacks the required role)
```

> 💡 **Key Point:** 403 means the user's identity is verified (authenticated), but they don't have permission (not authorized) for that specific resource.

---

#### 📊 Default vs Named JWT Policies

| Aspect               | Default Policy                 | Named Policies                                      |
| -------------------- | ------------------------------ | --------------------------------------------------- |
| **When to use**      | Single authentication provider | Multiple authentication providers                   |
| **Configuration**    | `.AddJwtBearer()`              | `.AddJwtBearer("PolicyName", ...)`                  |
| **Token Validation** | One secret key                 | Different secret keys per provider                  |
| **Controller Usage** | `[Authorize]`                  | `[Authorize(AuthenticationSchemes = "PolicyName")]` |

---

#### Step 1: Configure Multiple Secret Keys

Add separate secret keys for each authentication provider in `appsettings.json`:

**appsettings.json:**

```json
{
  "ConnectionStrings": {
    "CollegeAppDBConnection": "..."
  },
  // Different secret keys for different providers
  "JWTSecretForGoogle": "GoogleThisissecretkey$%^&*()cauefuihUCHELAW...",
  "JWTSecretForMicrosoft": "MicrosoftThisissecretkey$%^&*()cauefuihUCHELAW...",
  "JWTSecretForLocal": "LocalThisissecretkey$%^&*()cauefuihUCHELAW..."
}
```

---

#### Step 2: Configure Named JWT Policies in Program.cs

**Program.cs – Multiple Named JWT Policies:**

```csharp
// Program.cs

// Step 1: Read different secret keys for each provider
var keyGoogle = Encoding.ASCII.GetBytes(
    builder.Configuration.GetValue<string>("JWTSecretForGoogle")
);
var keyMicrosoft = Encoding.ASCII.GetBytes(
    builder.Configuration.GetValue<string>("JWTSecretForMicrosoft")
);
var keyLocal = Encoding.ASCII.GetBytes(
    builder.Configuration.GetValue<string>("JWTSecretForLocal")
);

// Step 2: Configure Authentication with Named Policies
builder.Services.AddAuthentication(options =>
{
    // Default scheme (used when no specific scheme is mentioned)
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
// Named Policy 1: For Google Users
.AddJwtBearer("LoginForGoogleUsers", options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyGoogle),  // Google's key
        ValidateIssuer = false,
        ValidateAudience = false
    };
})
// Named Policy 2: For Microsoft Users
.AddJwtBearer("LoginForMicrosoftUsers", options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyMicrosoft),  // Microsoft's key
        ValidateIssuer = false,
        ValidateAudience = false
    };
})
// Named Policy 3: For Local Users
.AddJwtBearer("LoginForLocalUsers", options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyLocal),  // Local key
        ValidateIssuer = false,
        ValidateAudience = false
    };
});
```

---

#### Step 3: Update LoginDTO to Include Policy

**LoginDTO.cs – With Policy Field:**

```csharp
// Model/LoginDTO.cs
using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreWebAPI.Model
{
    public class LoginDTO
    {
        [Required]
        public string Policy { get; set; }     // "Local", "Microsoft", or "Google"

        [Required]
        public string Username { get; set; }

        [Required]
        public string Password { get; set; }
    }
}
```

---

#### Step 4: Update LoginController to Handle Multiple Policies

**LoginController.cs – Dynamic Key Selection:**

```csharp
// Controllers/LoginController.cs
[Route("api/[controller]")]
[ApiController]
[AllowAnonymous]
public class LoginController : ControllerBase
{
    private readonly IConfiguration _configuration;

    public LoginController(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    [HttpPost]
    public ActionResult Login(LoginDTO model)
    {
        if (!ModelState.IsValid)
            return BadRequest("Please provide policy, username & password");

        LoginResponseDTO response = new() { Username = model.Username };

        // Step 1: Select the correct secret key based on policy
        byte[] key = null;
        if (model.Policy == "Local")
            key = Encoding.ASCII.GetBytes(
                _configuration.GetValue<string>("JWTSecretForLocal")
            );
        else if (model.Policy == "Microsoft")
            key = Encoding.ASCII.GetBytes(
                _configuration.GetValue<string>("JWTSecretForMicrosoft")
            );
        else if (model.Policy == "Google")
            key = Encoding.ASCII.GetBytes(
                _configuration.GetValue<string>("JWTSecretForGoogle")
            );

        // Step 2: Validate credentials
        if (model.Username == "Kartik" && model.Password == "Kartik@123")
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, model.Username),
                    new Claim(ClaimTypes.Role, "Admin")  // Role assigned to user
                }),
                Expires = DateTime.Now.AddHours(4),
                // Step 3: Sign with the selected key
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature
                )
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            response.token = tokenHandler.WriteToken(token);
        }
        else
        {
            return Ok("Invalid username & password");
        }

        return Ok(response);
    }
}
```

---

#### Step 5: Protect Controllers with Specific Authentication Schemes

**StudentController.cs – Using Local Policy:**

```csharp
// Controllers/StudentController.cs
[Route("api/[controller]")]
[ApiController]
// 👇 Uses "LoginForLocalUsers" scheme with role-based authorization
[Authorize(AuthenticationSchemes = "LoginForLocalUsers", Roles = "Superadmin, Admin")]
public class StudentController : ControllerBase
{
    // Only users with Local token AND (Superadmin OR Admin) role can access

    [HttpGet]
    [Route("All")]
    public async Task<ActionResult<IEnumerable<StudentDTO>>> GetStudentsAsync()
    {
        // Protected endpoint
        return Ok(await _studentRepository.GetAllAsync());
    }
}
```

**MicrosoftController.cs – Using Microsoft Policy:**

```csharp
// Controllers/MicrosoftController.cs
[Route("api/[controller]")]
[ApiController]
// 👇 Uses "LoginForMicrosoftUsers" scheme
[Authorize(AuthenticationSchemes = "LoginForMicrosoftUsers", Roles = "Superadmin, Admin")]
public class MicrosoftController : ControllerBase
{
    // Only users with Microsoft token AND (Superadmin OR Admin) role can access

    [HttpGet]
    public ActionResult Get()
    {
        return Ok("This is Microsoft protected data");
    }
}
```

---

#### 📊 Authentication Flow with Multiple Policies

```
┌──────────────────────────────────────────────────────────────────────────────┐
│                    MULTIPLE JWT POLICIES AUTHENTICATION                       │
├──────────────────────────────────────────────────────────────────────────────┤
│                                                                               │
│  ┌─────────────────────┐                        ┌─────────────────────┐      │
│  │     FRONTEND        │                        │      BACKEND        │      │
│  └─────────────────────┘                        └─────────────────────┘      │
│                                                                               │
│  ═══════════════════ LOGIN WITH POLICY SELECTION ════════════════════════    │
│                                                                               │
│  User selects policy:                                                         │
│  ┌─────────────────────┐                                                      │
│  │ ● Local             │                                                      │
│  │ ○ Microsoft         │                                                      │
│  │ ○ Google            │                                                      │
│  └─────────────────────┘                                                      │
│           │                                                                   │
│           ▼                                                                   │
│  ┌─────────────────────┐   POST /api/Login      ┌─────────────────────┐      │
│  │ login("Local",      │   ─────────────────▶   │  LoginController    │      │
│  │       "Kartik",     │   { policy: "Local",   │                     │      │
│  │       "Kartik@123") │     username, pwd }    │  Selects key based  │      │
│  └─────────────────────┘                        │  on policy          │      │
│                                                 └─────────────────────┘      │
│                                                          │                    │
│                                                          ▼                    │
│                                                 ┌─────────────────────┐      │
│  Token signed with:                             │  if (policy=="Local")│      │
│  JWTSecretForLocal ◀──────────────────────────  │    key = LocalKey   │      │
│                                                 │  else if (Microsoft) │      │
│                                                 │    key = MicrosoftKey│      │
│                                                 └─────────────────────┘      │
│                                                                               │
│  ═════════════════ ACCESSING PROTECTED ENDPOINTS ════════════════════════    │
│                                                                               │
│  With LOCAL token:                                                            │
│  ┌─────────────────────┐   GET /api/Student     ┌─────────────────────┐      │
│  │ Authorization:      │   ─────────────────▶   │ [Authorize(Schemes= │      │
│  │ Bearer <LocalToken> │                        │  "LoginForLocalUsers")]    │
│  └─────────────────────┘                        │       ✅ SUCCESS    │      │
│                                                 └─────────────────────┘      │
│                                                                               │
│  With LOCAL token:                                                            │
│  ┌─────────────────────┐   GET /api/Microsoft   ┌─────────────────────┐      │
│  │ Authorization:      │   ─────────────────▶   │ [Authorize(Schemes= │      │
│  │ Bearer <LocalToken> │                        │  "LoginForMicrosoft")]     │
│  └─────────────────────┘                        │       ❌ 401        │      │
│                                                 │  (Wrong policy!)    │      │
│                                                 └─────────────────────┘      │
│                                                                               │
│  ═══════════════════ ROLE MISMATCH (403 FORBIDDEN) ══════════════════════    │
│                                                                               │
│  User has role: "Admin"                                                       │
│  Controller requires: [Authorize(Roles = "Superadmin")]                       │
│                                                                               │
│  ┌─────────────────────┐   GET /api/Student     ┌─────────────────────┐      │
│  │ Token with role:    │   ─────────────────▶   │ Roles = "Superadmin"│      │
│  │ "Admin"             │                        │                     │      │
│  └─────────────────────┘                        │ ❌ 403 FORBIDDEN    │      │
│                                                 │ (Authenticated but  │      │
│                                                 │  not authorized!)   │      │
│                                                 └─────────────────────┘      │
│                                                                               │
└──────────────────────────────────────────────────────────────────────────────┘
```

---

#### Step 6: Frontend Integration

Update the frontend to send the policy with login:

**studentApi.js – With Policy Parameter:**

```javascript
// student-ui/src/api/studentApi.js
export const login = async (policy, username, password) => {
  // Send policy along with credentials
  const response = await axios.post(LOGIN_URL, { policy, username, password });
  return response.data;
};

// Call Microsoft-specific endpoint
export const callMicrosoft = async () => {
  const response = await axios.get(`${API_BASE}/Microsoft`);
  return response.data;
};
```

**Login.jsx – Policy Selection:**

```jsx
// student-ui/src/components/Login.jsx
const handleLogin = async () => {
  try {
    // For Local authentication (StudentController)
    const response = await login("Local", username, password);

    // For Microsoft authentication (MicrosoftController)
    // const response = await login("Microsoft", username, password);

    // For Google authentication
    // const response = await login("Google", username, password);

    if (response && response.token) {
      setToken(response.token);
      setLoggedInUser(response.username);
    }
  } catch (err) {
    setError(err.message);
  }
};
```

---

#### 🧪 Testing Different Scenarios

| Scenario                                            | Policy    | Endpoint         | Expected Result     |
| --------------------------------------------------- | --------- | ---------------- | ------------------- |
| Login with Local, access Student                    | Local     | `/api/Student`   | ✅ 200 OK           |
| Login with Local, access Microsoft                  | Local     | `/api/Microsoft` | ❌ 401 Unauthorized |
| Login with Microsoft, access Microsoft              | Microsoft | `/api/Microsoft` | ✅ 200 OK           |
| Login with Admin role, endpoint requires Superadmin | Any       | Any              | ❌ 403 Forbidden    |

---

#### 💡 Key Points to Remember

1. **Named Policies** – Use `.AddJwtBearer("PolicyName", ...)` for multiple providers
2. **Secret Keys** – Each policy should have its own secret key
3. **AuthenticationSchemes** – Controller must specify which scheme to use
4. **401 vs 403** – 401 = Not authenticated, 403 = Authenticated but wrong role
5. **Policy in Login Request** – Frontend must send the correct policy
6. **Token Matching** – Token must be signed with the key that the controller expects

> 🔒 **Security Best Practice:** Always verify that the token's policy matches the endpoint's authentication scheme. A token generated for "Local" policy won't work on endpoints protected by "Microsoft" policy.

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 28. JWT Authentication in Swagger UI

### 🤔 The Problem: Testing Protected Endpoints

Once you add `[Authorize]` to your controllers, your API endpoints become **protected** — they require a valid JWT token in every request. But when you open **Swagger UI** to test your endpoints, there's no built-in way to provide a token. Every request to a protected endpoint returns **401 Unauthorized**.

```
Without Swagger JWT Setup:

┌─────────────────────────┐          ┌──────────────────────┐
│       Swagger UI        │          │      Web API         │
│                         │          │                      │
│  GET /api/student/all   │────────▶│  [Authorize]         │
│  (No token!)            │          │  StudentController   │
│                         │◀────────│                      │
│  ❌ 401 Unauthorized    │          │  "Where is token?"   │
└─────────────────────────┘          └──────────────────────┘
```

> ⚠️ **Problem:** You can't test protected API endpoints in Swagger without manually adding the `Authorization` header every time!

---

### ✅ The Solution: Configure Swagger for JWT

We configure Swagger to show an **"Authorize" button** 🔒 at the top of the page. Once you click it and enter your JWT token, Swagger automatically adds the `Authorization: Bearer <token>` header to every request.

```
With Swagger JWT Setup:

┌─────────────────────────┐          ┌──────────────────────┐
│       Swagger UI        │          │      Web API         │
│                         │          │                      │
│  🔒 Authorize Button    │          │                      │
│  ┌───────────────────┐  │          │                      │
│  │ Bearer eyJhbG...  │  │          │                      │
│  └───────────────────┘  │          │                      │
│                         │          │                      │
│  GET /api/student/all   │────────▶│  [Authorize]         │
│  Authorization: Bearer  │          │  StudentController   │
│  eyJhbG...              │          │                      │
│                         │◀────────│                      │
│  ✅ 200 OK              │          │  "Token valid!"      │
└─────────────────────────┘          └──────────────────────┘
```

---

### 📦 How It Works – Step by Step

**Step 1:** Open Swagger UI → You see the 🔒 lock icon and **Authorize** button at the top

**Step 2:** First, call `POST /api/Login` with your credentials to get a JWT token

**Step 3:** Click the **Authorize** button → Enter `Bearer <your-token>`

**Step 4:** Now all subsequent API requests automatically include the token in the `Authorization` header

---

### 🔧 Code Implementation in `Program.cs`

To enable JWT authentication in Swagger, we need to configure **two things** inside `AddSwaggerGen`:

#### 1️⃣ Security Definition – Tell Swagger about Bearer token

```csharp
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the bearer scheme. Enter Bearer [space] add your token in the text input. Example: Bearer eyJhbG...",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Scheme = "Bearer"
    });
```

| Property      | Description                                                           |
| ------------- | --------------------------------------------------------------------- |
| `"Bearer"`    | The name of the security scheme (used as a reference ID)              |
| `Description` | Help text shown in the Authorize dialog explaining how to enter token |
| `Name`        | The HTTP header name where the token goes (`Authorization`)           |
| `In`          | Where to send the token — `ParameterLocation.Header`                  |
| `Scheme`      | Authentication scheme type — `Bearer`                                 |

#### 2️⃣ Security Requirement – Apply it globally to all endpoints

```csharp
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header
            },
            new List<string>()
        }
    });
});
```

| Property             | Description                                                    |
| -------------------- | -------------------------------------------------------------- |
| `Reference.Id`       | Must match the `"Bearer"` name used in `AddSecurityDefinition` |
| `ReferenceType`      | Tells Swagger this is a `SecurityScheme` reference             |
| `new List<string>()` | Empty list means no specific scopes are required               |

> 💡 **`AddSecurityDefinition`** tells Swagger **what** the security scheme is. **`AddSecurityRequirement`** tells Swagger to **apply** it to all endpoints.

---

### 🧪 Testing Flow in Swagger

```
Step 1: Login to get token
─────────────────────────
POST /api/Login
Body: { "policy": "Local", "username": "Kartik", "password": "Kartik@123" }
Response: { "username": "Kartik", "token": "eyJhbGciOi..." }

Step 2: Click Authorize button
──────────────────────────────
Enter: Bearer eyJhbGciOi...

Step 3: Call protected endpoint
───────────────────────────────
GET /api/Student/All
Header added automatically: Authorization: Bearer eyJhbGciOi...
Response: ✅ 200 OK with student data
```

---

### 💡 Key Points to Remember

1. **`AddSecurityDefinition`** – Defines the Bearer token scheme in Swagger UI
2. **`AddSecurityRequirement`** – Makes all endpoints require authentication by default
3. **OpenApiSecurityScheme** – Configures how the token is passed (Header, Bearer scheme)
4. **Authorize Button** – Appears in Swagger UI after configuration, allows entering the JWT token once
5. **NuGet Package** – `Swashbuckle.AspNetCore` (included by default in ASP.NET Core Web API projects) handles all Swagger functionality

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 29. Why We Need Issuer & Audience in JWT

### 🤔 What Are Issuer and Audience?

When a JWT token is created, it includes special claims that identify **who created the token** and **who should use the token**:

| Claim     | Full Name | Description                                | Property in Code   |
| --------- | --------- | ------------------------------------------ | ------------------ |
| **`iss`** | Issuer    | Who is **generating** the JWT token        | `ValidateIssuer`   |
| **`aud`** | Audience  | Who is **consuming** (using) the JWT token | `ValidateAudience` |

> 🔑 **Think of it like a letter:** The **Issuer** is the sender's address, and the **Audience** is the recipient's address. Just like you validate that a letter came from the right person and was meant for you!

---

### 🏢 Scenario 1: Third-Party Issuer & Audience

In real-world enterprise applications, the token generator and the token consumer are often **different systems**. A third-party identity provider (like Azure AD, Google, or Auth0) generates tokens that multiple applications can use.

```
                    THIRD-PARTY ISSUER & AUDIENCE SCENARIO

  ┌────────────────────┐
  │     Azure AD       │◀────── Third-Party Identity Provider
  │  (Token Generator) │
  │                    │
  │  ISS: "AzureAD"   │
  │  AUD: "NProviders" │
  └────────┬───────────┘
           │
           │ Generates JWT token
           │ (iss: AzureAD, aud: NProviders)
           ▼
  ┌────────────────────┐         ┌────────────────────┐
  │    Weather API     │────────▶│     Web API        │
  │  (Data Provider)   │  Data   │   "NProviders"     │
  │                    │         │  (Token Consumer)  │
  │  Validates Token:  │         │                    │
  │  ✅ ISS = AzureAD  │         │  Validates Token:  │
  │                    │         │  ✅ AUD = NProviders│
  └────────────────────┘         └─────────┬──────────┘
                                           │
                                           │ Provides Data
                                           ▼
                                 ┌────────────────────┐
                                 │    Angular App     │
                                 │ (Displays Weather) │
                                 └────────────────────┘
```

**How it works in Enterprise:**

1. **Weather API** doesn't create tokens itself — it relies on **Azure AD** (a third-party identity provider) to generate JWT tokens
2. Azure AD sets `iss: "AzureAD"` (it is the issuer) and `aud: "NProviders"` (the token is intended for NProviders)
3. **Weather API validates the Issuer** — It checks that the token was actually created by Azure AD (a trusted source)
4. **Web API (NProviders) validates the Audience** — It checks that the token was specifically meant for it
5. The Web API then provides the data to the Angular App for display

> 💡 **Why Third-Party?** In enterprise environments, you don't want every application to manage its own authentication. A centralized identity provider like Azure AD handles user authentication, and all applications trust the tokens it generates.

---

### 🏠 Scenario 2: Local Issuer & Audience (Our Approach)

In simpler applications (like our College App), a **single application** does everything — it generates the token, provides the data, and consumes the token itself.

```
                     LOCAL ISSUER & AUDIENCE SCENARIO

  ┌──────────────────────────────────────────────────┐
  │              ❌ Azure AD (Not needed)              │
  │              ❌ Weather API (Not needed)           │
  └──────────────────────────────────────────────────┘

  ┌──────────────────────────────────────────────────┐
  │           OUR SINGLE WEB API APPLICATION          │
  │                                                    │
  │   ┌────────────────┐                               │
  │   │ LoginController│ ◀── Token Generator (Issuer) │
  │   │  POST /Login   │                               │
  │   └───────┬────────┘                               │
  │           │                                        │
  │           │ Generates JWT Token                    │
  │           │ (iss: "NehantWorld", aud: "Local")     │
  │           ▼                                        │
  │   ┌────────────────┐                               │
  │   │StudentControllr│ ◀── Data Provider             │
  │   │ GET /Students  │     & Token Consumer          │
  │   │                │     (Audience)                │
  │   │ Validates:     │                               │
  │   │ ✅ ISS match   │                               │
  │   │ ✅ AUD match   │                               │
  │   └───────┬────────┘                               │
  │           │                                        │
  └───────────┼────────────────────────────────────────┘
              │ Provides Data
              ▼
  ┌──────────────────────┐
  │   Angular / React    │
  │    Frontend App      │
  │ (Displays Students)  │
  └──────────────────────┘
```

**How it works in Our Application:**

1. We are **NOT using** any third-party identity provider (❌ Azure AD, ❌ External Weather API)
2. Our **single Web API application does everything**:
   - **Token Generator (Issuer)** – `LoginController` creates the JWT token
   - **Data Provider** – `StudentController` provides the student data
   - **Token Consumer (Audience)** – The same application validates and consumes the token
3. All our previous demos follow this **Local approach** where everything is in a single app

> 🔒 **In this approach, we are everything** — we are the token generator (Issuer), data provider, and token consumer (Audience). No external services involved!

---

### ⚙️ Configuration in `appsettings.json`

For each JWT policy, we define separate **Issuer**, **Audience**, and **Secret Key** values:

```json
{
  "JWTSecretForGoogle": "GoogleThisissecretkey$%^&*()cauefuih...",
  "JWTSecretForMicrosoft": "MicrosoftThisissecretkey$%^&*()cauefuih...",
  "JWTSecretForLocal": "LocalThisissecretkey$%^&*()cauefuih...",

  "GoogleAudience": "Google",
  "MicrosoftAudience": "Microsoft",
  "LocalAudience": "Local",

  "GoogleIssuer": "GoogleAthentication",
  "MicrosoftIssuer": "MicrosoftAAD",
  "LocalIssuer": "NehantWorld"
}
```

| Policy        | Issuer                | Audience    | Description                                |
| ------------- | --------------------- | ----------- | ------------------------------------------ |
| **Google**    | `GoogleAthentication` | `Google`    | For users authenticating via Google        |
| **Microsoft** | `MicrosoftAAD`        | `Microsoft` | For users authenticating via Microsoft AAD |
| **Local**     | `NehantWorld`         | `Local`     | For local application authentication       |

---

### 🔧 How Issuer & Audience Are Validated in `Program.cs`

When configuring JWT authentication, we tell the application to **validate both the Issuer and Audience**:

```csharp
var keyLocal = Encoding.ASCII.GetBytes(builder.Configuration.GetValue<string>("JWTSecretForLocal"));
string LocalAudience = builder.Configuration.GetValue<string>("LocalAudience");
string LocalIssuer = builder.Configuration.GetValue<string>("LocalIssuer");

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer("LoginForLocalUsers", options =>
{
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        // validate the signing key
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(keyLocal),

        // ✅ Validate Issuer - check who generated the token
        ValidateIssuer = true,
        ValidIssuer = LocalIssuer,    // Must match "NehantWorld"

        // ✅ Validate Audience - check who should consume the token
        ValidateAudience = true,
        ValidAudience = LocalAudience, // Must match "Local"
    };
});
```

| Parameter                  | Value           | What It Does                                        |
| -------------------------- | --------------- | --------------------------------------------------- |
| `ValidateIssuer = true`    | Enabled         | Checks the `iss` claim in the token                 |
| `ValidIssuer`              | `"NehantWorld"` | Token must have `iss: "NehantWorld"` to be accepted |
| `ValidateAudience = true`  | Enabled         | Checks the `aud` claim in the token                 |
| `ValidAudience`            | `"Local"`       | Token must have `aud: "Local"` to be accepted       |
| `ValidateIssuerSigningKey` | Enabled         | Verifies the token was signed with the correct key  |

---

### 🔑 How Issuer & Audience Are Set in the Token (`LoginController.cs`)

When generating a token, the `LoginController` sets the **Issuer** and **Audience** based on the selected policy:

```csharp
[HttpPost]
public ActionResult Login(LoginDTO model)
{
    string audience = string.Empty;
    string issuer = string.Empty;
    byte[] key = null;

    if (model.Policy == "Local")
    {
        issuer = _configuration.GetValue<string>("LocalIssuer");       // "NehantWorld"
        audience = _configuration.GetValue<string>("LocalAudience");   // "Local"
        key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretForLocal"));
    }
    else if (model.Policy == "Microsoft")
    {
        issuer = _configuration.GetValue<string>("MicrosoftIssuer");   // "MicrosoftAAD"
        audience = _configuration.GetValue<string>("MicrosoftAudience"); // "Microsoft"
        key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretForMicrosoft"));
    }
    else if (model.Policy == "Google")
    {
        issuer = _configuration.GetValue<string>("GoogleIssuer");      // "GoogleAthentication"
        audience = _configuration.GetValue<string>("GoogleAudience");  // "Google"
        key = Encoding.ASCII.GetBytes(_configuration.GetValue<string>("JWTSecretForGoogle"));
    }

    // Token descriptor uses the selected issuer & audience
    var tokenDescriptor = new SecurityTokenDescriptor()
    {
        Issuer = issuer,       // 👈 Sets the ISS claim
        Audience = audience,   // 👈 Sets the AUD claim
        Subject = new ClaimsIdentity(new Claim[]
        {
            new Claim(ClaimTypes.Name, model.Username),
            new Claim(ClaimTypes.Role, "Admin")
        }),
        Expires = DateTime.Now.AddHours(4),
        SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha512Signature)
    };

    var token = tokenHandler.CreateToken(tokenDescriptor);
    // ...
}
```

---

### 🔄 Complete Validation Flow

```
Step 1: User Logs In with Policy = "Local"
──────────────────────────────────────────
LoginController reads from appsettings.json:
  Issuer = "NehantWorld"
  Audience = "Local"
  Key = "LocalThisissecretkey..."

Step 2: Token is Generated
──────────────────────────
JWT Token contains:
  iss: "NehantWorld"     ◀── Who created this token
  aud: "Local"           ◀── Who should use this token
  name: "Kartik"
  role: "Admin"
  exp: <4 hours from now>

Step 3: User Calls Protected Endpoint
──────────────────────────────────────
GET /api/Student/All
Authorization: Bearer eyJhbG...

Step 4: Validation Happens
──────────────────────────
TokenValidationParameters checks:
  ✅ ValidateIssuerSigningKey → Key matches "LocalThisissecretkey..."
  ✅ ValidateIssuer → iss "NehantWorld" matches ValidIssuer
  ✅ ValidateAudience → aud "Local" matches ValidAudience
  ✅ Token not expired

Result: 200 OK ✅
```

---

### ❌ What Happens When Validation Fails?

```
Scenario: Token generated for "Local" but endpoint uses "Microsoft" policy
────────────────────────────────────────────────────────────────────────────

Token has:     iss: "NehantWorld",  aud: "Local"
Endpoint expects: iss: "MicrosoftAAD", aud: "Microsoft"

Validation:
  ❌ ValidateIssuer → "NehantWorld" ≠ "MicrosoftAAD"  → FAIL!
  ❌ ValidateAudience → "Local" ≠ "Microsoft"          → FAIL!

Result: 401 Unauthorized ❌
```

> ⚠️ **Important:** Even if the username, password, and role are correct, the token will be **rejected** if the Issuer or Audience doesn't match. Each policy has its own Issuer, Audience, and Secret Key — they must all align.

---

### 📊 Comparison: With vs Without Issuer & Audience

| Feature                     | Without (ValidateIssuer = false)       | With (ValidateIssuer = true)              |
| --------------------------- | -------------------------------------- | ----------------------------------------- |
| **Security**                | Lower — any issuer accepted            | Higher — only trusted issuers accepted    |
| **Multi-Provider Support**  | ❌ Can't distinguish between providers | ✅ Each provider has its own identity     |
| **Token Misuse Prevention** | ❌ Token from any source works         | ✅ Token must come from expected source   |
| **Enterprise Ready**        | ❌ Not suitable                        | ✅ Supports Azure AD, Google, etc.        |
| **Use Case**                | Simple, single-app testing             | Production, multi-provider authentication |

---

### 💡 Key Points to Remember

1. **Issuer (`iss`)** – Identifies who generated/created the JWT token
2. **Audience (`aud`)** – Identifies who should consume/use the JWT token
3. **Third-Party Scenario** – External providers like Azure AD generate tokens for your app
4. **Local Scenario** – Your single application is the issuer, provider, and consumer all in one
5. **Validation** – Both `ValidateIssuer` and `ValidateAudience` should be `true` in production
6. **Policy Matching** – A token generated for one policy (e.g., Local) won't work on another policy's endpoint (e.g., Microsoft)
7. **Configuration** – Each policy needs its own `Issuer`, `Audience`, and `SecretKey` in `appsettings.json`

> 🔒 **Security Best Practice:** Always enable `ValidateIssuer` and `ValidateAudience` in production environments. Setting them to `false` is only acceptable during early development or testing.

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 30. Common API Response

### 🤔 The Problem: Inconsistent API Responses

Without a standard response format, every endpoint returns data differently. The frontend has no consistent way to check whether a request succeeded or failed:

```
❌ Without Standard Response:

GET /api/student/all    →  [ { "id": 1, "name": "Kartik" } ]     (raw array)
POST /api/student       →  { "id": 2, "name": "Aryan" }          (raw object)
DELETE /api/student/1   →  true                                  (raw boolean)
GET /api/student/999    →  "Student not found"                    (raw string)
Server error            →  { "title": "An error occurred" }       (different shape)
```

> ⚠️ **Problem:** Every endpoint returns a different shape of data. The frontend must write custom parsing logic for every single API call!

---

### ✅ The Solution: Common `APIResponse` Model

Create a **single, standard response wrapper** that every endpoint uses. Every response has the same structure — the frontend always knows what to expect:

```
✅ With Standard APIResponse:

Success Response                          Error Response
┌──────────────────────────┐              ┌──────────────────────────┐
│ {                        │              │ {                        │
│   "status": true,        │              │   "status": false,       │
│   "statusCode": 200,     │              │   "statusCode": 500,     │
│   "data": [...],         │              │   "data": null,          │
│   "errors": null         │              │   "errors": ["error msg"]│
│ }                        │              │ }                        │
└──────────────────────────┘              └──────────────────────────┘
```

> 💡 **Now the frontend simply checks `response.status`** — if `true`, use `response.data`; if `false`, show `response.errors`.

---

### 📦 The `APIResponse` Model

```csharp
using System.Net;

namespace ASPNETCoreWebAPI.Model
{
    public class APIResponse
    {
        public bool Status { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        public dynamic Data { get; set; }
        public List<string> Errors { get; set; }
    }
}
```

| Property     | Type             | Purpose                                                   |
| ------------ | ---------------- | --------------------------------------------------------- |
| `Status`     | `bool`           | `true` = success, `false` = failure                       |
| `StatusCode` | `HttpStatusCode` | HTTP status code (200, 400, 500, etc.)                    |
| `Data`       | `dynamic`        | The actual response data (any type — student, list, bool) |
| `Errors`     | `List<string>`   | List of error messages (populated on failure)             |

> 💡 **`dynamic` type** allows `Data` to hold any kind of data — a single student, a list of students, a boolean, etc. This makes the response truly generic.

---

### 🔧 Using `APIResponse` in `StudentController`

The controller initializes `_apiResponse` once in the constructor and uses it in every action method:

#### ✅ Success Example – Get All Students

```csharp
public async Task<ActionResult<APIResponse>> GetStudentsAsync()
{
    try
    {
        var students = await _studentRepository.GetAllAsync();

        _apiResponse.Data = _mapper.Map<List<StudentDTO>>(students);
        _apiResponse.Status = true;
        _apiResponse.StatusCode = HttpStatusCode.OK;

        return Ok(_apiResponse);
    }
    catch (Exception ex)
    {
        _apiResponse.Errors.Add(ex.Message);
        _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
        _apiResponse.Status = false;
        return _apiResponse;
    }
}
```

**Response:**

```json
{
  "status": true,
  "statusCode": 200,
  "data": [
    { "id": 1, "studentName": "Kartik", "email": "Kartik123@gmail.com" },
    { "id": 2, "studentName": "Aryan", "email": "Aryan123@gmail.com" }
  ],
  "errors": null
}
```

#### ❌ Error Example – Server Exception

If an exception occurs (e.g., database connection fails), the `catch` block fills the error details:

```json
{
  "status": false,
  "statusCode": 500,
  "data": null,
  "errors": ["Cannot open database connection"]
}
```

---

### 📊 Pattern Used Across All Endpoints

Every endpoint in `StudentController` follows the **same try-catch pattern** with `APIResponse`:

| Endpoint       | On Success                   | On Error                     |
| -------------- | ---------------------------- | ---------------------------- |
| `GET /All`     | `Data` = list of students    | `Errors` = exception message |
| `GET /{id}`    | `Data` = single student      | `Errors` = exception message |
| `POST /Create` | `Data` = created student DTO | `Errors` = exception message |
| `PUT /Update`  | `Data` = updated student DTO | `Errors` = exception message |
| `DELETE /{id}` | `Data` = `true`              | `Errors` = exception message |

---

### 💡 Key Points to Remember

1. **Consistency** – Every endpoint returns the same `APIResponse` shape
2. **`Status` flag** – Frontend checks `status: true/false` instead of guessing from data
3. **`dynamic Data`** – Can hold any type of data (object, list, boolean, etc.)
4. **`Errors` list** – Collects all error messages in one place
5. **Try-Catch pattern** – Success sets `Status = true` + `Data`; Catch sets `Status = false` + `Errors`
6. **Return type** – Controller methods return `ActionResult<APIResponse>` instead of raw types

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 31. Role-Based Authentication – Database Setup

### 🤔 What is Role-Based Authentication?

**Role-Based Access Control (RBAC)** is a security mechanism that restricts system access based on a user's assigned role. Instead of granting permissions to each user individually, you assign users to **roles** (like Admin, Customer, Company), and each role has specific **privileges** (like CanRead, CanWrite, CanDelete).

```
┌──────────┐      ┌──────────────┐      ┌───────────────────┐
│   User   │─────▶│ UserRole     │─────▶│      Role         │
│          │      │  Mapping     │      │                   │
│  Kartik  │      │ UserId=1     │      │  Admin            │
│  Aryan   │      │ RoleId=2     │      │  Customer         │
└──────────┘      └──────────────┘      └─────────┬─────────┘
                                                  │
                                                  ▼
                                        ┌───────────────────┐
                                        │  RolePrivilege    │
                                        │                   │
                                        │  CanRead          │
                                        │  CanWrite         │
                                        │  CanDelete        │
                                        └───────────────────┘
```

> 💡 **Why RBAC?** It makes managing access easier — instead of changing permissions for 100 users, just update the role once!

---

### 📁 Project Structure for Role-Based Tables

```
ASPNETCoreWebAPI/
├── Data/
│   ├── User.cs                      ◀── User entity
│   ├── Role.cs                      ◀── Role entity
│   ├── RolePrivilege.cs             ◀── Role privileges entity
│   ├── UserRoleMapping.cs           ◀── User ↔ Role mapping
│   └── Config/
│       ├── UserConfig.cs            ◀── EF config for Users table
│       ├── RoleConfig.cs            ◀── EF config for Roles table
│       ├── RolePrivilegeConfig.cs   ◀── EF config + FK for RolePrivileges
│       └── UserRoleMappingConfig.cs ◀── EF config for UserRoleMappings
```

---

### 1️⃣ Create User Table

The `User` entity stores login credentials and account status.

**`Data/User.cs`**

```csharp
namespace ASPNETCoreWebAPI.Data
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string PasswordSalt { get; set; }
        public int UserType { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
```

**`Data/Config/UserConfig.cs`** – EF Fluent API configuration:

```csharp
public class UserConfig : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.Username).IsRequired();
        builder.Property(x => x.Password).IsRequired();
        builder.Property(x => x.PasswordSalt).IsRequired();
        builder.Property(x => x.UserType).IsRequired();
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatedDate).IsRequired();
    }
}
```

> 🔐 **PasswordSalt** is used to add random data to the password before hashing, making it harder to crack.

**Migration Command:**

```
Add-Migration AddingUserTable -Context CollegeDBContext
Update-Database -Context CollegeDBContext
```

---

### 2️⃣ Create Role Table

The `Role` entity defines different roles like Admin, Customer, Company.

**`Data/Role.cs`**

```csharp
namespace ASPNETCoreWebAPI.Data
{
    public class Role
    {
        public int Id { get; set; }
        public string RoleName { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Navigation property – one Role has many RolePrivileges
        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
    }
}
```

**`Data/Config/RoleConfig.cs`** – EF Fluent API configuration:

```csharp
public class RoleConfig : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.RoleName).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Description);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatedDate).IsRequired();
    }
}
```

> 📌 The `RolePrivileges` navigation property creates a **one-to-many** relationship — one Role can have many privileges.

**Migration Command:**

```
Add-Migration AddingRoleTable -Context CollegeDBContext
Update-Database -Context CollegeDBContext
```

---

### 3️⃣ Create RolePrivilege Table

The `RolePrivilege` entity defines what actions (privileges) each role can perform.

**`Data/RolePrivilege.cs`**

```csharp
namespace ASPNETCoreWebAPI.Data
{
    public class RolePrivilege
    {
        public int Id { get; set; }
        public string RolePrivilegeName { get; set; }
        public string Description { get; set; }
        public int RoleId { get; set; }      // FK to Role table
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        // Navigation property – each RolePrivilege belongs to one Role
        public Role Role { get; set; }
    }
}
```

**Migration Command:**

```
Add-Migration AddingRolePrivilegeTable -Context CollegeDBContext
Update-Database -Context CollegeDBContext
```

---

### 4️⃣ Create RolePrivilege Foreign Key in EF Core

The **Foreign Key** links the `RolePrivilege` table to the `Role` table using EF Fluent API.

**`Data/Config/RolePrivilegeConfig.cs`**

```csharp
public class RolePrivilegeConfig : IEntityTypeConfiguration<RolePrivilege>
{
    public void Configure(EntityTypeBuilder<RolePrivilege> builder)
    {
        builder.ToTable("RolePrivileges");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.RolePrivilegeName).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Description);
        builder.Property(x => x.IsActive).IsRequired();
        builder.Property(x => x.IsDeleted).IsRequired();
        builder.Property(x => x.CreatedDate).IsRequired();

        // 🔑 Foreign Key – RolePrivilege → Role
        builder.HasOne(n => n.Role)
            .WithMany(n => n.RolePrivileges)
            .HasForeignKey(n => n.RoleId)
            .HasConstraintName("FK_RolePrivileges_Roles");
    }
}
```

> 🔑 **How FK Works in EF Core:**
>
> - `HasOne(n => n.Role)` → Each RolePrivilege has **one** Role
> - `.WithMany(n => n.RolePrivileges)` → Each Role has **many** RolePrivileges
> - `.HasForeignKey(n => n.RoleId)` → Uses `RoleId` column as FK
> - `.HasConstraintName(...)` → Names the FK constraint in the database

**Migration Command:**

```
Add-Migration CreatingFKRolePrivilegeRole -Context CollegeDBContext
Update-Database -Context CollegeDBContext
```

---

### 5️⃣ Creating User-Role Mapping in EF

The `UserRoleMapping` entity is a **junction table** that creates the **many-to-many** relationship between `User` and `Role`. A single user can have multiple roles, and a single role can be assigned to multiple users.

```
User (1) ──── (*) UserRoleMapping (*) ──── (1) Role
                    │
                    ├── UserId (FK → Users)
                    └── RoleId (FK → Roles)
```

**`Data/UserRoleMapping.cs`**

```csharp
namespace ASPNETCoreWebAPI.Data
{
    public class UserRoleMapping
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int RoleId { get; set; }

        public User User { get; set; }
        public Role Role { get; set; }
    }
}
```

> 📌 **Navigation properties** (`User` and `Role`) allow EF Core to load related entities using `.Include()`.

**Add navigation property in `User.cs`:**

```csharp
public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; }
```

**Add navigation property in `Role.cs`:**

```csharp
public virtual ICollection<UserRoleMapping> UserRoleMappings { get; set; }
```

**`Data/Config/UserRoleMappingConfig.cs`** – EF Fluent API with **two Foreign Keys** and a **unique index**:

```csharp
public class UserRoleMappingConfig : IEntityTypeConfiguration<UserRoleMapping>
{
    public void Configure(EntityTypeBuilder<UserRoleMapping> builder)
    {
        builder.ToTable("UserRoleMappings");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        // Unique index – a user can have each role only once
        builder.HasIndex(n => new { n.UserId, n.RoleId }, "UK_UserRoleMapping").IsUnique();

        builder.Property(n => n.UserId).IsRequired();
        builder.Property(n => n.RoleId).IsRequired();

        // FK → Role
        builder.HasOne(n => n.Role)
            .WithMany(n => n.UserRoleMappings)
            .HasForeignKey(n => n.RoleId)
            .HasConstraintName("FK_UserRoleMapping_Role");

        // FK → User
        builder.HasOne(n => n.User)
            .WithMany(n => n.UserRoleMappings)
            .HasForeignKey(n => n.UserId)
            .HasConstraintName("FK_UserRoleMapping_User");
    }
}
```

> 🔑 **Key Points:**
>
> - `HasIndex(...).IsUnique()` – Prevents duplicate user-role assignments
> - Two `HasOne().WithMany()` – Creates FK relationships to both `User` and `Role` tables
> - Custom constraint names make the database schema more readable

**Migration Command:**

```
Add-Migration AddingUserRoleMappingAndRelations -Context CollegeDBContext
Update-Database -Context CollegeDBContext
```

---

### 6️⃣ Creating UserType Table in EF

The `UserType` entity categorizes users into groups like Student, Faculty, Supporting Staff, etc. Each `User` belongs to one `UserType`.

```
UserType (1) ──── (*) User
   │
   ├── Student
   ├── Faculty
   ├── Supporting Staff
   └── Parents
```

**`Data/UserType.cs`**

```csharp
namespace ASPNETCoreWebAPI.Data
{
    public class UserType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
```

**Add FK property and navigation property in `User.cs`:**

```csharp
public int UserTypeId { get; set; }
public virtual UserType UserType { get; set; }
```

**`Data/Config/UserTypeConfig.cs`** – EF config with **seed data**:

```csharp
public class UserTypeConfig : IEntityTypeConfiguration<UserType>
{
    public void Configure(EntityTypeBuilder<UserType> builder)
    {
        builder.ToTable("UserTypes");

        builder.HasKey(x => x.Id);
        builder.Property(x => x.Id).UseIdentityColumn();

        builder.Property(x => x.Name).IsRequired().HasMaxLength(250);
        builder.Property(x => x.Description).HasMaxLength(1500);

        // Seed default user types
        builder.HasData(new List<UserType>()
        {
            new UserType { Id = 1, Name = "Student", Description = "For Student" },
            new UserType { Id = 2, Name = "Faculty", Description = "For Faculty" },
            new UserType { Id = 3, Name = "Supporting Staff", Description = "For Supporting Staff" },
            new UserType { Id = 4, Name = "Parents", Description = "For Parents" }
        });
    }
}
```

**FK configuration in `UserConfig.cs`:**

```csharp
builder.HasOne(n => n.UserType)
    .WithMany(n => n.Users)
    .HasForeignKey(n => n.UserTypeId)
    .HasConstraintName("FK_Users_UserType");
```

> 💡 **`HasData()`** seeds the `UserTypes` table with default values during migration — no manual insert needed!

**Migration Commands:**

```
Add-Migration AddingUserTypeTable -Context CollegeDBContext
Add-Migration AddingFK_Users_UserType -Context CollegeDBContext
Update-Database -Context CollegeDBContext
```

---

### 7️⃣ Creating RoleDTO

The `RoleDTO` is a **Data Transfer Object** that exposes only the necessary fields for API clients. It hides internal fields like `IsDeleted`, `CreatedDate`, and `ModifiedDate` from the API response.

```
Role Entity (DB)              RoleDTO (API)
┌─────────────────┐           ┌─────────────────┐
│ Id              │──────────▶│ Id              │
│ RoleName        │──────────▶│ RoleName ✅     │
│ Description     │──────────▶│ Description     │
│ IsActive        │──────────▶│ IsActive ✅     │
│ IsDeleted       │    ✖      │                 │
│ CreatedDate     │    ✖      │                 │
│ ModifiedDate    │    ✖      │                 │
└─────────────────┘           └─────────────────┘
```

**`Model/RoleDTO.cs`**

```csharp
using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreWebAPI.Model
{
    public class RoleDTO
    {
        public int Id { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
```

> 📌 `[Required]` validation ensures `RoleName` and `IsActive` must be provided in the request body.

**AutoMapper mapping in `AutoMapperConfig.cs`:**

```csharp
CreateMap<RoleDTO, Role>().ReverseMap();
```

> 💡 `.ReverseMap()` allows mapping in both directions — `RoleDTO → Role` for create/update and `Role → RoleDTO` for API responses.

---

### 8️⃣ Creating Role Creation Endpoint

The **Create Role** endpoint accepts a `RoleDTO`, maps it to a `Role` entity, saves it to the database, and returns the created role.

**`Controllers/RoleController.cs` – Setup:**

```csharp
[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private readonly IMapper _mapper;
    private readonly ICollegeRepository<Role> _roleRepository;
    private APIResponse _apiResponse;

    public RoleController(IMapper mapper, ICollegeRepository<Role> roleRepository)
    {
        _mapper = mapper;
        _roleRepository = roleRepository;
        _apiResponse = new();
    }
}
```

> 📌 Uses **Generic Repository** (`ICollegeRepository<Role>`) and **AutoMapper** — no direct DB calls needed!

**Create Endpoint:**

```csharp
[HttpPost]
[Route("Create")]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<ActionResult<APIResponse>> CreateRoleAsync(RoleDTO dto)
{
    try
    {
        if (dto == null)
            return BadRequest();

        Role role = _mapper.Map<Role>(dto);
        role.IsDeleted = false;
        role.CreatedDate = DateTime.Now;
        role.ModifiedDate = DateTime.Now;

        var result = await _roleRepository.CreateAsync(role);
        dto.Id = result.Id;
        _apiResponse.Data = dto;
        _apiResponse.Status = true;
        _apiResponse.StatusCode = HttpStatusCode.OK;

        return CreatedAtRoute("GetRoleById", new { id = dto.Id }, _apiResponse);
    }
    catch (Exception ex)
    {
        _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
        _apiResponse.Status = false;
        _apiResponse.Errors.Add(ex.Message);
        return _apiResponse;
    }
}
```

> 🔑 **Key Points:**
>
> - `_mapper.Map<Role>(dto)` – Converts DTO to entity using AutoMapper
> - Sets `IsDeleted = false` and timestamps **manually** (not exposed in DTO)
> - `CreatedAtRoute` returns **201 Created** with the location header pointing to `GetRoleById`

---

### 9️⃣ Creating Role List Endpoint

The **Get All Roles** endpoint fetches all roles from the database and maps them to DTOs.

```csharp
[HttpGet]
[Route("All", Name = "GetAllRoles")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<ActionResult<APIResponse>> GetRolesAsync()
{
    try
    {
        var roles = await _roleRepository.GetAllAsync();

        _apiResponse.Data = _mapper.Map<List<RoleDTO>>(roles);
        _apiResponse.Status = true;
        _apiResponse.StatusCode = HttpStatusCode.OK;

        return Ok(_apiResponse);
    }
    catch (Exception ex)
    {
        _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
        _apiResponse.Status = false;
        _apiResponse.Errors.Add(ex.Message);
        return _apiResponse;
    }
}
```

> 💡 `_mapper.Map<List<RoleDTO>>(roles)` maps the entire collection at once — AutoMapper handles the list mapping automatically.

---

### 🔟 Creating Role Update Endpoint

The **Update Role** endpoint accepts a `RoleDTO`, checks if the role exists, and updates it.

```csharp
[HttpPut]
[Route("Update")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<ActionResult<APIResponse>> UpdateRoleAsync(RoleDTO dto)
{
    try
    {
        if (dto == null || dto.Id <= 0)
            return BadRequest();

        var existingRole = await _roleRepository.GetAsync(role => role.Id == dto.Id, true);

        if (existingRole == null)
            return BadRequest($"Role not found with id: {dto.Id} to update");

        var newRole = _mapper.Map<Role>(dto);

        await _roleRepository.UpdateAsync(newRole);

        _apiResponse.Status = true;
        _apiResponse.StatusCode = HttpStatusCode.OK;
        _apiResponse.Data = newRole;

        return Ok(_apiResponse);
    }
    catch (Exception ex)
    {
        _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
        _apiResponse.Status = false;
        _apiResponse.Errors.Add(ex.Message);
        return _apiResponse;
    }
}
```

> 🔑 **Key Points:**
>
> - First validates `dto.Id > 0` — prevents invalid update requests
> - `GetAsync(..., true)` with tracking enabled — EF tracks the entity for update
> - Maps DTO → Entity → Saves via the generic repository

---

### 1️⃣1️⃣ Creating Role Delete Endpoint

The **Delete Role** endpoint takes an `id`, finds the role, and deletes it.

```csharp
[HttpDelete]
[Route("Delete/{id:int}", Name = "DeleteRoleById")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<ActionResult<APIResponse>> DeleteRoleAsync(int id)
{
    try
    {
        if (id <= 0)
            return BadRequest();

        var role = await _roleRepository.GetAsync(role => role.Id == id);

        if (role == null)
            return NotFound($"Role not found with id: {id} to delete");

        await _roleRepository.DeleteAsync(role);
        _apiResponse.Status = true;
        _apiResponse.StatusCode = HttpStatusCode.OK;
        _apiResponse.Data = true;

        return Ok(_apiResponse);
    }
    catch (Exception ex)
    {
        _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
        _apiResponse.Status = false;
        _apiResponse.Errors.Add(ex.Message);
        return _apiResponse;
    }
}
```

> 📌 Returns `NotFound()` (404) if the role doesn't exist — follows REST best practices for delete operations.

---

### 📊 Role API Endpoints Summary

| Endpoint                       | Method | Description       |
| ------------------------------ | ------ | ----------------- |
| `POST /api/role/Create`        | POST   | Create a new role |
| `GET /api/role/All`            | GET    | Get all roles     |
| `GET /api/role/{id}`           | GET    | Get role by ID    |
| `GET /api/role/{name}`         | GET    | Get role by name  |
| `PUT /api/role/Update`         | PUT    | Update a role     |
| `DELETE /api/role/Delete/{id}` | DELETE | Delete a role     |

---

### 🗄️ Database Tables After Migration

| Table                | Purpose                                    |
| -------------------- | ------------------------------------------ |
| **Users**            | Stores user credentials & status           |
| **UserTypes**        | Categorizes users (Student, Faculty, etc.) |
| **Roles**            | Defines roles (Admin, Customer)            |
| **RolePrivileges**   | Permissions assigned to each role          |
| **UserRoleMappings** | Links users to their assigned roles (M:N)  |

---

### 🎯 Key Takeaways

1. **User table** – Stores username, hashed password with salt, and account status
2. **Role table** – Defines roles with navigation properties to RolePrivileges and UserRoleMappings
3. **RolePrivilege table** – Stores per-role permissions with a FK to Role
4. **UserRoleMapping table** – Junction table linking Users ↔ Roles with a unique index
5. **UserType table** – Categorizes users with seed data using `HasData()`
6. **RoleDTO** – Exposes only necessary fields; uses `[Required]` validation
7. **AutoMapper** – `CreateMap<RoleDTO, Role>().ReverseMap()` for bidirectional mapping
8. **CRUD Endpoints** – Create, Read (All/ById/ByName), Update, Delete using Generic Repository
9. **Foreign Key in EF** – Use `HasOne().WithMany().HasForeignKey()` in Fluent API
10. **Separate migrations** – Create each table in its own migration for clean history
11. **Use `-Context`** – Specify the DbContext when multiple contexts exist
12. **RolePrivilege CRUD** – Full CRUD for permissions using the same Generic Repository pattern
13. **Filter by RoleId** – `GetAllByFilterAsync()` with a lambda to fetch privileges per role

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 32. Role Privilege CRUD – Managing Permissions per Role

After creating the **Role** table and its CRUD endpoints, the next step in a role-based authentication system is to manage **Role Privileges** — the specific permissions that each role is allowed to perform. For example, an "Admin" role might have privileges like "Create User", "Delete User", and "View Reports", while a "Customer" role might only have "View Orders" and "Edit Profile".

Role Privileges answer the question: **"What is this role allowed to do?"**

> 💡 **Why separate privileges from roles?**
>
> Storing permissions in a separate `RolePrivilege` table (instead of hard-coding them into the role) makes the system **flexible and maintainable**. You can add, remove, or update permissions at runtime without redeploying the application.

```
┌──────────────┐        1 : N        ┌───────────────────┐
│    Role      │ ◀──────────────────▶ │  RolePrivilege    │
│              │                      │                   │
│  Id          │                      │  Id               │
│  RoleName    │                      │  RolePrivilegeName│
│  Description │                      │  Description      │
│  IsActive    │                      │  RoleId (FK)      │
└──────────────┘                      │  IsActive         │
                                      │  IsDeleted        │
                                      │  CreatedDate      │
                                      │  ModifiedDate     │
                                      └───────────────────┘
```

---

### 3️⃣2️⃣.1️⃣ RolePrivilege Entity & DTO

The **RolePrivilege** entity maps directly to the `RolePrivileges` database table and contains a **foreign key** (`RoleId`) pointing to the `Role` table.

**RolePrivilege.cs (Entity):**

```csharp
namespace ASPNETCoreWebAPI.Data
{
    public class RolePrivilege
    {
        public int Id { get; set; }
        public string RolePrivilegeName { get; set; }
        public string Description { get; set; }
        public int RoleId { get; set; }       // FK → Role table
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }

        public Role Role { get; set; }        // Navigation property
    }
}
```

**RolePrivilegeDTO.cs (Data Transfer Object):**

```csharp
using System.ComponentModel.DataAnnotations;

namespace ASPNETCoreWebAPI.Model
{
    public class RolePrivilegeDTO
    {
        public int Id { get; set; }
        [Required]
        public string RolePrivilegeName { get; set; }
        public string Description { get; set; }
        public int RoleId { get; set; }
        [Required]
        public bool IsActive { get; set; }
    }
}
```

> 🔑 Notice the DTO **hides** `IsDeleted`, `CreatedDate`, and `ModifiedDate` from API consumers — these are managed internally by the server.

---

### 3️⃣2️⃣.2️⃣ Creating RolePrivilege Endpoint

The **Create** endpoint accepts a `RolePrivilegeDTO`, maps it to the entity, sets audit fields (`CreatedDate`, `ModifiedDate`, `IsDeleted`), and saves it via the Generic Repository.

```csharp
[HttpPost]
[Route("Create")]
[ProducesResponseType(StatusCodes.Status201Created)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<ActionResult<APIResponse>> CreateRolePrivilegeAsync(RolePrivilegeDTO dto)
{
    try
    {
        if (dto == null)
            return BadRequest();

        RolePrivilege rolePrivilege = _mapper.Map<RolePrivilege>(dto);
        rolePrivilege.IsDeleted = false;
        rolePrivilege.CreatedDate = DateTime.Now;
        rolePrivilege.ModifiedDate = DateTime.Now;

        var result = await _rolePrivilegeRepository.CreateAsync(rolePrivilege);
        dto.Id = result.Id;
        _apiResponse.Data = dto;
        _apiResponse.Status = true;
        _apiResponse.StatusCode = HttpStatusCode.OK;

        return Ok(_apiResponse);
    }
    catch (Exception ex)
    {
        _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
        _apiResponse.Status = false;
        _apiResponse.Errors.Add(ex.Message);
        return _apiResponse;
    }
}
```

> 📌 The server **auto-sets** `IsDeleted = false`, `CreatedDate`, and `ModifiedDate` — the client never controls these fields.

---

### 3️⃣2️⃣.3️⃣ Getting All RolePrivileges

This endpoint returns **every** role privilege record from the database.

```csharp
[HttpGet]
[Route("All", Name = "GetAllRolePrivileges")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<ActionResult<APIResponse>> GetRolesAsync()
{
    try
    {
        var roles = await _rolePrivilegeRepository.GetAllAsync();

        _apiResponse.Data = _mapper.Map<List<RolePrivilegeDTO>>(roles);
        _apiResponse.Status = true;
        _apiResponse.StatusCode = HttpStatusCode.OK;

        return Ok(_apiResponse);
    }
    catch (Exception ex)
    {
        _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
        _apiResponse.Status = false;
        _apiResponse.Errors.Add(ex.Message);
        return _apiResponse;
    }
}
```

---

### 3️⃣2️⃣.4️⃣ Getting RolePrivileges by RoleId

This is one of the most **important** endpoints in a role-based system. Instead of fetching all privileges, it filters by `RoleId` — so you can answer: _"What permissions does Role X have?"_

It uses the Generic Repository's `GetAllByFilterAsync()` method and passes a lambda expression to filter records where `RoleId` matches the given value.

```csharp
[HttpGet]
[Route("AllRolePrivilegesById", Name = "GetAllRolePrivilegesById")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<ActionResult<APIResponse>> GetRoleRolePrivilegesByIdAsync(int roleId)
{
    try
    {
        var rolePrivileges = await _rolePrivilegeRepository
            .GetAllByFilterAsync(rp => rp.RoleId == roleId);

        _apiResponse.Data = _mapper.Map<List<RolePrivilegeDTO>>(rolePrivileges);
        _apiResponse.Status = true;
        _apiResponse.StatusCode = HttpStatusCode.OK;

        return Ok(_apiResponse);
    }
    catch (Exception ex)
    {
        _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
        _apiResponse.Status = false;
        _apiResponse.Errors.Add(ex.Message);
        return _apiResponse;
    }
}
```

> 🔑 **Key Insight:** `GetAllByFilterAsync(rp => rp.RoleId == roleId)` translates to SQL: `SELECT * FROM RolePrivileges WHERE RoleId = @roleId`. This is how EF Core converts LINQ lambda expressions into efficient database queries.

---

### 3️⃣2️⃣.5️⃣ Getting RolePrivilege by Id & Name

**Get by ID** — fetch a single privilege by its primary key:

```csharp
[HttpGet]
[Route("{id:int}", Name = "GetRolePrivilegeById")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<ActionResult<APIResponse>> GetRolesAsync(int id)
{
    try
    {
        if (id <= 0)
            return BadRequest();

        var role = await _rolePrivilegeRepository.GetAsync(role => role.Id == id);
        if (role == null)
            return NotFound($"The role privilege with id {id} not found!.");

        _apiResponse.Data = _mapper.Map<RolePrivilegeDTO>(role);
        _apiResponse.Status = true;
        _apiResponse.StatusCode = HttpStatusCode.OK;

        return Ok(_apiResponse);
    }
    catch (Exception ex)
    {
        _apiResponse.Errors.Add(ex.Message);
        _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
        _apiResponse.Status = false;
        return _apiResponse;
    }
}
```

**Get by Name** — search for a privilege using a partial name match:

```csharp
[HttpGet]
[Route("{name:alpha}", Name = "GetRolePrivilegeByName")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
public async Task<ActionResult<APIResponse>> GetRolesAsync(string name)
{
    try
    {
        if (string.IsNullOrEmpty(name))
            return BadRequest();

        var rolePrivilege = await _rolePrivilegeRepository
            .GetAsync(rp => rp.RolePrivilegeName.ToLower().Contains(name.ToLower()));

        if (rolePrivilege == null)
            return NotFound($"The role privilege not found with name: {name}");

        _apiResponse.Data = _mapper.Map<RolePrivilegeDTO>(rolePrivilege);
        _apiResponse.Status = true;
        _apiResponse.StatusCode = HttpStatusCode.OK;

        return Ok(_apiResponse);
    }
    catch (Exception ex)
    {
        _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
        _apiResponse.Status = false;
        _apiResponse.Errors.Add(ex.Message);
        return _apiResponse;
    }
}
```

> 💡 The `{name:alpha}` route constraint ensures ASP.NET Core only matches alphabetic strings, avoiding conflicts with the `{id:int}` route.

---

### 3️⃣2️⃣.6️⃣ Updating a RolePrivilege

The **Update** endpoint finds the existing record (with tracking enabled), maps the new values, and saves.

```csharp
[HttpPut]
[Route("Update")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status400BadRequest)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<ActionResult<APIResponse>> UpdateRoleAsync(RolePrivilegeDTO dto)
{
    try
    {
        if (dto == null || dto.Id <= 0)
            return BadRequest();

        var existingRolePrivilege = await _rolePrivilegeRepository
            .GetAsync(rp => rp.Id == dto.Id, true);

        if (existingRolePrivilege == null)
            return BadRequest($"Role privilege not found with id: {dto.Id} to update");

        var newRolePrivilege = _mapper.Map<RolePrivilege>(dto);

        await _rolePrivilegeRepository.UpdateAsync(newRolePrivilege);

        _apiResponse.Status = true;
        _apiResponse.StatusCode = HttpStatusCode.OK;
        _apiResponse.Data = newRolePrivilege;

        return Ok(_apiResponse);
    }
    catch (Exception ex)
    {
        _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
        _apiResponse.Status = false;
        _apiResponse.Errors.Add(ex.Message);
        return _apiResponse;
    }
}
```

> 🔑 `GetAsync(..., true)` passes `true` for the tracking parameter so EF Core detaches the old entity before attaching the updated one — prevents "entity with same key already tracked" errors.

---

### 3️⃣2️⃣.7️⃣ Deleting a RolePrivilege

The **Delete** endpoint validates the ID, looks up the record, returns 404 if not found, and then removes it.

```csharp
[HttpDelete]
[Route("Delete/{id:int}", Name = "DeleteRolePrivilegeById")]
[ProducesResponseType(StatusCodes.Status200OK)]
[ProducesResponseType(StatusCodes.Status404NotFound)]
[ProducesResponseType(StatusCodes.Status500InternalServerError)]
public async Task<ActionResult<APIResponse>> DeleteRoleAsync(int id)
{
    try
    {
        if (id <= 0)
            return BadRequest();

        var rolePrivilege = await _rolePrivilegeRepository
            .GetAsync(rp => rp.Id == id);

        if (rolePrivilege == null)
            return NotFound($"Role privilege not found with id: {id} to delete");

        await _rolePrivilegeRepository.DeleteAsync(rolePrivilege);
        _apiResponse.Status = true;
        _apiResponse.StatusCode = HttpStatusCode.OK;
        _apiResponse.Data = true;

        return Ok(_apiResponse);
    }
    catch (Exception ex)
    {
        _apiResponse.StatusCode = HttpStatusCode.InternalServerError;
        _apiResponse.Status = false;
        _apiResponse.Errors.Add(ex.Message);
        return _apiResponse;
    }
}
```

---

### 📊 RolePrivilege API Endpoints Summary

| Endpoint                                                | Method | Description                            |
| ------------------------------------------------------- | ------ | -------------------------------------- |
| `POST /api/roleprivilege/Create`                        | POST   | Create a new role privilege            |
| `GET /api/roleprivilege/All`                            | GET    | Get all role privileges                |
| `GET /api/roleprivilege/AllRolePrivilegesById?roleId=1` | GET    | Get all privileges for a specific role |
| `GET /api/roleprivilege/{id}`                           | GET    | Get role privilege by ID               |
| `GET /api/roleprivilege/{name}`                         | GET    | Get role privilege by name             |
| `PUT /api/roleprivilege/Update`                         | PUT    | Update a role privilege                |
| `DELETE /api/roleprivilege/Delete/{id}`                 | DELETE | Delete a role privilege                |

---

### 🎯 Key Takeaways

1. **RolePrivilege entity** has a FK (`RoleId`) to the `Role` table — one role can have many privileges
2. **RolePrivilegeDTO** hides internal fields (`IsDeleted`, `CreatedDate`, `ModifiedDate`) from consumers
3. **GetAllByFilterAsync** with a lambda (`rp => rp.RoleId == roleId`) lets you query privileges per role
4. **Same Generic Repository** — `ICollegeRepository<RolePrivilege>` reuses the existing pattern, no new repository needed
5. **Route constraints** (`{id:int}`, `{name:alpha}`) prevent routing conflicts between Id-based and Name-based GET endpoints
6. **Audit fields** — `CreatedDate` and `ModifiedDate` are set server-side, never exposed to clients

⬆️ [Back to Table of Contents](#-table-of-contents)

---

## 🎉 Conclusion

You've learned:

- ✅ How Web APIs evolved from SOAP to REST
- ✅ The difference between traditional apps and Web APIs
- ✅ Why Web APIs are essential for modern development
- ✅ HTTP request/response structure
- ✅ HTTP verbs and their purposes
- ✅ How to create API endpoints in ASP.NET Core
- ✅ Why and how to use DTOs in Web APIs
- ✅ How to use `[HttpPost]` to create new resources
- ✅ How `CreatedAtRoute` provides proper REST responses
- ✅ How to use `[HttpPut]` to update existing resources
- ✅ How to use `[HttpPatch]` for efficient partial updates
- ✅ PUT vs PATCH comparison and when to use each
- ✅ Model validation to prevent invalid data
- ✅ Built-in validation attributes (`[Required]`, `[EmailAddress]`, `[Range]`, etc.)
- ✅ Creating custom validation attributes for business rules
- ✅ Dependency Injection for loose coupling and maintainability
- ✅ Built-in logger and log levels in Web API
- ✅ Serilog for advanced structured logging with file output
- ✅ Entity Framework Core for database operations with Code First approach
- ✅ Entity Framework Database First approach to scaffold existing databases
- ✅ AutoMapper for simplifying object mapping between entities and DTOs
- ✅ Repository Design Pattern for abstracting data access layer
- ✅ Generic Repository Pattern for reusable CRUD operations across all tables
- ✅ Security stages in Web API (CORS → Authentication → Authorization)
- ✅ CORS concepts and same-origin vs cross-origin understanding
- ✅ CORS scenarios (Simple Request, Preflight Request, Credentials)
- ✅ Multiple ways to enable CORS in ASP.NET Core Web API
- ✅ JWT (JSON Web Tokens) for secure API authentication
- ✅ JWT structure (Header, Payload, Signature) and token generation process
- ✅ JWT algorithms and how to configure JWT in ASP.NET Core
- ✅ Protecting controllers with `[Authorize]` attribute
- ✅ Generating JWT tokens with `JwtSecurityTokenHandler` and claims
- ✅ Complete frontend-backend JWT authentication flow (React + Web API)
- ✅ Multiple JWT authentication policies (Default vs Named)
- ✅ Understanding 401 (Unauthorized) vs 403 (Forbidden) errors
- ✅ JWT Authentication in Swagger UI with Authorize button
- ✅ Why Issuer (iss) and Audience (aud) claims are essential in JWT
- ✅ Third-Party vs Local Issuer & Audience scenarios
- ✅ Common API Response pattern for consistent, standard responses
- ✅ Role-Based Authentication database setup (User, Role, RolePrivilege tables)
- ✅ Foreign Key configuration in EF Core using Fluent API
- ✅ User-Role Mapping with junction table and unique index in EF Core
- ✅ UserType table with seed data for user categorization
- ✅ RoleDTO for secure, validated data transfer with AutoMapper
- ✅ Full CRUD operations for Role management (Create, List, Update, Delete)
- ✅ Role Privilege CRUD for managing per-role permissions
- ✅ Filtering RolePrivileges by RoleId using `GetAllByFilterAsync()` with lambda expressions

**Happy Coding!** 🚀

---

## 📚 Resources

- [ASP.NET Core Web API Documentation](https://docs.microsoft.com/en-us/aspnet/core/web-api/)
- [REST API Best Practices](https://restfulapi.net/)
- [HTTP Status Codes](https://httpstatuses.com/)
