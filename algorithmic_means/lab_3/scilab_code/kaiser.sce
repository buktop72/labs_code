// Kaiser
B=0.1102*(e2-8.7);
disp(B);
disp('Окно Кайзера')
wkr=window('kr',n,B);
disp(wkr)
[wft,wfm,fr]= wfir("bp", n+1, [.1 .2],"kr",[B 0]);
scf();xgrid();
xtitle('Kaiser','freq(kHz)', 'Amplitude(dB)')
plot2d(fr,20*log10(wfm))
