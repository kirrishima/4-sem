declare 
    @charVar char(10) = 'Биба',                 
    @varcharVar varchar(50) = 'Боба',
    @datetimeVar datetime,
    @timeVar time,
    @intVar int,
    @smallintVar smallint,
    @tinyintVar tinyint,
    @numericVar numeric(12,5);

set @datetimeVar = GETDATE();
set @timeVar = CAST(GETDATE() as time);
set @intVar = 1000;

select
    @smallintVar = 327,
    @tinyintVar = 50,
    @numericVar = 52.42;

select
    @charVar as char,
    @varcharVar as varcgar,
    CAST(@datetimeVar as varchar(30)) as datetime,
    CAST(@timeVar as varchar(30)) as time;

print 'int: ' + CAST(@intVar as varchar(20));
print 'smallint: ' + CAST(@smallintVar as varchar(20));
print 'tinyintV: ' + CAST(@tinyintVar as varchar(20));
print 'numeric: ' + CAST(@numericVar as varchar(20));


declare 
    @totalCapacity int,        
    @auditoriumsCount int,        
    @avgCapacity float,     
    @lessAvg int,         
    @lessAvgPercents float;

select
    @totalCapacity = SUM(AUDITORIUM_CAPACITY),
    @auditoriumsCount = COUNT(*)
from AUDITORIUM;


if @totalCapacity > 200
begin

    set @avgCapacity = @totalCapacity * 1.0 / @auditoriumsCount;

    select @lessAvg = COUNT(*)
    from AUDITORIUM
    where AUDITORIUM_CAPACITY < @avgCapacity;

    set @lessAvgPercents = (@lessAvg * 100.0) / @auditoriumsCount;


    select
        @auditoriumsCount as [всего],
        @avgCapacity as [средняя вместимость],
        @lessAvg as [меньше среднего],
        @lessAvgPercents as [меньше среднего в процентах];
end
else
begin
    print 'Общая вместимость аудиторий меньше 200: ' + CAST(@totalCapacity as varchar(10));
end;



-- 3

print 'Значение @@ROWCOUNT: ' + CAST(@@ROWCOUNT as varchar(10));
print 'Значение @@VERSION: ' + @@VERSION;
print 'Значение @@SPID: ' + CAST(@@SPID as varchar(10));
print 'Значение @@ERROR: ' + CAST(@@ERROR as varchar(10));
print 'Значение @@SERVERNAME: ' + @@SERVERNAME;
print 'Значение @@TRANCOUNT: ' + CAST(@@TRANCOUNT as varchar(10));
print 'Значение @@FETCH_STATUS: ' + CAST(@@FETCH_STATUS as varchar(10));
print 'Значение @@NESTLEVEL: ' + CAST(@@NESTLEVEL as varchar(10));



-- 4

declare @t float = 1.5, @x float = 1.2, @z float;
set @z = case 
            when @t > @x then POWER(SIN(@t), 2)
            when @t < @x then 4 * (@t + @x)
            else 1 - EXP(@x - 2)
         end;
select @z as Result;
go


select
    NAME,
    left(NAME, CHARINDEX(' ', NAME) - 1) + ' ' +
  left(SUBSTRING(NAME, CHARINDEX(' ', NAME) + 1, LEN(NAME)), 1) + '. ' +
  left(SUBSTRING(NAME, CHARINDEX(' ', NAME, CHARINDEX(' ', NAME) + 1) + 1, LEN(NAME)), 1) + '.'
    as Сокращенное
from STUDENT;
go


declare @NextMonth int;
set @NextMonth = case when MONTH(GETDATE()) = 12 then 1 else MONTH(GETDATE()) + 1 end;

print 'Студенты, у которых день рождения в следующем месяце, и их возраст:';
select
    IDSTUDENT,
    NAME,
    BDAY,
    DATEDIFF(YEAR, BDAY, GETDATE()) as Age
from STUDENT
where MONTH(BDAY) = @NextMonth;
go


declare @GroupID int = 5;
select distinct DATENAME(WEEKDAY, PDATE) as ExamDayOfWeek
from PROGRESS
where SUBJECT = 'СУБД'
    and IDSTUDENT in (select IDSTUDENT
    from STUDENT
    where IDGROUP = @GroupID);
go



-- 5

declare @AverageGrade decimal(4,2);

select @AverageGrade = AVG(CAST(NOTE as decimal(4,2)))
from PROGRESS
where SUBJECT = 'ОАиП';

if (@AverageGrade >= 8)
begin
    print 'невозможная успеваемость по ОАиП: ' + CAST(@AverageGrade as varchar(10));
end
else if (@AverageGrade >= 5 and @AverageGrade < 8)
begin
    print 'нормальная успеваемость по ОАиП: ' + CAST(@AverageGrade as varchar(10));
end
else
begin
    print 'обычная успеваемость по ОАиП:' + CAST(@AverageGrade as varchar(10));
end;



-- 6

select
    s.NAME,
    sub.SUBJECT_NAME,
    p.NOTE,

    case 
        when p.NOTE >= 9 then 'отлично'
        when p.NOTE >= 7 then 'хорошо'
        when p.NOTE >= 5 then 'норм'
        else 'плоха'
    end as оценка_оценки,
    f.FACULTY_NAME as факультет
from STUDENT s
    inner join GROUPS g on s.IDGROUP = g.IDGROUP
    inner join FACULTY f on g.FACULTY = f.FACULTY
    inner join PROGRESS p on s.IDSTUDENT = p.IDSTUDENT
    inner join SUBJECT sub on p.SUBJECT = sub.SUBJECT
where f.FACULTY = 'ИТ'; 
go


-- 7

create table #TempTable
(
    ID int,
    строка varchar(50),
    число int
);


declare @i int = 1;

while (@i <= 10)
begin
    insert into #TempTable
    values
        (@i, 'Строка_' + CAST(@i as varchar(10)), @i * 10);

    set @i = @i + 1;
end;

select *
from #TempTable;

go


-- 8

use UNIVER;
go
print 'А';
if (1=1)
    return;
print 'боба';
go


-- 9

begin try
    select 1/0 as Result;
end try
begin catch
    select ERROR_NUMBER() as ErrorNumber,
    ERROR_MESSAGE() as ErrorMessage,
    ERROR_LINE() as ErrorLine,
    ERROR_PROCEDURE() as ErrorProcedure,
    ERROR_SEVERITY() as ErrorSeverity,
    ERROR_STATE() as ErrorState;
end catch;
go
