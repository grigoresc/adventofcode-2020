import math
import itertools
l = [1721, 979, 366, 299, 675, 1456]

l = [int(line.strip()) for line in open("input.txt", 'r')]
# print(l)

ll = itertools.combinations(l, 2)
def prod(ll): return math.prod(next(filter(lambda x: sum(x) == 2020, ll)))


sln1 = prod(ll)
print(sln1)
# 1009899

ll2 = itertools.combinations(l, 3)
sln2 = prod(ll2)
print(sln2)
# 44211152
