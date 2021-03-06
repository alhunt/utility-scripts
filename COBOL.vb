Imports System
Imports EnvDTE
Imports EnvDTE80
Imports EnvDTE90
Imports System.Diagnostics
Imports System.Windows
Imports System.Windows.Forms

Public Module COBOL

    '' Move caret to Procedure Division
    '' Alex Hunt 2012
    ''
    Sub GoToProcedureDiv()
        DTE.Find.FindWhat = "PROCEDURE DIVISION"
        DTE.Find.Target = vsFindTarget.vsFindTargetCurrentDocument
        DTE.Find.MatchCase = True
        DTE.Find.MatchWholeWord = False
        DTE.Find.Backwards = False
        DTE.Find.MatchInHiddenText = True
        DTE.Find.PatternSyntax = vsFindPatternSyntax.vsFindPatternSyntaxLiteral
        DTE.Find.Action = vsFindAction.vsFindActionFind
        If (DTE.Find.Execute() = vsFindResult.vsFindResultNotFound) Then
            ' If no PROCEDURE DIVISION is found nothing changes.
        Else
            Dim textSelection As EnvDTE.TextSelection

            textSelection = DTE.ActiveDocument.Selection
            textSelection.ActivePoint.TryToShow(vsPaneShowHow.vsPaneShowTop)
            textSelection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText)
        End If
    End Sub

    '' Move caret to Data Division
    '' Alex Hunt 2012
    ''
    Sub GoToDataDiv()
        DTE.Find.FindWhat = "DATA DIVISION"
        DTE.Find.Target = vsFindTarget.vsFindTargetCurrentDocument
        DTE.Find.MatchCase = True
        DTE.Find.MatchWholeWord = False
        DTE.Find.Backwards = False
        DTE.Find.MatchInHiddenText = True
        DTE.Find.PatternSyntax = vsFindPatternSyntax.vsFindPatternSyntaxLiteral
        DTE.Find.Action = vsFindAction.vsFindActionFind
        If (DTE.Find.Execute() = vsFindResult.vsFindResultNotFound) Then
            ' If no DATA DIVISION is found nothing changes.
        Else
            Dim textSelection As EnvDTE.TextSelection

            textSelection = DTE.ActiveDocument.Selection
            textSelection.ActivePoint.TryToShow(vsPaneShowHow.vsPaneShowTop)
            textSelection.StartOfLine(vsStartOfLineOptions.vsStartOfLineOptionsFirstText)
        End If
    End Sub

    '' Remove sequence numbers from columns 1-6 and 73-80
    '' Alex Hunt 2012
    ''
    Sub RemoveSequenceNum()
        Dim textSelection As EnvDTE.TextSelection
        Dim startPoint As Integer
        Dim endPoint As Integer
        Dim temp As Integer
        Dim line As Integer

        textSelection = DTE.ActiveDocument.Selection()
        startPoint = DTE.ActiveDocument.Selection.TopLine
        endPoint = DTE.ActiveDocument.Selection.BottomLine

        If endPoint < startPoint Then
            temp = startPoint
            startPoint = endPoint
            endPoint = temp
        End If

        line = startPoint

        DTE.UndoContext.Open("Remove Line Numbers")
        Try
            Do While (line <= endPoint)
                textSelection.MoveToLineAndOffset(line, 1)
                textSelection.DestructiveInsert("      ")
                textSelection.MoveToLineAndOffset(line, 73)
                textSelection.DestructiveInsert("        ")
                textSelection.DeleteLeft(8)
                line = line + 1
            Loop
        Finally
            ' If an error occurred, then make sure that the undo context is cleaned up.
            ' Otherwise, the editor can be left in a perpetual undo context.
            DTE.UndoContext.Close()
        End Try
    End Sub
End Module

