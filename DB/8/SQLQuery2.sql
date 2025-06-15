create view
    Заказы_Подробно
as
    select
        О.НомерЗаказа,
        П.НазваниеПоставщика,
        О.Артикул,
        З.НазваниеДетали,
        О.КоличествоЗаказанныхДеталей,
        О.ДатаЗаказа
    from
        Заказы О
        join Поставщики П on О.КодПоставщика = П.КодПоставщика
        join Запчасти З on О.Артикул = З.Артикул;

go

select
    *
from
    Заказы_Подробно;
go


create view
    dbo.Поставщикии
with
    SCHEMABINDING
as
    select
        КодПоставщика,
        НазваниеПоставщика,
        Телефон
    from
        dbo.Поставщики
    where
    Телефон is not null;
go


create view
    Запчастии
as
    select
        Артикул,
        НазваниеДетали,
        Цена
    from
        Запчасти
    where
    Цена > 0
WITH
    check option;
go


delete from Запчасти;
