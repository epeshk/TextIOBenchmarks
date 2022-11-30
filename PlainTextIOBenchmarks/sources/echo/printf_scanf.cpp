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
        int a, b, c, d;
        scanf("%d%d%d%d", &a, &b, &c, &d);
        printf("%d %d %d %d\n", a, b, c, d);
    }
}