# NumberToWords Web Application
This project is a simple web application built with ASP.NET Core, designed to convert a numerical input (dollar amount) into its corresponding words representation (e.g., "123.45" to "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS").

## Table of Contents
- [Project Structure](#project-structure)
- [Installation](#installation)
- [Running the Application](#running-the-application)
- [Running Tests](#running-tests)
- [Usage](#usage)

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
	- The output will display the number in words (e.g., "ONE HUNDRED AND TWENTY-THREE DOLLARS AND FORTY-FIVE CENTS").


## Notes
- This project is designed for demonstration purposes and includes only basic input validation.
- It is recommended to enter valid decimal numbers within the range supported by System.Decimal.
