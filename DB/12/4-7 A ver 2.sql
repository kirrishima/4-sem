drop table if exists ##SUBJECTtmp;
select *
into ##SUBJECTtmp
from SUBJECT;

set transaction ISOLATION LEVEL read UNCOMMITTED;
-- SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
-- set transaction ISOLATION LEVEL REPEATABLE read;
-- set transaction ISOLATION LEVEL serializable;
begin tran
select SUBJECT, SUBJECT_NAME
from ##SUBJECTtmp
where SUBJECT = 'ад';


select SUBJECT, SUBJECT_NAME
from ##SUBJECTtmp
where SUBJECT = 'ад';
commit






set transaction ISOLATION LEVEL read UNCOMMITTED;
-- SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
-- set transaction ISOLATION LEVEL REPEATABLE read;
-- set transaction ISOLATION LEVEL serializable;
begin tran
select *
from ##SUBJECTtmp
where SUBJECT = 'ад';


select *
from ##SUBJECTtmp
where SUBJECT = 'ад';
commit






set transaction ISOLATION LEVEL read UNCOMMITTED;
-- SET TRANSACTION ISOLATION LEVEL READ COMMITTED;
-- set transaction ISOLATION LEVEL REPEATABLE read;
-- set transaction ISOLATION LEVEL serializable;
begin tran
select COUNT(*) as P1
from ##SUBJECTtmp
where SUBJECT like 'о%';


select COUNT(*) as P2
from ##SUBJECTtmp
where SUBJECT like 'о%';
commit