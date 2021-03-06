USE [TaskManagerDb]
GO
/****** Object:  Table [dbo].[Priorities]    Script Date: 16.11.2018 16:17:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Priorities](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Prioritys] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Statuses]    Script Date: 16.11.2018 16:17:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Statuses](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [varchar](20) NOT NULL,
 CONSTRAINT [PK_Statuses] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Tasks]    Script Date: 16.11.2018 16:17:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tasks](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Subject] [varchar](100) NOT NULL,
	[Description] [varchar](1000) NOT NULL,
	[Priority_Id] [int] NOT NULL,
	[Status_Id] [int] NOT NULL,
	[EndDate] [date] NULL,
 CONSTRAINT [PK_Tasks] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Priorities] FOREIGN KEY([Priority_Id])
REFERENCES [dbo].[Priorities] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Priorities]
GO
ALTER TABLE [dbo].[Tasks]  WITH CHECK ADD  CONSTRAINT [FK_Tasks_Statuses] FOREIGN KEY([Status_Id])
REFERENCES [dbo].[Statuses] ([Id])
GO
ALTER TABLE [dbo].[Tasks] CHECK CONSTRAINT [FK_Tasks_Statuses]
GO
/****** Object:  StoredProcedure [dbo].[GetFilteredTasks]    Script Date: 16.11.2018 16:17:41 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

CREATE PROCEDURE [dbo].[GetFilteredTasks]
	@Filter nvarchar(100)
AS
BEGIN
	SET NOCOUNT ON;
	SELECT t.Id, t.Subject, t.Description, t.Status_Id, t.Priority_Id, t.EndDate
	FROM Tasks as t
	JOIN Priorities as p ON p.Id = t.Priority_Id
	JOIN Statuses as s ON s.Id = t.Status_Id
	WHERE [Subject] like '%'+ @Filter +'%' 
	or [Description] like '%'+ @Filter + '%' 
	or p.[Name] like '%'+ @Filter + '%' 
	or s.Name like '%'+ @Filter + '%' 
	or  CONVERT(nvarchar, t.EndDate) like '%'+ @Filter + '%' 
END
GO
