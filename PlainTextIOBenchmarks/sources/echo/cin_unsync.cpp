#include <iostream>
#include <ios>
using namespace std;

int main()
{
    ios_base::sync_with_stdio(false);

    int t;
    cin >> t;

    for (int i = 0; i < t; ++i) {
        int a, b, c, d;

        cin >> a >> b >> c >> d;

        cout << a << ' ' << b << ' ' << c << ' ' << d << '\n';
    }
}