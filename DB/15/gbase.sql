create trigger TR_INSERT_SKLAD
on Склад
after insert
as
begin
    declare @msg nvarchar(300)
    select @msg = 'Добавлено: поставщик ' + cast(КодПоставщика as nvarchar) +
                  ', артикул ' + cast(Артикул as nvarchar) +
                  ', количество ' + cast(КоличествоНаСкладе as nvarchar)
    from inserted
    print @msg
end
go

insert into Склад
values
    (1, 102, 40);




create trigger TR_NO_PRICE_UPDATE
on Запчасти
instead of update
as
begin
    if update(Цена)
    begin
        raiserror('Изменение цены запрещено!', 16, 1)
        rollback
        return
    end
    else
    begin
        update Запчасти
        set НазваниеДетали = i.НазваниеДетали,
            Примечание = i.Примечание
        from inserted i
        where Запчасти.Артикул = i.Артикул
    end
end
go


update Запчасти set Цена = 500 where Артикул = 100;