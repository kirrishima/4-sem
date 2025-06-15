
#pragma once 
namespace combi3
{
	struct  permutation    // генератор   перестановок     
	{
		const static bool L = true;  // левая стрелка 
		const static bool R = false; // правая стрелка   
		short  n;         // количество элементов исходного множества 
		short* sset;        // массив индексов текущей перестановки
		bool* dart;        // массив  стрелок (левых-L и правых-R) 
		permutation(short n = 1); // конструктор (кол-во эл-ов исх. мн-ва) 
		void reset();             // сбросить генератор, начать сначала 
		long long getfirst();       // сформировать первый массив индексов    
		long long getnext();        // сформировать случайный массив индексов  
		short ntx(short i);       // получить i-й элемент масива индексов 
		unsigned long long np;      // номер перествновки 0,... count()-1 
		unsigned long long count() const; // вычислить общее кол. перестановок    
	};
};
