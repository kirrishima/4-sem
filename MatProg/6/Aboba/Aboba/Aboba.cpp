#include <iostream>
#include "Graph.h"



using namespace std;

int main() {

	setlocale(LC_ALL, "Russian");
	// Пример: создадим граф из 7 вершин.
	// Пусть ориентированный граф имеет следующие ребра:
	// 0 -> 1, 0 -> 3, 2 -> 5, 3 -> 1, 3 -> 4, 3 -> 6, 3 -> 2, 4 -> 1, 4 -> 6, 5 -> 6

	int n = 7; // число вершин
	AMatrix matrix(n);
	matrix.addEdge(0, 1);
	matrix.addEdge(0, 3);
	matrix.addEdge(2, 5);
	matrix.addEdge(3, 1);
	matrix.addEdge(3, 4);
	matrix.addEdge(3, 6);
	matrix.addEdge(3, 2);
	matrix.addEdge(4, 1);
	matrix.addEdge(4, 6);
	matrix.addEdge(5, 6);

	cout << "Матрица смежности:" << endl;
	matrix.print();

	// Преобразуем матрицу смежности в список смежности
	AList aList = convertMatrixToList(matrix);
	cout << "\nСписок смежности:" << endl;
	aList.print();

	// Преобразуем список обратно в матрицу для проверки
	AMatrix matrix2 = convertListToMatrix(aList);
	cout << "\nПреобразованная матрица:" << endl;
	matrix2.print();

	// Выполним обход графа в ширину (BFS), начиная с вершины 0
	cout << "\nВыполнение BFS обхода:" << endl;
	BFS(aList, 0);

	// Выполним обход графа в ширину (BFS), начиная с вершины 0
	cout << "\nВыполнение DFS обхода:" << endl;
	DFS(aList, 0);
	// В отчёт необходимо сделать копии экрана (скриншоты) работы программы,
	// например, с выводом матрицы, списка и результатом обхода.

	TOP_DFS(aList, 0);
	return 0;
}
