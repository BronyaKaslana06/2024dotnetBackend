// SnowflakeIDcreator.h
#pragma once

#include <chrono>
#include <mutex>

class SnowflakeIDcreator
{
private:
    static const long long twepoch;
    static const int workerIdBits = 4;
    static const int sequenceBits = 10;
    static const int workerIdShift;
    static const int timestampLeftShift;
    static const long long sequenceMask;
    static long long lastTimestamp;
    static long long sequence;
    static int workerId;
    static std::mutex mtx;

public:
    static void SetWorkerID(int id);
    static long long nextId();
    static long long tillNextMillis(long long lastTimestamp);
    static long long timeGen();
};


