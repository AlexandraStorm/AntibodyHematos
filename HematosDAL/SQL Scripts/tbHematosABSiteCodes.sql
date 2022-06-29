/****** Object:  Table [dbo].[tbHematosABSiteCodes]    Script Date: 12/12/2014 10:06:20 AM ******/
SET ANSI_NULLS, QUOTED_IDENTIFIER, ANSI_PADDING ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbHematosABSiteCodes]'))
DROP TABLE [dbo].[tbHematosABSiteCodes]
GO

Print N'Creating Site Codes Table'
CREATE TABLE [dbo].[tbHematosABSiteCodes](
	[Site Code] [varchar](2) NOT NULL,
	[Description] [varchar](100) NOT NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

Print N'Inserting Site Codes'
INSERT INTO dbo.[tbHematosABSiteCodes] ([Site Code], [Description]) Values('D1', 'Sheffield')
INSERT INTO dbo.[tbHematosABSiteCodes] ([Site Code], [Description]) Values('H1', 'Birmingham')
INSERT INTO dbo.[tbHematosABSiteCodes] ([Site Code], [Description]) Values('N1', 'Newcastle')
INSERT INTO dbo.[tbHematosABSiteCodes] ([Site Code], [Description]) Values('P1', 'Tooting')
INSERT INTO dbo.[tbHematosABSiteCodes] ([Site Code], [Description]) Values('T1', 'Fulton')
INSERT INTO dbo.[tbHematosABSiteCodes] ([Site Code], [Description]) Values('W1', 'Colindale')

Print N'Completed'
