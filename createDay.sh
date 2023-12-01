Date=Dec$1

cp -r Days/Template Days/$Date

to_replace=Template
replace_with=$Date

sed -i '' -e "s/$to_replace/$replace_with/g" Days/$Date/Solver.cs

to_replace=XXXXXX
replace_with=$Date

sed -i '' -e "s/$to_replace/$replace_with/g" Days/$Date/Solver.cs

to_replace=Days.Dec..
replace_with=Days.$Date

sed -i '' -e "s/$to_replace/$replace_with/g" Program.cs