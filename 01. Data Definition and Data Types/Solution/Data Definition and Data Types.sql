---------01. Create Database
CREATE DATABASE Minions

USE Minions

---------02. Create Tables
CREATE TABLE Minions(
	Id INT PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL,
	AGE TINYINT
)

CREATE TABLE Towns(
	Id INT PRIMARY KEY NOT NULL,
	[Name] NVARCHAR(50) NOT NULL
)

---------03. Alter Minions Table
ALTER TABLE Minions
ADD TownId INT FOREIGN KEY REFERENCES Towns(Id)

---------04. Insert Records in Both Tables
INSERT INTO Towns(Id, [Name])
	VALUES
		(1, 'Sofia'),
		(2, 'Plovdiv'),
		(3, 'Varna')


INSERT INTO Minions(Id, [Name], Age, TownId)
	VALUES 
		(1, 'Kevin', 22, 1),
		(2, 'Bob', 15, 3),
		(3, 'Steward', NULL, 2)

----------05. Truncate Table Minions
TRUNCATE TABLE Minions

----------06. Drop Aall Tables
DROP TABLE Minions
DROP TABLE Towns


----------07. Create Table Users
CREATE TABLE USERS(
	Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
	Username VARCHAR(30) UNIQUE NOT NULL,
	[Password] VARCHAR(26) NOT NULL,
	ProfilePicture VARBINARY(MAX)
	CHECK(DATALENGTH(ProfilePicture) <= 900 * 1024),
	LastLoginTime DATETIME2 NOT NULL,
	IsDeleted BIT NOT NULL,
)

INSERT INTO USERS(Username, [Password], LastLoginTime, IsDeleted)
	VALUES
		('Petko Ivanov', '940999', '05-19-2022', 0),
		('Petko Ivanov1', '940999', '05-19-2022', 1),
		('Petko Ivanov2', '940999', '05-19-2022', 0),
		('Petko Ivanov3', '940999', '05-19-2022', 1),
		('Petko Ivanov4', '940999', '05-19-2022', 0)

SELECT * FROM USERS

----------07. Create Table People
CREATE TABLE PEOPLE(
	Id BIGINT PRIMARY KEY IDENTITY NOT NULL,
	[Name] NVARCHAR(200) NOT NULL,
	Picture VARBINARY(MAX)
	CHECK(DATALENGTH(Picture) <= 1024 * 2 * 1024),
	 Height DECIMAL(3, 2),
    [Weight] DECIMAL(4, 2),
    Gender CHAR(1) NOT NULL,
    Birthdate DATE NOT NULL,
	Biography NVARCHAR(MAX)
)

INSERT INTO PEOPLE([NAME], Height, [Weight], Gender, Birthdate, Biography)
	VALUES
		('Pesho', 1.73, 89.50, 'm', '2003-07-22', 'zklzcxklk'),
		('Pesho1', 1.73, 89.50, 'm', '2003-07-22', 'zklzcxklk'),
		('Pesho2', 1.73, 89.50, 'm', '2003-07-22', 'zklzcxklk'),
		('Pesho3', 1.73, 89.50, 'm', '2003-07-22', 'zklzcxklk'),
		('Pesho4', 1.73, 89.50, 'm', '2003-07-22', 'zklzcxklk')

SELECT * FROM PEOPLE


----------09. Change Primary Key
ALTER TABLE USERS
DROP CONSTRAINT [PK__USERS__3214EC07BD87E20C]

ALTER TABLE USERS
ADD CONSTRAINT PK_Users_CompositeIdUsername
PRIMARY KEY(Id, Username)

----------10. Add Check Constraint
ALTER TABLE USERS
ADD CONSTRAINT CK_Users_PasswordLength
CHECK(LEN([Password]) >= 5)

----------11. Set Default Value of a Field
ALTER TABLE USERS
ADD CONSTRAINT DF_Users_LastLoginTime
DEFAULT GETDATE() FOR LastLoginTime

----------12. Set Unique Field
ALTER TABLE USERS
DROP CONSTRAINT PK_Users_CompositeIdUsername

ALTER TABLE USERS
ADD CONSTRAINT PK_Users_Id
PRIMARY KEY(Id)

ALTER TABLE USERS
ADD CONSTRAINT CK_Users_UsernameLength
CHECK(LEN(Username) >= 3)

----------13. Movies Database
CREATE DATABASE Movies

USE Movies

CREATE TABLE Directors(
	Id INT PRIMARY KEY IDENTITY,
	DirectorName NVARCHAR(50) NOT NULL,
	Notes TEXT
)
INSERT INTO Directors(DirectorName, Notes)
	VALUES
		('Petar', 'nomerEdno'),
		('Petar1', 'nomerEdno'),
		('Petar2', 'nomerEdno'),
		('Petar3', 'nomerEdno'),
		('Petar4', 'nomerEdno')

