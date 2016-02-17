# Master's Thesis / Report

This directory contains the source code for my master's thesis report.  
The report is written and typeset in [LaTeX][latex] using some extra software.

- Figures are vector graphics created using the [draw.io][draw.io] service.
- All plots are generated in LaTeX using the [pgfplots][pgfplots] package. 
- The [front][front], [title][title], and [copyright][copyright] pages have been recreated.
  - The [original templates][templates] are provided as Word documents. 
  - LaTeX templates are also provided but they are outdated.

If you're looking for final artifacts, you can download the [report][report].  
In case space is an issue, there is also a [minified version][report-min] available.

```
cb135a7e9330e579aa043468e722b16283112507  thesis.pdf
d9c6e638dadaac85b790b7dc70e44e910e9be58d  thesis.min.pdf
```

## Highlights

**2014-09-19** — Report was [published][published] by the university electronic press.  
**2015-05-19** — Awarded [best master's thesis 2014][best-thesis] at the university.  
**2015-05-25** — Featured on the IDA website; in [swedish][best-thesis-ida] and [english][best-thesis-ida-en].  
**2015-05-27** — An [article][best-thesis-dfs] from Dataföreningen on this year's winners.

## Compilation

The report is compiled using pdfLaTeX with BibTeX and MakeIndex.  
If you're compiling on Mac, [Texpad][texpad] and [MacTeX][mactex] are recommended.

The [minted][minted] package is used for highlighted source code includes.  
Using the `outputdir` option requires version 2.0 alpha 3 or above.

The report has been checked for errors and warnings; there should be none.  
If you want to compile the report manually, see the steps below for reference.

### Manual Compilation

Compilation can be performed using the following commands as reference.

```bash
pdflatex -shell-escape thesis.tex
bibtex thesis
makeindex -s thesis.ist -o thesis.gls -t thesis.glg thesis.glo
```

The steps above do not use the `outputdir` option of the minted package.  
In that case, you have to use the `-output-directory` option for pdfLaTeX.  
You would also need to prepend the output directory to the MakeIndex paths.

Note that you might have to run pdfLaTeX several times after these steps.  
If you're using some tooling support, like Texpad, this will be done for you.

Optionally, the output thesis report can be optimized as described below.

## Optimization

The minified report has had its file size optimized using [pdfsizeopt][pdfsizeopt].  
File size reduction of ~ 43.2% (376.9 KiB) versus the original version. 

The following parameters were used when invoking pdfsizeopt.  
`--use-pngout=true --use-jbig2=true --use-multivalent=true`

[latex]: http://www.latex-project.org
[draw.io]: https://www.draw.io
[pgfplots]: http://pgfplots.sourceforge.net
[front]: includes/university/front.tex
[title]: includes/university/title.tex
[copyright]: includes/university/copyright.tex
[templates]: http://www.ida.liu.se/edu/ugrad/thesis/templates/index.en.shtml
[report]: thesis.pdf
[report-min]: thesis.min.pdf
[published]: http://urn.kb.se/resolve?urn=urn:nbn:se:liu:diva-110676
[best-thesis]: http://www.student.liu.se/nyhetsarkiv/1.635568?l=sv
[best-thesis-ida]: http://www.ida.liu.se/department/news/2015.sv.shtml#130
[best-thesis-ida-en]: http://www.ida.liu.se/department/news/2015.en.shtml#130
[best-thesis-dfs]: https://dfs.se/jubileumsglans-at-arets-examensarbete
[texpad]: https://www.texpadapp.com
[mactex]: https://tug.org/mactex
[minted]: https://github.com/gpoore/minted
[pdfsizeopt]: https://code.google.com/p/pdfsizeopt