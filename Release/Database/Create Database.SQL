-- CREATE DATABASE

CREATE DATABASE LoggingPOC;

-- CREATE SCHEMA

use LoggingPOC;

-- CREATE TABLES

DROP TABLE [dbo].[Users]

CREATE TABLE [dbo].[Users]
(
	[UserId] [int] NOT NULL PRIMARY KEY IDENTITY,
	[UserName] [varchar] (255) NOT NULL
) ON [PRIMARY]

DROP TABLE [dbo].[UsersTestData]

CREATE TABLE [dbo].[UsersTestData]
(
	[TestDataId] [int] NOT NULL PRIMARY KEY IDENTITY,
	[UserId] [int] NOT NULL,
	[TestData] [varchar] (255) NOT NULL
) ON [PRIMARY]

-- CREATE DATA


INSERT INTO [dbo].[Users] VALUES ('Test 1');

INSERT INTO [dbo].[Users] VALUES ('Test 2');


SELECT * FROM [dbo].[Users];


INSERT INTO [dbo].[UsersTestData] VALUES (1, 'ABC');

INSERT INTO [dbo].[UsersTestData] VALUES (2, 'DEF');

INSERT INTO [dbo].[UsersTestData] VALUES (2, 'GHI');


SELECT * FROM [dbo].[UsersTestData];
