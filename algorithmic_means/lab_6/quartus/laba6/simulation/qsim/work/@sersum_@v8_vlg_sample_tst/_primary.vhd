library verilog;
use verilog.vl_types.all;
entity Sersum_V8_vlg_sample_tst is
    port(
        clk             : in     vl_logic;
        first           : in     vl_logic;
        xs              : in     vl_logic;
        ys              : in     vl_logic;
        sampler_tx      : out    vl_logic
    );
end Sersum_V8_vlg_sample_tst;
