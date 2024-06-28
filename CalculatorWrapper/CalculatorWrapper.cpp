// CalculatorWrapper.cpp
#include "CalculatorWrapper.h"
#include <math.h>

namespace CalculatorWrapper {

    double Calculator::CalculateDistanceInMeters(double lat1, double lon1, double lat2, double lon2) {
        double dLat = DegreesToRadians(lat2 - lat1);
        double dLon = DegreesToRadians(lon2 - lon1);
        double a = sin(dLat / 2) * sin(dLat / 2) +
            cos(DegreesToRadians(lat1)) * cos(DegreesToRadians(lat2)) *
            sin(dLon / 2) * sin(dLon / 2);
        double c = 2 * atan2(sqrt(a), sqrt(1 - a));
        return 6371000.0 * c;  // Earth radius in meters
    }

    double Calculator::DegreesToRadians(double degrees) {
        return degrees * 3.14159265358979323846 / 180.0;
    }

    double Calculator::ComputeSimilarityScore(String^ s, String^ keyword) {
        if (s == nullptr || keyword == nullptr) {
            return 0.0; 
        }
        int m = s->Length;
        int n = keyword->Length;
        array<int, 2>^ dp = gcnew array<int, 2>(m + 1, n + 1);

        for (int i = 0; i <= m; i++) {
            for (int j = 0; j <= n; j++) {
                if (i == 0 || j == 0)
                    dp[i, j] = 0;
                else if (s[i - 1] == keyword[j - 1])
                    dp[i, j] = dp[i - 1, j - 1] + 1;
                else
                    dp[i, j] = Math::Max(dp[i - 1, j], dp[i, j - 1]);
            }
        }
        return static_cast<double>(dp[m, n]) / Math::Max(m, n);
    }

}
