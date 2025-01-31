/* 
	Dealership sales database
    Name:	dealer_sales_db
	Author: Brendan Klostermann
    Description: Final project for relational databses and mysql
*/


/* If database exists delete it. */
IF EXISTS (SELECT 1 FROM master.dbo.sysdatabases
			WHERE name = 'dealershipDB')


BEGIN 
	DROP DATABASE dealershipDB
	print '' print '** dropping database dealershipDB **'
END
GO

/* Create new database */

print '' print'*** creating database dealershipDB'
GO
CREATE DATABASE dealershipDB
GO

print '' print'*** using database dealershipDB'
GO
USE [dealershipDB]
GO


/* Creating Tables for Database dealershipDB */

/* PayType table */
print '' print 'creating PayType table'
GO

CREATE TABLE [dbo].[PayType](
	[PayTypeID]		[int]			identity(100,1) 	not null,
    [PayType]		[nvarchar](25) 						not null,
	
	constraint [pk_PayTypeID] primary key([PayTypeID])
);
GO
 


/* Vehicle table */
print '' print 'creating Vehicle table'
GO
CREATE TABLE [dbo].[Vehicle](
	[VehicleID]		[int]			identity(100000,1)	not null,
    [Make]			[varchar](25)						not null,
    [Model]			[varchar](25)						not null,
    [SubModel]		[varchar](20)						null,
    [ModelYear]		[char](4)							not null,
    [Cost]			[money]								not null,
    [ServiceCost]	[money]								null,
    [Mileage]		[int]								not null,
    [Vin]			[varchar](20)						not null,
    [BodyStyle]		[varchar](20)						not null,
	[Available]		[bit] 								not null 	default 1,
	
	constraint	[pk_VehicleID]	primary key([VehicleID])
);
GO

print'' print'inserting vehicle information'
go

insert into [dbo].[Vehicle]
		([Make],[Model],[SubModel],[ModelYear],[Cost],[ServiceCost],[Mileage],[VIN],[BodyStyle])
	values
		("Chevy","Camaro","SS","2017",2000.00,200.00,48374,"1hdb38sh3840ajfnq","Coupe"),
		("Ford","Mustang","GT","2016",19500.00,892.93,87352,"1hdn39akd83jal4s8","Coupe")
GO



/* ZipCode Table */
print '' print 'creating zipcode table'
GO
CREATE TABLE	[dbo].[ZipCode](
	[ZipCode]		[int]								not null,
    [City]			[nvarchar](30)						not null,
    [StateName]		[nvarchar](30)						not null,
	
	constraint [pk_ZipCode]		primary key([ZipCode])
);
GO

print'' print'Inserting zipcodes'
GO

insert into [dbo].[ZipCode]
		([ZipCode],[City],[StateName])
	values
		(52402,'Cedar Rapids','Iowa'),
		(52302,'Marion','Iowa')
GO


/* Location Table */
print '' print 'creating location table'
GO
CREATE TABLE	[dbo].[Location](
	[LocationID]	[int]			identity(1000,1)	not null,
    [ZipCode]		[int]								not null,
    [StreetAddress]	[nvarchar](30) 						not null,
    [LocationName]	[nvarchar](25) 						not null,
    [PhoneNumber]	[nvarchar](15) 						not null,
    
    constraint [pk_LocationID]	primary key([LocationID]),
	constraint [fk_ZipCode_ZipCode_Loc]
		foreign key([ZipCode]) references[dbo].[ZipCode]([ZipCode])
);
GO

print'' print'Insertin Location information'
GO

insert into [dbo].[Location]
		([ZipCode],[StreetAddress],[LocationName],[PhoneNumber])
	values
		(52402,'123 Cherry St','Chevy sales','3195558978'),
		(52302,'456 Willow Ln','Kia Sales','3770981234'),
		(52402,'789 Elm St','Ford Sales','1238763948')
GO


/* Customer Table */
print '' print 'creating customer table'
GO

