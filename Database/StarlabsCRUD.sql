create database StarlabsCRUD
use StarlabsCRUD

create table Person(
	PersonId int identity(1,1) Primary key,
	Name varchar(100),
	Surname varchar(100),
	Age int
)
select * from Person
insert into Person values('Arber', 'Ejupi', 22)
delete from Person where Age = '22'