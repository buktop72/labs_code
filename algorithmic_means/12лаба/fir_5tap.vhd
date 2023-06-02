library IEEE;
use IEEE.STD_LOGIC_1164.ALL;
use IEEE.NUMERIC_STD.ALL; --описаны типы SIGNED
entity fir_5tap is
port( Clk : in std_logic; -- тактовый сигнал
Xin : in signed(7 downto 0); -- 8-битный входной сигнал
Yout : out signed(15 downto 0) -- 16-битный выходной сигнал
);
end fir_5tap;
architecture Behavioral of fir_5tap is
component DFFx is -- компонент задержки
 port(
Q : out signed(15 downto 0); -- выход на сумматор
Clk :in std_logic; -- тактовый вход
D :in signed(15 downto 0) -- вход данных из блока MCM
);
end component;
signal H0,H1,H2,H3,H4 : signed(7 downto 0) := (others => '0');
signal MCM0,MCM1,MCM2,MCM3,MCM4,add_out1,add_out2,add_out3,add_out4 : signed(15 downto 0) := (others => '0');
signal Q1,Q2,Q3,Q4 : signed(15 downto 0) := (others => '0');
begin ------------------------------ инициализация коэффициентов фильтра
H0 <= to_signed(-27,8);
H1 <= to_signed(-13,8); -- преобразование в тип SIGNED
H2 <= to_signed(13,8);
H3 <= to_signed(27,8);
H4 <= to_signed(42,8);
------------ умножение входного сигнала на постоянные коэффициенты
MCM4 <= H4*Xin;
MCM3 <= H3*Xin;
MCM2 <= H2*Xin;
MCM1 <= H1*Xin;
MCM0 <= H0*Xin;
------------------------------------------------------ формирование сумматоров
add_out1 <= Q1 + MCM3;
add_out2 <= Q2 + MCM2;
add_out3 <= Q3 + MCM1;
add_out4 <= Q4 + MCM0;
----------------------- триггерные операции (для формирования задержки)
dff1 : DFFx port map(Q1,Clk,MCM4);
dff2 : DFFx port map(Q2,Clk,add_out1);
dff3 : DFFx port map(Q3,Clk,add_out2);
dff4 : DFFx port map(Q4,Clk,add_out3);
-------- формирование выходного сигнала по переднему фронту тактов
process(Clk)
begin
 if(rising_edge(Clk)) then
Yout <= add_out4;
 end if;
end process;
end Behavioral;


