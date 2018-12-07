CREATE TABLE [WELDING2].[rt_weldmeasureprotddw] (
    [id]                            INT             IDENTITY (1, 1) NOT NULL,
    [timerId]                       INT             NULL,
    [_timestamp]                    DATETIME        NULL,
    [protRecord_ID]                 INT             NULL,
    [dateTime]                      DATETIME        NULL,
    [progNo]                        SMALLINT        NULL,
    [rt_spot_id]                    INT             NULL,
    [wear]                          NUMERIC (12, 2) NULL,
    [wearPerCent]                   NUMERIC (8, 2)  NULL,
    [monitorState]                  INT             NULL,
    [regulationState]               INT             NULL,
    [measureState]                  INT             NULL,
    [powerState]                    INT             NULL,
    [sequenceState]                 INT             NULL,
    [sequenceStateAdd]              INT             NULL,
    [sequenceRepeat]                INT             NULL,
    [monitorMode]                   INT             NULL,
    [iDemandStd]                    NUMERIC (8, 2)  NULL,
    [ilsts]                         NUMERIC (8, 2)  NULL,
    [regulationStd]                 INT             NULL,
    [iDemand1]                      NUMERIC (8, 2)  NULL,
    [iActual1]                      NUMERIC (8, 2)  NULL,
    [regulation1]                   INT             NULL,
    [iDemand2]                      NUMERIC (8, 2)  NULL,
    [iActual2]                      NUMERIC (8, 2)  NULL,
    [regulation2]                   INT             NULL,
    [iDemand3]                      NUMERIC (8, 2)  NULL,
    [iActual3]                      NUMERIC (8, 2)  NULL,
    [regulation3]                   INT             NULL,
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
    [uirRegulationActive]           INT             NULL,
    [uirMonitoringActive]           INT             NULL,
    [uirWeldTimeProlongationActive] INT             NULL,
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
    ON [WELDING2].[rt_weldmeasureprotddw]([rt_spot_id] ASC, [dateTime] ASC)
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