CREATE TABLE Genres(
	Id INT PRIMARY KEY IDENTITY,
	GenreName NVARCHAR(50) NOT NULL,
	Notes TEXT
)

INSERT INTO Genres(GenreName, Notes)
	VALUES
		('Nikolai', 'fgklflk'),
		('Nikolai1', 'fgklflk'),
		('Nikolai2', 'fgklflk'),
		('Nikolai3', 'fgklflk'),
		('Nikolai4', 'fgklflk')

CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	CategoryName NVARCHAR(50) NOT NULL,
	Notes TEXT
)

INSERT INTO Categories(CategoryName, Notes)
	VALUES
		('SPORT', 'DDVXDVXDVX'),
		('SPORT1', 'DDVXDVXDVX'),
		('SPORT2', 'DDVXDVXDVX'),
		('SPORT3', 'DDVXDVXDVX'),
		('SPORT4', 'DDVXDVXDVX')

CREATE TABLE Movies(
	Id INT PRIMARY KEY IDENTITY,
	Title CHAR(50) NOT NULL,
	DirectorId INT FOREIGN KEY REFERENCES Directors(Id) NOT NULL,
	CopyrightYear DATE NOT NULL,
	[Length] DECIMAL (5, 2),
	GenreId INT FOREIGN KEY REFERENCES Genres(Id) NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	Rating DECIMAL(3, 2),
	Notes TEXT
)

INSERT INTO Movies(Title, DirectorId, CopyrightYear, [Length], GenreId, CategoryId, Rating, Notes)
	VALUES
		('NXH', 2, '2020-12-12', '189.43', 3, 4, '4.56', 'vlvdsds')


SELECT * FROM Movies
SELECT * FROM Directors

----------16. Create SoftUni Database
CREATE DATABASE SoftUni

USE SoftUni

CREATE TABLE Towns(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Addresses(
	Id INT PRIMARY KEY IDENTITY,
	AddressText NVARCHAR(100) NOT NULL,
	TownId INT FOREIGN KEY REFERENCES Towns(Id) NOT NULL
)

CREATE TABLE Departments(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Employees(
	Id INT PRIMARY KEY IDENTITY,
	FirstName NVARCHAR(50) NOT NULL,
	MiddleName NVARCHAR(50),
	LastName NVARCHAR(50) NOT NULL,
	JobTitle NVARCHAR(30) NOT NULL,
	DepartmentId INT FOREIGN KEY REFERENCES Departments(Id) NOT NULL,
	HireDate DATE NOT NULL,
	Salary DECIMAL(7, 2) NOT NULL,
	AdressId INT FOREIGN KEY REFERENCES Addresses(Id)
)

----------18. Basic Insert
INSERT INTO Towns([Name])
	VALUES
		('Sofia'),
		('Plovdiv'),
		('Varna'),
		('Burgas')

INSERT INTO Departments([Name])
	VALUES
		('Engineering'),
		('Sales'),
		('Marketing'),
		('Software Development'),
		('Quality Assurance')

INSERT INTO Employees(FirstName, MiddleName, LastName, JobTitle, DepartmentId, HireDate, Salary)
	VALUES
		('Ivan', 'Ivanov', 'Ivanov', '.NET Developer', 4, '2013-02-01', 3500.00),
		('Petar', 'Petrov', 'Petrov', 'Senior Engineer', 1, '2004-03-02', 4000.00),
		('Maria', 'Petrova', 'Ivanova', 'Intern', 5, '2016-08-28', 525.25),
		('Georgi', 'Teziev', 'Ivanov', 'CEO', 2, '2007-12-09', 3000.00),
		('Peter', 'Pan', 'Pan', 'Intern', 3, '2016-08-28', 599.88)


----------19. Basic Select All Fields
SELECT * FROM Towns
SELECT * FROM Departments
SELECT * FROM Employees

---------20. Basic Select All Fields and Order Them
SELECT * FROM Towns
ORDER BY [Name] ASC

SELECT * FROM Departments
ORDER BY [Name] ASC

SELECT * FROM Employees
ORDER BY Salary DESC

---------21. Basic Select Some Fields
SELECT [Name] FROM Towns
ORDER BY [Name] ASC

SELECT [Name] FROM Departments
ORDER BY [Name] ASC

SELECT FirstName, LastName, JobTitle, Salary FROM Employees
ORDER BY Salary DESC

----------22. Increase Employees Salary
UPDATE Employees
SET Salary += Salary * 0.1

SELECT Salary FROM Employees