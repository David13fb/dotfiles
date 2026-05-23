#!/bin/bash
SIZE=($4 * $3);
tail -c0 $1 | head -c$SIZE > $2