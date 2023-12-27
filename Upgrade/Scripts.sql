
INSERT INTO [Car]
           ([CustomerId]
           ,[Make]
           ,[Battery]
           ,[PurchaseDate]
           ,[Guaranty]
           ,[LifeExpectancy]
           ,[Comments]
           ,[ReplaceDate]
           ,[StopNotify]
           ,isActive)
select
           Id
           ,car
           ,battery
           ,PurchaseDate
           ,Guaranty
           ,LifeExpectancy
           ,Comments
           ,ReplaceDate
           ,StopNotify
           ,0           
from customer