from ui import *
from graph import *
from numbers_sequence import *
from pearson import *

if __name__ == "__main__":
    numbers: Numbers = Numbers()
    app = Application(numbers)
    create_graph(numbers.numbers)
    app.mainloop()

