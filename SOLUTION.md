# Solution File Documentation

## What is a Solution File?

A `.sln` (solution) file is a Visual Studio/MSBuild file that acts as a container for organizing one or more related projects. It's used by .NET development tools to manage, build, and test multiple projects together.

## Purpose

- **Project Organization**: Groups related projects (web APIs, class libraries, test projects) into a single logical unit
- **Build Coordination**: Manages build order and dependencies between projects
- **IDE Integration**: Provides a single entry point for opening multiple projects in Visual Studio or other IDEs
- **Batch Operations**: Enables running builds, tests, or other operations across all projects at once

## Structure of bnotes.sln

```
bnotes.sln
├── bnotes-web-api          (.NET Web API project)
└── bnotes-web-api.Tests    (xUnit test project)
```

### Key Sections

1. **Project Declarations**: Lists all projects included in the solution with their GUIDs and paths
   ```
   Project("{FAE04EC0-301F-11D3-BF4B-00C04F79EFBC}") = "bnotes-web-api", "bnotes-web-api\bnotes-web-api.csproj", "{GUID}"
   ```

2. **Solution Configurations**: Defines build configurations (Debug/Release)
   ```
   GlobalSection(SolutionConfigurationPlatforms)
       Debug|Any CPU = Debug|Any CPU
       Release|Any CPU = Release|Any CPU
   ```

3. **Project Configurations**: Maps each project to solution configurations and specifies which should be built
   ```
   {GUID}.Debug|Any CPU.ActiveCfg = Debug|Any CPU
   {GUID}.Debug|Any CPU.Build.0 = Debug|Any CPU
   ```

## Common Operations

### Build All Projects
```bash
dotnet build bnotes.sln
```

### Run Tests Across All Test Projects
```bash
dotnet test bnotes.sln
```

### Restore NuGet Packages for All Projects
```bash
dotnet restore bnotes.sln
```

## Why bnotes-ui Was Removed

The `bnotes-ui` project is a JavaScript/React application (`.esproj`) that:
- Uses npm/node tooling, not .NET tooling
- Cannot participate in `dotnet test` (lacks VSTest target)
- Should be built and tested independently with `npm build` and `npm test`

Including it in the .NET solution caused build/test failures because MSBuild tried to run .NET operations on a non-.NET project.

## Best Practices

- **Include only .NET projects** in `.sln` files
- **Separate concerns**: JavaScript/TypeScript projects should use their own build systems
- **Add test projects** to enable `dotnet test` to discover and run tests
- **Keep configurations minimal**: Only include configurations you actually use

## Building and Testing the Entire Project

While the solution file only contains .NET projects, you can build and test the entire bnotes project (both .NET API and React UI) using:

### Using Build Scripts (Recommended for Local Development)

**Linux/macOS:**
```bash
./build.sh
```

**Windows:**
```cmd
build.bat
```

These scripts will:
1. Restore and build the .NET API
2. Run .NET API tests
3. Install dependencies for React UI
4. Run React UI tests
5. Build React UI for production

### Using CI/CD (Automated Testing)

The GitHub Actions workflow (`.github/workflows/ci.yml`) automatically:
- Runs both .NET and UI tests on every push/PR
- Uses separate jobs for API and UI (runs in parallel)
- Ensures both parts of the application are tested before merging

### Manual Build Commands

**Build .NET API and tests:**
```bash
dotnet build bnotes.sln
dotnet test bnotes.sln
```

**Build React UI:**
```bash
cd bnotes-ui
npm ci
npm test -- --watchAll=false
npm run build
```

