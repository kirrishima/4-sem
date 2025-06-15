create table TR_AUDIT
(
	ID int identity,
	--номер
	stmt varchar(20) -- DML-оператор 
		check (STMT in ('INS', 'DEL', 'UPD')),
	trname varchar(50),
	--имя триггера
	cc varchar(300)
	--комментарий
)
go

create trigger TR_TEACHER_INS on TEACHER after insert
as 
	declare @a1 char(10), @a2  nvarchar(100), @a3 char(1), @a4 char(20), @in nvarchar(300);

	set @a1 = (select [Teacher]
from inserted);
	set @a2 = (select [Teacher_Name]
from inserted);
	set @a3 = (select [Gender]
from inserted);
	set @a4 = (select [Pulpit]
from inserted);
	set @in = @a1 + ',' + @a2 + ', ' + @a3 + ', ' + @a4
	insert into TR_AUDIT
	(stmt, trname, cc)
values
	('INS', 'TR_TEACHER_INS', @in);
	return;


	select *
from TR_AUDIT

insert into TEACHER
values('авы', '', 'м', 'ИСиТ')

select *
from TR_AUDIT

go

create trigger TR_TEACHER_DEL on TEACHER after delete
as 
	declare @a1 char(10), @a2  nvarchar(100), @a3 char(1), @a4 char(20), @in nvarchar(300);
	print 'Операция удаления';
	set @a1 = (select [Teacher]
from deleted);
	set @a2 = (select [Teacher_Name]
from deleted);
	set @a3 = (select [Gender]
from deleted);
	set @a4 = (select [Pulpit]
from deleted);
	set @in = @a1 + ',' + @a2 + ', ' + @a3 + ', ' + @a4
	insert into TR_AUDIT
	(stmt, trname, cc)
values
	('DEL', 'TR_TEACHER_DEL', @in);
	return;


	select *
from TR_AUDIT

delete from TEACHER where TEACHER = 'авы'

select *
from TR_AUDIT

go



-- 3

create trigger TR_TEACHER_UPD on TEACHER after update
as 
	declare @a1 char(10), @a2  nvarchar(100), @a3 char(1), @a4 char(20), @in nvarchar(300);
	print 'Операция обновления';

	set @a1 = (select [Teacher]
from inserted);
	set @a2 = (select [Teacher_Name]
from inserted);
	set @a3 = (select [Gender]
from inserted);
	set @a4 = (select [Pulpit]
from inserted);
	set @in = @a1 + ',' + @a2 + ', ' + @a3 + ', ' + @a4

	set @a1 = (select [Teacher]
from deleted);
	set @a2 = (select [Teacher_Name]
from deleted);
	set @a3 = (select [Gender]
from deleted);
	set @a4 = (select [Pulpit]
from deleted);
	set @in = @a1 + ',' + @a2 + ', ' + @a3 + ', ' + @a4 + @in
	insert into TR_AUDIT
	(stmt, trname, cc)

values
	('UPD', 'TR_TEACHER_UPD', @in);
	return;


	insert into TEACHER
values('авы', '', 'м', 'ИСиТ')

	select *
from TR_AUDIT

update TEACHER set TEACHER = 'авыaa' where TEACHER = 'авы'


select *
from TR_AUDIT


go

-- 4

drop trigger if exists  TR_TEACHER_INS
drop trigger  if exists TR_TEACHER_DEL
drop trigger  if exists TR_TEACHER_UPD

go

create trigger TR_TEACHER on TEACHER after insert, delete, update
as 
	declare @a1 char(10), @a2  nvarchar(100), @a3 char(1), @a4 char(20), @in nvarchar(300);
	declare @ins int = (select count(*)
from inserted),
            @del int = (select count(*)
from deleted); 

if  @ins > 0 and @del = 0  
begin
	print 'Операция вставки';
	set @a1 = (select [Teacher]
	from inserted);
	set @a2 = (select [Teacher_Name]
	from inserted);
	set @a3 = (select [Gender]
	from inserted);
	set @a4 = (select [Pulpit]
	from inserted);
	set @in = @a1 + ',' + @a2 + ', ' + @a3 + ', ' + @a4
	insert into TR_AUDIT
		(stmt, trname, cc)
	values
		('INS', 'TR_TEACHER_INS', @in);
end; 

else	
if @ins = 0 and @del > 0  
begin
	print 'Операция удаления';
	set @a1 = (select [Teacher]
	from deleted);
	set @a2 = (select [Teacher_Name]
	from deleted);
	set @a3 = (select [Gender]
	from deleted);
	set @a4 = (select [Pulpit]
	from deleted);
	set @in = @a1 + ',' + @a2 + ', ' + @a3 + ', ' + @a4
	insert into TR_AUDIT
		(stmt, trname, cc)
	values
		('DEL', 'TR_TEACHER_DEL', @in);
