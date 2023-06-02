-- ����������� ���������
LIBRARY IEEE;
USE ieee.std_logic_1164.ALL;
USE ieee.std_logic_arith.all;
-- �������� ������� 
ENTITY lab_7 IS
 GENERIC ( 
 -- �0-�2 - ��������� �������� �������� �������
 C0 : INTEGER:= -1;
 C1 : INTEGER:= 11;
 C2 : INTEGER:= 29;
 W : NATURAL:=6); -- ����������� �������� ����
 
 -- �������� ������
 PORT ( 
 ADDR : IN INTEGER RANGE 0 to 3; -- ������� ����
 clk : IN STD_LOGIC; -- �������� ������
 DATA : OUT STD_LOGIC_VECTOR(W - 1 downto 0)); -- �������� ����
END lab_7;
---------------------------

-- �������� �����������
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
if (clk'EVENT AND clk = '1') then -- �������� ������� �� ����� ��� �������� ������� clk
DATA <= CONV_STD_LOGIC_VECTOR(s,W); -- �-� �������������� ������� s � ���������� ������ ����������� W
end if;
end process; 
end behavior; 