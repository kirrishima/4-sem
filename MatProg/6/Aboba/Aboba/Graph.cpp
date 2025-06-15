#include "Graph.h"

#include <iostream>
#include <vector>
#include <queue>
#include <stack>
#include <algorithm>

using namespace std;

AList convertMatrixToList(const AMatrix& matrix) {
	AList aList(matrix.n);
	for (int i = 0; i < matrix.n; i++) {
		for (int j = 0; j < matrix.n; j++) {
			if (matrix.mat[i][j] != 0) {
				aList.addEdge(i, j);
			}
		}
	}
	return aList;
}


AMatrix convertListToMatrix(const AList& aList) {
	AMatrix matrix(aList.n);
	for (int i = 0; i < aList.n; i++) {
		for (int v : aList.list[i]) {
			matrix.addEdge(i, v);
		}
	}
	return matrix;
}


void BFS(const AList& graph, int start) {
	int n = graph.n;
	vector<bool> visited(n, false);
	queue<int> q;

	// Инициализация обхода: стартовая вершина
	q.push(start);
	visited[start] = true;

	cout << "BFS обход (начиная с вершины " << start << "): ";
	while (!q.empty()) {
		int u = q.front();
		q.pop();
		cout << u << " ";

		// Проходим по всем соседям вершины u
		for (int v : graph.list[u]) {
			if (!visited[v]) {
				visited[v] = true;
				q.push(v);
			}
		}
	}
	cout << endl;
}


// Итеративный DFS обход с использованием стека (без рекурсии)
void DFS(const AList& graph, int start) {
	int n = graph.n;
	vector<bool> visited(n, false);
	stack<int> st;
	st.push(start);
	visited[start] = true;
	cout << "DFS обход (начиная с вершины " << start << "): ";

	while (!st.empty()) {
		int v = st.top();
		st.pop();



		cout << v << " ";

		// Для корректного порядка обхода добавляем соседей в стек в обратном порядке.
		for (int i = graph.list[v].size() - 1; i >= 0; i--) {
			int neighbor = graph.list[v][i];
			if (!visited[neighbor]) {
				st.push(neighbor);
				visited[neighbor] = true;
			}
		}

	}
	cout << endl;
}

struct StackItem {
	int v;
	int nextIndex;
};

void TOP_DFS(const AList& graph, int start) {
	int n = graph.n;
	std::vector<bool> visited(n, false);
	std::vector<int> topoOrder; // Будет хранить вершины в порядке завершения обработки
	std::stack<StackItem> st;

	// Лямбда-функция для итеративного DFS, начинающегося с вершины i.
	auto pushDfs = [&](int i) {
		st.push({ i, 0 });
		while (!st.empty()) {
			StackItem& item = st.top();
			int v = item.v;

			visited[v] = true;

			// Если у вершины v есть еще не обработанный сосед, переходим к нему.
			if (item.nextIndex < graph.list[v].size()) {
				int neighbor = graph.list[v][item.nextIndex];
				item.nextIndex++; // Переходим к следующему соседу при следующей итерации
				if (!visited[neighbor]) {
					st.push({ neighbor, 0 });
				}
			}
			else {
				// Все соседи обработаны: добавляем вершину в итоговый порядок и убираем из стека.
				topoOrder.push_back(v);
				st.pop();
			}
		}
		};

	// Обходим граф: сначала вершины от start до n-1, затем от 0 до start-1,
	// чтобы покрыть все компоненты графа.
	for (int i = start; i < n; i++) {
		if (!visited[i]) {
			pushDfs(i);
		}
	}
	for (int i = 0; i < start; i++) {
		if (!visited[i]) {
			pushDfs(i);
		}
	}

	// Реверсируем список, чтобы получить корректный топологический порядок.
	std::reverse(topoOrder.begin(), topoOrder.end());

	std::cout << "Топологическая сортировка: ";
	for (int v : topoOrder) {
		std::cout << v << " ";
	}
	std::cout << std::endl;
}