#pragma once
#ifndef gamemap_hpp
#define gamemap_hpp

#include <iostream>
#include <stdexcept>
#include <stdio.h>
#include <stdlib.h>
#include <string>
#include <type_traits>
#include "point.h"
#include "stack.h"
#include "queue.h"
#include "list.h"

class GameMap {
public:
	std::string filename;
	Point*** map;
	Point* start;
	Point* end;
	Stack path;
	int NUM_CYCLES_TIMEOUT;
	void aStarPathFind();
	GameMap(Point*** map);
};

#endif /* gamemap_hpp */
