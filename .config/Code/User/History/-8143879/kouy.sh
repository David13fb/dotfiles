#!/bin/bash
SIZE=($4 * $3);
tail -c$SIZE $1 | head -c$SIZE > $2