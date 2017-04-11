//
//  main.cpp
//  data-structures-maze-pathfinding
//

#include <ctime>
#include <iostream>
#include "fileloader.h"
#include "gamemap.h"

int main(int argc, const char * argv[]) {
	FileLoader fl;
	if (argc<2) {
		std::cout << "Please enter a filename." << std::endl;
		return 1;
	}
	std::string filename = argv[1];
	try {
		GameMap gm(fl.loadFile(filename.c_str()));
		gm.end = fl.end;
		if (argc>2) {
			gm.NUM_CYCLES_TIMEOUT = atoi(argv[2]);
		}
		else gm.NUM_CYCLES_TIMEOUT = gm.end->y * 100;
		std::cout << "Attemption to solve map " << filename << std::endl;
		clock_t begin = clock();
		gm.aStarPathFind();
		clock_t end = clock();
		std::cout << "It took " << double(end - begin) / CLOCKS_PER_SEC
			<< " seconds to solve map " << filename << std::endl;

		fl.saveFile(gm.map, gm.path, filename.c_str(), gm.end);
	}
	// catch invalid filename
	catch (std::invalid_argument e) {
		std::cout << e.what() << std::endl;
		return 1;
	}
	// catch no map path or cycle timeout
	catch (std::length_error e) {
		std::cout << e.what() << std::endl;
		return 2;
	}

	return 0;
}