CREATE TABLE [dbo].[Customer](
	[CustomerID]	[int]			identity(100000,1) 	not null,
    [FirstName]		[nvarchar](30)						not null,
    [LastName]		[nvarchar](30) 						not null,
    [PhoneNumber]	[nvarchar](15) 						not null,
    [EmailAddress]	[nvarchar](100) 					not null,
    [ZipCode]		[int]								not null,
	[Active]		[bit]								not null	default 1,
    
    constraint [pk_CustomerID]	primary key([CustomerID]),
	constraint [ak_EmailAddress] unique([EmailAddress]),
	constraint [fk_ZipCode_ZipCode_Cust]
		foreign key([ZipCode]) references[dbo].[ZipCode]([ZipCode])
);
GO

print '' print' inserting into Customer table'
GO

insert into [dbo].[Customer]
		([FirstName],[LastName],[PhoneNumber],[EmailAddress],[ZipCode])
	values
		('Hunter','Kiser','3194852938','hunter@gmail.com',52302),
		('Chase','Bading','3198274938','chase@gmail.com',52402)
GO



/* Factory Options table */
print '' print 'creating factory options table' 
GO

CREATE TABLE [dbo].[FactoryOptions](
	[FactoryOptionsID][int]			identity(100000,1) 	not null,
    [VehicleID]		[int]								not null,
    [EngineSize]	[nvarchar](10)						not null,
    [CylinderCount]	[int]								not null,
    [Transmission]	[nvarchar](50)						not null,
    [DriveLine]		[nvarchar](50) 						not null,
    [InteriorMaterial][nvarchar](50) 					not null,
    [InteriorColor]	[nvarchar](50)						not null,
    [ExteriorColor]	[nvarchar](50) 						not null,
        
    constraint [pk_FactoryOptions]	primary key([FactoryOptionsID]),
	constraint [fk_Vehicle_VehicleID_FOpt]
		foreign key([VehicleID]) references[dbo].[Vehicle]([VehicleID])
);
GO


print'' print'*** Inserting into facotry options ***'
GO

insert into [dbo].[FactoryOptions]
		([VehicleID],[EngineSize],[CylinderCount],[Transmission],[DriveLine],[InteriorMaterial],[InteriorColor],[ExteriorColor])
	values
		(100000,"6.2L",8,"Manual","RWD","Cloth","Gray","Midnight Gray Metallic"),
		(100001,"5.0",8,"Auto","RWD","Leather","Black","Rally Yellow")


/* Dealer Added Options table */
print'' print'creating dealer added options table'
GO

CREATE TABLE [dbo].[DealerAddedOptions](
	[DealerAddOnsID]	[int]		identity(100000,1)  not null,
    [VehicleID]			[int]							not null,
    [RustPreventionCost][decimal](9,2)					null,
    [ServicePackCost]	[decimal](9,2)					null,
    [InteriorProtectionPack][decimal](9,2)				null,
    [OffRoadPackCost]	[decimal](9,2)					null,
    [AccessoriesCost]	[decimal](9,2)					null,
     
	constraint [pk_DealerAddOnsID]	primary key([DealerAddOnsID]),
	constraint [fk_Vehicle_VehicleID_Opt]
		foreign key([VehicleID]) references[dbo].[Vehicle]([VehicleID])
);
GO



/* Employee table */
print'' print'creating employee table'
GO

CREATE TABLE [dbo].[Employee](
	[EmployeeID]	[int]			identity(10000,1)	not null,
    [FirstName]		[nvarchar](30) 						not null,
    [LastName]		[nvarchar](30) 						not null,
    [PhoneNumber]	[nvarchar](15)						not null,
    [LocationID]	[int]								null,
	[EmailAddress]	[nvarchar](100) 					not null,
	[PasswordHash]	[nvarchar](100)						not null default
		'9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',
	[Active]		[bit]								not null default 1,
	
	constraint [pk_EmployeeID] primary key([EmployeeID]),
	constraint [ak_EmailAddress_Emp] unique([EmailAddress]),
	
	constraint [fk_Location_LocationID_Emp]
		foreign key([LocationID]) references[dbo].[Location]([LocationID])
);
GO

print'' print'Inserting Employee Info'
GO

