drop table UserInCategory;
drop table UserInRole;
drop table SearchAgents;
drop table VacancyInUser;
drop table Users;
drop table UserDetailsInCategory;
drop table UserDetails;
drop table Roles;
drop table Cities;
drop table JobExperience;
drop table VacancyInComment;
drop table VacancyInCategory;
drop table Vacancy;
drop table Education;
drop table CompanyDetails;
drop table Categories;
drop table Comment;

create table CompanyDetails
(
	[Id] int primary key identity not null,
	[Name] nvarchar(32) not null,
	[TypeOfCompany] int not null, --Прямой работодатель, Кадровое агентство
	[WebSite] nvarchar(32)
)
go

create table Categories
(
	[Id] int primary key identity not null,
	[Name] nvarchar(32) not null
)
go


create table Education
(
	[Id] int primary key identity not null,
	[Level] nvarchar(32) not null,
	[Place] nvarchar(32) not null,
	[Speciality] nvarchar(32) not null
)
go


create table JobExperience
(
	[Id] int primary key identity,
	[Company] nvarchar(64) not null,
	[City] nvarchar(64) not null,
	[Post] nvarchar(64) not null,
	[Start] date not null,
	[End] date not null,
	[Desc] nvarchar(max)
)
go

create table UserDetails 
(
	[Id] int primary key identity not null,
	[Header] nvarchar(64) not null,
	[Salary] decimal not null,
	[JobExperienceId] int,
	[EducationId] int,
	[Description] nvarchar(max) not null,
	foreign key(JobExperienceId) references JobExperience(Id) on delete cascade,
	foreign key(EducationId) references Education(Id) on delete cascade
)
go

create table UserDetailsInCategory
(
	[Id] int primary key identity not null,
	[UserDetailsId] int not null,
	[CategoryId] int not null,
	foreign key(UserDetailsId) references UserDetails(Id),
	foreign key(CategoryId) references Categories(Id)
)
go

create table Roles 
(
	[Id] int primary key identity not null,
	[Name] nvarchar(32) not null
)
go
create table Cities 
(
	[Id] int primary key identity,
	[Name] nvarchar(64) not null
)
go

create table Users
(
	[Id] int primary key identity not null,
	[Email] nvarchar(32) unique not null,
	[Password] nvarchar(32) not null,
	[Birthday] date not null,
	[Name] nvarchar(32) not null,
	[Surname] nvarchar(32) not null,
	[FathersName] nvarchar(32) not null,
	[Phone] nvarchar(32) not null,
	[CityId] int not null,
    [CompanyDetails] int,
    [UserDetails] int, 
	FOREIGN KEY (CityId) REFERENCES Cities(Id) on delete cascade,
    FOREIGN KEY (CompanyDetails) REFERENCES CompanyDetails(Id) on delete cascade,
    FOREIGN KEY (UserDetails) REFERENCES UserDetails(Id) on delete cascade
)
go



create table UserInRole
(
	[Id] int primary key identity not null,
	[UserId] int not null,
	[RoleId] int not null,
	FOREIGN KEY (UserId) REFERENCES Users(Id) on delete cascade,
	FOREIGN KEY (RoleId) REFERENCES Roles(Id) on delete cascade
)
go

create table UserInCategory
(
	[Id] int primary key identity not null,
	[UserId] int not null,
	[CategoryId] int not null,
	FOREIGN KEY (UserId) REFERENCES Users(Id) on delete cascade,
	FOREIGN KEY (CategoryId) REFERENCES Categories(Id) on delete cascade
)
go


create table Vacancy
(
	[Id] int primary key identity not null,
	[ProfessionName] nvarchar(64) not null,
	[MinSalary] decimal not null,
	[MaxSalary] decimal not null,
	[CompanyId] int not null,
	foreign key(CompanyId) References CompanyDetails(Id) on delete cascade,
	[City] nvarchar(max) not null,
	[EmployerName] nvarchar(max) not null,
	[EmployerPhone] nvarchar(max) not null,
	[Requirments] nvarchar(max) not null,
	[TypeOfEmployment] nvarchar(max) not null,
	[Description] nvarchar(max) not null,
	[IsSolved] bit not null DEFAULT 0,
	[CreationTime] date
)
go

create table Comment
(
	[Id] int primary key identity not null,
	[UserId] int not null,
	[VacancyId] int not null,
	[Date] date not null,
	[Content] nvarchar(max) not null
)
go

