from mpmath import pi

START_AFTER_DOT_INDEX = 2


def reverse_n_pi_digits(n: int) -> str:
    return str(pi)[START_AFTER_DOT_INDEX:START_AFTER_DOT_INDEX + n][::-1]

if __name__ == '__main__':
    print(reverse_n_pi_digits(4))

