CREATE TABLE [UL].[h_measurementRemarks] (
    [id]                 INT           IDENTITY (1, 1) NOT NULL,
    [CompletedPlans_id]  INT           NULL,
    [spotname]           CHAR (50)     NULL,
    [EvaluationClass]    CHAR (50)     NULL,
    [Autocomment]        CHAR (30)     NULL,
    [InspectorComment]   CHAR (50)     NULL,
    [Partname]           CHAR (50)     NULL,
    [measuredThickness]  FLOAT (53)    NULL,
    [NominalDiameter]    FLOAT (53)    NULL,
    [ULDateTime]         DATETIME      NULL,
    [InspectionPlanname] CHAR (50)     NULL,
    [InspectionLaptop]   VARCHAR (255) NULL,
    [Inspector]          NCHAR (25)    NULL,
    CONSTRAINT [PK_h_measurementRemarks] PRIMARY KEY CLUSTERED ([id] ASC),
    CONSTRAINT [FK_h_measurementRemarks_h_CompletedPlans] FOREIGN KEY ([CompletedPlans_id]) REFERENCES [UL].[h_CompletedPlans] ([id])
);

