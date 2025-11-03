# 🧠 C# & .NET 9 Complete Practical Guide

This guide covers **C# fundamentals, OOP, modern features, .NET 9 ecosystem, and advanced concepts** including C# 13 upcoming features. It is structured with theory, examples, and exercises.

---

## 📘 Introduction

This guide provides the necessary **C# knowledge** to work with **.NET 9**.
It is organized by levels, from basics to advanced topics, including examples and exercises.

---

## 🧱 Level 1 – Language Fundamentals

### 🔹 Data Types & Variables

- Primitives: `int`, `float`, `double`, `decimal`, `bool`, `char`
- Text: `string`
- Type inference: `var`
- Constants: `const`
- Type conversion: `Convert`, `int.Parse()`, `ToString()`

**Example:**

```csharp
int age = 25;
string name = "Franco";
var active = true;
Console.WriteLine($"{name} is {age} years old. Active? {active}");
````

**Exercise:**
Declare three variables of different types, change their values, and print them.

---

### 🔹 Operators

- Arithmetic: `+`, `-`, `*`, `/`, `%`
- Comparison: `==`, `!=`, `<`, `>`, `<=`, `>=`
- Logical: `&&`, `||`, `!`
- Assignment: `=`, `+=`, `-=`, etc.
- Ternary: `condition ? value1 : value2`

**Example:**

```csharp
int x = 10, y = 20;
var max = (x > y) ? x : y;
Console.WriteLine($"Max: {max}");
```

---

### 🔹 Control Flow

- `if`, `else if`, `else`
- `switch` and `switch expression`
- `for`, `while`, `do while`, `foreach`

**Example:**

```csharp
for (int i = 0; i < 5; i++)
    Console.WriteLine($"Iteration {i}");
```

**Exercise:**
Create a console menu that repeats until the user selects "Exit".

---

### 🔹 Methods

- Definition, parameters, return values
- Optional parameters, `ref`, `out`

**Example:**

```csharp
static int Add(int a, int b) => a + b;
Console.WriteLine(Add(3, 4));
```

**Exercise:**
Create a method that receives a number and returns whether it is prime.

---

### 🔹 Arrays & Collections

- Arrays (`int[]`)
- `List<T>`
- `Dictionary<K,V>`

**Example:**

```csharp
List<string> names = new() { "Ana", "Luis", "Carlos" };
foreach (var n in names)
    Console.WriteLine(n);
```

---

## 🧩 Level 2 – Object-Oriented Programming (OOP)

### 🔹 Classes & Objects

**Example:**

```csharp
class Person {
    public string Name { get; set; }
    public int Age { get; set; }
    public void Greet() => Console.WriteLine($"Hi, I'm {Name}");
}
```

**Exercise:**
Create a `Product` class with properties and a method that calculates 18% tax.

---

### 🔹 Encapsulation & Properties

- Auto-properties (`get; set;`)
- Private fields

---

### 🔹 Inheritance & Polymorphism

**Example:**

```csharp
class Animal { public virtual void MakeSound() => Console.WriteLine("Generic sound"); }
class Dog : Animal { public override void MakeSound() => Console.WriteLine("Woof!"); }
```

---

### 🔹 Interfaces & Abstract Classes

- `interface IAnimal`
- `abstract class Animal`

**Exercise:**
Define `IVehicle` interface with `Drive()` and implement in `Car` and `Motorbike`.

---

## ⚙️ Level 3 – Modern C# Features

### 🔹 Lambda Expressions & LINQ

```csharp
var numbers = new List<int> { 1, 2, 3, 4, 5 };
var evens = numbers.Where(n => n % 2 == 0);
```

---

### 🔹 Tuples & Deconstruction

```csharp
(string, int) Person() => ("Franco", 25);
var (name, age) = Person();
```

---

### 🔹 Pattern Matching

```csharp
object obj = 10;
if (obj is int number && number > 5)
    Console.WriteLine("Greater than 5");
