#include <iostream>
#include <ios>
using namespace std;

void fastscan(int &number)
{
    //variable to indicate sign of input number
    bool negative = false;
    bool read_start = false;
  
    number = 0;
  
    // extract current character from buffer
    while (true)
    {
#ifdef _WIN32
        int c = _getchar_nolock();
#else
        int c = getchar_unlocked();
#endif

        if (c=='-')
        {
            // number is negative
            negative = true;
            read_start = true;
            continue;
        }
  
        if (c>47 && c<58)
        {
            number = number * 10 + (c - 48);
            read_start = true;
            continue;
        }

        if (read_start)
            break;
    }
  
    // if scanned input has a negative sign, negate the
    // value of the input number
    if (negative)
        number *= -1;
}

int main()
{
    int t;
    fastscan(t);

    int a_xor = 0, b_xor = 0, c_xor = 0, d_xor = 0;

    for (int i = 0; i < t; ++i) {
        int a, b, c, d;
        fastscan(a);
        fastscan(b);
        fastscan(c);
        fastscan(d);

        a_xor ^= a;
        b_xor ^= b;
        c_xor ^= c;
        d_xor ^= d;
    }

    printf("%d %d %d %d\n", a_xor, b_xor, c_xor, d_xor);
}