
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 11/28/2016 01:53:30
-- Generated from EDMX file: C:\Users\Jirka\Desktop\projekty\calendar\BasicForm\CalendarEntity.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [Calendar];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_Order_Customer]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Order_Customer];
GO
IF OBJECT_ID(N'[dbo].[FK_Office_Provider]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Offices] DROP CONSTRAINT [FK_Office_Provider];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_Office]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Order_Office];
GO
IF OBJECT_ID(N'[dbo].[FK_Procedure_Office]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Procedures] DROP CONSTRAINT [FK_Procedure_Office];
GO
IF OBJECT_ID(N'[dbo].[FK_Order_Procedure]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Orders] DROP CONSTRAINT [FK_Order_Procedure];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[Customers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Customers];
GO
IF OBJECT_ID(N'[dbo].[Offices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Offices];
GO
IF OBJECT_ID(N'[dbo].[Orders]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Orders];
GO
IF OBJECT_ID(N'[dbo].[Procedures]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Procedures];
GO
IF OBJECT_ID(N'[dbo].[Providers]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Providers];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'Customers'
CREATE TABLE [dbo].[Customers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Surname] nvarchar(50)  NULL,
    [Email] nvarchar(50)  NULL,
    [PersonalNumber] varchar(50)  NULL,
    [Phone] varchar(50)  NOT NULL
);
GO

-- Creating table 'Offices'
CREATE TABLE [dbo].[Offices] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [ProviderID] int  NOT NULL,
    [OpenMo] time  NOT NULL,
    [CloseMo] time  NOT NULL,
    [Active] bit  NOT NULL,
    [OpenTu] time  NOT NULL,
    [CloseTu] time  NOT NULL,
    [OpenWe] time  NOT NULL,
    [CloseWe] time  NOT NULL,
    [OpenTh] time  NOT NULL,
    [CloseTh] time  NOT NULL,
    [OpenFr] time  NOT NULL,
    [CloseFr] time  NOT NULL,
    [OpenSa] time  NOT NULL,
    [CloseSa] time  NOT NULL,
    [OpenSu] time  NOT NULL,
    [CloseSu] time  NOT NULL
);
GO

-- Creating table 'Orders'
CREATE TABLE [dbo].[Orders] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [CustomerID] int  NOT NULL,
    [ProcedureID] int  NOT NULL,
    [OfficeID] int  NOT NULL,
    [DescProvider] nvarchar(50)  NULL,
    [DescCustomer] nvarchar(50)  NULL,
    [Begin] datetime  NOT NULL,
    [End] datetime  NOT NULL
);
GO

-- Creating table 'Procedures'
CREATE TABLE [dbo].[Procedures] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NOT NULL,
    [Description] nvarchar(50)  NULL,
    [Lasts] int  NOT NULL,
    [OfficeID] int  NOT NULL,
    [Active] bit  NOT NULL
);
GO

-- Creating table 'Providers'
CREATE TABLE [dbo].[Providers] (
    [ID] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(50)  NULL,
    [Surname] nvarchar(50)  NULL,
    [Email] nvarchar(50)  NOT NULL,
    [PassHashed] nvarchar(50)  NOT NULL,
    [Payed] bit  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ID] in table 'Customers'
ALTER TABLE [dbo].[Customers]
ADD CONSTRAINT [PK_Customers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Offices'
ALTER TABLE [dbo].[Offices]
ADD CONSTRAINT [PK_Offices]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [PK_Orders]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Procedures'
ALTER TABLE [dbo].[Procedures]
ADD CONSTRAINT [PK_Procedures]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- Creating primary key on [ID] in table 'Providers'
ALTER TABLE [dbo].[Providers]
ADD CONSTRAINT [PK_Providers]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [CustomerID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Order_Customer]
    FOREIGN KEY ([CustomerID])
    REFERENCES [dbo].[Customers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_Customer'
CREATE INDEX [IX_FK_Order_Customer]
ON [dbo].[Orders]
    ([CustomerID]);
GO

-- Creating foreign key on [ProviderID] in table 'Offices'
ALTER TABLE [dbo].[Offices]
ADD CONSTRAINT [FK_Office_Provider]
    FOREIGN KEY ([ProviderID])
    REFERENCES [dbo].[Providers]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Office_Provider'
CREATE INDEX [IX_FK_Office_Provider]
ON [dbo].[Offices]
    ([ProviderID]);
GO

-- Creating foreign key on [OfficeID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Order_Office]
    FOREIGN KEY ([OfficeID])
    REFERENCES [dbo].[Offices]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_Office'
CREATE INDEX [IX_FK_Order_Office]
ON [dbo].[Orders]
    ([OfficeID]);
GO

-- Creating foreign key on [OfficeID] in table 'Procedures'
ALTER TABLE [dbo].[Procedures]
ADD CONSTRAINT [FK_Procedure_Office]
    FOREIGN KEY ([OfficeID])
    REFERENCES [dbo].[Offices]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Procedure_Office'
CREATE INDEX [IX_FK_Procedure_Office]
ON [dbo].[Procedures]
    ([OfficeID]);
GO

-- Creating foreign key on [ProcedureID] in table 'Orders'
ALTER TABLE [dbo].[Orders]
ADD CONSTRAINT [FK_Order_Procedure]
    FOREIGN KEY ([ProcedureID])
    REFERENCES [dbo].[Procedures]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_Order_Procedure'
CREATE INDEX [IX_FK_Order_Procedure]
ON [dbo].[Orders]
    ([ProcedureID]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------