-- Drop any existing Backup tables
print 'Backing up';

set nocount on;

declare @sql varchar(max) = '';

select @sql = @sql + 'print ''Dropping backup table dbo.[' + o.name + ']'';drop table dbo.[' + o.name + '];'
from sys.objects o
where o.type = 'U' and left(o.name, 7) = 'Backup_'
order by o.create_date asc;

if (@sql = '')
	print 'Backup missing.';
else
	exec(@sql);

-- Backup all existing data into Backup tables

set @sql = '';

select @sql = @sql + 'print ''Backing up table dbo.[' + o.name + ']'';select * into dbo.[Backup_' + o.name + '] from dbo.[' + o.name + '];'
from sys.objects o
	left join sys.extended_properties ex
	on ex.major_id = o.object_id
where
	o.type = 'U' and
	o.is_ms_shipped = 0 and
	left(o.name, 7) != 'Backup_' and
	-- exclude "system" tables (diagramming tables have specific extended properties)
	ex.major_id is null
order by o.create_date asc;

exec(@sql);

set nocount off;
