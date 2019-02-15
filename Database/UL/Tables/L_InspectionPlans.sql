CREATE TABLE [UL].[L_InspectionPlans] (
    [id]                 INT       IDENTITY (1, 1) NOT NULL,
    [InspectionPlanname] CHAR (50) NULL,
    CONSTRAINT [PK_L_InspectionPlans] PRIMARY KEY CLUSTERED ([id] ASC)
);

