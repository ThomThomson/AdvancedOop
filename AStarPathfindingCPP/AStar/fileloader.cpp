//
//  fileloader.cpp
//  data-structures-maze-pathfinding
//

#include "fileloader.h"

//  takes a filename, and a pointer to a linked list
//  saves every line to a node in the linked list
Point*** FileLoader::loadFile(const char* filename) {

	std::ifstream ifs(filename);
	Point*** map = new Point**[0];
	rowLength = 0;
	numRows = 0;
	bool b = true;

	// if the file isn't open, the argument is invalid
	if (ifs.fail()) {
		throw std::invalid_argument("Invalid file name");
	}

	// load every line of the file into the string, including whitespace.
	while (!ifs.eof()) {

		std::string line;
		getline(ifs, line);

		if (line.length() > rowLength) rowLength = (int)line.length();

		Point*** temp = new Point**[numRows];
		if (!b) {
			for (int i = 0; i < numRows; i++) {
				temp[i] = new Point*[rowLength];
				for (int j = 0; j < rowLength; j++) {
					temp[i][j] = map[i][j];
				}
			}
		}
		else b = false;

		numRows++;

		map = new Point**[numRows];
		for (int i = 0; i < numRows - 1; i++) {
			map[i] = new Point*[rowLength];
			for (int j = 0; j < rowLength; j++) {
				map[i][j] = temp[i][j];
			}
		}
		map[numRows - 1] = new Point*[rowLength];

		for (int y = 0; y < rowLength; y++) {
			int x = numRows - 1;
			char c = line[y];
			Point* point = new Point(x, y, c, c == ' ');
			map[x][y] = point;
		}

		for (int i = 0; i < numRows - 1; i++) {
			delete[] temp[i];
		}
		delete[] temp;
	}
	// close the stream
	ifs.close();
	this->end = map[numRows - 3][rowLength - 1];
	return map;
}

//  takes a pointer to a linked list, and a filename
//  saves every node to the given file
void FileLoader::saveFile(Point*** map, Stack path, const char* filename, Point* end) {
	std::string outfilename = filename;
	outfilename += "_solved";
	std::ofstream ofs(outfilename, std::fstream::out | std::fstream::trunc);

	// if the file isn't open, the argument is invalid
	if (ofs.fail()) throw std::invalid_argument("Invalid file name");

	// output the map to file

	char** a = new char*[end->x + 2];
	for (int i = 0; i < end->x + 2; i++) {
		a[i] = new char[end->y + 1];
		for (int j = 0; j < end->y + 1; j++) {
			a[i][j] = map[i][j]->data;
		}
	}
	while (true) {
		if (path.isEmpty()) break;
		Point* point = path.pop_back();
		if (point == NULL) break;
		a[point->x][point->y] = '#';
	}

	for (int i = 0; i < end->x + 2; i++) {
		for (int j = 0; j < end->y + 1; j++) {
			// output the character array to file;
			ofs << a[i][j];
		}
		ofs << std::endl;
	}
	// close the stream
	ofs.close();
}

// found this on stackoverflow. thanks to user763305 & Jimbo,
// and Aaron McDaid for asking the question so I didn't have to.
// http://stackoverflow.com/questions/6089231/getting-std-ifstream-to-handle-lf-cr-and-crlf
// also good on these guys for giving an answer that isn't "use boost"
std::istream& FileLoader::safeGetLine(std::istream& is, std::string& t)
{
	t.clear();

	// The characters in the stream are read one-by-one using a std::streambuf.
	// That is faster than reading them one-by-one using the std::istream.
	// Code that uses streambuf this way must be guarded by a sentry object.
	// The sentry object performs various tasks,
	// such as thread synchronization and updating the stream state.

	std::istream::sentry se(is, true);
	std::streambuf* sb = is.rdbuf();

	for (;;) {
		int c = sb->sbumpc();
		switch (c) {
		case '\n':
			return is;
		case '\r':
			if (sb->sgetc() == '\n')
				sb->sbumpc();
			return is;
		case EOF:
			// Also handle the case when the last line has no line ending
			if (t.empty())
				is.setstate(std::ios::eofbit);
			return is;
		default:
			t += (char)c;
		}
	}
}