insert into [dbo].[Employee]
		([FirstName],[LastName],[PhoneNumber],[LocationID],[EmailAddress],[PasswordHash],[Active])
	values
		('Brendan','Klostermann','3197205011',1000,'brendan@company.com','9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',1),
		('Marika','Bjornson','1928374839',1001,'marika@company.com','9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',1),
		('Jordan','Potts','3857167394',1000,'jordan@company.com','9c9064c59f1ffa2e174ee754d2979be80dd30db552ec03e7e327e9b1a4bd594e',1)


/* Roles table*/
print'' print'creating roles table'
GO

CREATE TABLE [dbo].[Roles](
	[RoleID]		[nvarchar](30)						not null,
	[Description]	[nvarchar](250)						not null,
	constraint	[pk_RoleID] primary key([RoleID])
);
GO

/* Insert roles */
Print'' print'inserting roles'
GO

insert into [dbo].[Roles]
		([RoleID],[Description])
	values
		('Director','Dealership sales director'),
		('Manager','Dealership Sales manager'),
		('Salesman','Dealership salesman')


/* Employee Role table*/
print'' print'creating employee role table'
GO

CREATE TABLE [dbo].[EmployeeRole](
	[EmployeeID]	[int]								not null,
	[RoleID]		[nvarchar](30)						not null,
	constraint [fk_RoleID_EmpRole]
		foreign key([RoleID]) references[dbo].[Roles]([RoleID]),
	constraint [fk_EmployeeID_EmpRole]
		foreign key([EmployeeID]) references[dbo].[Employee]([EmployeeID]),
	constraint [pk_EmployeeID_RoleID]
		primary key([EmployeeID],[RoleID])
);
GO

/* Insert into employee Roles */
print'' print'Insert employee roles'
GO

insert into [dbo].[EmployeeRole]
		([EmployeeID],[RoleID])
	values
		(10000,'Manager'),
		(10001,'Salesman'),
		(10002,'Director')

/* Sales Record table */
print'' print'creating sales Record'
GO

CREATE TABLE [dbo].[SaleRecord](
	[SaleID]		[int]			identity(100000,1)	not null,
    [LocationID]	[int]								not null,
    [PayTypeID]		[int] 								not null,
    [EmployeeID]	[int]								not null,
    [VehicleID]		[int]								not null,
    [TradeIn]		[bit]								not null		default 0,
    [SalePrice]		[decimal](9,2)						not null,
	
	constraint 	[pk_SaleID]		primary key([SaleID]),
	
	constraint 	[fk_Location_LocationID_Sale]
		foreign key([LocationID]) 	references[dbo].[Location]([LocationID]),
	constraint 	[fk_PayType_PayTypeID_Sale]
		foreign key([PayTypeID]) 	references[dbo].[PayType]([PayTypeID]),
	constraint 	[fk_Employee_EmployeeID_Sale]
		foreign key([EmployeeID])	references[dbo].[Employee]([EmployeeID]),
	constraint 	[fk_Vehicle_VehicleID_Sale]
		foreign key([VehicleID]) 	references[dbo].[Vehicle]([VehicleID])
);
GO


/* Customer Sale table */
print'' print'creating customer sale table'
GO

CREATE TABLE [dbo].[CustomerSale](
	[CustomerID]	[int]								not null,
    [SaleID]		[int] 								not null,
	
	constraint 	[fk_Customer_CustomerID]
		foreign key([CustomerID])	references[dbo].[Customer]([CustomerID]),
	constraint 	[fk_SaleRecord_SaleID]
		foreign key([SaleID])		references[dbo].[SaleRecord]([SaleID]),
	constraint 	[pk_CustomerID_SaleID]
		primary key([CustomerID],[SaleID])
);
GO


/* Trade In table */
print'' print'creating TradeId table'
GO

CREATE TABLE [dbo].[TradeIn](
	[TradeInID]		[int]			identity(100000,1)	not null,
    [SaleID]		[int]								not null,
    [TradeInValue]	[decimal](9,2) 						not null,
    [Make]			[nvarchar](15)						not null,
    [Model]			[varchar](25)						not null,
    [VIN]			[char](17)							not null,
    
    constraint 	[pk_TradeInID] 	primary key([TradeInID]),
	constraint 	[ak_VIN]		unique([VIN]),
	constraint 	[fk_SaleID_Trade]
		foreign key([SaleID])	references[dbo].[SaleRecord]([SaleID])
);


