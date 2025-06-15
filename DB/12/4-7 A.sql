-- 4-5
drop table if exists ##SUBJECTtmp;
select *
into ##SUBJECTtmp
from SUBJECT;

-- A

-- для 4 задания
set transaction ISOLATION LEVEL read UNCOMMITTED;

-- для 5 задания
-- SET TRANSACTION ISOLATION LEVEL READ COMMITTED;

begin tran T1;

-- (1) первое чтение
select SUBJECT, SUBJECT_NAME
from ##SUBJECTtmp
where SUBJECT = 'БД';

-- ждем, пока T2 обновит, но не коммитит
waitfor DELAY '00:00:06';

-- (2) второе чтение — увидим незакоммиченные изменения
select SUBJECT, SUBJECT_NAME
from ##SUBJECTtmp
where SUBJECT = 'БД';

print '--- Non-Repeatable Read ---';
-- (3) считаем до вставки
select *
from ##SUBJECTtmp
where SUBJECT = 'БД';

-- ждем, пока T2 вставит и закоммитит
waitfor DELAY '00:00:06';

-- (4) считаем после вставки — число изменилось
select *
from ##SUBJECTtmp
where SUBJECT = 'БД';

print '--- Phantom Read ---';
-- (5) считаем до фантомов
select COUNT(*) as P1
from ##SUBJECTtmp
where SUBJECT like 'П%';

-- ждем, пока T2 создаст фантомные записи
waitfor DELAY '00:00:06';

-- (6) считаем после — фантомные строки появились
select COUNT(*) as P2
from ##SUBJECTtmp
where SUBJECT like 'П%';

commit tran T1;






-- 6-7

drop table if exists ##SUBJECTtmp;
select *
into ##SUBJECTtmp
from SUBJECT;



-- фа
set transaction ISOLATION LEVEL REPEATABLE read;
--set transaction ISOLATION LEVEL serializable;

begin tran T1;

select COUNT(*) as P1
from ##SUBJECTtmp
where SUBJECT like 'П%';

waitfor DELAY '00:00:03';

select COUNT(*) as P2
from ##SUBJECTtmp
where SUBJECT like 'П%';

commit tran T1