```

---

### 🔹 Records (C# 9)

```csharp
public record User(string Name, int Age);
```

---

### 🔹 Init-only Properties

```csharp
public class Person {
    public string Name { get; init; }
}
```

---

### 🔹 Async / Await

```csharp
static async Task DownloadAsync() {
    await Task.Delay(1000);
    Console.WriteLine("Download complete");
}
```

---

## 🌐 Level 4 – .NET 9 Ecosystem

### 🔹 dotnet CLI Basics

```bash
dotnet new console -n MyApp
dotnet run
dotnet add package Newtonsoft.Json
```

---

### 🔹 Namespaces & `using`

```csharp
using System;
using System.Collections.Generic;
```

---

### 🔹 File Handling

```csharp
File.WriteAllText("data.txt", "Hello .NET 9");
string text = File.ReadAllText("data.txt");
```

---

### 🔹 JSON

```csharp
using System.Text.Json;

var person = new { Name = "Ana", Age = 30 };
string json = JsonSerializer.Serialize(person);
```

---

## 🚀 Final Project: Console Mini App

**Goal:** Create a console application that:

1. Manages a list of users/products.
2. Supports add, list, remove, and save in JSON.
3. Uses classes, LINQ, and file handling.

---

## ⚡ Level 5 – Advanced C# Concepts

### 🔹 Access Modifiers

- `public`, `private`, `protected`, `internal`, `protected internal`, `private protected`

---

### 🔹 Interface vs Abstract vs Virtual Members

```csharp
interface IFlyable { void Fly(); }
abstract class Animal {
    public abstract void Speak();
    public virtual void Sleep() => Console.WriteLine("Sleeping...");
}
class Bird : Animal, IFlyable {
    public override void Speak() => Console.WriteLine("Chirp!");
    public void Fly() => Console.WriteLine("Flying!");
}
```

---

### 🔹 Primary Constructors

```csharp
class Person(string name, int age) {
    public string Name { get; } = name;
    public int Age { get; } = age;
}
```

---

### 🔹 Collection Expressions

```csharp
var numbers = [1, 2, 3, 4, 5];
var names = ["Alice", "Bob"];
```

---

### 🔹 Alias Any Type

```csharp
using StringList = List<string>;
StringList names = new() { "Ana", "Luis" };
```

---

### 🔹 Optional Parameters in Lambdas

```csharp
Func<int, int, int> Add = (x = 1, y = 2) => x + y;
Console.WriteLine(Add()); // 3
```

---

### 🔹 Interceptors (Experimental)

```csharp
// Pseudo-syntax
[Intercept(typeof(Logger))]
void DoWork() { ... }
```

---

### 🔹 Params with Collections

```csharp
void PrintNumbers(params List<int>[] lists) {
    foreach (var list in lists)
        Console.WriteLine(string.Join(",", list));
}
```

---

### 🔹 New `lock` Type

```csharp
System.Threading.Lock myLock = new();
lock(myLock) {
    // Thread-safe
}
```

---

### 🔹 Partial Properties & Indexers

```csharp
partial class Data {
    public partial string Name { get; set; }
}
```

---

### 🔹 Implicit Index Access in Initializers

```csharp
var array = new int[5] { 1,2,3,4,5 };
var last = array[^1]; // 5
```

---

### 🔹 Improved `ref struct` Support

```csharp
ref struct SpanWrapper {
    public Span<int> Values;
}
```

---

### 🔹 Pattern Matching Review

```csharp
object obj = 42;
if (obj is int x && x > 10)
    Console.WriteLine("Integer > 10");
```

---

### 🔹 Inline Arrays & LINQ

```csharp
var numbers = [1,2,3,4,5];
var evens = numbers.Where(n => n % 2 == 0).ToList();
```

---

### 🔹 Indexers

```csharp
class Matrix {
    private int[,] data = new int[3,3];
    public int this[int row,int col] {
        get => data[row,col];
        set => data[row,col] = value;
    }
}
var matrix = new Matrix();
matrix[0,1] = 42;
```

---

### 🔹 Unsafe Code

```csharp
unsafe {
    int value = 42;
    int* ptr = &value;
    *ptr = 100;
    Console.WriteLine(value); // 100
}
```

---

### 🔹 XML Documentation

```csharp
/// <summary>
/// Calculates sum of two numbers
/// </summary>
/// <param name="a">First number</param>
/// <param name="b">Second number</param>
/// <returns>Sum of a and b</returns>
int Sum(int a, int b) => a + b;
```

---

> 💡 **Tip:** Combine these advanced features in small projects. Experiment with **C# 13 new features** to stay ahead in .NET 9 development.
