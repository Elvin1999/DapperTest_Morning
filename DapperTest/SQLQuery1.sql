CREATE DATABASE GameDB
GO
USE GameDB
GO

CREATE TABLE Players(
	Id INT PRIMARY KEY IDENTITY(1,1) NOT NULL,
	[Name] NVARCHAR(30) NOT NULL,
	Score FLOAT NOT NULL,
	IsStar BIT NOT NULL DEFAULT(0)
)

GO

INSERT INTO Players([Name],[Score],[IsStar])
VALUES('Lebron James',99,1),
('Stephan Curry',95,1),
('Maykl Jordan',94,1)

SELECT * FROM Players