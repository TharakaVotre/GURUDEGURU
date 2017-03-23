USE [SchoolMGT]
GO
/****** Object:  Table [dbo].[tblParentToSchoolMessageDetail]    Script Date: 3/20/2017 5:33:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblParentToSchoolMessageDetail](
	[SeqNo] [bigint] IDENTITY(1,1) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[MessageId] [bigint] NOT NULL,
	[RecepientUser] [varchar](20) NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[AuthorizationDate] [datetime] NOT NULL,
	[AuthorizedBy] [varchar](20) NOT NULL,
	[AuthStatus] [varchar](1) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblParentToSchoolMessageDetail] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageDetail] ADD  CONSTRAINT [DF_tblParentToSchoolMessageDetail_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblParentToSchoolMessageDetail_tblParentToSchoolMessageHeader] FOREIGN KEY([SchoolId], [MessageId])
REFERENCES [dbo].[tblParentToSchoolMessageHeader] ([SchoolId], [MessageId])
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageDetail] CHECK CONSTRAINT [FK_tblParentToSchoolMessageDetail_tblParentToSchoolMessageHeader]
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblParentToSchoolMessageDetail_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageDetail] CHECK CONSTRAINT [FK_tblParentToSchoolMessageDetail_tblSchool]
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblParentToSchoolMessageDetail_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageDetail] CHECK CONSTRAINT [FK_tblParentToSchoolMessageDetail_tblUser]
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblParentToSchoolMessageDetail_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageDetail] CHECK CONSTRAINT [FK_tblParentToSchoolMessageDetail_tblUser1]
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblParentToSchoolMessageDetail_tblUser2] FOREIGN KEY([RecepientUser])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageDetail] CHECK CONSTRAINT [FK_tblParentToSchoolMessageDetail_tblUser2]
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblParentToSchoolMessageDetail_tblUser3] FOREIGN KEY([AuthorizedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageDetail] CHECK CONSTRAINT [FK_tblParentToSchoolMessageDetail_tblUser3]
GO
