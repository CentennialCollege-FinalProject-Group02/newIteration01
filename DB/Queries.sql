
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