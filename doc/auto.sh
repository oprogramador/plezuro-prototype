grep -r 'class\|interface\|enum' `find ../src -name '*.cs'` | grep '{' | cut -d '/' -f5-20 | cut -d '{' -f1 > code.tex
