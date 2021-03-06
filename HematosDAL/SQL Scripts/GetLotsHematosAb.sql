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
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

		SELECT tbAntibodyLot.LotID, AssayName
	FROM tbAntibodyLot INNER JOIN
                          (SELECT DISTINCT lotID, CONVERT(varchar(20), createdt, 101) AS importdate
                            FROM          tbAntibodyExpSet) AS tbAntibodyExpSet_1 ON tbAntibodyLot.LotID = tbAntibodyExpSet_1.lotID
WHERE tbAntibodyLot.available <> 0 and (tbAntibodyLot.LotID like '%LMX') OR (tbAntibodyLot.LotID like '%LM2Q') OR (tbAntibodyLot.LotID like '%LM1') OR (tbAntibodyLot.LotID like '%SA1') OR (tbAntibodyLot.LotID like '%SA2') OR (tbAntibodyLot.LotID like '%SAM')
	ORDER BY  ExpirationDate DESC
END
GO

