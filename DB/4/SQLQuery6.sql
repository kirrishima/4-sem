select * from SUBJECT s join PULPIT p on s.PULPIT = p.PULPIT 
join FACULTY f on p.FACULTY = f.FACULTY where f.FACULTY_NAME like '%Факультет информационных технологий%'