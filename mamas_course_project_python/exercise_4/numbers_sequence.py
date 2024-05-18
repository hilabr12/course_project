NEW_LINE = "\n"


def input_numbers() -> list[int]:
    """
    Prompts the user to input numbers until -1 is entered.

    Returns:
    list[int]: A list of numbers entered by the user.
    """
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

    def get_average(self) -> float:
        """
        Calculate the average of the entered numbers.

        Returns:
        float: The average of the entered numbers.
        """
        return sum(self.numbers) / len(self.numbers)

    def get_amount_of_positive_numbers(self) -> int:
        """
        Calculate the amount of positive numbers in the entered list.

        Returns:
        int: The count of positive numbers in the entered list.
        """
        return sum(1 for number in self.numbers if number > 0)

    def get_sorted_numbers(self) -> list[int]:
        """
        Return a sorted list of the entered numbers.

        Returns:
        list[int]: A sorted list of the entered numbers.
        """
        return sorted(self.numbers)

