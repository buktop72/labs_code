onerror {quit -f}
vlib work
vlog -work work laba5.vo
vlog -work work laba5.vt
vsim -novopt -c -t 1ps -L maxii_ver -L altera_ver -L altera_mf_ver -L 220model_ver -L sgate work.laba5_vlg_vec_tst
vcd file -direction laba5.msim.vcd
vcd add -internal laba5_vlg_vec_tst/*
vcd add -internal laba5_vlg_vec_tst/i1/*
add wave /*
run -all
