import math


def num_len(number: int) -> int:
    return len([x for x in iter(lambda: number // 10, 0)])


print(num_len(1234))
