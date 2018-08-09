
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 08/09/2018 11:42:53
-- Generated from EDMX file: C:\Users\SDEBEUL\Source\Repos\ExcelPluginFrontEnd\EqUiWebUi\Models\EQUIModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [GADATA];
GO
IF SCHEMA_ID(N'EQUI') IS NULL EXECUTE(N'CREATE SCHEMA [EQUI]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[EqUi].[FK_c_LogClassRules_c_Classification]', 'F') IS NOT NULL
    ALTER TABLE [EqUi].[c_LogClassRules] DROP CONSTRAINT [FK_c_LogClassRules_c_Classification];
GO
IF OBJECT_ID(N'[EqUi].[FK_c_LogClassRules_c_logClassSystem]', 'F') IS NOT NULL
    ALTER TABLE [EqUi].[c_LogClassRules] DROP CONSTRAINT [FK_c_LogClassRules_c_logClassSystem];
GO
IF OBJECT_ID(N'[EqUi].[FK_c_LogClassRules_c_Subgroup]', 'F') IS NOT NULL
    ALTER TABLE [EqUi].[c_LogClassRules] DROP CONSTRAINT [FK_c_LogClassRules_c_Subgroup];
GO
IF OBJECT_ID(N'[EqUi].[FK_c_logClassSystem_c_datasource]', 'F') IS NOT NULL
    ALTER TABLE [EqUi].[c_logClassSystem] DROP CONSTRAINT [FK_c_logClassSystem_c_datasource];
GO
IF OBJECT_ID(N'[EqUi].[FK_l_dummyLogClassResult_c_Classification]', 'F') IS NOT NULL
    ALTER TABLE [EqUi].[l_dummyLogClassResult] DROP CONSTRAINT [FK_l_dummyLogClassResult_c_Classification];
GO
IF OBJECT_ID(N'[EqUi].[FK_l_dummyLogClassResult_c_Subgroup]', 'F') IS NOT NULL
    ALTER TABLE [EqUi].[l_dummyLogClassResult] DROP CONSTRAINT [FK_l_dummyLogClassResult_c_Subgroup];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[EqUi].[c_datasource]', 'U') IS NOT NULL
    DROP TABLE [EqUi].[c_datasource];
GO
IF OBJECT_ID(N'[EqUi].[c_LogClassRules]', 'U') IS NOT NULL
    DROP TABLE [EqUi].[c_LogClassRules];
GO
IF OBJECT_ID(N'[EqUi].[c_logClassSystem]', 'U') IS NOT NULL
    DROP TABLE [EqUi].[c_logClassSystem];
GO
IF OBJECT_ID(N'[EqUi].[l_dummyLogClassResult]', 'U') IS NOT NULL
    DROP TABLE [EqUi].[l_dummyLogClassResult];
GO
IF OBJECT_ID(N'[EqUi].[Wiki]', 'U') IS NOT NULL
    DROP TABLE [EqUi].[Wiki];
GO
IF OBJECT_ID(N'[Volvo].[c_Classification]', 'U') IS NOT NULL
    DROP TABLE [Volvo].[c_Classification];
GO
IF OBJECT_ID(N'[Volvo].[c_Subgroup]', 'U') IS NOT NULL
    DROP TABLE [Volvo].[c_Subgroup];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'c_datasource'
CREATE TABLE [EQUI].[c_datasource] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Description] varchar(max)  NULL,
    [Type] int  NOT NULL,
    [ConnectionString] varchar(max)  NOT NULL,
    [isAlertSource] bit  NOT NULL
);
GO

-- Creating table 'c_LogClassRules'
CREATE TABLE [EQUI].[c_LogClassRules] (
    [id] int IDENTITY(1,1) NOT NULL,
    [c_logClassSystem_id] int  NOT NULL,
    [coderangeStart] int  NULL,
    [coderangeEnd] int  NULL,
    [textSearch] varchar(255)  NULL,
    [I_comment] varchar(255)  NULL,
    [c_ClassificationId] int  NOT NULL,
    [c_SubgroupId] int  NOT NULL
);
GO

-- Creating table 'c_logClassSystem'
CREATE TABLE [EQUI].[c_logClassSystem] (
    [id] int  NOT NULL,
    [c_datasource_id] int  NOT NULL,
    [Name] varchar(50)  NOT NULL,
    [Description] varchar(max)  NULL,
    [SelectStatement] varchar(max)  NOT NULL,
    [UpdateStatement] varchar(max)  NULL,
    [RunRuleStatement] varchar(max)  NULL
);
GO

-- Creating table 'c_Classification'
CREATE TABLE [EQUI].[c_Classification] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Classification] varchar(50)  NOT NULL,
    [Discription] varchar(100)  NOT NULL
);
GO

-- Creating table 'c_Subgroup'
CREATE TABLE [EQUI].[c_Subgroup] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Subgroup] varchar(50)  NOT NULL,
    [Discription] varchar(100)  NOT NULL
);
GO

