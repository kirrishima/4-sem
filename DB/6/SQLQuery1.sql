-- 1-2
select at.AUDITORIUM_TYPE AS Тип,
max(a.AUDITORIUM_CAPACITY) as Максимальная,
min(a.AUDITORIUM_CAPACITY) as Минимальная,
avg(a.AUDITORIUM_CAPACITY) as Средняя,
sum(a.AUDITORIUM_CAPACITY) as Суммарная,
count(a.AUDITORIUM) as Количество 
from AUDITORIUM a inner join AUDITORIUM_TYPE at on a.AUDITORIUM_TYPE =at.AUDITORIUM_TYPE group  by at.AUDITORIUM_TYPE


-- 3
select * from 
(select case when NOTE between 8 and 10 then '8-10' when NOTE between 6 and 7 then '6-7' else '0-5' end as интервал,
count(*) as число from PROGRESS group by 
case when NOTE between 8 and 10 then '8-10' when NOTE between 6 and 7 then '6-7' else '0-5' end) s
order by интервал desc


-- 4
select  g.FACULTY, g.PROFESSION,
round(avg(cast(p.NOTE as float(4))),2) as среднее from FACULTY f
inner join GROUPS g on g.FACULTY = f.FACULTY
inner join STUDENT s on s.IDGROUP = g.IDGROUP
inner join PROGRESS p on p.IDSTUDENT = s.IDSTUDENT
group by g.FACULTY, g.PROFESSION, g.IDGROUP 
order by среднее desc



--5
select  g.FACULTY, g.PROFESSION, g.IDGROUP,
round(avg(cast(p.NOTE as float(4))),2) as среднее from FACULTY f
inner join GROUPS g on g.FACULTY = f.FACULTY
inner join STUDENT s on s.IDGROUP = g.IDGROUP
inner join PROGRESS p on p.IDSTUDENT = s.IDSTUDENT where p.SUBJECT like '%БД%' or p.SUBJECT like '%ОАиП%'
group by g.FACULTY, g.PROFESSION, g.IDGROUP 
order by среднее desc



-- 6
select  g.FACULTY, g.PROFESSION,
round(avg(cast(p.NOTE as float(4))),2) as среднее from FACULTY f
inner join GROUPS g on g.FACULTY = f.FACULTY
inner join STUDENT s on s.IDGROUP = g.IDGROUP
inner join PROGRESS p on p.IDSTUDENT = s.IDSTUDENT where f.FACULTY like '%ТОВ%'
group by g.FACULTY, g.PROFESSION 
order by среднее desc


--  7
select p.SUBJECT as предмет, count(p.NOTE) as число from PROGRESS p group by p.SUBJECT , NOTE having p.NOTE = 8 or p.note = 9




