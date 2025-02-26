#include <iostream>
#include <iomanip>
#include <ctime>
#include <cstdlib>
#include "Boat.h"

#define NN 25  // Количество контейнеров
#define MM 5   // Количество мест на судне
#define V 1500 // Ограничение по весу

int main() {
	setlocale(LC_ALL, "rus");
	srand(time(0)); // Инициализация генератора случайных чисел

	int v[NN]; // Массив весов контейнеров
	int c[NN]; // Массив доходов от контейнеров

	// Заполнение случайными значениями
	for (int i = 0; i < NN; i++) {
		v[i] = rand() % 801 + 100;  // Вес от 100 до 900 кг
		c[i] = rand() % 141 + 10;   // Доход от 10 до 150 у.е.
	}

	short r[MM]; // Массив индексов выбранных контейнеров

	// Вызов функции оптимального размещения контейнеров
	int cc = boat(V, MM, NN, v, c, r);

	// Подсчёт общего веса выбранных контейнеров
	int s = 0;
	for (int i = 0; i < MM; i++) {
		s += v[r[i]];
	}

	// Вывод результатов в виде таблицы
	std::cout << "\nРезультаты размещения контейнеров на судне:\n\n";
	std::cout << std::left
		<< std::setw(10) << "Индекс"
		<< std::setw(10) << "Вес"
		<< std::setw(10) << "Доход"
		<< std::setw(10) << "Выбран"
		<< std::endl;
	std::cout << "---------------------------------------------" << std::endl;

	bool selected[NN] = { false };
	for (int i = 0; i < MM; i++) {
		if (r[i] >= 0 && r[i] < NN)
			selected[r[i]] = true;
	}

	for (int i = 0; i < NN; i++) {
		std::cout << std::setw(10) << i
			<< std::setw(10) << v[i]
			<< std::setw(10) << c[i]
			<< std::setw(10) << (selected[i] ? "Да" : "Нет")
			<< std::endl;
	}

	std::cout << "\nОбщее количество контейнеров: " << NN
		<< "\nКоличество мест на судне    : " << MM
		<< "\nОграничение по весу         : " << V
		<< "\n\nИтоговый доход от перевозки : " << cc
		<< "\nОбщий вес выбранных контейнеров: " << s
		<< "\n\n";

	system("pause");
	return 0;
}
