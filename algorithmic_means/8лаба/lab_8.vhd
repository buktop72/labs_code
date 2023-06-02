library IEEE; 
use IEEE.STD_LOGIC_1164.ALL; 
LIBRARY lpm;
USE lpm.lpm_components.all;

ENTITY lab_8 IS
GENERIC (
	W : NATURAL:=4;  -- разрядность сумматора
	N : NATURAL:=6);  -- разрядность сдвигового регистра
PORT (
	add_sub : IN STD_LOGIC;  -- установка режима суммирования (1-суммирование, 0-вычитание
	clk : IN STD_LOGIC;  -- тактовый сигнал
	X : IN STD_LOGIC_VECTOR(W-1 downto 0);  --W-разрядный входной сигнал
	nReset : IN STD_LOGIC;  -- предварительный сброс
	DOUT : OUT STD_LOGIC_VECTOR(W+N-1 downto 0));  -- (W+N)-разрядный выходной сигнал
END lab_8; 
architecture struct of lab_8 is
COMPONENT lpm_add_sub
	GENERIC (
		LPM_WIDTH: POSITIVE;
		LPM_REPRESENTATION: STRING := "SIGNED";
		LPM_DIRECTION: STRING := "UNUSED";
		LPM_PIPELINE: INTEGER := 0;
		LPM_TYPE: STRING := "LPM_ADD_SUB";
		LPM_HINT: STRING := "UNUSED");
	PORT(
		dataa,datab: IN STD_LOGIC_VECTOR(LPM_WIDTH-1 DOWNTO 0);
		aclr, clock, cin: IN STD_LOGIC := '0';
		clken, add_sub: IN STD_LOGIC := '1';
		result: OUT STD_LOGIC_VECTOR(LPM_WIDTH-1 DOWNTO 0);
		cout, overflow: OUT STD_LOGIC);
END COMPONENT;
COMPONENT lpm_shiftreg
	GENERIC (
		LPM_WIDTH: POSITIVE;
		LPM_AVALUE: STRING := "UNUSED";
		LPM_SVALUE: STRING := "UNUSED";
		LPM_PVALUE: STRING := "UNUSED";
		LPM_DIRECTION: STRING := "UNUSED";
		LPM_TYPE: STRING := "LPM_SHIFTREG";
		-- MAXIMIZE_SPEED: POSITIVE;
		LPM_HINT: STRING := "UNUSED");
	PORT(
		data: IN STD_LOGIC_VECTOR(LPM_WIDTH-1 DOWNTO 0) := (OTHERS => '0');
		clock: IN STD_LOGIC;
		enable, shiftin: IN STD_LOGIC := '1';
		load, sclr, sset, aclr, aset: IN STD_LOGIC := '0';
		q:OUT STD_LOGIC_VECTOR(LPM_WIDTH-1 DOWNTO 0); 
		shiftout: OUT STD_LOGIC);
END COMPONENT;
signal sum: STD_LOGIC_VECTOR(W-1 downto 0);
signal tmp_out: STD_LOGIC_VECTOR(W+N-1 downto 0);
signal rst: STD_LOGIC;
BEGIN
rst <= NOT nReset;
adder: lpm_add_sub
GENERIC MAP(
	LPM_DIRECTION => "DEFAULT",
	LPM_PIPELINE => 1, 
	LPM_REPRESENTATION => "SIGNED",
	LPM_WIDTH => W)
	--MAXIMIZE_SPEED => 5
	
PORT MAP(
	add_sub => add_sub,
	aclr => rst,
	clock => clk,
	dataa => sum,
	datab => X,
	result => tmp_out(W+N-1 downto N));
shifter: lpm_shiftreg
GENERIC MAP(LPM_DIRECTION => "RIGHT",
	LPM_WIDTH => N)
PORT MAP(
	shiftin => tmp_out(N),
	clock => clk,
	q => tmp_out(N-1 downto 0));
sum(W-1) <= tmp_out(W+N-1);
sum(W-2 downto 0) <= tmp_out(W+N-1 downto N+1);
DOUT <= tmp_out;
end struct; 