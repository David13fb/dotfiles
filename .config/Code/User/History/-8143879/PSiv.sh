#!/bin/bash
SIZE=$4x$3
tail -c+$1 inputfile | head -c$SIZE > $2