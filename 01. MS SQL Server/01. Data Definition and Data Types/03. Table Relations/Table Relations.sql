CREATE DATABASE TableRelations
USE TableRelations

----------01. One-To-One Relationship
CREATE TABLE Passports(
	PassportID INT PRIMARY KEY IDENTITY NOT NULL,
	PassportNumber NVARCHAR(50) NOT NULL
)
INSERT INTO Passports(PassportNumber)
	VALUES
		('N34FG218'),
		('K65L04R7'),
		('ZE657QP2')

CREATE TABLE Persons(
	PersonID INT PRIMARY KEY IDENTITY NOT NULL,
	FirstName NVARCHAR(50) NOT NULL,
	Salary DECIMAL (7, 2),
	PassportID INT FOREIGN KEY REFERENCES Passports(PassportID)
)

INSERT INTO Persons(FirstName, Salary, PassportID)
	VALUES
		('Roberto', 43300.00, 2),
		('Tom', 56100.00, 3),
		('Yana', 60200.00, 1)

SELECT * FROM Persons
SELECT * FROM Passports

----------02. One-To-Many Relationship
CREATE TABLE Manufacturers(
	ManufacturerID INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	EstablishedOn DATETIME2 NOT NULL

)

CREATE TABLE Models(
	ModelID INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	ManufacturerID INT FOREIGN KEY REFERENCES Manufacturers(ManufacturerID)
)

INSERT INTO Manufacturers([Name], EstablishedOn)
	VALUES
		('BMW', '1916-03-07'),
		('Tesla', '2003-01-01'),
		('Lada', '1966-05-01')

INSERT INTO Models([Name], ManufacturerID)
	VALUES
		('X1', 1),
		('i6', 1),
		('Model S', 2),
		('Model X', 2),
		('Model 3', 2),
		('Nova', 3)

SELECT * FROM Models
SELECT * FROM Manufacturers

----------03. Many-To-Many Relationship
CREATE TABLE Students(
	StudentID INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] VARCHAR(50) NOT NULL
)

INSERT INTO Students([Name])
	VALUES
		('Mila'),
		('Toni'),
		('Ron')

CREATE TABLE Exams(
	ExamID INT PRIMARY KEY IDENTITY (101, 1) NOT NULL,
	[Name] VARCHAR(50) NOT NULL
)

INSERT INTO Exams([Name])
	VALUES
		('SpringMVC'),
		('Neo4j'),
		('Oracle 11g')

CREATE TABLE StudentsExams(
	StudentID INT NOT NULL FOREIGN KEY REFERENCES Students(StudentID),
	ExamID INT NOT NULL, FOREIGN KEY REFERENCES Exams(ExamID),
	PRIMARY KEY(StudentID, ExamID)
)

INSERT INTO StudentsExams(StudentID, ExamID)
	VALUES
		(1, 101),
		(1, 102),
		(2, 101),
		(3, 103),
		(2, 102),
		(2, 103)

SELECT * FROM Students
SELECT * FROM Exams
SELECT * FROM StudentsExams

----------04. Self-Referencing
CREATE TABLE Teachers(
	TeacherID INT PRIMARY KEY IDENTITY(101, 1) NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	ManagerID INT FOREIGN KEY REFERENCES Teachers(TeacherID)
)

INSERT INTO Teachers([Name], ManagerID)
	VALUES
		('John', NULL),
		('Maya', 106),
		('Silvia', 106),
		('Ted', 105),
		('Mark', 101),
		('Greta', 101)

SELECT * FROM Teachers

----------05. Online Store Database
CREATE DATABASE OnlineStore
USE [OnlineStore]
CREATE TABLE Orders(
	OrderID INT PRIMARY KEY IDENTITY NOT NULL,
	CustomerID INT NOT NULL
)
CREATE TABLE ItemTypes(
	ItemTypeID INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] VARCHAR(50) NOT NULL
)
CREATE TABLE Items(
	ItemID INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	ItemTypeID INT FOREIGN KEY REFERENCES ItemTypes(ItemTypeID) NOT NULL,
)
CREATE TABLE OrderItems(
	OrderID INT FOREIGN KEY REFERENCES Orders(OrderID),
	ItemID INT FOREIGN KEY REFERENCES Items(ItemID)
)
CREATE TABLE Cities(
	CityID INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] VARCHAR(50) NOT NULL
)
CREATE TABLE Customers(
	CustomerID INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] VARCHAR(50) NOT NULL,
	Birthday DATETIME2 NOT NULL,
	CityID INT FOREIGN KEY REFERENCES Cities(CityID)
)

----------06. University Database
CREATE DATABASE University
USE University

CREATE TABLE Majors(
	MajorID INT PRIMARY KEY IDENTITY NOT NULL,
	[Name] VARCHAR(50) NOT NULL
)

CREATE TABLE Subjects(
	SubjectID INT PRIMARY KEY IDENTITY NOT NULL,
	SubjectName VARCHAR(50) NOT NULL
)

CREATE TABLE Students(
	StudentID INT PRIMARY KEY IDENTITY NOT NULL,
	StudentNumber INT NOT NULL,
	StudentName VARCHAR(50) NOT NULL,
	MajorID INT FOREIGN KEY REFERENCES Majors(MajorID)
)
CREATE TABLE Payments(
	PaymentID INT FOREIGN KEY REFERENCES Students(StudentID),
	PaymentDate VARCHAR(50) NOT NULL,
	PaymentAmount CHAR(20) NOT NULL,
	StudentID CHAR(20) NOT NULL
)

CREATE TABLE Agenda(
	StudentID INT FOREIGN KEY REFERENCES Students(StudentID),
	SubjectID INT FOREIGN KEY REFERENCES Subjects(SubjectID)
)

----------09. *Peaks in Rila
SELECT MountainRange, PeakName, Elevation FROM Peaks
JOIN Mountains ON Peaks.MountainId = Mountains.Id
WHERE MountainRange = 'Rila'
ORDER BY Elevation DESC



