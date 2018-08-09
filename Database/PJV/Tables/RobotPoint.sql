﻿CREATE TABLE [PJV].[RobotPoint] (
    [id]             INT           IDENTITY (1, 1) NOT NULL,
    [isDead]         INT           NULL,
    [_timestamp]     DATETIME      NULL,
    [_lasttimestamp] DATETIME      NULL,
    [v_name]         VARCHAR (MAX) NULL,
    [robotRoutineId] INT           NULL,
    [datatype]       INT           NULL,
    [X]              FLOAT (53)    NULL,
    [Y]              FLOAT (53)    NULL,
    [Z]              FLOAT (53)    NULL,
    [switchon]       INT           NULL,
    [processValue1]  VARCHAR (50)  NULL,
    [processValue2]  VARCHAR (50)  NULL,
    [frame]          VARCHAR (50)  NULL,
    [tool]           VARCHAR (50)  NULL,
    [vel]            VARCHAR (50)  NULL,
    [remote_tool]    INT           NULL,
    [spd]            VARCHAR (50)  NULL,
    [traj]           VARCHAR (50)  NULL,
    [zone]           VARCHAR (50)  NULL,
    [programLine]    INT           NULL,
    [process_s]      VARCHAR (50)  NULL,
    [rev_created]    INT           NULL,
    [rev_modified]   INT           NULL,
    [pp_comment]     VARCHAR (200) NULL,
    [R_VX_1]         FLOAT (53)    NULL,
    [R_VX_2]         FLOAT (53)    NULL,
    [R_VX_3]         FLOAT (53)    NULL,
    [R_VY_1]         FLOAT (53)    NULL,
    [R_VY_2]         FLOAT (53)    NULL,
    [R_VY_3]         FLOAT (53)    NULL,
    [R_VZ_1]         FLOAT (53)    NULL,
    [R_VZ_2]         FLOAT (53)    NULL,
    [R_VZ_3]         FLOAT (53)    NULL,
    CONSTRAINT [PK_RobotPoint] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_RobotPoint_RobotRoutine] FOREIGN KEY ([robotRoutineId]) REFERENCES [PJV].[RobotRoutine] ([id])
);

