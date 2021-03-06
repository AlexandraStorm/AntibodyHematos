IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CheckTailHematosAb]'))
BEGIN
DROP Function [dbo].[CheckTailHematosAb]
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create FUNCTION [dbo].[CheckTailHematosAb] (@batchID NVARCHAR(100), @sampleID NVARCHAR(100))
   RETURNS Int
AS
   BEGIN
     DECLARE @ret int, @locusID int

	  DECLARE @AntibodyID INT

	  SET @AntibodyID = (SELECT AntibodyID FROM dbo.tbAntibodyMethod where sessionID = @batchID and sampleID = @sampleID)

	  SET @locusID = (SELECT locusID from tbAntibodyMethod INNER JOIN dbo.tbLogic ON dbo.tbAntibodyMethod.logicID = dbo.tbLogic.logicID where sessionID = @batchID AND sampleID = @sampleID)

	  IF @locusID <> 14 -- Ignore LMX samples
	  BEGIN
			IF @locusID = 21 OR @locusID = 22
			BEGIN
			IF ((select COUNT(*) FROM tbAntibodyData where AntibodyID = @AntibodyID and assignment = 'Positive' and Bead <> '77' and Bead NOT like 'CON%') > 0 and (Select COUNT(*) FROM tbAntibodyStats where AntibodyID = @AntibodyID and tail > 0) = 0)
			BEGIN
				SELECT @ret = 1
			END
			ELSE
			BEGIN
				SELECT @ret = 0
			END
			END
			ELSE
			BEGIN
			IF ((select COUNT(*) FROM tbAntibodyData where AntibodyID = @AntibodyID and assignment = 'Positive') > 0 and (Select COUNT(*) FROM tbAntibodyStats where AntibodyID = @AntibodyID and tail > 0) = 0)
			BEGIN
				SELECT @ret = 1
			END
			ELSE
			BEGIN
				SELECT @ret = 0
			END
			END
		END
		ELSE
		BEGIN
			SELECT @ret = 0
		END
      RETURN @ret
   END
GO