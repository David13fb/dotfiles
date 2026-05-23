#!/bin/bash
SIZE=$4*$3
tail -c+$1 inputfile | head -c$SIZE > $2