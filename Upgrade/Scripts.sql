
INSERT INTO [Car]
           ([CustomerId]
           ,[Make]
           ,[Battery]
           ,[PurchaseDate]
           ,[Guaranty]
           ,[LifeExpectancy]
           ,[Comments]
           ,[ReplaceDate]
           )
select
           Id
           ,car
           ,battery
           ,PurchaseDate
           ,Guaranty
           ,LifeExpectancy
           ,Comments
           ,ReplaceDate
           
from customer