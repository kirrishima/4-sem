#ifndef LCS_H
#define LCS_H

// –екурсивное вычисление длины LCS
// lenx Ц длина последовательности x, leny Ц длина последовательности y
// x, y Ц входные последовательности
int lcs(int lenx, const char x[], int leny, const char y[]);

// ¬ычисление LCS методом динамического программировани€
// x, y Ц входные последовательности, z Ц буфер дл€ результата (LCS)
// ‘ункци€ возвращает длину LCS, а строка LCS записываетс€ в z.
int lcsd(const char x[], const char y[], char z[]);

#endif // LCS_H
