import math


def num_len(number: int) -> int:
    """
        Gets the length of an int.
        Args:
        number (str): The number to check it's length.

        Returns:
        int: number length.
        """
    return int(math.log10(number) + 1)


if __name__ == '__main__':
    print(num_len(1234))
