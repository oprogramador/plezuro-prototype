pdflatex "\def\lang{pl}\input doc.tex"
mv doc.pdf doc_pl.pdf
pdflatex "\def\lang{en}\input doc.tex"
mv doc.pdf doc_en.pdf

