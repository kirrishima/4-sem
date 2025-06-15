
-- 1
select p.SUBJECT, g.PROFESSION, f.FACULTY, avg(p.NOTE)[среднее]
from FACULTY f inner join GROUPS g on f.FACULTY = g.FACULTY
    inner join STUDENT s on s.IDGROUP = g.IDGROUP
    inner join PROGRESS p on s.IDSTUDENT = p.IDSTUDENT
where f.FACULTY = 'ТОВ'
group by p.SUBJECT, g.PROFESSION, f.FACULTY


select p.SUBJECT, g.PROFESSION, f.FACULTY, avg(p.NOTE)[среднее]
from FACULTY f inner join GROUPS g on f.FACULTY = g.FACULTY
    inner join STUDENT s on s.IDGROUP = g.IDGROUP
    inner join PROGRESS p on s.IDSTUDENT = p.IDSTUDENT
where f.FACULTY = 'ТОВ'
group by rollup (p.SUBJECT, g.PROFESSION, f.FACULTY)




-- 2
select p.SUBJECT, g.PROFESSION, f.FACULTY, avg(p.NOTE)[среднее]
from FACULTY f inner join GROUPS g on f.FACULTY = g.FACULTY
    inner join STUDENT s on s.IDGROUP = g.IDGROUP
    inner join PROGRESS p on s.IDSTUDENT = p.IDSTUDENT
where f.FACULTY = 'ИТ'
group by cube (p.SUBJECT, g.PROFESSION, f.FACULTY)



-- 3 
    select p.SUBJECT, g.PROFESSION, avg(p.note)[Средняя оценка]
    from FACULTY as f inner join GROUPS as g on f.FACULTY = g.FACULTY
        inner join STUDENT as s on s.IDGROUP = g.IDGROUP
        inner join PROGRESS as p on s.IDSTUDENT = p.IDSTUDENT
    where f.FACULTY = 'ТОВ'
    group by p.SUBJECT, g.PROFESSION
union
    select p.SUBJECT, g.PROFESSION, avg(p.note)[Средняя оценка]
    from FACULTY as f inner join GROUPS as g on f.FACULTY = g.FACULTY
        inner join STUDENT as s on s.IDGROUP = g.IDGROUP
        inner join PROGRESS as p on s.IDSTUDENT = p.IDSTUDENT
    where f.FACULTY = 'ХТиТ'
    group by p.SUBJECT, g.PROFESSION


    select p.SUBJECT, g.PROFESSION, avg(p.note)[Средняя оценка]
    from FACULTY as f inner join GROUPS as g on f.FACULTY = g.FACULTY
        inner join STUDENT as s on s.IDGROUP = g.IDGROUP
        inner join PROGRESS as p on s.IDSTUDENT = p.IDSTUDENT
    where f.FACULTY = 'ТОВ'
    group by p.SUBJECT, g.PROFESSION
union all
    select p.SUBJECT, g.PROFESSION, avg(p.note)[Средняя оценка]
    from FACULTY as f inner join GROUPS as g on f.FACULTY = g.FACULTY
        inner join STUDENT as s on s.IDGROUP = g.IDGROUP
        inner join PROGRESS as p on s.IDSTUDENT = p.IDSTUDENT
    where f.FACULTY = 'ХТиТ'
    group by p.SUBJECT, g.PROFESSION




-- 4
    select p.SUBJECT, g.PROFESSION, avg(p.note)[Средняя оценка]
    from FACULTY as f inner join GROUPS as g on f.FACULTY = g.FACULTY
        inner join STUDENT as s on s.IDGROUP = g.IDGROUP
        inner join PROGRESS as p on s.IDSTUDENT = p.IDSTUDENT
    where f.FACULTY = 'ТОВ'
    group by p.SUBJECT, g.PROFESSION
intersect
    select p.SUBJECT, g.PROFESSION, avg(p.note)[Средняя оценка]
    from FACULTY as f inner join GROUPS as g on f.FACULTY = g.FACULTY
        inner join STUDENT as s on s.IDGROUP = g.IDGROUP
        inner join PROGRESS as p on s.IDSTUDENT = p.IDSTUDENT
    where f.FACULTY = 'ХТиТ'
    group by p.SUBJECT, g.PROFESSION




-- 5
    select p.SUBJECT, g.PROFESSION, avg(p.note)[Средняя оценка]
    from FACULTY as f inner join GROUPS as g on f.FACULTY = g.FACULTY
        inner join STUDENT as s on s.IDGROUP = g.IDGROUP
        inner join PROGRESS as p on s.IDSTUDENT = p.IDSTUDENT
    where f.FACULTY = 'ТОВ'
    group by p.SUBJECT, g.PROFESSION
except
    select p.SUBJECT, g.PROFESSION, avg(p.note)[Средняя оценка]
    from FACULTY as f inner join GROUPS as g on f.FACULTY = g.FACULTY
        inner join STUDENT as s on s.IDGROUP = g.IDGROUP
        inner join PROGRESS as p on s.IDSTUDENT = p.IDSTUDENT
    where f.FACULTY = 'ХТиТ'
    group by p.SUBJECT, g.PROFESSION
