declare @sellerCode int,
        @sellerName varchar(255),
        @adress varchar(255),
        @Телефон varchar(50);


declare sellers_cur cursor for
    select КодПоставщика,
    НазваниеПоставщика,
    Адрес,
    Телефон
from Поставщики
order by КодПоставщика;

open sellers_cur;


fetch NEXT from sellers_cur
into @sellerCode, @sellerName, @adress, @Телефон;


while @@FETCH_STATUS = 0
begin
    print 'Поставщик ID = ' + CAST(@sellerCode as VARCHAR(10))
          + ', Название = ' + @sellerName
          + ', Адрес = ' + ISNULL(@adress, '<нет данных>')
          + ', Телефон = ' + ISNULL(@Телефон, '<нет данных>');

    fetch NEXT from sellers_cur
    into @sellerCode, @sellerName, @adress, @Телефон;
end;


close sellers_cur;
deallocate sellers_cur;
go




go

declare @sellerCode int,
        @articool int,
        @amount int;

declare storage_cur cursor for
    select КодПоставщика, Артикул, КоличествоНаСкладе
from Склад
order by КодПоставщика, Артикул;

open storage_cur;

fetch NEXT from storage_cur
into @sellerCode, @articool, @amount;

while @@FETCH_STATUS = 0
begin
    print 'Поставщик ' + CAST(@sellerCode as VARCHAR(10))
          + ', Артикул ' + CAST(@articool as VARCHAR(10))
          + ' — Остаток: ' + CAST(@amount as VARCHAR(10));

    fetch NEXT from storage_cur
    into @sellerCode, @articool, @amount;
end;

close storage_cur;
deallocate storage_cur;

