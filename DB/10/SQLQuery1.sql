-- 1 
exec sp_helpindex 'AUDITORIUM_TYPE';
exec sp_helpindex 'AUDITORIUM';
exec sp_helpindex 'FACULTY';
exec sp_helpindex 'PROFESSION';
exec sp_helpindex 'PULPIT';
exec sp_helpindex 'TEACHER';
exec sp_helpindex 'SUBJECT';
exec sp_helpindex 'GROUPS';
exec sp_helpindex 'STUDENT';
exec sp_helpindex 'PROGRESS';

drop table if exists #tmpTest1;
create table #tmpTest1
(
    i int,
    t varchar(20),
    Pow2 int
);

declare @i int = 0;
while @i < 10000
begin
    insert into #tmpTest1
    values
        (@i, CAST(@i as varchar(20)), POWER(@i, 2));
    set @i += 1;
end;

drop index if exists #tmpTest_CL on #tmpTest1
select *
from #tmpTest1



create clustered index #tmpTest_CL on #tmpTest1(i asc)

select *
from #tmpTest1
go;

-- 2
drop table if exists #tmpTest2;
create table #tmpTest2
(
    i int,
    t varchar(20),
    Pow2 int
);

set nocount on;
declare @i int = 0;
while @i < 30000
begin
    insert into #tmpTest2
    values
        (@i, CAST(@i as varchar(20)), POWER(@i, 2));
    set @i += 1;
end;

drop index if exists #tmpTest2_NONCLU on #tmpTest2;
select COUNT(*)
from #tmpTest2;
select *
from #tmpTest2;

create index #tmpTest2_NONCLU on #tmpTest2(i, t)

select *
from #tmpTest2
where i > 15000

select *
from #tmpTest2
order by i, t


select *
from #tmpTest2
where i = 500

go

-- 3
drop table if exists #tmpTest3;
create table #tmpTest3
(
    i int,
    t varchar(20),
    Pow2 int
);

set nocount on;
declare @i int = 0;
while @i < 30000
begin
    insert into #tmpTest3
    values
        (@i, CAST(@i as varchar(20)), POWER(@i, 2));
    set @i += 1;
end;

drop index if exists #tmpTest_i on #tmpTest3;
create index #tmpTest_i on #tmpTest3(i) include (t);

select t
from #tmpTest3
where i > 15000

go


-- 4
drop table if exists #tmpTest4;
create table #tmpTest4
(
    i int,
    t varchar(20),
    Pow2 int
);

set nocount on;
declare @i int = 0;
while @i < 30000
begin
    insert into #tmpTest4
    values
        (@i, CAST(@i as varchar(20)), POWER(@i, 2));
    set @i += 1;
end;

drop index if exists #tmpTest_i on #tmpTest4;
create index #tmpTest_WHERE on #tmpTest4(i) where i > 15000 and i < 20000;

select i
from #tmpTest4
where i > 15000 and i < 20000

go


-- 5
drop table if exists #tmpTest5;
create table #tmpTest5
(
    i int,
    t varchar(20),
    Pow2 int
);

set nocount on;
declare @i int = 0;
while @i < 10000
begin
    insert into #tmpTest5
    values
        (@i, CAST(@i as varchar(20)), POWER(@i, 2));
    set @i += 1;
end;

drop index if exists #tmpTest5_NONCLU on #tmpTest5;
create index #tmpTest5_NONCLU on #tmpTest5(i)


select ii.name [Индекс], ss.avg_fragmentation_in_percent [Фрагментация (%)]
from sys.dm_db_index_physical_stats(DB_ID('tempdb'), OBJECT_ID('tempdb..#tmpTest5'), null, null, null) ss
    join tempdb.sys.indexes ii on ss.object_id = ii.object_id and ss.index_id = ii.index_id
where ii.name is not null;


declare @j   int = 1,
        @rnd int;
while @j <= 100000
begin
    set @rnd = CAST(RAND(CHECKSUM(NEWID())) * 30000 as int);
    insert into #tmpTest5
    values
        (
            @rnd,
            REPLICATE('X', 20),
            POWER(@rnd, 2)
    );
    set @j += 1;
end;

alter index #tmpTest5_NONCLU on #tmpTest5 reorganize;

alter index #tmpTest5_NONCLU on #tmpTest5 rebuild with (online = off);


-- 6
drop index if exists #tmpTest5_NONCLU on #tmpTest5;
create index #tmpTest5_NONCLU on #tmpTest5(i) with (fillfactor = 65)

select *
from #tmpTest5
where i = 12

