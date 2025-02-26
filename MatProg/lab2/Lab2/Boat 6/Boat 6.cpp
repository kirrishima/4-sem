// ============================  ЗАВИСИМОСТЬ ВРЕМЕНИ ВЫЧИСЛЕНИЯ  (Task6)  ============================
#include <iostream>
#include <iomanip>
#include "../Boat 5/Boat.h"
#include <ctime>
#include <string>

#define NN (sizeof(v)/sizeof(int))
#define MM 6

int main()
{
	setlocale(LC_ALL, "rus");
	int V = 1500;
	int v[] = { 250, 560, 670, 400, 200, 270, 370, 330, 330, 440, 530, 120,
				200, 270, 370, 330, 330, 440, 700, 120, 550, 540, 420, 170,
				600, 700, 120, 550, 540, 420, 430, 140, 300, 370, 310 };
	int c[NN] = { 15,26, 27, 43, 16, 26, 42, 22, 34, 12, 33, 30,
				  42,22, 34, 43, 16, 26, 14, 12, 25, 41, 17, 28,
				  12,45, 60, 41, 33, 11, 14, 12, 25, 41, 30 };
	short r[MM];
	int maxcc = 0;
	clock_t t1, t2;

	std::cout << "\nЗадача об оптимальной загрузке судна\n";
	std::cout << "Ограничение по весу: " << V << "\nКоличество мест: " << MM << "\n\n";

	// Заголовок таблицы
	std::cout << std::left
		<< std::setw(25) << "Количество контейнеров"
		<< std::setw(20) << "Время (такт)"
		<< std::setw(20) << "Время (сек)"
		<< std::endl;
	std::cout << std::string(65, '-') << std::endl;

	for (int i = 25; i <= NN; i++)
	{
		t1 = clock();
		maxcc = boat(V, MM, i, v, c, r);
		t2 = clock();
		double seconds = (t2 - t1) / static_cast<double>(CLOCKS_PER_SEC);

		std::cout << std::setw(25) << i
			<< std::setw(20) << (t2 - t1)
			<< std::setw(20) << std::fixed << std::setprecision(6) << seconds
			<< std::endl;
	}

	std::cout << std::endl;
	system("pause");
	return 0;
}
