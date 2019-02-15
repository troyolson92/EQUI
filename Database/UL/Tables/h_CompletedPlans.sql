CREATE TABLE [UL].[h_CompletedPlans] (
    [id]                  INT           IDENTITY (1, 1) NOT NULL,
    [InspectionPlan_id]   INT           NOT NULL,
    [planStartDT]         DATETIME      NULL,
    [planEndDT]           DATETIME      NULL,
    [Inspector]           NCHAR (25)    NULL,
    [InspectionLaptop]    VARCHAR (255) NULL,
    [PlanLenght]          INT           NULL,
    [TotalMeasurements]   INT           NULL,
    [UniqueMeasurePoints] INT           NULL,
    [Remarks]             INT           NULL,
    CONSTRAINT [PK_h_CompletedPlans] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_h_CompletedPlans_L_InspectionPlans] FOREIGN KEY ([InspectionPlan_id]) REFERENCES [UL].[L_InspectionPlans] ([id])
);



