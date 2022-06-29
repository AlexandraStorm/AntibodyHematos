/****** Object:  Table [dbo].[tbHematosABTestMethodCodes]    Script Date: 12/12/2014 10:06:20 AM ******/
SET ANSI_NULLS, QUOTED_IDENTIFIER, ANSI_PADDING ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbHematosABTestMethodCodes]'))
DROP TABLE [dbo].[tbHematosABTestMethodCodes]
GO

Print N'Creating Test Method Codes Table'
CREATE TABLE [dbo].[tbHematosABTestMethodCodes](
	[TestMethodCode] [varchar](6) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[ShortDescription] [varchar](100) NOT NULL,
	[ForImport] [bit] NULL
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

Print N'Inserting Test Method Codes'
INSERT INTO [dbo].[tbHematosABTestMethodCodes] ([TestMethodCode], [Description], [ShortDescription],[ForImport]) Values('F7006','Luminex ID HLA Class I', 'LUM cI',1)
INSERT INTO [dbo].[tbHematosABTestMethodCodes] ([TestMethodCode], [Description], [ShortDescription],[ForImport]) Values('F7007','Luminex ID HLA Class II', 'LUM cII',1)
INSERT INTO [dbo].[tbHematosABTestMethodCodes] ([TestMethodCode], [Description], [ShortDescription],[ForImport]) Values('F7008','HLA-A (single antigen beads)','LUM SAB A',0)
INSERT INTO [dbo].[tbHematosABTestMethodCodes] ([TestMethodCode], [Description], [ShortDescription],[ForImport]) Values('F7009','HLA-B (single antigen beads)','LUM SAB B',0)
INSERT INTO [dbo].[tbHematosABTestMethodCodes] ([TestMethodCode], [Description], [ShortDescription],[ForImport]) Values('F7010','HLA-Cw (single antigen beads)','LUM SAB Cw',0)
INSERT INTO [dbo].[tbHematosABTestMethodCodes] ([TestMethodCode], [Description], [ShortDescription],[ForImport]) Values('F7011','HLA-DR (single antigen beads)','LUM SAB DR',0)
INSERT INTO [dbo].[tbHematosABTestMethodCodes] ([TestMethodCode], [Description], [ShortDescription],[ForImport]) Values('F7012','HLA-DQ (single antigen beads)','LUM SAB DQ',0)
INSERT INTO [dbo].[tbHematosABTestMethodCodes] ([TestMethodCode], [Description], [ShortDescription],[ForImport]) Values('F7013','HLA-DP (single antigen beads)','LUM SAB DP',0)
INSERT INTO [dbo].[tbHematosABTestMethodCodes] ([TestMethodCode], [Description], [ShortDescription],[ForImport]) Values('F7025','Luminex SAB HLA Class I', 'SAB cI',1)
INSERT INTO [dbo].[tbHematosABTestMethodCodes] ([TestMethodCode], [Description], [ShortDescription],[ForImport]) Values('F7026','Luminex SAB HLA Class II', 'SAB cII',1)

Print N'Completed'
