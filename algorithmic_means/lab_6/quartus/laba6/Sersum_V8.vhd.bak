library ieee;
use ieee.std_logic_1164.all;
entity Sersum_V8 is
port (xs,ys,first,clk 	: in std_logic;
		ssum 					: out std_logic);
end Sersum_V8;
architecture ss_arch of Sersum_V8 is
signal ci : std_logic := '0';
begin
process(clk) begin
	if (rising_edge(clk)) then
		if (first = '1') then
			 ssum <= xs xor ys xor ci;
			 ci <= (xs and ys) or (xs and ci) or (ys and ci);
		end if;
	end if;
end process;
end ss_arch;