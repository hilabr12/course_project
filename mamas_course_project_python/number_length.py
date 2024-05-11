import math


def num_len(number: int) -> int:
    return int(math.log10(number) + 1)

if __name__ == '__main__':
    print(num_len(1234))

