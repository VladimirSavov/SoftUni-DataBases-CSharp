----------01. Employee Address
SELECT TOP(5) e.EmployeeID, e.JobTitle, a.AddressID AS [AddressId], a.AddressText
FROM Addresses AS a
JOIN Employees AS e ON a.AddressID = e.AddressID
ORDER BY AddressID

----------02. Addresses with Towns
SELECT TOP(50) e.FirstName, e.LastName, t.[Name] AS [Town], a.AddressText
FROM Employees AS e
JOIN Addresses AS a ON a.AddressID = e.AddressID
JOIN Towns AS t ON t.TownID = a.TownID
ORDER BY FirstName, LastName

----------03. Sales Employee
SELECT e.EmployeeID, e.FirstName, e.LastName, d.[Name] AS DepartmentName
FROM Employees AS e
JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
WHERE d.[Name] = 'Sales'
ORDER BY EmployeeID ASC

----------04. Employee Departments
SELECT TOP(5) e.EmployeeID, e.FirstName, e.Salary, d.[Name] AS DepartmentName
FROM Employees AS e
JOIN Departments AS d ON e.DepartmentID = d.DepartmentID
WHERE e.Salary > 15000
ORDER BY e.DepartmentID

----------05. Employees Without Project
SELECT TOP(3) e.EmployeeID, e.FirstName 
FROM Employees AS e
LEFT OUTER JOIN EmployeesProjects AS p ON e.EmployeeID = p.EmployeeID
WHERE p.EmployeeID IS NULL
ORDER BY EmployeeID

----------06. Employees Hired After
SELECT e.FirstName, e.LastName, e.HireDate, d.[Name] AS DeptName
FROM Employees AS e
JOIN Departments AS d ON d.DepartmentID = e.DepartmentID
WHERE e.HireDate > '01-01-1999' 
AND d.[Name] IN ('Sales', 'Finance') 
ORDER BY HireDate

----------07. Employees with Project
SELECT TOP(5) e.EmployeeID, e.FirstName, p.[Name] AS ProjectName
FROM Employees AS e
JOIN EmployeesProjects AS c
ON e.EmployeeID = c.EmployeeID
JOIN Projects As p
ON c.ProjectID = p.ProjectID
WHERE p.StartDate > '08-13-2002' 
AND p.EndDate IS NULL
ORDER BY e.EmployeeID

----------08. Employee 24
SELECT e.EmployeeID, e.FirstName,
CASE
	WHEN YEAR(p.StartDate) >= 2005 THEN NULL
	ELSE p.[Name] 
END AS [ProjectName]
FROM Employees AS e
JOIN EmployeesProjects AS ep
ON e.EmployeeID = ep.EmployeeID
JOIN Projects AS p
ON p.ProjectID = ep.ProjectID
WHERE e.EmployeeID = 24 

----------09. Employee Manager
SELECT e1.EmployeeID, e1.FirstName, e1.ManagerID, e2.FirstName AS [ManagerName]
FROM Employees AS e1
LEFT OUTER JOIN Employees AS e2
ON e1.ManagerID = e2.EmployeeID 
WHERE e1.ManagerID IN (3, 7)
ORDER BY e1.EmployeeID

----------10. Employees Summary
SELECT TOP(50) e1.EmployeeID, 
	CONCAT(e1.FirstName, ' ', e1.LastName) AS [EmployeeName],
	CONCAT(e2.FirstName, ' ', e2.LastName) AS [ManagerName],
	d.[Name] AS [DepartmentName]
	FROM Employees AS e1
LEFT OUTER JOIN Employees AS e2
ON e1.ManagerID = e2.EmployeeID
INNER JOIN Departments AS d
ON e1.DepartmentID = d.DepartmentID
ORDER BY e1.EmployeeID

----------11. Min Average Salary
SELECT MIN([Average Salary]) AS [MinAverageSalary]
FROM
	(
	SELECT DepartmentID, AVG(Salary) AS [Average Salary]
	FROM Employees
	GROUP BY DepartmentID
	) AS [AverageSalaryQuery]

----------12. Highest Peaks in Bulgaria
USE [Geography]
SELECT mc.CountryCode, m.MountainRange, p.PeakName, p.Elevation
FROM Countries AS c
JOIN MountainsCountries AS mc
ON c.CountryCode = mc.CountryCode
JOIN Mountains AS m
ON mc.MountainId = m.Id
JOIN Peaks AS p
ON p.MountainId = m.Id
WHERE p.Elevation > 2835 
AND c.CountryCode = 'BG'
ORDER BY p.Elevation DESC

----------13. Count Mountain Ranges
SELECT CountryCode, COUNT(MountainId) AS [MountainRanges]
FROM MountainsCountries
WHERE CountryCode IN ('US', 'RU', 'BG')
GROUP BY CountryCode

