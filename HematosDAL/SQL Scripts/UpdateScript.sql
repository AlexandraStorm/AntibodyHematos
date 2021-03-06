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

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLotsHematosAb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetLotsHematosAb]
GO

/****** Object:  StoredProcedure [dbo].[GetLotsHematosAb]    Script Date: 1/10/2015 1:30:17 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- -----------------------------------------------------------------------------
-- <summary>
-- Return Available Lots for Hemaots Ab Export
-- </summary>
-- -----------------------------------------------------------------------------
Create PROCEDURE [dbo].[GetLotsHematosAb]	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	SET NOCOUNT ON;

		SELECT tbAntibodyLot.LotID, AssayName
	FROM tbAntibodyLot INNER JOIN
						  (SELECT DISTINCT lotID, CONVERT(varchar(20), createdt, 101) AS importdate
							FROM          tbAntibodyExpSet) AS tbAntibodyExpSet_1 ON tbAntibodyLot.LotID = tbAntibodyExpSet_1.lotID
WHERE tbAntibodyLot.available <> 0 and (tbAntibodyLot.LotID like '%LMX') OR (tbAntibodyLot.LotID like '%LM2Q') OR (tbAntibodyLot.LotID like '%LM1') OR (tbAntibodyLot.LotID like '%SA1') OR (tbAntibodyLot.LotID like '%SA2') OR (tbAntibodyLot.LotID like '%SAM')
	ORDER BY  ExpirationDate DESC
END
GO



IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetBatchesHematosAb]') AND type in (N'P', N'PC'))
Begin
DROP PROCEDURE [dbo].[GetBatchesHematosAb]
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 12-18-14
-- Description:	Retrieves batches for export
-- =============================================
Create PROCEDURE [dbo].[GetBatchesHematosAb]
	-- Add the parameters for the stored procedure here
	@lotid nvarchar(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
SELECT [sessionID] AS [BatchID] FROM tbBatch WHERE [lotID] = @lotid ORDER BY createdt DESC
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetLumInfoHematosAb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetLumInfoHematosAb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 1-5-15
-- Description:	Retrieves Luminex Info for export
-- =============================================
Create PROCEDURE [dbo].[GetLumInfoHematosAb]
	-- Add the parameters for the stored procedure here
	@batchID nvarchar(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF (SELECT        Count(tbLuminexData.LuminexServer)
FROM            tbBatch INNER JOIN
						 tbLuminexData ON tbBatch.bServer = tbLuminexData.LuminexServer
						 WHERE tbBatch.batchName = @batchID ) >= 1
Begin
SELECT        tbBatch.batchName AS BatchID, tbLuminexData.SerialNumber, tbLuminexData.LuminexID AS LumID, tbLuminexData.LuminexServer
FROM            tbBatch INNER JOIN
						 tbLuminexData ON tbBatch.bServer = tbLuminexData.LuminexServer
						 WHERE tbBatch.batchName = @batchID
END
ELSE IF (SELECT        Count(tbLuminexData.SerialNumber)
FROM            tbBatch INNER JOIN
						 tbLuminexData ON tbBatch.bServer = tbLuminexData.SerialNumber
						 WHERE tbBatch.batchName = @batchID) = 0
Begin
	SELECT        batchName As BatchID, bServer As SerialNumber, submittedBy
FROM         tbBatch   
	where batchName = @batchID
END

IF (SELECT        Count(tbLuminexData.SerialNumber)
FROM            tbBatch INNER JOIN
						 tbLuminexData ON tbBatch.bServer = tbLuminexData.SerialNumber 
						 WHERE tbBatch.batchName = @batchID) >= 1
Begin
SELECT        tbBatch.batchName AS BatchID, tbLuminexData.SerialNumber, tbLuminexData.LuminexID AS LumID
FROM            tbBatch INNER JOIN
						 tbLuminexData ON tbBatch.bServer = tbLuminexData.SerialNumber
						 WHERE tbBatch.batchName = @batchID
END
END
GO

--==============================================
--tbHematosLociRules
--==============================================
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbHematosLociRules]'))
BEGIN
CREATE TABLE [dbo].[tbHematosLociRules](
	[Loci] varchar(10) NOT NULL,
	[Serology] varchar(10) NOT NULL,
	[AlleleName] varchar(20) NOT NULL,
	[selectedAllelicAll] varchar(35) NOT NULL,
	[useMedianRawValuesAll] varchar(50) NOT NULL,
	[selectedAllelicMany] varchar(35) NOT NULL,
	[useMedainRawValuesMany] varchar(50) NOT NULL,
	[SelectAllelicOne] varchar(35) NOT NULL,
	[useMedianRawValuesOne] varchar(50) NOT NULL
) ON [PRIMARY]
END
GO
--=============================================
--Load HematosLociRules
--=============================================
if((select count(*) from tbHematosLociRules) = 0)
BEGIN 
insert into tbHematosLociRules values('A','A1','A*01:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A11','A*11:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A11','A*11:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A2','A*02:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A2','A*02:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A2','A*02:05','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A2','A*02:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A23','A*23:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A24','A*24:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A24','A*24:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A25','A*25:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A26','A*26:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A29','A*29:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A29','A*29:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A3','A*03:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A30','A*30:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A31','A*31:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A32','A*32:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A33','A*33:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A33','A*33:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A34','A*34:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A36','A*36:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A43','A*43:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A66','A*66:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A66','A*66:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A68','A*68:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A68','A*68:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A69','A*69:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A74','A*74:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('A','A80','A*80:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B13','B*13:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B15','B*15:12','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B18','B*18:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B27','B*27:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B27','B*27:05','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B27','B*27:08','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B35','B*35:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B35','B*35:08','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B37','B*37:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B38','B*38:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B39','B*39:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B41','B*41:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B42','B*42:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B44','B*44:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B44','B*44:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B45','B*45:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B46','B*46:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B47','B*47:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B48','B*48:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B49','B*49:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B50','B*50:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B51','B*51:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B52','B*52:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B53','B*53:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B54','B*54:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B55','B*55:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B56','B*56:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B57','B*57:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B58','B*58:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B59','B*59:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B60','B*40:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B61','B*40:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B62','B*15:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B63','B*15:16','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B64','B*14:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B65','B*14:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B67','B*67:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B7','B*07:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B7','B*07:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B71','B*15:18','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B72','B*15:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B73','B*73:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B75','B*15:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B77','B*15:13','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B78','B*78:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B8','B*08:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B81','B*81:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('B','B82','B*82:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw1','C*01:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw10','C*03:04','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw12','C*12:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw14','C*14:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw15','C*15:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw16','C*16:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw17','C*17:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw18','C*18:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw2','C*02:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw4','C*04:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw4','C*04:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw5','C*05:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw6','C*06:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw7','C*07:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw7','C*07:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw8','C*08:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw8','C*08:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('C','Cw9','C*03:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPA1','DPA1*01:03','DPA1*01:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPA1','DPA1*02:01','DPA1*02:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPA1','DPA1*02:02','DPA1*02:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPA1','DPA1*03:01','DPA1*03:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPA1','DPA1*04:01','DPA1*04:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP9','DPB1*09:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP11','DPB1*11:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP13','DPB1*13:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP14','DPB1*14:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP15','DPB1*15:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP17','DPB1*17:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP18','DPB1*18:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP19','DPB1*19:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP28','DPB1*28:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP1','DPB1*01:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP2','DPB1*02:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP3','DPB1*03:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP4','DPB1*04:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP4','DPB1*04:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP5','DPB1*05:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DPB1','DP6','DPB1*06:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQA1','DQA1*01:01','DQA1*01:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQA1','DQA1*01:02','DQA1*01:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQA1','DQA1*01:03','DQA1*01:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQA1','DQA1*01:04','DQA1*01:04','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQA1','DQA1*02:01','DQA1*02:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQA1','DQA1*03:01','DQA1*03:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQA1','DQA1*03:02','DQA1*03:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQA1','DQA1*04:01','DQA1*04:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQA1','DQA1*05:01','DQA1*05:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQA1','DQA1*06:01','DQA1*06:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQB1','DQ2','DQB1*02:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQB1','DQ2','DQB1*02:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQB1','DQ4','DQB1*04:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQB1','DQ4','DQB1*04:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQB1','DQ5','DQB1*05:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQB1','DQ5','DQB1*05:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQB1','DQ5','DQB1*05:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQB1','DQ6','DQB1*06:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQB1','DQ6','DQB1*06:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQB1','DQ6','DQB1*06:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQB1','DQ6','DQB1*06:04','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQB1','DQ7','DQB1*03:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQB1','DQ8','DQB1*03:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DQB1','DQ9','DQB1*03:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR1','DRB1*01:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR1','DRB1*01:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR10','DRB1*10:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR103','DRB1*01:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR11','DRB1*11:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR11','DRB1*11:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR11','DRB1*11:04','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR12','DRB1*12:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR12','DRB1*12:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR13','DRB1*13:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR13','DRB1*13:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR13','DRB1*13:05','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR14','DRB1*14:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR14','DRB1*14:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR14','DRB1*14:04','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR15','DRB1*15:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR15','DRB1*15:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR15','DRB1*15:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR16','DRB1*16:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR16','DRB1*16:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR17','DRB1*03:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR18','DRB1*03:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR18','DRB1*03:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR4','DRB1*04:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR4','DRB1*04:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR4','DRB1*04:03','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR4','DRB1*04:04','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR4','DRB1*04:05','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR7','DRB1*07:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR8','DRB1*08:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR8','DRB1*08:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB1','DR9','DRB1*09:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB3','DR52','DRB3*01:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB3','DR52','DRB3*02:02','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB3','DR52','DRB3*03:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB4','DR53','DRB4*01:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB5','DR51','DRB5*01:01','Serology','BCM','Allelic','BCM','Allelic','BCM')
insert into tbHematosLociRules values('DRB5','DR51','DRB5*02:02','Serology','BCM','Allelic','BCM','Allelic','BCM')

END 
GO
-- =============================================
--tbHematosABExportHistory table
-- =============================================
IF  NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbHematosABExportHistory]'))
BEGIN
CREATE TABLE [dbo].[tbHematosABExportHistory](
	[FileName] nvarchar(300) NOT NULL,
	[ExportDate] DateTime NOT NULL,
	[LotID] varchar(30) NOT NULL,
	[BatchID] nvarchar(100) NOT NULL,
	[sampleID] nvarchar(100) NOT NULL,
	[LuminexID] varchar(20) NOT NULL,
	[SiteCode] varchar(2) NOT NULL,
	[SpecCode] varchar(20) NULL,
	[Comments] NVARCHAR(1024) NULL
) ON [PRIMARY]
END
GO

IF NOT EXISTS (Select * FROM sys.columns where Name = N'SpecCode' and Object_ID = Object_ID(N'[dbo].[tbHematosABExportHistory]')) 
Begin
ALTER TABLE [dbo].[tbHematosABExportHistory]
 ADD [SpecCode] varchar(20) NULL
End
GO

IF NOT EXISTS (Select * FROM sys.columns where Name = N'Comments' and Object_ID = Object_ID(N'[dbo].[tbHematosABExportHistory]')) 
Begin
ALTER TABLE [dbo].[tbHematosABExportHistory]
  ADD [Comments] NVARCHAR(1024) NULL
End
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSamplesHematosAb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSamplesHematosAb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 1-7-15
-- Description:	Retrieves batches for export
-- =============================================
Create PROCEDURE [dbo].[GetSamplesHematosAb]
	-- Add the parameters for the stored procedure here
	@batchID nvarchar(100),
	@Processed int
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	CREATE TABLE #HematosSamples
	(
		BatchID NVARCHAR(100),
		sampleID NVARCHAR(100)
	);

	CREATE TABLE #HematosSamplesRET
	(
		BatchID NVARCHAR(100),
		sampleID NVARCHAR(100),
		CheckSpecification INT
	);
	
	IF @Processed = 0
		BEGIN
		INSERT INTO #HematosSamples 
			SELECT [sessionID] AS [BatchID], [sampleID]
				FROM tbAntibodyMethod WHERE [sessionID] = @batchID and [approved] = 1
			EXCEPT
			SELECT [BatchID], [sampleID]
				FROM tbHematosABExportHistory WHERE [BatchID] = @batchID

				INSERT INTO #HematosSamplesRET SELECT BatchID, sampleID, dbo.CheckTailHematosAb(BatchID, sampleID) as CheckSpecification 
					FROM #HematosSamples

					SELECT * FROM #HematosSamplesRET
				
		END
		ELSE
		BEGIN
		INSERT INTO #HematosSamples 
			SELECT [sessionID] AS [BatchID], [sampleID]
				FROM tbAntibodyMethod WHERE sampleID IN (
			SELECT [sampleID]
			 FROM tbHematosABExportHistory WHERE [BatchID] = @batchID) and sessionID = @batchID

			 INSERT INTO #HematosSamplesRET SELECT BatchID, sampleID, dbo.CheckTailHematosAb(BatchID, sampleID) as CheckSpecification 
					FROM #HematosSamples

					SELECT * FROM #HematosSamplesRET	 
		END
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetExportedHematosAbData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetExportedHematosAbData]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 11-11-15
-- Description:	Retrieves previous data for samples already exported
-- =============================================
Create PROCEDURE [dbo].[GetExportedHematosAbData]
	-- Add the parameters for the stored procedure here
	@batchID nvarchar(100),
	@sampleID nvarchar(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	SELECT [sampleID], [SpecCode], [Comments]
			 FROM tbHematosABExportHistory WHERE [BatchID] = @batchID and [sampleID] = @sampleID
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetUserNameHematosAb]'))
DROP Function [dbo].[GetUserNameHematosAb]
GO

/****** Object:  UserDefinedFunction [dbo].[GetUserNameHematosAb]    Script Date: 1/11/2015 4:52:22 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create FUNCTION [dbo].[GetUserNameHematosAb] (@UserID INT)
   RETURNS VARCHAR (55)
AS
   BEGIN
	  DECLARE @nRet   VARCHAR (55)

	  SET @nRet =
			 (SELECT UserName
			FROM dbo.tbUser
			WHERE dbo.tbUser.UserID = @UserID)
	  RETURN @nRet
   END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSampleInfo_IDHematosAb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSampleInfo_IDHematosAb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 1-11-15
-- Description:	Retrieves ID sample results for export
-- =============================================
Create PROCEDURE [dbo].[GetSampleInfo_IDHematosAb]
	-- Add the parameters for the stored procedure here
	@BatchID nvarchar(100),
	@SampleID nvarchar(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

IF (SELECT  count(tbAntibodyMethod.sampleID)
FROM            dbo.tbAntibodyStats  INNER JOIN
						 dbo.tbAntibodyMethod ON tbAntibodyMethod.AntibodyID = tbAntibodyStats.AntibodyID					
						  Where sessionID  = @BatchID and sampleID = @SampleID and (dbo.tbAntibodyStats.tail > 0)) = 0
						  Begin
						  SELECT  tbAntibodyMethod.sessionID AS batchID, tbAntibodyMethod.sampleID, dbo.GetUserNameHematosAb(tbAntibodyMethod.completedBy) AS completedBy, 
						 CONVERT(varchar(11), tbAntibodyMethod.CompletedDt, 103) AS completedDt, dbo.GetUserNameHematosAb(tbAntibodyMethod.approvedBy) AS approvedBy, tbAntibodyMethod.comments
FROM                      dbo.tbAntibodyMethod		
						  Where sessionID  = @BatchID and sampleID = @SampleID
						  END


Else 
		Begin

		SELECT  tbAntibodyMethod.sessionID AS batchID, tbAntibodyMethod.sampleID, tbAntibodyStats.antigen, dbo.GetUserNameHematosAb(tbAntibodyMethod.completedBy) AS completedBy, 
						 CONVERT(varchar(11), tbAntibodyMethod.CompletedDt, 103) AS completedDt, dbo.GetUserNameHematosAb(tbAntibodyMethod.approvedBy) AS approvedBy, CAST(Round(tbAntibodyStats.strength, 0) as int) AS [Strength], tbAntibodyMethod.comments
FROM            dbo.tbAntibodyStats  INNER JOIN
						 dbo.tbAntibodyMethod ON tbAntibodyMethod.AntibodyID = tbAntibodyStats.AntibodyID					
						  Where sessionID  = @BatchID and sampleID = @SampleID and (dbo.tbAntibodyStats.tail > 0)
						  Order By dbo.tbAntibodyStats.antigen
		END
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSampleInfo_LMXHematosAb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSampleInfo_LMXHematosAb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 1-13-15
-- Description:	Retrieves LMX sample results for export
-- =============================================
Create PROCEDURE [dbo].[GetSampleInfo_LMXHematosAb]
	-- Add the parameters for the stored procedure here
	@BatchID nvarchar(100),
	@SampleID nvarchar(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

SELECT Distinct tbAntibodyMethod.sessionID AS [Batch Name], tbAntibodyMethod.sampleID, tbAntibodyMethod.lotID, ClassI.result AS ClassIResults, 
						 ClassII.result AS ClassIIResults, tbAntibodyMethod.comments, dbo.[GetUserNameHematosAb](tbAntibodyMethod.completedBy) As completedBy, CONVERT(varchar(11), tbAntibodyMethod.CompletedDt, 103) As completedDt, dbo.[GetUserNameHematosAb](tbAntibodyMethod.approvedBy) As approvedBy
FROM            tbAntibodyMethod INNER JOIN
						 tbAntibodyResults AS ClassI ON tbAntibodyMethod.AntibodyID = ClassI.AntibodyID INNER JOIN
						 tbAntibodyResults AS ClassII ON tbAntibodyMethod.AntibodyID = ClassII.AntibodyID
						 Where tbAntibodyMethod.sessionID = @BatchID and tbAntibodyMethod.sampleID = @SampleID and ClassI.resultType = 'ClassI' and ClassII.resultType = 'ClassII'
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetSampleInfo_SAHematosAb]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[GetSampleInfo_SAHematosAb]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 1-15-15
-- Description:	Retrieves SA sample results for export
-- =============================================
CREATE PROCEDURE  [dbo].[GetSampleInfo_SAHematosAb]
	-- Add the parameters for the stored procedure here
	@BatchID nvarchar(100),
	@SampleID nvarchar(100)
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;


DECLARE @CONCount int, @AntibodyID INT, @versionNum INT

SET @versionNum = (SELECT [Version] FROM dbo.tbAntibodyMethod where sessionID = @batchID and sampleID = @sampleID)


SET @AntibodyID = (SELECT AntibodyID FROM dbo.tbAntibodyMethod where sessionID = @batchID and sampleID = @sampleID)
SET	@CONCount =(select count(*) from tbAntibodyExpSet where lotid = (select distinct lotid from tbAntibodyMethod where (tbAntibodyMethod.sessionID = @batchID)) and consensus <> '0')


If @versionNum = 1
	BEGIN
	If @CONCount = 3
		BEGIN	
			IF (SELECT  count(sampleID) FROM [dbo].[SingleAntigenExportView] where assignment = 'Positive' and [sampleID] = @SampleID and [Batch Name] = @BatchID) = 0 OR (Select COUNT(*) FROM tbAntibodyStats where AntibodyID = @AntibodyID and tail > 0) = 0
				Begin
				SELECT Distinct [Batch Name]
				  ,[sampleID]
				  ,dbo.[GetUserNameHematosAb](completedBy) As completedBy
				  ,dbo.[GetUserNameHematosAb](approvedBy) As approvedBy
				  ,CONVERT(varchar(11), [CompletedDt], 103) As completedDt
				  ,comments
				FROM [dbo].[SingleAntigenExportView] where [sampleID] = @SampleID and [Batch Name] = @BatchID
				END
			Else 
				Begin
					SELECT DISTINCT	tbAntibodyMethod.sessionID, tbAntibodyMethod.sampleID, tbAntibodyMethod.lotID, 
					dbo.[GetUserNameHematosAb](tbAntibodyMethod.completedBy) As completedBy , dbo.[GetUserNameHematosAb](tbAntibodyMethod.approvedBy) As approvedBy , CONVERT(varchar(11), tbAntibodyMethod.CompletedDt, 103) As completedDt, tbAntibodyMethod.comments, tbAntibodyStats.antigen, 
						tbAntibodyStats.tail, 	 CAST(Round(tbAntibodyStats.strength, 0) AS INT) AS strength,
						(CASE WHEN tbAntibodyCalculations.IsStandard = 1 THEN CAST(Round(dbo.GetBCM(sessionid,sampleid,antigen), 0) AS INT) ELSE 0 END) AS adjust1,
						tbHematosLociRules.serology, tbAntibodyData.rawValue, tbantibodyData.assignment,
						tbHematosLociRules.selectedAllelicAll as SELECTALL, tbHematosLociRules.useMedianRawValuesAll as USEALL, 
						tbHematosLociRules.selectedAllelicMany as SELECTMANY, tbHematosLociRules.useMedainRawValuesMany as USEMANY,
						tbHematosLociRules.selectAllelicOne as SELECTONE, tbHematosLociRules.useMedianRawValuesOne as USEONE,
						tbAntigens.AntigenSortOrder, tbAntigens.SerologySortOrder, CAST(Round(tbAntibodyData.adjust1,0) as INT) as ADJ
				FROM    tbAntibodyStats INNER JOIN
						tbAntibodyMethod ON tbAntibodyStats.AntibodyID = tbAntibodyMethod.AntibodyID INNER JOIN
						tbAntibodyData ON tbAntibodyMethod.AntibodyID = tbAntibodyData.AntibodyID INNER JOIN
						tbHematosLociRules ON tbAntibodyStats.Antigen = tbHematosLociRules.allelename INNER JOIN
						tbAntibodyCalculations ON tbAntibodyMethod.CalcUsed = tbAntibodyCalculations.CalculationID INNER JOIN
						tbAlleleProbeVals ON tbAntibodyData.bead = tbAlleleProbeVals.probeName 
										  AND tbAntibodyStats.antigen = tbAlleleProbeVals.allele 
										  AND tbAntibodyMethod.logicID = tbAlleleProbeVals.logicId INNER JOIN
							tbAntigens ON tbAlleleProbeVals.allele = tbAntigens.AntigenName
				WHERE   tbAntibodyMethod.sessionID = @BatchID
				AND		tbAntibodyMethod.sampleID = @SampleID
				AND		tbAlleleProbeVals.positiveFlag = 1
				--AND		tbAntibodyStats.tail > 0
				ORDER by antigen
				END
			END
	Else
			IF (SELECT  count(sampleID) FROM [dbo].[SingleAntigenOncCONExportView] where assignment = 'Positive' and [sampleID] = @SampleID and [Batch Name] = @BatchID) = 0 OR (Select COUNT(*) FROM tbAntibodyStats where AntibodyID = @AntibodyID and tail > 0) = 0
				Begin
					SELECT Distinct [Batch Name]
					  ,[sampleID]
					  ,dbo.[GetUserNameHematosAb](completedBy) As completedBy
					  ,dbo.[GetUserNameHematosAb](approvedBy) As approvedBy
					  ,CONVERT(varchar(11), [CompletedDt], 103) As completedDt
					  ,comments
					FROM [dbo].[SingleAntigenOncCONExportView] where [sampleID] = @SampleID and [Batch Name] = @BatchID
				END
			Else 
				Begin
					SELECT DISTINCT	tbAntibodyMethod.sessionID, tbAntibodyMethod.sampleID, tbAntibodyMethod.lotID, 
							dbo.[GetUserNameHematosAb](tbAntibodyMethod.completedBy) As completedBy , dbo.[GetUserNameHematosAb](tbAntibodyMethod.approvedBy) As approvedBy , CONVERT(varchar(11), tbAntibodyMethod.CompletedDt, 103) As completedDt, tbAntibodyMethod.comments, tbAntibodyStats.antigen, 
							tbAntibodyStats.tail, 	 CAST(Round(tbAntibodyStats.strength, 0) AS INT) AS strength,
							(CASE WHEN tbAntibodyCalculations.IsStandard = 1 THEN CAST(Round(dbo.GetBCM(sessionid,sampleid,antigen), 0) AS INT) ELSE 0 END) AS adjust1,
							tbHematosLociRules.serology, tbAntibodyData.rawValue,  tbantibodyData.assignment,
						tbHematosLociRules.selectedAllelicAll as SELECTALL, tbHematosLociRules.useMedianRawValuesAll as USEALL, 
						tbHematosLociRules.selectedAllelicMany as SELECTMANY, tbHematosLociRules.useMedainRawValuesMany as USEMANY,
						tbHematosLociRules.selectAllelicOne as SELECTONE, tbHematosLociRules.useMedianRawValuesOne as USEONE,
						tbAntigens.AntigenSortOrder, tbAntigens.SerologySortOrder, CAST(Round(tbAntibodyData.adjust1,0) as INT) as ADJ
					FROM    tbAntibodyStats INNER JOIN
							tbAntibodyMethod ON tbAntibodyStats.AntibodyID = tbAntibodyMethod.AntibodyID INNER JOIN
							tbAntibodyData ON tbAntibodyMethod.AntibodyID = tbAntibodyData.AntibodyID INNER JOIN
							tbHematosLociRules ON tbAntibodyStats.Antigen = tbHematosLociRules.allelename INNER JOIN
							tbAntibodyCalculations ON tbAntibodyMethod.CalcUsed = tbAntibodyCalculations.CalculationID INNER JOIN
							tbAlleleProbeVals ON tbAntibodyData.bead = tbAlleleProbeVals.probeName 
											  AND tbAntibodyStats.antigen = tbAlleleProbeVals.allele 
											  AND tbAntibodyMethod.logicID = tbAlleleProbeVals.logicId	INNER JOIN
							tbAntigens ON tbAlleleProbeVals.allele = tbAntigens.AntigenName
					WHERE   tbAntibodyMethod.sessionID = @BatchID
					AND		tbAntibodyMethod.sampleID = @SampleID
					AND		tbAlleleProbeVals.positiveFlag = 1
					--AND		tbAntibodyStats.tail > 0
					ORDER by antigen
				END
	END
ELSE --Version #2
	BEGIN
		If @CONCount = 3
		BEGIN	
			IF (SELECT  count(sampleID) FROM [dbo].[SingleAntigenExportView] where assignment = 'Positive' and [sampleID] = @SampleID and [Batch Name] = @BatchID) = 0 OR (Select COUNT(*) FROM tbAntibodyStats where AntibodyID = @AntibodyID and tail > 0) = 0
				Begin
				SELECT Distinct [Batch Name]
				  ,[sampleID]
				  ,dbo.[GetUserNameHematosAb](completedBy) As completedBy
				  ,dbo.[GetUserNameHematosAb](approvedBy) As approvedBy
				  ,CONVERT(varchar(11), [CompletedDt], 103) As completedDt
				  ,comments
				FROM [dbo].[SingleAntigenExportView] where [sampleID] = @SampleID and [Batch Name] = @BatchID
				END
			Else 
				Begin
					SELECT DISTINCT	tbAntibodyMethod.sessionID, tbAntibodyMethod.sampleID, tbAntibodyMethod.lotID, 
					dbo.[GetUserNameHematosAb](tbAntibodyMethod.completedBy) As completedBy , dbo.[GetUserNameHematosAb](tbAntibodyMethod.approvedBy) As approvedBy , CONVERT(varchar(11), tbAntibodyMethod.CompletedDt, 103) As completedDt, tbAntibodyMethod.comments, tbAntibodyStats.antigen, 
						tbAntibodyStats.tail, 	 CAST(Round(tbAntibodyData.adjust3, 0) AS INT) AS strength,
						(CASE WHEN tbAntibodyCalculations.IsStandard = 1 THEN CAST(Round(dbo.GetBCM(sessionid,sampleid,antigen), 0) AS INT) ELSE 0 END) AS adjust1,
						tbHematosLociRules.serology, tbAntibodyData.rawValue,  tbantibodyData.assignment,
						tbHematosLociRules.selectedAllelicAll as SELECTALL, tbHematosLociRules.useMedianRawValuesAll as USEALL, 
						tbHematosLociRules.selectedAllelicMany as SELECTMANY, tbHematosLociRules.useMedainRawValuesMany as USEMANY,
						tbHematosLociRules.selectAllelicOne as SELECTONE, tbHematosLociRules.useMedianRawValuesOne as USEONE,
						tbAntigens.AntigenSortOrder, tbAntigens.SerologySortOrder, CAST(Round(tbAntibodyData.adjust1,0) as INT) as ADJ
				FROM    tbAntibodyStats INNER JOIN
						tbAntibodyMethod ON tbAntibodyStats.AntibodyID = tbAntibodyMethod.AntibodyID INNER JOIN
						tbAntibodyData ON tbAntibodyMethod.AntibodyID = tbAntibodyData.AntibodyID INNER JOIN
						tbHematosLociRules ON tbAntibodyStats.Antigen = tbHematosLociRules.allelename INNER JOIN
						tbAntibodyCalculations ON tbAntibodyMethod.CalcUsed = tbAntibodyCalculations.CalculationID INNER JOIN
						tbAlleleProbeVals ON tbAntibodyData.bead = tbAlleleProbeVals.probeName 
										  AND tbAntibodyStats.antigen = tbAlleleProbeVals.allele 
										  AND tbAntibodyMethod.logicID = tbAlleleProbeVals.logicId INNER JOIN
							tbAntigens ON tbAlleleProbeVals.allele = tbAntigens.AntigenName
				WHERE   tbAntibodyMethod.sessionID = @BatchID
				AND		tbAntibodyMethod.sampleID = @SampleID
				AND		tbAlleleProbeVals.positiveFlag = 1
				--AND		tbAntibodyStats.tail > 0
				ORDER by antigen
				END
		END
		Else
			BEGIN
			IF (Select COUNT(*) FROM tbAntibodyStats where AntibodyID = @AntibodyID and tail > 0) = 0
				Begin
				print @antibodyid
					SELECT Distinct [Batch Name]
					  ,[sampleID]
					  ,dbo.[GetUserNameHematosAb](completedBy) As completedBy
					  ,dbo.[GetUserNameHematosAb](approvedBy) As approvedBy
					  ,CONVERT(varchar(11), [CompletedDt], 103) As completedDt
					  ,comments
					FROM [dbo].[SingleAntigenOncCONExportView] where [sampleID] = @SampleID and [Batch Name] = @BatchID
				END
			Else 
				Begin
				
					SELECT DISTINCT	tbAntibodyMethod.sessionID, tbAntibodyMethod.sampleID, tbAntibodyMethod.lotID, 
							dbo.[GetUserNameHematosAb](tbAntibodyMethod.completedBy) As completedBy , dbo.[GetUserNameHematosAb](tbAntibodyMethod.approvedBy) As approvedBy , CONVERT(varchar(11), tbAntibodyMethod.CompletedDt, 103) As completedDt, tbAntibodyMethod.comments, tbAntibodyStats.antigen, 
							tbAntibodyStats.tail, 	 CAST(Round(tbAntibodyData.adjust1, 0) AS INT) AS adjust1,
							(CASE WHEN tbAntibodyCalculations.IsStandard = 1 THEN CAST(Round(dbo.GetBCM(sessionid,sampleid,antigen), 0) AS INT) ELSE 0 END) AS adjust3,
							tbHematosLociRules.serology, tbAntibodyData.rawValue,  tbantibodyData.assignment,
						tbHematosLociRules.selectedAllelicAll as SELECTALL, tbHematosLociRules.useMedianRawValuesAll as USEALL, 
						tbHematosLociRules.selectedAllelicMany as SELECTMANY, tbHematosLociRules.useMedainRawValuesMany as USEMANY,
						tbHematosLociRules.selectAllelicOne as SELECTONE, tbHematosLociRules.useMedianRawValuesOne as USEONE,
						tbAntigens.AntigenSortOrder, tbAntigens.SerologySortOrder, CAST(Round(tbAntibodyData.adjust1,0) as INT) as ADJ
					FROM    tbAntibodyStats INNER JOIN
							tbAntibodyMethod ON tbAntibodyStats.AntibodyID = tbAntibodyMethod.AntibodyID INNER JOIN
							tbAntibodyData ON tbAntibodyMethod.AntibodyID = tbAntibodyData.AntibodyID INNER JOIN
							tbHematosLociRules ON tbAntibodyStats.Antigen = tbHematosLociRules.allelename INNER JOIN
							tbAntibodyCalculations ON tbAntibodyMethod.CalcUsed = tbAntibodyCalculations.CalculationID INNER JOIN
							tbAlleleProbeVals ON tbAntibodyData.bead = tbAlleleProbeVals.probeName 
											  AND tbAntibodyStats.antigen = tbAlleleProbeVals.allele 
											  AND tbAntibodyMethod.logicID = tbAlleleProbeVals.logicId INNER JOIN
							tbAntigens ON tbAlleleProbeVals.allele = tbAntigens.AntigenName
					WHERE   tbAntibodyMethod.sessionID = @BatchID
					AND		tbAntibodyMethod.sampleID = @SampleID
					AND		tbAlleleProbeVals.positiveFlag = 1
					--AND		tbAntibodyStats.tail > 0
					ORDER by antigen
				END
			END
	END
END                                                                                                                       
                                                                                                                           
                                                                                                    
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[InsertHematosABData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[InsertHematosABData]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		JLimauro
-- Create date: 1-11-15
-- Description:	Inserts export data
-- =============================================
Create PROCEDURE [dbo].[InsertHematosABData]
	-- Add the parameters for the stored procedure here
	@FileName nvarchar (300),
	@LotID nvarchar(30),
	@BatchID nvarchar (100),
	@SampleID nvarchar(100),
	@LuminexID varchar(20),
	@SiteCode varchar(2),
	@SpecCode varchar(20),
	@Comments nvarchar(1024)	
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	INSERT INTO dbo.tbHematosABExportHistory Values(@FileName, GETDATE(), @LotID, @BatchID, @SampleID, @LuminexID, @SiteCode, @SpecCode, @Comments)
END
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UpdateHematosABData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[UpdateHematosABData]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetHematosLociRules]'))
BEGIN
DROP PROCEDURE [dbo].[GetHematosLociRules]
END
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE dbo.GetHematosLociRules

AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    select distinct serology, useMedianRawValuesAll from tbHematosLociRules where selectedAllelicAll = 'Serology' group by serology, useMedianRawValuesAll
	select distinct serology, useMedainRawValuesMany from tbHematosLociRules where selectedAllelicMany = 'Serology' group by serology, useMedainRawValuesMany
	select distinct serology, useMedianRawValuesOne from tbHematosLociRules where selectAllelicOne = 'Serology' group by serology, useMedianRawValuesOne

	select distinct allelename, useMedianRawValuesAll from tbHematosLociRules where selectedAllelicAll = 'Allelic' group by allelename, useMedianRawValuesAll
	select distinct allelename, useMedainRawValuesMany from tbHematosLociRules where selectedAllelicMany = 'Allelic' group by allelename, useMedainRawValuesMany
	select distinct allelename, useMedianRawValuesOne from tbHematosLociRules where selectAllelicOne = 'Allelic' group by allelename, useMedianRawValuesOne

END
GO
-- =============================================
-- Author:		JLimauro
-- Create date: 1-11-15
-- Description:	Inserts export data
-- =============================================
Create PROCEDURE [dbo].[UpdateHematosABData]
	-- Add the parameters for the stored procedure here
	@FileName nvarchar (300),
	@BatchID nvarchar (100),
	@SampleID nvarchar(100),
	@LuminexID varchar(20),
	@SiteCode varchar(2),
	@SpecCode varchar(20),
	@Comments nvarchar(1024)	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	UPDATE dbo.tbHematosABExportHistory SET [FileName] = @FileName, [ExportDate] = GETDATE(), [LuminexID] = @LuminexID, [SiteCode] = @SiteCode, [SpecCode] = @SpecCode, [Comments] = @Comments WHERE [BatchID] = @BatchID and [sampleID] = @SampleID
END
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[GetADBG]'))
BEGIN
DROP Function [dbo].[GetADBG]
END
GO

CREATE FUNCTION [dbo].[GetADBG] 
(
	@sessionid VARCHAR(30),
	@sampleid VARCHAR(30),
	@allele VARCHAR(30)
)
RETURNS  DECIMAL(12,4)

AS
BEGIN
	-- Declare the return variable here
	DECLARE @Result DECIMAL(12,4)
	DECLARE @lotversion AS INT
	
SELECT  @lotversion = version FROM tbantibodymethod WHERE sessionID=@sessionid AND sampleID =@sampleid
IF @lotversion < 2 OR @lotversion IS NULL
BEGIN
		SET @Result= (SELECT AVG(tbAntibodyData.adjustN)
		FROM         tbAntibodyMethod INNER JOIN
							  tbAntibodyData ON tbAntibodyMethod.AntibodyID = tbAntibodyData.AntibodyID INNER JOIN
							  tbAntibodyStats ON tbAntibodyMethod.AntibodyID = tbAntibodyStats.AntibodyID INNER JOIN
							  tbAlleleProbeVals ON tbAntibodyStats.antigen = tbAlleleProbeVals.allele AND tbAntibodyData.bead = tbAlleleProbeVals.probeName AND 
							  tbAntibodyMethod.logicID = tbAlleleProbeVals.logicId
		WHERE     (tbAntibodyStats.antigen = @allele) 
		AND (tbAntibodyMethod.sessionID = @sessionid) AND (tbAntibodyMethod.sampleID = @sampleid)
		AND tbAlleleProbeVals.probeName IN (SELECT probeName FROM tbAntibodyExpSet WHERE lotid = 
		(SELECT lotid FROM tbAntibodyMethod WHERE sessionid = @sessionid AND sampleid = @sampleid) AND consensus = '0')
		AND (tbAlleleProbeVals.positiveFlag = 1))
END
ELSE
BEGIN
	SET @Result= (SELECT AVG(tbAntibodyData.adjustn)
		FROM         tbAntibodyMethod INNER JOIN
							  tbAntibodyData ON tbAntibodyMethod.AntibodyID = tbAntibodyData.AntibodyID INNER JOIN
							  tbAntibodyStats ON tbAntibodyMethod.AntibodyID = tbAntibodyStats.AntibodyID INNER JOIN
							  tbAlleleProbeVals ON tbAntibodyStats.antigen = tbAlleleProbeVals.allele AND tbAntibodyData.bead = tbAlleleProbeVals.probeName AND 
							  tbAntibodyMethod.logicID = tbAlleleProbeVals.logicId
		WHERE     (tbAntibodyStats.antigen = @allele) 
		AND (tbAntibodyMethod.sessionID = @sessionid) AND (tbAntibodyMethod.sampleID = @sampleid)
		AND tbAlleleProbeVals.probeName IN (SELECT probeName FROM tbAntibodyExpSet WHERE lotid = 
		(SELECT lotid FROM tbAntibodyMethod WHERE sessionid = @sessionid AND sampleid = @sampleid) AND consensus = '0')
		AND (tbAlleleProbeVals.positiveFlag = 1))
END

	-- Return the result of the function
	RETURN @Result

END

GO