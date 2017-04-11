//
//  queue.cpp
//  data-structures-maze-pathfinding
//

#include "queue.h"

void Queue::push_back(char data) {
	char* temp = new char[length];
	for (int i = 0; i < length; i++) {
		temp[i] = items[i];
	}
	length++;
	items = new char[length];
	for (int i = 0; i < length - 1; i++) {
		items[i] = temp[i];
	}
	items[length - 1] = data;
	delete[] temp;
}

char Queue::pop() {
	if (this->isEmpty()) {
		throw std::out_of_range("Queue is empty.");
	}
	char data = items[0];
	length--;
	char* temp = new char[length];
	for (int i = 0; i < length; i++) {
		temp[i] = items[i + 1];
	}
	items = new char[length];
	for (int i = 0; i < length; i++) {
		items[i] = temp[i];
	}
	delete[] temp;

	return data;
}

char Queue::peek() {
	if (this->isEmpty()) {
		throw std::out_of_range("Queue is empty.");
	}
	return items[0];
}

bool Queue::isEmpty() {
	return (length == 0);
}

int Queue::size() {
	return length;
}
