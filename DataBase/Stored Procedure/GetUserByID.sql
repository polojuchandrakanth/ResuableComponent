CREATE PROCEDURE [dbo].[SP_User_GetUserByID](
@UserId nvarchar(100) null)
AS
BEGIN

IF EXISTS (SELECT * FROM userProfileTbl  Where UserId = @UserId)

END
