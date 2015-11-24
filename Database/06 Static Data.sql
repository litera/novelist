set nocount on;

/* Recreating data => drop Backup_x tables */
print char(10) + 'Static data';

declare @sql varchar(max) = '';

select @sql = @sql + 'print ''Dropping backup table dbo.[' + o.name + ']'';drop table dbo.[' + o.name + '];'
from sys.objects o
where
	o.type = 'U' and
	left(o.name, 7) = 'Backup_'
order by o.create_date asc;

exec(@sql);

/* Add static predefined data */

print 'Adding generic users'
	insert dbo.[User] (Name, Email, SimpleHash)
	values
		('Administrator', 'admin@novelist.com', -871874097),	-- password: admin (1156371652 on AppHb)
		('Demonstrator', 'demo@novelist.com', -111691625);		-- password: demo (1209474165 in AppHb)

set nocount off;