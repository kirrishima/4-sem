select p.PULPIT_NAME, f.FACULTY_NAME from PULPIT p , FACULTY f where f.FACULTY = p.FACULTY and f.FACULTY in 
(select pr.FACULTY from PROFESSION pr where pr.PROFESSION_NAME like '%технология%' 
or pr.PROFESSION_NAME like '%технологии%')



select p.PULPIT_NAME, f.FACULTY_NAME from PULPIT p join FACULTY f on f.FACULTY = p.FACULTY where f.FACULTY in 
(select pr.FACULTY from PROFESSION pr where pr.PROFESSION_NAME like '%технология%' 
or pr.PROFESSION_NAME like '%технологии%')



select distinct p.PULPIT_NAME, f.FACULTY_NAME from 
PULPIT p join FACULTY f on f.FACULTY = p.FACULTY
 join PROFESSION pr on f.FACULTY = pr.FACULTY
where PROFESSION_NAME like '%технология%' or PROFESSION_NAME like '%технологии%'


select a.AUDITORIUM_NAME, a.AUDITORIUM_TYPE, a.AUDITORIUM_CAPACITY from AUDITORIUM a
where a.AUDITORIUM = (select top 1 aa.AUDITORIUM from AUDITORIUM aa where aa.AUDITORIUM_TYPE = a.AUDITORIUM_TYPE
order by aa.AUDITORIUM_CAPACITY desc)
order by a.AUDITORIUM_CAPACITY desc;



 select f.FACULTY_NAME from FACULTY f where not exists (select * from PULPIT p where p.FACULTY = f.FACULTY)



 select * from 
(select AVG(p1.NOTE) as ОАиП from PROGRESS p1 where p1.SUBJECT like '%ОАиП%') r1,
(select AVG(p2.NOTE) as БД from PROGRESS p2 where p2.SUBJECT like '%БД%') r2,
(select AVG(p3.NOTE) as СУБД from PROGRESS p3 where p3.SUBJECT like '%СУБД%') r3 

-- select * from 
--(select p1.SUBJECT, AVG(p1.NOTE) as noteavg from PROGRESS p1 where p1.SUBJECT like '%ОАиП%' group by p1.SUBJECT) r1,
--(select p2.SUBJECT, AVG(p2.NOTE) as noteavg from PROGRESS p2 where p2.SUBJECT like '%БД%' group by p2.SUBJECT) r2,
--(select p3.SUBJECT, AVG(p3.NOTE) as noteavg from PROGRESS p3 where p3.SUBJECT like '%СУБД%' group by p3.SUBJECT) r3 



select p.IDSTUDENT, p.SUBJECT, p.NOTE from PROGRESS p where p.NOTE > all(select AVG(p2.NOTE) from PROGRESS p2)



select * from TEACHER t where t.PULPIT = any(select t.PULPIT from TEACHER t where t.PULPIT like '%иси%')



