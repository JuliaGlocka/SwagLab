# SwagLab Test Automation Framework

A Selenium-based test automation framework for testing the [SauceDemo](https://www.saucedemo.com/) web application using C#, xUnit, and the Page Object Model pattern.

## Technologies Used

- **C# .NET** - Primary programming language
- **Selenium WebDriver** - Web automation framework
- **xUnit** - Testing framework
- **FluentAssertions** - Assertion library
- **log4net** - Logging framework
- **WebDriverManager** - Automatic WebDriver management

## Project Structure

```
SwagLab/
├── PageObject.Test/                    # Main test project
│   ├── Tests/                         # Test classes
│   │   ├── LoginPageTests.cs          # Login functionality tests
│   │   ├── WebDriverFactoryTests.cs   # WebDriver factory tests
│   │   └── xunit.runner.json          # xUnit runner configuration
│   └── log4net.config                 # Logging configuration
└── SwagLab/                           # Page Object library
    └── PageObject/                     # Page Object classes
        ├── LoginPage.cs               # Login page object
        └── WebDriverFactory.cs       # WebDriver factory class
```

## Features

### Cross-Browser Testing
Supports Chrome, Firefox, Microsoft Edge, Safari (macOS only), and Internet Explorer with automatic driver management.

### Test Coverage
- Login with empty credentials validation
- Login with empty password validation  
- Login with valid credentials verification
- Invalid credentials error handling

## Prerequisites

- **.NET 6.0 or higher**
- **Visual Studio 2022** or **Visual Studio Code**

## Setup & Running Tests

1. **Clone and build**
   ```bash
   git clone <repository-url>
   cd SwagLab
   dotnet restore
   dotnet build
   ```

2. **Run tests**
   ```bash
   # Run all tests
   dotnet test
   
   # Run with detailed output
   dotnet test --logger "console;verbosity=detailed"
   ```

## Test Cases

| Test Case | Description |
|-----------|-------------|
| **UC1** | Login with empty credentials - validates required field errors |
| **UC2** | Login with empty password - validates password required error |
| **UC3** | Login with valid credentials - verifies successful authentication |
| **Invalid Login** | Login with wrong credentials - verifies error handling |

## Valid Test Credentials

- **Standard User**: `standard_user` / `secret_sauce`
- **Problem User**: `problem_user` / `secret_sauce`

## Architecture

- **Page Object Model** for maintainable test code
- **WebDriver Factory** with automatic driver management
- **Explicit Waits** for reliable element interactions (10-second timeout)
- **Cross-browser support** via parameterized tests
- **Comprehensive logging** with log4net

## Configuration

Tests run sequentially (non-parallel) as configured in `xunit.runner.json`. Logging is configured via `log4net.config` with console and file output.