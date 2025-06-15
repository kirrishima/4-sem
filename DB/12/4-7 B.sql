-- 4 

-- B

-- Dirty-read часть: обновим и **не** зафиксируем сразу
begin tran T2a;
waitfor DELAY '00:00:01';
update ##SUBJECTtmp
  set SUBJECT_NAME = 'мяу'
  where SUBJECT = 'БД';
-- первая выборка T1 успеет показать старое значение; вторая — грязное чтение
-- для задания 4 только это
-- WAITFOR DELAY '00:00:09';
waitfor DELAY '00:00:06';
rollback tran T2a;
waitfor DELAY '00:00:01';
-- для задания 5 еще и это
-- WAITFOR DELAY '00:00:02';

-- Non-repeatable часть: обновим поле SUBJECT и зафиксируем
begin tran T2b;

update ##SUBJECTtmp
  set SUBJECT = 'мяу'
  where SUBJECT = 'БД';
--для задания 5 можно также вставить новую строку
--INSERT INTO ##SUBJECTtmp(SUBJECT, SUBJECT_NAME, PULPIT)
--  VALUES('ZZ1','Тест №1','ИСиТ');
commit tran T2b;

-- Phantom часть: вставим две «фантомные» и зафиксируем
-- для задания 4 только это
waitfor DELAY '00:00:05';
-- для задания 5 только это
-- WAITFOR DELAY '00:00:07';

begin tran T2c;
insert into ##SUBJECTtmp
  (SUBJECT, SUBJECT_NAME, PULPIT)
values
  ('ПX', 'Фантом 1', 'ИСиТ'),
  ('ПY', 'Фантом 2', 'ИСиТ');
commit tran T2c;

-- 4 

-- B

-- Dirty-read часть: обновим и **не** зафиксируем сразу
begin tran T2a;
update ##SUBJECTtmp
  set SUBJECT_NAME = 'мяу'
  where SUBJECT = 'БД';
waitfor DELAY '00:00:05';
rollback tran T2a;




-- Non-repeatable вставка новой строки и фиксация
begin tran T2b;
update ##SUBJECTtmp
  set SUBJECT = 'мяу'
  where SUBJECT = 'БД';
commit tran T2b;

waitfor DELAY '00:00:05';

update ##SUBJECTtmp
  set SUBJECT = 'БД'
  where SUBJECT = 'мяу';




-- 6
begin tran T2c;
insert into ##SUBJECTtmp
  (SUBJECT, SUBJECT_NAME, PULPIT)
values
  ('ПX', 'Фантом 1', 'ИСиТ'),
  ('ПY', 'Фантом 2', 'ИСиТ');
commit tran T2c;

