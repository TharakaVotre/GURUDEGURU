USE [SchoolMGT]
GO
/****** Object:  Table [dbo].[tblParentToSchollMessageAttachments]    Script Date: 3/20/2017 5:33:47 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblParentToSchollMessageAttachments](
	[MessageId] [bigint] NOT NULL,
	[AttachementName] [varchar](250) NOT NULL,
	[AttachementPath] [varchar](500) NOT NULL,
	[SeqNo] [bigint] IDENTITY(1,1) NOT NULL,
 CONSTRAINT [PK_tblParentToSchollMessageAttachments] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
