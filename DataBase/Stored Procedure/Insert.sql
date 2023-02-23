CREATE OR ALTER PROCEDURE [dbo].[SP_User_Insert](
@FirstName nvarchar(100)= null,
@LastName nvarchar(100)=null,
@Email nvarchar(50)=null,
@Password nvarchar(50)=null,
@IsActive bit not null,
@IsDeleted bit not null,
@CreatedBy nvarchar(100) = @FirstName,
@LastModifiedBy nvarchar(100) = @FirstName,
@RefreshToken varchar(100) = null,
@Mesg nvarchar(Max)=null,
@UserId nvarchar(100)=null Out
)
As
Begin Try

IF EXISTS (SELECT * FROM userProfileTbl WHERE Email = @Email) 
	BEGIN
	SET @Mesg='User already Existed'
	END
ELSE
	BEGIN
	SELECT @UserId =dbo.GENERATEUNIQUEUSERID()
	INSERT INTO userProfileTbl VALUES(@UserId,@FirstName,@LastName,@Email,@Password,1,0,
	SYSDATETIME(),@CreatedBy,SYSDATETIME(),@LastModifiedBy,@RefreshToken)
	SET @Mesg = 'User Details Inserted'
	END
END TRY

BEGIN CATCH
SET @Mesg = ERROR_MESSAGE()
END CATCH

GO

exec [dbo].[SP_User_Insert] 'SraddhaSrikari','Chaganti','srikari.sraddha@qentelli.com','Siri@1997','Sraddha','Sraddha'