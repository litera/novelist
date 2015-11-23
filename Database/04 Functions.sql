print char(10) + 'Functions:';

--print 'Creating function FuncName'
--go
--	create function dbo.FuncName (
--		@Param TYPE
--	)
--	returns TYPE
--	as
--	begin
--		set @Param = ;
--		return @Param;
--	end
--	go
--	grant execute on dbo.FuncName to public;
--	go


print 'Creating function FullTrim'
go
	create function dbo.FullTrim (
		@Value nvarchar(max)
	)
	returns nvarchar(max)
	as
	begin
		set @Value = replace(replace(replace(ltrim(rtrim(@Value)), '  ', ' ' + char(7)), char(7) + ' ', ''), char(7), '');
		return @Value;
	end
	go
	grant execute on dbo.FullTrim to public;
	go