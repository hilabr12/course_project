import tkinter as tk
from numbers_sequence import *

NEW_LINE = "\n"


class Application(tk.Tk):
    def __init__(self, numbers: Numbers):
        super().__init__()
        self.title("Results")
        self.geometry("400x200")
        self.numbers: Numbers = numbers
        self.create_widgets()

    def create_widgets(self):
        avg: float = self.numbers.get_average()
        positive_count: int = self.numbers.get_amount_of_positive_numbers()
        sorted_list: list[int] = self.numbers.get_sorted_numbers()

        result_label = tk.Label(self, text=f"The average of numbers is: {avg} {NEW_LINE}"
                                           f"There are {positive_count} positive numbers{NEW_LINE} "
                                           f"Sorted numbers list: {sorted_list}")
        result_label.pack()