/* Audit tables*/

/* May not get implemented

create table sales_audit(
	saleid		int 		not null,
    vehicleid	int			not null,
    action_type varchar(50)	not null,
    action_date	datetime 	not null,
    action_user	varchar(100)	not null
);


create table vehicle_audit(
	vehicleid	int		not null,
    action_type	varchar(50) not null,
    action_date	datetime	not null,
    action_user	varchar(100) not null
);


create table customer_audit(
	customerid	int		not null,
    action_type	varchar(50) not null,
    action_date	datetime	not null,
    action_user	varchar(100) not null
);

*/








/* 
**** STORED PROCEDURES **** 
*/


/* ***** Select Procedures ***** */

print '' print'*** creating sp_authenticate_user ***'
GO
CREATE PROCEDURE [dbo].[sp_authenticate_user]
(
	@Email			[nvarchar] (100),
	@PasswordHash	[nvarchar] (100)
)
AS
	BEGIN
		SELECT COUNT([EmployeeID]) as 'Authenticated'
		FROM [Employee]
		WHERE @Email = [EmailAddress]
			AND @PasswordHash = [PasswordHash]
			AND	Active = 1;
	END
GO


print '' print'*** creating sp_select_user_by_email ***'
GO
CREATE PROCEDURE [dbo].[sp_select_user_by_email]
(
	@Email		nvarchar(100)
)
AS
	BEGIN
		SELECT 	[EmployeeID],[FirstName],[LastName],[PhoneNumber],[LocationID],[EmailAddress],[Active]
		FROM 	[Employee]
		WHERE 	[EmailAddress] = @Email
	END	
GO 


print '' print'*** creating sp_select_roles_by_EmployeeID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_roles_by_EmployeeID]
(
	@EmployeeID		[int]
)
AS
	BEGIN
		SELECT 	[RoleID]
		FROM 	[EmployeeRole]
		WHERE 	@EmployeeID = [EmployeeID]
	END	
GO


print'' print '*** creating sp_select_all_available_vehicles ***'
GO
create procedure [dbo].[sp_select_all_available_vehicles]
as
	begin
		select 	[VehicleID],[Vin],[Make],[Model],[SubModel],[ModelYear],[Mileage],[BodyStyle],[Cost],[ServiceCost],[Available]
		from 	[Vehicle]
		where	[Available] = 1;
	end
GO

print'' print '*** creating sp_select_all_roles ***'
GO
create procedure [dbo].[sp_select_all_roles]
as
	begin
		select 	[RoleID],[Description]
		from 	[Roles]
	end
GO


print'' print '*** creating sp_select_vehicle_by_vehicleID ***'
GO
create procedure [dbo].[sp_select_vehicle_by_vehicleID]
(
	@vehID	[int]
)
as
	begin
		select 	[VehicleID],[Vin],[Make],[Model],[SubModel],[ModelYear],[Mileage],[BodyStyle],[Cost],[ServiceCost],[Available]
		from 	[Vehicle]
		where	@vehID = [VehicleID];
	end
GO


print '' print '*** creating sp_select_factory_options_by_vehicleID ***'
GO
create procedure [dbo].[sp_select_factory_options_by_vehicleID]
(
	@VehID	[int]
)
as
	begin
		select 	[EngineSize],[CylinderCount],[Transmission],[DriveLine],[InteriorMaterial],[InteriorColor],[ExteriorColor]
		from 	[FactoryOptions]
		where 	@vehID = [VehicleID]
	end
GO



print'' print'*** Creating sp_select_all_active_customers ***'
GO
create procedure [dbo].[sp_select_all_active_customers]
as
	begin
		select 	[CustomerID],[FirstName],[LastName],[PhoneNumber],[EmailAddress],[ZipCode]
		from 	[Customer]
		where 	[Active] = 1;
	end
GO

