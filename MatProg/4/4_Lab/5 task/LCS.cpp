#include "LCS.h"
#include <cstring>
#include <algorithm>

// ------------------------------
// Рекурсивное решение LCS
// ------------------------------
int lcs(int lenx, const char x[], int leny, const char y[])
{
	int rc = 0;
	if (lenx > 0 && leny > 0)
	{
		if (x[lenx - 1] == y[leny - 1])
			rc = 1 + lcs(lenx - 1, x, leny - 1, y);
		else
			rc = std::max(lcs(lenx, x, leny - 1, y), lcs(lenx - 1, x, leny, y));
	}
	return rc;
}

// ------------------------------
// Динамическое программирование для LCS
// ------------------------------

#define LCS_C(i,j)  (C[(i)*(leny+1)+(j)])
#define LCS_B(i,j)  (B[(i)*(leny+1)+(j)])
#define LCS_X(i)    (x[(i)-1])
#define LCS_Y(i)    (y[(i)-1])
#define LCS_Z(i)    (z[(i)-1])

enum Dart { TOP, LEFT, LEFTTOP };

void getLCScontent(int lenx, int leny, const char x[],
	const Dart* B, int n, int i, int j, char z[])
{
	if (i > 0 && j > 0 && n > 0)
	{
		if (LCS_B(i, j) == LEFTTOP)
		{
			getLCScontent(lenx, leny, x, B, n - 1, i - 1, j - 1, z);
			LCS_Z(n) = LCS_X(i);
			LCS_Z(n + 1) = '\0';
		}
		else if (LCS_B(i, j) == TOP)
			getLCScontent(lenx, leny, x, B, n, i - 1, j, z);
		else
			getLCScontent(lenx, leny, x, B, n, i, j - 1, z);
	}
}

int lcsd(const char x[], const char y[], char z[])
{
	int lenx = std::strlen(x);
	int leny = std::strlen(y);
	int* C = new int[(lenx + 1) * (leny + 1)];
	Dart* B = new Dart[(lenx + 1) * (leny + 1)];

	std::fill(C, C + (lenx + 1) * (leny + 1), 0);

	for (int i = 1; i <= lenx; i++)
	{
		for (int j = 1; j <= leny; j++)
		{
			if (LCS_X(i) == LCS_Y(j))
			{
				LCS_C(i, j) = LCS_C(i - 1, j - 1) + 1;
				LCS_B(i, j) = LEFTTOP;
			}
			else if (LCS_C(i - 1, j) >= LCS_C(i, j - 1))
			{
				LCS_C(i, j) = LCS_C(i - 1, j);
				LCS_B(i, j) = TOP;
			}
			else
			{
				LCS_C(i, j) = LCS_C(i, j - 1);
				LCS_B(i, j) = LEFT;
			}
		}
	}
	int lcs_length = LCS_C(lenx, leny);
	getLCScontent(lenx, leny, x, B, lcs_length, lenx, leny, z);

	delete[] C;
	delete[] B;

	return lcs_length;
}

#undef LCS_Z
#undef LCS_C
#undef LCS_B
#undef LCS_X
#undef LCS_Y
