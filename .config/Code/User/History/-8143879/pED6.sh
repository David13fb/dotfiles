#!/bin/bash
SIZE=($4 * $3);
tail -$SIZE $1 | head -c$SIZE > $2