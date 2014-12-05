#NoEnv  ; Recommended for performance and compatibility with future AutoHotkey releases.
SendMode Input  ; Recommended for new scripts due to its superior speed and reliability.
SetWorkingDir %A_ScriptDir%  ; Ensures a consistent starting directory.

;;;GUI;;;
;Gui for constraints
  ; The label ButtonOK (if it exists) will be run when the button is pressed.


;;;HOTSTRINGS;;;
:*:<sql>::
SendInput, {Raw}<sql>`r`r</sql>
GoUp(1,0)
Return

:*:<init>::
SendInput, {Raw}<?xml version="1.0" encoding="UTF-8"?>`r`r<databaseChangeLog`r  xmlns="http://www.liquibase.org/xml/ns/dbchangelog"`r  xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"`r  xsi:schemaLocation="http://www.liquibase.org/xml/ns/dbchangelog`r         http://www.liquibase.org/xml/ns/dbchangelog/dbchangelog-3.1.xsd">`r`r</databaseChangeLog>
GoUp(1,0)
Return

:*:<rollback>::
SendInput, {Raw}<rollback>`r<sql>`r`r</sql>`r</rollback>
GoUp(2,0)
Return

:*:<DI>::
SendInput, {Raw}<dropIndex indexName="" tableName=""/>
GoUp(0,16)
Return

:*:<CP>::
SendInput, {Raw}<createProcedure procedureName="">`r`r</createProcedure>
GoUp(2,2)
Return

:*:<AUC>::
SendInput, {Raw}<addUniqueConstraint constraintName="" tableName="" columnNames=""/>
GoUp(0,31)
Return

:*:<DUC>::
SendInput, {Raw}<dropUniqueConstraint tableName="" constraintName=""/>
GoUp(0,21)
Return

:*:<DFK>::
SendInput, {Raw}<dropForeignKeyConstraint baseTableName="" constraintName=""/>
GoUp(0,21)
Return

:*:<DT>::
SendInput, {Raw}<dropTable tableName=""/>
GoUp(0,3)
Return

:*:<DC>::
SendInput, {Raw}<dropColumn tableName="" columnName=""/>
GoUp(0,17)
Return

:*:<CS>::
SendInput, {Raw}<changeSet id="" author="Caio">`n<comment></comment>`n`n</changeSet>
SendInput, {Up 3}{End}{Left 16}
Return

:*:<RT>::
SendInput, {RAW}<renameTable `rnewTableName=""`roldTableName=""`r/>
SendInput, {Up 2}{End}{Left}
Return

:*:<MDT>::
SendInput, {RAW}<modifyDataType `rtableName=""`rcolumnName=""`rnewDataType=""`r/>
SendInput, {Up 3}{End}{Left}
Return



:*:<RC>::
InputBox, tableName, Table Name
InputBox, nCol, Number of Columns
Loop %nCol%
{
SendInput, {RAW}<renameColumn `rtableName="%tableName%"`roldColumnName=""`rnewColumnName=""`r/>`r
}
nCol := 5*(nCol-1)+3
GoUp(nCol)
Return

:*:<RV>::
SendInput, {RAW}<renameView `roldViewName=""`rnewViewName=""`r/>
SendInput, {Up 2}{End}{Left}
Return

:*:<CT>::
InputBox, nCol, Number of Columns
SendInput, {RAW}<createTable tableName="">`r
Loop %nCol% 
{
addColumn("")
}
nCol := 5*nCol+2
SendInput, {RAW}`r</createTable>
goUp(nCol,2)
Return

:*:<AC>::
addColumn()
goUp(5,2)
Return

:*:<INS>::
InputBox, nCol, How many columns?
SendInput, {RAW}<insert `rtableName="">`r
Loop %nCol%
{
SendInput, {RAW}<column name="" value=""/>`r
}
SendInput, {RAW}</insert>
nCol := nCol*1+1
GoUp(nCol,2)
Return

:*:<constraints>::
Gui, Add, Text,, Foreign Key Name:
Gui, Add, Text,, References Table(Column):
Gui, Add, Edit, vFKName ym
Gui, Add, Edit, vReferences
Gui, Add, CheckBox, vNullable Checked, Nullable
Gui, Add, CheckBox, vPrimary, Primary Key
Gui, Add, Button, default, OK
Gui, Show
return


;;;FUNCTIONS;;;
goUp(num=1, num2=1)
{
SendInput, {Up %num%}{End}{Left %num2%}
}

addColumn(tableName=0)
{
if (tableName=0)
    SendInput, {RAW}<addColumn`rtableName="">`r<column name=""`rtype="">`r</column>`r</addColumn>`r
else
    SendInput, {RAW}<column name=""`rtype="">`r</column>`r
Return
}


GuiClose:
ButtonOK:
Gui, Submit  ; Save the input from the user to each control's associated variable.
if Primary
    Primary:="primaryKey=""true"" " 
else
    Primary=
if not Nullable
    Nullable:="nullable=""false"" "
else
    Nullable=
if (References and FKName)
    References=foreignKeyName="%FKName%" references="%References%" deleteCascade="true"
else
    References=
SendInput, {RAW}<constraints %Primary%%Nullable%%References%/>
Gui, Destroy
return

;;;HOTKEYS;;;
F11::
SendInput, ^f
SendInput, {Raw}""
SendInput, {Enter}{ESC}{Left}{Right}
Return


^r::Reload