// SnowflakeIDcreator.cpp
#include "pch.h"
#include "SnowflakeIDcreator.h"

const long long SnowflakeIDcreator::twepoch = 68020LL;
const int SnowflakeIDcreator::workerIdShift = SnowflakeIDcreator::sequenceBits;
const int SnowflakeIDcreator::timestampLeftShift = SnowflakeIDcreator::sequenceBits + SnowflakeIDcreator::workerIdBits;
const long long SnowflakeIDcreator::sequenceMask = -1LL ^ (-1LL << SnowflakeIDcreator::sequenceBits);
long long SnowflakeIDcreator::lastTimestamp = -1LL;
long long SnowflakeIDcreator::sequence = 0LL;
int SnowflakeIDcreator::workerId = 1;
std::mutex SnowflakeIDcreator::mtx;

void SnowflakeIDcreator::SetWorkerID(int id)
{
    workerId = id;
}

long long SnowflakeIDcreator::nextId()
{
    std::lock_guard<std::mutex> lock(mtx);
    long long timestamp = timeGen();

    if (lastTimestamp == timestamp) {
        sequence = (sequence + 1) & sequenceMask;
        if (sequence == 0) {
            timestamp = tillNextMillis(lastTimestamp);
        }
    }
    else {
        sequence = 0;
    }

    if (timestamp < lastTimestamp) {
        throw std::runtime_error("Clock moved backwards.");
    }

    lastTimestamp = timestamp;
    return ((timestamp - twepoch) << timestampLeftShift) | (workerId << workerIdShift) | sequence;
}

long long SnowflakeIDcreator::tillNextMillis(long long lastTimestamp)
{
    long long timestamp = timeGen();
    while (timestamp <= lastTimestamp) {
        timestamp = timeGen();
    }
    return timestamp;
}

long long SnowflakeIDcreator::timeGen()
{
    auto now = std::chrono::system_clock::now().time_since_epoch();
    return std::chrono::duration_cast<std::chrono::milliseconds>(now).count();
}
