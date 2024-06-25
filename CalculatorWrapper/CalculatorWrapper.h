// CalculatorWrapper.h
#pragma once
using namespace System;

namespace CalculatorWrapper {
    public ref class Calculator {
    public:
        static double CalculateDistanceInMeters(double lat1, double lon1, double lat2, double lon2);
        static double ComputeSimilarityScore(String^ s, String^ keyword);
    private:
        static double DegreesToRadians(double degrees);
    };
}
