#pragma once
#ifndef queue_hpp
#define queue_hpp

#include <stdexcept>
#include <stdio.h>

class Queue {
private:
	int length;
	char* items;
public:
	void push_back(char data);
	char pop();
	char peek();
	bool isEmpty();
	int size();
};
#endif /* queue_hpp */
