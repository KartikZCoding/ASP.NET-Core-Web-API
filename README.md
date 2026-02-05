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
20. [AutoMapper – Simplifying Object Mapping](#20-automapper--simplifying-object-mapping)

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
- ✅ AutoMapper for simplifying object mapping between entities and DTOs

**Happy Coding!** 🚀

---

## 📚 Resources

- [ASP.NET Core Web API Documentation](https://docs.microsoft.com/en-us/aspnet/core/web-api/)
- [REST API Best Practices](https://restfulapi.net/)
- [HTTP Status Codes](https://httpstatuses.com/)
