#ifndef AUXIL_H
#define AUXIL_H

namespace auxil
{
	void start();

	double  dget(
		double rmin,   // [in] минимальное значение   
		double rmax    // [in] максимальное значение
	);

	int  iget(
		int rmin,       // [in] минимальное значение   
		int rmax        // [in] максимальное значение
	);

}

#endif // !AUXIL_H