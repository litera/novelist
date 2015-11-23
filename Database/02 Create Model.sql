print char(10) + 'Create Model:';

print 'Creating table User'
	create table dbo.[User] (
		Id int not null
			identity
			primary key,
		Name nvarchar(100) not null,
		Email nvarchar(250) not null
			unique,
		SimpleHash int not null,
		CreatedOn datetime2 not null
			default sysdatetime()
	)
	go

print 'Creating table Post';
	create table dbo.Post (
		Id int not null
			identity
			primary key,
		Title nvarchar(200) not null,
		Content nvarchar(max) not null,
		AuthorId int not null
			references dbo.[User](Id),
		CreatedOn datetime2 not null
			default sysdatetime()
	)
	go
