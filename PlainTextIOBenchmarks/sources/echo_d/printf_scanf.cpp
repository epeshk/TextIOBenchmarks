#include <iostream>
#include <ios>
using namespace std;

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);

    int t;
    scanf("%d", &t);

    for (int i = 0; i < t; ++i) {
        double a, b, c, d;
        scanf("%lf%lf%lf%lf", &a, &b, &c, &d);
        printf("%.0lf %.0lf %.0lf %.0lf\n", a, b, c, d);
    }
}