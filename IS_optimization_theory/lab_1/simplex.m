% ������� � 7
% f(x)=10*x*log(x)-(x^2/2), ��� 0.1 < x < 1.0
% e=0.0001
% ��������� ������
ezplot ('10*x*log(x)-(x^2/2)', [0.1 1.0])
hold on
format long
% 1. ����� ��������
% �� ������� �����, ��� ������� ������� ��������� ����� x=0.3 � x=0.5
f = @(x) 10*x*log(x)-(x^2/2) % ���� �������
h = 0.00001; % ������ ���
x = 0.3; % ��������� �����������
i = 0; % ������� ��������
while f(x+h) <= f(x) % ���� ������� �����������, �-� �������
 x = x + h;
 i = i + 1;
end
disp('1. ����� ��������, x= :')
disp(x) % ������� - �����, ����� �-� ����� ���������� (x=0.3822)
disp('��������� ��������, i= :')
disp(i) % 0.3822
% 2. ����� ������������ ������
e = 0.00001; % �������� �����������
h = 0.01; % ������ �������������� ���
x = 0.3; % ��������� �����������
i = 0; % ������� ��������
while h > e
 while f(x+h) <= f(x) % ���� ������� �����������, �-� �������
 x = x + h;
 i = i + 1;
 end
 %disp(x)
 h = h/10; % ��������� ��� � ��������� ����
end
disp('2. ����� ������������ ������, x= :')
disp(x) % �������, �����, ����� �-� ����� ����������, x=0.3822
disp('��������� ��������, i= :')
disp(i) % 12
% 3. ����� ���������
e = 0.00001; % �������� �����������
a = 0.3; % ������ ��������� ������� [a,b]
b = 0.5;
i=0; % ������� ��������
while (b-a)/2 > e % ���� �� �������� ��������� ��������
 x1 = (a+b+e)/2; % ��������� ����������� �������� �������
 x2 = (a+b-e)/2;
 if f(x1) > f(x2) % ��������� �������� �-� � ���� ����� � ����������
 b=x2; % ������ ����� �������
 else
 a=x1; % ������ ����� �������
 end
 i = i+1;
end
disp('3. ����� ���������, x= :')
disp((x1+x2)/2)
disp('��������� ��������, i= :')
disp(i) % 13
% 4. ����� �������� �������
e = 0.00001; % �������� �����������
a = 0.3; % ������ ��������� ������� [a,b]
b = 0.5;
i=0; % ������� ��������
r=(sqrt(5)+1)/2; % ������� �����������
a=0.3; % ��������� �����
b=0.5;
e=0.00001;
a2= a;
b2= b;
while abs(b-a) >= e % ���� ����������� ������ ���������
 x1= b-(b-a)/r;
 x2= a+(b-a)/r;
 y1= f(x1);
 y2= f(x2);
 if y1 >= y2
 a=x1;
 else b=x2;
 end
i=i+1;
end
disp('4. ����� �������� �������, x= :')
x=(a+b)/2;
disp(x)
disp('��������� ��������, i= :')
disp(i)
% 5. ����� �������
e=0.00001; % �������� �����������
x0 = 0.3; % ��������� �����������
delta=0.01;
dx=0.0001;
h=0.5*dx;
i=0; % ������� ��������
while delta>e
 f1=f(x0+dx);
 f2=f(x0);
 f3=f(x0-dx);
 x=x0-h*(f1-f3)/(f1-2*f2+f3); % �������� ����� �������
 delta=abs(x-x0);
 x0=x;
 i=i+1;
end;
disp('5. ����� �������, x= :')
disp(x0)
disp('��������� ��������, i= :')
disp(i) 