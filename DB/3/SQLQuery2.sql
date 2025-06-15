use G_MyBase;
go

create table Поставщики
(
    КодПоставщика int primary key,
    НазваниеПоставщика varchar(255) not null,
    Адрес varchar(255),
    Телефон varchar(50)
) on [PRIMARY];
go

create table Запчасти
(
    Артикул int primary key,
    НазваниеДетали varchar(255) not null,
    Цена real,
    Примечание text
) on [PRIMARY];
go

create table Склад
(
    КодПоставщика int,
    Артикул int,
    КоличествоНаСкладе int,
    primary key (КодПоставщика, Артикул),
    foreign key (КодПоставщика) references Поставщики(КодПоставщика) on update cascade,
    foreign key (Артикул) references Запчасти(Артикул) on update cascade
) on fg1;
go

create table Заказы
(
    НомерЗаказа int primary key,
    КодПоставщика int,
    Артикул int,
    КоличествоЗаказанныхДеталей int,
    ДатаЗаказа date,
    foreign key (КодПоставщика) references Поставщики(КодПоставщика) on update cascade,
    foreign key (Артикул) references Запчасти(Артикул) on update cascade
) on fg1;
go


drop table Заказы;
drop table Склад;
drop table Поставщики;
drop table Запчасти;

delete Заказы;
delete Склад;
delete Поставщики;
delete Запчасти;