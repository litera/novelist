-- Restore backup data

set nocount on;

print '';
declare @sql nvarchar(max) = '';

print 'Checking data compatibility between backup and new schema.';

--============================
-- Backup schema table columns
------------------------------
	select
		substring(o.name, 8, 100) as TableName,
		c.name as ColumnName
	into #OldColumns
	from sys.objects o
		join sys.columns c
		on c.object_id = o.object_id
	where
		o.type = 'U' and
		left(o.name, 7) = 'Backup_';

--=========================
-- New schema table columns
---------------------------
	select
		o.name as TableName,
		c.name as ColumnName,
		case when c.is_nullable = 0 and c.is_computed = 0 and d.object_id is null then 1 else 0 end as IsRequired -- SQL Server < 2012
		-- iif(c.is_nullable = 0 and c.is_computed = 0 and d.object_id is null, 1, 0) as IsRequired -- SQL Server >= 2012
	into #NewColumns
	from sys.objects o
		join sys.columns c
			left join sys.default_constraints d
			on d.object_id = c.default_object_id
		on c.object_id = o.object_id
		left join sys.extended_properties ex
		on ex.major_id = o.object_id
	where
		o.type = 'U' and
		o.is_ms_shipped = 0 and
		left(o.name, 7) != 'Backup_' and
		ex.major_id is null;

--=====================
-- Backup schema tables
-----------------------
	select
		oc.TableName,
		stuff((
			select ', ' + cat.ColumnName
			from #OldColumns cat
			where cat.TableName = oc.TableName
			order by cat.ColumnName
			for xml path('')
		), 1, 2, '') as Columns
	into #OldTables
	from #OldColumns oc
	group by oc.TableName;

--==================
-- New schema tables
--------------------
	select
		nc.TableName,
		stuff((
			select ', ' + cat.ColumnName
			from #NewColumns cat
			where cat.TableName = nc.TableName
			order by cat.ColumnName
			for xml path('')
		), 1, 2, '') as Columns
	into #NewTables
	from #NewColumns nc
	group by nc.TableName

--====================================
-- Check table column name differences
--------------------------------------
	-- only check those tables that are backed up
	select @sql = @sql + 'print ''WARNING: Table schema mismatch in dbo.[' + nt.TableName + ']'';print ''         OLD columns: ' + ot.Columns + ''';print ''         NEW columns: ' + nt.Columns + ''';'
	from #NewTables nt
		join #OldTables ot
		on (ot.TableName = nt.TableName)
	where nt.Columns != ot.Columns;

--====================================
-- Check for removed tables (renamed?)
--------------------------------------
	select @sql = @sql + 'print ''WARNING: Table dbo.[' + ot.TableName + '] has been renamed or removed from schema.'';'
	from #OldTables ot
		left join #NewTables nt
		on (nt.TableName = ot.TableName)
	where nt.TableName is null;

	if (@sql = '')
		print 'Database allows frictionless data restore';
	else
	begin
		exec(@sql);
		print '';
		print '---------------------------------------------------------------------';
		print 'Manually investigate WARNINGS related to database schema differences!';
		print '---------------------------------------------------------------------';
	end

--==============================
-- Prepare statements for errors
--------------------------------
set @sql = '';

--=================================
-- Check required columns in backup
-----------------------------------
	select @sql = @sql + 'print ''ERROR: Column dbo.[' + nc.TableName + '].[' + nc.ColumnName + '] is required but missing in backup table.'';'
	from #NewColumns nc
		-- only check against backed-up tables; new tables' requirements don't matter;
		join #OldTables ot
		on (ot.TableName = nc.TableName)
		left join #OldColumns oc
		on (oc.TableName = nc.TableName and oc.ColumnName = nc.ColumnName)
	where
		nc.IsRequired = 1 and
		oc.ColumnName is null;

	if (@@rowcount > 0)
	begin
		print '';
		exec(@sql);
		print '';
		raiserror('Backup data mismatch. Automated data restore is terminated.', 10, 1);
	end

print '';
print 'Restoring data...';
print '';

--=============
-- Data restore
---------------
	select @sql = @sql + 'print ''Restoring data for table dbo.[' + o.name + ']'';begin try set identity_insert dbo.[' + o.name + '] on end try begin catch end catch;insert dbo.[' + o.name + '] (' + nt.Columns + ') select ' + nt.Columns + ' from dbo.[Backup_' + nt.TableName + '];begin try set identity_insert dbo.[' + o.name + '] off end try begin catch end catch;'
	from sys.objects o
		join #NewTables nt
			join #OldTables ot
			on (ot.TableName = nt.TableName)
		on (nt.TableName = o.name)
	where o.type = 'U'
	order by o.create_date asc;

exec(@sql);

--========================
-- Temporary table cleanup
--------------------------
drop table #OldTables;
drop table #OldColumns;
drop table #NewTables;
drop table #NewColumns;

print '';
print 'Data restore completed.';
print '';

set nocount off;
