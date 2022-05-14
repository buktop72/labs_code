library verilog;
use verilog.vl_types.all;
entity Sersum_V8 is
    port(
        xs              : in     vl_logic;
        ys              : in     vl_logic;
        first           : in     vl_logic;
        clk             : in     vl_logic;
        ssum            : out    vl_logic
    );
end Sersum_V8;
