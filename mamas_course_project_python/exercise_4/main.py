from ui import *
from graph import *
from numbers_sequence import *

if __name__ == "__main__":
    numbers: Numbers = Numbers()
    app = Application(numbers)
    app.mainloop()
    create_graph(numbers.numbers)
