----------01. Find Names of All Employees by First Name
SELECT FirstName, LastName FROM Employees
	WHERE SUBSTRING(FirstName, 1, 2) = 'Sa'

----------02. Find Names of All Employees by Last Name
SELECT FirstName, LastName FROM Employees
	WHERE LastName LIKE '%ei%'

----------03. Find First Names of All Employees
SELECT FirstName FROM Employees
	WHERE DepartmentId IN(3, 10)
	AND YEAR(HireDate) BETWEEN 1995 AND 2005

----------04. Find All Employees Except Engineers
SELECT FirstName, LastName FROM Employees
	WHERE JobTitle != '%engineer%'

----------05. Find Towns with Name Length
SELECT [Name] FROM Towns
WHERE LEN([Name]) BETWEEN 5 AND 6
ORDER BY [Name] ASC

----------06. Find Towns Starting With
SELECT TownID, [Name] FROM Towns
WHERE SUBSTRING([Name], 1, 1) IN('M', 'K', 'B', 'E')
ORDER BY [Name] ASC

----------07. Find Towns Not Starting With
SELECT TownID, [Name] FROM Towns
WHERE [Name] NOT LIKE '[RBD]%'
ORDER BY [Name] ASC

----------08. Ceate View Employees Hired After 2000 Year
CREATE VIEW V_EmployeesHiredAfter2000 AS 
SELECT FirstName, LastName FROM Employees
WHERE DATEPART(YEAR, HireDate) > 2000

----------09. Length of Last Name
SELECT FirstName, LastName FROM Employees
WHERE LEN(LastName) = 5 

----------10. AND 11. Rank Employees by Salary
SELECT * FROM
(SELECT EmployeeID, FirstName, LastName, Salary,
DENSE_RANK()
OVER (PARTITION BY Salary ORDER BY EmployeeID) AS [Rank] 
FROM Employees
WHERE Salary BETWEEN 10000 AND 50000) AS [Virtual Table]
WHERE [Rank] = 2
ORDER BY Salary DESC

----------12. Countries Holding 'A' 3 or More Times
USE [Geography]
SELECT CountryName AS [Country Name], IsoCode AS[ISO Code] FROM Countries
WHERE CountryName LIKE '%a%a%a%'
Order BY IsoCode ASC
----------13. Mix of Peak and River Names
SELECT p.PeakName, r.RiverName, LOWER(
CONCAT(p.PeakName, SUBSTRING(r.RiverName, 2, LEN(r.RiverName) - 1))) AS [Mix]
FROM Peaks AS p, Rivers AS r
WHERE RIGHT(p.PeakName, 1) = LEFT(r.RiverName, 1)
ORDER BY [Mix] 

----------14. Games from 2011 and 2012 Year
Use Diablo
SELECT TOP(50) [Name], FORMAT([Start], 'yyyy-MM-dd') AS [Start] FROM Games
WHERE DATEPART(YEAR, [Start]) IN (2011, 2012)
ORDER BY [Start], [Name]

----------15. User Email Providers
SELECT Username, 
      SUBSTRING_INDEX(Email, '@', 1) AS [Email Provider]
FROM Users
ORDER BY [Email Provider] ASC, Username ASC;

----------16. Get Users with IP Address Like Pattern
SELECT Username, IpAddress FROM Users
WHERE IpAddress LIKE '___.1_%._%.___'
ORDER BY Username ASC

----------17. Show All Games with Duration and Part of the Day
SELECT [Name] AS [Game], 
	CASE 
		WHEN DATEPART(HOUR, [Start]) BETWEEN 0 AND 11 THEN 'Morning'
		WHEN DATEPART(HOUR, [Start]) BETWEEN 12 AND 17 THEN 'Afternoon'
		ELSE 'Evening'
	END AS[Part of the Day],
	CASE
		WHEN Duration <= 3 THEN 'Extra Short'
		WHEN Duration BETWEEN 4 AND 6 THEN 'Short'
		WHEN Duration > 6 THEN 'Long'
		ELSE 'Extra Long'
	END AS[Duration]
FROM Games
ORDER BY [Name] ASC, Duration ASC, [Part of the Day] ASC

----------19. People Table
CREATE TABLE People(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	Birthdate DATETIME2 NOT NULL
)

INSERT INTO People([Name], Birthdate)
	VALUES
	('Victor', '2000-12-07'),
	('Steven', '1992-09-10'),
	('Stephen', '1910-09-19'),
	('John', '2010-01-06')

SELECT [Name],
DATEDIFF(year, Birthdate, GETDATE()) AS [Age in Years],
DATEDIFF(month, Birthdate, GETDATE()) AS [Age in Months],
DATEDIFF(day, Birthdate, GETDATE()) AS [Age in Days],
DATEDIFF(minute, Birthdate, GETDATE()) AS [Age in Minutes]
FROM People

