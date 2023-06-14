USE Gringotts

----------01. Records' Count
SELECT COUNT(Id)
FROM WizzardDeposits

----------02. Longest Magic Wand
SELECT MAX(MagicWandSize)
FROM WizzardDeposits

----------03. Longest Magic Wand Per Deposit Groups
SELECT DepositGroup, MAX(MagicWandSize)
FROM WizzardDeposits
GROUP BY DepositGroup

----------04. Smallest Deposit Group Per Magic Wand Size
SELECT TOP (2) DepositGroup FROM
		(
		SELECT DepositGroup, AVG(MagicWandSize) AS [AverageWandSize]
		FROM WizzardDeposits
		GROUP BY DepositGroup
		) AS [VB]
		ORDER BY [AverageWandSize]

----------05. Deposits Sum
SELECT DepositGroup, SUM(DepositAmount) AS [TotalSum]
FROM WizzardDeposits
GROUP BY DepositGroup

----------06. Deposits Sum for Ollivander Family
SELECT DepositGroup, SUM(DepositAmount) AS [TotalSum]
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup

----------07. Deposits Filter
SELECT DepositGroup, SUM(DepositAmount) AS [TotalSum]
FROM WizzardDeposits
WHERE MagicWandCreator = 'Ollivander family'
GROUP BY DepositGroup
HAVING SUM(DepositAmount) < 150000
ORDER BY [TotalSum] DESC

----------08. Deposit Charge
SELECT DepositGroup, MagicWandCreator, MIN(DepositCharge) AS [MinDepositCharge]
FROM WizzardDeposits
GROUP BY DepositGroup
ORDER BY MagicWandCreator, DepositGroup

----------09. Age Groups
SELECT AgeGroup, COUNT(*) AS [WizzardCount]
	FROM (SELECT
		CASE
			WHEN Age <= 10 THEN '[0-10]'
			WHEN Age >= 11 AND Age <= 20 THEN '[11-20]'
			WHEN Age >= 21 AND Age <= 30 THEN '[21-30]'
			WHEN Age >= 31 AND Age <= 40 THEN '[31-40]'
			WHEN Age >= 41 AND Age <= 50 THEN '[41-50]'
			WHEN Age >= 51 AND Age <= 60 THEN '[51-60]'
			ELSE '[61+]'
			END AS [AgeGroup], *
	FROM WizzardDeposits) AS [AgeGroupQuery]
GROUP BY [AgeGroup]

----------10. First Letter
SELECT DISTINCT SUBSTRING([FirstName], 1, 1) AS [FirstLetter]
FROM WizzardDeposits
WHERE DepositGroup = 'Troll chest'
GROUP BY FirstName

----------11. Average Interest
SELECT DepositGroup, IsDepositExpired, AVG(DepositInterest) AS [AverageInterest]
FROM WizzardDeposits
WHERE DepositStartDate > '01-01-1985'
GROUP BY DepositGroup, IsDepositExpired
ORDER BY DepositGroup DESC, IsDepositExpired ASC

----------13. Departments Total Salaries
SELECT DepartmentID, SUM(Salary) AS [TotalSalary]
FROM Employees
GROUP BY DepartmentID

----------14. Employees Minimum Salaries
SELECT DepartmentID, MIN(Salary) AS [MinimumSalary]
FROM Employees
WHERE DepartmentID IN (2, 5, 7)
AND HireDate > '01-01-2000'
GROUP BY DepartmentID

----------15. Employees Average Salaries
CREATE TABLE AverageSalary AS
SELECT * 
FROM Employees

----------16. Employees Maximum Salaries
SELECT DepartmentID, MAX(Salary) AS [MaxSalary]
FROM Employees
WHERE Salary < 30000
OR Salary > 70000
GROUP BY DepartmentID

----------17. Employees Count Salaries
SELECT COUNT(Salary) AS [Count]
FROM Employees
WHERE ManagerID IS NULL


