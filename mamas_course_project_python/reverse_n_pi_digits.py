from mpmath import pi


def reverse_n_pi_digits(n: int) -> str:
    """
        Reverse the first n digits of Pi after the decimal point.

        Args:
        n (int): The number of digits to reverse.

        Returns:
        str: The reversed n digits of Pi.
    """
    return (str(pi)[:1] + str(pi)[2:])[: n][::-1]


if __name__ == '__main__':
    print(reverse_n_pi_digits(4))
