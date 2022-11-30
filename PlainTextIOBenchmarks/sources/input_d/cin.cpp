#include <iostream>
#include <ios>
using namespace std;

int main()
{
    int t;
    cin >> t;

    int a_xor = 0, b_xor = 0, c_xor = 0, d_xor = 0;

    for (int i = 0; i < t; ++i) {
        double a, b, c, d;

        cin >> a >> b >> c >> d;

        a_xor ^= (int)a;
        b_xor ^= (int)b;
        c_xor ^= (int)c;
        d_xor ^= (int)d;
    }

    cout << a_xor << ' ' << b_xor << ' ' << c_xor << ' ' << d_xor;
}