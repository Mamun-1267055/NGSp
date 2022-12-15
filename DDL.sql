Create Database MyWebApp_JWT
Go
Use MyWebApp_JWT
Go
--Create All Tables--

Create Table Category
(
	Id int primary key identity,
	CategoryName Varchar(50) Not Null
)
Go
Create Table Products
(
	Id int primary key identity,
	CategoryId int References Category(Id) ON  DELETE CASCADE,
	ProductName Varchar(50) Not Null,
	Quantity decimal Not Null,
	UnitPrice decimal Not Null,
	StoreDate Date Not Null,
	IsAvailable Bit
)
Go
Create Table ProductImage
(
  Id INT PRIMARY KEY IDENTITY,
  ProductId INT REFERENCES Products(Id) ON  DELETE CASCADE,
  ImagePath VARCHAR(500),
)
GO

--Start Create Store Procedure--
CREATE PROC Sp_ShowProducts
AS
BEGIN
	SELECT * FROM Products
END
GO

--alter store procedure for show products

Create PROC [dbo].[Sp_ShowProductsWithImage]
AS
BEGIN
	SELECT p.Id,c.CategoryName,p.ProductName,p.Quantity,p.UnitPrice,StoreDate,p.IsAvailable,i.ImagePath FROM Products p
	inner join Category c on p.CategoryId=c.Id
	inner join  ProductImage i on p.id=i.ProductId
END
GO

--show product by category

Create PROC [dbo].[Sp_ShowProductsByCategory]
AS
BEGIN
	SELECT p.Id,c.CategoryName,p.ProductName,p.Quantity,p.UnitPrice,StoreDate,p.IsAvailable,i.ImagePath FROM Products p
	inner join Category c on p.CategoryId=c.Id
	inner join  ProductImage i on p.id=i.ProductId
	where p.CategoryId=c.Id
END
GO

/*insert product by store proc*/

Create Proc Sp_ProductInsert
@catId int,
@productName varchar(50),
@quantity decimal,
@unitPrice decimal,
@storeDate Date,
@isAvailable bit
As	
Begin
	Insert Into Products(CategoryId,ProductName,Quantity,UnitPrice,StoreDate,IsAvailable)
	Values(@catId,@productName,@quantity,@unitPrice,@storeDate,@isAvailable)
End
Go

/*test procedure*/

Exec [dbo].[Sp_ProductInsert] 2,'T-Shirt',100,800,'2022-01-01','True'
Go

/*create proc for product update*/

Create Proc Sp_ProductUpdate
@id int,
@catId int,
@productName varchar(50),
@quantity decimal,
@unitPrice decimal,
@storeDate Date,
@isAvailable bit
As	
Begin
	Update Products Set CategoryId=@catId,ProductName=@productName,Quantity=@quantity,UnitPrice=@unitPrice,StoreDate=@storeDate,IsAvailable=@isAvailable
	Where Id=@id
End
Go

--text proc
Exec Sp_ProductUpdate 1,1,'Sharee',50,1200,'2022-01-01','True'
Go

--create proc for delete product

Create Proc Sp_DeleteProduct
@id int
As
Begin
	Delete Products Where Id=@id
End
Go

--test proc
Exec Sp_DeleteProduct 3
Go

Insert into Products Values(2,'Cap',100.00,500.00,'2022-04-04','True')
Go
Select * From Products
Go

