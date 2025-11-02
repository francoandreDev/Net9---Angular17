# 🧠 Guía de Práctica — Fundamentos de C# para .NET 9

## 📘 Introducción

Esta guía cubre los fundamentos del lenguaje **C#** necesarios para trabajar con **.NET 9**.
Está estructurada por niveles, con teoría, ejemplos y ejercicios prácticos para afianzar los conceptos.

---

## 🧱 Nivel 1 – Fundamentos del Lenguaje

### 🔹 Tipos de datos y variables

- Primitivos: `int`, `float`, `double`, `decimal`, `bool`, `char`
- Texto: `string`
- Inferencia de tipo: `var`
- Constantes: `const`
- Conversión de tipos: `Convert`, `int.Parse()`, `ToString()`

**Ejemplo:**

```csharp
int edad = 25;
string nombre = "Franco";
var activo = true;
Console.WriteLine($"{nombre} tiene {edad} años. ¿Activo?: {activo}");
````

**Ejercicio:**
Declara tres variables de distinto tipo, cambia sus valores y muéstralos en consola.

---

### 🔹 Operadores

- Aritméticos: `+`, `-`, `*`, `/`, `%`
- Relacionales: `==`, `!=`, `<`, `>`, `<=`, `>=`
- Lógicos: `&&`, `||`, `!`
- Asignación: `=`, `+=`, `-=`, etc.
- Ternario: `condición ? valor1 : valor2`

**Ejemplo:**

```csharp
int x = 10, y = 20;
var mayor = (x > y) ? x : y;
Console.WriteLine($"El mayor es: {mayor}");
```

---

### 🔹 Control de flujo

- `if`, `else if`, `else`
- `switch` y `switch expression`
- `for`, `while`, `do while`, `foreach`

**Ejemplo:**

```csharp
for (int i = 0; i < 5; i++)
    Console.WriteLine($"Iteración {i}");
```

**Ejercicio:**
Crea un menú en consola que repita opciones hasta que el usuario elija “Salir”.

---

### 🔹 Métodos

- Definición, parámetros y retorno
- Parámetros opcionales, `ref`, `out`

**Ejemplo:**

```csharp
static int Sumar(int a, int b) => a + b;

Console.WriteLine(Sumar(3, 4));
```

**Ejercicio:**
Crea un método que reciba un número y devuelva si es primo o no.

---

### 🔹 Arreglos y colecciones

- Arreglos (`int[]`)
- `List<T>`
- `Dictionary<K,V>`

**Ejemplo:**

```csharp
List<string> nombres = new() { "Ana", "Luis", "Carlos" };
foreach (var n in nombres)
    Console.WriteLine(n);
```

---

## 🧩 Nivel 2 – Programación Orientada a Objetos (POO)

### 🔹 Clases y objetos

**Ejemplo:**

```csharp
class Persona {
    public string Nombre { get; set; }
    public int Edad { get; set; }
    public void Saludar() => Console.WriteLine($"Hola, soy {Nombre}");
}
```

**Ejercicio:**
Crea una clase `Producto` con propiedades y un método que calcule el IGV (18%).

---

### 🔹 Encapsulación y propiedades

- Propiedades automáticas (`get; set;`)
- Campos privados

### 🔹 Herencia y polimorfismo

**Ejemplo:**

```csharp
class Animal { public virtual void HacerSonido() => Console.WriteLine("Sonido genérico"); }
class Perro : Animal { public override void HacerSonido() => Console.WriteLine("Guau!"); }
```

---

### 🔹 Interfaces y clases abstractas

- `interface IAnimal`
- `abstract class`

**Ejercicio:**
Define una interfaz `IVehiculo` con un método `Conducir()` e impleméntala en `Auto` y `Moto`.

---

## ⚙️ Nivel 3 – Características modernas de 'C#'

### 🔹 Expresiones lambda y LINQ

```csharp
var numeros = new List<int> { 1, 2, 3, 4, 5 };
var pares = numeros.Where(n => n % 2 == 0);
```

### 🔹 Tuplas y desestructuración

```csharp
(string, int) Persona() => ("Franco", 25);
var (nombre, edad) = Persona();
```

### 🔹 Pattern matching

```csharp
object obj = 10;
if (obj is int numero && numero > 5)
    Console.WriteLine("Mayor a 5");
```

### 🔹 Records (C# 9)

```csharp
public record Usuario(string Nombre, int Edad);
```

### 🔹 Propiedades init-only

```csharp
public class Persona {
    public string Nombre { get; init; }
}
```

### 🔹 Async / Await

```csharp
static async Task DescargarAsync()
{
    await Task.Delay(1000);
    Console.WriteLine("Descarga completa");
}
```

---

## 🌐 Nivel 4 – Ecosistema .NET 9

### 🔹 dotnet CLI básico

```bash
dotnet new console -n MiApp
dotnet run
dotnet add package Newtonsoft.Json
```

### 🔹 Namespaces y `using`

```csharp
using System;
using System.Collections.Generic;
```

### 🔹 Manejo de archivos

```csharp
File.WriteAllText("datos.txt", "Hola .NET 9");
string texto = File.ReadAllText("datos.txt");
```

### 🔹 JSON

```csharp
using System.Text.Json;

var persona = new { Nombre = "Ana", Edad = 30 };
string json = JsonSerializer.Serialize(persona);
```

---

## 🚀 Proyecto Final: Mini App de Consola

**Objetivo:** Crear una aplicación de consola que:

1. Gestione una lista de usuarios/productos.
2. Permita agregar, listar, eliminar y guardar datos en JSON.
3. Use clases, LINQ y manejo de archivos.

---

## 📚 Recursos Recomendados

- [Documentación oficial de C#](https://learn.microsoft.com/dotnet/csharp/)
- [Tutorial de .NET CLI](https://learn.microsoft.com/dotnet/core/tools/)
- [Ejemplos de LINQ](https://learn.microsoft.com/dotnet/csharp/programming-guide/concepts/linq/)

---

> 💡 **Consejo:** practica escribiendo el código desde cero y usa `dotnet run` para probar cada bloque.
> Usa Visual Studio Code con la extensión “C# Dev Kit” para una mejor experiencia.
