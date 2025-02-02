USE [master]
GO
/****** Object:  Database [NimapData]    Script Date: 2/1/2025 6:18:53 PM ******/
CREATE DATABASE [NimapData]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'NimapData', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\NimapData.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'NimapData_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL16.MSSQLSERVER\MSSQL\DATA\NimapData_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
 WITH CATALOG_COLLATION = DATABASE_DEFAULT, LEDGER = OFF
GO
ALTER DATABASE [NimapData] SET COMPATIBILITY_LEVEL = 160
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [NimapData].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [NimapData] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [NimapData] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [NimapData] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [NimapData] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [NimapData] SET ARITHABORT OFF 
GO
ALTER DATABASE [NimapData] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [NimapData] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [NimapData] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [NimapData] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [NimapData] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [NimapData] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [NimapData] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [NimapData] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [NimapData] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [NimapData] SET  ENABLE_BROKER 
GO
ALTER DATABASE [NimapData] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [NimapData] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [NimapData] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [NimapData] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [NimapData] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [NimapData] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [NimapData] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [NimapData] SET RECOVERY FULL 
GO
ALTER DATABASE [NimapData] SET  MULTI_USER 
GO
ALTER DATABASE [NimapData] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [NimapData] SET DB_CHAINING OFF 
GO
ALTER DATABASE [NimapData] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [NimapData] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [NimapData] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [NimapData] SET ACCELERATED_DATABASE_RECOVERY = OFF  
GO
EXEC sys.sp_db_vardecimal_storage_format N'NimapData', N'ON'
GO
ALTER DATABASE [NimapData] SET QUERY_STORE = ON
GO
ALTER DATABASE [NimapData] SET QUERY_STORE (OPERATION_MODE = READ_WRITE, CLEANUP_POLICY = (STALE_QUERY_THRESHOLD_DAYS = 30), DATA_FLUSH_INTERVAL_SECONDS = 900, INTERVAL_LENGTH_MINUTES = 60, MAX_STORAGE_SIZE_MB = 1000, QUERY_CAPTURE_MODE = AUTO, SIZE_BASED_CLEANUP_MODE = AUTO, MAX_PLANS_PER_QUERY = 200, WAIT_STATS_CAPTURE_MODE = ON)
GO
USE [NimapData]
GO
/****** Object:  Table [dbo].[CategoryMaster]    Script Date: 2/1/2025 6:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoryMaster](
	[CategoryId] [int] IDENTITY(1,1) NOT NULL,
	[CategoryName] [nvarchar](255) NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[CategoryId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ProductMaster]    Script Date: 2/1/2025 6:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ProductMaster](
	[ProductId] [int] IDENTITY(1,1) NOT NULL,
	[ProductName] [nvarchar](255) NOT NULL,
	[CategoryId] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ProductId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[ProductMaster]  WITH CHECK ADD FOREIGN KEY([CategoryId])
REFERENCES [dbo].[CategoryMaster] ([CategoryId])
ON DELETE SET NULL
GO
/****** Object:  StoredProcedure [dbo].[DeleteCate]    Script Date: 2/1/2025 6:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[DeleteCate](@id int) as begin

delete from CategoryMaster where CategoryId =@id;
end
GO
/****** Object:  StoredProcedure [dbo].[Deleteprod]    Script Date: 2/1/2025 6:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[Deleteprod] (@id int) as begin 

delete from ProductMaster where ProductId=@id
end
GO
/****** Object:  StoredProcedure [dbo].[GetById]    Script Date: 2/1/2025 6:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[GetById](@id int) as begin

select * from CategoryMaster where CategoryId=@id
end
GO
/****** Object:  StoredProcedure [dbo].[GetCategories]    Script Date: 2/1/2025 6:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
  create proc [dbo].[GetCategories] as begin

  select * from CategoryMaster 

  end
GO
/****** Object:  StoredProcedure [dbo].[GetDataTwo]    Script Date: 2/1/2025 6:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[GetDataTwo](@PageSize int, @Offset int) as begin  


-- SQL Query with Pagination
SELECT p.CategoryId, p.ProductId,p.ProductName, c.CategoryName from 
  ProductMaster p left join CategoryMaster c on p.CategoryId= c.CategoryId 
ORDER BY ProductId
OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;

end
GO
/****** Object:  StoredProcedure [dbo].[InsertCategory]    Script Date: 2/1/2025 6:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[InsertCategory](@CategoryName varchar(255)) as begin

insert into CategoryMaster(CategoryName) values (@CategoryName)

end
GO
/****** Object:  StoredProcedure [dbo].[InsertProduct]    Script Date: 2/1/2025 6:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[InsertProduct](@CategoryId int,@ProductName varchar(255)) as begin

insert into ProductMaster (CategoryId,ProductName) values (@CategoryId,@ProductName);


end
GO
/****** Object:  StoredProcedure [dbo].[ProductGetbyID]    Script Date: 2/1/2025 6:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE proc [dbo].[ProductGetbyID] (@id int) as begin 

select p.CategoryId,c.CategoryName,p.ProductId,p.ProductName from ProductMaster p inner join CategoryMaster c on  p.ProductId=@id

end
GO
/****** Object:  StoredProcedure [dbo].[Readcategory]    Script Date: 2/1/2025 6:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Readcategory]
    @Offset INT,
    @PageSize INT
AS
BEGIN
    SELECT *
    FROM CategoryMaster
    ORDER BY CategoryId
    OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateData]    Script Date: 2/1/2025 6:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[UpdateData]
    @id INT,               -- Input parameter for CategoryId
    @CategoryName VARCHAR(255) -- Input parameter for the new CategoryName
AS
BEGIN
    -- Update the CategoryName where CategoryId matches the provided ID
    UPDATE CategoryMaster
    SET CategoryName = @CategoryName
    WHERE CategoryId = @id;
END
GO
/****** Object:  StoredProcedure [dbo].[UpdateProduct]    Script Date: 2/1/2025 6:18:53 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
create proc [dbo].[UpdateProduct](@id int ,@ProductName varchar(255),@CategoryId int) as begin

 update ProductMaster set ProductName=@ProductName 
 ,CategoryId=@CategoryId where ProductId=@id 

end
GO
USE [master]
GO
ALTER DATABASE [NimapData] SET  READ_WRITE 
GO
