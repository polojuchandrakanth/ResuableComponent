CREATE OR ALTER PROCEDURE [dbo].[SP_User_Update](
@UserId nvarchar(100)=null,
@FirstName nvarchar(100)= null,
@LastName nvarchar(100)=null,
@Email nvarchar(50)=null,
@LastModifiedBy nvarchar(100) = @FirstName,
@Mesg nvarchar(Max)=null
)

AS
BEGIN TRY

UPDATE userProfileTbl 
SET 
FirstName = @FirstName,
LastName = @LastName,
Email = @Email,
LastModifiedBy = @LastModifiedBy,
LastModifiedOn = SYSDATETIME()

WHERE UserId = @UserId
SET @Mesg = 'User Details Updated Successfully'

END TRY
BEGIN CATCH
SET @Mesg = ERROR_MESSAGE()
END CATCH
