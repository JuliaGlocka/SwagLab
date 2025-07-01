# SwagLab Test Automation Framework

A complete Selenium-based test automation framework for SauceDemo UI testing using C#, xUnit, log4net, and the Page Object Model.

---

## ⚒️ Technologies Used

- **C# .NET 8**
- **Selenium WebDriver**
- **xUnit** for test execution
- **log4net** for logging
- **WebDriverManager** for automatic driver resolution
- **Page Object Model** design pattern
- **Microsoft.Extensions.Configuration** for reading settings from `appsettings.json`

---

## 📁 Project Structure

```
SwagLab/
├── Core/                                 # Shared core utilities
│   ├── WebDriverFactory.cs               # Creates browser instances
│   ├── LogInitializer.cs                 # Global logger setup from log4net.config
│   └── appsettings.json                  # Stores browser configuration
│
├── UI/                                   # Page Object Model layer
│   └── Pages/
│       └── LoginPage.cs                  # Login page abstraction
│
└── Tests/                                # Test project
    ├── LoginPageTests.cs                 # UC-1, UC-2, UC-3 and invalid login
    ├── WebDriverFactoryTests.cs         # Driver factory unit tests
    ├── log4net.config                    # Console + file appender
    └── xunit.runner.json                 # Parallel test execution config
```

---

## 🚀 Features

- **Cross-browser support** via WebDriverManager
- **Runs on Edge, Firefox** (configurable via `Configurator.Browser`)
- **Page Object Model** for reusable logic and maintainable code
- **Parallel test execution** via `xunit.runner.json`
- **Explicit waits** with `WebDriverWait` + `ExpectedConditions`
- **log4net logging** with file and console outputs

---

## 📄 Configuration

### `appsettings.json`

```json
{
  "Browser": "chrome",
  "SupportedBrowsers": [
    "chrome",
    "firefox",
    "edge",
    "safari",
    "ie"
  ]
}
```

Set the desired browser here. Supported values: `chrome`, `firefox`, `edge`.

### `xunit.runner.json`

```json
{
  "parallelizeAssembly": true,
  "parallelizeTestCollections": true,
  "maxParallelThreads": 2,
  "methodDisplay": "method",
  "diagnosticMessages": true,
  "preEnumerateTheories": false
}
```

### `log4net.config`

```xml
<?xml version="1.0" encoding="utf-8" ?>
<configuration>
  <log4net>
    <appender name="FileAppender" type="log4net.Appender.FileAppender">
      <file value="test-log.txt" />
      <appendToFile value="true" />
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%date [%thread] %-5level %logger - %message%newline" />
      </layout>
    </appender>
    <appender name="ConsoleAppender" type="log4net.Appender.ConsoleAppender">
      <layout type="log4net.Layout.PatternLayout">
        <conversionPattern value="%date [%thread] %-5level %logger - %message%newline" />
      </layout>
    </appender>
    <root>
      <level value="INFO" />
      <appender-ref ref="FileAppender" />
      <appender-ref ref="ConsoleAppender" />
    </root>
  </log4net>
</configuration>
```

---

## 🏃️ Running the Tests

### Restore and Build

```bash
dotnet restore
dotnet build
```

### Run All Tests

```bash
dotnet test
```

### Run for Specific Browser

```bash
dotnet test --filter "Configurator.Browser=edge"
```

### Run One Test Class

```bash
dotnet test --filter "FullyQualifiedName~LoginPageTests"
```

### Verbose Output

```bash
dotnet test --logger "console;verbosity=detailed"
```

---

## 🔮 Test Cases

### LoginPageTests.cs

| Test Case                      | Description                     | Expected Result                |
| ------------------------------ | ------------------------------- | ------------------------------ |
| UC1\_LoginWithEmptyCredentials | Login with no username/password | Shows "Username is required"   |
| UC2\_LoginWithEmptyPassword    | Username filled, no password    | Shows "Password is required"   |
| UC3\_LoginWithValidCredentials | Valid credentials login         | Navigates to product dashboard |
| Login\_WithInvalidCredentials  | Invalid username/password       | Shows login error message      |

### WebDriverFactoryTests.cs

| Test Case                            | Description                 | Expected Result                      |
| ------------------------------------ | --------------------------- | ------------------------------------ |
| CreateDriver\_WithSupportedBrowser   | Chrome, Firefox, Edge       | Returns proper WebDriver instance    |
| CreateDriver\_WithUnsupportedBrowser | Attempt unsupported browser | Throws ArgumentException             |
| CreateDriverSafariOnNonMac           | Safari driver on non-macOS  | Throws PlatformNotSupportedException |

---

## 📊 Test Data

### Credentials

- `standard_user` / `secret_sauce`
- `problem_user` / `secret_sauce`

### Browsers

- `chrome`
- `firefox`
- `edge`
- `safari` *(macOS only)*
- `internet explorer`

---

## 📜 Logging

- Uses `log4net` with global init via `LogInitializer`
- Output to:
  - Console
  - `Logs/test-log.txt`
- Logs:
  - Test start/end
  - Browser setup
  - UI actions (clicks, inputs)
  - Assertion outcomes
  - Driver disposal

---

## 🏋️ Troubleshooting

### WebDriver Not Found

- Check WebDriverManager NuGet is installed
- Internet required on first run for downloading drivers

### Random Test Failures

- Check for dynamic loading in app
- Use stable locators and proper wait timeouts

### Safari / IE

- Safari: macOS only
- IE: legacy support, not recommended

---

## 🚧 Architecture Summary

### Page Object Model

- Centralized selectors
- Clear test-to-page separation
- High reusability and readability

### WebDriverFactory

- Cross-platform support
- Browser string read from config
- Centralized creation and disposal

### Logging

- Thread-safe, timestamped logs
- Central error/debug tracking

