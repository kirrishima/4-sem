
drop view if exists Преподаватель;
go
create view Преподаватель
as
    select TEACHER[Код], TEACHER_NAME[имя преподавателя], GENDER[пол], PULPIT[код кафедры]
    from TEACHER
go

select *
from Преподаватель



drop view if exists [Количество_кафедр];
go
create view [Количество_кафедр]
as
    select p.FACULTY as факультет, COUNT(*) as число
    from FACULTY f join PULPIT p on f.FACULTY = p.FACULTY
    group by p.FACULTY

select *
from [Количество_кафедр]



drop view if exists Аудитории;
go
drop view Аудитории
create view [Аудитории]
as
    select a.AUDITORIUM as код, a.AUDITORIUM_TYPE[наименование аудитории]
    from AUDITORIUM a
    where a.AUDITORIUM_TYPE like '%лк%'

select *
from Аудитории;

insert into Аудитории
    (код, [наименование аудитории])
values
    ('A101АА', 'лк');
select *
from Аудитории;

update Аудитории
set [наименование аудитории] = 'ЛК-К'
where код = 'A101АА';
select *
from Аудитории;

delete from Аудитории
where код = 'A101АА';
select *
from Аудитории;


drop view if exists Лекционные_аудитории;
go

create view [Лекционные_аудитории]
as
    select a.AUDITORIUM as код, a.AUDITORIUM_TYPE[наименование аудитории]
    from AUDITORIUM a
    where a.AUDITORIUM_TYPE like '%лк%' with check option

select *
from Лекционные_аудитории;

insert into Лекционные_аудитории
    (код, [наименование аудитории])
values
    ('A101АА', 'лк');
select *
from Лекционные_аудитории;

update Лекционные_аудитории
set [наименование аудитории] = 'ЛК-К'
where код = 'A101АА';
select *
from Лекционные_аудитории;

update Лекционные_аудитории
set [наименование аудитории] = 'ЛБ-СК'
where код = 'A101АА';
select *
from Лекционные_аудитории;

select *
from Лекционные_аудитории;



drop view if exists Дисциплины;
go
create view [Дисциплины]
as
    select top 10
        SUBJECT[код], SUBJECT_NAME[наименование дисциплины], PULPIT[код кафедры]
    from SUBJECT
    order by код

select *
from Дисциплины


drop view if exists [Количество_кафедр];
go
create view [Количество_кафедр]
with
    SCHEMABINDING
as
    select p.FACULTY as факультет, COUNT(*) as число
    from dbo.FACULTY f join dbo.PULPIT p on f.FACULTY = p.FACULTY
    group by p.FACULTY


alter table dbo.FACULTY
drop column FACULTY;
