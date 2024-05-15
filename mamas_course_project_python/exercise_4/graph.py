import matplotlib.pyplot as plt
import numpy as np
from pearson import print_pearson


def create_graph(numbers: list[int]):
    input_order: list[int] = [index + 1 for index in range(len(numbers))]

    x = np.array(input_order)
    y = np.array(numbers)
    print(numbers)
    fig, ax = plt.subplots()

    ax.set_xlabel('Input order')
    ax.set_ylabel('Numbers')

    ax.set_title('Numbers according to order')
    plt.scatter(x, y, color='hotpink')
    plt.show()
    print_pearson(input_order, numbers)
