#include <iostream>
#define INF INT_MAX  

#include "Combi3.h"
int salesman(     // функция возвращает длину оптимального маршрута
	int n,         //  количество городов 
	const int* d,  //  массив [n*n] расстояний 
	int* r         // массив [n] маршрут 0 x x x x 
);
