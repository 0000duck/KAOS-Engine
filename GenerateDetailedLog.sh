#The following command generates a report from the git log history.


git log --pretty=format:'<div><p><a href="http://github.com/stomppah/KAOS-Engine/commit/%H">%s</a>  -  %h</p><p>authored by %an, %ar</p></div><pre>' --shortstat -p -U1 --date-order --reverse --decorate -- *.cs *.glsl --dense --ancestry-path | grep -v -- Properties/ > ../DetailedLog-Ancestry-Path.html