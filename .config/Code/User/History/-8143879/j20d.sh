#!/bin/bash
SIZE=($4 * $3);
tail -c 0 $1 | head -c$SIZE > $2