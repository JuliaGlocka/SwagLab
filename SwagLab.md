# SwagLabs Automated UI Testing

## Overview

This project automates UI tests for the login functionality of the SwagLabs demo application (https://www.saucedemo.com/). It uses Selenium WebDriver with support for multiple browsers (Edge, Firefox, Chrome, Safari, Internet Explorer) and follows the Page Object Pattern.

## Features

- Tests written in C# with xUnit  
- Parallel execution supported  
- Logging with Log4Net (optional)  
- Parameterized tests using xUnit's Theory and MemberData  
- CSS selectors for locating elements  
- Assertions using FluentAssertions  
- Optional support for design patterns like Abstract Factory, Adapter, Bridge  

## Project Structure
```bash
SwagLab.sln
├── PageObject/
│ ├── Login.cs # Page object model for login page
│ └── WebDriverFactory.cs # Factory class for browser drivers
├── PageObject.Test/
│ └── LoginTests.cs # Tests for UC-1, UC-2, UC-3
└── README.md
```

## Test Cases

**UC-1: Login with Empty Credentials**  
Steps:  
- Enter any value into Username and Password fields.  
- Clear both fields.  
- Click the Login button.  
- Assert error: "Username is required"  

**UC-2: Login with Empty Password**  
Steps:  
- Enter valid or any value into Username field.  
- Enter value in Password field.  
- Clear the Password field.  
- Click the Login button.  
- Assert error: "Password is required"  

**UC-3: Successful Login**  
Steps:  
- Use a username from "Accepted usernames" section (e.g., standard_user).  
- Enter password secret_sauce.  
- Click the Login button.  
- Assert the page title is "Swag Labs"  

## Browser Support

Tests run on:  
- Microsoft Edge  
- Mozilla Firefox  
- Google Chrome  
- Safari (macOS only)  
- Internet Explorer (Windows only, limited support)  

Browser selection is abstracted through Abstract Factory pattern inside `WebDriverFactory`. Singleton pattern is used to manage WebDriver lifecycle.

## Running Tests

From the test project directory (`PageObject.Test`), run:

```bash
dotnet test --logger:trx
```

For parallel execution:

```bash
dotnet test --parallel all
```

## Tools and Libraries
* Selenium.WebDriver: UI automation

* WebDriverManager: Auto-download browser drivers

* xUnit: Test runner

F* luentAssertions: Rich assertions

* Log4Net (optional): Logging test lifecycle

## Notes
* Tests use CSS selectors for stability and performance.

* Data is passed to test methods via xUnit's [MemberData] and [Theory].

* Optional: SpecFlow may be used to extend the project for BDD-style tests.

If you have any questions, feel free to reach out.