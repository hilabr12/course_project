from numbers_sequence import *
import tkinter as tk


class Application(tk.Tk):
    def __init__(self):
        super().__init__()
        self.title("Results")
        self.geometry("400x200")
        self.create_widgets()

    def create_widgets(self):
        numbers = Numbers()
        avg = numbers.get_average()
        positive_count = numbers.get_amount_of_positive_numbers()
        sorted_list = numbers.get_sorted_numbers()

        result_label = tk.Label(self, text=f"The average of numbers is: {avg} {NEW_LINE}"
                                           f"There are {positive_count} positive numbers{NEW_LINE} "
                                           f"Sorted numbers list: {sorted_list}")
        result_label.pack()


