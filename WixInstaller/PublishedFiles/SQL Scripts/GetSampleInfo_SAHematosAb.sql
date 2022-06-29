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
CREATE PROCEDURE [dbo].[GetSampleInfo_SAHematosAb]
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