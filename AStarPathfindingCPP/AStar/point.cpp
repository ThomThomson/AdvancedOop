//
//  point.cpp
//  data-structures-maze-pathfinding
//

#include "point.h"

void Point::calculate(Point* end) {
	if (parent != NULL) {
		gCost = parent->gCost;
		gCost += ((parent->x>this->x) ? parent->x - this->x : this->x - parent->x)
			+ ((parent->y>this->y) ? parent->y - this->y : this->y - parent->y);
	}
	else {
		gCost = 0;
	}
	hCost = ((end->x>this->x) ? end->x - this->x : this->x - end->x)
		+ ((end->y>this->y) ? end->y - this->y : this->y - end->y);
	fCost = gCost + hCost;

}

void Point::calculate(Point* newParent, Point* end) {
	this->parent = newParent;
	calculate(end);
}

bool Point::newParentIsBetter(Point* testParent) {
	return testParent->gCost < parent->gCost;
}
