pdflatex "\def\lang{pl}\input documentation.tex"
mv documentation.pdf documentation_pl.pdf
pdflatex "\def\lang{en}\input documentation.tex"
mv documentation.pdf documentation_en.pdf

