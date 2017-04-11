//
//  gamemap.cpp
//  data-structures-maze-pathfinding
//

#include "gamemap.h"

GameMap::GameMap(Point*** map) :
	map(map), start(map[1][0]) {}

void GameMap::aStarPathFind() {
	Point* current;
	Point* child;
	List openPoints;

	if (end == NULL) throw std::invalid_argument("Point End not defined");

	start->calculate(end);
	openPoints.push_back(start);
	start->opened = true;
	current = start;
	int n = 0;

	while (true) {
		n++;
		if (n > NUM_CYCLES_TIMEOUT) {
			std::string message = "Timeout: Map took more than ";
			message += std::to_string(NUM_CYCLES_TIMEOUT);
			message += " cycles to solve.";
			throw std::length_error(message);
		}
		if (openPoints.isEmpty()) {
			throw std::length_error("No available path through map");
		}
		// look for the Point with the lowest fCost, or the first available one, I guess
		for (int i = 0; i < openPoints.size(); i++) {
			if (openPoints.isEmpty()) continue;
			if (openPoints.get(i) == NULL) {
				continue;
			}
			if (i == 0 || openPoints.get(i)->fCost < current->fCost) {
				current = openPoints.get(i);
			}
			else if (openPoints.get(i)->fCost == current->fCost &&
				openPoints.get(i)->hCost <= current->hCost) {
				current = openPoints.get(i);
			}
		}

		if (current == end) break;

		openPoints.remove(current);
		current->opened = false;
		current->closed = true;

		for (int i = -1; i < 2; i++) {
			for (int j = -1; j < 2; j++) {
				// if i and j are the current point's x and y, or the movement is diagonal, skip this.
				if ((i == 0 && j == 0) || (i != 0 && j != 0)) continue;
				if (current->x + i < start->x ||
					current->y + j < start->y ||
					current->x + i > end->x ||
					current->y + j > end->y) continue;

				child = map[current->x + i][current->y + j];
				// if that point has already been checked or is a wall, skip this.
				if (child->closed || !child->traversable) continue;

				if (child->opened) {
					if (child->newParentIsBetter(current)) {
						child->calculate(current, end);
					}
				}
				else {
					child->opened = true;
					child->calculate(current, end);
					openPoints.push_back(child);
				}
			}
		}
	}

	current = end;
	while (current != NULL) {
		path.push_back(current);
		current = current->parent;
	}
}
















