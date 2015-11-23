print 'Procedures:';

--print 'Creating procedure ProcName'
--go
--create procedure dbo.ProcName (
--	@Param
--)
--as
--begin
--end
--go
--grant execute on dbo.ProcName to public;
--go


print 'Creating procedure Post_Get'
go
create procedure dbo.Post_Get (
	@PostId int
)
as
begin
	select
		-- post
		p.Id, p.Title, p.Details, p.CreatedOn,
		-- user
		u.Id, u.Name, u.Email
	from dbo.Post p
		join dbo.[User] u
		on u.Id = p.AuthorId
	where
		p.Id = @PostId;
end
go
grant execute on dbo.Post_Get to public;
go


print 'Creating procedure Post_GetAll'
go
create procedure dbo.Post_GetAll
as
begin
	select
		-- post
		p.Id, p.Title, p.Details, p.CreatedOn,
		-- user
		u.Id, u.Name, u.Email
	from dbo.Post p
		join dbo.[User] u
		on u.Id = p.AuthorId
	order by p.CreatedOn desc;
end
go
grant execute on dbo.Post_GetAll to public;
go


print 'Creating procedure Post_Create'
go
create procedure dbo.Post_Create (
	@AuthorId int,
	@Title nvarchar(200),
	@Details nvarchar(max)
)
as
begin
	set @Title = dbo.FullTrim(@Title);
	set @Details = dbo.FullTrim(@Details);

	insert dbo.Post (Title, Details, AuthorId)
	values (@Title, @Details, @AuthorId);

	declare @postId int = scope_identity();

	-- return newly created post
	exec @PostId = @postId;
end
go
grant execute on dbo.Post_Create to public;
go