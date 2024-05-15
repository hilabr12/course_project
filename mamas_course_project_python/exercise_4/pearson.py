from typing import List
import numpy as np


def print_pearson(x: List[int], y: List[int]) -> None:  # Prints the pearson correlation coefficient
    pearson = np.corrcoef(x, y)[0, 1]
    print(f"Pearson correlation coefficient: {pearson}")
