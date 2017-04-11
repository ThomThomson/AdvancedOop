#pragma once
#ifndef stack_hpp
#define stack_hpp

#include <stdio.h>
#include <stdexcept>

#include "point.h"

class Stack {
private:
	int length;
	Point** items;
public:
	void push_back(Point* data);
	Point* pop_back();
	Point* peek();
	bool isEmpty();
	int size();
};
#endif /* stack_hpp */
