// ============================  ����������� ������� ����������  (Task6)  ============================
#include <iostream>
#include <iomanip>
#include "../Boat 5/Boat.h"
#include <time.h>

#define NN (sizeof(v)/sizeof(int))
#define MM 6
#define SPACE(n) std::setw(n)<<" "

int main()
{
	setlocale(LC_ALL, "rus");
	int V = 1500,
		v[] = { 250, 560, 670, 400, 200, 270, 370, 330, 330, 440, 530, 120,
			   200, 270, 370, 330, 330, 440, 700, 120, 550, 540, 420, 170,
			   600, 700, 120, 550, 540, 420, 430, 140, 300, 370, 310 };
	int c[NN] = { 15,26,  27,  43,  16,  26,  42,  22,  34,  12,  33,  30,
			   42,22,  34,  43,  16,  26,  14,  12,  25,  41,  17,  28,
			   12,45,  60,  41,  33,  11,  14,  12,  25,  41,  30 };
	short r[MM];
	int maxcc = 0;
	clock_t t1, t2;
	std::cout << std::endl << "-- ������ �� ����������� �������� ����� -- ";
	std::cout << std::endl << "-  ����������� �� ����    : " << V;
	std::cout << std::endl << "-  ���������� ����        : " << MM;
	std::cout << std::endl << "-- ���������� ------ ����������������� -- ";
	std::cout << std::endl << "   �����������        ����������  ";
	for (int i = 25; i <= NN; i++)
	{
		t1 = clock();
		maxcc = boat(V, MM, i, v, c, r);
		t2 = clock();
		std::cout << std::endl << SPACE(7) << std::setw(2) << i
			<< SPACE(15) << std::setw(5) << (t2 - t1);
	}

	std::cout << std::endl << std::endl;
	system("pause");
	return 0;
}