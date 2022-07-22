Create database BookDB_Demo
go
use BookDB_Demo
go

create table About
(
	AboutID int primary key identity,
	[Title] [nvarchar](250) NULL,
	[Image] [nvarchar](500) NULL,
	[Content] [ntext] NULL


)

Create table Contact
(
	ContactID int primary key identity,
	[ContactName] [nvarchar](50) NULL

)

Create table FAQ
(
	ContactID int primary key identity,
	[Question] [ntext] NULL,
	[Answer] [ntext] NULL
)


Create table Feedback
(
	FeedbackID int primary key identity,
	[Name] [nvarchar](50) NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[Description] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[Status] [bit] NOT NULL
)

Create table News
(
	NewsID int primary key identity,
	[Title] [nvarchar](250) NULL,
	[Image] [nvarchar](500) NULL,
	[Content] [ntext] NULL,
	[CreatedDate] [datetime] NULL


)

Create table Genre
(
	GenreID int primary key identity,
	GenreName [nvarchar](250) unique

)

Create table Authors
(
	AuthorID int primary key identity,
	AuthorName [nvarchar](250) unique
)

Create table Book
(
	BookID int primary key identity,
	BookName [nvarchar](150) not null,
	[Price] decimal,
	[Description] [nvarchar](500) NULL,
	[CreatedDate] [datetime] NULL,
	[Status] [bit] NOT NULL,
	GenreID int references dbo.Genre(GenreID) on delete cascade, 
	AuthorID int references dbo.Authors(AuthorID) on delete cascade

)

Create table FILES
(
	FileId int primary key identity,
	FileName [nvarchar](150) not null,
	Path [nvarchar](max) not null,
	BookID int references dbo.Book(BookID)
)

Create table Employee
(
	EmployeeID int primary key identity,
	[Username] [varchar](25) unique ,
	[Password] [varchar](max) NULL,
	[Name] [nvarchar](50) NULL,
	[Image] [nvarchar](500) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](20) NULL,
	[Address] [nvarchar](200) NULL,
	[Birthday] [datetime] NULL,
	[Gender] [nvarchar](10) NULL,
	[Role] [nvarchar] (50) NULL,
	[Status] [bit] NOT NULL
)

Create table Users
(
	UserID int primary key identity,
	[Username] [varchar](25) unique ,
	[Password] [varchar](max) NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,	
	[Address] [nvarchar](200) NULL,
	[Email] [nvarchar](50) NULL,
	[Phone] [nvarchar](20) NULL,
	[Status] [bit] NOT NULL
)

create table Comment
(
	CommentID int primary key identity,
	Rating float,
	Content ntext,
	BookID int references dbo.Book(BookID) on delete cascade ,
	UserID int references dbo.users(UserID)	on delete cascade

)

Create table Orders
(
	OrderID int primary key identity,
	[Address] [nvarchar](200) NULL,
	[Phone] [nvarchar](20) NULL,
	[Email] [nvarchar](50) NULL,
	[CreatedDate] [datetime] NULL,
	TotalPrice decimal,
	[Status] [bit] NOT NULL


)

Create table OrderDetails
(

	OrderDetailsID int primary key identity,
	[ProductName] [nvarchar](200) NULL,
	BookID int references dbo.Book(BookID) on delete cascade,
	[Quantity] int,
	Price decimal,
	OrderID int references dbo.Orders(OrderID) on delete cascade,
	EmployeeID int references dbo.Employee(EmployeeID) on delete cascade,
	UserID int references dbo.Users(UserID) on delete cascade
	
)






