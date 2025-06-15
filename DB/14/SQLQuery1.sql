
-- 1

create function COUNT_STUDENTS(@faculty varchar(20)) returns int
as begin
    declare @rc int = 0
    set @rc = (select count(*)
    from FACULTY f
        join GROUPS g on f.FACULTY = g.FACULTY
        join STUDENT s on g.IDGROUP = s.IDGROUP
    where f.FACULTY = @faculty)
    return @rc
end;
go

declare @f int = dbo.COUNT_STUDENTS('ИТ')
print 'количество: ' + cast(@f as varchar(4));
go

alter function COUNT_STUDENTS(@faculty varchar(20) = null, @prof varchar(20) = null) returns int
as begin
    declare @rc int = 0
    set @rc = (select count(*)
    from FACULTY f
        join GROUPS g on f.FACULTY = g.FACULTY
        join STUDENT s on g.IDGROUP = s.IDGROUP
    where f.FACULTY = isnull(@faculty, f.FACULTY) and g.PROFESSION = isnull(@prof, g.PROFESSION))
    return @rc
end;
go

declare @r int = dbo.COUNT_STUDENTS('ИТ', '1-40 01 02')
print 'количество: ' + cast(@r as varchar)

select f.FACULTY , dbo.COUNT_STUDENTS(f.FACULTY, null)
from FACULTY f


go

-- 2

create function FSUBJECTS(@p varchar(20)) returns varchar(300)
as begin
    declare @subj_name char(20);
    declare @t varchar(300) = '';

    declare cur cursor local
    for select s.SUBJECT
    from SUBJECT s
    where s.PULPIT = @p

    open cur;

    fetch  cur into @subj_name;

    while @@fetch_status = 0    
	begin
        set @t += rtrim(@subj_name) + ', ';
        fetch  cur into @subj_name;
    end;
    return @t;
end;  
	 go


select PULPIT, dbo.FSUBJECTS(s.PULPIT)
from SUBJECT s
group by PULPIT

go

-- 3

create function FFACPUL(@faculty varchar(20), @pulpit varchar(20)) returns table
as return
select f.FACULTY, p.PULPIT
from FACULTY f
    left join PULPIT p on f.FACULTY = p.FACULTY
where f.FACULTY = isnull(@faculty, f.FACULTY)
    and
    p.PULPIT = ISNULL(@pulpit, p.PULPIT);

go

select *
from dbo.FFACPUL(null, null)
select *
from dbo.FFACPUL('ТОВ', null)
select *
from dbo.FFACPUL(null, 'ОХ')
select *
from dbo.FFACPUL('ИТ', 'ИСиТ')



-- 4
drop function if exists FSUBJECTS
drop function if exists FFACPUL
drop function if exists COUNT_STUDENTS 

go

create function FCTEACHER(@pulpit varchar(20)) returns int
as begin
    declare @rc int = (select count(*)
    from TEACHER t
    where t.PULPIT = isnull(@pulpit, t.pulpit));

    return @rc
end;
	
	go

select PULPIT, dbo.FCTEACHER(PULPIT) [Количество]
from PULPIT

select dbo.FCTEACHER(null) [Всего преподавателей]




-- 6

go

create function dbo.COUNT_PULPITS(@faculty varchar(50))
returns int
as
begin
    declare @rc int;
    select @rc = count(*)
    from PULPIT
    where FACULTY = @faculty;
    return @rc;
end;
go


create function dbo.COUNT_GROUPS(@faculty varchar(50))
returns int
as
begin
    declare @rc int;
    select @rc = count(*)
    from GROUPS
    where FACULTY = @faculty;
    return @rc;
end;
go


create function dbo.COUNT_STUDENTS(@faculty varchar(20) = null, @prof varchar(20) = null) returns int
as
begin
    declare @rc int = 0;
    set @rc = (select count(*)
    from FACULTY f
        join GROUPS  g on f.FACULTY = g.FACULTY
        join STUDENT s on g.IDGROUP = s.IDGROUP
    where f.FACULTY    = isnull(@faculty, f.FACULTY)
        and g.PROFESSION = isnull(@prof,    g.PROFESSION)
    );
    return @rc;
end;
go


create  function dbo.COUNT_PROFS(@faculty varchar(50)) returns int
as
begin
    declare @rc int;
    select @rc = count(*)
    from PROFESSION
    where FACULTY = @faculty;
    return @rc;
end;
go


create function dbo.FACULTY_REPORT(@c int)
returns @fr table(
    [Факультет] varchar(50),
    [Количество кафедр] int,
    [Количество групп] int,
    [Количество студентов] int,
    [Количество специальностей] int)
as
begin
    declare cc cursor static for
        select FACULTY
    from FACULTY
    where dbo.COUNT_STUDENTS(FACULTY, default) > @c;

    declare @f varchar(50);

    open cc;
    fetch cc into @f;
    while @@fetch_status = 0
    begin
        insert into @fr
        values(@f, dbo.COUNT_PULPITS(@f), dbo.COUNT_GROUPS(@f), dbo.COUNT_STUDENTS(@f, default), dbo.COUNT_PROFS(@f));
        fetch cc into @f;
    end;

    close cc;
    deallocate cc;

    return;
end;
go

select *
from dbo.FACULTY_REPORT(0)