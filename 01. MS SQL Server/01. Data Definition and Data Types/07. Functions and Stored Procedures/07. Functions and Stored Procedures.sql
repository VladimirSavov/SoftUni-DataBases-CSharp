----------01. Employees with Salary Above 35000
CREATE PROCEDURE usp_GetEmployeesSalaryAbove35000 
AS
SELECT FirstName AS [First Name], LastName AS [Last Name]
FROM Employees
WHERE Salary > 35000

EXEC usp_GetEmployeesSalaryAbove35000

----------02. Employees with Salary Above Number
CREATE PROCEDURE usp_GetEmployeesSalaryAboveNumber(@minSalary DECIMAL(18, 4))
AS
SELECT FirstName AS [First Name], LastName AS [Last Name]
FROM Employees
WHERE Salary >= @minSalary

EXEC usp_GetEmployeesSalaryAboveNumber 48100

----------03. Town Names Starting With
CREATE PROCEDURE usp_GetTownsStartingWith(@letter NVARCHAR(50))
AS
	SELECT [Name]
	FROM Towns
	WHERE LEFT([Name], LEN(@letter)) = @letter

EXEC usp_GetTownsStartingWith be

----------04. Employees from Town
CREATE PROCEDURE usp_GetEmployeesFromTown(@name NVARCHAR(50))
AS
	SELECT e.FirstName AS [First Name], e.LastName AS [Last Name]
	FROM Employees AS e
	JOIN Addresses AS a
	ON e.AddressID = a.AddressID
	JOIN Towns AS t
	ON a.TownID = t.TownID
	WHERE t.[Name] = @name

EXEC usp_GetEmployeesFromTown Sofia

----------05. Salary Level Funtion
CREATE FUNCTION ufn_GetSalaryLeve(@salary DECIMAL(18, 4))
RETURNS VARCHAR(10)
AS
BEGIN
    DECLARE @level VARCHAR(10)

    IF @salary < 30000
        SET @level = 'Low'
    ELSE IF @salary >= 30000 AND @salary <= 50000
        SET @level = 'Average'
    ELSE
        SET @level = 'High'

    RETURN @level
END

GO 

SELECT FirstName, LastName, dbo.ufn_GetSalaryLeve(Salary) AS [Salary Level]
FROM Employees

----------06. Employees by Salary Level
CREATE PROCEDURE usp_EmployeesBySalaryLevel(@type NVARCHAR(7))
AS
	SELECT FirstName AS [FirstName],
			LastName AS [LastName]
			FROM Employees
			WHERE dbo.ufn_GetSalaryLeve(Salary) = @type

EXEC usp_EmployeesBySalaryLevel 'High'

----------09. Find Full Name
CREATE OR ALTER PROCEDURE usp_GetHoldersFullName
AS
	SELECT (FirstName + ' ' + LastName) AS [Full Name]
	FROM AccountHolders

EXEC usp_GetHoldersFullName

----------10. People with Balance Higher Than
CREATE OR ALTER PROCEDURE usp_GetHoldersWithBalanceHigherThan(@Balance DECIMAL (18, 4))
AS
	SELECT ah.FirstName AS [First Name], ah.LastName AS [Last Name]
	FROM Accounts AS a
	JOIN AccountHolders AS ah
	ON a.AccountHolderId = ah.Id
	GROUP BY FirstName, LastName
	HAVING SUM(Balance) > @Balance
	ORDER BY FirstName, LastName

EXEC usp_GetHoldersWithBalanceHigherThan 7000

----------11. Future Value Function
CREATE OR ALTER FUNCTION ufn_CalculateFutureValue(@sum DECIMAL(18, 4), @yir FLOAT, @yearsCount INT)
RETURNS DECIMAL(18, 4)
AS
BEGIN
	DECLARE @futureValue DECIMAL(18, 4)

	SET @futureValue = @sum * (POWER((1 + @yir), @yearsCount));

	RETURN @futureValue;
END
SELECT dbo.ufn_CalculateFutureValue(1000, 0.1, 5)

----------12. Calculating Interest
CREATE OR ALTER PROCEDURE usp_CalculateFutureValueForAccount
AS
	SELECT a.AccountHolderId AS [Account Id],
			ah.FirstName AS [First Name],
			ah.LastName AS [Last Name],
			SUM(Balance) AS [Current Balance],
			dbo.ufn_CalculateFutureValue(SUM(Balance), 0.1, 5)
	FROM Accounts AS a
	JOIN AccountHolders AS ah
	ON a.AccountHolderId = ah.Id
	GROUP BY FirstName, LastName





	