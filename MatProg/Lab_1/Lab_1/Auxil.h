#ifndef AUXIL_H
#define AUXIL_H

namespace auxil
{
	void start();

	double  dget(
		double rmin,   // [in] ����������� ��������   
		double rmax    // [in] ������������ ��������
	);

	int  iget(
		int rmin,       // [in] ����������� ��������   
		int rmax        // [in] ������������ ��������
	);

}

#endif // !AUXIL_H