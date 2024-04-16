
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 04/14/2024 02:57:23
-- Generated from EDMX file: D:\Mini_Project\Rail_Reservations\RAIL.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [RAIL_RESERVATIONS];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK__BOOKING_T__TRAIN__4316F928]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOKING_TICKETS] DROP CONSTRAINT [FK__BOOKING_T__TRAIN__4316F928];
GO
IF OBJECT_ID(N'[dbo].[FK__BOOKING_TI__U_ID__4222D4EF]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[BOOKING_TICKETS] DROP CONSTRAINT [FK__BOOKING_TI__U_ID__4222D4EF];
GO
IF OBJECT_ID(N'[dbo].[FK__CANCEL_TI__BOOK___48CFD27E]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CANCEL_TICKET] DROP CONSTRAINT [FK__CANCEL_TI__BOOK___48CFD27E];
GO
IF OBJECT_ID(N'[dbo].[FK__CANCEL_TI__TRAIN__49C3F6B7]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CANCEL_TICKET] DROP CONSTRAINT [FK__CANCEL_TI__TRAIN__49C3F6B7];
GO
IF OBJECT_ID(N'[dbo].[FK__TRAIN_SEA__BOOK___534D60F1]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TRAIN_SEAT] DROP CONSTRAINT [FK__TRAIN_SEA__BOOK___534D60F1];
GO
IF OBJECT_ID(N'[dbo].[FK__TRAIN_SEA__TRAIN__52593CB8]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[TRAIN_SEAT] DROP CONSTRAINT [FK__TRAIN_SEA__TRAIN__52593CB8];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ADMIN_LOGIN]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ADMIN_LOGIN];
GO
IF OBJECT_ID(N'[dbo].[BOOKING_TICKETS]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BOOKING_TICKETS];
GO
IF OBJECT_ID(N'[dbo].[CANCEL_TICKET]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CANCEL_TICKET];
GO
IF OBJECT_ID(N'[dbo].[TRAIN]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TRAIN];
GO
IF OBJECT_ID(N'[dbo].[TRAIN_SEAT]', 'U') IS NOT NULL
    DROP TABLE [dbo].[TRAIN_SEAT];
GO
IF OBJECT_ID(N'[dbo].[USER_LOGIN]', 'U') IS NOT NULL
    DROP TABLE [dbo].[USER_LOGIN];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ADMIN_LOGIN'
CREATE TABLE [dbo].[ADMIN_LOGIN] (
    [ADMIN_ID] int  NOT NULL,
    [ADMIN_NAME] varchar(50)  NOT NULL,
    [APASSWORD] varchar(255)  NOT NULL
);
GO

-- Creating table 'BOOKING_TICKETS'
CREATE TABLE [dbo].[BOOKING_TICKETS] (
    [BOOK_ID] int  NOT NULL,
    [U_ID] int  NOT NULL,
    [TRAIN_NO] int  NOT NULL,
    [CLASSES] varchar(255)  NOT NULL,
    [BOOKING_DATE] datetime  NULL,
    [SEAT_BOOKED] int  NOT NULL
);
GO

-- Creating table 'CANCEL_TICKET'
CREATE TABLE [dbo].[CANCEL_TICKET] (
    [CANCEL_ID] int  NOT NULL,
    [DATE_OF_CANCEL] datetime  NOT NULL,
    [TRAIN_NO] int  NOT NULL,
    [NO_OF_SEATS] int  NOT NULL,
    [REFUND] decimal(10,2)  NOT NULL,
    [REMARK] varchar(255)  NULL,
    [BOOK_ID] int  NOT NULL
);
GO

-- Creating table 'TRAINs'
CREATE TABLE [dbo].[TRAINs] (
    [TRAIN_NO] int  NOT NULL,
    [TRAIN_NAME] varchar(255)  NULL,
    [CLASSES] varchar(255)  NULL,
    [TOTAL_SEATS] int  NOT NULL,
    [FARE] decimal(10,2)  NULL,
    [STATUS] varchar(50)  NOT NULL,
    [SOURCE] varchar(255)  NULL,
    [DESTINATION] varchar(255)  NULL,
    [AVAILABLE_SEATS] int  NULL
);
GO

-- Creating table 'TRAIN_SEAT'
CREATE TABLE [dbo].[TRAIN_SEAT] (
    [SEAT_ID] int  NOT NULL,
    [TRAIN_NO] int  NOT NULL,
    [CLASSES] varchar(255)  NOT NULL,
    [SEAT_NO] int  NOT NULL,
    [STATUS] varchar(50)  NULL,
    [BOOK_ID] int  NULL
);
GO

-- Creating table 'USER_LOGIN'
CREATE TABLE [dbo].[USER_LOGIN] (
    [ID] int  NOT NULL,
    [UNAME] varchar(255)  NOT NULL,
    [UPASSWORD] varchar(255)  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [ADMIN_ID] in table 'ADMIN_LOGIN'
ALTER TABLE [dbo].[ADMIN_LOGIN]
ADD CONSTRAINT [PK_ADMIN_LOGIN]
    PRIMARY KEY CLUSTERED ([ADMIN_ID] ASC);
GO

-- Creating primary key on [BOOK_ID] in table 'BOOKING_TICKETS'
ALTER TABLE [dbo].[BOOKING_TICKETS]
ADD CONSTRAINT [PK_BOOKING_TICKETS]
    PRIMARY KEY CLUSTERED ([BOOK_ID] ASC);
GO

-- Creating primary key on [CANCEL_ID] in table 'CANCEL_TICKET'
ALTER TABLE [dbo].[CANCEL_TICKET]
ADD CONSTRAINT [PK_CANCEL_TICKET]
    PRIMARY KEY CLUSTERED ([CANCEL_ID] ASC);
GO

-- Creating primary key on [TRAIN_NO] in table 'TRAINs'
ALTER TABLE [dbo].[TRAINs]
ADD CONSTRAINT [PK_TRAINs]
    PRIMARY KEY CLUSTERED ([TRAIN_NO] ASC);
GO

-- Creating primary key on [SEAT_ID] in table 'TRAIN_SEAT'
ALTER TABLE [dbo].[TRAIN_SEAT]
ADD CONSTRAINT [PK_TRAIN_SEAT]
    PRIMARY KEY CLUSTERED ([SEAT_ID] ASC);
GO

-- Creating primary key on [ID] in table 'USER_LOGIN'
ALTER TABLE [dbo].[USER_LOGIN]
ADD CONSTRAINT [PK_USER_LOGIN]
    PRIMARY KEY CLUSTERED ([ID] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [TRAIN_NO] in table 'BOOKING_TICKETS'
ALTER TABLE [dbo].[BOOKING_TICKETS]
ADD CONSTRAINT [FK__BOOKING_T__TRAIN__4316F928]
    FOREIGN KEY ([TRAIN_NO])
    REFERENCES [dbo].[TRAINs]
        ([TRAIN_NO])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BOOKING_T__TRAIN__4316F928'
CREATE INDEX [IX_FK__BOOKING_T__TRAIN__4316F928]
ON [dbo].[BOOKING_TICKETS]
    ([TRAIN_NO]);
GO

-- Creating foreign key on [U_ID] in table 'BOOKING_TICKETS'
ALTER TABLE [dbo].[BOOKING_TICKETS]
ADD CONSTRAINT [FK__BOOKING_TI__U_ID__4222D4EF]
    FOREIGN KEY ([U_ID])
    REFERENCES [dbo].[USER_LOGIN]
        ([ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__BOOKING_TI__U_ID__4222D4EF'
CREATE INDEX [IX_FK__BOOKING_TI__U_ID__4222D4EF]
ON [dbo].[BOOKING_TICKETS]
    ([U_ID]);
GO

-- Creating foreign key on [BOOK_ID] in table 'CANCEL_TICKET'
ALTER TABLE [dbo].[CANCEL_TICKET]
ADD CONSTRAINT [FK__CANCEL_TI__BOOK___48CFD27E]
    FOREIGN KEY ([BOOK_ID])
    REFERENCES [dbo].[BOOKING_TICKETS]
        ([BOOK_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CANCEL_TI__BOOK___48CFD27E'
CREATE INDEX [IX_FK__CANCEL_TI__BOOK___48CFD27E]
ON [dbo].[CANCEL_TICKET]
    ([BOOK_ID]);
GO

-- Creating foreign key on [BOOK_ID] in table 'TRAIN_SEAT'
ALTER TABLE [dbo].[TRAIN_SEAT]
ADD CONSTRAINT [FK__TRAIN_SEA__BOOK___534D60F1]
    FOREIGN KEY ([BOOK_ID])
    REFERENCES [dbo].[BOOKING_TICKETS]
        ([BOOK_ID])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TRAIN_SEA__BOOK___534D60F1'
CREATE INDEX [IX_FK__TRAIN_SEA__BOOK___534D60F1]
ON [dbo].[TRAIN_SEAT]
    ([BOOK_ID]);
GO

-- Creating foreign key on [TRAIN_NO] in table 'CANCEL_TICKET'
ALTER TABLE [dbo].[CANCEL_TICKET]
ADD CONSTRAINT [FK__CANCEL_TI__TRAIN__49C3F6B7]
    FOREIGN KEY ([TRAIN_NO])
    REFERENCES [dbo].[TRAINs]
        ([TRAIN_NO])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__CANCEL_TI__TRAIN__49C3F6B7'
CREATE INDEX [IX_FK__CANCEL_TI__TRAIN__49C3F6B7]
ON [dbo].[CANCEL_TICKET]
    ([TRAIN_NO]);
GO

-- Creating foreign key on [TRAIN_NO] in table 'TRAIN_SEAT'
ALTER TABLE [dbo].[TRAIN_SEAT]
ADD CONSTRAINT [FK__TRAIN_SEA__TRAIN__52593CB8]
    FOREIGN KEY ([TRAIN_NO])
    REFERENCES [dbo].[TRAINs]
        ([TRAIN_NO])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK__TRAIN_SEA__TRAIN__52593CB8'
CREATE INDEX [IX_FK__TRAIN_SEA__TRAIN__52593CB8]
ON [dbo].[TRAIN_SEAT]
    ([TRAIN_NO]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------