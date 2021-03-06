﻿CREATE TABLE [WELDING2].[h_weldmeasure] (
    [id]                        INT        IDENTITY (1, 1) NOT NULL,
    [L_timeline_starttime]      DATETIME   NULL,
    [L_timeline_endtime]        DATETIME   NULL,
    [L_timeline_id]             INT        NULL,
    [timerId]                   INT        NULL,
    [progNo]                    SMALLINT   NULL,
    [spotId]                    INT        NULL,
    [count]                     INT        NULL,
    [dt_FirstValue]             DATETIME   NULL,
    [dt_LastValue]              DATETIME   NULL,
    [Expulsions]                INT        NULL,
    [Rewelds]                   INT        NULL,
    [AVG_uirExpulsionTime]      FLOAT (53) NULL,
    [AVG_voltage]               FLOAT (53) NULL,
    [AVG_current]               FLOAT (53) NULL,
    [AVG_weldtime]              FLOAT (53) NULL,
    [AVG_enegery]               FLOAT (53) NULL,
    [AVG_power]                 FLOAT (53) NULL,
    [AVG_resistance]            FLOAT (53) NULL,
    [AVG_pulseWith]             FLOAT (53) NULL,
    [AVG_uip]                   FLOAT (53) NULL,
    [AVG_stabilisationFactor]   FLOAT (53) NULL,
    [MIN_uirExpulsionTime]      INT        NULL,
    [MIN_voltage]               FLOAT (53) NULL,
    [MIN_current]               FLOAT (53) NULL,
    [MIN_weldtime]              INT        NULL,
    [MIN_enegery]               FLOAT (53) NULL,
    [MIN_power]                 FLOAT (53) NULL,
    [MIN_resistance]            INT        NULL,
    [MIN_pulseWith]             FLOAT (53) NULL,
    [MIN_uip]                   INT        NULL,
    [MIN_stabilisationFactor]   FLOAT (53) NULL,
    [MAX_uirExpulsionTime]      INT        NULL,
    [MAX_voltage]               FLOAT (53) NULL,
    [MAX_current]               FLOAT (53) NULL,
    [MAX_weldtime]              INT        NULL,
    [MAX_enegery]               FLOAT (53) NULL,
    [MAX_power]                 FLOAT (53) NULL,
    [MAX_resistance]            INT        NULL,
    [MAX_pulseWith]             FLOAT (53) NULL,
    [MAX_uip]                   INT        NULL,
    [MAX_stabilisationFactor]   FLOAT (53) NULL,
    [STDEV_uirExpulsionTime]    FLOAT (53) NULL,
    [STDEV_voltage]             FLOAT (53) NULL,
    [STDEV_current]             FLOAT (53) NULL,
    [STDEV_weldtime]            FLOAT (53) NULL,
    [STDEV_enegery]             FLOAT (53) NULL,
    [STDEV_power]               FLOAT (53) NULL,
    [STDEV_resistance]          FLOAT (53) NULL,
    [STDEV_pulseWith]           FLOAT (53) NULL,
    [STDEV_uip]                 FLOAT (53) NULL,
    [STDEV_stabilisationFactor] FLOAT (53) NULL,
    CONSTRAINT [PK_h_weldmeasure] PRIMARY KEY CLUSTERED ([id] ASC)
);

