select o.НомерЗаказа,
    o.ДатаЗаказа,
    p.НазваниеПоставщика,
    z.НазваниеДетали,
    o.КоличествоЗаказанныхДеталей
from Заказы as o
    join Поставщики as p on o.КодПоставщика = p.КодПоставщика
    join Запчасти    as z on o.Артикул         = z.Артикул
where o.ДатаЗаказа <= DATEADD(MONTH, -1, GETDATE());
go


create nonclustered index Заказы_Дата_NONCL
on Заказы (ДатаЗаказа)
include (НомерЗаказа, КодПоставщика);
go


create nonclustered index Заказы_Поставщик_Дата_NONCLU
on Заказы (КодПоставщика, ДатаЗаказа)
include (Артикул, КоличествоЗаказанныхДеталей);
go


create nonclustered index Заказы_ПоследнийМесяц_NONCL
on Заказы (ДатаЗаказа)
include (НомерЗаказа)
where ДатаЗаказа >= '2005-04-26';
go