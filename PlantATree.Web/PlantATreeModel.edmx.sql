
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, and Azure
-- --------------------------------------------------
-- Date Created: 03/02/2011 01:28:33
-- Generated from EDMX file: D:\Programming\Silverlight\PlantATree\PlantATree-WCF\PlantATree\PlantATree.Web\PlantATreeModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [PlantATree];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Trees]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Trees];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Trees'
CREATE TABLE [dbo].[Trees] (
    [TreeId] int IDENTITY(1,1) NOT NULL,
    [CreatorName] nvarchar(72)  NULL,
    [Message] nvarchar(255)  NULL,
    [CoordinateX] decimal(18,0)  NULL,
    [CoordinateY] decimal(18,0)  NULL,
    [CoordinateZ] decimal(18,0)  NULL,
    [CreationDate] datetime  NULL,
    [CreatorEmail] nvarchar(255)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [TreeId] in table 'Trees'
ALTER TABLE [dbo].[Trees]
ADD CONSTRAINT [PK_Trees]
    PRIMARY KEY CLUSTERED ([TreeId] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------