print'' print'*** Creating sp_select_all_ZipCode ***'
GO
create procedure [dbo].[sp_select_all_ZipCode]
as
	begin
		select	[ZipCode],[City],[stateName]
		FROM	[ZipCode]
	end
Go

print'' print'*** Creating sp_select_all_locations***'
GO
create procedure [dbo].[sp_select_all_locations]
as
	begin
		select [LocationID], [ZipCode], [StreetAddress], [LocationName], [PhoneNumber]
		from [Location]
	end
GO


print'' print'*** Creating sp_select_all_sale_records ***'
go
create procedure [dbo].[sp_select_all_sales_records]
as	
	begin
		select 	[SaleID],[LocationID],[PayTypeID],[EmployeeID],[VehicleID],[TradeIn],[SalePrice]
		from 	[SaleRecord]
	END
go


print'' print'*** Creating sp_select_sale_by_sale_id ***'
go
create procedure [dbo].[sp_select_sale_by_sale_id]
(
	@saleID 	int
)
as	
	begin
		select 	[SaleID],[LocationID],[PayTypeID],[EmployeeID],[VehicleID],[TradeIn],[SalePrice]
		from 	[SaleRecord]
		where 	[SaleID] = @saleID
	end
go


print '' print'*** Creating sp_select_sales_by_customer_id***'
go
create procedure [dbo].[sp_select_sales_by_customer_id]
(
	@custID 	int
)
as
	begin
		select 	[SaleRecord].[SaleID],[LocationID],[PayTypeID],[EmployeeID],[VehicleID],[TradeIn],[SalePrice]
		from 	[SaleRecord]	join	[CustomerSale] 
			on [CustomerSale].[SaleID] = [SaleRecord].[SaleID]
		where	[CustomerSale].[CustomerID] = @custID
	end
go


print'' print'*** Creating sp_select_trade_by_sale_id ***'
go
create procedure [dbo].[sp_select_trade_by_sale_id]
(
	@saleID 	int
)
as
	begin
		select 	[TradeInID],[SaleID],[TradeInValue],[Make],[Model],[VIN]
		from 	[TradeIn]
		where 	[SaleID] = @saleID
	end
go

		





/* ****** INSERT STATEMENTS ****** */

print'' print'*** Creating sp_insert_customer ***'
GO
create procedure [dbo].[sp_insert_customer]
(
	@fName			[nvarchar](30),
    @lName			[nvarchar](30),
    @phone			[nvarchar](15),
    @email			[nvarchar](100),
    @zip			[int]
)
as
	begin
		insert into [dbo].[Customer]
			([FirstName],[LastName],[PhoneNumber],[EmailAddress],[ZipCode])
		values
			(@fName,@lName,@phone,@email,@zip)
	end
GO

print'' print'*** Creating sp_insert_employee ***'
GO
create procedure [dbo].[sp_insert_employee]
(
	@fname			[nvarchar](30),
    @lname			[nvarchar](30),
    @phone			[nvarchar](15),
    @location		[int],
	@email			[nvarchar](100)
)
as
	begin
		insert into [dbo].[Employee]
			([FirstName],[LastName],[PhoneNumber],[LocationID],[EmailAddress])
		values
			(@fname,@lname,@phone,@location,@email)
	end
GO



print '' print "*** Creating sp_insert_vehicle ***"
GO
create procedure [dbo].[sp_insert_vehicle]
(
	@make 		[nvarchar](25),
	@model 		[nvarchar](25),
	@submodel 	[nvarchar](20),
	@modelYear 	[char](4),
	@cost 		[money],
	@serviceCost [money],
	@miles 		[int],
	@vin 		[nvarchar](20),
	@bodyStyle 	[nvarchar](20),
	@available 	[bit]
)
as
	begin
		insert into [dbo].[Vehicle]
			([Make],[Model],[SubModel],[ModelYear],[Cost],[ServiceCost],[Mileage],[Vin],[BodyStyle],[Available])
		values
			(@make,@model,@submodel,@modelYear,@cost,@serviceCost,@miles,@vin,@bodyStyle,@available)
		end
	select scope_identity()
go


