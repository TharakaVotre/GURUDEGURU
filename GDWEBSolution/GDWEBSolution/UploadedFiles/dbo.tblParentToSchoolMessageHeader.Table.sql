USE [SchoolMGT]
GO
/****** Object:  Table [dbo].[tblParentToSchoolMessageHeader]    Script Date: 3/20/2017 5:33:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblParentToSchoolMessageHeader](
	[SchoolId] [varchar](14) NOT NULL,
	[MessageId] [bigint] NOT NULL,
	[ParentId] [bigint] NOT NULL,
	[MessageType] [bigint] NULL,
	[Message] [varchar](300) NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
	[Attachments] [int] NULL,
	[Subject] [varchar](50) NULL,
 CONSTRAINT [PK_tblParentToSchoolMessageHeader] PRIMARY KEY CLUSTERED 
(
	[SchoolId] ASC,
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader] ADD  CONSTRAINT [DF_tblParentToSchoolMessageHeader_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblMessageType] FOREIGN KEY([MessageType])
REFERENCES [dbo].[tblMessageType] ([MessageTypeId])
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader] CHECK CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblMessageType]
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblParentToSchoolMessageHeader] FOREIGN KEY([ParentId])
REFERENCES [dbo].[tblParent] ([ParentId])
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader] CHECK CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblParentToSchoolMessageHeader]
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader] CHECK CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblSchool]
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader] CHECK CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblUser]
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader] CHECK CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblUser1]
GO
