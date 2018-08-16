
CREATE FUNCTION [NGAC].[VASCstate](@state int) RETURNS VARCHAR(MAX)
AS
BEGIN
IF (@state = 0)
BEGIN
  RETURN 'Done nothing (startup)' 
END
IF (@state = 1)
BEGIN
  RETURN 'Connected' 
END
IF (@state = 2)
BEGIN
  RETURN 'Connecting' 
END
IF (@state = -1)
BEGIN
  RETURN 'no connection (abb error)' 
END
IF (@state = -2)
BEGIN
  RETURN 'Lost connection' 
END
IF (@state = -3)
BEGIN
  RETURN 'No ping' 
END
IF (@state = -4)
BEGIN
  RETURN 'No name' 
END
IF (@state = -5)
BEGIN
  RETURN 'No ip or sysid (check config)' 
END
IF (@state = -6)
BEGIN
  RETURN 'New controller' 
END
IF (@state = -7)
BEGIN
  RETURN 'Bad format' 
END
IF (@state = -8)
BEGIN
  RETURN 'This is not an NGAC robot! (check config)' 
END
IF (@state = -9)
BEGIN
  RETURN 'no info' 
END
IF (@state = -10)
BEGIN
  RETURN 'run level' 
END
IF (@state = -11)
BEGIN
  RETURN 'avail' 
END
IF (@state = -12)
BEGIN
  RETURN 'bad ip (check config)' 
END
IF (@state = -13)
BEGIN
  RETURN 'SYS FAILURE STATE!' 
END
IF (@state = -14)
BEGIN
  RETURN 'NO PCSDK session available! (please check who is connected to this robot)' 
END
IF (@state = -15)
BEGIN
  RETURN 'VASC shutdown (session has been stopped)' 
END


RETURN 'UNKNOWN state: ' + cast(@state as varchar(max))  + ' (please update NGAC.[VASCstate] function)'
END