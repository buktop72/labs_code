# Программа для нахождения минимума функции симплекс методом
import numpy as np
from scipy.optimize import linprog
# коэффициенты целевой функции
z = list(map(float,input('введите коэффициенты целевой функции через пробел:').split()))
kn = int(input('введите количество ограничений-неравенств: '))
# коэффициенты из левой части неравенств (только "<=", неравенства вида ">="
# приводим к "<=")
aub = []
for i in range(kn):
    aub.append(list(map(float, input(f'введите коэффициенты из правой части неравенства {i+1} через пробел: ').split())))
# правая часть неравенств
bub = list(map(float, input(f'введите значения из левой части неравенств 1-{kn} через пробел: ').split()))
kr = int(input('введите количество ограничений-равенств: '))
aeq = [] # коэффициенты из левой части равенств
for i in range(kr):
    aeq.append(list(map(float, input(f'введите коэффициенты из левой части равенства {i+1} через пробел: ').split())))

# правая часть равенств
beq = list(map(float, input(f'введите значения из правой части равенств 1-{kr} через пробел: ').split()))
# готовим исходные данные
A_ub = np.array(aub, dtype=object)
b_ub = np.array(bub, dtype=object)
A_eq = np.array(aeq, dtype=object)
b_eq = np.array(beq, dtype=object)
c = np.array(z, dtype=object)
# Вызываем функцию с нашими аргументами:
res = linprog(c, A_ub, b_ub, A_eq, b_eq, bounds=(0, None))