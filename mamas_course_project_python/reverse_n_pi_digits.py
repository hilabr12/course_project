from mpmath import pi


def reverse_n_pi_digits(n: int) -> str:
    return (str(pi)[:1] + str(pi)[2:])[: n][::-1]


if __name__ == '__main__':
    print(reverse_n_pi_digits(4))
