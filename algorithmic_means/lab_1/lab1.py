"""
Задание: Для своего варианта вычислить реакцию ЛДС в виде 20 отсчетов при нулевых начальных условиях в
режиме калькулятора и с использованием программы. Построить графики импульсной характеристики, воздействия и реакции.
h(n) - Импульсная характеристика
x(n) - дискретный сигнал (воздействие)
y(n) - реакция
n - количество отсчетов
"""
import matplotlib.pyplot as plt
import pandas as pd

# Исходные данные:
h_n = [2, 2, 3, 3, 3, 4, 4, 2, 1, 0]
x_n = [3, 1, 2, 1, 4, 3, 1, 5, 0, 2]
n = 20

y_n = [] # реакция системы (искомое)

# если длина h(n) и x(n) меньше n, до дополним нулями:
if len(h_n) < n:
    for i in range(n - len(h_n)):
        h_n.append(0)
        x_n.append(0)

# находим y(n):
for i in range(n):
    for j in range(i + 1):
        ls1 = h_n[:j + 1]
        ls2 = x_n[:j + 1]
        ls2.reverse()
        y = 0
        for h, x in zip(ls1, ls2):
            y += h * x
    y_n.append(y)

print('Реакция системы: ')
print(y_n)

# Построение графиков:
data = {'импульсная характеристика h(n)': h_n}
df = pd.DataFrame(data)
df.plot(kind='bar', color='g')
plt.grid()
plt.show()

data = {'дискретный сигнал x(n)': x_n}
df = pd.DataFrame(data)
df.plot(kind='bar', color='b')
plt.grid()
plt.show()

data = {'реакция y(n)': y_n}
df = pd.DataFrame(data)
df.plot(kind='bar', color='r')
plt.grid()
plt.show()
