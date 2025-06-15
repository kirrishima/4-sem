use G_MyBase;
go


create procedure usp_AddSupplier
    @КодПоставщика int,
    @НазваниеПоставщика varchar(255),
    @Адрес varchar(255) = null,
    @Телефон varchar(50) = null
as
begin
    insert into Поставщики (КодПоставщика, НазваниеПоставщика, Адрес, Телефон)
    values (@КодПоставщика, @НазваниеПоставщика, @Адрес, @Телефон);
end;
go

exec usp_AddSupplier 1, N'Поставщик А', N'Москва, ул. Ленина, 1', '+7-495-123-45-67';
exec usp_AddSupplier 2, N'Поставщик Б', N'Санкт-Петербург, Невский пр., 10', '+7-812-987-65-43';





create procedure usp_AddPart
    @Артикул int,
    @НазваниеДетали varchar(255),
    @Цена real,
    @Примечание text = null
as
begin
    insert into Запчасти (Артикул, НазваниеДетали, Цена, Примечание)
    values (@Артикул, @НазваниеДетали, @Цена, @Примечание);
end;
go


exec usp_AddPart 100, N'Фильтр масляный', 250.50, N'Для двигателей 2.0L';
exec usp_AddPart 200, N'Свеча зажигания', 150.00, N'Стандартная';
