from math import pow


def pythagorean_triplet_by_sum(sum_triplet: int) -> None:
    """
        Find Pythagorean triplets whose sum equals the given sum.

        Args:
        sum_triplet (int): The desired sum of the Pythagorean triplet.

        Returns:
        None: Prints the Pythagorean triplets.
    """
    a = 1
    while a < sum_triplet:
        b = a + 1
        while b < sum_triplet:
            c = sum_triplet - a - b
            if pow(a,2) + pow(b,2) == pow(c,2):
                print(f"{a} < {b} < {c}")
            b += 1
        a += 1


if __name__ == '__main__':
    pythagorean_triplet_by_sum(12)
    pythagorean_triplet_by_sum(30)
    pythagorean_triplet_by_sum(40)
    pythagorean_triplet_by_sum(56)
    pythagorean_triplet_by_sum(24)
