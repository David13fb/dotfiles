#!/bin/bash
SIZE=($4 * $3);
head -c$SIZE $1 | head -c$SIZE > $2