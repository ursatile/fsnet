PRINT @@VERSION
GO
CREATE DATABASE [tikitapp] COLLATE Latin1_General_CI_AI
GO
CREATE LOGIN [tikitapp_user] WITH 
	PASSWORD=N'tikitapp_password', 
	DEFAULT_DATABASE=[tikitapp], 
	CHECK_EXPIRATION=OFF, 
	CHECK_POLICY=OFF
GO
USE [tikitapp]
GO
PRINT 'Adding user [tikitapp_user] to database [tikitapp]'
CREATE USER [tikitapp_user] FOR LOGIN [tikitapp_user]
PRINT 'Done.'
GO
PRINT 'Adding user [tikitapp_user] to role [db_owner] in [tikitapp] database'
ALTER ROLE [db_owner] ADD MEMBER [tikitapp_user]
PRINT 'Done'
GO