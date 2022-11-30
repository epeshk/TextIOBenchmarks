#include <iostream>
#include <ios>
using namespace std;

int main()
{
    ios_base::sync_with_stdio(false);
    cin.tie(NULL);

    int t;
    scanf("%d", &t);

    int a_xor = 0, b_xor = 0, c_xor = 0, d_xor = 0;

    for (int i = 0; i < t; ++i) {
        int a, b, c, d;
        scanf("%d%d%d%d", &a, &b, &c, &d);

        a_xor ^= a;
        b_xor ^= b;
        c_xor ^= c;
        d_xor ^= d;
    }

    printf("%d %d %d %d\n", a_xor, b_xor, c_xor, d_xor);
}