end; 
else	  
if @ins > 0 and @del > 0  
begin
	print 'Операция обновления';

	set @a1 = (select [Teacher]
	from inserted);
	set @a2 = (select [Teacher_Name]
	from inserted);
	set @a3 = (select [Gender]
	from inserted);
	set @a4 = (select [Pulpit]
	from inserted);
	set @in = @a1 + ',' + @a2 + ', ' + @a3 + ', ' + @a4

	set @a1 = (select [Teacher]
	from deleted);
	set @a2 = (select [Teacher_Name]
	from deleted);
	set @a3 = (select [Gender]
	from deleted);
	set @a4 = (select [Pulpit]
	from deleted);
	set @in = @a1 + ',' + @a2 + ', ' + @a3 + ', ' + @a4 + @in
	insert into TR_AUDIT
		(stmt, trname, cc)
	values
		('UPD', 'TR_TEACHER_UPD', @in);
end

return;

delete from   TR_AUDIT
insert into TEACHER
values('ff', '', 'м', 'ИСиТ')
update TEACHER set TEACHER = 'ffff' where TEACHER = 'ff'
delete from TEACHER where TEACHER = 'ffff'

select *
from TR_AUDIT

-- 5

select *
from TR_AUDIT

insert into TEACHER
values(null, '', 'м', 'ИСиТ')

select *
from TR_AUDIT

drop trigger if exists TR_TEACHER_INS
drop trigger  if exists TR_TEACHER_DEL
drop trigger  if exists TR_TEACHER_UPD
drop trigger  if exists TR_TEACHER


-- 6

go

create trigger TR_TEACHER_DEL1 on TEACHER after delete  
as
	declare @in nvarchar(300);
	set @in = 'вызван TR_TEACHER_DEL1'
	insert into TR_AUDIT
	(stmt, trname, cc)
values
	('DEL', 'TR_TEACHER_DEL', @in);
	print 'TR_TEACHER_DEL1';
	return;  
go

create trigger TR_TEACHER_DEL2 on TEACHER after delete  
as
	declare @in nvarchar(300);
	set @in = 'вызван TR_TEACHER_DEL2'
	insert into TR_AUDIT
	(stmt, trname, cc)
values
	('DEL', 'TR_TEACHER_DEL', @in);
	print 'TR_TEACHER_DEL2';
	return;  
go

create trigger TR_TEACHER_DEL3 on TEACHER after delete  
as
	declare @in nvarchar(300);
	set @in = 'вызван TR_TEACHER_DEL3'
	insert into TR_AUDIT
	(stmt, trname, cc)
values
	('DEL', 'TR_TEACHER_DEL', @in);
	print 'TR_TEACHER_DEL3';
	return;  
go

insert into TEACHER
values('ff', '', 'м', 'ИСиТ')

delete from TEACHER where TEACHER = 'ff'



select t.name, e.type_desc
from sys.triggers t join sys.trigger_events e
	on t.object_id = e.object_id
where OBJECT_NAME(t.parent_id) = 'TEACHER' and e.type_desc = 'DELETE'

exec  SP_SETTRIGGERORDER @triggername = 'TR_TEACHER_DEL3', 
@order = 'First', @stmttype = 'DELETE';

exec  SP_SETTRIGGERORDER @triggername = 'TR_TEACHER_DEL2', 
@order = 'Last', @stmttype = 'DELETE';

drop trigger TR_TEACHER_DEL1
drop trigger TR_TEACHER_DEL2
drop trigger TR_TEACHER_DEL3



-- 7

go

create trigger TEACHER_TRAN on TEACHER after insert, delete, update
as 
	rollback
return

go

insert into TEACHER
values('ff', '', 'м', 'ИСиТ')

select *
from TEACHER

go

drop trigger TEACHER_TRAN


-- 8
go

create trigger TEACHER_INSTEADOF on TEACHER INSTEAD of delete
as 
	raiserror('Удаление запрещено',10,1)
return

go

delete from TEACHER
select *
from TEACHER

go

drop trigger if exists TR_TEACHER_INS;
drop trigger if exists TR_TEACHER_DEL;
drop trigger if exists TR_TEACHER_UPD;
drop trigger if exists TR_TEACHER;
drop trigger if exists TR_TEACHER_DEL1;
drop trigger if exists TR_TEACHER_DEL2;
drop trigger if exists TR_TEACHER_DEL3;
drop trigger if exists TEACHER_TRAN;
drop trigger if exists TEACHER_INSTEADOF;




-- 9

go

create  trigger Univer on database 
for DDL_DATABASE_LEVEL_EVENTS 
as   
  declare @t varchar(50) =  EVENTDATA().value('(/EVENT_INSTANCE/EventType)[1]', 'varchar(50)');
  declare @t1 varchar(50) = EVENTDATA().value('(/EVENT_INSTANCE/ObjectName)[1]', 'varchar(50)');
  declare @t2 varchar(50) = EVENTDATA().value('(/EVENT_INSTANCE/ObjectType)[1]', 'varchar(50)'); 
  if @t1 = 'TEACHER' 
  begin
	print 'тип события: '+@t;
	print 'имя объекта: '+@t1;
	print 'тип объекта: '+@t2;
	raiserror( 'операции с таблицей TEACHER запрещены', 11, 1);
	rollback;
end;

go

alter table TEACHER drop column TEACHER_NAME
