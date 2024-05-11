ODD = 1


def is_odd_number(word: str) -> bool:
    return word % 2 == 1


def is_word_sorted_in_alphabetical_order(word: str) -> bool:
    is_sorted = all([word[i] <= word[i + 1] for i in range(len(word) - 1)])
    return is_sorted


def is_palindrome(word: str) -> bool:
    return word == word[::-1]


def is_sorted_palindrome(word: str) -> bool:
    half_length_of_word = len(word) // 2
    first_sliced_word: str = word[:half_length_of_word]
    second_sliced_word: str = word[half_length_of_word + 1:][::-1]

    if is_odd_number:
        remaining_char = word[half_length_of_word]
        first_sliced_word += remaining_char
        second_sliced_word += remaining_char

    return is_palindrome(word) and is_word_sorted_in_alphabetical_order(
        first_sliced_word) and is_word_sorted_in_alphabetical_order(second_sliced_word)


print(is_sorted_palindrome("abcdcba"))
print(is_sorted_palindrome("AbcdCbA"))
print(is_sorted_palindrome("bab"))
