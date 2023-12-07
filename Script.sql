create table blogpost
(
Id int IDENTITY(1,1) NOT NULL,
Title varchar(100),
Contents varchar(1000),
Timestamp DateTime,
CategoryId int,
PRIMARY KEY (Id),
)--drop table Categorycreate table Category(CategoryId int IDENTITY(1,1) NOT NULL,
[Name] Varchar(100))insert into Category values ('General')insert into Category values ('Technology')insert into Category values ('Random')--insert into blogpost values ('Blog Post 2 Title', '<p>This is another blog post</p>', '2022-01-31',1)--insert into blogpost values ('"Blog Post Title 1"', '"<p>This is a blog post</p>', '2021-01-31',2)--insert into blogpost values ('"Blog Post Delete Test"', '"<p>This is a blog post</p>', '2021-01-31',3)SELECT b.Id, b.Title, b.Contents, c.CategoryId, c.[Name] FROM blogpost b INNER JOIN CATEGORY c ON c.CategoryId = b.CategoryIdCREATE PROCEDURE [dbo].[GetBlogPosts] 
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		b.Id, 
		b.Title, 
		b.Contents, 
		c.CategoryId, 
		c.[Name] 
	FROM blogpost b 
	INNER JOIN CATEGORY c ON c.CategoryId = b.CategoryId
END

--exec GetBlogPosts

