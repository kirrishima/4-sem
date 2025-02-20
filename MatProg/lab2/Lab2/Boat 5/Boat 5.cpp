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

    // Вывод данных
    std::cout << std::endl << "- Задача о размещении контейнеров на судне";
    std::cout << std::endl << "- общее количество контейнеров    : " << NN;
    std::cout << std::endl << "- количество мест для контейнеров : " << MM;
    std::cout << std::endl << "- ограничение по суммарному весу  : " << V;
    std::cout << std::endl << "- веса контейнеров               : ";
    for (int i = 0; i < NN; i++) std::cout << std::setw(3) << v[i] << " ";
    std::cout << std::endl << "- доход от перевозки              : ";
    for (int i = 0; i < NN; i++) std::cout << std::setw(3) << c[i] << " ";
    std::cout << std::endl << "- выбраны контейнеры (индексы)    : ";
    for (int i = 0; i < MM; i++) std::cout << r[i] << " ";
    std::cout << std::endl << "- доход от перевозки              : " << cc;

    // Подсчёт общего веса выбранных контейнеров
    int s = 0;
    for (int i = 0; i < MM; i++) s += v[r[i]];
    std::cout << std::endl << "- общий вес выбранных контейнеров : " << s;

    std::cout << std::endl << std::endl;
    system("pause");
    return 0;
}
