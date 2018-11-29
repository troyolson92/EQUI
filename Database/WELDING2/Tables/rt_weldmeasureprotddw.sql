CREATE TABLE [WELDING2].[rt_weldmeasureprotddw] (
    [id]                            INT             IDENTITY (1, 1) NOT NULL,
    [timerId]                       INT             NULL,
    [_timestamp]                    DATETIME        NULL,
    [protRecord_ID]                 INT             NULL,
    [dateTime]                      DATETIME        NULL,
    [progNo]                        SMALLINT        NULL,
    [spotId]                        INT             NULL,
    [wear]                          NUMERIC (12, 2) NULL,
    [wearPerCent]                   NUMERIC (8, 2)  NULL,
    [monitorState]                  INT             NULL,
    [monitorState_txt]              VARCHAR (64)    NULL,
    [regulationState]               INT             NULL,
    [regulationState_txt]           VARCHAR (64)    NULL,
    [measureState]                  INT             NULL,
    [measureState_txt]              VARCHAR (64)    NULL,
    [powerState]                    INT             NULL,
    [powerState_txt]                VARCHAR (64)    NULL,
    [sequenceState]                 INT             NULL,
    [sequenceState_txt]             VARCHAR (64)    NULL,
    [sequenceStateAdd]              INT             NULL,
    [sequenceStateAdd_txt]          VARCHAR (64)    NULL,
    [sequenceRepeat]                INT             NULL,
    [sequenceRepeat_txt]            VARCHAR (64)    NULL,
    [monitorMode]                   INT             NULL,
    [monitorMode_txt]               VARCHAR (64)    NULL,
    [iDemandStd]                    NUMERIC (8, 2)  NULL,
    [ilsts]                         NUMERIC (8, 2)  NULL,
    [regulationStd]                 INT             NULL,
    [regulationStd_txt]             VARCHAR (64)    NULL,
    [iDemand1]                      NUMERIC (8, 2)  NULL,
    [iActual1]                      NUMERIC (8, 2)  NULL,
    [regulation1]                   INT             NULL,
    [regulation1_txt]               VARCHAR (64)    NULL,
    [iDemand2]                      NUMERIC (8, 2)  NULL,
    [iActual2]                      NUMERIC (8, 2)  NULL,
    [regulation2]                   INT             NULL,
    [regulation2_txt]               VARCHAR (64)    NULL,
    [iDemand3]                      NUMERIC (8, 2)  NULL,
    [iActual3]                      NUMERIC (8, 2)  NULL,
    [regulation3]                   INT             NULL,
    [regulation3_txt]               VARCHAR (64)    NULL,
    [phaStd]                        NUMERIC (8, 2)  NULL,
    [pha1]                          NUMERIC (8, 2)  NULL,
    [pha2]                          NUMERIC (8, 2)  NULL,
    [pha3]                          NUMERIC (8, 2)  NULL,
    [t_iDemandStd]                  NUMERIC (8, 2)  NULL,
    [tActualStd]                    NUMERIC (8, 2)  NULL,
    [partIdentString]               VARCHAR (16)    NULL,
    [tipDressCounter]               INT             NULL,
    [electrodeNo]                   INT             NULL,
    [voltageActualValue]            NUMERIC (8, 2)  NULL,
    [voltageRefValue]               NUMERIC (8, 2)  NULL,
    [currentActualValue]            NUMERIC (8, 2)  NULL,
    [currentReferenceValue]         NUMERIC (8, 2)  NULL,
    [weldTimeActualValue]           INT             NULL,
    [weldTimeRefValue]              INT             NULL,
    [energyActualValue]             REAL            NULL,
    [energyRefValue]                REAL            NULL,
    [powerActualValue]              REAL            NULL,
    [powerRefValue]                 REAL            NULL,
    [resistanceActualValue]         INT             NULL,
    [resistanceRefValue]            INT             NULL,
    [pulseWidthActualValue]         NUMERIC (8, 2)  NULL,
    [pulseWidthRefValue]            NUMERIC (8, 2)  NULL,
    [stabilisationFactorActValue]   NUMERIC (8, 2)  NULL,
    [stabilisationFactorRefValue]   NUMERIC (8, 2)  NULL,
    [thresholdStabilisationFactor]  NUMERIC (8, 2)  NULL,
    [wldEffectStabilisationFactor]  NUMERIC (8, 2)  NULL,
    [uipActualValue]                INT             NULL,
    [uipRefValue]                   INT             NULL,
    [uirExpulsionTime]              INT             NULL,
    [uirMeasuringActive]            INT             NULL,
    [uirMeasuringActive_txt]        VARCHAR (64)    NULL,
    [uirRegulationActive]           INT             NULL,
    [uirRegulationActive_txt]       VARCHAR (64)    NULL,
    [uirMonitoringActive]           INT             NULL,
    [uirMonitoringActive_txt]       VARCHAR (64)    NULL,
    [uirWeldTimeProlongationActive] INT             NULL,
    [uirWeldTimeProlongActive_txt]  VARCHAR (64)    NULL,
    [uirQStoppRefCntValue]          INT             NULL,
    [uirQStoppActCntValue]          INT             NULL,
    [uirUipUpperTol]                NUMERIC (8, 2)  NULL,
    [uirUipLowerTol]                NUMERIC (8, 2)  NULL,
    [uirUipCondTol]                 NUMERIC (8, 2)  NULL,
    [uirPsfLowerTol]                NUMERIC (8, 2)  NULL,
    [uirPsfCondTol]                 NUMERIC (8, 2)  NULL,
    CONSTRAINT [PK_RT_WeldMeasureProtDDW] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_rt_weldmeasureprotddw_c_timer] FOREIGN KEY ([timerId]) REFERENCES [WELDING2].[c_timer] ([ID])
);


GO
CREATE NONCLUSTERED INDEX [nci_Nut_bolt_measure2]
    ON [WELDING2].[rt_weldmeasureprotddw]([spotId] ASC, [dateTime] ASC)
    INCLUDE([timerId], [progNo], [wear], [iDemand2], [iActual2], [regulation2], [electrodeNo], [weldTimeActualValue], [energyActualValue], [energyRefValue], [uirExpulsionTime]);


GO
CREATE NONCLUSTERED INDEX [nci_Nut_bolt_measure]
    ON [WELDING2].[rt_weldmeasureprotddw]([timerId] ASC, [progNo] ASC);


GO
CREATE NONCLUSTERED INDEX [nci_MaxDateMidair]
    ON [WELDING2].[rt_weldmeasureprotddw]([progNo] ASC)
    INCLUDE([dateTime]);


GO
CREATE NONCLUSTERED INDEX [nci_VWSC_getMax_rt_weldmeasureprotddw]
    ON [WELDING2].[rt_weldmeasureprotddw]([timerId] ASC)
    INCLUDE([protRecord_ID]);

