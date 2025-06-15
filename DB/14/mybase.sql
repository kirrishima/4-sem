create or alter function dbo.GetSupplierPhone(@id int)  
returns varchar(50) as
begin
    declare @p varchar(50);
    select @p = Телефон
    from Поставщики
    where КодПоставщика = @id;
    return @p;
end; 
go

create or alter function dbo.GetStockBySupplier(@id int)  
returns table as return
(
    select s.Артикул, z.НазваниеДетали, s.КоличествоНаСкладе
from Склад s
    join Запчасти z on z.Артикул = s.Артикул
where s.КодПоставщика = @id
); 
go

select dbo.GetSupplierPhone(1);
select *
from dbo.GetStockBySupplier(1);


drop function if exists GetStockBySupplier
drop function if exists GetSupplierPhone