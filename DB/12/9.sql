begin try
    begin transaction;

    declare @кодПоставщика int = 1;
    declare @артикул int = 101;
    declare @количество int = 10;

    if exists (
        select 1 from Склад 
        where КодПоставщика = @кодПоставщика 
          and Артикул = @артикул 
          and КоличествоНаСкладе >= @количество
    )
    begin
        insert into Заказы (НомерЗаказа, КодПоставщика, Артикул, КоличествоЗаказанныхДеталей, ДатаЗаказа)
        values (1001, @кодПоставщика, @артикул, @количество, getdate());


        update Склад
        set КоличествоНаСкладе = КоличествоНаСкладе - @количество
        where КодПоставщика = @кодПоставщика and Артикул = @артикул;

        commit transaction;
        print 'Заказ успешно оформлен и склад обновлён.';
    end
    else
    begin
        throw 50001, 'Недостаточно деталей на складе для заказа', 1;
    end
end try
begin catch
    if @@trancount > 0 rollback transaction;
    print 'Ошибка при оформлении заказа: ' + error_message();
	print 'Изменения не внесены'
end catch;
