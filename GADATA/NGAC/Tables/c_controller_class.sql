﻿CREATE TABLE [NGAC].[c_controller_class] (
    [id]                                INT          IDENTITY (1, 1) NOT NULL,
    [name]                              VARCHAR (30) NOT NULL,
    [doConnect]                         BIT          NULL,
    [evStateChange]                     INT          NULL,
    [evOperatingModeChange]             INT          NULL,
    [evConnectionChange]                INT          NULL,
    [evExecutionStatus]                 INT          NULL,
    [evExecutionStatusTRob1]            INT          NULL,
    [evBackupCompleted]                 INT          NULL,
    [evDataResolveChange]               INT          NULL,
    [evExecutionCycleChange]            INT          NULL,
    [evTaskEnabledChange]               INT          NULL,
    [evMasterChange]                    INT          NULL,
    [evMotionPointerTRob1Change]        INT          NULL,
    [evProgramPointerTRob1Change]       INT          NULL,
    [evMotionPointerTRob1ManualChange]  INT          NULL,
    [evProgramPointerTRob1ManualChange] INT          NULL,
    [cVariableMask]                     INT          NULL,
    [cVariableSearchMask]               INT          NULL,
    [cDeviceInfoMask]                   INT          NULL,
    [cCSVLogMask]                       INT          NULL,
    [cJobMask]                          INT          NULL,
    [logCategoryMask]                   INT          NULL,
    [handleHSocket]                     BIT          NULL,
    [Username]                          VARCHAR (50) NULL,
    [Password]                          VARCHAR (50) NULL,
    [setClock]                          INT          NULL,
    [evLogMessageAction]                INT          NULL,
    CONSTRAINT [PK_c_controller_class_1] PRIMARY KEY CLUSTERED ([id] ASC)
);



