#The following command generates a report from the git log history.
#

git log --pretty=format:'<div><p><a href="http://github.com/stomppah/KAOS-Engine/commit/%H">%s</a>  -  %h</p><p>authored by %an, %ar</p></div><pre>' --shortstat -p -U1 --reverse  --decorate -- AWGL/*.cs -- KAOS/*.cs -- Game/*cs -- AWGL/*.glsl -- KAOS/*.glsl | grep -v -- Properties/ > ../DetailedLog.html