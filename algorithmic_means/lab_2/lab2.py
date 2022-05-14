"""
Задание: по варианту предыдущей работы для заданного входного сигнала x(n)
определить значения амплитудного и фазового откликов, используя формулу
дискретного преобразования Фурье.
Построить графики амплитудного и фазового откликов. 
"""
import matplotlib.pyplot as plt
from pprint import pprint
from scipy.fft import fft
from math import pi

# входной сигнал:
x_n = [3, 1, 2, 1, 4, 3, 1, 5, 0, 2]

dpf = fft(x_n)  # прямое одномерное ДПФ.
print('Значения ДПФ:')
pprint(dpf)

ls = []  # преобразование массива комплексных чисел в список
for i in dpf:
    ls.append([i.real, i.imag])
# print(ls)

# амплитудный отклик
am = []
for i in ls:
    am.append(round(((i[0] ** 2 + i[1] ** 2) ** 0.5), 2))

# фазовый отклик
phi = []
for i in range(10):
    phi.append(round((-4 * pi * i / 10), 2))

print('\n', 'амплитудный отклик:')
pprint(am)
print('\n', 'фазовый отклик:')
pprint(phi)

# Построение графиков:

# График амплитудного отклика
plt.title('График амплитудного отклика')
plt.grid()
plt.stem(am, bottom=0.0, use_line_collection=True)
plt.show()


# График фазового отклика
plt.title('График амплитудного отклика')
plt.grid()
plt.stem(phi, bottom=0.0, use_line_collection=True)
plt.show()


