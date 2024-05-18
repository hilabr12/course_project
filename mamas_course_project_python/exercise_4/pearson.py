from typing import List
import numpy as np


def print_pearson(x: List[int], y: List[int]) -> None:
    """
    Calculate and print the Pearson correlation coefficient between two lists of numbers.

    Args:
    x (List[int]): The first list of numbers.
    y (List[int]): The second list of numbers.

    Returns:
    None
    """
    pearson = np.corrcoef(x, y)[0, 1]
    print(f"Pearson correlation coefficient: {pearson}")
