# SwagLab Test Automation Framework

A comprehensive Selenium-based test automation framework for testing the [SauceDemo](https://www.saucedemo.com/) web application using C#, xUnit, and the Page Object Model pattern.

## 🛠️ Technologies Used

*   **C# .NET** - Primary programming language
*   **Selenium WebDriver** - Web automation framework
*   **xUnit** - Testing framework
*   **FluentAssertions** - Assertion library for more readable tests
*   **log4net** - Logging framework
*   **WebDriverManager** - Automatic WebDriver management
*   **Page Object Model** - Design pattern for maintainable test code

## 📁 Project Structure

```bash
SwagLab/
├── PageObject.Test/                    # Main test project
│   ├── Dependencies/                   # Project dependencies
│   ├── Tests/                         # Test classes
│   │   ├── LoginPageTests.cs          # Login functionality tests
│   │   ├── WebDriverFactoryTests.cs   # WebDriver factory tests
│   │   └── xunit.runner.json          # xUnit runner configuration
│   └── log4net.config                 # Logging configuration
└── SwagLab/                           # Page Object library
    ├── Dependencies/                   # Library dependencies
    ├── PageObject/                     # Page Object classes
    │   ├── LoginPage.cs               # Login page object
    │   └── WebDriverFactory.cs       # WebDriver factory class
```

## 🚀 Features

### Cross-Browser Testing

*   **Chrome** - Full support with automatic driver management
*   **Firefox** - Full support with automatic driver management
*   **Microsoft Edge** - Full support with automatic driver management
*   **Safari** - macOS only support
*   **Internet Explorer** - Legacy browser support

### Test Coverage

*   **UC1**: Login with empty credentials validation
*   **UC2**: Login with empty password validation
*   **UC3**: Login with valid credentials verification
*   **Invalid credentials** error handling

### Page Object Model Implementation

*   Centralized element locators
*   Reusable page methods
*   Explicit waits for element interactions
*   Clean separation of test logic and page logic

## 📋 Prerequisites

*   **.NET 6.0 or higher**
*   **Visual Studio 2022** or **Visual Studio Code**
*   **Git** (for cloning the repository)

## ⚙️ Setup Instructions

1.  **Clone the repository**
    
    ```bash
    git clone <your-repository-url>
    cd SwagLab
    ```
    
2.  **Restore NuGet packages**
    
    ```bash
    dotnet restore
    ```
    
3.  **Build the solution** `bash dotnet build`

## 🏃‍♂️ Running Tests

### Run All Tests

```bash
dotnet test
```

### Run Tests for Specific Browser

```bash
dotnet test --filter "browser=edge"
```

### Run Specific Test Class

```bash
dotnet test --filter "LoginPageTests"
```

### Run Tests with Detailed Output

```bash
dotnet test --logger "console;verbosity=detailed"
```

## 🧪 Test Cases

### Login Page Tests (`LoginPageTests.cs`)

| Test Case | Description | Expected Result |
| --- | --- | --- |
| **UC1\_LoginWithEmptyCredentials** | Attempt login with empty username and password | Shows "Username is required" error |
| **UC2\_LoginWithEmptyPassword** | Attempt login with username but empty password | Shows "Password is required" error |
| **UC3\_LoginWithValidCredentials** | Login with valid credentials | Successfully redirects to dashboard |
| **Login\_WithInvalidCredentials** | Login with invalid username/password | Shows authentication error message |

### WebDriver Factory Tests (`WebDriverFactoryTests.cs`)

| Test Case | Description | Expected Result |
| --- | --- | --- |
| **CreateDriver\_WithSupportedBrowser** | Create driver for Chrome, Firefox, Edge | Returns appropriate WebDriver instance |
| **CreateDriver\_WithUnsupportedBrowser** | Attempt to create driver for unsupported browser | Throws ArgumentException |
| **CreateDriver**_**Safari**_**OnNonMac** | Create Safari driver on non-macOS | Throws PlatformNotSupportedException |

## 📊 Test Data

### Valid User Credentials

*   **Standard User**: `standard_user` / `secret_sauce`
*   **Problem User**: `problem_user` / `secret_sauce`

### Supported Browsers

*   `chrome`
*   `firefox`
*   `edge`
*   `safari` (macOS only)
*   `internet explorer`

## 📝 Logging

The framework uses **log4net** for comprehensive logging:

*   Test execution start/end
*   Browser initialization details
*   User actions and inputs
*   Error messages and assertions
*   WebDriver lifecycle events

Logs help with:

*   Debugging test failures
*   Monitoring test execution
*   Performance analysis
*   Compliance reporting

## 🏗️ Architecture Highlights

### Page Object Model Benefits

*   **Maintainability**: Centralized element management
*   **Reusability**: Methods can be shared across tests
*   **Readability**: Tests read like business requirements
*   **Scalability**: Easy to add new pages and functionality

### WebDriver Factory Pattern

*   **Automatic driver management** via WebDriverManager
*   **Cross-platform support** with OS detection
*   **Centralized browser configuration**
*   **Easy browser switching** for test execution

### Explicit Waits Implementation

*   **WebDriverWait** with 10-second timeout
*   **ExpectedConditions** for reliable element interactions
*   **Reduced test flakiness** from timing issues

## 🔧 Configuration

### xUnit Runner Configuration (`xunit.runner.json`)

```bash
{
  "parallelizeTestCollections": true,
  "maxParallelThreads": 4
}
```

### log4net Configuration

*   Console and file appenders
*   Configurable log levels
*   Timestamped log entries
*   Thread-safe logging

## 🐛 Troubleshooting

### Common Issues

**WebDriver not found**

*   Ensure WebDriverManager is properly installed
*   Check internet connectivity for driver downloads

**Tests failing intermittently**

*   Increase wait timeouts in WebDriverWait
*   Check for dynamic content loading issues

**Browser compatibility**

*   Verify browser versions are supported
*   Update WebDriverManager package for latest drivers

**Cross-platform issues**

*   Safari tests only run on macOS
*   Use appropriate browsers for your operating system
