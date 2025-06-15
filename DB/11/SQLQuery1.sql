-- 1
select SUBJECT
from SUBJECT
where PULPIT like '%»—Ë“%'

declare subjects cursor for 
    select SUBJECT
from SUBJECT
where PULPIT like '%»—Ë“%';

declare @name varchar(100), @log varchar(1000) = '';

open subjects;
fetch subjects into @name;

print 'ƒËÒˆËÔÎËÌ˚ Ì‡ Í‡ÙÂ‰Â »—Ë“';

while @@FETCH_STATUS = 0
begin
    set @log = @log + case when @log = '' then '' else ', ' end + RTRIM(@name);
    fetch subjects into @name;
end;

print @log;

close subjects;
deallocate subjects;
go;

-- 2.1
declare lc cursor LOCAL for
    select SUBJECT
from dbo.SUBJECT
where PULPIT like N'%»—Ë“%';

open lc;
go


declare @name nvarchar(100);
fetch NEXT from lc into @name;
print @name;

-- 2.2
declare gc cursor global for
    select SUBJECT
from dbo.SUBJECT
where PULPIT like N'%»—Ë“%';

open gc;
go

declare @name nvarchar(100);
fetch NEXT from gc into @name;
print @name;

deallocate gc;
go;



-- 3
drop table if exists #Comp;

create table #Comp
(
    SUBJECT nvarchar(50) primary key,
    STATIC_NAME nvarchar(100),
    DYNAMIC_NAME nvarchar(100)
);

drop table if exists subject_cpy;
select *
into subject_cpy
from SUBJECT;


declare csr_s cursor STATIC
for
    select SUBJECT, SUBJECT_NAME
from subject_cpy
where PULPIT like N'%»—Ë“%';

declare csr_d cursor DYNAMIC
for
    select SUBJECT, SUBJECT_NAME
from subject_cpy
where PULPIT like N'%»—Ë“%';

open csr_s;
open csr_d;

update subject_cpy
   set SUBJECT_NAME = N'Œ¡ÕŒ¬À≈ÕŒ'
 where SUBJECT = '¡ƒ';

declare @sub nvarchar(50), @name nvarchar(100);

fetch NEXT from csr_s into @sub, @name;
while @@FETCH_STATUS = 0
begin
    insert into #Comp
        (SUBJECT, STATIC_NAME)
    values
        (@sub, @name);

    fetch NEXT from csr_s into @sub, @name;
end

close csr_s;
deallocate csr_s;


fetch NEXT from csr_d into @sub, @name;
while @@FETCH_STATUS = 0
begin
    update #Comp
       set DYNAMIC_NAME = @name
     where SUBJECT = @sub;

    fetch NEXT from csr_d into @sub, @name;
end

close csr_d;
deallocate csr_d;

select
    SUBJECT,
    STATIC_NAME,
    DYNAMIC_NAME
from #Comp
order by SUBJECT;

drop table if exists #Comp;
drop table if exists subject_cpy;


-- 4

select SUBJECT
from SUBJECT
where PULPIT like N'%»—Ë“%'

declare @subj char(10) = '';
declare cur cursor local scroll for select SUBJECT
from SUBJECT
where PULPIT like N'%»—Ë“%';
open cur;

fetch first from cur into @subj;
print 'first: ' + @subj;

fetch last from cur into @subj;
print 'last: ' + @subj;

fetch absolute 10 from cur into @subj;
print 'absolute: ' + @subj;

fetch relative 2 from cur into @subj;
print 'relative: ' + @subj;

fetch next from cur into @subj;
print 'next: ' + @subj;

fetch prior from cur into @subj;
print 'prior: ' + @subj;



-- 5
drop table if exists subject_cpy;
select *
into subject_cpy
from SUBJECT;

declare @subj char(10);

declare cur cursor local for select SUBJECT
from subject_cpy
for
update;

open cur;

select *
from subject_cpy;

fetch from cur into @subj;

delete subject_cpy where current of cur;

fetch from cur into @subj;

update subject_cpy set SUBJECT = 'aaa' where current of cur;

select *
from subject_cpy;
close cur;
drop table if exists subject_cpy;


-- 6

-- 6.1
select s.NAME, p.SUBJECT, p.NOTE
from PROGRESS p
    join STUDENT s on s.IDSTUDENT = p.IDSTUDENT
    join GROUPS g on g.IDGROUP = s.IDGROUP
where p.NOTE < 4


insert into progress
    (subject, idstudent, pdate, note)
values
    ('¡ƒ', 1001, '2025-05-01', 2),
    ('¡ƒ', 1002, '2025-05-02', 3),
    ('¡ƒ', 1003, '2025-05-03', 1),
    ('¡ƒ', 1004, '2025-05-04', 2);


declare 
    @sb char(10),  
    @is int,      
    @pd date;

declare curdel cursor local
for
select
    p.SUBJECT,
    p.IDSTUDENT,
    p.PDATE
from PROGRESS p
    join STUDENT  s on p.IDSTUDENT = s.IDSTUDENT
    join GROUPS   g on s.IDGROUP   = g.IDGROUP
where p.note < 4;

open curdel;
fetch next from curdel into @sb, @is, @pd;

while @@fetch_status = 0
begin
    delete from PROGRESS
    where  current of curdel;

    fetch next from curdel into @sb, @is, @pd;
end

close curdel;
deallocate curdel;
go;
-- 6.2

declare 
    @sb char(10), 
    @is int,     
    @pd date,
	@ids_for_search int = 1001;

update PROGRESS set NOTE = 7 where IDSTUDENT = @ids_for_search;
select *
from PROGRESS
where IDSTUDENT = @ids_for_search;

declare curdel cursor local
for
select
    p.SUBJECT,
    p.IDSTUDENT,
    p.PDATE
from PROGRESS p
where p.IDSTUDENT = @ids_for_search;

open curdel;
fetch next from curdel into @sb, @is, @pd;

while @@fetch_status = 0
begin
    update PROGRESS set NOTE += 1 
    where  current of curdel;

    fetch next from curdel into @sb, @is, @pd;
end

close curdel;
deallocate curdel;

select *
from PROGRESS
where IDSTUDENT = @ids_for_search;