-- Creating table 'l_dummyLogClassResult'
CREATE TABLE [EQUI].[l_dummyLogClassResult] (
    [id] int  NOT NULL,
    [text] varchar(max)  NULL,
    [c_logcClassRules_id] int  NULL,
    [c_Classification_id] int  NULL,
    [c_Subgroup_id] int  NULL,
    [code] int  NULL
);
GO

-- Creating table 'Wiki'
CREATE TABLE [EQUI].[Wiki] (
    [id] int IDENTITY(1,1) NOT NULL,
    [Title] varchar(50)  NULL,
    [Description] varchar(50)  NULL,
    [Culture] varchar(50)  NULL,
    [wiki1] varchar(max)  NULL,
    [searchtags] varchar(max)  NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'c_datasource'
ALTER TABLE [EQUI].[c_datasource]
ADD CONSTRAINT [PK_c_datasource]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [id] in table 'c_LogClassRules'
ALTER TABLE [EQUI].[c_LogClassRules]
ADD CONSTRAINT [PK_c_LogClassRules]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'c_logClassSystem'
ALTER TABLE [EQUI].[c_logClassSystem]
ADD CONSTRAINT [PK_c_logClassSystem]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'c_Classification'
ALTER TABLE [EQUI].[c_Classification]
ADD CONSTRAINT [PK_c_Classification]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'c_Subgroup'
ALTER TABLE [EQUI].[c_Subgroup]
ADD CONSTRAINT [PK_c_Subgroup]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'l_dummyLogClassResult'
ALTER TABLE [EQUI].[l_dummyLogClassResult]
ADD CONSTRAINT [PK_l_dummyLogClassResult]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- Creating primary key on [id] in table 'Wiki'
ALTER TABLE [EQUI].[Wiki]
ADD CONSTRAINT [PK_Wiki]
    PRIMARY KEY CLUSTERED ([id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [c_datasource_id] in table 'c_logClassSystem'
ALTER TABLE [EQUI].[c_logClassSystem]
ADD CONSTRAINT [FK_c_logClassSystem_c_datasource]
    FOREIGN KEY ([c_datasource_id])
    REFERENCES [EQUI].[c_datasource]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_c_logClassSystem_c_datasource'
CREATE INDEX [IX_FK_c_logClassSystem_c_datasource]
ON [EQUI].[c_logClassSystem]
    ([c_datasource_id]);
GO

-- Creating foreign key on [c_ClassificationId] in table 'c_LogClassRules'
ALTER TABLE [EQUI].[c_LogClassRules]
ADD CONSTRAINT [FK_c_LogClassRules_c_Classification]
    FOREIGN KEY ([c_ClassificationId])
    REFERENCES [EQUI].[c_Classification]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_c_LogClassRules_c_Classification'
CREATE INDEX [IX_FK_c_LogClassRules_c_Classification]
ON [EQUI].[c_LogClassRules]
    ([c_ClassificationId]);
GO

-- Creating foreign key on [c_logClassSystem_id] in table 'c_LogClassRules'
ALTER TABLE [EQUI].[c_LogClassRules]
ADD CONSTRAINT [FK_c_LogClassRules_c_logClassSystem]
    FOREIGN KEY ([c_logClassSystem_id])
    REFERENCES [EQUI].[c_logClassSystem]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_c_LogClassRules_c_logClassSystem'
CREATE INDEX [IX_FK_c_LogClassRules_c_logClassSystem]
ON [EQUI].[c_LogClassRules]
    ([c_logClassSystem_id]);
GO

-- Creating foreign key on [c_SubgroupId] in table 'c_LogClassRules'
ALTER TABLE [EQUI].[c_LogClassRules]
ADD CONSTRAINT [FK_c_LogClassRules_c_Subgroup]
    FOREIGN KEY ([c_SubgroupId])
    REFERENCES [EQUI].[c_Subgroup]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_c_LogClassRules_c_Subgroup'
CREATE INDEX [IX_FK_c_LogClassRules_c_Subgroup]
ON [EQUI].[c_LogClassRules]
    ([c_SubgroupId]);
GO

-- Creating foreign key on [c_Classification_id] in table 'l_dummyLogClassResult'
ALTER TABLE [EQUI].[l_dummyLogClassResult]
ADD CONSTRAINT [FK_l_dummyLogClassResult_c_Classification]
    FOREIGN KEY ([c_Classification_id])
    REFERENCES [EQUI].[c_Classification]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_l_dummyLogClassResult_c_Classification'
CREATE INDEX [IX_FK_l_dummyLogClassResult_c_Classification]
ON [EQUI].[l_dummyLogClassResult]
    ([c_Classification_id]);
GO

-- Creating foreign key on [c_Subgroup_id] in table 'l_dummyLogClassResult'
ALTER TABLE [EQUI].[l_dummyLogClassResult]
ADD CONSTRAINT [FK_l_dummyLogClassResult_c_Subgroup]
    FOREIGN KEY ([c_Subgroup_id])
    REFERENCES [EQUI].[c_Subgroup]
        ([id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_l_dummyLogClassResult_c_Subgroup'
CREATE INDEX [IX_FK_l_dummyLogClassResult_c_Subgroup]
ON [EQUI].[l_dummyLogClassResult]
    ([c_Subgroup_id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------