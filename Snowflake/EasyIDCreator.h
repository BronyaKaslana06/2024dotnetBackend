// EasyIDCreator.h
#pragma once

#include <vector>
#include <string>
#include <algorithm>
#include <random>
#include "EntityType.h"  // Include the EntityType enum header

class EasyIDCreator {
private:
    static std::vector<long> allIds;
    static std::vector<std::string> allId;
    static std::mt19937 randEngine;
    static bool initialized;

public:
    static std::string CreateId_New(int entityType);
    static long CreateId();
    static void Initialize();  // This method will be used to initialize the random engine and other static members if needed
};