select AUDITORIUM, AUDITORIUM_TYPENAME 
from AUDITORIUM inner join AUDITORIUM_TYPE on AUDITORIUM.AUDITORIUM_TYPE = AUDITORIUM_TYPE.AUDITORIUM_TYPE




select a.AUDITORIUM, t.AUDITORIUM_TYPENAME from AUDITORIUM as a inner join AUDITORIUM_TYPE as t on a.AUDITORIUM_TYPE = t.AUDITORIUM_TYPE
where t.AUDITORIUM_TYPENAME like '%компьютер%';



	
select FACULTY.FACULTY_NAME as Факультет, PULPIT.PULPIT_NAME as Кафедра, GROUPS.PROFESSION as Специальность, SUBJECT.SUBJECT_NAME as Дисциплина, STUDENT.NAME as Имя_Студента,
case when PROGRESS.NOTE = 6 then 'шесть' when PROGRESS.NOTE = 7 then 'семь' when PROGRESS.NOTE = 8 then 'восемь' end as Оценка
from PROGRESS inner join STUDENT on PROGRESS.IDSTUDENT = STUDENT.IDSTUDENT
inner join GROUPS on STUDENT.IDGROUP = GROUPS.IDGROUP
inner join SUBJECT on PROGRESS.SUBJECT = SUBJECT.SUBJECT
inner join PULPIT on SUBJECT.PULPIT = PULPIT.PULPIT
inner join FACULTY on GROUPS.FACULTY = FACULTY.FACULTY
where PROGRESS.NOTE between 6 and 8 order by PROGRESS.NOTE desc;





select PULPIT.PULPIT[Кафедра], isnull(TEACHER_NAME, '***')[Преподаватель] 
from PULPIT left outer join TEACHER on TEACHER.PULPIT = PULPIT.PULPIT




create table TABLE_A (
    ID int primary key,
    DATA_A varchar(50)
);

create table TABLE_B (
    ID int primary key,
    DATA_B varchar(50)
);

insert into TABLE_A (ID, DATA_A) values (1, 'Value A1');
insert into TABLE_A (ID, DATA_A) values (2, 'Value A2');
insert into TABLE_A (ID, DATA_A) values (3, 'Value A3');

insert into TABLE_B (ID, DATA_B) values (2, 'Value B2');
insert into TABLE_B (ID, DATA_B) values (3, 'Value B3');
insert into TABLE_B (ID, DATA_B) values (4, 'Value B4');

select * from TABLE_A a full outer join TABLE_B b on a.ID = b.ID where b.ID is null

select * from TABLE_A a full outer join TABLE_B b on a.ID = b.ID where a.ID is null

select * from TABLE_A a full outer join TABLE_B b on a.ID = b.ID where b.ID is not null and a.ID is not null

  



select AUDITORIUM, AUDITORIUM_TYPENAME from AUDITORIUM a 
cross join AUDITORIUM_TYPE t where a.AUDITORIUM_TYPE = t.AUDITORIUM_TYPE