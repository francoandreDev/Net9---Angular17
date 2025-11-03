# .NET CLI Commands Guide

A quick reference guide for using the **dotnet CLI** in terminal/command prompt.

---

## 📦 Project & Solution Management

### Create a new project

```bash
dotnet new <TEMPLATE> -n <PROJECT_NAME>
````

**Keywords:** `new`, `project`, `template`

* `<TEMPLATE>` examples: `console`, `classlib`, `webapi`, `mvc`, `blazor`, `worker`
* `<PROJECT_NAME>`: Name of the project
* Creates a new project in a folder with the same name.

**Example:**

```bash
dotnet new console -n MyApp
```

---

### Create a new solution

```bash
dotnet new sln -n <SOLUTION_NAME>
```

**Keywords:** `solution`, `sln`, `new`

* Generates a solution file (`.sln`) to organize multiple projects.

---

### Add project to solution

```bash
dotnet sln <SOLUTION_FILE> add <PROJECT_FILE>
```

**Keywords:** `sln`, `add`, `project`

* Links an existing project to a solution.

---

### Remove project from solution

```bash
dotnet sln <SOLUTION_FILE> remove <PROJECT_FILE>
```

**Keywords:** `remove`, `project`, `sln`

---

## 🔧 Build & Run

### Build a project or solution

```bash
dotnet build <PROJECT_OR_SOLUTION>
```

**Keywords:** `build`, `compile`

* Compiles the code and generates binaries.
* Optional flags: `-c Release` (for release configuration)

---

### Run a project

```bash
dotnet run --project <PROJECT_FILE>
```

**Keywords:** `run`, `execute`, `project`

* Compiles (if needed) and runs a project.
* Pass runtime arguments after `--`:

```bash
dotnet run --project MyApp -- arg1 arg2
```

---

### Clean build outputs

```bash
dotnet clean <PROJECT_OR_SOLUTION>
```

**Keywords:** `clean`, `remove`, `bin`, `obj`

* Deletes all build artifacts (`bin/` and `obj/` folders).

---

## 📦 Package Management

### Add NuGet package

```bash
dotnet add <PROJECT_FILE> package <PACKAGE_NAME> --version <VERSION>
```

**Keywords:** `add`, `package`, `nuget`

* Installs a NuGet package and updates the project file.

**Example:**

```bash
dotnet add MyApp.csproj package Newtonsoft.Json --version 13.0.3
```

---

### Remove NuGet package

```bash
dotnet remove <PROJECT_FILE> package <PACKAGE_NAME>
```

**Keywords:** `remove`, `package`, `nuget`

---

### List installed packages

```bash
dotnet list <PROJECT_FILE> package
```

**Keywords:** `list`, `package`, `nuget`

* Shows all NuGet dependencies in the project.

---

### Restore packages

```bash
dotnet restore <PROJECT_OR_SOLUTION>
```

**Keywords:** `restore`, `dependencies`, `nuget`

* Downloads all NuGet dependencies for a project or solution.
* Usually run automatically before `build` or `run`.

---

## 🧪 Testing

### Run unit tests

```bash
dotnet test <PROJECT_OR_SOLUTION>
```

**Keywords:** `test`, `unit`, `xunit`, `nunit`, `mstest`

* Builds and executes tests in test projects.
* Optional flags: `-c Release`, `--filter "Category=Integration"`

---

## 📄 Publishing

### Publish a project

```bash
dotnet publish <PROJECT_FILE> -c Release -o <OUTPUT_DIR>
```

**Keywords:** `publish`, `deploy`, `release`

* Compiles the project and prepares it for deployment.
* Produces ready-to-run binaries in `<OUTPUT_DIR>`.

---

### Run a self-contained publish

```bash
dotnet publish -r <RUNTIME_IDENTIFIER> -c Release --self-contained true
```

**Keywords:** `self-contained`, `runtime`, `RID`

* Example Runtime Identifiers: `win-x64`, `linux-x64`, `osx-arm64`
* Includes the .NET runtime with the app (no installation needed).

---

## ℹ️ Information & Help

### Show installed .NET SDKs

```bash
dotnet --list-sdks
```

**Keywords:** `sdk`, `list`, `version`

### Show installed .NET runtimes

```bash
dotnet --list-runtimes
```

**Keywords:** `runtime`, `list`, `version`

### Show project information

```bash
dotnet --info
```

**Keywords:** `info`, `environment`

* Displays SDK, runtime, OS, and global.json settings.

### Show help

```bash
dotnet --help
dotnet <command> --help
```

**Keywords:** `help`, `usage`, `documentation`

* Provides usage details for the CLI or a specific command.

---

## 🔑 Tips & Keywords

* **CLI entry:** `dotnet <command> [options]`
* **Project file:** `.csproj`
* **Solution file:** `.sln`
* **Configurations:** `Debug` (default), `Release`
* **NuGet commands:** `add`, `remove`, `list`, `restore`
* **Targets:** `build`, `run`, `test`, `publish`, `clean`
