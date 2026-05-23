ls -l | tail -n +2 | cut -c1 | while read t; do [[ "$tipos" != *"$t"* ]] && tipos+="$t" && echo "$t"; done
