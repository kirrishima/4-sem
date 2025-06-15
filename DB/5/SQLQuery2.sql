select з.Артикул, п.КодПоставщика from Заказы з, Поставщики п where п.КодПоставщика = з.КодПоставщика
and з.Артикул not in (select с.Артикул from Склад с where с.КодПоставщика = п.КодПоставщика)



select з.Артикул, п.КодПоставщика from Заказы з join Поставщики п on п.КодПоставщика = з.КодПоставщика
and з.Артикул not in (select с.Артикул from Склад с where с.КодПоставщика = п.КодПоставщика)



select з.Артикул, п.КодПоставщика from Заказы з join Поставщики п on п.КодПоставщика = з.КодПоставщика
left join Склад с on с.КодПоставщика = п.КодПоставщика and с.Артикул = з.Артикул where с.Артикул is null



select * from Запчасти з where з.Цена = (select top 1 Цена from Запчасти order by Цена desc)


select з.НазваниеДетали from Запчасти з where exists(select * from Склад с where с.Артикул = з.Артикул)


select AVG(з.Цена) from Запчасти з

select * from Запчасти з;

select * from Запчасти з where з.Цена > Any(select з1.Цена from Запчасти з1)

select * from Запчасти з where з.Цена >= All(select з1.Цена from Запчасти з1)

