library IEEE;
use IEEE.STD_LOGIC_1164.ALL;
use IEEE.NUMERIC_STD.ALL; --������� ���� SIGNED
entity fir_5tap is
port( Clk : in std_logic; -- �������� ������
Xin : in signed(7 downto 0); -- 8-������ ������� ������
Yout : out signed(15 downto 0) -- 16-������ �������� ������
);
end fir_5tap;
architecture Behavioral of fir_5tap is
component DFFx is -- ��������� ��������
 port(
Q : out signed(15 downto 0); -- ����� �� ��������
Clk :in std_logic; -- �������� ����
D :in signed(15 downto 0) -- ���� ������ �� ����� MCM
);
end component;
signal H0,H1,H2,H3,H4 : signed(7 downto 0) := (others => '0');
signal MCM0,MCM1,MCM2,MCM3,MCM4,add_out1,add_out2,add_out3,add_out4 : signed(15 downto 0) := (others => '0');
signal Q1,Q2,Q3,Q4 : signed(15 downto 0) := (others => '0');
begin ------------------------------ ������������� ������������� �������
H0 <= to_signed(-27,8);
H1 <= to_signed(-13,8); -- �������������� � ��� SIGNED
H2 <= to_signed(13,8);
H3 <= to_signed(27,8);
H4 <= to_signed(42,8);
------------ ��������� �������� ������� �� ���������� ������������
MCM4 <= H4*Xin;
MCM3 <= H3*Xin;
MCM2 <= H2*Xin;
MCM1 <= H1*Xin;
MCM0 <= H0*Xin;
------------------------------------------------------ ������������ ����������
add_out1 <= Q1 + MCM3;
add_out2 <= Q2 + MCM2;
add_out3 <= Q3 + MCM1;
add_out4 <= Q4 + MCM0;
----------------------- ���������� �������� (��� ������������ ��������)
dff1 : DFFx port map(Q1,Clk,MCM4);
dff2 : DFFx port map(Q2,Clk,add_out1);
dff3 : DFFx port map(Q3,Clk,add_out2);
dff4 : DFFx port map(Q4,Clk,add_out3);
-------- ������������ ��������� ������� �� ��������� ������ ������
process(Clk)
begin
 if(rising_edge(Clk)) then
Yout <= add_out4;
 end if;
end process;
end Behavioral;


