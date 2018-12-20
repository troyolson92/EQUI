CREATE TABLE [WELDING2].[rt_spottable] (
    [ID]                      INT            IDENTITY (1, 1) NOT NULL,
    [_timestamp]              DATETIME       NULL,
    [timerId]                 INT            NOT NULL,
    [SpotName]                VARCHAR (50)   NULL,
    [pjv_joiningpointdata_id] INT            NULL,
    [vwscComment]             VARCHAR (256)  NULL,
    [Zone]                    TINYINT        CONSTRAINT [DF_Spot_Zone] DEFAULT ((0)) NULL,
    [Comment1]                NVARCHAR (256) NULL,
    [Comment2]                NVARCHAR (256) NULL,
    [Comment3]                NVARCHAR (256) NULL,
    [PlateCombinationtId]     INT            CONSTRAINT [DF_Spot_PlateCombinationtId] DEFAULT ((0)) NOT NULL,
    [weldProgNo]              INT            NOT NULL,
    [ElectrodeDia]            TINYINT        CONSTRAINT [DF_Spot_ElectrodeDia] DEFAULT ((0)) NOT NULL,
    [AlternativeNumber]       NCHAR (25)     NULL,
    [Model]                   NCHAR (10)     NULL,
    [Variant]                 NCHAR (50)     NULL,
    [JobCode]                 INT            NULL,
    [NuggetDemand]            REAL           NULL,
    [HiddenSpot]              BIT            NULL,
    [GeoSpot]                 BIT            NULL,
    [SpotLeft]                INT            NULL,
    [SpotRight]               INT            NULL,
    [isDead]                  INT            NULL,
    CONSTRAINT [PK_welding2_spot] PRIMARY KEY CLUSTERED ([ID] ASC),
    CONSTRAINT [FK_rt_spottable_c_timer] FOREIGN KEY ([timerId]) REFERENCES [WELDING2].[c_timer] ([ID]),

);


GO





GO


