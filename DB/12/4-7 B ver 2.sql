begin tran ;
update ##SUBJECTtmp
  set SUBJECT_NAME = 'м€у'
  where SUBJECT = 'Ѕƒ';


rollback tran ;





begin tran;
update ##SUBJECTtmp
  set SUBJECT_NAME = 'м€у м€у м€у'
  where SUBJECT = 'Ѕƒ';
commit tran;


update ##SUBJECTtmp
  set SUBJECT_NAME = 'Ѕазы данных'
  where SUBJECT = 'Ѕƒ';





begin tran;
insert into ##SUBJECTtmp
  (SUBJECT, SUBJECT_NAME, PULPIT)
values
  ('ѕX', '‘антом 1', '»—и“'),
  ('ѕY', '‘антом 2', '»—и“');
commit tran;