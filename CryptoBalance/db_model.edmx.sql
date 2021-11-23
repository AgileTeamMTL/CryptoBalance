
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/23/2021 09:26:02
-- Generated from EDMX file: C:\Users\andre\Desktop\test\CryptoBalance\CryptoBalance\db_model.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [db_cryptoBalance];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[users]', 'U') IS NOT NULL
    DROP TABLE [dbo].[users];
GO
IF OBJECT_ID(N'[dbo].[transactions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[transactions];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'users'
CREATE TABLE [dbo].[users] (
    [username] varchar(50)  NOT NULL,
    [password] varchar(255)  NOT NULL,
    [first_name] varchar(50)  NOT NULL,
    [last_name] varchar(50)  NOT NULL,
    [email] varchar(50)  NOT NULL,
    [phone] varchar(50)  NOT NULL,
    [city] varchar(50)  NOT NULL,
    [province] varchar(50)  NOT NULL,
    [country] varchar(50)  NOT NULL,
    [date_of_birth] datetime  NOT NULL,
    [creation_date] datetime  NOT NULL,
    [modification_date] datetime  NOT NULL
);
GO

-- Creating table 'transactions'
CREATE TABLE [dbo].[transactions] (
    [username] nvarchar(50)  NOT NULL,
    [transaction_id] nvarchar(50)  NOT NULL,
    [crypto_coin] nvarchar(10)  NOT NULL,
    [market_price] float  NOT NULL,
    [amount] float  NOT NULL,
    [cryto_total] float  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [username] in table 'users'
ALTER TABLE [dbo].[users]
ADD CONSTRAINT [PK_users]
    PRIMARY KEY CLUSTERED ([username] ASC);
GO

-- Creating primary key on [username] in table 'transactions'
ALTER TABLE [dbo].[transactions]
ADD CONSTRAINT [PK_transactions]
    PRIMARY KEY CLUSTERED ([username] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------