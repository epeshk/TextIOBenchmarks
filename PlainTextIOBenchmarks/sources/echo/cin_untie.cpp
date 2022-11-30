#include <iostream>
#include <ios>
using namespace std;

int main()
{
    cin.tie(NULL);

    int t;
    cin >> t;

    for (int i = 0; i < t; ++i) {
        int a, b, c, d;

        cin >> a >> b >> c >> d;

        cout << a << ' ' << b << ' ' << c << ' ' << d << '\n';
    }
}