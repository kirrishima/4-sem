-- 1
create procedure PSUBJECT
as
begin
	set nocount on;
	declare @PSUBJECT_c int = (select count(*)
	from SUBJECT);
	select SUBJECT as код, SUBJECT_NAME[дисциплина], PULPIT[кафедра]
	from SUBJECT;
	return @PSUBJECT_c;
end;
go

declare @i int;
exec @i = PSUBJECT;
print @i;
go



-- 2

alter procedure [dbo].[PSUBJECT]
	@p varchar(20) = null,
	@c int output
as begin
	set nocount on;
	declare @PSUBJECT_c int = (select count(*)
	from SUBJECT);
	select SUBJECT as код, SUBJECT_NAME[дисциплина], PULPIT[кафедра]
	from SUBJECT
	where PULPIT = @p;
	set @c = @@ROWCOUNT;
	return @PSUBJECT_c;
end;

go

declare @k int = 0, @r int = 0, @p varchar(20)
set @p = 'ИСиТ'
exec @k = PSUBJECT @p, @c = @r output
print 'k = ' + cast(@k as varchar(5))
print 'with pulpit ' + @p + '=' + cast(@r as varchar(3))




-- 3

create table #SUBJECT
(
	код char(10) primary key,
	дисциплина nvarchar(100),
	кафедра char(20)
)
go

alter procedure PSUBJECT
	@p varchar(20)
as begin
	set nocount on;
	declare @PSUBJECT_c int = (select count(*)
	from SUBJECT);
	select SUBJECT as код, SUBJECT_NAME[дисциплина], PULPIT[кафедра]
	from SUBJECT
	where PULPIT = @p;
	return @PSUBJECT_c;
end;
go

insert #SUBJECT
exec PSUBJECT @p = 'ИСиТ'

select *
from #SUBJECT;
go




-- 4

create procedure PAUDITORIUM_INSERT
	@a char(20),
	@n varchar(50),
	@c int = 0,
	@t char(10)
as
declare @rc int = 1
begin try
		insert into AUDITORIUM
	(AUDITORIUM, AUDITORIUM_NAME, AUDITORIUM_CAPACITY, AUDITORIUM_TYPE)
values
	(@a, @n, @c, @t)
		return @rc;
	end try
	begin catch
		print 'код ошибки: ' + cast(error_number() as varchar(6))
		print 'серьезность: ' + cast(error_severity() as varchar(6))
		print 'сообщение: ' + error_message()
		if ERROR_PROCEDURE() is not null
		print 'имя процедуры: ' + error_procedure()
		return -1;
	end catch
	go


declare @rc int;
exec @rc = PAUDITORIUM_INSERT @a='413-1',  @n='413-1', @c='15', @t='ЛК-К'
print 'код возврата: ' + cast(@rc as varchar(3))

select *
from AUDITORIUM

delete from AUDITORIUM where AUDITORIUM = '413-1'
go




-- 5

create procedure SUBJECT_REPORT
	@p varchar(20)
as
begin try
	declare @rc int = -1, @sn varchar(20)='', @t varchar(200) = ''
	declare subj_cur cursor local static for
	(select SUBJECT_NAME
from SUBJECT
where SUBJECT.PULPIT = @p)

	if not exists (select SUBJECT_NAME
from SUBJECT
where SUBJECT.PULPIT = @p)
		raiserror('ошибка в SUBJECT_REPORT', 11, 1)

	open subj_cur
	fetch subj_cur into @sn
	print 'дисциплины:'
	while @@FETCH_STATUS = 0
	begin
	set @t += rtrim(@sn) + ', ';
	set @rc = @rc + 1;
	fetch subj_cur into @sn;
end;   
	 print @t;        
	 close subj_cur;
     return @rc;
end try  
   begin catch              
        print 'ошибка в параметрах' 
        if error_procedure() is not null   
		print 'имя процедуры : ' + error_procedure();
        return @rc;
   end catch;

declare @count int;

exec @count = SUBJECT_REPORT @p = 'ИСиТ'
print 'количество дисциплин=' + cast(@count as varchar(3))

exec @count = SUBJECT_REPORT @p = 'ваы'
print 'количество дисциплин=' + cast(@count as varchar(3))



go

-- 6



create procedure PAUDITORIUM_INSERTX
	@a char(20),
	@n varchar(50),
	@c int = 0,
	@t char(10),
	@tn varchar(50)
as
begin try 
    set transaction isolation level SERIALIZABLE;          
    begin tran
    insert into AUDITORIUM_TYPE
values
	(@t, @tn)
	exec PAUDITORIUM_INSERT @a, @n, @c, @t 
    commit tran;            
end try
begin catch 
    print 'номер ошибки  : ' + cast(error_number() as varchar(6));
    print 'сообщение     : ' + error_message();
    print 'уровень       : ' + cast(error_severity()  as varchar(6));
    print 'метка         : ' + cast(error_state()   as varchar(8));
    print 'номер строки  : ' + cast(error_line()  as varchar(8));
    if error_procedure() is not  null   
                     print 'имя процедуры : ' + error_procedure();
     if @@trancount > 0 rollback tran ; 
     return -1;	  
end catch;

declare @rc int;
exec @rc = PAUDITORIUM_INSERTX @a='9999',  @n='9999', @c=15, @t='ММММ', @tn = 'fd'

select *
from AUDITORIUM
select *
from AUDITORIUM_TYPE

delete from AUDITORIUM where AUDITORIUM = '9999'
delete from AUDITORIUM_TYPE where AUDITORIUM_TYPE = 'ММММ'



drop procedure if exists PAUDITORIUM_INSERTX;
drop procedure if exists SUBJECT_REPORT;
drop procedure if exists PAUDITORIUM_INSERT;
drop procedure if exists PSUBJECT;
