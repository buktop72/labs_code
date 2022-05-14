// увеличим порядок фильтра на 20
n = n - 20;
// Rectangle
w=1:n;
for i=0:n-1
    if ((i>=0)&(i<=n))then
        w(i+1)=0.54-0.46*cos((2*%pi*(i))/(n-1));
    else 
        w(i+1)=1;
    end
end
disp('Прямоугольное окно + 20')
disp(w)

// wfir(тип фильтра, порядок фильтра, промежуток срезаемых частот, тип окна, параметры окна)
// Выходные параметры: 
// fr –сетка частот, wfm – частотная характеристика фильтра, wft – коэффициенты фильтра.
[wft,wfm,fr]= wfir("bp", n+1, [.1 .2],"re",[0 0]);
scf();xgrid();
xtitle('Rectangle- 20','freq(kHz)', 'Amplitude(dB)')
plot2d(fr,20*log10(wfm))


// Hemming
w=1:n;
for p=0:n-1
    if ((p>=0)&(p<=n))then
        w(p+1)=0.54-0.46*cos((2*%pi*(p))/(n-1));
    else 
        w(p+1)=1;
    end
end
disp('Окно Хемминга +20')
disp(w)

// wfir(тип фильтра, порядок фильтра, промежуток срезаемых частот, тип окна, параметры окна)
// Выходные параметры: 
// fr –сетка частот, wfm – частотная характеристика фильтра, wft – коэффициенты фильтра.
[wft,wfm,fr]= wfir("bp", n+1, [.1 .2],"hm",[0 0]);
scf();xgrid();
xtitle('Hemming- 20','freq(kHz)', 'Amplitude(dB)')
plot2d(fr,20*log10(wfm))

// Henning
w=1:n;
for i=0:n-1
    if ((i>=0)&(i<=n))then
        whm(i+1)=0.5*(1-cos((2*%pi*(i))/(n-1)));
    else 
        w(i+1)=0;
    end
end
disp('Окно Хеннинга +20');
disp(whm);

[wft,wfm,fr]= wfir("bp", n+1, [.1 .2],"hn",[0 0]);
scf();xgrid();
xtitle('Henning- 20','freq(kHz)', 'Amplitude(dB)')
plot2d(fr,20*log10(wfm))
