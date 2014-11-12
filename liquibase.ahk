#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.


:*:<CS>::
SendInput, {Raw}<changeSet id="" author="Caio">`n<comment></comment>`n`n</changeSet>
SendInput, {Up 3}{End}{Left 16}
Return

:*:<RT>::
SendInput, {RAW}<renameTable `rnewTableName=""`roldTableName=""`r/>
SendInput, {Up 2}{End}{Left}
Return

:*:<RC>::
InputBox, tableName, Table Name
InputBox, nCol, Number of Columns
Loop %nCol%
{
SendInput, {RAW}<renameColumn `rtableName="%tableName%"`roldColumnName=""`rnewColumnName=""`r/>`r
}
nCol := nCol-1
Loop %nCol%
{
SendInput, {Up 5}
}
SendInput, {Up 3}{End}{Left}
Return

:*:<RV>::
SendInput, {RAW}<renameView `roldViewName=""`rnewViewName=""`r/>
SendInput, {Up 2}{End}{Left}
Return

^r::Reload