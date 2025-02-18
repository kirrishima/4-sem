#include "pch.h"
#include "Auxil.h"
#include <iostream>
#include <ctime>
#include <locale>
#include <tchar.h>

#define  CYCLE  1000000                       // количество циклов  

ull fibonacci(ull n)
{
	if (n == 0) return 0;
	if (n == 1) return 1;
	return fibonacci(n - 1) + fibonacci(n - 2);
}

int _tmain(int argc, _TCHAR* argv[])
{
	setlocale(LC_ALL, "rus");

	double  av1 = 0, av2 = 0;
	clock_t  t1 = 0, t2 = 0;

	setlocale(LC_ALL, "rus");

	auxil::start();                          // старт генерации 

	for (size_t i = 0; i < 10; i++)
	{
		int cycles = pow(10, i);
		t1 = clock();                            // фиксация времени 
		for (int i = 0; i < cycles; i++)
		{
			av1 += (double)auxil::iget(-100, 100); // сумма случайных чисел 
			av2 += auxil::dget(-100, 100);         // сумма случайных чисел 
		}
		t2 = clock();                            // фиксация времени 

		std::cout << std::endl << "количество циклов:         " << cycles;
		std::cout << std::endl << "среднее значение (int):    " << av1 / cycles;
		std::cout << std::endl << "среднее значение (double): " << av2 / cycles;
		std::cout << std::endl << "продолжительность (у.е):   " << (t2 - t1);
		std::cout << std::endl << "                  (сек):   "
			<< ((double)(t2 - t1)) / ((double)CLOCKS_PER_SEC);
		std::cout << std::endl;
	}

	t1 = 0, t2 = 0;

	auxil::start();                          // старт генерации 

	for (size_t i = 1; i <= 10; i++)
	{
		ull x = 10 * i / 2;

		std::cout << std::endl << '(' << "цикл " << i << ") число фибоначи номер " << x;

		t1 = clock();
		fibonacci(x);
		t2 = clock();

		std::cout << std::endl << "продолжительность (у.е):   " << (t2 - t1);
		std::cout << std::endl << "                  (сек):   "
			<< ((double)(t2 - t1)) / ((double)CLOCKS_PER_SEC);
	}
	std::cout << '\n';
	return 0;
}
