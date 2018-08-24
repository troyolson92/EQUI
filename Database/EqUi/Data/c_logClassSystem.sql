print 'init check [EqUi].[c_logClassSystem]'
IF (SELECT count(*) FROM  [EqUi].[c_logClassSystem]) = 0
BEGIN
Print 'init data insert [EqUi].[c_logClassSystem]'
SET IDENTITY_INSERT [EqUi].[c_logClassSystem] ON 

INSERT [EqUi].[c_logClassSystem] ([id], [c_datasource_id], [Name], [Description], [SelectStatement], [UpdateStatement], [RunRuleStatement]) VALUES (8, 1, N'VASC_NGAC', N'ABB ngac gneration robot. (data in ngac.L_error)', 
N'SELECT L_error._id as ''id''
      ,L_error.Number as ''code''
      ,L_error.Title + ' ' + l_description.Description as ''text''
      ,L_error.c_RuleId as  ''c_logcClassRules_id''
      ,L_error.c_ClassificationId as ''c_Classification_id''
      ,L_error.c_SubgroupId as ''c_Subgroup_id''
  FROM NGAC.L_error 
  left join NGAC.L_description on L_description.id = L_error.l_description_id ', 
  
N'  UPDATE GADATA.NGAC.l_error
SET  c_ClassificationID =  CASE  
							WHEN @Clear = 0 THEN @c_ClassificationId 
							ELSE NULL
						    END ,
           c_SubgroupId =  CASE  
							WHEN @Clear = 0 THEN @c_SubgroupId 
							ELSE NULL
						    END,
	           c_RuleId =   CASE  
							WHEN @Clear = 0 THEN -1 
							ELSE NULL
						    END
 FROM NGAC.L_error
 left join NGAC.L_description on L_description.id = L_error.l_description_id
  WHERE  
  --Group update
  (
  L_error.Title + ' ' + l_description.Description like @textSearch
  AND 
  L_error.Number between @coderangeStart and @coderangeEnd
  AND
  @rowID = 0
  )
  --single set 
  OR
  (
  L_error._id = @rowID
  AND
  @rowID <> 0 
  )', 
  
  N'  UPDATE GADATA.NGAC.l_error
SET  c_ClassificationID =  CASE  
							WHEN @Clear = 0 THEN r.c_ClassificationId 
							ELSE NULL
						    END ,
           c_SubgroupId =  CASE  
							WHEN @Clear = 0 THEN r.c_SubgroupId 
							ELSE NULL
						    END,
	           c_RuleId =   CASE  
							WHEN @Clear = 0 THEN ISNULL(r.id,0)
							ELSE NULL
						    END

  FROM NGAC.L_error as L
  left join NGAC.L_description on L_description.id = L.l_description_id
  left join EQUI.c_LogClassRules as r on
  (
  r.c_logClassSystem_id = @logClassSystem_id
  AND
  L.Title + ' ' + l_description.Description like ISNULL(r.textSearch,'%')
  AND 
  l.Number between ISNULL(r.coderangeStart,0) and ISNULL(r.coderangeEnd,1000000)
  )
  WHERE
  --handle single rule run 
  (
  r.id = @ruleID --single rule
  OR
  @ruleID = 0 --run all
  )
  AND --handle overrideManualSet
  (
  L.c_RuleId <> -1
  OR 
  L.c_RuleId is null 
  OR
  @overrideManualSet = 1
  )
  AND --handle update (reapply rule)
  (
  L.c_RuleId is null
  OR 
  @UPDATE = 1
  )

  

  
')
SET IDENTITY_INSERT [EqUi].[c_logClassSystem] OFF
END