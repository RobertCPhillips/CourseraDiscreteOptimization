#!/usr/bin/python
# -*- coding: utf-8 -*-

import os
from subprocess import Popen, PIPE

def solve_it(input_data):

    # Writes the inputData to a temporary file
    tmp_file_name = 'tmp.data'
    tmp_file = open(tmp_file_name, 'w')
    tmp_file.write(input_data)
    tmp_file.close()

    # Runs the command: facility.exe tmp.data
    myapp = os.path.normpath('c:/Users/robertc/Desktop/discrete opt/Class/MySolutions/facility/bin/Debug/facility.exe')
    
    process = Popen([myapp, tmp_file_name], stdout=PIPE)
    (stdout, stderr) = process.communicate()

    # removes the temporary file
    os.remove(tmp_file_name)

    return stdout.strip()

import sys

if __name__ == '__main__':
    if len(sys.argv) > 1:
        file_location = sys.argv[1].strip()
        input_data_file = open(file_location, 'r')
        input_data = ''.join(input_data_file.readlines())
        input_data_file.close()
        print 'Solving:', file_location
        print solve_it(input_data)
    else:
        print 'This test requires an input file.  Please select one from the data directory. (i.e. python solver.py ./data/fl_16_2)'

