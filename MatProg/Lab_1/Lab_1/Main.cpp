#include "pch.h"
#include "Auxil.h"                            // вспомогательные функции 
#include <iostream>
#include <ctime>
#include <locale>
#include <tchar.h>
#include <omp.h>

#define  CYCLE  10                       // количество циклов  

ull fibonachi(ull n)
{
	ull a = 0, b = 1;
	switch (n)
	{
	case 1:
		return 0;
	case 2:
		return 1;
	default:
		for (size_t i = 0; i < n - 2; i++)
		{
			b = a + b;
			a = b - a;
		}
		return b;
	}
}

int _tmain(int argc, _TCHAR* argv[])
{

	double  av1 = 0, av2 = 0;
	clock_t  t1 = 0, t2 = 0;

	setlocale(LC_ALL, "rus");

	auxil::start();                          // старт генерации 

	for (size_t i = 1; i <= CYCLE; i++)
	{
		ull x = powl(10, i);

		std::cout << std::endl << "число фибоначи номер " << x;
		std::cout << std::endl << "цикл " << i;

		t1 = clock();
		fibonachi(x);
		t2 = clock();

		std::cout << std::endl << "продолжительность (у.е):   " << (t2 - t1);
		std::cout << std::endl << "                  (сек):   "
			<< ((double)(t2 - t1)) / ((double)CLOCKS_PER_SEC);
	}
	system("pause");

	return 0;
}