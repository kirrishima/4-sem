#ifndef GRAPH_H
#define GRAPH_H

#include <iostream>
#include <vector>
#include <queue>

using namespace std;

class AMatrix {
public:
	vector<vector<int>> mat;
	int n; // число вершин

	// Конструктор: инициализация n x n матрицы нулями
	AMatrix(int n) : n(n) {
		mat.assign(n, vector<int>(n, 0));
	}

	// Функция добавления ориентированного ребра из u в v
	void addEdge(int u, int v) {
		if (u >= 0 && u < n && v >= 0 && v < n) {
			mat[u][v] = 1;
		}
	}

	// Вывод матрицы смежности на экран
	void print() const {
		for (int i = 0; i < n; i++) {
			for (int j = 0; j < n; j++)
				cout << mat[i][j] << " ";
			cout << endl;
		}
	}
};


class AList {
public:
	vector<vector<int>> list;
	int n; // число вершин

	// Конструктор: инициализация списка для n вершин
	AList(int n) : n(n) {
		list.resize(n);
	}

	// Функция добавления ребра из u в v
	void addEdge(int u, int v) {
		if (u >= 0 && u < n) {
			list[u].push_back(v);
		}
	}

	// Вывод списка смежности на экран
	void print() const {
		for (int i = 0; i < n; i++) {
			cout << i << ": ";
			for (int v : list[i])
				cout << v << " ";
			cout << endl;
		}
	}
};

// Функция преобразования графа, заданного матрицей смежности, в список смежности
AList convertMatrixToList(const AMatrix& matrix);

// Функция преобразования графа, заданного списком смежности, в матрицу смежности
AMatrix convertListToMatrix(const AList& aList);

// Функция обхода графа в ширину (BFS) по списку смежности.
// Аргументы: graph - граф, start - стартовая вершина.
void BFS(const AList& graph, int start);

// Прототипы функций DFS
void DFS(const AList& graph, int start);

void TOP_DFS(const AList& graph, int start);
#endif // GRAPH_H
