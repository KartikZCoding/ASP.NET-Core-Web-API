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
namespace CollegeApp.Model
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
namespace CollegeApp.Model
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
using CollegeApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
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
namespace CollegeApp.Model
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
using CollegeApp.Model;
using Microsoft.AspNetCore.Mvc;

namespace CollegeApp.Controllers
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

namespace CollegeApp.Model
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

namespace CollegeApp.Validators
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
using CollegeApp.Validators;  // ◀── Import custom validators

namespace CollegeApp.Model
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

**Happy Coding!** 🚀

---

## 📚 Resources

- [ASP.NET Core Web API Documentation](https://docs.microsoft.com/en-us/aspnet/core/web-api/)
- [REST API Best Practices](https://restfulapi.net/)
- [HTTP Status Codes](https://httpstatuses.com/)
