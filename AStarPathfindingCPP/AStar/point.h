#pragma once
#ifndef point_hpp
#define point_hpp

#include <stdio.h>

class Point {
public:
	char data;
	Point* parent = NULL;
	bool closed = false;
	bool opened = false;
	bool traversable;

	int x;
	int y;
	int fCost = -1;
	int gCost;
	int hCost;


	Point() {};
	Point(int x, int y, char data, bool traversable) :
		x(x), y(y), data(data), traversable(traversable) {}

	void calculate(Point* end);
	void calculate(Point* newParent, Point* end);
	bool newParentIsBetter(Point* testParent);
};

#endif /* point_hpp */
