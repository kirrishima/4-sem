#include <algorithm>
#include <iostream>
#include <ctime>
#include <iomanip>
#include <cmath>       
#include "Levenshtein.h"

int main()
{
	setlocale(LC_ALL, "rus");
	srand((unsigned)time(0));

	// -----------------------------
	// 1) Генерация случайных строк
	// -----------------------------
	const int MAX_X = 300;
	const int MAX_Y = 250;
	char x[MAX_X + 1];
	char y[MAX_Y + 1];

	for (int i = 0; i < MAX_X; i++)
	{
		x[i] = static_cast<char>('a' + rand() % 26);
	}
	x[MAX_X] = '\0';

	for (int i = 0; i < MAX_Y; i++)
	{
		y[i] = static_cast<char>('a' + rand() % 26);
	}
	y[MAX_Y] = '\0';

	std::cout << "String X: " << x << std::endl << std::endl;
	std::cout << "String Y: " << y << std::endl << std::endl;

	// -----------------------------
	// 2) Массив k и длины строк
	// -----------------------------
	double ks[] = { 1.0 / 25, 1.0 / 20, 1.0 / 15, 1.0 / 10, 1.0 / 5, 1.0 / 2, 1.0 };
	int countK = sizeof(ks) / sizeof(ks[0]);


	int lenX = MAX_X;
	int lenY = MAX_Y;

	std::cout << "-- Расстояние Левенштейна для различных k --\n";
	std::cout << "len(X) = " << lenX << ", len(Y) = " << lenY << "\n\n";
	std::cout << std::setw(8) << "k"
		<< std::setw(10) << "lenX_k"
		<< std::setw(10) << "lenY_k"
		<< std::setw(12) << "LevRec"
		<< std::setw(10) << "tRec"
		<< std::setw(12) << "LevDP"
		<< std::setw(10) << "tDP"
		<< std::endl;

	// ------------------------------------------
	// 3) Цикл по всем k: вычисляем расстояния
	// ------------------------------------------
	for (int i = 0; i < countK; i++)
	{
		double kVal = ks[i];

		int lenX_k = (int)(kVal * lenX);
		int lenY_k = (int)(kVal * lenY);


		clock_t t1 = clock();
		int distRec = levenshtein_r(lenX_k, x, lenY_k, y);
		clock_t t2 = clock();


		clock_t t3 = clock();
		int distDP = levenshtein(lenX_k, x, lenY_k, y);
		clock_t t4 = clock();


		long long timeRec = (long long)(t2 - t1);
		long long timeDP = (long long)(t4 - t3);

		// ------------------------------------------
		// 4) Вывод результатов для текущего k
		// ------------------------------------------
		std::cout << std::fixed << std::setprecision(3)
			<< std::setw(8) << kVal
			<< std::setw(10) << lenX_k
			<< std::setw(10) << lenY_k
			<< std::setw(12) << distRec
			<< std::setw(10) << timeRec
			<< std::setw(12) << distDP
			<< std::setw(10) << timeDP
			<< std::endl;
	}

	system("pause");
	return 0;
}
