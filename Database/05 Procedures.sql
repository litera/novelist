print char(10) + 'Procedures:';

--print 'Creating procedure ProcName'
--go
--	create procedure dbo.ProcName (
--		@Param
--	)
--	as
--	begin
--	end
--	go
--	grant execute on dbo.ProcName to public;
--	go


print 'Creating procedure User_Get'
go
	create procedure dbo.User_Get (
		@Id int = null,
		@Email nvarchar(250) = null
	)
	as
	begin
		-- at least one parameter should be provided
		if (not (@Id is null and @Email is null))
		begin
			select
				Id, Name, Email
			from dbo.[User]
			where
				Id = isnull(@Id, -1) or
				-- ID has precedence over Email when both are provided
				Email = isnull(isnull(cast(@Id as nvarchar), @Email), '');
		end
	end
	go
	grant execute on dbo.User_Get to public;
	go


print 'Creating procedure User_Authenticate'
go
	create procedure dbo.User_Authenticate (
		@Email nvarchar(250),
		@Hash int
	)
	as
	begin
		select Id, Name, Email
		from dbo.[User]
		where
			Email = @Email and
			SimpleHash = @Hash;
	end
	go
	grant execute on dbo.User_Authenticate to public;
	go


print 'Creating procedure User_Create'
go
	create procedure dbo.User_Create (
		@Name nvarchar(100),
		@Email nvarchar(250),
		@Hash int
	)
	as
	begin
		set @Name = dbo.FullTrim(@Name);
		set @Email = lower(dbo.FullTrim(@Email));

		-- Email MUST BE unique
		if not exists(select 1 from dbo.[User] where Email = @Email)
		begin
			insert dbo.[User] (Name, Email, SimpleHash)
			values (@Name, @Email, @Hash)

			-- return newly created record
			declare @id int = scope_identity();
			exec dbo.User_Get @Id = @id;
		end
	end
	go
	grant execute on dbo.User_Create to public;
	go


print 'Creating procedure Post_Get'
go
	create procedure dbo.Post_Get (
		@PostId int
	)
	as
	begin
		select
			-- post
			p.Id, p.Title, p.Content, p.CreatedOn,
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
			p.Id, p.Title, p.Content, p.CreatedOn,
			-- user
			u.Id, u.Name, u.Email
		from dbo.Post p
			join dbo.[User] u
			on u.Id = p.AuthorId
		order by p.IsFeatured desc, p.CreatedOn desc;
	end
	go
	grant execute on dbo.Post_GetAll to public;
	go


print 'Creating procedure Post_Create'
go
	create procedure dbo.Post_Create (
		@AuthorId int,
		@Title nvarchar(200),
		@Content nvarchar(max)
	)
	as
	begin
		set @Title = dbo.FullTrim(@Title);
		set @Content = dbo.FullTrim(@Content);

		insert dbo.Post (Title, Content, AuthorId)
		values (@Title, @Content, @AuthorId);

		declare @postId int = scope_identity();

		-- return newly created post
		exec dbo.Post_Get @PostId = @postId;
	end
	go
	grant execute on dbo.Post_Create to public;
	go