# Lab2_UnitTestingProject

This solution is part of Lab 2 in the course "Software Testing 1" and includes unit testing in C# using MSTest, NUnit, and xUnit.
The lab covers three main assignments: testing a BankAccount class, comparing test frameworks, and implementing tests for a Roman Numeral converter.

# Solution Structure & Framework Usage

# BankAccountApp
A console application containing the implementation of the `BankAccount` class with basic functionality for debit and credit operations.

# BankAccountTests_MSTest
A test project using MSTest. Includes unit tests for the `BankAccount` class. Covers both valid and invalid debit and credit operations.
MSTest is used in: `BankAccountTests_MSTest`

# BankAccountTests_NUnit
A test project using NUnit. Contains equivalent tests to MSTest, adapted for the NUnit framework.
NUnit is used in: `BankAccountTests_NUnit`

# BankAccountTests_xUnit
A test project using xUnit. Contains the same logical test cases as the other two frameworks but written using xUnit syntax.
xUnit is used in: `BankAccountTests_xUnit`

# RomanNumeralsLib
A class library containing the `RomanNumeral` class, which can convert between integers and Roman numerals. Also supports parsing and arithmetic operations.

# RomanNumeralsTests
A test project targeting `RomanNumeralsLib`. Includes:

- `GoodInputTests.cs`: Validates correct conversion of integers to Roman numerals and vice versa.
- `BadInputTests.cs`: Handles invalid input cases and verifies correct handling (e.g., null, unsupported characters, invalid structure).
xUnit is used in: `RomanNumeralsTests`

# How to Run the Tests

All tests can be run via the Test Explorer in Visual Studio:

1. Open the solution `CSharpUnitTestingLab.sln` in Visual Studio (preferably Enterprise Edition).
2. Make sure all projects build successfully.
3. Open Test > Test Explorer.
4. Click `Run All` to execute all unit tests.

Each test project is independent and uses its own framework. Ensure that all test projects are referenced correctly to `BankAccountApp` or `RomanNumeralsLib`.

# Notes

- Roman numeral conversion supports values from 1 to 3999.
- The RomanNumeral class includes alternative and historical notations; tests focus on the standard subtractive form.
- Bad input tests are adjusted to work without modifying the source class, by asserting returned values instead of expecting thrown exceptions.

# Code Coverage

Code coverage was measured using Visual Studio Enterprise by selecting **Test > Analyze Code Coverage > All Tests**.
The following was observed:

- `BankAccount.cs`: All key methods (`Credit`, `Debit`, etc.) are fully tested across MSTest, NUnit, and xUnit. The demo `Main()` method in `Program.cs` is not covered, which is acceptable.
- `RomanNumeral.cs`: All conversion logic (`ToString()`, `ToRoman()`, `ParseRoman()`), validation (`ValidateRomanNumeral`) and structure-checking methods are covered. Some optional operator overloads were not tested, as those are listed as voluntary in the lab instructions.
- Total coverage for core logic is high, and no critical untested paths remain.

# 2.5 Lab Feedback

# a) Were the lab relevant and appropriate and what about length etc?

Yes, the lab was relevant and provided a good opportunity to get hands-on experience with multiple unit testing frameworks in C#. The length was reasonable, though parts of 2.3 (e.g. Roman numeral parsing logic) required some additional effort to understand the provided code and adapt the tests properly.

# b) What corrections and/or improvements do you suggest for this lab?

It would help if the provided Roman numeral code explicitly threw exceptions on invalid input, especially since the lab asks students to write tests expecting such exceptions. Currently, the tests must be adapted to fit the code rather than the other way around. More clarity in 2.3 regarding expected behaviors (e.g. how to handle out-of-range values or unsupported formats) would also improve the assignment. Lastly, providing a minimal solution template with all projects pre-created could reduce setup time and help students focus on writing tests.