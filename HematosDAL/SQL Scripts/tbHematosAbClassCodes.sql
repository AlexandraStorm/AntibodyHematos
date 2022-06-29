/****** Object:  Table [dbo].[tbHematosAbClassCodes]    Script Date: 12/12/2014 10:06:20 AM ******/
SET ANSI_NULLS, QUOTED_IDENTIFIER, ANSI_PADDING ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbHematosAbClassCodes]'))
DROP TABLE [dbo].[tbHematosAbClassCodes]
GO

Print N'Creating Antibody Class Codes Table'
CREATE TABLE [dbo].[tbHematosAbClassCodes](
	[AbClassTestCode] [varchar](6) NOT NULL,
	[ClassResultCode] [varchar](100) NOT NULL,
	[ResultDescription] [varchar](100) NOT NULL,
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

Print N'Inserting Antibody Class Codes'
INSERT INTO [dbo].[tbHematosAbClassCodes] ([AbClassTestCode], [ClassResultCode], [ResultDescription]) Values('F8001','A','IgA')
INSERT INTO [dbo].[tbHematosAbClassCodes] ([AbClassTestCode], [ClassResultCode], [ResultDescription]) Values('F8001','AGM','IgA+IgG+IgM')
INSERT INTO [dbo].[tbHematosAbClassCodes] ([AbClassTestCode], [ClassResultCode], [ResultDescription]) Values('F8001','G','IgG')
INSERT INTO [dbo].[tbHematosAbClassCodes] ([AbClassTestCode], [ClassResultCode], [ResultDescription]) Values('F8001','GM','IgG+IgM')
INSERT INTO [dbo].[tbHematosAbClassCodes] ([AbClassTestCode], [ClassResultCode], [ResultDescription]) Values('F8001','M','IgM')
INSERT INTO [dbo].[tbHematosAbClassCodes] ([AbClassTestCode], [ClassResultCode], [ResultDescription]) Values('F8001','NT','Not Tested')
Print N'Completed'
GO
