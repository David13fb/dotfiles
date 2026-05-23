#!/bin/bash
SIZE=($2 * $3);
head -c$SIZE $0 > $1