#!/bin/bash

tail -c+$1 inputfile | head -c($4*$3) > outputfile