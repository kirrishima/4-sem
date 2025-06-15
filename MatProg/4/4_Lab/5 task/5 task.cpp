#include <iostream>
#include <ctime>
#include <iomanip>
#include "LCS.h"

int main()
{
	setlocale(LC_ALL, "rus");

	const char x[] = "ABCDFGI";
	const char y[] = "EATUFI";

	std::cout << "-- Рекурсивный метод LCS --" << std::endl;
	std::cout << "Последовательность X: " << x << std::endl;
	std::cout << "Последовательность Y: " << y << std::endl;

	clock_t t1 = clock();
	int lcs_length_rec = lcs(sizeof(x) - 1, x, sizeof(y) - 1, y);
	clock_t t2 = clock();
	double time_rec = double(t2 - t1);

	std::cout << "Длина LCS (рекурсия): " << lcs_length_rec << std::endl;
	std::cout << "Время (рекурсия): " << time_rec << std::endl;

	std::cout << "\n-- Динамическое программирование LCS --" << std::endl;
	char z[100] = { 0 };

	clock_t t3 = clock();
	int lcs_length_dp = lcsd(x, y, z);
	clock_t t4 = clock();
	double time_dp = double(t4 - t3);

	std::cout << "Последовательность X: " << x << std::endl;
	std::cout << "Последовательность Y: " << y << std::endl;
	std::cout << "Наибольшая общая подпоследовательность (LCS): " << z << std::endl;
	std::cout << "Длина LCS (динамическое программирование): " << lcs_length_dp << std::endl;
	std::cout << "Время (динамическое программирование): " << time_dp << std::endl;

	return 0;
}
