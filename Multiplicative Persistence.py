# https://www.youtube.com/watch?v=Wim9WJeDTHQ

import time

def per(n):
    steps = 0

    # if (len(str(n)) == 1):
        # print(n)
        # return steps

    while len(str(n)) > 1:
        digits = [int(i) for i in str(n)]

        result = 1
        for j in digits:
            result *= j
        
        n = str(result)
        # print(result)
        steps += 1
    # print(str(steps) + " STEPS")
    return steps

# currentNumber = 277777788888899
# currentNumber = 277777788888899277777998888887

# input("Hit Enter to begin brute-forcing multiplicative persistence starting at " + str(currentNumber) + "\n")
# while True:
#     steps = per(currentNumber)
    
#     if (steps >= 11):
#         print("Current Number = " + str(currentNumber) + "\tSteps = " + str(steps) + "\tTime: " + str(time.strftime("%r")))
#     currentNumber += 1

print(per(2928393091232093912))