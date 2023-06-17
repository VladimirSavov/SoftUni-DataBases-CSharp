----------01. Database design
CREATE TABLE Categories(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(50) NOT NULL
)

CREATE TABLE Addresses(
	Id INT PRIMARY KEY IDENTITY,
	StreetName NVARCHAR(100) NOT NULL,
	StreetNumber INT NOT NULL,
	Town NVARCHAR(30) NOT NULL,
	Country NVARCHAR(50) NOT NULL,
	ZIP INT NOT NULL
)

CREATE TABLE Publishers(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) UNIQUE NOT NULL,
	AddressId INT FOREIGN KEY REFERENCES Addresses(Id) NOT NULL,
	Website NVARCHAR(40) NOT NULL,
	Phone NVARCHAR(20) NOT NULL
)

CREATE TABLE PlayersRanges(
	Id INT PRIMARY KEY IDENTITY,
	PlayersMin INT NOT NULL,
	PlayersMax INT NOT NULL
)

CREATE TABLE Boardgames(
	Id INT PRIMARY KEY IDENTITY,
	[Name] NVARCHAR(30) NOT NULL,
	YearPublished INT NOT NULL,
	Rating DECIMAL(18, 2) NOT NULL,
	CategoryId INT FOREIGN KEY REFERENCES Categories(Id) NOT NULL,
	PublisherId INT FOREIGN KEY REFERENCES Publishers(Id) NOT NULL,
	PlayersRangeId INT FOREIGN KEY REFERENCES PlayersRanges(Id) NOT NULL
)

CREATE TABLE Creators(
	Id INT PRIMARY KEY IDENTITY NOT NULL,
	FirstName NVARCHAR(30) NOT NULL,
	LastName NVARCHAR(30) NOT NULL,
	Email NVARCHAR(30) NOT NULL
)

CREATE TABLE CreatorsBoardgames(
	CreatorId INT REFERENCES Creators(Id) NOT NULL,
	BoardgameId INT REFERENCES Boardgames(Id) NOT NULL
	PRIMARY KEY(CreatorId, BoardgameId)
)

----------02. Insert
INSERT INTO Boardgames([Name], YearPublished, Rating, CategoryId, PublisherId, PlayersRangeId)
	VALUES
		('Deep Blue', '2019', '5.67', 1, 15, 7),
		('Paris', '2016', '9.78', 7, 1, 5),
		('Catan: Starfarers', '2021', '9.87', 7, 13, 6),
		('Bleeding Kansas', '2020', '3.25', 3, 7, 4),
		('One Small Step', '2019', '5.75', 5, 9, 2)

INSERT INTO Publishers([Name], AddressId, Website, Phone)
	VALUES
		('Agman Games', 5, 'www.agmangames,com', '+16546135542'),
		('Amethyst Games', 7, 'www.amethystgames.com', '+15558889992'),
		('BattleBooks', 13, 'www.battlebooks.com', '+12345678907')

----------03. Update
	UPDATE PlayersRanges
	SET PlayersMax = 3
	WHERE PlayersMin = 2 AND PlayersMax = 2;
	UPDATE Boardgames 
	SET [Name] = [Name] + 'V2'
	WHERE YearPublished >= '2020'

----------04. Delete
DELETE FROM a
FROM Addresses AS a
JOIN Publishers As p
On a.Id = p.AddressId
WHERE LEFT(Town, 1) = 'L';

----------05. Boardgames by Year of Publication
SELECT [Name], Rating
	FROM Boardgames
	ORDER BY [YearPublished] ASC, [Name] DESC

----------06. Boardgames by Category
SELECT b.Id, b.[Name], YearPublished, c.[Name]
	FROM Boardgames AS b
	JOIN Categories AS c 
	ON b.CategoryId = c.Id
	WHERE c.[Name] IN ('Strategy Games', 'Wargames')
	ORDER BY YearPublished DESC

----------07. Creators without Boardgames
SELECT
  Id,
  CONCAT(FirstName, ' ', LastName) AS CreatorName,
  Email
FROM
  Creators
WHERE
  Id NOT IN (
    SELECT DISTINCT CreatorId
    FROM CreatorsBoardGames
  )
ORDER BY
  CreatorName ASC;

----------08. First 5 Boardgames
SELECT TOP(5) b.[Name], b.Rating, c.[Name] AS CategoryName
	FROM Boardgames	AS b
	JOIN PlayersRanges AS pr
	ON pr.Id = b.PlayersRangeId
	JOIN Categories AS c
	ON c.Id = b.CategoryId
	WHERE (b.Rating >  7.00 
	AND b.[Name] LIKE '&a%' OR b.[Name] LIKE '%a' OR b.[Name] LIKE '%a%')
	OR (b.Rating > 7.50 
	AND pr.PlayersMin = 2 AND pr.PlayersMax = 5)
	ORDER BY b.[Name] ASC, b.Rating DESC

----------09. Creators with Emails
SELECT CONCAT(FirstName, ' ', LastName) AS FullName,
		Email,
		MAX(b.Rating) AS [Rating]
		FROM Creators
		JOIN CreatorsBoardgames AS cb
		ON Creators.Id = cb.CreatorId
		JOIN Boardgames AS b
		ON cb.BoardgameId = b.Id
		WHERE RIGHT(Creators.Email, 4) = '.com'
		GROUP BY FirstName, LastName, Email
		ORDER BY Rating ASC, FullName ASC
	
----------10. Creators by Rating
SELECT c.LastName,
		CEILING(AVG(b.Rating)) AS [AverageRating],
		p.[Name] AS [PublisherName]
		FROM Creators AS c
		JOIN CreatorsBoardgames AS cb
		ON c.Id = cb.CreatorId
		JOIN Boardgames AS b
		ON cb.BoardgameId = b.Id
		JOIN Publishers AS p
		ON b.PublisherId = p.Id
		WHERE p.[Name] = 'Stonemaier Games'
		GROUP BY LastName, b.Rating, p.[Name]
		ORDER BY AverageRating DESC

----------11. Creator with Boardgames
GO
CREATE FUNCTION udf_CreatorWithBoardgames(@name NVARCHAR(30))
RETURNS INT
AS
BEGIN
	DECLARE @count INT;

	SELECT @count = COUNT(*)
	FROM Creators
	JOIN CreatorsBoardgames AS cb
	ON cb.CreatorId = Creators.Id
	WHERE Creators.FirstName = @name;

	RETURN @count;
END

SELECT dbo.udf_CreatorWithBoardgames('Bruno')

----------12. Search for Boardgame with Specific Category
GO
CREATE PROCEDURE usp_SearchByCategory(@category NVARCHAR(30)) 
AS
	SELECT b.[Name],
			b.YearPublished,
			b.Rating,
			c.[Name] AS CategoryName,
			p.[Name] AS PublisherName,
			CONCAT(pr.PlayersMin, ' ', 'people'),
			CONCAT(pr.PlayersMax, ' ', 'people')
			FROM Boardgames AS b
			JOIN Categories AS c
			ON b.CategoryId = c.Id
			JOIN Publishers AS p
			ON b.PublisherId = p.Id
			JOIN PlayersRanges AS pr
			ON b.PlayersRangeId = pr.Id
			WHERE c.[Name] = @category
			ORDER BY PublisherName ASC, YearPublished DESC
GO

EXEC usp_SearchByCategory 'Wargames'