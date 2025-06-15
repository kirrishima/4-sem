use G_MyBase
alter table Поставщики add Пол int;

alter table Поставщики alter column Пол int not null;

alter table Поставщики add constraint полож_Поставщики_Пол check( Пол > 0 );

alter table Поставщики drop column Пол;