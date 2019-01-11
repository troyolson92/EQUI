﻿CREATE TABLE [NGAC].[c_controller] (
    [id]                        INT           IDENTITY (1, 1) NOT NULL,
    [controller_name]           VARCHAR (30)  NOT NULL,
    [enable_bit]                INT           NOT NULL,
    [systemId]                  VARCHAR (50)  NULL,
    [ip]                        VARCHAR (50)  NULL,
    [class_id]                  INT           NOT NULL,
    [flags]                     INT           NOT NULL,
    [LocationTree]              VARCHAR (MAX) NULL,
    [Assetnum]                  VARCHAR (MAX) NULL,
    [ProductionTeam]            VARCHAR (MAX) NULL,
    [ResponsibleTechnicianTeam] VARCHAR (50)  NULL,
    [ResponsibleProductionTeam] VARCHAR (50)  NULL,
    [ClassificationTree]        VARCHAR (MAX) NULL,
    [CLassificationId]          VARCHAR (MAX) NULL,
    [hasRackidAsBodynum]        BIT           NOT NULL,
    [hasSpotweld]               BIT           NOT NULL,
    [hasTipchanger]             BIT           NOT NULL,
    [Asset_x]                   INT           NULL,
    [Asset_y]                   INT           NULL,
    [Asset_png]                 VARCHAR (50)  NULL,
    [Station_x]                 INT           NULL,
    [Station_y]                 INT           NULL,
    [Station_png]               VARCHAR (50)  NULL,
    [Line_x]                    INT           NULL,
    [Line_y]                    INT           NULL,
    [Line_png]                  VARCHAR (50)  NULL,
    [Station]                   VARCHAR (50)  NULL,
    [Line]                      VARCHAR (50)  NULL,
    CONSTRAINT [PK_c_controller_1] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_c_controller_c_controller_class] FOREIGN KEY ([class_id]) REFERENCES [NGAC].[c_controller_class] ([id])
);





