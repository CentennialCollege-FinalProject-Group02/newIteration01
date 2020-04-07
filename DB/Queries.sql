
insert into [dbo].[AspNetUsers]
With info as (
SELECT [StreetAddress]
      ,[AddressLine2]
      ,[City]
      ,[Province]
      ,[PostalCode]
      ,[Latitude]
      ,[Longitude]
      ,[Email]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEndDateUtc]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
      ,substring( [UserName],1,8) USERNAME
	  ,ROW_NUMBER() over (order by [AccessFailedCount]) rnum
  FROM [HappySitter].[dbo].[AspNetUsers] a
	, [dbo].[AspNetRoles]	
	,[dbo].[Schedules]
   where email like 'Customer1%'
)
insert into [AspNetUsers](
id, [StreetAddress]
      ,[AddressLine2]
      ,[City]
      ,[Province]
      ,[PostalCode]
      ,[Latitude]
      ,[Longitude]
      ,[Email]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEndDateUtc]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
	  ,USERNAME)
select 
NEWID(),
[StreetAddress]
      ,[AddressLine2]
      ,[City]
      ,[Province]
      ,[PostalCode]
      ,[Latitude]
      ,[Longitude]
      ,REPLACE([Email],'1',RNUM)
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,REPLACE([PhoneNumber],'1',RNUM)
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEndDateUtc]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
	  ,USERNAME +CAST( RNUM AS NVARCHAR(3)) username
from info 
WHERE RNUM != 1




'242ed62f-23be-41d2-895a-3e58349b0808' -- customer role
'990fdc43-f7f3-4128-93a4-73c618f55b11' -- sitter role




insert into   [dbo].[AspNetUserRoles]
select id ,'242ed62f-23be-41d2-895a-3e58349b0808'   
--select * 
FROM [HappySitter].[dbo].[AspNetUsers] a	
   where email like 'customer%' and email not like '%manager%'
   and Username != 'customer1'
   order by id




With info as (
SELECT [StreetAddress]
      ,[AddressLine2]
      ,[City]
      ,[Province]
      ,[PostalCode]
      ,[Latitude]
      ,[Longitude]
      ,[Email]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEndDateUtc]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
      ,substring( [UserName],1,8) USERNAME
	  ,ROW_NUMBER() over (order by [AccessFailedCount]) rnum
  FROM [HappySitter].[dbo].[AspNetUsers] a
	, [dbo].[AspNetRoles]	
	,[dbo].[Schedules]
   where email like 'Sitter1%'
)
insert into [AspNetUsers](
id, [StreetAddress]
      ,[AddressLine2]
      ,[City]
      ,[Province]
      ,[PostalCode]
      ,[Latitude]
      ,[Longitude]
      ,[Email]
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,[PhoneNumber]
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEndDateUtc]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
	  ,USERNAME)
select 
NEWID(),
[StreetAddress]
      ,[AddressLine2]
      ,[City]
      ,[Province]
      ,[PostalCode]
      ,[Latitude]
      ,[Longitude]
      ,REPLACE([Email],'1',RNUM)
      ,[EmailConfirmed]
      ,[PasswordHash]
      ,[SecurityStamp]
      ,REPLACE([PhoneNumber],'1',RNUM)
      ,[PhoneNumberConfirmed]
      ,[TwoFactorEnabled]
      ,[LockoutEndDateUtc]
      ,[LockoutEnabled]
      ,[AccessFailedCount]
	  ,USERNAME +CAST( RNUM AS NVARCHAR(3)) username
from info 
WHERE RNUM != 1 and rnum < 13

insert into   [dbo].[AspNetUserRoles]
select id ,'990fdc43-f7f3-4128-93a4-73c618f55b11'   
--select * 
FROM [HappySitter].[dbo].[AspNetUsers] a	
   where email like 'sitter%' and email not like '%manager%'
   and Username != 'sitter1' and Username != 'sitter'
   order by id



   insert into [dbo].[Schedules]
	   select b.[Id]
      ,[DayOfWeek]
      ,[FromTime]
      ,[ToTime]
      ,[DateLastModified]  from [dbo].[Schedules] a ,[dbo].[AspNetUsers]  b
	  where a.userid ='b13b78ca-12f5-4669-8869-039f5580f011'
	  and b.username like 'Sitter%'
	   and b.id <> 'b13b78ca-12f5-4669-8869-039f5580f011'



--Test Senario

select * from [dbo].[AspNetUsers] where Email ='sitter1@test.com'

select * from [dbo].[AspNetRoles] where id =''

select * from [dbo].[AspNetUserRoles] where userid =''



select * from [dbo].[Schedules] where userid = 'b13b78ca-12f5-4669-8869-039f5580f011'


DECLARE @FromTime varchar(60), @ToTime varchar(60), @Date date
SET @FromTime ='13:00'
SET @ToTime = '14:00'
SET @Date = '20200330'

select CAST(@FromTime AS time)
SELECT * from [dbo].[Schedules]
	                WHERE DayOfWeek = DATEPART(dw,@Date)
		                AND FromTime <= CAST(@FromTime AS Time)
		                AND ToTime >=  CAST(@ToTime AS Time) 

--select DATEPART(dw,@Date)
select *  from [dbo].[Schedules] where FromTime >= @FromTime
	
               select * from [dbo].[AspNetUsers] 
                   where Id in(
                  SELECT UserId from [dbo].[Schedules]
	                WHERE DayOfWeek = DATEPART(dw,@Date)
		                AND FromTime >= @FromTime
		                AND ToTime >= @ToTime 
		                )

       select * from [dbo].[AspNetUsers] where username like 'Sitter%'
	   and id <> 'b13b78ca-12f5-4669-8869-039f5580f011'

	   insert into [dbo].[Schedules]
	   select b.[Id]
      ,[DayOfWeek]
      ,[FromTime]
      ,[ToTime]
      ,[DateLastModified]  from [dbo].[Schedules] a ,[dbo].[AspNetUsers]  b
	  where a.userid ='b13b78ca-12f5-4669-8869-039f5580f011'
	  and b.username like 'Sitter%'
	   and b.id <> 'b13b78ca-12f5-4669-8869-039f5580f011'



	   select *  from [dbo].[Schedules] where userid <>'b13b78ca-12f5-4669-8869-039f5580f011'

	   delete  from [dbo].[Schedules] where userid <>'b13b78ca-12f5-4669-8869-039f5580f011'

SELECT TOP (1000) [Id]
      ,[UserId]
      ,[DayOfWeek]
      ,[FromTime]
      ,[ToTime]
      ,[DateLastModified]
  FROM [HappySitter].[dbo].[Schedules]
  and 


  DECLARE @FromTime varchar(60), @ToTime varchar(60), @Date date
SET @FromTime ='13:00'
SET @ToTime = '14:00'
SET @Date = '20200330'

--select DATEPART(dw,@Date)
select *  from [dbo].[Schedules] where FromTime >= @FromTime
	
               select * from [dbo].[AspNetUsers] 
                   where Id in(
                  SELECT UserId from [dbo].[Schedules]
	                WHERE DayOfWeek = DATEPART(dw,@Date)
		                AND FromTime >= @FromTime
		                AND ToTime >= @ToTime 
		                )