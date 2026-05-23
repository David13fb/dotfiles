cd /run;
ls -l | tail -n +2 | cut -c1 | sort | uniq
