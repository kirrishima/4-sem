-- 1
drop table if exists Y;

declare @flag char(1) = 'r',  
        @rowCount int;


set IMPLICIT_TRANSACTIONS on;

create table Y
(
    id int identity(1,1) primary key,
    name varchar(50) not null
);

insert into Y
    (name)
values
    (N'Иван'),
    (N'Мария'),
    (N'Алексей');


select @rowCount = COUNT(*)
from Y;
print 'строк в таблице Y: ' + CAST(@rowCount as VARCHAR(3));


if @flag = 'c'
    commit; 
else
    rollback;

set IMPLICIT_TRANSACTIONS off;

if OBJECT_ID(N'DBO.Y') is not null
    print 'таблица Y есть';
else
    print 'таблицы Y нету';




-- 2

begin try 
	begin tran 
select top 5
    SUBJECT, SUBJECT_NAME
from SUBJECT
order by SUBJECT;
select COUNT(*)
from SUBJECT;

insert into SUBJECT
    (SUBJECT, PULPIT, SUBJECT_NAME)
values
    ('python', 'ИСиТ', 'питон'),
    ('c++', 'ИСиТ', 'cc++'),
    ('c#', 'ИСиТ', 'csharp');
select COUNT(*)
from SUBJECT;

update SUBJECT set SUBJECT_NAME = 'changed' where SUBJECT = 'БД';
select top 5
    SUBJECT, SUBJECT_NAME
from SUBJECT
order by SUBJECT;

drop table SUBJECT;
commit tran;
end try
begin catch
print 'ошибка: ' + case when error_number() = 3726 then 'Попытка удалить таблицу, на которую ссылается вторичный ключ другой таблицы'
else error_message() + '(код ' + cast(error_number() as varchar(5)) + ')' end;
if @@TRANCOUNT > 0 rollback tran ;
end catch;

select COUNT(*)
from SUBJECT;
select SUBJECT, SUBJECT_NAME
from SUBJECT;



-- 3
select *
into #SUBJECTtmp
from SUBJECT;
alter table #SUBJECTtmp 
add constraint SUBJECTtmp_SUBJECT_UNQ unique(SUBJECT) ;

select *
from #SUBJECTtmp
declare @checkpoint varchar(32);
begin try 
	begin tran 
select top 5
    SUBJECT, SUBJECT_NAME
from #SUBJECTtmp
order by SUBJECT;
select COUNT(*)
from #SUBJECTtmp;

insert into #SUBJECTtmp
    (SUBJECT, PULPIT, SUBJECT_NAME)
values
    ('python', 'ИСиТ', 'питон'),
    ('c++', 'ИСиТ', 'cc++'),
    ('c#', 'ИСиТ', 'csharp');
select COUNT(*)
from #SUBJECTtmp;
set @checkpoint = 'p1'; save tran @checkpoint;

update SUBJECT set SUBJECT_NAME = 'changed' where SUBJECT = 'БД';
select top 5
    SUBJECT, SUBJECT_NAME
from #SUBJECTtmp
order by SUBJECT;
set @checkpoint = 'p2'; save tran @checkpoint;

insert into #SUBJECTtmp
    (SUBJECT)
values
    ('БД');

set @checkpoint = 'p3'; save tran @checkpoint;

commit tran;
end try
begin catch
print 'ошибка: ' + case when error_number() = 3726 then 'Попытка удалить таблицу, на которую ссылается вторичный ключ другой таблицы'
else error_message() + '(код ' + cast(error_number() as varchar(5)) + ')' end;

if @@TRANCOUNT > 0 
begin
    print 'откат к контрольной точке ' + @checkpoint;
    rollback tran @checkpoint;
    commit tran;
end;

end catch;

select COUNT(*)
from #SUBJECTtmp;
select SUBJECT, SUBJECT_NAME
from #SUBJECTtmp;
drop table if exists #SUBJECTtmp;

