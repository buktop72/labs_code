-- подключение библиотек
LIBRARY IEEE;
USE ieee.std_logic_1164.ALL;
USE ieee.std_logic_arith.all;
-- описание объекта 
ENTITY lab_7 IS
 GENERIC ( 
 -- —0-—2 - множители согласно варианта задани€
 C0 : INTEGER:= -1;
 C1 : INTEGER:= 11;
 C2 : INTEGER:= 29;
 W : NATURAL:=6); -- разр€дность выходной шины
 
 -- описание портов
 PORT ( 
 ADDR : IN INTEGER RANGE 0 to 3; -- входной порт
 clk : IN STD_LOGIC; -- тактовый сигнал
 DATA : OUT STD_LOGIC_VECTOR(W - 1 downto 0)); -- выходной порт
END lab_7;
---------------------------

-- описание архитектуры
architecture behavior of lab_7 is
signal s : integer RANGE - (2**W)+1 TO 2**W - 1;
begin
process(ADDR)
begin
case ADDR is
when 0 => s <= C0;
when 1 => s <= C1;
when 2 => s <= C2;
when 3 => s <= C1 + C2;
end case;
end process;
process(clk)
begin
if (clk'EVENT AND clk = '1') then -- передача сигнала на выход при переднем фпронте clk
DATA <= CONV_STD_LOGIC_VECTOR(s,W); -- ф-€ преобразовани€ сигнала s в логический вектор размерности W
end if;
end process; 
end behavior; 