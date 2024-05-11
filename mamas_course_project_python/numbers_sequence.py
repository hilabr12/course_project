def input_numbers() -> list[int]:
    numbers: list[int] = []
    while True:
        try:
            number: str = input("Enter a number or -1 to stop: ").strip()
            if number == '-1':
                break
            numbers.append(int(number))
        except ValueError:
            print("Please enter a valid number.")
    return numbers


class Numbers:
    def __init__(self):
        self.numbers: list[int] = input_numbers()

    def get_average(self) -> None:
        print(f"The average of numbers is: {sum(self.numbers) / len(self.numbers)}")

    def get_amount_of_positive_numbers(self) -> None:
        # adds the number 1 to the list whenever the current number is positive and then sums the list
        print(f"there are {sum(1 for number in self.numbers if number > 0)} positive numbers")

    def get_sorted_numbers(self) -> None:
        print(f"sorted numbers list: {self.numbers.sort()}")


o = Numbers()
o.get_amount_of_positive_numbers()
o.get_average()
o.get_sorted_numbers()
