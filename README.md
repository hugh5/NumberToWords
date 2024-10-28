# NumberToWords Web Application
This project is a simple web application built with ASP.NET Core, designed to convert a numerical input (dollar amount) into its corresponding words representation (e.g., "123.45" to "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS").

![image](https://github.com/user-attachments/assets/9bd26646-9b3a-4c4f-9249-0909ddfbc39b)


## Table of Contents
- [Project Structure](#project-structure)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [Running Tests](#running-tests)
- [Usage](#usage)
- [Technlogy Reasoning](#technology-reasoning)
	- [C# with ASP.NET Core](#c-with-aspnet-core)
	- [Playwright with MSTest](#playwright-with-mstest)
- [Test Plan](#test-plan)
	- [Objectives](#objectives)
	- [Testing Strategy](#testing-strategy)
	- [Not apart of the test plan](#not-apart-of-the-test-plan)

## Project Structure
The project consists of two main components:
1. Source Files (`NumberToWords` Project)
	- `Pages/Index.cshtml`: The main page where users can input a number to convert.
	- `Pages/Index.cshtml.cs`: The `IndexModel` class, which handles the core logic for converting numbers to words.
	- Supporting Files:
		- `_ViewImports.cshtml` and `_ViewStart.cshtml`: Standard ASP.NET Core Razor configuration files.
		- `wwwroot/`: Contains static assets like CSS, JavaScript, and other resources.
2. Test Files (`MSTest` Project)
	- `UnitTest1.cs`: Contains the test class `UnitTest1`, which uses MSTest to validate the functionality of the `IndexModel` class.
	- Test Methods:
		- `TestInitialize` and `TestCleanup`: Setup and cleanup methods for the test environment.
		- `TestZero`, `TestOneCent`, `TestOneDollarOneCent`, etc.: Test cases to validate different input scenarios, such as zero input, valid dollar and cent amounts, and maximum allowable values.
	- Locators (`ILocator`): Defines test-specific locators for identifying the input field, submit button, and result display.


## Installation
1. Clone the Repository:
```bash
git clone https://github.com/hugh5/NumberToWords
```
2. Open the Solution: Open the project `NumberToWords.sln` in Visual Studio.
3. Restore Dependencies: Ensure all necessary dependencies are installed by restoring NuGet packages:
```bash
dotnet restore
```


## Running the Application
1. Build and Run:
- Run the project from Visual Studio by pressing F5 or selecting Debug > Start Debugging.
- The application should open in a browser window. You can enter a number in the input field, and upon submitting, the corresponding words representation of the number will be displayed.


## Running Tests
1. Build the Solution: Ensure the solution is built, so all test files are compiled.
2. Run Tests in Test Explorer:
	- Open the Test Explorer in Visual Studio (Test > Test Explorer).
	- Select Run All to execute all tests.
The UnitTest1.cs file includes tests for the IndexModel functionality, covering cases like zero values, boundary cases, and invalid inputs.

## Usage
1. Navigate to the Home Page.
2. Convert a Number to Words:
	- Enter a decimal number (e.g., "123.45") in the input box.
	- Click the Convert button.
	- The output will display the number in words (e.g., "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS").\
- This project is designed for demonstration purposes and includes only basic input validation.
- It is recommended to enter valid decimal numbers within the range supported by System.Decimal.


## Technlogy Reasoning

#### C# with ASP.NET Core
Reason for Choice:
- ASP.NET Core provides a powerful framework for building web applications, which allows for rapid development and integration with various tools and services e.g. testing frameworks and deployment pipelines.
- C#'s strong typing and object-oriented features enable better maintainability and readability of the codebase, reducing the likelihood of runtime errors and enhancing code quality.
- ASP.NET Core allows the application to run on various operating systems, making it flexible for deployment in different environments.

#### Playwright with MSTest
Reason for Choice:
- Playwright enables testing across different browsers, ensuring that the application behaves consistently regardless of the user’s choice of browser.
- The framework's API is straightforward, making it easy to write and maintain tests. This is particularly useful for ensuring that the application remains functional as changes are made during development.

## Test Plan

####  Objectives
1. Verify that the application displays the correct title.
1. Ensure the application accurately converts valid numeric inputs into words.
1. Validate that the application handles edge cases and maximum input values appropriately.
1. Confirm that the application provides appropriate error messages for invalid inputs.

#### Testing Strategy
1. Type of Testing: Automated Functional Testing
1. Testing Framework: Playwright with MSTest
1. Test Types: Unit tests for individual functionalities
	- This includes coming up with test cases for different scenarios, such as zero values, valid dollar and cent amounts, and maximum allowable values.
	- And then comparing the expected output with the actual output.
1. Environment: Local development server (http://localhost:5000)

#### Not apart of the test plan
1. Performance testing
1. Security testing
1. Compatibility testing on different browsers and devices (this may be considered for future testing)
