
set nocount on;
drop table if exists ##SUBJECTtmp;
select top 3
    *
into ##SUBJECTtmp
from SUBJECT;


begin tran OuterT;
print 'внешний begin, @@TRANCOUNT=' + CAST(@@TRANCOUNT as VARCHAR);

insert into ##SUBJECTtmp
    (SUBJECT, SUBJECT_NAME, PULPIT)
values
    ('T1', 'внешний', ' ');


begin tran InnerT;
print 'вложенный begin, @@TRANCOUNT=' + CAST(@@TRANCOUNT as VARCHAR);

insert into ##SUBJECTtmp
    (SUBJECT, SUBJECT_NAME, PULPIT)
values
    ('T2', 'вложенный', ' ');


commit tran InnerT;
print 'вложенный commit, @@TRANCOUNT=' + CAST(@@TRANCOUNT as VARCHAR);


select SUBJECT_NAME
from ##SUBJECTtmp;


rollback tran OuterT;
print 'внешний rollback, @@TRANCOUNT=' + CAST(@@TRANCOUNT as VARCHAR);


select SUBJECT_NAME
from ##SUBJECTtmp;