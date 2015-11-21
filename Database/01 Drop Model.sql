-- Drop all user defined objects in reverse order they were created

print 'Droping model:';
declare @sql varchar(max) = '';

with types (type, name) as
(
	select 'FN', 'function' union all	-- scalar function
	select 'IF', 'function' union all	-- inline table function
	select 'TF', 'function' union all	-- table function
	select 'P', 'procedure' union all	-- stored procedure
	select 'TR', 'trigger' union all	-- SQL DML trigger
	select 'U', 'table' union all		-- user table
	select 'V', 'view' union all		-- view
	select 'TT', 'type'					-- table type
)
select @sql = @sql + 'print ''Dropping ' + t.name + ' dbo.[' + coalesce(tt.name, o.name) + ']'';drop ' + t.name + ' dbo.[' + coalesce(tt.name, o.name) + '];'
from sys.objects o
	join types t
	on (t.type = o.type)
	left join sys.table_types tt
	on (tt.type_table_object_id = o.object_id)
	left join sys.extended_properties ex
	on (ex.major_id = o.object_id)
where
	left(o.name, 7) != 'Backup_' and
	(o.is_ms_shipped = 0 or tt.is_user_defined = 1) and
	-- exclude "system" objects (diagramming objects have specific extended properties)
	ex.major_id is null
order by o.create_date desc

-- drop all
exec (@sql);
