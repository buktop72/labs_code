% ��������:
% F(x) = 10*x*log(x)-(x^2/2) => min,  x [0.1, 1]
% �������� ������
ezplot ('10*x*log(x)-(x^2/2)', [0 2.0])
hold on
ezplot ('10*(log(x)+1)-x', [0 2.0])
xline(0)
yline(0)
% 1. ����� ������� �����
f = @(x) 10*x*log(x)-(x^2/2); % ���� �������
Df = @(x) 10*(log(x)+1)-x; % �����������
e = 0.00001; % �������� �����������
a = 0.3; % ������ ��������� ������� [a,b]
b = 0.5;
x=(a+b)/2; % ������� �����
i=1; % ������� ��������
while abs(Df(x)) > e % ���� �� �������� ��������� ��������
 if Df(x) > 0 %
 b=x; % ������ ����� ������� [a, x]
 else
 a=x; % ������ ����� ������� [x, b]
 end
 i = i+1;
 x=(a+b)/2; % ����� ������� �����
end
disp('1. ����� ������� �����, x= :')
disp(x)
disp('����������� �������� �������, f(x)= :')
disp(f(x))
disp('��������� ��������, i= :')
disp(i)
% 2. ����� ����
f = @(x) 10*x*log(x)-(x^2/2); % ���� �������
Df = @(x) 10*(log(x)+1)-x; % �����������
e = 0.00001; % �������� �����������
a = 0.3; % ������ ��������� ������� [a,b]
b = 0.5;
x = a-(Df(a)/(Df(a)-Df(b)))*(a-b); % ����� ����������� �����
i=1; % ������� ��������
while abs(Df(x)) > e % ���� �� �������� ��������� ��������
 if Df(x) > 0 %
 b=x; % ������ ����� ������� [a, x]
 else
 a=x; % ������ ����� ������� [x, b]
 end
 i = i+1;
 x=a-(Df(a)/(Df(a)-Df(b)))*(a-b); % ����� ������� �����
end
disp('2. ����� ����, x= :')
disp(x)
disp('����������� �������� �������, f(x)= :')
disp(f(x))
disp('��������� ��������, i= :')
disp(i)
% 3. ����� �������
f = @(x) 10*x*log(x)-(x^2/2); % ���� �������
Df = @(x) 10*(log(x)+1)-x; % ����������� ������� �������
Df2 = @(x) 10/x-1; % ����������� ������� �������
e = 0.00001; % �������� �����������
a = 0.3; % ������ ��������� ������� [a,b]
b = 0.5;
x = (a+b)/2; % ��������� �����������
i=1; % ������� ��������
while abs(Df(x)) > e % ���� �� �������� ��������� ��������
 i = i+1;
 x = x - Df(x)/Df2(x); % ����� �����������
end
disp('3. ����� �������, x=')
disp(x)
disp('����������� �������� �������, f(x)=')
disp(f(x))
disp('��������� ��������, i= ')
disp(i)