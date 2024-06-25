// EasyIDCreator.cpp
#include "pch.h"
#include "EasyIDCreator.h"

std::vector<long> EasyIDCreator::allIds;
std::vector<std::string> EasyIDCreator::allId;
std::mt19937 EasyIDCreator::randEngine;
bool EasyIDCreator::initialized = false;

void EasyIDCreator::Initialize() {
    if (!initialized) {
        std::random_device rd;
        randEngine.seed(rd());
        initialized = true;
    }
}

std::string EasyIDCreator::CreateId_New(int entityType) {
    Initialize();

    const int rangeFrom = 0;
    const int rangeTo = 99999999;
    std::uniform_int_distribution<int> dist(rangeFrom, rangeTo);

    while (true) {
        std::string id = std::to_string(dist(randEngine));
        std::string prefix = "";
        switch (static_cast<EntityType>(entityType)) {
        case EntityType::Administrator:
            prefix = "2";
            break;
        case EntityType::Employee:
            prefix = "1";
            break;
        case EntityType::VehicleOwner:
            prefix = "0";
            break;
        }
        std::string idWithPrefix = prefix + id;
        if (std::find(allId.begin(), allId.end(), idWithPrefix) == allId.end()) {
            allId.push_back(idWithPrefix);
            return idWithPrefix;
        }
    }
}

long EasyIDCreator::CreateId() {
    Initialize();

    const long long rangeFrom = 10000000;
    const long long rangeTo = 99999999;
    std::uniform_int_distribution<long long> dist(rangeFrom, rangeTo);

    while (true) {
        long id = dist(randEngine);
        if (std::find(allIds.begin(), allIds.end(), id) == allIds.end()) {
            allIds.push_back(id);
            return id;
        }
    }
}