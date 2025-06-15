// - main  

#include <iostream>
#include "Levenshtein.h"
int main()
{
	setlocale(LC_ALL, "rus");
	char X[] = "лом", Y[] = "гомон";
	std::cout << std::endl << "-- дистанция Левенштейна (рекурсия)";
	std::cout << std::endl << X << " --> " << Y << " = "
		<< levenshtein_r(sizeof(X) - 1, X, sizeof(Y) - 1, Y) << std::endl;
	system("pause");
	return 0;
}