create table VacancyInCategory
(
	[Id] int primary key identity,
	[VacancyId] int not null,
	[CategoryId] int not null,
	foreign key(VacancyId) references Vacancy(Id)  on delete cascade,
	foreign key(CategoryId) references Categories(Id)  on delete cascade
)
go

create table VacancyInComment
(
	[Id] int primary key identity,
	[VacancyId] int not null,
	[CommentId] int not null,
	foreign key(VacancyId) references Vacancy(Id)  on delete cascade,
	foreign key(CommentId) references Comment(Id)  on delete cascade
)
go

create table VacancyInUser
(
	[Id] int primary key identity,
	[VacancyId] int not null,
	[UserId] int not null,
	foreign key(VacancyId) references Vacancy(Id)  on delete cascade,
	foreign key(UserId) references Users(Id)
)
go

create table SearchAgents
(
	[Id] int primary key identity,
	[UserId] int not null,
	[Request] nvarchar(max) not null,
	foreign key(UserId) references Users(Id) on delete cascade
)
go

create table SearchResults
(
[Id] int primary key identity,
[AgentId] int not null,
[VacancyId] int not null,
foreign key(AgentId) references SearchAgents(Id) on delete cascade,
foreign key(VacancyId) references Vacancy(Id) 
)
go 

create table SearchAgentsEmployer
(
[Id] int primary key identity,
[UserId] int not null,
[Request] nvarchar(max) not null,
foreign key(UserId) references Users(Id) on delete cascade
)
go

create table SearchResultsEmployer
(
[Id] int primary key identity,
[AgentId] int not null,
[UserId] int not null,
foreign key(AgentId) references SearchAgentsEmployer(Id) on delete cascade,
foreign key(UserId) references Users(Id) 
)
go

USE [WorkAtUa]
GO

insert into Roles(Name)
values('Admin')
insert into Roles(Name)
values('Employer')
insert into Roles(Name)
values('Employee')

insert into Categories(Name)
values('Managment')
insert into Categories(Name)
values('IT')
insert into Categories(Name)
values('Architecture')
insert into Categories(Name)
values('Building')
insert into Categories(Name)
values('Marketing')
insert into Categories(Name)
values('Learning')
insert into Categories(Name)
values('Economics')
insert into Categories(Name)
values('Administration')

insert into [dbo].[UserInRole](RoleId, UserId)
values(1, 10)

--ALTER TABLE Vacancy
--ADD CreationDate date

--update Vacancy
--set CreationDate = GETDATE()
--where Id > 0


insert into Cities(Name)
values('Kyiv')
insert into Cities(Name)
values('Zaporozhye')
insert into Cities(Name)
values('Lugansk')
insert into Cities(Name)
values('Kherson')
insert into Cities(Name)
values('Zhytomyr')
insert into Cities(Name)
values('Kharkiv')
insert into Cities(Name)
values('Lviv')
insert into Cities(Name)
values('Makiyivka')
insert into Cities(Name)
values('Poltava')
insert into Cities(Name)
values('Odessa')
insert into Cities(Name)
values('Krivoy Rog')
insert into Cities(Name)
values('Vinnitsa')
insert into Cities(Name)
values('Chernihiv')
insert into Cities(Name)
values('Khmelnitsky')
insert into Cities(Name)
values('Dnepropetrovsk')
insert into Cities(Name)
values('Nikolaev')
insert into Cities(Name)
values('Simferopol')
insert into Cities(Name)
values('Cherkasy')
insert into Cities(Name)
values('Chernivtsi')
insert into Cities(Name)
values('Donetsk')
insert into Cities(Name)
values('Mariupol')
insert into Cities(Name)
values('Sevastopol')
insert into Cities(Name)
values('Gorlivka')
insert into Cities(Name)
values('Rivne')


INSERT INTO [dbo].[Users]
           ([Email]
           ,[Password]
           ,[Birthday]
           ,[Name]
           ,[Surname]
           ,[FathersName]
           ,[Phone]
		   ,[CityId]
           ,[CompanyDetails]
           ,[UserDetails])
     VALUES
           ('root3@gmail.com'
           ,'123'
           ,CONVERT(DATE, '24.11.97', 4)
           ,'Vova'
           ,'Novak'
           ,'Yuriyovuch'
           ,'0508702324'
		   , 1
           ,null
           ,null)
GO


insert into UserInRole(UserId, RoleId)
values(1, 3)

insert into UserInCategory(UserId, CategoryId)
values(1, 1)

insert into UserInCategory(UserId, CategoryId)
values(1, 2)