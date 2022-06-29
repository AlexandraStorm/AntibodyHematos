/****** Object:  Table [dbo].[tbHematosABTestingKitsCodes]    Script Date: 12/12/2014 10:06:20 AM ******/
SET ANSI_NULLS, QUOTED_IDENTIFIER, ANSI_PADDING ON
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[tbHematosABTestingKitsCodes]'))
DROP TABLE [dbo].[tbHematosABTestingKitsCodes]
GO

Print N'Creating Testing Kits Codes Table'
CREATE TABLE [dbo].[tbHematosABTestingKitsCodes](
	[AntibodyKitTestCode] [varchar](6) NOT NULL,
	[KitResultCode] [varchar](100) NOT NULL,
	[ResultDescription] [varchar](100) NOT NULL,
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

Print N'Inserting Testing Kits Codes'
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','LABIDI','Class I ID-Labscreen')
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','LABIDII','Class II ID-Labscreen')
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','LABSAI','Class I SA-Labscreen')
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','LABSAII','Class II SA-Labscreen')
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','LABSCRNI','Class I Screen-Labscreen')
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','LABSCRNII','Class II Screen-Labscreen')
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','LIFEIDI','Class I ID-Lifecodes')
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','LIFEIDII','Class II ID-Lifecodes')
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','LIFESAI','Class I SA-Lifecodes')
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','LIFESAII','Class II SA-Lifecodes')
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','LIFESCRNI','Class I Screen-Lifecodes')
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','LIFESCRNII','Class II Screen-Lifecodes')
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','MIXED','MIXED')
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','NT','Not Tested')
INSERT INTO [dbo].[tbHematosABTestingKitsCodes] ([AntibodyKitTestCode], [KitResultCode], [ResultDescription]) Values('F8003','OTHER','OTHER')
Print N'Completed'
GO