print '' print '*** creating sp_insert_factory_options ***'
GO
CREATE PROCEDURE [dbo].[sp_insert_factory_options]
(
    @vehicleID int,
    @engineSize nvarchar(10),
    @cylinderCount int,
    @transmission nvarchar(50),
    @driveLine nvarchar(50),
    @interiorMaterial nvarchar(50),
    @interiorColor nvarchar(50),
    @exteriorColor nvarchar(50)
)
AS
	BEGIN
		INSERT INTO [dbo].[FactoryOptions]
			([VehicleID], [EngineSize], [CylinderCount], [Transmission], [DriveLine], [InteriorMaterial], [InteriorColor], [ExteriorColor])
		VALUES
			(@vehicleID, @engineSize, @cylinderCount, @transmission, @driveLine, @interiorMaterial, @interiorColor, @exteriorColor)
	END
GO


print'' print'*** Creating sp_insert_sale_record ***'
go
create procedure [dbo].[sp_insert_sale_record]
(
	@custID 	int,
	@locID 		int,
	@payID 		int,
	@empID 		int,
	@vehID 		int,
	@trade 		bit,
	@price 		decimal(9,2)
)
as
	begin 
		insert into [dbo].[SaleRecord]
			([LocationID],[PayTypeID],[EmployeeID],[VehicleID],[TradeIn],[SalePrice])
		values
			(@locID,@payID,@empID,@vehID,@trade,@price)


		insert into [dbo].[CustomerSale]
			([CustomerID],[SaleID])
		values	
			(@custID, scope_identity())
		
	end
go


print'' print'*** Creating sp_insert_trade_in ***'
go
create procedure [dbo].[sp_insert_trade_in]
(
	@saleID 	int,
	@tradeValue decimal(9,2),
	@make 		nvarchar(25),
	@model 		nvarchar(25),
	@vin 		nvarchar(20)
)
as 
	begin
		insert into [dbo].[TradeIn]
			([SaleID],[TradeInValue],[Make],[Model],[VIN])
		values
			(@saleID,@tradeValue,@make,@model,@vin)
	end
go



/* ****** Update Statements ****** */

print'' print'*** Creating sp_deactivate_customer ***'
GO
create procedure [dbo].[sp_deactivate_customer]
(
	@custID 	int
)
as 
	begin
		update 	[Customer]
		set		[Active] = 0
		where 	@custID = [CustomerID]
	end
GO

print'' print'*** Creating sp_update_customer ***'
GO
create procedure [dbo].[sp_update_customer]
(
	@custID			[int],
	@fName			[nvarchar](30),
    @lName			[nvarchar](30),
    @phone			[nvarchar](15),
    @email			[nvarchar](100),
    @zip			[int]
)
as
	begin
		update	[Customer]
		
		SET		[FirstName] = @fName,
				[LastName] = @lName,
				[PhoneNumber] = @phone,
				[EmailAddress] = @email,
				[ZipCode] = @zip
				
		where	[CustomerID] = @custID
	end
GO


print'' print'*** Creating sp_deactivate_vehicle ***'
GO
create procedure [dbo].[sp_deactivate_vehicle]
(
@vehID		[int]
)	
as
	begin
		UPDATE	[Vehicle]
		set 	[Available] = 0
		where	@vehID = [VehicleID]
	end
GO

print '' PRINT '*** Creating sp_select_customer_by_customerID ***'
GO
CREATE PROCEDURE [dbo].[sp_select_customer_by_customerID]
(
    @customerID int
)
AS
	BEGIN
		SELECT [CustomerID], [FirstName], [LastName], [PhoneNumber], [EmailAddress], [ZipCode]
		FROM [Customer]
		WHERE @customerID = [CustomerID]
	END
GO








/* ****** Deletions ****** */

print'' print'*** Creating sp_delete_trade_in ***'
go
create procedure [dbo].[sp_delete_trade_in]
(
	@tradeID 	int
)
as
	begin
		delete from [TradeIn]
		where [TradeInID] = @tradeID
	end
go






/* ****** Views ****** */

print'' print'*** Creating vw_customer_email_list ***'
GO
create view [dbo].[vw_customer_email_list]	
	as
		select	[EmailAddress]
		FROM	[Customer]	
go
