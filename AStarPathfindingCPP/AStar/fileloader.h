#pragma once
#ifndef fileloader_hpp
#define fileloader_hpp

#include <fstream>
#include <iostream>
#include <stdio.h>
#include <string>

#include "point.h"
#include "stack.h"

class FileLoader {
	int numRows;
	int rowLength;
	std::istream& safeGetLine(std::istream& is, std::string& t);
public:
	Point* end;
	Point*** loadFile(const char* filename);
	void saveFile(Point*** map, Stack path, const char* filename, Point* end);
};

#endif /* fileloader_hpp */