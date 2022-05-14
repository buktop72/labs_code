clear
//Исходные данные:
Fs = 2000 // частота дискретизации
Fc1 = 200  // нижняя ч-та среза
Fc2 = 300 // верхняя ч-та среза
e1 = 0.5 // неравномерность АЧХ в полосе пропускания
e2 = 80 // уровень подавления

// Рассчет порядка фильтра:
a = Fc1 / (Fc2 - Fc1) // прямоугольность
//disp("a = ", a)
b = Fs / Fc1 // узкополосность
//disp("b = ", b)
L = -(2 / 3) * log10(10 * e1 * e2) // лог-й показатель част-ой избир-ти
//disp("L = ", L)
n = ceil(abs(a * b * L)) // порядок фильтра
disp("N = ", n)

n = n - 20;
//Расчет коэффициентов фильтра:
// Rectangle
wr=1:n;
for i=0:n-1
    if ((i>=0)&(i<=n))then
        wr(i+1)=0.54-0.46*cos((2*%pi*(i))/(n-1));
    else 
        wr(i+1)=1;
    end
end
disp('Прямоугольное окно')
disp(wr)
// wfir(тип фильтра, порядок фильтра, промежуток срезаемых частот, тип окна, параметры окна)
// Выходные параметры: 
// fr –сетка частот, wfm – частотная характеристика фильтра, wft – коэффициенты фильтра.
// https://help.scilab.org/docs/5.5.2/en_US/wfir.html
[wft,wfm,fr]= wfir("bp", n+1, [.2 .3],"re",[0 0]);
scf();xgrid();
xtitle('Rectangle -20 ','freq(kHz)', 'Amplitude(dB)')
plot2d(fr,20*log10(wfm))

// Hemming
whem=1:n;
for i=0:n-1
    if ((i>=0)&(i<=n))then
        whem(i+1)=0.54-0.46*cos((2*%pi*(i))/(n-1));
    else 
        whem(i+1)=1;
    end
end
disp('Окно Хемминга')
disp(whem)

[wft,wfm,fr]= wfir("bp", n+1, [.2 .3],"hm",[0 0]);
scf();xgrid();
xtitle('Hemming -20','freq(kHz)', 'Amplitude(dB)')
plot2d(fr,20*log10(wfm))

// Henning
when=1:n;
for i=0:n-1
    if ((i>=0)&(i<=n))then
        when(i+1)=0.5*(1-cos((2*%pi*(i))/(n-1)));
    else 
        when(i+1)=0;
    end
end
disp('Окно Хеннинга');
disp(when);

[wft,wfm,fr]= wfir("bp", n+1, [.2 .3],"hn",[0 0]);
scf();xgrid();
xtitle('Henning -20 ','freq(kHz)', 'Amplitude(dB)')
plot2d(fr,20*log10(wfm))
