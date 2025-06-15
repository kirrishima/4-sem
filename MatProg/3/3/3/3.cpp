#include <iostream>
#include <iomanip> 
#include <tchar.h>
#include <stdio.h>
#include "Salesman.h"

constexpr int N = 5;
constexpr int n = 3;

int _tmain(int argc, _TCHAR* argv[])
{
	setlocale(LC_ALL, "rus");
	int d[N][N] = { //0			  1			  2        3			4        
				   { INF,		2 * n,		21 + n,		INF,		n},			 //  0
				   { n,		    INF,		15 + n,		68 - n,		84 - n},     //  1
				   { 2 + n,     3 * n,      INF,		86,			49 + n},     //  2 
				   { 17 + n,    58 - n,     4 * n,      INF,		3 * n},      //  3
				   { 93 - n,    66 + n,     52,			13 + n,     INF} };		 //  4 
	int r[N];                     // результат 

	int s = salesman(
		N,          //  количество городов 
		(int*)d,          //  массив [n*n] расстояний 
		r           // массив [n] маршрут 0 x x x x  

	);
	std::cout << std::endl << "-- Задача коммивояжера -- ";
	std::cout << std::endl << "-- количество  городов: " << N;
	std::cout << std::endl << "-- матрица расстояний : ";
	for (int i = 0; i < N; i++) {
		std::cout << std::endl;
		for (int j = 0; j < N; j++)
			if (d[i][j] != INF) std::cout << std::setw(3) << d[i][j] << " ";
			else std::cout << std::setw(3) << "INF" << " ";
	}
	std::cout << std::endl << "-- оптимальный маршрут: ";
	for (int i = 0; i < N; i++) std::cout << r[i] + 1 << "-->"; std::cout << 1;
	std::cout << std::endl << "-- длина маршрута     : " << s;
	std::cout << std::endl;
	return 0;
}

