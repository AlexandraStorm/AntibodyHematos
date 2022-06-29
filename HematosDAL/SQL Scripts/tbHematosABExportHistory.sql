SET ANSI_NULLS, QUOTED_IDENTIFIER, ANSI_PADDING ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbHematosABExportHistory]'))
DROP TABLE [dbo].[tbHematosABExportHistory]
GO

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
GO

SET ANSI_PADDING OFF
GO

