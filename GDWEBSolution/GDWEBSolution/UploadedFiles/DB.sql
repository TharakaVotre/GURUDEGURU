USE [master]
GO
/****** Object:  Database [SchoolMGT]    Script Date: 3/8/2017 11:43:28 AM ******/
CREATE DATABASE [SchoolMGT]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'SchoolMGT', FILENAME = N'c:\Program Files\Microsoft SQL Server\MSSQL11.SQLEXPRESS\MSSQL\DATA\SchoolMGT.mdf' , SIZE = 4160KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
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
/****** Object:  StoredProcedure [dbo].[SMGTDeleteTeacherExtraCurricularActivity]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SMGTgetAllTeachers]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SMGTgetGradeSubjects]    Script Date: 3/8/2017 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROC [dbo].[SMGTgetGradeSubjects](@GradeId varchar(20))
as begin
select S.SubjectId, ShortName, SubjectName, S.IsActive
from tblSubject S, tblGradeSubject G 
where S.SubjectId = G.SubjectId
and  G.GradeId like @GradeId

End

GO
/****** Object:  StoredProcedure [dbo].[SMGTgetSchoolGrade]    Script Date: 3/8/2017 11:43:28 AM ******/
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

End


GO
/****** Object:  StoredProcedure [dbo].[SMGTgetSchoolTeacher]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SMGTgetTeacher]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SMGTgetTeacherExActivity]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SMGTgetTeacherQualification]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SMGTgetTeachers]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  StoredProcedure [dbo].[SMGTgetTeacherSubjects]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblApplicationStatus]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblAssignmentDueDate]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblAssignmentHeader]    Script Date: 3/8/2017 11:43:28 AM ******/
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
	[StudentId] [varchar](20) NOT NULL,
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
/****** Object:  Table [dbo].[tblAssignmentQuestionAid]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblAssignmentQuestionAnswer]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblAssignmentQuestionType]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblAssingmentQuestion]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblClass]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblClassTeacher]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblDistrict]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblDivision]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblEntranceApplication]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblEvaluationDetail]    Script Date: 3/8/2017 11:43:28 AM ******/
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
	[ScheduledTime] [time](7) NOT NULL,
	[SchoolId] [varchar](14) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblEvaluationDetail] PRIMARY KEY CLUSTERED 
(
	[EvaluationDetailSeqNo] ASC,
	[SchoolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblEvaluationHeader]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblEvaluationResult]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblEvaluationType]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblEventCalendar]    Script Date: 3/8/2017 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblEventCalendar](
	[SchoolId] [varchar](14) NOT NULL,
	[EventNo] [bigint] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[tblEventcategory]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblEventParticipant]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblExtraCurricularActivity]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblFunction]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblGrade]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblGradeSubject]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblHouse]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblMessageType]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblParameters]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblParent]    Script Date: 3/8/2017 11:43:28 AM ******/
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
 CONSTRAINT [PK_tblParent] PRIMARY KEY CLUSTERED 
(
	[ParentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblParentObservationType]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblParentStudent]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblParentToSchoolMessageDetail]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblParentToSchoolMessageHeader]    Script Date: 3/8/2017 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblParentToSchoolMessageHeader](
	[SchoolId] [varchar](14) NOT NULL,
	[MessageId] [bigint] IDENTITY(1,1) NOT NULL,
	[ParentId] [bigint] NOT NULL,
	[MessageType] [bigint] NOT NULL,
	[Message] [varchar](100) NOT NULL,
	[GradeId] [varchar](20) NULL,
	[ClassId] [varchar](20) NULL,
	[SubjectId] [int] NULL,
	[ExtraActivityId] [varchar](6) NULL,
	[Status] [varchar](1) NOT NULL,
	[CreatedBy] [varchar](20) NOT NULL,
	[CreatedDate] [datetime] NOT NULL,
	[ModifiedBy] [varchar](20) NULL,
	[ModifiedDate] [datetime] NULL,
	[IsActive] [varchar](1) NOT NULL,
 CONSTRAINT [PK_tblParentToSchoolMessageHeader] PRIMARY KEY CLUSTERED 
(
	[SchoolId] ASC,
	[MessageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblProvince]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblQualification]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblRelashionship]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblSchool]    Script Date: 3/8/2017 11:43:28 AM ******/
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
	[Email] [varchar](50) NOT NULL,
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
 CONSTRAINT [PK_tblSchool] PRIMARY KEY CLUSTERED 
(
	[SchoolId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblSchoolCalendar]    Script Date: 3/8/2017 11:43:28 AM ******/
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
	[AcadamicDate] [datetime] NOT NULL,
	[IsHoliday] [varchar](1) NOT NULL,
	[SpecialComment] [varchar](100) NOT NULL,
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
/****** Object:  Table [dbo].[tblSchoolCategory]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblSchoolExtraCurricularActivity]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblSchoolGrade]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblSchoolGroup]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblSchoolRank]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblSchoolSubject]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblSchoolToParentMessageDetail]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblSchoolToParentMessageHeader]    Script Date: 3/8/2017 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblSchoolToParentMessageHeader](
	[SchoolId] [varchar](14) NOT NULL,
	[MessageId] [bigint] IDENTITY(1,1) NOT NULL,
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
/****** Object:  Table [dbo].[tblStudent]    Script Date: 3/8/2017 11:43:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[tblStudent](
	[SchoolId] [varchar](14) NOT NULL,
	[StudentId] [varchar](20) NOT NULL,
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
/****** Object:  Table [dbo].[tblStudentExtraCurricularActivity]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblStudentOptionalSubject]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblSubject]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblSubjectCategory]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblTeacher]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblTeacherCategory]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblTeacherExtraCurricularActivity]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblTeacherQualification]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblTeacherSchool]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblTeacherSubject]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblTimeTable]    Script Date: 3/8/2017 11:43:28 AM ******/
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
 CONSTRAINT [PK_tblTimeTable] PRIMARY KEY CLUSTERED 
(
	[SeqNo] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUser]    Script Date: 3/8/2017 11:43:28 AM ******/
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
/****** Object:  Table [dbo].[tblUserCategory]    Script Date: 3/8/2017 11:43:28 AM ******/
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
 CONSTRAINT [PK_tblUserCategory] PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[tblUserCategoryFunction]    Script Date: 3/8/2017 11:43:28 AM ******/
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
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblClass] FOREIGN KEY([ClassId], [GradeId], [SchoolId])
REFERENCES [dbo].[tblClass] ([ClassId], [GradeId], [SchoolId])
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader] CHECK CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblClass]
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblGrade] FOREIGN KEY([GradeId])
REFERENCES [dbo].[tblGrade] ([GradeId])
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader] CHECK CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblGrade]
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
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader]  WITH CHECK ADD  CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblSubject] FOREIGN KEY([SubjectId])
REFERENCES [dbo].[tblSubject] ([SubjectId])
GO
ALTER TABLE [dbo].[tblParentToSchoolMessageHeader] CHECK CONSTRAINT [FK_tblParentToSchoolMessageHeader_tblSubject]
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
