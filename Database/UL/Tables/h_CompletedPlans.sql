CREATE TABLE [UL].[h_CompletedPlans] (
    [id]                  INT           IDENTITY (1, 1) NOT NULL,
    [InspectionPlanname]  CHAR (50)     NULL,
    [planStartDT]         DATETIME      NULL,
    [planEndDT]           DATETIME      NULL,
    [Inspector]           NCHAR (25)    NULL,
    [InspectionLaptop]    VARCHAR (255) NULL,
    [PlanLenght]          INT           NULL,
    [TotalMeasurements]   INT           NULL,
    [UniqueMeasurePoints] INT           NULL,
    [Remarks]             INT           NULL,
    CONSTRAINT [PK_h_CompletedPlans] PRIMARY KEY CLUSTERED ([id] ASC)
);

