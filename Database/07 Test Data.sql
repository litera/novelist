set nocount on;

print '';

print 'Adding generic test users'
	insert dbo.[User] (Name, Email, SimpleHash)
	values
		('Robert Koritnik', 'r@k', -198892813); -- password: koji

set nocount off;