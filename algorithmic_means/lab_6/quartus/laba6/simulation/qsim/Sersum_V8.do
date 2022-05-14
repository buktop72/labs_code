onerror {quit -f}
vlib work
vlog -work work Sersum_V8.vo
vlog -work work Sersum_V8.vt
vsim -novopt -c -t 1ps -L maxii_ver -L altera_ver -L altera_mf_ver -L 220model_ver -L sgate work.Sersum_V8_vlg_vec_tst
vcd file -direction Sersum_V8.msim.vcd
vcd add -internal Sersum_V8_vlg_vec_tst/*
vcd add -internal Sersum_V8_vlg_vec_tst/i1/*
add wave /*
run -all
