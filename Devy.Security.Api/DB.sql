CREATE DATABASE Devy_Test

USE [Devy_Test]
GO
/****** Object:  Table [dbo].[Employee_Contacts]    Script Date: 08/02/2025 11:18:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employee_Contacts](
	[Employee_Id] [int] NOT NULL,
	[Country_Code] [nvarchar](5) NOT NULL,
	[Number] [nvarchar](20) NOT NULL,
	[Location_Id] [int] NOT NULL,
 CONSTRAINT [PK__Employee__781134A146B55D34] PRIMARY KEY CLUSTERED 
(
	[Employee_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Locations]    Script Date: 08/02/2025 11:18:03 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Locations](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[City] [nvarchar](200) NOT NULL,
	[Country] [nvarchar](200) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Employee_Contacts]  WITH CHECK ADD  CONSTRAINT [FK__Employee___Locat__398D8EEE] FOREIGN KEY([Location_Id])
REFERENCES [dbo].[Locations] ([Id])
GO
ALTER TABLE [dbo].[Employee_Contacts] CHECK CONSTRAINT [FK__Employee___Locat__398D8EEE]
GO


