
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/20/2020 14:57:56
-- Generated from EDMX file: E:\Software Development Year 2 Semester 2\Object-Orientated Development\Project\ProjectTemplate\ProjectTemplate\WatchedMovies.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [WatchedMovies];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------


-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'WatchedMovies'
CREATE TABLE [dbo].[WatchedMovies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [MovieName] nvarchar(max)  NOT NULL,
    [MovieGenre] nvarchar(max)  NOT NULL,
    [MovieDescription] nvarchar(max)  NOT NULL,
    [MovieRating] nvarchar(max)  NOT NULL,
    [DateWatched] datetime  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'WatchedMovies'
ALTER TABLE [dbo].[WatchedMovies]
ADD CONSTRAINT [PK_WatchedMovies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------