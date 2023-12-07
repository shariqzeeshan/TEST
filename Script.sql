create table blogpost
(
Id int IDENTITY(1,1) NOT NULL,
Title varchar(100),
Contents varchar(1000),
Timestamp DateTime,
CategoryId int,
PRIMARY KEY (Id),
)
[Name] Varchar(100)
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
