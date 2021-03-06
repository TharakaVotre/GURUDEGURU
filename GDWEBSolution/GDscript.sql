USE [master]
GO
/****** Object:  Database [SchoolMGT]    Script Date: 3/4/2019 11:50:30 AM ******/
CREATE DATABASE [SchoolMGT]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SchoolMGT', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\SchoolMGT.mdf' , SIZE = 5184KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'SchoolMGT_log', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\SchoolMGT_log.ldf' , SIZE = 832KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [SchoolMGT] SET COMPATIBILITY_LEVEL = 110
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [SchoolMGT].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [SchoolMGT] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [SchoolMGT] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [SchoolMGT] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [SchoolMGT] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [SchoolMGT] SET ARITHABORT OFF 
GO
ALTER DATABASE [SchoolMGT] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [SchoolMGT] SET AUTO_CREATE_STATISTICS ON 
GO
ALTER DATABASE [SchoolMGT] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [SchoolMGT] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [SchoolMGT] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [SchoolMGT] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [SchoolMGT] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [SchoolMGT] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [SchoolMGT] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [SchoolMGT] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [SchoolMGT] SET  ENABLE_BROKER 
GO
ALTER DATABASE [SchoolMGT] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [SchoolMGT] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [SchoolMGT] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [SchoolMGT] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [SchoolMGT] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [SchoolMGT] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [SchoolMGT] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [SchoolMGT] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [SchoolMGT] SET  MULTI_USER 
GO
ALTER DATABASE [SchoolMGT] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [SchoolMGT] SET DB_CHAINING OFF 
GO
ALTER DATABASE [SchoolMGT] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [SchoolMGT] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
USE [SchoolMGT]
GO
/****** Object:  StoredProcedure [dbo].[DCISgetSchool]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DCISgetSchool]

AS
BEGIN

	SELECT a.* ,c.SchoolCategoryName from tblSchool a ,tblSchoolCategory c where 
	a.IsActive='Y' and c.SchoolCategoryId=a.SchoolCategory

END






GO
/****** Object:  StoredProcedure [dbo].[DCISgetSchoolAdmin]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[DCISgetSchoolAdmin](@SchoolId varchar(20))

AS
BEGIN

	SELECT a.* ,c.SchoolCategoryName from tblSchool a ,tblSchoolCategory c where 
	a.IsActive='Y' and c.SchoolCategoryId=a.SchoolCategory
	and a.SchoolId=@SchoolId

END






GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupportingDocumentDown]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISgetSupportingDocumentDown](@Status varchar(20),@requestID varchar(20),@reqstID varchar(20))
AS
BEGIN
select
c.SupportingDocumentName,
b.SupportingDocID,
b.RequestID,
b.DownloadPath,
b.RequestBy,
b.RequestDate,
b.CertificateRequestId,
e.UserID,
b.ApprovedDate,
b.ApprovedBy,
b.IsDownloaded,
'Not Given' as Consignor,

'Not Given' as Consignee




from tblSupportingDocApproveRequest b, tblSupportingDocuments c ,tblUser e

where b.SupportingDocID=c.SupportingDocumentId and
b.CertificateRequestId is null and 
 b.Status like @Status and b.CustomerID like @requestID and b.RequestID like @reqstID and e.CustomerId=b.CustomerID and b.RequestBy=e.UserID  and b.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate )


 union


 select
c.SupportingDocumentName,
b.SupportingDocID,
b.RequestID,
b.DownloadPath,
b.RequestBy,
b.RequestDate,
b.CertificateRequestId,
e.UserID,
b.ApprovedDate,
b.ApprovedBy,
b.IsDownloaded,
 f.Consignor,

f.Consignee




from tblSupportingDocApproveRequest b, tblSupportingDocuments c ,tblUser e,tblCertifcateRequestHeader f

where b.SupportingDocID=c.SupportingDocumentId and
b.CertificateRequestId=f.RequestId and
 b.Status like @Status and b.CustomerID like @requestID and b.RequestID like @reqstID and e.CustomerId=b.CustomerID and b.RequestBy=e.UserID  and b.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate )
 order by  Consignor Desc

END




GO
/****** Object:  StoredProcedure [dbo].[DCISgetSupportingDocumentDownDate]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[DCISgetSupportingDocumentDownDate](@Status varchar(20),@requestID varchar(20),@reqstID varchar(20),@StartDate  varchar(10),@EndDate varchar(10))
AS
BEGIN
select
c.SupportingDocumentName,
b.SupportingDocID,
b.RequestID,
b.DownloadPath,
b.RequestBy,
b.RequestDate,
b.CertificateRequestId,
e.UserID,
b.ApprovedDate,
b.ApprovedBy,
b.IsDownloaded,
'Not Given' as Consignor,

'Not Given' as Consignee




from tblSupportingDocApproveRequest b, tblSupportingDocuments c ,tblUser e

where b.SupportingDocID=c.SupportingDocumentId and
b.CertificateRequestId is null and 
 b.Status like @Status and b.CustomerID like @requestID and b.RequestID like @reqstID and e.CustomerId=b.CustomerID and b.RequestBy=e.UserID  and b.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate )

	and 
convert(varchar,B.RequestDate,112)>=@StartDate
and convert(varchar,B.RequestDate,112)<=@EndDate


 union


 select
c.SupportingDocumentName,
b.SupportingDocID,
b.RequestID,
b.DownloadPath,
b.RequestBy,
b.RequestDate,
b.CertificateRequestId,
e.UserID,
b.ApprovedDate,
b.ApprovedBy,
b.IsDownloaded,
 f.Consignor,

f.Consignee




from tblSupportingDocApproveRequest b, tblSupportingDocuments c ,tblUser e,tblCertifcateRequestHeader f

where b.SupportingDocID=c.SupportingDocumentId and
b.CertificateRequestId=f.RequestId and
 b.Status like @Status and b.CustomerID like @requestID and b.RequestID like @reqstID and e.CustomerId=b.CustomerID and b.RequestBy=e.UserID  and b.RequestID NOT in
	(SELECT DocumentId FROM tblCancelledcertificate )
	and 
convert(varchar,B.RequestDate,112)>=@StartDate
and convert(varchar,B.RequestDate,112)<=@EndDate

 order by  Consignor Desc

END




GO
/****** Object:  StoredProcedure [dbo].[DCISModifySchool]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE  PROCEDURE [dbo].[DCISModifySchool]
(

@SchoolId varchar(20),@SchoolGroup bigint,@SchoolName varchar(50),@SchoolRank int,@IsActive varchar(20),@Division int,@District int,@Description varchar(20),@ModifiedBy varchar(20),@Address1 varchar(20),@Address2 varchar(20),@Address3 varchar(20),@Email varchar(50),@Fax varchar(20),@ImagePath varchar(200),@MinuteforPeriod int ,@Telephone varchar(20),@SchoolCategory int ,@Province int,@WebUrl varchar(50),@LogoPath varchar(200))
AS

BEGIN

update tblSchool
set 

SchoolGroup=@SchoolGroup,
SchoolName=@SchoolName,
SchoolRank=@SchoolRank,
IsActive=@IsActive,
Division=@Division,
District=@District,
Description=@Description,
ModifiedBy=@ModifiedBy,
ModifiedDate=GETDATE(),

Address1=@Address1,
Address2=@Address2,
Address3=@Address3,
Email=@Email,
Fax=@Fax,
MinuteforPeriod=@MinuteforPeriod,
Telephone=@Telephone,
SchoolCategory=@SchoolCategory,
Province=@Province,
WebUrl=@WebUrl,
LogoPath=@LogoPath,
ImagePath=@ImagePath
where
SchoolId=@SchoolId
　

END




GO
/****** Object:  StoredProcedure [dbo].[DCISsetSchool]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[DCISsetSchool](@SchoolId varchar(20),@SchoolGroup bigint,@SchoolName varchar(50),@SchoolRank int,@IsActive varchar(20),@Division int,@District int,@Description varchar(20),@CreatedBy varchar(20),@Address1 varchar(20),@Address2 varchar(20),@Address3 varchar(200),@Email varchar(50),@Fax varchar(20),@ImagePath varchar(200),@MinuteforPeriod int ,@Telephone varchar(20),@SchoolCategory int ,@Province int,@WebUrl varchar(50),@LogoPath varchar(200))
AS

BEGIN


insert into tblSchool
(SchoolId,SchoolGroup,SchoolName,SchoolRank,IsActive,Division,District,Description,CreatedBy,CreatedDate,Address1,Address2,Address3,Email,Fax,MinuteforPeriod,Telephone,SchoolCategory,Province,WebUrl,LogoPath,ImagePath,AccadamicYear)
values
(
@SchoolId,@SchoolGroup,@SchoolName,@SchoolRank,@IsActive,@Division,@District,@Description,@CreatedBy,GETDATE(),@Address1,@Address2,@Address3,@Email,@Fax,@MinuteforPeriod,@Telephone,@SchoolCategory,@Province,@WebUrl,@LogoPath,@ImagePath,year(GETDATE()))
　

END











GO
/****** Object:  StoredProcedure [dbo].[GDCheckStudentAccacdemicYear]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDCheckStudentAccacdemicYear]
	@AccYear varchar(4),@GradeId varchar(20),@ClassId varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblStudent WHERE AcademicYear=@AccYear
	AND GradeId=@GradeId
	AND ClassId=@ClassId
END



GO
/****** Object:  StoredProcedure [dbo].[GDdeleteAllApplicationStatus]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDdeleteAllApplicationStatus]
	(@Isactive varchar(1),@StatusCode bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblApplicationStatus SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE StatusCode=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteApplication]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDdeleteApplication] 
	-- Add the parameters for the stored procedure here
	(@id bigint,@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE tblEntranceApplication set IsActive=@IsActive WHERE AppStatusSeqNo=@id
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteEvaluationType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDdeleteEvaluationType]
	(@Isactive varchar(1),@StatusCode bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblEvaluationType SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE EvaluationTypeCode=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteEventCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[GDdeleteEventCategory]
	(@Isactive varchar(1),@StatusCode bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblEventCategory SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE EventCategoryId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteExtraCurricularActivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDdeleteExtraCurricularActivity]
	(@Isactive varchar(1),@StatusCode varchar(6),@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblExtraCurricularActivity SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE ActivityCode=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteFunction]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDdeleteFunction]
	(@Isactive varchar(1),@StatusCode varchar(5),@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblFunction SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE FunctionId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteGradeMaintenance]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDdeleteGradeMaintenance]

	(@Isactive varchar(1),@StatusCode varchar(20),@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblGrade SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE GradeId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteGradeSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDdeleteGradeSubject] 
	-- Add the parameters for the stored procedure here
	(@AcademicYear varchar(4),@SchoolId varchar(14),@GradeId varchar(20),@SubjectId int,@isActive varchar(1),@userId varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update tblGradeSubject SET IsActive=@IsActive,ModifiedBy=@userid,ModifiedDate=GETDATE()
	 Where
	AcademicYear=@AcademicYear 
	AND GradeId=@GradeId
	AND SubjectId=@SubjectId
	AND SchoolId=@SchoolId
END





GO
/****** Object:  StoredProcedure [dbo].[GDDeleteHomeWork]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDDeleteHomeWork]
	(@Isactive varchar(1),@AssignmentNo bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblAssignmentHeader SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE AssignmentNo=@AssignmentNo
END






GO
/****** Object:  StoredProcedure [dbo].[GDdeleteMassageType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDdeleteMassageType]
	(@Isactive varchar(1),@StatusCode bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblMessageType SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE MessageTypeId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteParentObservationType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDdeleteParentObservationType]
	(@Isactive varchar(1),@StatusCode int,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblParentObservationType SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE ObTypeId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteQualification]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[GDdeleteQualification]
	(@Isactive varchar(1),@StatusCode int,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblQualification SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE QualificationId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDDeleteSchoolCalenderActivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDDeleteSchoolCalenderActivity] 
	-- Add the parameters for the stored procedure here
	(@SeqNo bigint,@userId varchar(20),@isActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE tblSchoolCalendar SET IsActive=@isActive,ModifiedBy=@userId,ModifiedDate=GETDATE()
	WHERE CalenderSeqNo=@SeqNo
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteSchoolCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[GDdeleteSchoolCategory]

	(@Isactive varchar(1),@StatusCode int,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblSchoolCategory SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE SchoolCategoryId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteSchoolGroup]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[GDdeleteSchoolGroup]


	(@Isactive varchar(1),@StatusCode bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblSchoolGroup SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE GroupId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteSchoolRank]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[GDdeleteSchoolRank]



	(@Isactive varchar(1),@StatusCode bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblSchoolRank SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE SchoolRankId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteStudentEvaluationType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[GDdeleteStudentEvaluationType]



	(@Isactive varchar(1),@StatusCode bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblEvaluationType SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE EvaluationTypeCode=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteStudentEveluationResult]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDdeleteStudentEveluationResult]
	-- Add the parameters for the stored procedure here
	@EveluationResultId bigint,@user varchar(20),@IsActive varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE tblEvaluationResult SET IsActive=@IsActive,ModifiedBy=@user,ModifiedDate=GETDATE() WHERE EveluationResultSeqNo=@EveluationResultId
	

END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDdeleteSubject]
	(@Isactive varchar(1),@StatusCode int,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblSubject SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE SubjectId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDdeleteSubjectCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDdeleteSubjectCategory]
	(@Isactive varchar(1),@StatusCode int,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblSubjectCategory SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE SubjectCategoryId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllApplicationStatus]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllApplicationStatus]
	(@Isactive varchar(1),@id bigint)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if(@id!=0)
	SELECT * FROM tblApplicationStatus WHERE IsActive=@Isactive AND StatusCode like @id
	else
	SELECT * FROM tblApplicationStatus WHERE IsActive=@Isactive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllClass]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllClass] 
	@IsaActive varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblClass WHERE IsActive=@IsaActive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllEvaluationType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDgetAllEvaluationType]
	(@Isactive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	SELECT * FROM tblEvaluationType WHERE IsActive=@Isactive 
	
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllEventCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllEventCategory]
	-- Add the parameters for the stored procedure here
	(@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblEventcategory WHERE IsActive=@IsActive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllExtraCurricularActivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllExtraCurricularActivity]
	(@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblExtraCurricularActivity WHERE IsActive=@IsActive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllFunction]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllFunction]
	-- Add the parameters for the stored procedure here
	(@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblFunction WHERE IsActive=@IsActive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllGradeMaintenance]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllGradeMaintenance]
	-- Add the parameters for the stored procedure here
	(@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblGrade WHERE IsActive=@IsActive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllGradeSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE
 PROCEDURE [dbo].[GDgetAllGradeSubject] 
	(@AcademicYear varchar(4),@SchoolId varchar(14),@GradeId varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.AcademicYear,A.SubjectId,A.Optional,B.ShortName,B.SubjectName,C.SubjectCategoryId,C.SubjectCategoryName,D.GradeName,D.GradeId,A.CreatedBy,A.CreatedDate,A.IsActive,A.ModifiedBy,A.ModifiedDate
	FROM tblGradeSubject A,tblSubject B,tblSubjectCategory C,tblGrade D WHERE AcademicYear=@AcademicYear
	 AND A.GradeId=@GradeId
	 AND A.SchoolId=@SchoolId
	  AND A.SubjectId=B.SubjectId
	 AND A.SubjectCategoryId=C.SubjectCategoryId
	 AND A.GradeId=D.GradeId
	 And A.IsActive=@IsActive

	END





GO
/****** Object:  StoredProcedure [dbo].[GDgetAllMassageType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllMassageType] 
	(@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblMessageType WHERE IsActive=@IsActive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllParentObservationType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllParentObservationType] 
	-- Add the parameters for the stored procedure here
	(@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblParentObservationType WHERE IsActive=@IsActive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllQualification]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDgetAllQualification]
	-- Add the parameters for the stored procedure here
	(@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblQualification WHERE IsActive=@IsActive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllSchoolCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllSchoolCategory]
	(@Isactive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblSchoolCategory WHERE IsActive=@Isactive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllSchoolClass]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllSchoolClass]
	-- Add the parameters for the stored procedure here
	(@SchoolId varchar(14),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblClass WHERE SchoolId=@SchoolId AND IsActive=@IsActive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllSchoolGroup]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllSchoolGroup]
	(@Isactive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblSchoolGroup WHERE IsActive=@Isactive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllSchoolRank]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllSchoolRank]
	(@Isactive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblSchoolRank WHERE IsActive=@Isactive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllStudentEvaluationType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDgetAllStudentEvaluationType]
	(@Isactive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblStudentEvaluationType WHERE IsActive=@Isactive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllStudentInGrade]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllStudentInGrade]
	-- Add the parameters for the stored procedure here
	(@AcademicYear varchar(4),@schoolId varchar(14),@GradeId varchar(20),@ClassId varchar(20),@isactive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.SchoolId, A.StudentId, A.studentName, A.DateofBirth, A.GradeId,B.GradeName, A.ClassId,D.ClassName, A.Gender, A.UserId, A.ImgUrl, A.CreatedBy, A.CreatedDate, A.ModifiedBy, A.ModifiedDate, A.IsActive 
	FROM tblStudent A,tblGrade B,tblClass D WHERE
	 A.GradeId=@GradeId 
	 AND A.IsActive=@isactive 
	 AND A.GradeId=B.GradeId 
	 AND A.SchoolId=@schoolId 
	 AND A.ClassId=D.ClassId   
	 AND D.GradeId=A.GradeId 
	 AND A.ClassId=@ClassId
	 AND A.AcademicYear=@AcademicYear
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllSubject]
	(@Isactive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblSubject WHERE IsActive=@Isactive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllSubjectCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllSubjectCategory]
	(@Isactive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblSubjectCategory WHERE IsActive=@Isactive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAllTeacherCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAllTeacherCategory]
	(@Isactive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblTeacherCategory WHERE IsActive=@Isactive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetApplication]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetApplication]
	(@StatusCode bigint,@SchoolId varchar(14),@startDate varchar(10),@EndDate varchar(10),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if(@StatusCode=0)
	SELECT A.AppStatusSeqNo, A.RefNo, A.StatusCode, A.Remarks,A.CreatedDate,B.StatusDescription FROM tblEntranceApplication A,tblApplicationStatus B WHERE
	convert(varchar,A.CreatedDate,112)>=@startDate AND convert(varchar,A.CreatedDate,112)<=@EndDate
	AND A.IsActive=@IsActive
	AND A.SchoolId=@SchoolId
	AND A.StatusCode=B.StatusCode
	
	else
	
	SELECT A.AppStatusSeqNo, A.RefNo, A.StatusCode, A.Remarks,A.CreatedDate,B.StatusDescription FROM tblEntranceApplication A,tblApplicationStatus B WHERE
	A.StatusCode=B.StatusCode
	AND convert(varchar,A.CreatedDate,112)>=@startDate AND convert(varchar,A.CreatedDate,112)<=@EndDate
	AND A.IsActive=@IsActive
	AND A.SchoolId=@SchoolId
	AND A.StatusCode=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetApplicationStatus]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[GDgetApplicationStatus]
	(@Id bigint)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblApplicationStatus WHERE StatusCode=@id
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetAttentionRequiredSubjectsGradeWiseSchool]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAttentionRequiredSubjectsGradeWiseSchool]
	(@SchoolId varchar(14),@AccemicYear varchar(4),@EvaluationType bigint,@Grade varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	if(@EvaluationType!=0)
    -- Insert statements for procedure here
		SELECT MAX(A.Mark) AS Maximum,MIN(A.Mark) AS Minimun,AVG(A.Mark) AS Avarage,C.SubjectName,D.GradeName
	FROM tblEvaluationResult A,tblEvaluationHeader B,tblSubject C,tblGrade D,tblEvaluationDetail E
	WHERE A.EvaluationDetailSeqNo=E.EvaluationDetailSeqNo 
	AND A.SubjectId=C.SubjectId
	AND A.Grade=D.GradeId
	AND B.EvaluationNo=E.EvaluationNo
	AND A.SchoolId=@SchoolId
	AND B.AccedamicYear=@AccemicYear
	AND B.EvaluationNo Like @EvaluationType
	group By C.SubjectName,D.GradeName

	else
		SELECT MAX(A.Mark) AS Maximum,MIN(A.Mark) AS Minimun,AVG(A.Mark) AS Avarage,C.SubjectName,D.GradeName
	FROM tblEvaluationResult A,tblEvaluationHeader B,tblSubject C,tblGrade D,tblEvaluationDetail E
	WHERE A.EvaluationDetailSeqNo=E.EvaluationDetailSeqNo 
	AND A.SubjectId=C.SubjectId
	AND A.Grade=D.GradeId
	AND B.EvaluationNo=E.EvaluationNo
	AND A.SchoolId=@SchoolId
	AND B.AccedamicYear=@AccemicYear
	AND B.EvaluationNo Like '%'
	group By C.SubjectName,D.GradeName
END





GO
/****** Object:  StoredProcedure [dbo].[GDgetAttentionRequiredSubjectsSchool]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetAttentionRequiredSubjectsSchool]
	-- Add the parameters for the stored procedure here
	(@SchoolId varchar(14),@AccemicYear varchar(4),@EvaluationType bigint,@Grade varchar(20))
AS
BEGIN
	SET NOCOUNT ON;
	if(@EvaluationType!=0)
	SELECT MAX(A.Mark) AS Maximum,MIN(A.Mark) AS Minimun,AVG(A.Mark) AS Avarage,C.SubjectName
	FROM tblEvaluationResult A,tblEvaluationHeader B,tblSubject C,tblGrade D,tblEvaluationDetail E
	WHERE A.EvaluationDetailSeqNo=E.EvaluationDetailSeqNo 
	AND A.SubjectId=C.SubjectId
	AND A.Grade=D.GradeId
	AND B.EvaluationNo=E.EvaluationNo
	AND A.SchoolId=@SchoolId
	AND B.AccedamicYear=@AccemicYear
	AND E.EvaluationDetailSeqNo Like @EvaluationType
	group By C.SubjectName
	else
	SELECT MAX(A.Mark) AS Maximum,MIN(A.Mark) AS Minimun,AVG(A.Mark) AS Avarage,C.SubjectName
	FROM tblEvaluationResult A,tblEvaluationHeader B,tblSubject C,tblGrade D,tblEvaluationDetail E
	WHERE A.EvaluationDetailSeqNo=E.EvaluationDetailSeqNo 
	AND A.SubjectId=C.SubjectId
	AND A.Grade=D.GradeId
	AND B.EvaluationNo=E.EvaluationNo
	AND A.SchoolId=@SchoolId
	AND B.AccedamicYear=@AccemicYear
	AND E.EvaluationDetailSeqNo Like '%'
	group By C.SubjectName
END







GO
/****** Object:  StoredProcedure [dbo].[GDgetClassAttentionRequiredSubjects]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetClassAttentionRequiredSubjects] 
	-- Add the parameters for the stored procedure here
	(@SchoolId varchar(14),@AccYear varchar(4),@GradeId varchar(20),@ClassId varchar(20),@EvaluationType bigint)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if(@EvaluationType!=0)
	SELECT AVG(A.Mark) As Average,MAX(A.Mark) AS Maximum,MIN(A.Mark) AS Minimum,B.SubjectName FROM tblEvaluationResult A,tblSubject B,tblEvaluationHeader C,tblEvaluationDetail E WHERE
	A.SubjectId=B.SubjectId
	AND A.Grade=@GradeId
	AND C.EvaluationNo=E.EvaluationNo
	AND E.Class=@ClassId
	AND E.SchoolId=@SchoolId
	AND A.EvaluationDetailSeqNo=E.EvaluationDetailSeqNo 
	AND C.EvaluationNo like @EvaluationType
	AND C.AccedamicYear=@AccYear
	Group By B.SubjectName
	else
	SELECT AVG(A.Mark) As Average,MAX(A.Mark) AS Maximum,MIN(A.Mark) AS Minimum,B.SubjectName FROM tblEvaluationResult A,tblSubject B,tblEvaluationHeader C,tblEvaluationDetail E WHERE
	A.SubjectId=B.SubjectId
	AND A.Grade=@GradeId
	AND C.EvaluationNo=E.EvaluationNo
	AND E.Class=@ClassId
	AND E.SchoolId=@SchoolId
	AND A.EvaluationDetailSeqNo=E.EvaluationDetailSeqNo 
	AND C.EvaluationNo like '%'
	AND C.AccedamicYear=@AccYear
	Group By B.SubjectName


END







GO
/****** Object:  StoredProcedure [dbo].[GDgetClassEveluation]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetClassEveluation]
	-- Add the parameters for the stored procedure here
	@SchoolId varchar(14),@ClassId varchar(20),@GradeId varchar(20),@IsActive varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT B.EvaluationDetailSeqNo,A.EvaluationDescription FROM tblEvaluationHeader A,tblEvaluationDetail B
	WHERE A.EvaluationNo=B.EvaluationNo
	AND A.SchoolId=@SchoolId
	AND B.Grade like @GradeId
	AND B.Class like @ClassId
	AND B.IsActive=@IsActive
	AND A.isActive=@IsActive
	
END



GO
/****** Object:  StoredProcedure [dbo].[GDgetEveluation]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetEveluation]
	-- Add the parameters for the stored procedure here
	@SchoolId varchar(14),@IsActive varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.EvaluationNo,A.EvaluationDescription FROM tblEvaluationHeader A
	WHERE A.isActive=@IsActive
	AND A.SchoolId=@SchoolId
END





GO
/****** Object:  StoredProcedure [dbo].[GDgetEveluationResult]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetEveluationResult] 
	-- Add the parameters for the stored procedure here
	@SchoolId varchar(14),@GradeId varchar(20),@Eveluationseq bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.SubjectId,A.Mark,A.StudentId FROM tblEvaluationResult A WHERE
	A.SchoolId=@SchoolId
	AND A.Grade=@GradeId
	AND A.EvaluationDetailSeqNo=@Eveluationseq
	
	
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetEveluationSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetEveluationSubject] 
	-- Add the parameters for the stored procedure here
	@SchoolId varchar(14),@GradeId varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.SubjectName,A.SubjectId FROM tblSubject A,tblGradeSubject B WHERE
	A.SubjectId=B.SubjectId
	AND B.SchoolId=@SchoolId
	AND B.GradeId=@GradeId
	
	
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetExtraCurriculerAllStudentReport]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetExtraCurriculerAllStudentReport] 
	-- Add the parameters for the stored procedure here
	@SchoolId varchar(14),@Activity varchar(6),@GradeId varchar(20),@ClassId varchar(20),@IsActive varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT B.studentName,C.GradeName,D.ClassName FROM tblStudentExtraCurricularActivity A,tblStudent B,tblGrade C,tblClass D
	WHERE
	A.SchoolId=@SchoolId
	AND A.ActivityCode=@Activity
	AND A.StudentId=B.StudentId
	AND C.GradeId=B.GradeId
	AND B.GradeId Like @GradeId
	AND D.ClassId Like @ClassId
	AND D.SchoolId=A.SchoolId
	AND B.ClassId=D.ClassId



END




GO
/****** Object:  StoredProcedure [dbo].[GDgetExtraCurriculerStudent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[GDgetExtraCurriculerStudent]
	-- Add the parameters for the stored procedure here
	@SchoolId varchar(14),@ActivityCode varchar(6),@IsActive varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT B.StudentId,B.studentName,B.Gender,B.DateofBirth FROM tblStudentExtraCurricularActivity A,tblStudent B WHERE
	A.SchoolId=@SchoolId
	AND A.ActivityCode=@ActivityCode
	AND A.IsActive=@IsActive
	AND A.StudentId=B.StudentId
	AND A.IsActive=@IsActive
	

END




GO
/****** Object:  StoredProcedure [dbo].[GDgetGradeActiveClass]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE
 PROCEDURE [dbo].[GDgetGradeActiveClass] 
	(@GradeId varchar(20),@SchoolId varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblClass WHERE
	  GradeId like @GradeId
	 AND SchoolId=@SchoolId
	 AND IsActive=@IsActive

	END




GO
/****** Object:  StoredProcedure [dbo].[GDgetGradeActiveSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE
 PROCEDURE [dbo].[GDgetGradeActiveSubject] 
	(@SchoolId varchar(14),@GradeId varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.SubjectId,B.SubjectName FROM tblGradeSubject A,tblSubject B WHERE
	  A.GradeId like @GradeId
	  AND
	  A.SubjectId=B.SubjectId
	  AND A.SchoolId=@SchoolId
	 AND A.IsActive=@IsActive

	END




GO
/****** Object:  StoredProcedure [dbo].[GDgetGradeSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetGradeSubject] 
	-- Add the parameters for the stored procedure here
	@SchoolId varchar(14),@GradeId varchar(20),@IsActive varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.SubjectId,B.SubjectName  FROM tblGradeSubject A,tblSubject B
	WHERE 
	A.SubjectId=B.SubjectId
	AND A.SchoolId=@SchoolId
	AND A.GradeId=@GradeId
	AND A.IsActive=@IsActive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetHomeWork]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetHomeWork] 
	-- Add the parameters for the stored procedure here
	@Id bigint
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.AssignmentNo,A.BatchDescription,A.BatchNo,A.ClassId,A.GradeId,A.FilePath,A.SubjectId,B.DueDate FROM tblAssignmentHeader A,tblAssignmentDueDate B WHERE
	A.AssignmentNo=B.AssignmentId
	AND
	A.AssignmentNo=@Id
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetHomeWorkAdd]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetHomeWorkAdd]
	-- Add the parameters for the stored procedure here
	(@SchoolId varchar(14),@TeacherId bigint,@IsActive varchar(1),@Fromdate varchar(10),@Todate varchar(10))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here

	SELECT A.AssignmentNo, A.AssignmentDescription, A.SchoolId, A.GradeId,C.GradeName, A.ClassId,D.ClassName,E.SubjectName, A.TeacherId, A.FilePath, A.BatchNo, A.BatchDescription, A.SubjectId, A.CreatedBy, A.CreatedDate, A.ModifiedBy, A.ModifiedDate, A.IsActive,B.DueDate 
	FROM tblAssignmentHeader A,tblAssignmentDueDate B ,tblGrade C,tblClass D,tblSubject E
	WHERE A.SchoolId=@SchoolId
	AND A.TeacherId=@TeacherId 
	AND A.IsActive=@IsActive
	AND A.GradeId=C.GradeId
	AND A.GradeId=D.GradeId
	AND A.ClassId=D.ClassId
	AND D.SchoolId=A.SchoolId
	AND A.SubjectId=E.SubjectId
	AND convert(varchar,B.CreatedDate,112)>=@Fromdate AND convert(varchar,B.CreatedDate,112)<=@Todate
	AND
	A.AssignmentNo=B.AssignmentId
	order By A.GradeId,D.ClassId,B.CreatedDate 
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetParentApplicationStatus]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetParentApplicationStatus]
	-- Add the parameters for the stored procedure here
	(@RefNo varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.AppStatusSeqNo,A.RefNo,A.Remarks,A.StatusCode,A.CreatedDate,B.StatusDescription FROM tblEntranceApplication A,tblApplicationStatus B WHERE A.StatusCode=B.StatusCode AND A.RefNo=@RefNo	 
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetParentAttentionRequiredSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetParentAttentionRequiredSubject]
	(@AccYear varchar(4),@EveluationType bigint,@userName varchar(20)) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	if(@EveluationType!=0)
		SELECT A.Mark,B.SubjectName FROM
		 tblEvaluationResult A,tblSubject B,tblEvaluationHeader C,tblEvaluationDetail E,tblParent D,tblParentStudent F WHERE
	A.SubjectId=B.SubjectId
	AND C.EvaluationNo=E.EvaluationNo
	AND A.EvaluationDetailSeqNo=E.EvaluationDetailSeqNo 
	AND A.EvaluationDetailSeqNo like @EveluationType
	AND C.AccedamicYear=@AccYear
	AND	F.StudentId=A.StudentId
	AND F.ParentId=D.ParentId
	AND D.UserId=@userName
	else
		SELECT A.Mark,B.SubjectName FROM
		 tblEvaluationResult A,tblSubject B,tblEvaluationHeader C,tblEvaluationDetail E,tblParent D,tblParentStudent F WHERE
	A.SubjectId=B.SubjectId
	AND C.EvaluationNo=E.EvaluationNo
	AND A.EvaluationDetailSeqNo=E.EvaluationDetailSeqNo 
	AND C.EvaluationType like '%'
	AND C.AccedamicYear=@AccYear
	AND	F.StudentId=A.StudentId
	AND F.ParentId=D.ParentId
	AND D.UserId=@userName
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetParentHomeWork]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetParentHomeWork]
	-- Add the parameters for the stored procedure here
	(@SchoolId varchar(14),@Fromdate varchar(10),@Todate varchar(10),@userId varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT A.AssignmentNo, A.AssignmentDescription, A.SchoolId, A.GradeId,F.GradeName,G.ClassName, A.ClassId, A.TeacherId, A.FilePath, A.BatchNo, A.BatchDescription, A.SubjectId,H.SubjectName, A.CreatedBy, A.CreatedDate, A.ModifiedBy, A.ModifiedDate, A.IsActive,B.DueDate
    FROM 
	tblAssignmentHeader A,tblAssignmentDueDate B,tblStudent C,tblParent D,tblParentStudent E,tblGrade F,tblClass G,tblSubject H
	WHERE 
	A.SchoolId=@SchoolId
	AND A.IsActive=@IsActive
	AND convert(varchar,A.CreatedDate,112)>=@Fromdate AND convert(varchar,A.CreatedDate,112)<=@Todate
	AND
	A.AssignmentNo=B.AssignmentId
	AND C.ClassId=A.ClassId
	AND A.GradeId=F.GradeId
	AND A.GradeId=G.GradeId
	AND A.SubjectId=H.SubjectId
	AND B.SchoolId=@SchoolId
	AND G.SchoolId=A.SchoolId
	AND A.ClassId=G.ClassId
	AND	C.GradeId=A.GradeId
	AND	E.StudentId=C.StudentId
	AND E.ParentId=D.ParentId
	AND D.UserId=@userId
END





GO
/****** Object:  StoredProcedure [dbo].[GDgetSchoolCalenderEvent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetSchoolCalenderEvent] 
	-- Add the parameters for the stored procedure here
	(@SchoolId varchar(14),@year varchar(4),@IsActive varchar(1)) 
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblSchoolCalendar WHERE SchoolId=@SchoolId AND AcadamicYear=@year AND IsActive=@IsActive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetSchoolExtraCurriculerActivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetSchoolExtraCurriculerActivity] 
	-- Add the parameters for the stored procedure here
	@SchoolId varchar(14),@IsActive varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT B.ActivityCode,B.ActivityName FROM tblSchoolExtraCurricularActivity A,tblExtraCurricularActivity B
	WHERE 
	A.SchoolId=@SchoolId
	AND A.ActivityCode=B.ActivityCode
	AND A.IsActive=@IsActive
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetSchoolGrade]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetSchoolGrade]
	-- Add the parameters for the stored procedure here
	
	(@SchoolId varchar(14),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   SELECT A.GradeId,A.GradeName FROM tblGrade A,tblSchoolGrade B WHERE B.SchoolId=@SchoolId
   AND A.GradeId=B.GradeId
   AND A.IsActive=@IsActive

END



GO
/****** Object:  StoredProcedure [dbo].[GdgetStudentAttentionRequiredSubjects]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GdgetStudentAttentionRequiredSubjects] 
	-- Add the parameters for the stored procedure here
	(@AccYear varchar(4),@StudentId varchar(20),@EvaluationType bigint)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	if(@EvaluationType!=0)
	SELECT A.Mark,B.SubjectName FROM tblEvaluationResult A,tblSubject B,tblEvaluationHeader C,tblEvaluationDetail E WHERE
	A.SubjectId=B.SubjectId
	AND A.StudentId=@StudentId
	AND C.EvaluationNo=E.EvaluationNo
	AND A.EvaluationDetailSeqNo=E.EvaluationDetailSeqNo 
	AND C.EvaluationNo like @EvaluationType
	AND C.AccedamicYear=@AccYear
	else
		SELECT A.Mark,B.SubjectName FROM tblEvaluationResult A,tblSubject B,tblEvaluationHeader C,tblEvaluationDetail E WHERE
		A.SubjectId=B.SubjectId
	AND A.StudentId=@StudentId
	AND C.EvaluationNo=E.EvaluationNo
	AND A.EvaluationDetailSeqNo=E.EvaluationDetailSeqNo 
	AND C.EvaluationNo like '%'
	AND C.AccedamicYear=@AccYear

END







GO
/****** Object:  StoredProcedure [dbo].[GDgetStudentInClass]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetStudentInClass]
	-- Add the parameters for the stored procedure here
	@SchoolId varchar(14),@GradeId varchar(20),@ClassId varchar(20),@IsActive varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT * FROM tblStudent WHERE
	GradeId=@GradeId
	AND ClassId Like @ClassId
	AND IsActive=@IsActive
	AND SchoolId=@SchoolId

END




GO
/****** Object:  StoredProcedure [dbo].[GDgetStudentResult]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetStudentResult]
	-- Add the parameters for the stored procedure here
	@SchoolId varchar(14),@Eveluation varchar(6),@AccYear varchar(4),@UserId varchar(20),@IsActive varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT E.SubjectName,E.ShortName,A.Mark,G.EvaluationDetailSeqNo
	FROM tblEvaluationResult A,tblUser B,tblParentStudent C,tblParent D,tblSubject E,tblEvaluationHeader F,tblEvaluationDetail G
	WHERE A.StudentId=C.StudentId
	AND C.ParentId=D.ParentId
	AND D.UserId=@UserId
	AND F.AccedamicYear=@AccYear
	AND B.UserId=D.UserId
	AND A.SchoolId=@SchoolId
	AND E.SubjectId=A.SubjectId
	AND A.EvaluationDetailSeqNo=G.EvaluationDetailSeqNo
	AND F.EvaluationNo=@Eveluation
	AND F.EvaluationNo=G.EvaluationNo
	Order By A.StudentId
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetStudentSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetStudentSubject] 
	-- Add the parameters for the stored procedure here
	@SchoolId varchar(14),@AccYear varchar(4),@GradeId varchar(20),@StudentId varchar(20),@IsActive varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT B.SubjectName,B.ShortName,B.SubjectId,A.Optional FROM tblGradeSubject A,tblSubject B WHERE 
	A.AcademicYear=@AccYear
	AND A.GradeId=@GradeId
	AND A.IsActive=@IsActive
	AND A.SubjectId=B.SubjectId
	UNION
	SELECT B.SubjectName,B.ShortName,B.SubjectId,'Y' as Optional FROM tblStudentOptionalSubject A,tblSubject B
	WHERE 
	A.AcademicYear=@AccYear
	AND A.GradeId=@GradeId
	AND A.StudentId=@StudentId
	AND A.SubjectId=B.SubjectId
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetSubjectsResult]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetSubjectsResult]
	-- Add the parameters for the stored procedure here
	@SchoolId varchar(14),@GradeId varchar(20),@SubjectId int,@Eveluation bigint,@Isactive varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.EveluationResultSeqNo,A.StudentId,B.studentName,A.Mark FROM tblEvaluationResult A,tblStudent B
	WHERE 
	A.StudentId=B.StudentId
	AND A.SchoolId=@SchoolId
	AND A.Grade=@GradeId
	AND A.EvaluationDetailSeqNo=@Eveluation
	AND A.IsActive=@Isactive
	AND A.SubjectId=@SubjectId
	
END




GO
/****** Object:  StoredProcedure [dbo].[GDgetSubjectStudent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDgetSubjectStudent]
	-- Add the parameters for the stored procedure here
	(@SchoolId varchar(20),@GradeId varchar(20),@ClassId varchar(20),@SubjectId int,@EvelutionSeq bigint)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	SELECT A.studentName,A.StudentId,A.Gender,A.DateofBirth,'N' As Optional FROM tblStudent A,tblGradeSubject B,tblSubject C
	WHERE
	B.GradeId=@GradeId
	AND A.SchoolId=@SchoolId
	AND A.ClassId=@ClassId
	AND A.GradeId=B.GradeId
	AND B.SubjectId=@SubjectId
	AND A.StudentId
	NOT IN(SELECT A.StudentId FROM tblEvaluationResult D WHERE D.StudentId=A.StudentId AND D.SubjectId=@SubjectId AND D.SchoolId=@SchoolId AND D.EvaluationDetailSeqNo=@EvelutionSeq AND D.IsActive='Y' )
	Union
	SELECT A.studentName,A.StudentId,A.Gender,A.DateofBirth,'Y' As Optional FROM tblStudent A,tblStudentOptionalSubject B
	WHERE
	A.GradeId=@GradeId
	AND A.ClassId=@ClassId
	AND B.SchoolId=@SchoolId
	AND B.StudentId=A.StudentId
	AND B.SubjectId=@SubjectId
	AND A.StudentId
	NOT IN(SELECT A.StudentId FROM tblEvaluationResult D WHERE D.StudentId=A.StudentId AND D.SubjectId=@SubjectId AND D.SchoolId=@SchoolId AND D.EvaluationDetailSeqNo=@EvelutionSeq AND D.IsActive='Y' )
	
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifyAllApplicationStatus]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifyAllApplicationStatus]
	(@Description varchar(50),@StatusCode bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblApplicationStatus SET StatusDescription=@Description,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE StatusCode=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifyApplicationStatus]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifyApplicationStatus]
	-- Add the parameters for the stored procedure here
	(@id bigint,@remark varchar(200),@StatusCode bigint,@refNo varchar(50),@userId varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE tblEntranceApplication
	SET Remarks=@remark,StatusCode=@StatusCode,RefNo=@refNo,ModifiedBy=@userId,ModifiedDate=GETDATE() 
	WHERE AppStatusSeqNo=@id
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifyEvaluationTypee]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDModifyEvaluationTypee]
	(@Description varchar(50),@StatusCode bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblEvaluationType SET EvaluationTypeDesc=@Description,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE EvaluationTypeCode=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifyEventCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifyEventCategory]
	(@Description varchar(50),@StatusCode bigint,@ModyfiedBy varchar(20), @Broadcast varchar(1),@ParentApproval varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblEventcategory SET EventCategoryDesc=@Description,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE(),BroadcastMessage=@Broadcast,ParentApprovalNeeded=@ParentApproval WHERE EventCategoryId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifyExtraCurricularActivit]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifyExtraCurricularActivit]
	(@Description varchar(50),@StatusCode varchar(6),@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblExtraCurricularActivity SET ActivityName=@Description,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE ActivityCode=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifyFunction]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifyFunction]
	(@Description varchar(50),@StatusCode varchar(5),@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblFunction SET FunctionName=@Description,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE FunctionId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifyGrade]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifyGrade]
	(@Description varchar(50),@StatusCode varchar(20),@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblGrade SET GradeName=@Description,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE GradeId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifyGradeSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifyGradeSubject] 
	-- Add the parameters for the stored procedure here
	(@AcademicYear varchar(4),@SchoolId varchar(14),@GradeId varchar(20),@SubjectId int,@SubjectCatId int,@option varchar(1),@UserId varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	Update tblGradeSubject SET SubjectCategoryId=@SubjectCatId,Optional=@option,ModifiedBy=@userid,ModifiedDate=GETDATE()
	 Where
	AcademicYear=@AcademicYear 
	AND GradeId=@GradeId
	AND SubjectId=@SubjectId
	AND SchoolId=@SchoolId
END





GO
/****** Object:  StoredProcedure [dbo].[GDModifyHomeWork]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifyHomeWork]
	(@AssignmentDescription varchar(200),@SchoolId varchar(14),@GradeId varchar(20),@ClassId varchar(20),@FilePath varchar(200),
	@BatchNo varchar(10),@BatchDescription varchar(200),@SubjectId int,@AssignmentNo bigint,@DueDate date,@DueId bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE tblAssignmentHeader SET 
	AssignmentDescription=@AssignmentDescription,
	SchoolId=@SchoolId,
	ClassId=@ClassId,
	ModifiedBy=@ModyfiedBy,
	GradeId=@GradeId,
	FilePath=@FilePath,
	BatchNo=@BatchNo,
	BatchDescription=@BatchDescription,
	SubjectId=@SubjectId,
	ModifiedDate=GETDATE()
	 WHERE AssignmentNo=@AssignmentNo

	UPDATE tblAssignmentDueDate SET
	 DueDate=@DueDate,
	 ModifiedBy=@ModyfiedBy,
	 ModifiedDate=GETDATE() 
	 WHERE AssignmentId=@AssignmentNo

END






GO
/****** Object:  StoredProcedure [dbo].[GDModifyMassageType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifyMassageType]
	(@Description varchar(100),@StatusCode bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblMessageType SET MessageTypeDescription=@Description,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE MessageTypeId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifyParentObservationType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifyParentObservationType]
	(@Description varchar(100),@StatusCode int,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblParentObservationType SET Description=@Description,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE ObTypeId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifyQualification]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[GDModifyQualification]
	(@Description varchar(50),@StatusCode int,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblQualification SET QualificationName=@Description,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE QualificationId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifySchoolAccademicYear]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifySchoolAccademicYear] 
	-- Add the parameters for the stored procedure here
	@SchoolId varchar(14),@AccYear varchar(4)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE tblAccadamicYear SET AccadamicYear=@AccYear WHERE SchoolId=@SchoolId
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifySchoolCalenderActivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifySchoolCalenderActivity] 
	-- Add the parameters for the stored procedure here
	(@SeqNo bigint,@Todate date,@FromDate date,@Comment varchar(100),@DateComment varchar(50),@Isholiday varchar(1),@userId varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE tblSchoolCalendar SET ToDate=@Todate,FromDate=@FromDate,SpecialComment=@Comment,DateComment=@DateComment,IsHoliday=@Isholiday,ModifiedBy=@userId,ModifiedDate=GETDATE()
	WHERE CalenderSeqNo=@SeqNo
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifySchoolCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[GDModifySchoolCategory]

	(@Description varchar(50),@StatusCode int,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblSchoolCategory SET SchoolCategoryName=@Description,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE SchoolCategoryId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifySchoolGroup]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifySchoolGroup]

	(@Description varchar(50),@StatusCode bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblSchoolGroup SET GroupName=@Description,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE GroupId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifySchoolRank]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[GDModifySchoolRank]


	(@Description varchar(50),@StatusCode bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblSchoolRank SET SchoolRankName=@Description,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE SchoolRankId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifyStudentEvaluationType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[GDModifyStudentEvaluationType]


	(@Description varchar(50),@StatusCode bigint,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblEvaluationType SET EvaluationTypeDesc=@Description,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE EvaluationTypeCode=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifyStudentEveluationResult]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifyStudentEveluationResult] 
	-- Add the parameters for the stored procedure here
	@EveluationResultId bigint,@mark decimal,@user varchar(20)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	UPDATE tblEvaluationResult SET Mark=@mark,ModifiedBy=@user,ModifiedDate=GETDATE() WHERE EveluationResultSeqNo=@EveluationResultId
	

END




GO
/****** Object:  StoredProcedure [dbo].[GDModifyStudentGradeAdvance]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifyStudentGradeAdvance] 
	-- Add the parameters for the stored procedure here
	(@schoolId varchar(14),@StudentId varchar(20),@gradeId varchar(20),@ClassId varchar(20),@AccYear varchar(4),@userId varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE tblStudent set AcademicYear=@AccYear,GradeId=@gradeId,ClassId=@ClassId,UserId=@userId,ModifiedDate=GETDATE() WHERE SchoolId=@schoolId AND StudentId=@StudentId
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifyStudentLeaver]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDModifyStudentLeaver] 
	-- Add the parameters for the stored procedure here
	(@schoolId varchar(14),@StudentId varchar(20),@userId varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    UPDATE tblStudent set IsActive=@IsActive,UserId=@userId,ModifiedDate=GETDATE() WHERE SchoolId=@schoolId AND StudentId=@StudentId
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifySubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifySubject]


	(@ShortName varchar(20),@SubjectName varchar(50),@StatusCode int,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblSubject SET ShortName=@ShortName,SubjectName=@SubjectName,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE SubjectId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDModifySubjectCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDModifySubjectCategory]


	(@SubjectName varchar(50),@StatusCode int,@ModyfiedBy varchar(20) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   UPDATE tblSubjectCategory SET SubjectCategoryName=@SubjectName,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE SubjectCategoryId=@StatusCode
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetApplicationStatus]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDsetApplicationStatus]
	-- Add the parameters for the stored procedure here
	(@Description varchar(50),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblApplicationStatus (StatusDescription, CreatedBy, CreatedDate, IsActive)
   Values (@Description,@CreatedBy,GETDATE(),@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetEvaluationType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDsetEvaluationType]
	-- Add the parameters for the stored procedure here
	(@Description varchar(50),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblEvaluationType(EvaluationTypeDesc, CreatedBy, CreatedDate, IsActive)
   Values (@Description,@CreatedBy,GETDATE(),@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetEventCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDsetEventCategory]
	-- Add the parameters for the stored procedure here
	(@Description varchar(50),@Broadcast varchar(1),@Approval varchar(1),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblEventcategory(EventCategoryDesc,BroadcastMessage,ParentApprovalNeeded, CreatedBy, CreatedDate, IsActive)
   Values (@Description,@Broadcast,@Approval,@CreatedBy,GETDATE(),@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetExtraCurricularActivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDsetExtraCurricularActivity]
	-- Add the parameters for the stored procedure here
	(@Code varchar(6),@Description varchar(50),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblExtraCurricularActivity(ActivityCode,ActivityName, CreatedBy, CreatedDate, IsActive)
   Values (@Code,@Description,@CreatedBy,GETDATE(),@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetFunction]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDsetFunction]
	-- Add the parameters for the stored procedure here
	(@Id varchar(5),@Description varchar(50),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblFunction(FunctionId,FunctionName, CreatedBy, CreatedDate, IsActive)
   Values (@id,@Description,@CreatedBy,GETDATE(),@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetGrade]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDsetGrade]
	-- Add the parameters for the stored procedure here
	(@Id varchar(20),@Description varchar(50),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblGrade (GradeId, GradeName, CreatedDate, CreatedBy, IsActive)
   Values (@id,@Description,GETDATE(),@CreatedBy,@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetGradeSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDsetGradeSubject]
	(@AcademicYear varchar(4),@SchoolId varchar(14), @GradeId varchar(20), @SubjectId int, @SubjectCategoryId int, @Optional varchar(1), @CreatedBy varchar(20), @IsActive varchar(1))
	
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DELETE FROM tblGradeSubject WHERE AcademicYear=@AcademicYear AND GradeId=@GradeId AND SubjectId=@SubjectId

   INSERT INTO tblGradeSubject(AcademicYear,SchoolId, GradeId, SubjectId, SubjectCategoryId, Optional, CreatedBy, CreatedDate,IsActive)Values
   (@AcademicYear,@SchoolId, @GradeId, @SubjectId, @SubjectCategoryId, @Optional, @CreatedBy, GETDATE(),@IsActive)
END





GO
/****** Object:  StoredProcedure [dbo].[GDsetHomeWork]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDsetHomeWork]
	(@AssignmentDescription varchar(200),@SchoolId varchar(14),@GradeId varchar(20),@ClassId varchar(20),@FilePath varchar(200),@TeacherId bigint,
	@BatchNo varchar(10),@BatchDescription varchar(200),@SubjectId int,@AssignmentNo bigint,@DueDate date,@DueId bigint,@User varchar(20),@IsActive varchar(1) )
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	

	
	INSERT INTO tblAssignmentHeader
	(AssignmentDescription, SchoolId, GradeId, ClassId, TeacherId, FilePath, BatchNo, BatchDescription, SubjectId, CreatedBy, CreatedDate, IsActive)
	Values
	(@AssignmentDescription, @SchoolId, @GradeId, @ClassId, @TeacherId, @FilePath, @BatchNo, @BatchDescription, @SubjectId, @User, GETDATE(), @IsActive)
	
	--@AssignmentNo = SELECT MAX(AssignmentNo) FROM tblAssignmentHeader

	INSERT INTO tblAssignmentDueDate(AssignmentId, SchoolId, DueDate, CreatedBy, CreatedDate, IsActive)
	
	(
	SELECT MAX(AssignmentNo) , @SchoolId, @DueDate, @User, GETDATE(), @IsActive From tblAssignmentHeader)

 

END


GO
/****** Object:  StoredProcedure [dbo].[GDsetMassageType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDsetMassageType]

	-- Add the parameters for the stored procedure here
	(@Description varchar(100),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblMessageType(MessageTypeDescription, CreatedBy, CreatedDate, IsActive)
   Values (@Description,@CreatedBy,GETDATE(),@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetParentObservationType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDsetParentObservationType]

	-- Add the parameters for the stored procedure here
	(@Description varchar(100),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblParentObservationType(Description, CreatedBy, CreatedDate, IsActive)
   Values (@Description,@CreatedBy,GETDATE(),@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetQualification]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDsetQualification]

	-- Add the parameters for the stored procedure here
	(@Description varchar(50),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblQualification(QualificationName, CreatedBy, CreatedDate, IsActive)
   Values (@Description,@CreatedBy,GETDATE(),@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetSchoolCalenderActivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDsetSchoolCalenderActivity] 
	-- Add the parameters for the stored procedure here
	(@SchoolId varchar(14),@AcadamicYear varchar(4), @DateComment varchar(50), @IsHoliday varchar(1), @SpecialComment varchar(100), @FromDate Date, @ToDate Date, @User varchar(20), @IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO tblSchoolCalendar(SchoolId, AcadamicYear, DateComment, IsHoliday, SpecialComment, FromDate, ToDate, CreatedBy, CreatedDate, IsActive)
	Values(@SchoolId, @AcadamicYear, @DateComment, @IsHoliday, @SpecialComment, @FromDate, @ToDate, @User, GETDATE(), @IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetSchoolCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDsetSchoolCategory]


	-- Add the parameters for the stored procedure here
	(@Description varchar(50),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblSchoolCategory(SchoolCategoryName, CreatedBy, CreatedDate, IsActive)
   Values (@Description,@CreatedBy,GETDATE(),@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetSchoolGroup]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDsetSchoolGroup]


	-- Add the parameters for the stored procedure here
	(@Description varchar(50),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblSchoolGroup(GroupName, CreatedBy, CreatedDate, IsActive)
   Values (@Description,@CreatedBy,GETDATE(),@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetSchoolRank]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDsetSchoolRank]



	-- Add the parameters for the stored procedure here
	(@Description varchar(50),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblSchoolRank(SchoolRankName, CreatedBy, CreatedDate, IsActive)
   Values (@Description,@CreatedBy,GETDATE(),@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetStudentEvaluationType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDsetStudentEvaluationType]




	-- Add the parameters for the stored procedure here
	(@Description varchar(50),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblEvaluationType(EvaluationTypeDesc, CreatedBy, CreatedDate, IsActive)
   Values (@Description,@CreatedBy,GETDATE(),@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetStudentEveluationResult]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDsetStudentEveluationResult] 
	-- Add the parameters for the stored procedure here
	@EveluationSeq bigint,@StudentId varchar(20),@SubjectId int,@mark decimal(18,2),@GradeId varchar(20),@Comment varchar(100),@SchoolId varchar(14),@User varchar(20),@IsActive varchar(1)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	INSERT INTO tblEvaluationResult(EvaluationDetailSeqNo, StudentId, SchoolId, SubjectId, Mark, Grade, TeacherComment, CreatedBy, CreatedDate, IsActive)Values
	(@EveluationSeq, @StudentId, @SchoolId, @SubjectId, @Mark, @GradeId, @Comment, @User, GetDate(), @IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetStudentHistory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[GDsetStudentHistory] 
	-- Add the parameters for the stored procedure here
	(@StudentId varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO tblStudentHistory ( AcademicYear, SchoolId, StudentId, GradeId, ClassId, CreatedBy, CreadedDate )
SELECT  AcademicYear, SchoolId, StudentId, GradeId, ClassId, CreatedBy, GETDATE()
FROM    tblStudent 
WHERE StudentId=@StudentId 

END





GO
/****** Object:  StoredProcedure [dbo].[GDsetSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDsetSubject]


	-- Add the parameters for the stored procedure here
	(@ShortName varchar(20),@SubjectName varchar(50),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblSubject(ShortName,SubjectName, CreatedBy, CreatedDate, IsActive)
   Values (@ShortName,@SubjectName,@CreatedBy,GETDATE(),@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[GDsetSubjectCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
Create PROCEDURE [dbo].[GDsetSubjectCategory]


	-- Add the parameters for the stored procedure here
	(@Description varchar(50),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

   INSERT INTO tblSubjectCategory(SubjectCategoryName, CreatedBy, CreatedDate, IsActive)
   Values (@Description,@CreatedBy,GETDATE(),@IsActive)
END




GO
/****** Object:  StoredProcedure [dbo].[getAllEvaluationType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[getAllEvaluationType]
	(@Isactive varchar(1))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	
	SELECT * FROM tblEvaluationType WHERE IsActive=@Isactive 
	
END




GO
/****** Object:  StoredProcedure [dbo].[SMGT_getParentInbox]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SMGT_getParentInbox](@ParentId bigint)
as begin
Select 
H.SchoolId, 
H.MessageId, 
ParentId, 
H.Sender,
H.MessageType, 
M.MessageTypeDescription,
Message,    
H.IsActive,
D.SeqNo, 
H.CreatedDate,
H.Subject
from
tblSchoolToParentMessageHeader H ,tblSchoolToParentMessageDetail D , tblMessageType M
where H.SchoolId = D.SchoolId
and H.MessageId = D.MessageId
and ParentId = @ParentId
and M.MessageTypeId = H.MessageType
and H.IsActive ='Y'

union

Select 
H.SchoolId, 
H.MessageId, 
ParentId, 
H.Sender,
H.MessageType, 
M.MessageTypeDescription,
Message,    
H.IsActive,
D.SeqNo, 
H.CreatedDate,
H.Subject
from
tblSchoolToParentMessageHeader H ,tblSchoolToParentMessageDetail D , tblMessageType M
where H.SchoolId = D.SchoolId
and H.MessageId = D.MessageId
and ParentId = -1
and M.MessageTypeId = H.MessageType
and H.IsActive ='Y'

order by CreatedDate desc

End



GO
/****** Object:  StoredProcedure [dbo].[SMGT_GETPasswordChangeEMailConfig]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SMGT_GETPasswordChangeEMailConfig]
as
Begin
SELECT ParameterId,ParameterValue FROM tblParameters WHERE ParameterId LIKE '_EMAIL'
UNION
SELECT ParameterId,ParameterValue FROM tblParameters WHERE ParameterId LIKE '_PASS'
UNION
SELECT ParameterId,ParameterValue FROM tblParameters WHERE ParameterId LIKE '_SMTP'
UNION
SELECT ParameterId,ParameterValue FROM tblParameters WHERE ParameterId LIKE '_SMTP_PORT'
End




GO
/****** Object:  StoredProcedure [dbo].[SMGT_getSchoolClassTeachersList]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SMGT_getSchoolClassTeachersList](@Schoolid varchar(14),@AccademicYear varchar(4))
as begin 
Select
A.SchoolId, 
A.TeacherId, 
T.Name,
A.GradeId, 
G.GradeName,
A.ClassId,
C.ClassName ,
AccedamicYear, 
A.CreatedBy, 
A.CreatedDate, 
A.ModifiedBy, 
A.ModifiedDate, 
A.IsActive
from tblClassTeacher A, tblTeacher T, tblClass C , tblGrade G , tblSchoolGrade SG 
where A.GradeId = G.GradeId
and G.GradeId = C.GradeId
and A.GradeId = SG.GradeId
and G.GradeId = SG.GradeId
and A.SchoolId = SG.SchoolId
and C.SchoolId = SG.SchoolId
and A.TeacherId = T.TeacherId
and A.ClassId = C.ClassId
and A.SchoolId like @Schoolid
and A.AccedamicYear like @AccademicYear
and A.IsActive like 'Y'

End



GO
/****** Object:  StoredProcedure [dbo].[SMGT_getSchoolExActivities]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create proc [dbo].[SMGT_getSchoolExActivities] (@SchoolId varchar(14))
as begin
Select
E.ActivityCode, ActivityName
From tblExtraCurricularActivity E, tblSchoolExtraCurricularActivity S
where E.ActivityCode = S.ActivityCode
and SchoolId like @SchoolId
and S.IsActive like 'Y'

End


GO
/****** Object:  StoredProcedure [dbo].[SMGT_getSchoolExactivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SMGT_getSchoolExactivity](@SchoolId varchar(14))
as begin
SELECT E.ActivityName, E.ActivityCode
FROM tblExtraCurricularActivity E, tblSchoolExtraCurricularActivity S
WHERE E.ActivityCode = S.ActivityCode 
AND S.SchoolId LIKE @SchoolId
AND S.IsActive LIKE 'Y'
end


GO
/****** Object:  StoredProcedure [dbo].[SMGT_getSchoolExactivityParent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[SMGT_getSchoolExactivityParent](@SchoolId varchar(14),@ActivityCode varchar(6))
as begin
select P.ParentId,P.ParentName, d.studentName
from tblParent P, tblParentStudent S, tblStudentExtraCurricularActivity e , tblStudent d
where P.ParentId = S.ParentId
and e.StudentId = d.StudentId
AND s.StudentId = e.StudentId
and e.SchoolId = d.SchoolId
and e.SchoolId like @SchoolId
and e.ActivityCode like @ActivityCode


End



GO
/****** Object:  StoredProcedure [dbo].[SMGT_getSchoolGreadClassParent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
Create proc [dbo].[SMGT_getSchoolGreadClassParent](@SchoolId varchar(14),@GreadId varchar(20),@ClassId varchar(20))
as begin
select P.ParentId,P.ParentName, R.studentName
from tblParent P, tblParentStudent S, tblStudent R
where P.ParentId = S.ParentId
and R.StudentId = S.StudentId
and R.SchoolId like @SchoolId
and R.GradeId like @GreadId
and R.ClassId like @ClassId

End



GO
/****** Object:  StoredProcedure [dbo].[SMGT_getSchooltoParentSentMail]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[SMGT_getSchooltoParentSentMail](@CreatedBy varchar(20))
as begin
Select 
H.SchoolId, 
H.MessageId, 
D.ParentId, 
P.ParentName,
H.MessageType, 
M.MessageTypeDescription,
Message,    
H.IsActive,
D.SeqNo, 
H.CreatedDate,
H.Subject
from
tblSchoolToParentMessageHeader H ,tblSchoolToParentMessageDetail D, tblMessageType M, tblParent P
where H.SchoolId = D.SchoolId
and H.MessageId = D.MessageId
and D.ParentId = P.ParentId
and H.CreatedBy = @CreatedBy
and M.MessageTypeId = H.MessageType

order by CreatedDate desc


End



GO
/****** Object:  StoredProcedure [dbo].[SMGT_getStoPMessageView]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SMGT_getStoPMessageView](@MessageId bigint)
as Begin
Select 
H.SchoolId, H.MessageId, MessageType,M.MessageTypeDescription,Sender,Subject, Message, H.CreatedBy, H.CreatedDate, 
SeqNo 
from tblSchoolToParentMessageHeader H, tblSchoolToParentMessageDetail D , tblMessageType M
where H.SchoolId = D.SchoolId
and H.MessageId = D.MessageId
and H.MessageType = M.MessageTypeId
and H.MessageId = @MessageId

End 



GO
/****** Object:  StoredProcedure [dbo].[SMGT_getTeacherInbox]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SMGT_getTeacherInbox](@RecepientUser varchar(20))
as begin
Select 
H.SchoolId, 
H.MessageId, 
H.ParentId, 
P.ParentName,
H.MessageType, 
M.MessageTypeDescription,
Message,  
H.Status,   
H.IsActive,
D.SeqNo, 
D.RecepientUser,
T.Name,
H.CreatedDate,
H.Attachments,
H.Subject
from
tblParentToSchoolMessageHeader H ,tblParentToSchoolMessageDetail D , tblTeacher T, tblMessageType M , tblUser U ,tblParent P
where H.SchoolId = D.SchoolId
and H.MessageId = D.MessageId
and T.UserId = RecepientUser
and T.UserId = U.UserId
and H.ParentId = P.ParentId
and M.MessageTypeId = H.MessageType
and RecepientUser = @RecepientUser

order by CreatedDate desc


End



GO
/****** Object:  StoredProcedure [dbo].[SMGT_UserLogin]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create proc [dbo].[SMGT_UserLogin](@UserId varchar(20),@Password nvarchar(350))

as begin

select UserId, 
LoginEmail, 
Password, 
SchoolId, 
UserCategory, 
PersonName, 
JobDescription, 
Mobile, 
IsActive
from tblUser 
where UserId = @UserId
and Password = @Password


End



GO
/****** Object:  StoredProcedure [dbo].[SMGTdeleteextraCstudent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SMGTdeleteextraCstudent] 
	-- Add the parameters for the stored procedure here
	(@SchoolId varchar(20),@ActivityCode varchar(20))
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	delete from tblStudentExtraCurricularActivity WHERE SchoolId=@SchoolId
	and ActivityCode=@ActivityCode
END




GO
/****** Object:  StoredProcedure [dbo].[SMGTDeleteParent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SMGTDeleteParent]
	(@Isactive varchar(1),@ParentId varchar(30),@ModyfiedBy varchar(20) )
AS
BEGIN

	SET NOCOUNT ON;

   UPDATE tblParent SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE ParentId=@ParentId 
END






GO
/****** Object:  StoredProcedure [dbo].[SMGTDeleteSchool]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SMGTDeleteSchool]
	(@Isactive varchar(1),@SchoolId varchar(20),@ModyfiedBy varchar(20) )
AS
BEGIN

	SET NOCOUNT ON;

   UPDATE tblSchool SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE SchoolId=@SchoolId
END






GO
/****** Object:  StoredProcedure [dbo].[SMGTDeleteStudent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SMGTDeleteStudent]
	(@Isactive varchar(1),@studentId varchar(30),@ModyfiedBy varchar(20) )
AS
BEGIN

	SET NOCOUNT ON;

   UPDATE tblStudent SET IsActive=@Isactive,ModifiedBy=@ModyfiedBy,ModifiedDate=GETDATE() WHERE StudentId=@studentId
END






GO
/****** Object:  StoredProcedure [dbo].[SMGTDeleteTeacherExtraCurricularActivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



CREATE PROC [dbo].[SMGTDeleteTeacherExtraCurricularActivity](@Teacherid bigint,@Schoolid varchar(14),@ActivityCode varchar(6))
as begin

Delete from tblTeacherExtraCurricularActivity
where TeacherId = @Teacherid
and SchoolId = @Schoolid
AND ActivityCode = @ActivityCode

End



GO
/****** Object:  StoredProcedure [dbo].[SMGTgetAllEvaluationdetailList]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SMGTgetAllEvaluationdetailList](@Schoolid varchar(14),@EvaluationNo varchar(14))
as begin

SELECT 
t.EvaluationNo,
t.EvaluationType,
t.EvaluationDescription,
t.TestPaperFee,
c.EvaluationDetailSeqNo,
g.GradeName,
cl.ClassName,
c.ScheduledDate,
c.ScheduledTimeStart,
c.ScheduledTimeEnd



FROM tblEvaluationHeader T, tblEvaluationDetail C,tblGrade G,tblClass cl
WHERE t.EvaluationNo = c.EvaluationNo and
t.isActive like 'Y' and c.SchoolId=@Schoolid
and c.IsActive like 'Y'
and g.GradeId=c.Grade
and cl.ClassId=c.Class
and cl.GradeId=c.Grade
and cl.SchoolId=@Schoolid
and t.EvaluationNo=@EvaluationNo



End




GO
/****** Object:  StoredProcedure [dbo].[SMGTgetAllEvaluationHdetail]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SMGTgetAllEvaluationHdetail](@Schoolid varchar(14))
as begin

SELECT 
t.EvaluationNo,
t.EvaluationType,
t.EvaluationDescription,
t.TestPaperFee,
c.EvaluationTypeDesc
 



FROM tblEvaluationHeader T, tblEvaluationType C
WHERE T.EvaluationType = c.EvaluationTypeCode and
t.isActive like 'Y' and t.SchoolId like @Schoolid



End


GO
/****** Object:  StoredProcedure [dbo].[SMGTgetAllSchoolExtraCurricularActivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SMGTgetAllSchoolExtraCurricularActivity]
	(@IsActive varchar(1))
AS
BEGIN
	
	SET NOCOUNT ON;

 
	SELECT * FROM tblSchoolExtraCurricularActivity WHERE IsActive=@IsActive
END



GO
/****** Object:  StoredProcedure [dbo].[SMGTgetAllSchoolGrades]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SMGTgetAllSchoolGrades]
	(@IsActive varchar(1))
AS
BEGIN
	
	SET NOCOUNT ON;

 
	SELECT * FROM tblSchoolGrade WHERE IsActive=@IsActive
END






GO
/****** Object:  StoredProcedure [dbo].[SMGTgetAllSchoolSubjects]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SMGTgetAllSchoolSubjects]
	(@IsActive varchar(1))
AS
BEGIN
	
	SET NOCOUNT ON;

 
	SELECT * FROM tblSchoolSubject WHERE IsActive=@IsActive
END






GO
/****** Object:  StoredProcedure [dbo].[SMGTgetAllTeachers]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SMGTgetAllTeachers](@Schoolid varchar(14), @UserId varchar(20) ,@IsActive varchar(1))
as begin

SELECT 
T.TeacherId, 
Name, 
T.Address1, 
T.Address2, 
T.Address3, 
T.TeacherCategoryId,
C.TeacherCategoryName, 
T.Telephone, 
Gender, 
T.Description, 
EmployeeNo, 
NIC, 
DrivingLicense, 
Passport, 
T.UserId, 
ImgUrl, 
DateOfBirth, 
T.CreatedBy, 
T.CreatedDate, 
T.ModifiedBy, 
T.ModifiedDate, 
T.IsActive,
O.SchoolId

FROM tblTeacher T, tblTeacherCategory C, tblSchool S,tblTeacherSchool O
WHERE T.TeacherCategoryId = C.TeacherCategoryId
AND T.TeacherId = O.TeacherId
AND O.SchoolId = S.SchoolId
AND T.IsActive LIKE @IsActive
AND S.SchoolId LIKE @Schoolid
AND T.UserId LIKE @UserId

End



GO
/****** Object:  StoredProcedure [dbo].[SMGTgetclassadd]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SMGTgetclassadd](@SchoolId varchar(20),@GradeId varchar(50))
as begin
select 
a.ClassId,a.ClassName
from tblClass a
where a.SchoolId=@SchoolId
and a.GradeId like @GradeId

end





GO
/****** Object:  StoredProcedure [dbo].[SMGTgetDayTimeTabel]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create Proc [dbo].[SMGTgetDayTimeTabel] (@ClassId varchar(20), @GreadId varchar(20), @SchoolId varchar(14), @Day varchar(20))
as Begin
SELECT SeqNo, AcademicYear, T.SchoolId, T.GradeId, ClassId, 
Day, T.SubjectId,S.SubjectName, FORMAT(CAST(FromTime AS DATETIME),'hh:mm tt') AS FromTime, FORMAT(CAST(ToTime AS DATETIME),'hh:mm tt') AS ToTime,PeriodSequenceNo


FROM tblTimeTable T, tblSubject S, tblSchoolGrade SG , tblGrade G
WHERE ClassId = @ClassId
and T.GradeId = @GreadId
and T.SchoolId = @SchoolId
and Day like @Day
and SG.GradeId = T.GradeId
and T.SchoolId = SG.SchoolId
and S.SubjectId = T.SubjectId
and SG.GradeId = G.GradeId
and AcademicYear like (Select [ParameterValue] from tblParameters where ParameterId like 'AY')
order by PeriodSequenceNo


End


GO
/****** Object:  StoredProcedure [dbo].[SMGTgetGradeclassadd]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[SMGTgetGradeclassadd](@SchoolId varchar(20),@GradeId varchar(50))
as begin
select 
a.ClassId,a.ClassName,a.GradeId,b.GradeName
from tblClass a,tblGrade b
where a.SchoolId=@SchoolId
and a.GradeId like @GradeId
and a.GradeId=b.GradeId
order by GradeName

end


GO
/****** Object:  StoredProcedure [dbo].[SMGTgetGradeSubjects]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SMGTgetGradeSubjects](@GradeId varchar(20), @Schoolid varchar(14))
as begin
select S.SubjectId, ShortName, SubjectName, S.IsActive
from tblSubject S, tblGradeSubject G 
where S.SubjectId = G.SubjectId
and  G.GradeId like @GradeId
and G.IsActive like 'Y'
and G.SchoolId like @Schoolid

End




GO
/****** Object:  StoredProcedure [dbo].[SMGTgetOptionalSubjectadd]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SMGTgetOptionalSubjectadd](@SchoolId varchar(20),@GradeId varchar(50),@AcademicYear varchar(6))
as begin
select 
a.SubjectId,a.ShortName,a.SubjectName
from tblSubject a,tblGradeSubject b
where 
 b.AcademicYear like @AcademicYear
and b.GradeId like @GradeId
and b.Optional like 'Y'
and a.Subjectid=b.SubjectId
and b.SchoolId like @SchoolId

end





GO
/****** Object:  StoredProcedure [dbo].[SMGTGetParent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SMGTGetParent](@StudentID varchar(20),@SchoolId varchar(20))

AS
BEGIN

	SELECT a.*, b.RelashionshipName from tblParent a   ,tblRelashionship b where 
	a.IsActive='Y' 
	and a.RelationshipId=b.RelashionshipId
	and a.SchoolId like @SchoolId
END




GO
/****** Object:  StoredProcedure [dbo].[SMGTGetParentschildern]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
create PROCEDURE [dbo].[SMGTGetParentschildern](@ParentId varchar(20))

AS
BEGIN

	SELECT a.ParentId,a.StudentId,b.studentName from tblParentStudent a   ,tblStudent b where 
	a.IsActive='Y' and
	b.IsActive='Y' and
	a.StudentId=b.StudentId


END




GO
/****** Object:  StoredProcedure [dbo].[SMGTgetParentStudentadd]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SMGTgetParentStudentadd](@ParentId varchar(20),@SchoolId varchar(20))
as begin
select 
Q.ParentId, Q.StudentId, S.ParentName,u.studentName 
from tblParentStudent Q  ,tblParent S , tblStudent U
where q.ParentId = s.ParentId
and q.StudentId=u.StudentId
and u.SchoolId like @SchoolId

and q.ParentId = @ParentId
end


GO
/****** Object:  StoredProcedure [dbo].[SMGTgetParentToSchoolSentMail]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SMGTgetParentToSchoolSentMail](@ParentId bigint)
as begin
Select 
H.SchoolId, 
H.MessageId, 
ParentId, 
H.MessageType, 
M.MessageTypeDescription,
Message,  
H.Status,   
H.IsActive,
D.SeqNo, 
D.RecepientUser,
T.Name,
H.CreatedDate,
H.Attachments,
H.Subject
from
tblParentToSchoolMessageHeader H ,tblParentToSchoolMessageDetail D , tblTeacher T, tblMessageType M
where H.SchoolId = D.SchoolId
and H.MessageId = D.MessageId
and ParentId = @ParentId
and T.UserId = RecepientUser
and M.MessageTypeId = H.MessageType

order by CreatedDate desc


End


GO
/****** Object:  StoredProcedure [dbo].[SMGTgetPtoSMessageView]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SMGTgetPtoSMessageView](@MessageId bigint)
as Begin
Select 
H.SchoolId, H.MessageId, H.ParentId,P.ParentName, MessageType,M.MessageTypeDescription, Message, H.Status, H.CreatedBy, H.CreatedDate, Attachments, Subject,
SeqNo, RecepientUser,U.PersonName, AuthorizationDate, AuthorizedBy, AuthStatus
from tblParentToSchoolMessageHeader H, tblParentToSchoolMessageDetail D , tblMessageType M, tblUser U,tblParent P
where H.SchoolId = D.SchoolId
and H.MessageId = D.MessageId
and H.MessageType = M.MessageTypeId
and U.UserId = D.RecepientUser
and H.ParentId = P.ParentId
and H.MessageId = @MessageId

End 



GO
/****** Object:  StoredProcedure [dbo].[SMGTgetSchoolClasses]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SMGTgetSchoolClasses](@SchoolId varchar(20),@GradeId varchar(20))
as begin
select 
Q.ClassId,Q.ClassName,g.GradeName
from tblclass Q  ,tblGrade G
where Q.SchoolId like @SchoolId
and Q.GradeId like @GradeId
and Q.GradeId=g.GradeId
and Q.IsActive='Y'

order by ClassName

end



GO
/****** Object:  StoredProcedure [dbo].[SMGTgetSchoolExtraCadd]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[SMGTgetSchoolExtraCadd](@SchoolId varchar(20))
as begin
select 
Q.ActivityCode, Q.ActivityName,S.SchoolName, Q.IsActive,c.SchoolId
from tblExtraCurricularActivity Q  ,tblSchool S,tblSchoolExtraCurricularActivity c 
where q.ActivityCode = c.ActivityCode
and s.SchoolId=c.SchoolId

and c.SchoolId= @SchoolId
end




GO
/****** Object:  StoredProcedure [dbo].[SMGTgetSchoolGrade]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SMGTgetSchoolGrade](@SchoolId varchar(14))
as begin

select G.GradeId, G.GradeName, S.IsActive
from tblGrade G, tblSchoolGrade S
where G.GradeId = S.GradeId
and S.SchoolId like @SchoolId
order by G.GradeName

End





GO
/****** Object:  StoredProcedure [dbo].[SMGTgetSchoolGradeadd]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[SMGTgetSchoolGradeadd](@SchoolId varchar(20))
as begin
select 
Q.SchoolId, Q.GradeId, S.SchoolName,u.GradeName , Q.IsActive
from tblSchoolGrade Q  ,tblSchool S , tblgrade U
where q.SchoolId = s.SchoolId
and q.GradeId=u.GradeId

and q.SchoolId = @SchoolId
end





GO
/****** Object:  StoredProcedure [dbo].[SMGTgetSchoolHouseadd]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SMGTgetSchoolHouseadd](@SchoolId varchar(20))
as begin
select 
Q.HouseId, Q.HouseName,Q.HouseInchargeId,S.SchoolName, Q.IsActive
from tblHouse Q  ,tblSchool S 
where q.SchoolId = s.SchoolId

and q.IsActive like 'Y'
and q.SchoolId = @SchoolId
end





GO
/****** Object:  StoredProcedure [dbo].[SMGTgetSchoolSubadd]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SMGTgetSchoolSubadd](@SchoolId varchar(20),@academicyear varchar(20))
as begin
select 
Q.SchoolId,q.SubjectId, S.SchoolName , Q.IsActive,u.SubjectName,sc.SubjectCategoryName,u.ShortName
from tblSchoolSubject Q  ,tblSchool S , tblSubject u,tblSubjectCategory sc
where q.SchoolId = s.SchoolId
and q.SubjectId=u.SubjectId
and q.SubjectCategoryId=sc.SubjectCategoryId

and q.SchoolId=@SchoolId
and q.AcademicYear like @academicyear
end





GO
/****** Object:  StoredProcedure [dbo].[SMGTgetSchoolTeacher]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SMGTgetSchoolTeacher](@SchoolId varchar(14))
as begin
select
T.TeacherId, 
Name, 
UserId, 
T.IsActive

from tblTeacher T,tblSchool S, tblTeacherSchool C
Where T.TeacherId = C.TeacherId
and s.SchoolId = C.SchoolId
and C.SchoolId like @SchoolId

End





GO
/****** Object:  StoredProcedure [dbo].[SMGTgetSchoolteachers]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[SMGTgetSchoolteachers](@SchoolId varchar(20))
as begin
select 
q.TeacherId, q.Name
from tblTeacher Q  ,tblTeacherSchool r 
where q.TeacherId = r.TeacherId


and r.SchoolId like @SchoolId

end





GO
/****** Object:  StoredProcedure [dbo].[SMGTgetStoPMessageView]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROC [dbo].[SMGTgetStoPMessageView](@MessageId bigint, @ParentId bigint)
as Begin
Select 
H.SchoolId, H.MessageId, D.ParentId,P.ParentName, MessageType,M.MessageTypeDescription, Message, H.CreatedBy, H.CreatedDate, Subject,
SeqNo,Sender,U.PersonName
from tblSchoolToParentMessageHeader H, tblSchoolToParentMessageDetail D , tblMessageType M, tblUser U,tblParent P
where H.SchoolId = D.SchoolId
and H.MessageId = D.MessageId
and H.MessageType = M.MessageTypeId
and U.UserId = H.CreatedBy
and D.ParentId = P.ParentId
and D.ParentId = @ParentId
and H.MessageId = @MessageId

End 





GO
/****** Object:  StoredProcedure [dbo].[SMGTGetStudent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[SMGTGetStudent](@SchoolId varchar(24))

AS
BEGIN

	SELECT a.* ,b.SchoolName,g.GradeName,c.ClassName from tblStudent a ,tblSchool b ,tblGrade g,tblClass c   where 
	a.IsActive='Y' and a.SchoolId=b.SchoolId 
	and a.ClassId=c.ClassId
	and a.GradeId=g.GradeId
	and c.GradeId=a.GradeId
	and a.SchoolId=c.SchoolId
	and b.SchoolId like @SchoolId


END


GO
/****** Object:  StoredProcedure [dbo].[SMGTgetStudentExtraCadd]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SMGTgetStudentExtraCadd](@SchoolId varchar(20),@StudentId varchar(20))
as begin
select 
Q.ActivityCode, Q.ActivityName,S.StudentId,s.studentName,c.SchoolId
from tblExtraCurricularActivity Q  ,tblStudent S,tblStudentExtraCurricularActivity c 
where q.ActivityCode = c.ActivityCode
and s.SchoolId=c.SchoolId
and s.StudentId=c.StudentId

and c.SchoolId like @SchoolId
and c.StudentId like @StudentId
end





GO
/****** Object:  StoredProcedure [dbo].[SMGTgetStudentforoptionalSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SMGTgetStudentforoptionalSubject](@SchoolId varchar(20),@GradeId varchar(50),@AcademicYear varchar(6),@ClassId varchar(20))
as begin
select 
a.StudentId,a.StudentName
from tblStudent a
where 
 a.AcademicYear like @AcademicYear
and a.GradeId like @GradeId
and a.ClassId = @ClassId
and a.SchoolId like @SchoolId
and a.IsActive='Y'

end





GO
/****** Object:  StoredProcedure [dbo].[SMGTgetStudentOptionalSubjects]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SMGTgetStudentOptionalSubjects] (@SchoolId varchar(50),@StudentId varchar(50))
as begin

SELECT 
T.SeqNo,
T.SchoolId,
T.AcademicYear,
s.SchoolName,
g.GradeName,
c.ClassName,
sb.SubjectName,
st.studentName,
T.StudentId,
T.GradeId,
T.ClassId,
T.SubjectId


FROM tblStudentOptionalSubject T, tblSchool S,tblgrade g,tblClass C,tblSubject sb,tblStudent st
WHERE 
T.SchoolId like @SchoolId
and
T.StudentId like @StudentId
and 
T.SchoolId=s.SchoolId
and 
t.StudentId=st.StudentId
and 
t.GradeId=g.GradeId
and
t.ClassId=c.ClassId and
t.SubjectId=sb.SubjectId
and 
t.GradeId=c.GradeId
and t.SchoolId=c.SchoolId
End






GO
/****** Object:  StoredProcedure [dbo].[SMGTGetStudentsAllSubjectMarks]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SMGTGetStudentsAllSubjectMarks](@SchoolId varchar(20),@StudentId varchar(20),@GradeID varchar(20),@AcademicYear varchar(20),@EvaluationType varchar(30))

AS
BEGIN

	SELECT a.studentName,a.StudentId,e.EvaluationNo,e.EvaluationType,e.AccedamicYear,d.Grade,d.Class,d.SchoolId,r.Mark,r.SubjectId,s.EvaluationTypeDesc,Sb.SubjectName from
	 tblStudent a ,tblEvaluationHeader e,tblEvaluationDetail d,tblEvaluationResult r,tblEvaluationType s,tblSubject Sb
	  where 
	a.IsActive='Y' and
	Sb.SubjectId=r.SubjectId and
	s.EvaluationTypeCode=e.EvaluationNo and
	d.EvaluationNo=e.EvaluationNo and
	d.EvaluationDetailSeqNo=r.EvaluationDetailSeqNo and
	r.StudentId=a.StudentId and
	r.SchoolId like @SchoolId and
	r.StudentId like @StudentId and
	r.Grade like @GradeID and
	e.EvaluationType like @EvaluationType

	and e.AccedamicYear like @AcademicYear

	 
	

END




GO
/****** Object:  StoredProcedure [dbo].[SMGTgetTeacher]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SMGTgetTeacher] (@TeacherID bigint)
as begin

SELECT 
TeacherId, 
Name, 
Address1, 
Address2, 
Address3, 
T.TeacherCategoryId,
C.TeacherCategoryName, 
Telephone, 
Gender, 
Description, 
EmployeeNo, 
NIC, 
DrivingLicense, 
Passport, 
T.UserId, 
ImgUrl, 
DateOfBirth, 
T.CreatedBy, 
T.CreatedDate, 
T.ModifiedBy, 
T.ModifiedDate, 
T.IsActive

FROM tblTeacher T, tblTeacherCategory C
WHERE TeacherId like @TeacherID
AND T.TeacherCategoryId = C.TeacherCategoryId

End




GO
/****** Object:  StoredProcedure [dbo].[SMGTgetTeacherExActivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE proc [dbo].[SMGTgetTeacherExActivity](@TeacherID bigint)
as begin
select 
T.TeacherId,C.Name, T.SchoolId, T.ActivityCode, E.ActivityName, T.IsActive
from tblTeacherExtraCurricularActivity T , tblExtraCurricularActivity E, tblSchool S, tblTeacher C
where T.TeacherId = C.TeacherId
and E.ActivityCode = T.ActivityCode
and s.SchoolId = T.SchoolId
and T.TeacherId = @TeacherID

End



GO
/****** Object:  StoredProcedure [dbo].[SMGTgetTeacherfromSchool]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROC [dbo].[SMGTgetTeacherfromSchool](@SchoolId varchar(14))
as begin

select a.TeacherId ,b.Name,a.SchoolId
from tblTeacherSchool a,tblTeacher b
where a.TeacherId=b.TeacherId
and a.SchoolId like @SchoolId

End







GO
/****** Object:  StoredProcedure [dbo].[SMGTgetTeacherQualification]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[SMGTgetTeacherQualification](@TeacherId bigint)
as begin
select 
Q.TeacherId, Q.SchoolId, S.SchoolName, Q.QualificationId,U.QualificationName, Q.IsActive
from tblTeacherQualification Q , tblTeacher T ,tblSchool S , tblQualification U
where q.QualificationId = u.QualificationId
and t.TeacherId = q.TeacherId
and s.SchoolId = q.SchoolId
and q.QualificationId = u.QualificationId
and q.TeacherId = @TeacherId
end



GO
/****** Object:  StoredProcedure [dbo].[SMGTgetTeachers]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SMGTgetTeachers]
as begin

SELECT 
TeacherId, 
Name, 
Address1, 
Address2, 
Address3, 
Telephone, 
Gender, 
Description, 
EmployeeNo, 
NIC, 
DrivingLicense, 
Passport,  
ImgUrl, 
DateOfBirth 
FROM tblTeacher
End




GO
/****** Object:  StoredProcedure [dbo].[SMGTgetTeacherSubjects]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SMGTgetTeacherSubjects](@TeacherId bigint)
as begin

select 
TeacherSubjectSeqNo, 
AcedemicYear, 
TeacherId, 
SchoolIds, 
G.GradeId, 
G.GradeName,
ClassId, 
S.SubjectId, 
S.SubjectName,
T.IsActive

from tblTeacherSubject T,tblGrade G,tblSubject S
where T.GradeId = G.GradeId
and  T.SubjectId = S.SubjectId
and T.TeacherId = @TeacherId

End



GO
/****** Object:  StoredProcedure [dbo].[SMGTgetUserCategoryFunction]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROC [dbo].[SMGTgetUserCategoryFunction](@UserCategoryId varchar(5))
as begin
select 
C.CategoryId,C.CategoryName, F.FunctionId,F.FunctionName, U.CreatedBy, 
U.CreatedDate, U.ModifiedBy, U.ModifiedDate, U.IsActive
from tblUserCategoryFunction U, tblFunction F , tblUserCategory C
where U.FunctionId = f.FunctionId
and U.CategoryId = C.CategoryId
and U.CategoryId like @UserCategoryId
end


GO
/****** Object:  StoredProcedure [dbo].[SMGTloadScholExtraCadd]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create proc [dbo].[SMGTloadScholExtraCadd](@SchoolId varchar(20),@StudentId varchar(20))
as begin
select 
Q.ActivityCode, Q.ActivityName,c.SchoolId
from tblExtraCurricularActivity Q  ,tblSchoolExtraCurricularActivity c 
where q.ActivityCode = c.ActivityCode


and c.SchoolId like @SchoolId

end





GO
/****** Object:  StoredProcedure [dbo].[SMGTModifyAdminUser]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create PROCEDURE [dbo].[SMGTModifyAdminUser]
(

@SchoolId varchar(20),@Adminid varchar(20),@PersonName varchar(20),@Mobile varchar(20),@Password varchar(100),@LoginEmail varchar(100),@Modifiedby varchar(20) )
AS

BEGIN

update tblUser
set 


PersonName= @PersonName,
Mobile=@Mobile,
Password=@Password,
LoginEmail=@LoginEmail,
ModifiedBy=@Modifiedby,
ModifiedDate=GETDATE()








where
SchoolId like @SchoolId and 
UserId like @Adminid
　

END





GO
/****** Object:  StoredProcedure [dbo].[SMGTModifyClassStatus]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[SMGTModifyClassStatus]
(

@SchoolId varchar(20),@ClassId varchar(20),@GradeId varchar(20))
AS

BEGIN

update tblClass
set 
IsActive='N'



where
SchoolId like @SchoolId and 
ClassId like @ClassId and
GradeId like @GradeId
　

END




GO
/****** Object:  StoredProcedure [dbo].[SMGTModifyEvaluationDetailStatus]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[SMGTModifyEvaluationDetailStatus]
(

@SchoolId varchar(20),@EvaluatDtailSeq varchar(20))
AS

BEGIN

delete from tblEvaluationDetail




where
SchoolId like @SchoolId and 
EvaluationDetailSeqNo like @EvaluatDtailSeq
　

END




GO
/****** Object:  StoredProcedure [dbo].[SMGTModifyEvaluationHeaderStatus]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[SMGTModifyEvaluationHeaderStatus]
(

@SchoolId varchar(20),@EvaluationNo varchar(20))
AS

BEGIN

update tblEvaluationHeader
set 
IsActive='N'



where
SchoolId like @SchoolId and 
EvaluationNo like @EvaluationNo
　

END




GO
/****** Object:  StoredProcedure [dbo].[SMGTModifySchoolHouseStatus]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[SMGTModifySchoolHouseStatus]
(

@SchoolId varchar(20),@HouseId varchar(20))
AS

BEGIN

update tblHouse
set 
IsActive='N'



where
SchoolId like @SchoolId and 
HouseId like @HouseId
　

END



GO
/****** Object:  StoredProcedure [dbo].[SMGTModifyStudent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create  PROCEDURE [dbo].[SMGTModifyStudent]
(

@SchoolId varchar(20),@StudentId varchar(20),@studentName varchar(100),@DateofBirth datetime,@GradeId varchar(20),@ClassId varchar(20),@Gender varchar(2),@UserId varchar(50),@HouseId varchar(30),@ImgUrl varchar(200),@ModifiedBy varchar(50),@IsActive varchar(2))
AS

BEGIN

update tblStudent
set 


SchoolId=@SchoolId,
StudentId=@StudentId,
studentName=@studentName,
DateofBirth=@DateofBirth,
GradeId=@GradeId,
ClassId=@ClassId,
Gender=@Gender,
UserId=@UserId,
HouseId=@HouseId,
ImgUrl=@ImgUrl,
ModifiedBy=@ModifiedBy,
ModifiedDate=GETDATE(),
IsActive=@IsActive

where
SchoolId=@SchoolId
and StudentId=@StudentId
　

END



GO
/****** Object:  StoredProcedure [dbo].[SMGTSchoolCount]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SMGTSchoolCount]
AS
BEGIN

select
	COUNT (SchoolId)
	from
	tblSchool
	


END




GO
/****** Object:  StoredProcedure [dbo].[SMGTsetParent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[SMGTsetParent](@ParentName varchar(50),@RelationshipId int,@Address1 varchar(150),@Address2 varchar(150),@Address3 varchar(150),@HomeTelephone varchar(20),@PersonalEmail varchar(100),@PersonalMobile varchar(20),@Occupation varchar(50),@OfficeAddress1 varchar(150),@OfficeAddress2 varchar(150),@OfficeAddress3 varchar(150),@OfficePhone varchar(20),@officeEmail varchar(50),@NIC varchar(20),@UserId varchar(20),@ImgUrl varchar(200),@DateofBirth date ,@CreatedBy varchar(20),@IsActive  varchar(2),@SchoolId varchar(20))
AS

BEGIN


insert into tblParent
(ParentName,RelationshipId,Address1,Address2,Address3,HomeTelephone,PersonalEmail,PersonalMobile,Occupation,OfficeAddress1,OfficeAddress2,OfficeAddress3,OfficePhone,officeEmail,NIC,UserId,ImgUrl,DateofBirth,CreatedBy,CreatedDate,IsActive,SchoolId)
values
(
@ParentName,@RelationshipId,@Address1,@Address2,@Address3,@HomeTelephone,@PersonalEmail,@PersonalMobile,@Occupation,@OfficeAddress1,@OfficeAddress2,@OfficeAddress3,@OfficePhone,@officeEmail,@NIC,@UserId,@ImgUrl,@DateofBirth,@CreatedBy,GetDate(),@IsActive,@SchoolId)
　

END












GO
/****** Object:  StoredProcedure [dbo].[SMGTsetSchoolGrade]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

Create PROCEDURE [dbo].[SMGTsetSchoolGrade]



	
	(@SchoolId varchar(20),@GradeId varchar(20),@CreatedBy varchar(20),@IsActive varchar(1))
AS
BEGIN
	
	SET NOCOUNT ON;

   INSERT INTO tblSchoolGrade(SchoolId,GradeId, CreatedBy, CreatedDate, IsActive)
   Values (@SchoolId,@GradeId,@CreatedBy,GETDATE(),@IsActive)
END






GO
/****** Object:  StoredProcedure [dbo].[SMGTsetStudent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE[dbo].[SMGTsetStudent](@SchoolId varchar(20),@StudentId varchar(100),@studentName varchar(100),@DateofBirth Datetime,@GradeId varchar (30),@ClassId varchar (50),@Gender varchar (50),@UserId varchar(50),@HouseId varchar(30),@ImgUrl varchar(200),@CreatedBy varchar(50),@IsActive varchar(50))
AS

BEGIN


insert into tblStudent
(SchoolId,StudentId,studentName,DateofBirth,GradeId,ClassId,Gender,UserId,HouseId,ImgUrl,CreatedBy,CreatedDate,IsActive,AcademicYear)
values
(
@SchoolId,@StudentId,@studentName,@DateofBirth,@GradeId,@ClassId,@Gender,@UserId,@HouseId,@ImgUrl,@CreatedBy,GETDATE(),@IsActive,YEAR(getdate()))
　

END










GO
/****** Object:  StoredProcedure [dbo].[SMGTsetStudentOptionalSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

create PROCEDURE [dbo].[SMGTsetStudentOptionalSubject]



	
	(@SchoolId varchar(20),@GradeId varchar(20),@CreatedBy varchar(20),@IsActive varchar(1),@AcademicYear varchar(20),@StudentId  varchar(50),@ClassId varchar(50),@SubjectId varchar(50) )
AS
BEGIN
	
	SET NOCOUNT ON;

   INSERT INTO tblStudentOptionalSubject(SchoolId,AcademicYear,StudentId,ClassId,SubjectId,GradeId, CreatedBy, CreatedDate, IsActive)
   Values (@SchoolId,@AcademicYear,@StudentId,@ClassId,@SubjectId,@GradeId,@CreatedBy,GETDATE(),@IsActive)
END








GO
/****** Object:  Table [dbo].[tblAccadamicYear]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAccadamicYear](
	[SeqNo] [int] IDENTITY(1,1) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[AccadamicYear] [varchar](4) NOT NULL,
	[CreatedDate] [date] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[ModifiedDate] [date] NULL,
	[ModifiedBy] [varchar](20) NULL,
 CONSTRAINT [PK_tblAccadamicYear] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblApplicationStatus]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblApplicationStatus](
	[StatusCode] [bigint] IDENTITY(1,1) NOT NULL,
	[StatusDescription] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblApplicationStatus] PRIMARY KEY CLUSTERED 
(
	[StatusCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAssignmentDueDate]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAssignmentDueDate](
	[DueId] [bigint] IDENTITY(1,1) NOT NULL,
	[AssignmentId] [bigint] NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[DueDate] [date] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblAssignmentDueDate] PRIMARY KEY CLUSTERED 
(
	[DueId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAssignmentHeader]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAssignmentHeader](
	[AssignmentNo] [bigint] IDENTITY(1,1) NOT NULL,
	[AssignmentDescription] [varchar](200) NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[GradeId] [varchar](20) NOT NULL,
	[ClassId] [varchar](20) NOT NULL,
	[StudentId] [varchar](20) NULL,
	[TeacherId] [bigint] NOT NULL,
	[FilePath] [varchar](200) NOT NULL,
	[BatchNo] [varchar](10) NULL,
	[BatchDescription] [varchar](200) NULL,
	[SubjectId] [int] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblAssignmentHeader] PRIMARY KEY CLUSTERED 
(
	[AssignmentNo] ASC,
	[SchoolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAssignmentQuestionAid]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAssignmentQuestionAid](
	[AidId] [bigint] IDENTITY(1,1) NOT NULL,
	[AssignmentId] [bigint] NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[QuestionId] [bigint] NOT NULL,
	[SubQuestionId] [varchar](20) NULL,
	[ImagePath] [varchar](200) NULL,
	[TextFilePath] [varchar](200) NULL,
	[AidType] [varchar](1) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblAssignmentQuestionAid] PRIMARY KEY CLUSTERED 
(
	[AidId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAssignmentQuestionAnswer]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAssignmentQuestionAnswer](
	[AnswerSeqNo] [bigint] IDENTITY(1,1) NOT NULL,
	[AssignmentId] [bigint] NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[QuestionId] [bigint] NOT NULL,
	[SubQuestionId] [varchar](20) NULL,
	[AnswerId] [varchar](20) NOT NULL,
	[Answer] [varchar](100) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblQuestionAnswer] PRIMARY KEY CLUSTERED 
(
	[AnswerSeqNo] ASC,
	[SchoolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAssignmentQuestionType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAssignmentQuestionType](
	[QuestionTypeId] [int] IDENTITY(1,1) NOT NULL,
	[TypeDescription] [varchar](20) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblQuestionType] PRIMARY KEY CLUSTERED 
(
	[QuestionTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblAssingmentQuestion]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblAssingmentQuestion](
	[AssingmentNo] [bigint] NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[QustionId] [bigint] IDENTITY(1,1) NOT NULL,
	[SubQuestionNo] [varchar](20) NULL,
	[QuestionType] [int] NOT NULL,
	[QuestionNo] [varchar](20) NOT NULL,
	[LevelId] [int] NULL,
	[Question] [varchar](200) NOT NULL,
	[Hint] [varchar](200) NULL,
	[EntitleMark] [decimal](3, 2) NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
	[ParentObservationType] [int] NULL,
	[ParentDescription] [varchar](200) NULL,
 CONSTRAINT [PK_tblAssingmentQuestion] PRIMARY KEY CLUSTERED 
(
	[SchoolId] ASC,
	[QustionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_tblAssingmentQuestion] UNIQUE NONCLUSTERED 
(
	[AssingmentNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblClass]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblClass](
	[ClassId] [varchar](20) NOT NULL,
	[ClassName] [varchar](50) NOT NULL,
	[GradeId] [varchar](20) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblClass] PRIMARY KEY CLUSTERED 
(
	[ClassId] ASC,
	[GradeId] ASC,
	[SchoolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblClassTeacher]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblClassTeacher](
	[SchoolId] [varchar](14) NOT NULL,
	[TeacherId] [varchar](20) NOT NULL,
	[GradeId] [varchar](20) NOT NULL,
	[ClassId] [varchar](20) NOT NULL,
	[AccedamicYear] [varchar](4) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblClassTeacher] PRIMARY KEY CLUSTERED 
(
	[SchoolId] ASC,
	[TeacherId] ASC,
	[GradeId] ASC,
	[ClassId] ASC,
	[AccedamicYear] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblDaysOfWeek]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDaysOfWeek](
	[DayNo] [int] IDENTITY(1,1) NOT NULL,
	[DayName] [varchar](50) NULL,
 CONSTRAINT [PK_tblDaysOfWeek] PRIMARY KEY CLUSTERED 
(
	[DayNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblDistrict]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDistrict](
	[DistrictId] [int] IDENTITY(1,1) NOT NULL,
	[DistrictName] [varchar](50) NOT NULL,
	[ProvinceId] [int] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblDistrict] PRIMARY KEY CLUSTERED 
(
	[DistrictId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblDivision]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblDivision](
	[DivisionId] [int] IDENTITY(1,1) NOT NULL,
	[DivisionName] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblDivision] PRIMARY KEY CLUSTERED 
(
	[DivisionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblEntranceApplication]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblEntranceApplication](
	[AppStatusSeqNo] [bigint] IDENTITY(1,1) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[RefNo] [varchar](50) NOT NULL,
	[StatusCode] [int] NOT NULL,
	[Remarks] [varchar](200) NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblEntranceApplication] PRIMARY KEY CLUSTERED 
(
	[AppStatusSeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblEvaluationDetail]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblEvaluationDetail](
	[EvaluationDetailSeqNo] [bigint] IDENTITY(1,1) NOT NULL,
	[EvaluationNo] [bigint] NOT NULL,
	[Grade] [varchar](20) NOT NULL,
	[Class] [varchar](20) NOT NULL,
	[ScheduledDate] [date] NOT NULL,
	[ScheduledTimeStart] [time](7) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
	[ScheduledTimeEnd] [time](7) NOT NULL,
 CONSTRAINT [PK_tblEvaluationDetail] PRIMARY KEY CLUSTERED 
(
	[EvaluationDetailSeqNo] ASC,
	[SchoolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblEvaluationHeader]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblEvaluationHeader](
	[EvaluationNo] [bigint] IDENTITY(1,1) NOT NULL,
	[EvaluationDescription] [varchar](100) NOT NULL,
	[EvaluationType] [bigint] NOT NULL,
	[AccedamicYear] [varchar](4) NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[TestPaperFee] [decimal](18, 2) NULL,
	[isActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblEvaluationHeader] PRIMARY KEY CLUSTERED 
(
	[EvaluationNo] ASC,
	[SchoolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblEvaluationResult]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblEvaluationResult](
	[EveluationResultSeqNo] [bigint] IDENTITY(1,1) NOT NULL,
	[EvaluationDetailSeqNo] [bigint] NOT NULL,
	[StudentId] [varchar](20) NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[Mark] [decimal](18, 2) NOT NULL,
	[Grade] [varchar](20) NOT NULL,
	[TeacherComment] [varchar](100) NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblEvaluationResult_1] PRIMARY KEY CLUSTERED 
(
	[EveluationResultSeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblEvaluationType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblEvaluationType](
	[EvaluationTypeCode] [bigint] IDENTITY(1,1) NOT NULL,
	[EvaluationTypeDesc] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblStudentEvaluationType] PRIMARY KEY CLUSTERED 
(
	[EvaluationTypeCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblEventCalendar]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblEventCalendar](
	[SchoolId] [varchar](14) NOT NULL,
	[EventNo] [bigint] IDENTITY(1,1) NOT NULL,
	[EventTitle] [varchar](150) NOT NULL,
	[EventDescription] [varchar](200) NOT NULL,
	[FromDate] [date] NOT NULL,
	[ToDate] [date] NOT NULL,
	[FromTime] [time](7) NOT NULL,
	[ToTime] [time](7) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[EventOrganizer] [varchar](100) NOT NULL,
	[EventCategory] [bigint] NOT NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblEventCalendar] PRIMARY KEY CLUSTERED 
(
	[EventNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblEventcategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblEventcategory](
	[EventCategoryId] [bigint] IDENTITY(1,1) NOT NULL,
	[EventCategoryDesc] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[ParentApprovalNeeded] [varchar](1) NOT NULL,
	[BroadcastMessage] [varchar](1) NOT NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblEventcategory] PRIMARY KEY CLUSTERED 
(
	[EventCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblEventParticipant]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblEventParticipant](
	[SchoolId] [varchar](14) NOT NULL,
	[EventNo] [bigint] NOT NULL,
	[ParticipantUserId] [varchar](20) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsConfirmed] [varchar](1) NOT NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblEventParticipant] PRIMARY KEY CLUSTERED 
(
	[SchoolId] ASC,
	[EventNo] ASC,
	[ParticipantUserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblExtraCurricularActivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblExtraCurricularActivity](
	[ActivityCode] [varchar](6) NOT NULL,
	[ActivityName] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblExtraCurricularActivity] PRIMARY KEY CLUSTERED 
(
	[ActivityCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblFunction]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblFunction](
	[FunctionId] [varchar](5) NOT NULL,
	[FunctionName] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblFunction] PRIMARY KEY CLUSTERED 
(
	[FunctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblGrade]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblGrade](
	[GradeId] [varchar](20) NOT NULL,
	[GradeName] [varchar](50) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblGrade] PRIMARY KEY CLUSTERED 
(
	[GradeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblGradeSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblGradeSubject](
	[AcademicYear] [varchar](4) NOT NULL,
	[GradeId] [varchar](20) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[SubjectCategoryId] [int] NOT NULL,
	[Optional] [varchar](1) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
	[SchoolId] [varchar](14) NULL,
 CONSTRAINT [PK_tblGradeSubject] PRIMARY KEY CLUSTERED 
(
	[AcademicYear] ASC,
	[GradeId] ASC,
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblHouse]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblHouse](
	[SchoolId] [varchar](14) NOT NULL,
	[HouseId] [varchar](20) NOT NULL,
	[HouseName] [varchar](50) NOT NULL,
	[HouseInchargeId] [varchar](20) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblHouse] PRIMARY KEY CLUSTERED 
(
	[SchoolId] ASC,
	[HouseId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblMessageType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblMessageType](
	[MessageTypeId] [bigint] IDENTITY(1,1) NOT NULL,
	[MessageTypeDescription] [varchar](100) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblMessageType] PRIMARY KEY CLUSTERED 
(
	[MessageTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblParameters]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblParameters](
	[SeqNo] [bigint] IDENTITY(1,1) NOT NULL,
	[ParameterId] [varchar](10) NOT NULL,
	[ParameterName] [varchar](50) NOT NULL,
	[ParameterValue] [varchar](50) NOT NULL,
 CONSTRAINT [PK_tblParameters] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_tblParameters] UNIQUE NONCLUSTERED 
(
	[ParameterId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblParent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblParent](
	[ParentId] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentName] [varchar](100) NOT NULL,
	[RelationshipId] [int] NOT NULL,
	[Address1] [varchar](150) NOT NULL,
	[Address2] [varchar](150) NOT NULL,
	[Address3] [varchar](150) NULL,
	[HomeTelephone] [varchar](20) NULL,
	[PersonalEmail] [varchar](100) NOT NULL,
	[PersonalMobile] [varchar](20) NULL,
	[Occupation] [varchar](50) NULL,
	[OfficeAddress1] [varchar](150) NULL,
	[OfficeAddress2] [varchar](150) NULL,
	[OfficeAddress3] [varchar](150) NULL,
	[OfficePhone] [varchar](20) NULL,
	[officeEmail] [varchar](100) NULL,
	[NIC] [varchar](20) NULL,
	[UserId] [varchar](20) NOT NULL,
	[ImgUrl] [varchar](200) NULL,
	[DateofBirth] [date] NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
	[SchoolId] [varchar](14) NULL,
 CONSTRAINT [PK_tblParent] PRIMARY KEY CLUSTERED 
(
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblParentObservationType]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblParentObservationType](
	[ObTypeId] [int] IDENTITY(1,1) NOT NULL,
	[Description] [varchar](100) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblParentObservationType] PRIMARY KEY CLUSTERED 
(
	[ObTypeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblParentStudent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblParentStudent](
	[SeqNo] [bigint] IDENTITY(1,1) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[ParentId] [bigint] NOT NULL,
	[StudentId] [varchar](20) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblParentStudent] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblParentToSchollMessageAttachments]    Script Date: 3/4/2019 11:50:30 AM ******/
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
	[SeqNo] [bigint] NOT NULL,
 CONSTRAINT [PK_tblParentToSchollMessageAttachments] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblParentToSchoolMessageDetail]    Script Date: 3/4/2019 11:50:30 AM ******/
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
/****** Object:  Table [dbo].[tblParentToSchoolMessageHeader]    Script Date: 3/4/2019 11:50:30 AM ******/
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
/****** Object:  Table [dbo].[tblProvince]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblProvince](
	[ProvinceId] [int] NOT NULL,
	[ProvinceName] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblProvince] PRIMARY KEY CLUSTERED 
(
	[ProvinceId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblQualification]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblQualification](
	[QualificationId] [int] IDENTITY(1,1) NOT NULL,
	[QualificationName] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblQualification] PRIMARY KEY CLUSTERED 
(
	[QualificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblRelashionship]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblRelashionship](
	[RelashionshipId] [int] IDENTITY(1,1) NOT NULL,
	[RelashionshipName] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblRelashionship] PRIMARY KEY CLUSTERED 
(
	[RelashionshipId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSchool]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSchool](
	[SchoolId] [varchar](14) NOT NULL,
	[SchoolName] [varchar](100) NOT NULL,
	[Address1] [varchar](150) NOT NULL,
	[Address2] [varchar](150) NULL,
	[Address3] [varchar](150) NULL,
	[Telephone] [varchar](20) NOT NULL,
	[Email] [varchar](50) NULL,
	[WebUrl] [varchar](100) NULL,
	[Fax] [varchar](20) NULL,
	[Province] [int] NULL,
	[District] [int] NULL,
	[Division] [int] NULL,
	[SchoolGroup] [bigint] NULL,
	[SchoolRank] [int] NULL,
	[Description] [varchar](500) NULL,
	[LogoPath] [nvarchar](500) NULL,
	[ImagePath] [nvarchar](500) NULL,
	[MinuteforPeriod] [int] NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](20) NULL,
	[SchoolCategory] [int] NOT NULL,
	[IsActive] [varchar](1) NOT NULL,
	[AccadamicYear] [varchar](4) NULL,
 CONSTRAINT [PK_tblSchool] PRIMARY KEY CLUSTERED 
(
	[SchoolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSchoolCalendar]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSchoolCalendar](
	[CalenderSeqNo] [bigint] IDENTITY(1,1) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[AcadamicYear] [varchar](4) NOT NULL,
	[AcadamicDate] [datetime] NULL,
	[IsHoliday] [varchar](1) NOT NULL,
	[SpecialComment] [varchar](100) NOT NULL,
	[DateComment] [varchar](50) NOT NULL,
	[FromDate] [date] NOT NULL,
	[ToDate] [date] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblSchoolCalendar] PRIMARY KEY CLUSTERED 
(
	[CalenderSeqNo] ASC,
	[SchoolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSchoolCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSchoolCategory](
	[SchoolCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[SchoolCategoryName] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblSchoolCategory] PRIMARY KEY CLUSTERED 
(
	[SchoolCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSchoolExtraCurricularActivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSchoolExtraCurricularActivity](
	[SchoolId] [varchar](14) NOT NULL,
	[ActivityCode] [varchar](6) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblSchoolExtraCurricularActivity] PRIMARY KEY CLUSTERED 
(
	[SchoolId] ASC,
	[ActivityCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSchoolGrade]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSchoolGrade](
	[SchoolId] [varchar](14) NOT NULL,
	[GradeId] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblSchoolGrade] PRIMARY KEY CLUSTERED 
(
	[SchoolId] ASC,
	[GradeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSchoolGroup]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSchoolGroup](
	[GroupId] [bigint] IDENTITY(1,1) NOT NULL,
	[GroupName] [varchar](100) NOT NULL,
	[IsActive] [varchar](1) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[ModifiedDate] [datetime] NULL,
	[ModifiedBy] [varchar](20) NULL,
 CONSTRAINT [PK_tblSchoolGroup] PRIMARY KEY CLUSTERED 
(
	[GroupId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSchoolRank]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSchoolRank](
	[SchoolRankId] [int] IDENTITY(1,1) NOT NULL,
	[SchoolRankName] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblSchoolRank] PRIMARY KEY CLUSTERED 
(
	[SchoolRankId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSchoolSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSchoolSubject](
	[AcademicYear] [varchar](4) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[SubjectCategoryId] [int] NOT NULL,
	[Optional] [varchar](1) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblSchoolSubject] PRIMARY KEY CLUSTERED 
(
	[AcademicYear] ASC,
	[SchoolId] ASC,
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSchoolToParentMessageAttachment]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSchoolToParentMessageAttachment](
	[SeqNo] [bigint] NOT NULL,
	[MessageId] [bigint] NOT NULL,
	[AttachmentName] [varchar](350) NOT NULL,
	[AttachmentPath] [varchar](350) NOT NULL,
 CONSTRAINT [PK_tblSchoolToParentMessageAttachment] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSchoolToParentMessageDetail]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSchoolToParentMessageDetail](
	[SeqNo] [bigint] IDENTITY(1,1) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[MessageId] [bigint] NOT NULL,
	[ParentId] [bigint] NOT NULL,
	[Status] [varchar](1) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblSchoolToParentMessageDetail] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSchoolToParentMessageHeader]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSchoolToParentMessageHeader](
	[SchoolId] [varchar](14) NOT NULL,
	[MessageId] [bigint] NOT NULL,
	[Sender] [varchar](250) NOT NULL,
	[Subject] [varchar](100) NOT NULL,
	[MessageType] [bigint] NOT NULL,
	[Message] [varchar](1000) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblSchoolToParentMessageHeader] PRIMARY KEY CLUSTERED 
(
	[SchoolId] ASC,
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblStudent]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblStudent](
	[SchoolId] [varchar](14) NOT NULL,
	[StudentId] [varchar](20) NOT NULL,
	[AcademicYear] [varchar](4) NOT NULL,
	[studentName] [varchar](100) NOT NULL,
	[DateofBirth] [date] NULL,
	[GradeId] [varchar](20) NOT NULL,
	[ClassId] [varchar](20) NOT NULL,
	[Gender] [varchar](1) NOT NULL,
	[UserId] [varchar](20) NOT NULL,
	[HouseId] [varchar](20) NULL,
	[ImgUrl] [varchar](200) NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblStudent] PRIMARY KEY CLUSTERED 
(
	[SchoolId] ASC,
	[StudentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblStudentExtraCurricularActivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblStudentExtraCurricularActivity](
	[StudentId] [varchar](20) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[ActivityCode] [varchar](6) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblStudentExtraCurricularActivity] PRIMARY KEY CLUSTERED 
(
	[StudentId] ASC,
	[SchoolId] ASC,
	[ActivityCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblStudentHistory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblStudentHistory](
	[SeqNo] [bigint] IDENTITY(1,1) NOT NULL,
	[AcademicYear] [varchar](4) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[StudentId] [varchar](20) NOT NULL,
	[GradeId] [varchar](20) NOT NULL,
	[ClassId] [varchar](20) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreadedDate] [datetime] NOT NULL,
	[ModifyedBy] [varchar](20) NULL,
	[ModifyedDate] [datetime] NULL,
 CONSTRAINT [PK_tblStudentHistory] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblStudentOptionalSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblStudentOptionalSubject](
	[SeqNo] [bigint] IDENTITY(1,1) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[AcademicYear] [varchar](4) NOT NULL,
	[StudentId] [varchar](20) NOT NULL,
	[GradeId] [varchar](20) NOT NULL,
	[ClassId] [varchar](20) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblStudentOptionalSubject] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSubject](
	[SubjectId] [int] IDENTITY(1,1) NOT NULL,
	[ShortName] [varchar](20) NULL,
	[SubjectName] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblSubject] PRIMARY KEY CLUSTERED 
(
	[SubjectId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSubjectCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSubjectCategory](
	[SubjectCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[SubjectCategoryName] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblSubjectCategory] PRIMARY KEY CLUSTERED 
(
	[SubjectCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTeacher]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTeacher](
	[TeacherId] [bigint] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](100) NOT NULL,
	[Address1] [varchar](150) NOT NULL,
	[Address2] [varchar](150) NULL,
	[Address3] [varchar](150) NULL,
	[TeacherCategoryId] [int] NOT NULL,
	[Telephone] [varchar](20) NULL,
	[Gender] [varchar](1) NOT NULL,
	[Description] [varchar](500) NULL,
	[EmployeeNo] [varchar](20) NOT NULL,
	[NIC] [varchar](20) NULL,
	[DrivingLicense] [varchar](20) NULL,
	[Passport] [varchar](20) NULL,
	[UserId] [varchar](20) NOT NULL,
	[ImgUrl] [varchar](200) NULL,
	[DateOfBirth] [date] NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblTeacher] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTeacherCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTeacherCategory](
	[TeacherCategoryId] [int] IDENTITY(1,1) NOT NULL,
	[TeacherCategoryName] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblTeacherCategory] PRIMARY KEY CLUSTERED 
(
	[TeacherCategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTeacherExtraCurricularActivity]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTeacherExtraCurricularActivity](
	[TeacherId] [bigint] NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[ActivityCode] [varchar](6) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblTeacherExtraCurricularActivity] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC,
	[SchoolId] ASC,
	[ActivityCode] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTeacherQualification]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTeacherQualification](
	[TeacherId] [bigint] NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[QualificationId] [int] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblTeacherQualification] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC,
	[SchoolId] ASC,
	[QualificationId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTeacherSchool]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTeacherSchool](
	[TeacherId] [bigint] NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[TeacherCategoryId] [int] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblTeacherSchool] PRIMARY KEY CLUSTERED 
(
	[TeacherId] ASC,
	[SchoolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTeacherSubject]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTeacherSubject](
	[TeacherSubjectSeqNo] [bigint] IDENTITY(1,1) NOT NULL,
	[AcedemicYear] [varchar](4) NOT NULL,
	[TeacherId] [bigint] NOT NULL,
	[SchoolIds] [varchar](14) NOT NULL,
	[GradeId] [varchar](20) NOT NULL,
	[ClassId] [varchar](20) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblTeacherSubject_1] PRIMARY KEY CLUSTERED 
(
	[TeacherSubjectSeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_tblTeacherSubject] UNIQUE NONCLUSTERED 
(
	[TeacherSubjectSeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblTimeTable]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblTimeTable](
	[SeqNo] [bigint] IDENTITY(1,1) NOT NULL,
	[AcademicYear] [varchar](4) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[GradeId] [varchar](20) NOT NULL,
	[ClassId] [varchar](20) NOT NULL,
	[Day] [varchar](20) NOT NULL,
	[SubjectId] [int] NOT NULL,
	[FromTime] [time](7) NOT NULL,
	[ToTime] [time](7) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
	[PeriodSequenceNo] [int] NOT NULL,
 CONSTRAINT [PK_tblTimeTable] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUser](
	[UserId] [varchar](20) NOT NULL,
	[LoginEmail] [varchar](50) NOT NULL,
	[Password] [varchar](150) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[UserCategory] [varchar](5) NOT NULL,
	[PersonName] [varchar](100) NOT NULL,
	[JobDescription] [varchar](100) NULL,
	[Mobile] [varchar](20) NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblUser] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
 CONSTRAINT [IX_tblUser] UNIQUE NONCLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUserCategory]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUserCategory](
	[CategoryId] [varchar](5) NOT NULL,
	[CategoryName] [varchar](50) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
	[IsApplicationSide] [varchar](1) NULL,
 CONSTRAINT [PK_tblUserCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUserCategoryFunction]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUserCategoryFunction](
	[CategoryId] [varchar](5) NOT NULL,
	[FunctionId] [varchar](5) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblUserCategoryFunction] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC,
	[FunctionId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUserCode]    Script Date: 3/4/2019 11:50:30 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblUserCode](
	[Seqno] [bigint] IDENTITY(1,1) NOT NULL,
	[UserId] [varchar](20) NOT NULL,
	[Code] [bigint] NOT NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblUserCode] PRIMARY KEY CLUSTERED 
(
	[Seqno] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
ALTER TABLE [dbo].[tblApplicationStatus] ADD  CONSTRAINT [DF_tblApplicationStatus_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblAssignmentDueDate] ADD  CONSTRAINT [DF_tblAssignmentDueDate_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblAssignmentHeader] ADD  CONSTRAINT [DF_tblAssignmentHeader_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAid] ADD  CONSTRAINT [DF_tblAssignmentQuestionAid_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAnswer] ADD  CONSTRAINT [DF_tblQuestionAnswer_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblAssignmentQuestionType] ADD  CONSTRAINT [DF_tblQuestionType_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblAssingmentQuestion] ADD  CONSTRAINT [DF_tblQuestion_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblClass] ADD  CONSTRAINT [DF_tblClass_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblClassTeacher] ADD  CONSTRAINT [DF_tblClassTeacher_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblDistrict] ADD  CONSTRAINT [DF_tblDistrict_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblDivision] ADD  CONSTRAINT [DF_tblDivision_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblEntranceApplication] ADD  CONSTRAINT [DF_tblEntranceApplication_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblEvaluationDetail] ADD  CONSTRAINT [DF_tblEvaluationDetail_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblEvaluationHeader] ADD  CONSTRAINT [DF_tblEvaluationHeader_isActive]  DEFAULT ('Y') FOR [isActive]
GO
ALTER TABLE [dbo].[tblEvaluationResult] ADD  CONSTRAINT [DF_tblEvaluationResult_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblEvaluationType] ADD  CONSTRAINT [DF_tblStudentEvaluationType_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblEventCalendar] ADD  CONSTRAINT [DF_tblEventCalendar_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblEventcategory] ADD  CONSTRAINT [DF_tblEventcategory_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblEventParticipant] ADD  CONSTRAINT [DF_tblEventParticipant_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblExtraCurricularActivity] ADD  CONSTRAINT [DF_tblExtraCurricularActivity_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblFunction] ADD  CONSTRAINT [DF_tblFunction_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblGrade] ADD  CONSTRAINT [DF_tblGrade_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblGradeSubject] ADD  CONSTRAINT [DF_tblGradeSubject_Optional]  DEFAULT ('Y') FOR [Optional]
GO
ALTER TABLE [dbo].[tblGradeSubject] ADD  CONSTRAINT [DF_tblGradeSubject_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblHouse] ADD  CONSTRAINT [DF_tblHouse_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblMessageType] ADD  CONSTRAINT [DF_tblMessageType_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblParent] ADD  CONSTRAINT [DF_tblParent_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblParentObservationType] ADD  CONSTRAINT [DF_tblParentObservationType_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblParentStudent] ADD  CONSTRAINT [DF_tblParentStudent_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageDetail] ADD  CONSTRAINT [DF_tblParentToSchoolMessageDetail_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader] ADD  CONSTRAINT [DF_tblParentToSchoolMessageHeader_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblProvince] ADD  CONSTRAINT [DF_tblProvince_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblQualification] ADD  CONSTRAINT [DF_tblQualification_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblRelashionship] ADD  CONSTRAINT [DF_tblRelashionship_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblSchool] ADD  CONSTRAINT [DF_tblSchool_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblSchoolCalendar] ADD  CONSTRAINT [DF_tblSchoolCalendar_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblSchoolCategory] ADD  CONSTRAINT [DF_tblSchoolCategory_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblSchoolExtraCurricularActivity] ADD  CONSTRAINT [DF_tblSchoolExtraCurricularActivity_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblSchoolGrade] ADD  CONSTRAINT [DF_tblSchoolGrade_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblSchoolGroup] ADD  CONSTRAINT [DF_tblSchoolGroup_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblSchoolRank] ADD  CONSTRAINT [DF_tblSchoolRank_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblSchoolSubject] ADD  CONSTRAINT [DF_tblSchoolSubject_Optional]  DEFAULT ('Y') FOR [Optional]
GO
ALTER TABLE [dbo].[tblSchoolSubject] ADD  CONSTRAINT [DF_tblSchoolSubject_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageDetail] ADD  CONSTRAINT [DF_tblSchoolToParentMessageDetail_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageHeader] ADD  CONSTRAINT [DF_tblSchoolToParentMessageHeader_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblStudent] ADD  CONSTRAINT [DF_tblStudent_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblStudentExtraCurricularActivity] ADD  CONSTRAINT [DF_tblStudentExtraCurricularActivity_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject] ADD  CONSTRAINT [DF_tblStudentOptionalSubject_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblSubject] ADD  CONSTRAINT [DF_tblSubject_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblSubjectCategory] ADD  CONSTRAINT [DF_tblSubjectCategory_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblTeacher] ADD  CONSTRAINT [DF_tblTeacher_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblTeacherCategory] ADD  CONSTRAINT [DF_tblTeacherCategory_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblTeacherExtraCurricularActivity] ADD  CONSTRAINT [DF_tblTeacherExtraCurricularActivity_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblTeacherQualification] ADD  CONSTRAINT [DF_tblTeacherQualification_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblTeacherSchool] ADD  CONSTRAINT [DF_tblTeacherSchool_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblTeacherSubject] ADD  CONSTRAINT [DF_tblTeacherSubject_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblTimeTable] ADD  CONSTRAINT [DF_tblTimeTable_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblUser] ADD  CONSTRAINT [DF_tblUser_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblUserCategory] ADD  CONSTRAINT [DF_tblUserCategory_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblUserCategoryFunction] ADD  CONSTRAINT [DF_tblUserCategoryFunction_IsActive]  DEFAULT ('Y') FOR [IsActive]
GO
ALTER TABLE [dbo].[tblAccadamicYear]  WITH CHECK ADD  CONSTRAINT [FK_tblAccadamicYear_tblAccadamicYear] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblAccadamicYear] CHECK CONSTRAINT [FK_tblAccadamicYear_tblAccadamicYear]
GO
ALTER TABLE [dbo].[tblAccadamicYear]  WITH CHECK ADD  CONSTRAINT [FK_tblAccadamicYear_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblAccadamicYear] CHECK CONSTRAINT [FK_tblAccadamicYear_tblUser]
GO
ALTER TABLE [dbo].[tblAccadamicYear]  WITH CHECK ADD  CONSTRAINT [FK_tblAccadamicYear_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblAccadamicYear] CHECK CONSTRAINT [FK_tblAccadamicYear_tblUser1]
GO
ALTER TABLE [dbo].[tblApplicationStatus]  WITH CHECK ADD  CONSTRAINT [FK_tblApplicationStatus_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblApplicationStatus] CHECK CONSTRAINT [FK_tblApplicationStatus_tblUser]
GO
ALTER TABLE [dbo].[tblApplicationStatus]  WITH CHECK ADD  CONSTRAINT [FK_tblApplicationStatus_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblApplicationStatus] CHECK CONSTRAINT [FK_tblApplicationStatus_tblUser1]
GO
ALTER TABLE [dbo].[tblAssignmentDueDate]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentDueDate_tblAssignmentHeader] FOREIGN KEY([AssignmentId], [SchoolId])
REFERENCES [dbo].[tblAssignmentHeader] ([AssignmentNo], [SchoolId])
GO
ALTER TABLE [dbo].[tblAssignmentDueDate] CHECK CONSTRAINT [FK_tblAssignmentDueDate_tblAssignmentHeader]
GO
ALTER TABLE [dbo].[tblAssignmentDueDate]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentDueDate_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblAssignmentDueDate] CHECK CONSTRAINT [FK_tblAssignmentDueDate_tblSchool]
GO
ALTER TABLE [dbo].[tblAssignmentDueDate]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentDueDate_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblAssignmentDueDate] CHECK CONSTRAINT [FK_tblAssignmentDueDate_tblUser]
GO
ALTER TABLE [dbo].[tblAssignmentDueDate]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentDueDate_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblAssignmentDueDate] CHECK CONSTRAINT [FK_tblAssignmentDueDate_tblUser1]
GO
ALTER TABLE [dbo].[tblAssignmentHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentHeader_tblAssignmentHeader] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[tblTeacher] ([TeacherId])
GO
ALTER TABLE [dbo].[tblAssignmentHeader] CHECK CONSTRAINT [FK_tblAssignmentHeader_tblAssignmentHeader]
GO
ALTER TABLE [dbo].[tblAssignmentHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentHeader_tblClass] FOREIGN KEY([ClassId], [GradeId], [SchoolId])
REFERENCES [dbo].[tblClass] ([ClassId], [GradeId], [SchoolId])
GO
ALTER TABLE [dbo].[tblAssignmentHeader] CHECK CONSTRAINT [FK_tblAssignmentHeader_tblClass]
GO
ALTER TABLE [dbo].[tblAssignmentHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentHeader_tblGrade] FOREIGN KEY([GradeId])
REFERENCES [dbo].[tblGrade] ([GradeId])
GO
ALTER TABLE [dbo].[tblAssignmentHeader] CHECK CONSTRAINT [FK_tblAssignmentHeader_tblGrade]
GO
ALTER TABLE [dbo].[tblAssignmentHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentHeader_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblAssignmentHeader] CHECK CONSTRAINT [FK_tblAssignmentHeader_tblSchool]
GO
ALTER TABLE [dbo].[tblAssignmentHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentHeader_tblStudent] FOREIGN KEY([SchoolId], [StudentId])
REFERENCES [dbo].[tblStudent] ([SchoolId], [StudentId])
GO
ALTER TABLE [dbo].[tblAssignmentHeader] CHECK CONSTRAINT [FK_tblAssignmentHeader_tblStudent]
GO
ALTER TABLE [dbo].[tblAssignmentHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentHeader_tblSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
GO
ALTER TABLE [dbo].[tblAssignmentHeader] CHECK CONSTRAINT [FK_tblAssignmentHeader_tblSubject]
GO
ALTER TABLE [dbo].[tblAssignmentHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentHeader_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblAssignmentHeader] CHECK CONSTRAINT [FK_tblAssignmentHeader_tblUser]
GO
ALTER TABLE [dbo].[tblAssignmentHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentHeader_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblAssignmentHeader] CHECK CONSTRAINT [FK_tblAssignmentHeader_tblUser1]
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAid]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentQuestionAid_tblAssignmentHeader] FOREIGN KEY([AssignmentId], [SchoolId])
REFERENCES [dbo].[tblAssignmentHeader] ([AssignmentNo], [SchoolId])
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAid] CHECK CONSTRAINT [FK_tblAssignmentQuestionAid_tblAssignmentHeader]
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAid]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentQuestionAid_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAid] CHECK CONSTRAINT [FK_tblAssignmentQuestionAid_tblSchool]
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAid]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentQuestionAid_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAid] CHECK CONSTRAINT [FK_tblAssignmentQuestionAid_tblUser]
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAid]  WITH CHECK ADD  CONSTRAINT [FK_tblAssignmentQuestionAid_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAid] CHECK CONSTRAINT [FK_tblAssignmentQuestionAid_tblUser1]
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestionAnswer_tblAssignmentHeader] FOREIGN KEY([AssignmentId], [SchoolId])
REFERENCES [dbo].[tblAssignmentHeader] ([AssignmentNo], [SchoolId])
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAnswer] CHECK CONSTRAINT [FK_tblQuestionAnswer_tblAssignmentHeader]
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestionAnswer_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAnswer] CHECK CONSTRAINT [FK_tblQuestionAnswer_tblSchool]
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestionAnswer_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAnswer] CHECK CONSTRAINT [FK_tblQuestionAnswer_tblUser]
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAnswer]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestionAnswer_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblAssignmentQuestionAnswer] CHECK CONSTRAINT [FK_tblQuestionAnswer_tblUser1]
GO
ALTER TABLE [dbo].[tblAssignmentQuestionType]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestionType_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblAssignmentQuestionType] CHECK CONSTRAINT [FK_tblQuestionType_tblUser]
GO
ALTER TABLE [dbo].[tblAssignmentQuestionType]  WITH CHECK ADD  CONSTRAINT [FK_tblQuestionType_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblAssignmentQuestionType] CHECK CONSTRAINT [FK_tblQuestionType_tblUser1]
GO
ALTER TABLE [dbo].[tblAssingmentQuestion]  WITH CHECK ADD  CONSTRAINT [FK_tblAssingmentQuestion_tblAssignmentQuestionType] FOREIGN KEY([QuestionType])
REFERENCES [dbo].[tblAssignmentQuestionType] ([QuestionTypeId])
GO
ALTER TABLE [dbo].[tblAssingmentQuestion] CHECK CONSTRAINT [FK_tblAssingmentQuestion_tblAssignmentQuestionType]
GO
ALTER TABLE [dbo].[tblAssingmentQuestion]  WITH CHECK ADD  CONSTRAINT [FK_tblAssingmentQuestion_tblParentObservationType] FOREIGN KEY([ParentObservationType])
REFERENCES [dbo].[tblParentObservationType] ([ObTypeId])
GO
ALTER TABLE [dbo].[tblAssingmentQuestion] CHECK CONSTRAINT [FK_tblAssingmentQuestion_tblParentObservationType]
GO
ALTER TABLE [dbo].[tblAssingmentQuestion]  WITH CHECK ADD  CONSTRAINT [FK_tblAssingmentQuestion_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblAssingmentQuestion] CHECK CONSTRAINT [FK_tblAssingmentQuestion_tblSchool]
GO
ALTER TABLE [dbo].[tblAssingmentQuestion]  WITH CHECK ADD  CONSTRAINT [FK_tblAssingmentQuestion_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblAssingmentQuestion] CHECK CONSTRAINT [FK_tblAssingmentQuestion_tblUser]
GO
ALTER TABLE [dbo].[tblAssingmentQuestion]  WITH CHECK ADD  CONSTRAINT [FK_tblAssingmentQuestion_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblAssingmentQuestion] CHECK CONSTRAINT [FK_tblAssingmentQuestion_tblUser1]
GO
ALTER TABLE [dbo].[tblClass]  WITH CHECK ADD  CONSTRAINT [FK_tblClass_tblGrade] FOREIGN KEY([GradeId])
REFERENCES [dbo].[tblGrade] ([GradeId])
GO
ALTER TABLE [dbo].[tblClass] CHECK CONSTRAINT [FK_tblClass_tblGrade]
GO
ALTER TABLE [dbo].[tblClass]  WITH CHECK ADD  CONSTRAINT [FK_tblClass_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblClass] CHECK CONSTRAINT [FK_tblClass_tblSchool]
GO
ALTER TABLE [dbo].[tblClass]  WITH CHECK ADD  CONSTRAINT [FK_tblClass_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblClass] CHECK CONSTRAINT [FK_tblClass_tblUser]
GO
ALTER TABLE [dbo].[tblClass]  WITH CHECK ADD  CONSTRAINT [FK_tblClass_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblClass] CHECK CONSTRAINT [FK_tblClass_tblUser1]
GO
ALTER TABLE [dbo].[tblClassTeacher]  WITH CHECK ADD  CONSTRAINT [FK_tblClassTeacher_tblClass] FOREIGN KEY([ClassId], [GradeId], [SchoolId])
REFERENCES [dbo].[tblClass] ([ClassId], [GradeId], [SchoolId])
GO
ALTER TABLE [dbo].[tblClassTeacher] CHECK CONSTRAINT [FK_tblClassTeacher_tblClass]
GO
ALTER TABLE [dbo].[tblClassTeacher]  WITH CHECK ADD  CONSTRAINT [FK_tblClassTeacher_tblGrade] FOREIGN KEY([GradeId])
REFERENCES [dbo].[tblGrade] ([GradeId])
GO
ALTER TABLE [dbo].[tblClassTeacher] CHECK CONSTRAINT [FK_tblClassTeacher_tblGrade]
GO
ALTER TABLE [dbo].[tblClassTeacher]  WITH CHECK ADD  CONSTRAINT [FK_tblClassTeacher_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblClassTeacher] CHECK CONSTRAINT [FK_tblClassTeacher_tblSchool]
GO
ALTER TABLE [dbo].[tblClassTeacher]  WITH CHECK ADD  CONSTRAINT [FK_tblClassTeacher_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblClassTeacher] CHECK CONSTRAINT [FK_tblClassTeacher_tblUser]
GO
ALTER TABLE [dbo].[tblClassTeacher]  WITH CHECK ADD  CONSTRAINT [FK_tblClassTeacher_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblClassTeacher] CHECK CONSTRAINT [FK_tblClassTeacher_tblUser1]
GO
ALTER TABLE [dbo].[tblDistrict]  WITH CHECK ADD  CONSTRAINT [FK_tblDistrict_tblProvince] FOREIGN KEY([ProvinceId])
REFERENCES [dbo].[tblProvince] ([ProvinceId])
GO
ALTER TABLE [dbo].[tblDistrict] CHECK CONSTRAINT [FK_tblDistrict_tblProvince]
GO
ALTER TABLE [dbo].[tblDistrict]  WITH CHECK ADD  CONSTRAINT [FK_tblDistrict_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblDistrict] CHECK CONSTRAINT [FK_tblDistrict_tblUser]
GO
ALTER TABLE [dbo].[tblDistrict]  WITH CHECK ADD  CONSTRAINT [FK_tblDistrict_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblDistrict] CHECK CONSTRAINT [FK_tblDistrict_tblUser1]
GO
ALTER TABLE [dbo].[tblDivision]  WITH CHECK ADD  CONSTRAINT [FK_tblDivision_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblDivision] CHECK CONSTRAINT [FK_tblDivision_tblUser]
GO
ALTER TABLE [dbo].[tblDivision]  WITH CHECK ADD  CONSTRAINT [FK_tblDivision_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblDivision] CHECK CONSTRAINT [FK_tblDivision_tblUser1]
GO
ALTER TABLE [dbo].[tblEntranceApplication]  WITH CHECK ADD  CONSTRAINT [FK_tblEntranceApplication_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblEntranceApplication] CHECK CONSTRAINT [FK_tblEntranceApplication_tblSchool]
GO
ALTER TABLE [dbo].[tblEntranceApplication]  WITH CHECK ADD  CONSTRAINT [FK_tblEntranceApplication_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEntranceApplication] CHECK CONSTRAINT [FK_tblEntranceApplication_tblUser]
GO
ALTER TABLE [dbo].[tblEntranceApplication]  WITH CHECK ADD  CONSTRAINT [FK_tblEntranceApplication_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEntranceApplication] CHECK CONSTRAINT [FK_tblEntranceApplication_tblUser1]
GO
ALTER TABLE [dbo].[tblEvaluationDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationDetail_tblClass] FOREIGN KEY([Class], [Grade], [SchoolId])
REFERENCES [dbo].[tblClass] ([ClassId], [GradeId], [SchoolId])
GO
ALTER TABLE [dbo].[tblEvaluationDetail] CHECK CONSTRAINT [FK_tblEvaluationDetail_tblClass]
GO
ALTER TABLE [dbo].[tblEvaluationDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationDetail_tblEvaluationHeader] FOREIGN KEY([EvaluationNo], [SchoolId])
REFERENCES [dbo].[tblEvaluationHeader] ([EvaluationNo], [SchoolId])
GO
ALTER TABLE [dbo].[tblEvaluationDetail] CHECK CONSTRAINT [FK_tblEvaluationDetail_tblEvaluationHeader]
GO
ALTER TABLE [dbo].[tblEvaluationDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationDetail_tblGrade] FOREIGN KEY([Grade])
REFERENCES [dbo].[tblGrade] ([GradeId])
GO
ALTER TABLE [dbo].[tblEvaluationDetail] CHECK CONSTRAINT [FK_tblEvaluationDetail_tblGrade]
GO
ALTER TABLE [dbo].[tblEvaluationDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationDetail_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblEvaluationDetail] CHECK CONSTRAINT [FK_tblEvaluationDetail_tblSchool]
GO
ALTER TABLE [dbo].[tblEvaluationDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationDetail_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEvaluationDetail] CHECK CONSTRAINT [FK_tblEvaluationDetail_tblUser]
GO
ALTER TABLE [dbo].[tblEvaluationDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationDetail_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEvaluationDetail] CHECK CONSTRAINT [FK_tblEvaluationDetail_tblUser1]
GO
ALTER TABLE [dbo].[tblEvaluationHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationHeader_tblEvaluationType] FOREIGN KEY([EvaluationType])
REFERENCES [dbo].[tblEvaluationType] ([EvaluationTypeCode])
GO
ALTER TABLE [dbo].[tblEvaluationHeader] CHECK CONSTRAINT [FK_tblEvaluationHeader_tblEvaluationType]
GO
ALTER TABLE [dbo].[tblEvaluationHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationHeader_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblEvaluationHeader] CHECK CONSTRAINT [FK_tblEvaluationHeader_tblSchool]
GO
ALTER TABLE [dbo].[tblEvaluationHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationHeader_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEvaluationHeader] CHECK CONSTRAINT [FK_tblEvaluationHeader_tblUser]
GO
ALTER TABLE [dbo].[tblEvaluationHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationHeader_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEvaluationHeader] CHECK CONSTRAINT [FK_tblEvaluationHeader_tblUser1]
GO
ALTER TABLE [dbo].[tblEvaluationResult]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationResult_tblEvaluationDetail] FOREIGN KEY([EvaluationDetailSeqNo], [SchoolId])
REFERENCES [dbo].[tblEvaluationDetail] ([EvaluationDetailSeqNo], [SchoolId])
GO
ALTER TABLE [dbo].[tblEvaluationResult] CHECK CONSTRAINT [FK_tblEvaluationResult_tblEvaluationDetail]
GO
ALTER TABLE [dbo].[tblEvaluationResult]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationResult_tblGrade] FOREIGN KEY([Grade])
REFERENCES [dbo].[tblGrade] ([GradeId])
GO
ALTER TABLE [dbo].[tblEvaluationResult] CHECK CONSTRAINT [FK_tblEvaluationResult_tblGrade]
GO
ALTER TABLE [dbo].[tblEvaluationResult]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationResult_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblEvaluationResult] CHECK CONSTRAINT [FK_tblEvaluationResult_tblSchool]
GO
ALTER TABLE [dbo].[tblEvaluationResult]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationResult_tblStudent] FOREIGN KEY([SchoolId], [StudentId])
REFERENCES [dbo].[tblStudent] ([SchoolId], [StudentId])
GO
ALTER TABLE [dbo].[tblEvaluationResult] CHECK CONSTRAINT [FK_tblEvaluationResult_tblStudent]
GO
ALTER TABLE [dbo].[tblEvaluationResult]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationResult_tblSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
GO
ALTER TABLE [dbo].[tblEvaluationResult] CHECK CONSTRAINT [FK_tblEvaluationResult_tblSubject]
GO
ALTER TABLE [dbo].[tblEvaluationResult]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationResult_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEvaluationResult] CHECK CONSTRAINT [FK_tblEvaluationResult_tblUser]
GO
ALTER TABLE [dbo].[tblEvaluationResult]  WITH CHECK ADD  CONSTRAINT [FK_tblEvaluationResult_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEvaluationResult] CHECK CONSTRAINT [FK_tblEvaluationResult_tblUser1]
GO
ALTER TABLE [dbo].[tblEvaluationType]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentEvaluationType_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEvaluationType] CHECK CONSTRAINT [FK_tblStudentEvaluationType_tblUser]
GO
ALTER TABLE [dbo].[tblEvaluationType]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentEvaluationType_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEvaluationType] CHECK CONSTRAINT [FK_tblStudentEvaluationType_tblUser1]
GO
ALTER TABLE [dbo].[tblEventCalendar]  WITH CHECK ADD  CONSTRAINT [FK_tblEventCalendar_tblEventcategory] FOREIGN KEY([EventCategory])
REFERENCES [dbo].[tblEventcategory] ([EventCategoryId])
GO
ALTER TABLE [dbo].[tblEventCalendar] CHECK CONSTRAINT [FK_tblEventCalendar_tblEventcategory]
GO
ALTER TABLE [dbo].[tblEventCalendar]  WITH CHECK ADD  CONSTRAINT [FK_tblEventCalendar_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblEventCalendar] CHECK CONSTRAINT [FK_tblEventCalendar_tblSchool]
GO
ALTER TABLE [dbo].[tblEventCalendar]  WITH CHECK ADD  CONSTRAINT [FK_tblEventCalendar_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEventCalendar] CHECK CONSTRAINT [FK_tblEventCalendar_tblUser]
GO
ALTER TABLE [dbo].[tblEventCalendar]  WITH CHECK ADD  CONSTRAINT [FK_tblEventCalendar_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEventCalendar] CHECK CONSTRAINT [FK_tblEventCalendar_tblUser1]
GO
ALTER TABLE [dbo].[tblEventcategory]  WITH CHECK ADD  CONSTRAINT [FK_tblEventcategory_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEventcategory] CHECK CONSTRAINT [FK_tblEventcategory_tblUser]
GO
ALTER TABLE [dbo].[tblEventcategory]  WITH CHECK ADD  CONSTRAINT [FK_tblEventcategory_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEventcategory] CHECK CONSTRAINT [FK_tblEventcategory_tblUser1]
GO
ALTER TABLE [dbo].[tblEventParticipant]  WITH CHECK ADD  CONSTRAINT [FK_tblEventParticipant_tblEventCalendar] FOREIGN KEY([EventNo])
REFERENCES [dbo].[tblEventCalendar] ([EventNo])
GO
ALTER TABLE [dbo].[tblEventParticipant] CHECK CONSTRAINT [FK_tblEventParticipant_tblEventCalendar]
GO
ALTER TABLE [dbo].[tblEventParticipant]  WITH CHECK ADD  CONSTRAINT [FK_tblEventParticipant_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEventParticipant] CHECK CONSTRAINT [FK_tblEventParticipant_tblUser]
GO
ALTER TABLE [dbo].[tblEventParticipant]  WITH CHECK ADD  CONSTRAINT [FK_tblEventParticipant_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEventParticipant] CHECK CONSTRAINT [FK_tblEventParticipant_tblUser1]
GO
ALTER TABLE [dbo].[tblEventParticipant]  WITH CHECK ADD  CONSTRAINT [FK_tblEventParticipant_tblUser2] FOREIGN KEY([ParticipantUserId])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblEventParticipant] CHECK CONSTRAINT [FK_tblEventParticipant_tblUser2]
GO
ALTER TABLE [dbo].[tblExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblExtraCurricularActivity_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblExtraCurricularActivity] CHECK CONSTRAINT [FK_tblExtraCurricularActivity_tblUser]
GO
ALTER TABLE [dbo].[tblExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblExtraCurricularActivity_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblExtraCurricularActivity] CHECK CONSTRAINT [FK_tblExtraCurricularActivity_tblUser1]
GO
ALTER TABLE [dbo].[tblFunction]  WITH CHECK ADD  CONSTRAINT [FK_tblFunction_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblFunction] CHECK CONSTRAINT [FK_tblFunction_tblUser]
GO
ALTER TABLE [dbo].[tblFunction]  WITH CHECK ADD  CONSTRAINT [FK_tblFunction_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblFunction] CHECK CONSTRAINT [FK_tblFunction_tblUser1]
GO
ALTER TABLE [dbo].[tblGrade]  WITH CHECK ADD  CONSTRAINT [FK_tblGrade_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblGrade] CHECK CONSTRAINT [FK_tblGrade_tblUser]
GO
ALTER TABLE [dbo].[tblGrade]  WITH CHECK ADD  CONSTRAINT [FK_tblGrade_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblGrade] CHECK CONSTRAINT [FK_tblGrade_tblUser1]
GO
ALTER TABLE [dbo].[tblGradeSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblGradeSubject_tblGrade] FOREIGN KEY([SubjectCategoryId])
REFERENCES [dbo].[tblSubjectCategory] ([SubjectCategoryId])
GO
ALTER TABLE [dbo].[tblGradeSubject] CHECK CONSTRAINT [FK_tblGradeSubject_tblGrade]
GO
ALTER TABLE [dbo].[tblGradeSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblGradeSubject_tblSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
GO
ALTER TABLE [dbo].[tblGradeSubject] CHECK CONSTRAINT [FK_tblGradeSubject_tblSubject]
GO
ALTER TABLE [dbo].[tblGradeSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblGradeSubject_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblGradeSubject] CHECK CONSTRAINT [FK_tblGradeSubject_tblUser]
GO
ALTER TABLE [dbo].[tblGradeSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblGradeSubject_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblGradeSubject] CHECK CONSTRAINT [FK_tblGradeSubject_tblUser1]
GO
ALTER TABLE [dbo].[tblHouse]  WITH CHECK ADD  CONSTRAINT [FK_tblHouse_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblHouse] CHECK CONSTRAINT [FK_tblHouse_tblSchool]
GO
ALTER TABLE [dbo].[tblHouse]  WITH CHECK ADD  CONSTRAINT [FK_tblHouse_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblHouse] CHECK CONSTRAINT [FK_tblHouse_tblUser]
GO
ALTER TABLE [dbo].[tblHouse]  WITH CHECK ADD  CONSTRAINT [FK_tblHouse_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblHouse] CHECK CONSTRAINT [FK_tblHouse_tblUser1]
GO
ALTER TABLE [dbo].[tblMessageType]  WITH CHECK ADD  CONSTRAINT [FK_tblMessageType_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblMessageType] CHECK CONSTRAINT [FK_tblMessageType_tblUser]
GO
ALTER TABLE [dbo].[tblMessageType]  WITH CHECK ADD  CONSTRAINT [FK_tblMessageType_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblMessageType] CHECK CONSTRAINT [FK_tblMessageType_tblUser1]
GO
ALTER TABLE [dbo].[tblParent]  WITH CHECK ADD  CONSTRAINT [FK_tblParent_tblRelashionship] FOREIGN KEY([RelationshipId])
REFERENCES [dbo].[tblRelashionship] ([RelashionshipId])
GO
ALTER TABLE [dbo].[tblParent] CHECK CONSTRAINT [FK_tblParent_tblRelashionship]
GO
ALTER TABLE [dbo].[tblParent]  WITH CHECK ADD  CONSTRAINT [FK_tblParent_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblParent] CHECK CONSTRAINT [FK_tblParent_tblUser]
GO
ALTER TABLE [dbo].[tblParent]  WITH CHECK ADD  CONSTRAINT [FK_tblParent_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblParent] CHECK CONSTRAINT [FK_tblParent_tblUser1]
GO
ALTER TABLE [dbo].[tblParentObservationType]  WITH CHECK ADD  CONSTRAINT [FK_tblParentObservationType_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblParentObservationType] CHECK CONSTRAINT [FK_tblParentObservationType_tblUser]
GO
ALTER TABLE [dbo].[tblParentObservationType]  WITH CHECK ADD  CONSTRAINT [FK_tblParentObservationType_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblParentObservationType] CHECK CONSTRAINT [FK_tblParentObservationType_tblUser1]
GO
ALTER TABLE [dbo].[tblParentStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblParentStudent_tblParent] FOREIGN KEY([ParentId])
REFERENCES [dbo].[tblParent] ([ParentId])
GO
ALTER TABLE [dbo].[tblParentStudent] CHECK CONSTRAINT [FK_tblParentStudent_tblParent]
GO
ALTER TABLE [dbo].[tblParentStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblParentStudent_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblParentStudent] CHECK CONSTRAINT [FK_tblParentStudent_tblSchool]
GO
ALTER TABLE [dbo].[tblParentStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblParentStudent_tblStudent] FOREIGN KEY([SchoolId], [StudentId])
REFERENCES [dbo].[tblStudent] ([SchoolId], [StudentId])
GO
ALTER TABLE [dbo].[tblParentStudent] CHECK CONSTRAINT [FK_tblParentStudent_tblStudent]
GO
ALTER TABLE [dbo].[tblParentStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblParentStudent_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblParentStudent] CHECK CONSTRAINT [FK_tblParentStudent_tblUser]
GO
ALTER TABLE [dbo].[tblParentStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblParentStudent_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblParentStudent] CHECK CONSTRAINT [FK_tblParentStudent_tblUser1]
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
ALTER TABLE [dbo].[tblProvince]  WITH CHECK ADD  CONSTRAINT [FK_tblProvince_tblProvince] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblProvince] CHECK CONSTRAINT [FK_tblProvince_tblProvince]
GO
ALTER TABLE [dbo].[tblProvince]  WITH CHECK ADD  CONSTRAINT [FK_tblProvince_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblProvince] CHECK CONSTRAINT [FK_tblProvince_tblUser]
GO
ALTER TABLE [dbo].[tblQualification]  WITH CHECK ADD  CONSTRAINT [FK_tblQualification_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblQualification] CHECK CONSTRAINT [FK_tblQualification_tblUser]
GO
ALTER TABLE [dbo].[tblQualification]  WITH CHECK ADD  CONSTRAINT [FK_tblQualification_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblQualification] CHECK CONSTRAINT [FK_tblQualification_tblUser1]
GO
ALTER TABLE [dbo].[tblRelashionship]  WITH CHECK ADD  CONSTRAINT [FK_tblRelashionship_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblRelashionship] CHECK CONSTRAINT [FK_tblRelashionship_tblUser]
GO
ALTER TABLE [dbo].[tblRelashionship]  WITH CHECK ADD  CONSTRAINT [FK_tblRelashionship_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblRelashionship] CHECK CONSTRAINT [FK_tblRelashionship_tblUser1]
GO
ALTER TABLE [dbo].[tblSchool]  WITH CHECK ADD  CONSTRAINT [FK_tblSchool_tblDistrict] FOREIGN KEY([District])
REFERENCES [dbo].[tblDistrict] ([DistrictId])
GO
ALTER TABLE [dbo].[tblSchool] CHECK CONSTRAINT [FK_tblSchool_tblDistrict]
GO
ALTER TABLE [dbo].[tblSchool]  WITH CHECK ADD  CONSTRAINT [FK_tblSchool_tblDivision] FOREIGN KEY([Division])
REFERENCES [dbo].[tblDivision] ([DivisionId])
GO
ALTER TABLE [dbo].[tblSchool] CHECK CONSTRAINT [FK_tblSchool_tblDivision]
GO
ALTER TABLE [dbo].[tblSchool]  WITH CHECK ADD  CONSTRAINT [FK_tblSchool_tblProvince] FOREIGN KEY([Province])
REFERENCES [dbo].[tblProvince] ([ProvinceId])
GO
ALTER TABLE [dbo].[tblSchool] CHECK CONSTRAINT [FK_tblSchool_tblProvince]
GO
ALTER TABLE [dbo].[tblSchool]  WITH CHECK ADD  CONSTRAINT [FK_tblSchool_tblSchoolCategory] FOREIGN KEY([SchoolCategory])
REFERENCES [dbo].[tblSchoolCategory] ([SchoolCategoryId])
GO
ALTER TABLE [dbo].[tblSchool] CHECK CONSTRAINT [FK_tblSchool_tblSchoolCategory]
GO
ALTER TABLE [dbo].[tblSchool]  WITH CHECK ADD  CONSTRAINT [FK_tblSchool_tblSchoolGroup] FOREIGN KEY([SchoolGroup])
REFERENCES [dbo].[tblSchoolGroup] ([GroupId])
GO
ALTER TABLE [dbo].[tblSchool] CHECK CONSTRAINT [FK_tblSchool_tblSchoolGroup]
GO
ALTER TABLE [dbo].[tblSchool]  WITH CHECK ADD  CONSTRAINT [FK_tblSchool_tblSchoolRank] FOREIGN KEY([SchoolRank])
REFERENCES [dbo].[tblSchoolRank] ([SchoolRankId])
GO
ALTER TABLE [dbo].[tblSchool] CHECK CONSTRAINT [FK_tblSchool_tblSchoolRank]
GO
ALTER TABLE [dbo].[tblSchool]  WITH CHECK ADD  CONSTRAINT [FK_tblSchool_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchool] CHECK CONSTRAINT [FK_tblSchool_tblUser]
GO
ALTER TABLE [dbo].[tblSchool]  WITH CHECK ADD  CONSTRAINT [FK_tblSchool_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchool] CHECK CONSTRAINT [FK_tblSchool_tblUser1]
GO
ALTER TABLE [dbo].[tblSchoolCalendar]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolCalendar_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblSchoolCalendar] CHECK CONSTRAINT [FK_tblSchoolCalendar_tblSchool]
GO
ALTER TABLE [dbo].[tblSchoolCalendar]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolCalendar_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolCalendar] CHECK CONSTRAINT [FK_tblSchoolCalendar_tblUser]
GO
ALTER TABLE [dbo].[tblSchoolCalendar]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolCalendar_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolCalendar] CHECK CONSTRAINT [FK_tblSchoolCalendar_tblUser1]
GO
ALTER TABLE [dbo].[tblSchoolCategory]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolCategory_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolCategory] CHECK CONSTRAINT [FK_tblSchoolCategory_tblUser]
GO
ALTER TABLE [dbo].[tblSchoolCategory]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolCategory_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolCategory] CHECK CONSTRAINT [FK_tblSchoolCategory_tblUser1]
GO
ALTER TABLE [dbo].[tblSchoolExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolExtraCurricularActivity_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblSchoolExtraCurricularActivity] CHECK CONSTRAINT [FK_tblSchoolExtraCurricularActivity_tblSchool]
GO
ALTER TABLE [dbo].[tblSchoolExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolExtraCurricularActivity_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolExtraCurricularActivity] CHECK CONSTRAINT [FK_tblSchoolExtraCurricularActivity_tblUser]
GO
ALTER TABLE [dbo].[tblSchoolExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolExtraCurricularActivity_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolExtraCurricularActivity] CHECK CONSTRAINT [FK_tblSchoolExtraCurricularActivity_tblUser1]
GO
ALTER TABLE [dbo].[tblSchoolGrade]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolGrade_tblGrade] FOREIGN KEY([GradeId])
REFERENCES [dbo].[tblGrade] ([GradeId])
GO
ALTER TABLE [dbo].[tblSchoolGrade] CHECK CONSTRAINT [FK_tblSchoolGrade_tblGrade]
GO
ALTER TABLE [dbo].[tblSchoolGrade]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolGrade_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblSchoolGrade] CHECK CONSTRAINT [FK_tblSchoolGrade_tblSchool]
GO
ALTER TABLE [dbo].[tblSchoolGrade]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolGrade_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolGrade] CHECK CONSTRAINT [FK_tblSchoolGrade_tblUser]
GO
ALTER TABLE [dbo].[tblSchoolGrade]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolGrade_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolGrade] CHECK CONSTRAINT [FK_tblSchoolGrade_tblUser1]
GO
ALTER TABLE [dbo].[tblSchoolGroup]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolGroup_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolGroup] CHECK CONSTRAINT [FK_tblSchoolGroup_tblUser]
GO
ALTER TABLE [dbo].[tblSchoolGroup]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolGroup_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolGroup] CHECK CONSTRAINT [FK_tblSchoolGroup_tblUser1]
GO
ALTER TABLE [dbo].[tblSchoolRank]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolRank_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolRank] CHECK CONSTRAINT [FK_tblSchoolRank_tblUser]
GO
ALTER TABLE [dbo].[tblSchoolRank]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolRank_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolRank] CHECK CONSTRAINT [FK_tblSchoolRank_tblUser1]
GO
ALTER TABLE [dbo].[tblSchoolSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolSubject_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblSchoolSubject] CHECK CONSTRAINT [FK_tblSchoolSubject_tblSchool]
GO
ALTER TABLE [dbo].[tblSchoolSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolSubject_tblSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
GO
ALTER TABLE [dbo].[tblSchoolSubject] CHECK CONSTRAINT [FK_tblSchoolSubject_tblSubject]
GO
ALTER TABLE [dbo].[tblSchoolSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolSubject_tblSubjectCategory] FOREIGN KEY([SubjectCategoryId])
REFERENCES [dbo].[tblSubjectCategory] ([SubjectCategoryId])
GO
ALTER TABLE [dbo].[tblSchoolSubject] CHECK CONSTRAINT [FK_tblSchoolSubject_tblSubjectCategory]
GO
ALTER TABLE [dbo].[tblSchoolSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolSubject_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolSubject] CHECK CONSTRAINT [FK_tblSchoolSubject_tblUser]
GO
ALTER TABLE [dbo].[tblSchoolSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolSubject_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolSubject] CHECK CONSTRAINT [FK_tblSchoolSubject_tblUser1]
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolToParentMessageDetail_tblParent] FOREIGN KEY([ParentId])
REFERENCES [dbo].[tblParent] ([ParentId])
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageDetail] CHECK CONSTRAINT [FK_tblSchoolToParentMessageDetail_tblParent]
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolToParentMessageDetail_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageDetail] CHECK CONSTRAINT [FK_tblSchoolToParentMessageDetail_tblSchool]
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolToParentMessageDetail_tblSchoolToParentMessageHeader] FOREIGN KEY([SchoolId], [MessageId])
REFERENCES [dbo].[tblSchoolToParentMessageHeader] ([SchoolId], [MessageId])
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageDetail] CHECK CONSTRAINT [FK_tblSchoolToParentMessageDetail_tblSchoolToParentMessageHeader]
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolToParentMessageDetail_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageDetail] CHECK CONSTRAINT [FK_tblSchoolToParentMessageDetail_tblUser]
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageDetail]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolToParentMessageDetail_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageDetail] CHECK CONSTRAINT [FK_tblSchoolToParentMessageDetail_tblUser1]
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolToParentMessageHeader_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageHeader] CHECK CONSTRAINT [FK_tblSchoolToParentMessageHeader_tblSchool]
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolToParentMessageHeader_tblSchoolToParentMessageHeader] FOREIGN KEY([MessageType])
REFERENCES [dbo].[tblMessageType] ([MessageTypeId])
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageHeader] CHECK CONSTRAINT [FK_tblSchoolToParentMessageHeader_tblSchoolToParentMessageHeader]
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolToParentMessageHeader_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageHeader] CHECK CONSTRAINT [FK_tblSchoolToParentMessageHeader_tblUser]
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblSchoolToParentMessageHeader_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSchoolToParentMessageHeader] CHECK CONSTRAINT [FK_tblSchoolToParentMessageHeader_tblUser1]
GO
ALTER TABLE [dbo].[tblStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblStudent_tblClass] FOREIGN KEY([ClassId], [GradeId], [SchoolId])
REFERENCES [dbo].[tblClass] ([ClassId], [GradeId], [SchoolId])
GO
ALTER TABLE [dbo].[tblStudent] CHECK CONSTRAINT [FK_tblStudent_tblClass]
GO
ALTER TABLE [dbo].[tblStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblStudent_tblGrade] FOREIGN KEY([GradeId])
REFERENCES [dbo].[tblGrade] ([GradeId])
GO
ALTER TABLE [dbo].[tblStudent] CHECK CONSTRAINT [FK_tblStudent_tblGrade]
GO
ALTER TABLE [dbo].[tblStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblStudent_tblHouse] FOREIGN KEY([SchoolId], [HouseId])
REFERENCES [dbo].[tblHouse] ([SchoolId], [HouseId])
GO
ALTER TABLE [dbo].[tblStudent] CHECK CONSTRAINT [FK_tblStudent_tblHouse]
GO
ALTER TABLE [dbo].[tblStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblStudent_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblStudent] CHECK CONSTRAINT [FK_tblStudent_tblSchool]
GO
ALTER TABLE [dbo].[tblStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblStudent_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblStudent] CHECK CONSTRAINT [FK_tblStudent_tblUser]
GO
ALTER TABLE [dbo].[tblStudent]  WITH CHECK ADD  CONSTRAINT [FK_tblStudent_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblStudent] CHECK CONSTRAINT [FK_tblStudent_tblUser1]
GO
ALTER TABLE [dbo].[tblStudentExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentExtraCurricularActivity_tblExtraCurricularActivity] FOREIGN KEY([ActivityCode])
REFERENCES [dbo].[tblExtraCurricularActivity] ([ActivityCode])
GO
ALTER TABLE [dbo].[tblStudentExtraCurricularActivity] CHECK CONSTRAINT [FK_tblStudentExtraCurricularActivity_tblExtraCurricularActivity]
GO
ALTER TABLE [dbo].[tblStudentExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentExtraCurricularActivity_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblStudentExtraCurricularActivity] CHECK CONSTRAINT [FK_tblStudentExtraCurricularActivity_tblSchool]
GO
ALTER TABLE [dbo].[tblStudentExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentExtraCurricularActivity_tblStudent] FOREIGN KEY([SchoolId], [StudentId])
REFERENCES [dbo].[tblStudent] ([SchoolId], [StudentId])
GO
ALTER TABLE [dbo].[tblStudentExtraCurricularActivity] CHECK CONSTRAINT [FK_tblStudentExtraCurricularActivity_tblStudent]
GO
ALTER TABLE [dbo].[tblStudentExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentExtraCurricularActivity_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblStudentExtraCurricularActivity] CHECK CONSTRAINT [FK_tblStudentExtraCurricularActivity_tblUser]
GO
ALTER TABLE [dbo].[tblStudentExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentExtraCurricularActivity_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblStudentExtraCurricularActivity] CHECK CONSTRAINT [FK_tblStudentExtraCurricularActivity_tblUser1]
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentOptionalSubject_tblClass] FOREIGN KEY([ClassId], [GradeId], [SchoolId])
REFERENCES [dbo].[tblClass] ([ClassId], [GradeId], [SchoolId])
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject] CHECK CONSTRAINT [FK_tblStudentOptionalSubject_tblClass]
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentOptionalSubject_tblGrade] FOREIGN KEY([GradeId])
REFERENCES [dbo].[tblGrade] ([GradeId])
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject] CHECK CONSTRAINT [FK_tblStudentOptionalSubject_tblGrade]
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentOptionalSubject_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject] CHECK CONSTRAINT [FK_tblStudentOptionalSubject_tblSchool]
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentOptionalSubject_tblStudent] FOREIGN KEY([SchoolId], [StudentId])
REFERENCES [dbo].[tblStudent] ([SchoolId], [StudentId])
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject] CHECK CONSTRAINT [FK_tblStudentOptionalSubject_tblStudent]
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentOptionalSubject_tblSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject] CHECK CONSTRAINT [FK_tblStudentOptionalSubject_tblSubject]
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentOptionalSubject_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject] CHECK CONSTRAINT [FK_tblStudentOptionalSubject_tblUser]
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblStudentOptionalSubject_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblStudentOptionalSubject] CHECK CONSTRAINT [FK_tblStudentOptionalSubject_tblUser1]
GO
ALTER TABLE [dbo].[tblSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblSubject_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSubject] CHECK CONSTRAINT [FK_tblSubject_tblUser]
GO
ALTER TABLE [dbo].[tblSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblSubject_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSubject] CHECK CONSTRAINT [FK_tblSubject_tblUser1]
GO
ALTER TABLE [dbo].[tblSubjectCategory]  WITH CHECK ADD  CONSTRAINT [FK_tblSubjectCategory_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSubjectCategory] CHECK CONSTRAINT [FK_tblSubjectCategory_tblUser]
GO
ALTER TABLE [dbo].[tblSubjectCategory]  WITH CHECK ADD  CONSTRAINT [FK_tblSubjectCategory_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblSubjectCategory] CHECK CONSTRAINT [FK_tblSubjectCategory_tblUser1]
GO
ALTER TABLE [dbo].[tblTeacher]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacher_tblTeacherCategory] FOREIGN KEY([TeacherCategoryId])
REFERENCES [dbo].[tblTeacherCategory] ([TeacherCategoryId])
GO
ALTER TABLE [dbo].[tblTeacher] CHECK CONSTRAINT [FK_tblTeacher_tblTeacherCategory]
GO
ALTER TABLE [dbo].[tblTeacher]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacher_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblTeacher] CHECK CONSTRAINT [FK_tblTeacher_tblUser]
GO
ALTER TABLE [dbo].[tblTeacher]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacher_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblTeacher] CHECK CONSTRAINT [FK_tblTeacher_tblUser1]
GO
ALTER TABLE [dbo].[tblTeacherCategory]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherCategory_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblTeacherCategory] CHECK CONSTRAINT [FK_tblTeacherCategory_tblUser]
GO
ALTER TABLE [dbo].[tblTeacherCategory]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherCategory_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblTeacherCategory] CHECK CONSTRAINT [FK_tblTeacherCategory_tblUser1]
GO
ALTER TABLE [dbo].[tblTeacherExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherExtraCurricularActivity_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblTeacherExtraCurricularActivity] CHECK CONSTRAINT [FK_tblTeacherExtraCurricularActivity_tblSchool]
GO
ALTER TABLE [dbo].[tblTeacherExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherExtraCurricularActivity_tblSchool1] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblTeacherExtraCurricularActivity] CHECK CONSTRAINT [FK_tblTeacherExtraCurricularActivity_tblSchool1]
GO
ALTER TABLE [dbo].[tblTeacherExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherExtraCurricularActivity_tblTeacher] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[tblTeacher] ([TeacherId])
GO
ALTER TABLE [dbo].[tblTeacherExtraCurricularActivity] CHECK CONSTRAINT [FK_tblTeacherExtraCurricularActivity_tblTeacher]
GO
ALTER TABLE [dbo].[tblTeacherExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherExtraCurricularActivity_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblTeacherExtraCurricularActivity] CHECK CONSTRAINT [FK_tblTeacherExtraCurricularActivity_tblUser]
GO
ALTER TABLE [dbo].[tblTeacherExtraCurricularActivity]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherExtraCurricularActivity_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblTeacherExtraCurricularActivity] CHECK CONSTRAINT [FK_tblTeacherExtraCurricularActivity_tblUser1]
GO
ALTER TABLE [dbo].[tblTeacherQualification]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherQualification_tblQualification] FOREIGN KEY([QualificationId])
REFERENCES [dbo].[tblQualification] ([QualificationId])
GO
ALTER TABLE [dbo].[tblTeacherQualification] CHECK CONSTRAINT [FK_tblTeacherQualification_tblQualification]
GO
ALTER TABLE [dbo].[tblTeacherQualification]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherQualification_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblTeacherQualification] CHECK CONSTRAINT [FK_tblTeacherQualification_tblSchool]
GO
ALTER TABLE [dbo].[tblTeacherQualification]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherQualification_tblTeacher] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[tblTeacher] ([TeacherId])
GO
ALTER TABLE [dbo].[tblTeacherQualification] CHECK CONSTRAINT [FK_tblTeacherQualification_tblTeacher]
GO
ALTER TABLE [dbo].[tblTeacherQualification]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherQualification_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblTeacherQualification] CHECK CONSTRAINT [FK_tblTeacherQualification_tblUser]
GO
ALTER TABLE [dbo].[tblTeacherQualification]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherQualification_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblTeacherQualification] CHECK CONSTRAINT [FK_tblTeacherQualification_tblUser1]
GO
ALTER TABLE [dbo].[tblTeacherSchool]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherSchool_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblTeacherSchool] CHECK CONSTRAINT [FK_tblTeacherSchool_tblSchool]
GO
ALTER TABLE [dbo].[tblTeacherSchool]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherSchool_tblTeacher] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[tblTeacher] ([TeacherId])
GO
ALTER TABLE [dbo].[tblTeacherSchool] CHECK CONSTRAINT [FK_tblTeacherSchool_tblTeacher]
GO
ALTER TABLE [dbo].[tblTeacherSchool]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherSchool_tblTeacherCategory] FOREIGN KEY([TeacherCategoryId])
REFERENCES [dbo].[tblTeacherCategory] ([TeacherCategoryId])
GO
ALTER TABLE [dbo].[tblTeacherSchool] CHECK CONSTRAINT [FK_tblTeacherSchool_tblTeacherCategory]
GO
ALTER TABLE [dbo].[tblTeacherSchool]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherSchool_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblTeacherSchool] CHECK CONSTRAINT [FK_tblTeacherSchool_tblUser]
GO
ALTER TABLE [dbo].[tblTeacherSchool]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherSchool_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblTeacherSchool] CHECK CONSTRAINT [FK_tblTeacherSchool_tblUser1]
GO
ALTER TABLE [dbo].[tblTeacherSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherSubject_tblClass] FOREIGN KEY([ClassId], [GradeId], [SchoolIds])
REFERENCES [dbo].[tblClass] ([ClassId], [GradeId], [SchoolId])
GO
ALTER TABLE [dbo].[tblTeacherSubject] CHECK CONSTRAINT [FK_tblTeacherSubject_tblClass]
GO
ALTER TABLE [dbo].[tblTeacherSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherSubject_tblGrade] FOREIGN KEY([GradeId])
REFERENCES [dbo].[tblGrade] ([GradeId])
GO
ALTER TABLE [dbo].[tblTeacherSubject] CHECK CONSTRAINT [FK_tblTeacherSubject_tblGrade]
GO
ALTER TABLE [dbo].[tblTeacherSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherSubject_tblSchool] FOREIGN KEY([SchoolIds])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblTeacherSubject] CHECK CONSTRAINT [FK_tblTeacherSubject_tblSchool]
GO
ALTER TABLE [dbo].[tblTeacherSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherSubject_tblSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
GO
ALTER TABLE [dbo].[tblTeacherSubject] CHECK CONSTRAINT [FK_tblTeacherSubject_tblSubject]
GO
ALTER TABLE [dbo].[tblTeacherSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherSubject_tblTeacher] FOREIGN KEY([TeacherId])
REFERENCES [dbo].[tblTeacher] ([TeacherId])
GO
ALTER TABLE [dbo].[tblTeacherSubject] CHECK CONSTRAINT [FK_tblTeacherSubject_tblTeacher]
GO
ALTER TABLE [dbo].[tblTeacherSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherSubject_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblTeacherSubject] CHECK CONSTRAINT [FK_tblTeacherSubject_tblUser]
GO
ALTER TABLE [dbo].[tblTeacherSubject]  WITH CHECK ADD  CONSTRAINT [FK_tblTeacherSubject_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblTeacherSubject] CHECK CONSTRAINT [FK_tblTeacherSubject_tblUser1]
GO
ALTER TABLE [dbo].[tblTimeTable]  WITH CHECK ADD  CONSTRAINT [FK_tblTimeTable_tblClass] FOREIGN KEY([ClassId], [GradeId], [SchoolId])
REFERENCES [dbo].[tblClass] ([ClassId], [GradeId], [SchoolId])
GO
ALTER TABLE [dbo].[tblTimeTable] CHECK CONSTRAINT [FK_tblTimeTable_tblClass]
GO
ALTER TABLE [dbo].[tblTimeTable]  WITH CHECK ADD  CONSTRAINT [FK_tblTimeTable_tblGrade] FOREIGN KEY([GradeId])
REFERENCES [dbo].[tblGrade] ([GradeId])
GO
ALTER TABLE [dbo].[tblTimeTable] CHECK CONSTRAINT [FK_tblTimeTable_tblGrade]
GO
ALTER TABLE [dbo].[tblTimeTable]  WITH CHECK ADD  CONSTRAINT [FK_tblTimeTable_tblSchool] FOREIGN KEY([SchoolId])
REFERENCES [dbo].[tblSchool] ([SchoolId])
GO
ALTER TABLE [dbo].[tblTimeTable] CHECK CONSTRAINT [FK_tblTimeTable_tblSchool]
GO
ALTER TABLE [dbo].[tblTimeTable]  WITH CHECK ADD  CONSTRAINT [FK_tblTimeTable_tblSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
GO
ALTER TABLE [dbo].[tblTimeTable] CHECK CONSTRAINT [FK_tblTimeTable_tblSubject]
GO
ALTER TABLE [dbo].[tblTimeTable]  WITH CHECK ADD  CONSTRAINT [FK_tblTimeTable_tblUser] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblTimeTable] CHECK CONSTRAINT [FK_tblTimeTable_tblUser]
GO
ALTER TABLE [dbo].[tblTimeTable]  WITH CHECK ADD  CONSTRAINT [FK_tblTimeTable_tblUser1] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblTimeTable] CHECK CONSTRAINT [FK_tblTimeTable_tblUser1]
GO
ALTER TABLE [dbo].[tblUserCategoryFunction]  WITH CHECK ADD  CONSTRAINT [FK_tblUserCategoryFunction_tblFunction] FOREIGN KEY([FunctionId])
REFERENCES [dbo].[tblFunction] ([FunctionId])
GO
ALTER TABLE [dbo].[tblUserCategoryFunction] CHECK CONSTRAINT [FK_tblUserCategoryFunction_tblFunction]
GO
ALTER TABLE [dbo].[tblUserCategoryFunction]  WITH CHECK ADD  CONSTRAINT [FK_tblUserCategoryFunction_tblUser] FOREIGN KEY([ModifiedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblUserCategoryFunction] CHECK CONSTRAINT [FK_tblUserCategoryFunction_tblUser]
GO
ALTER TABLE [dbo].[tblUserCategoryFunction]  WITH CHECK ADD  CONSTRAINT [FK_tblUserCategoryFunction_tblUser1] FOREIGN KEY([CreatedBy])
REFERENCES [dbo].[tblUser] ([UserId])
GO
ALTER TABLE [dbo].[tblUserCategoryFunction] CHECK CONSTRAINT [FK_tblUserCategoryFunction_tblUser1]
GO
USE [master]
GO
ALTER DATABASE [SchoolMGT] SET  READ_WRITE 
GO
