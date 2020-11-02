Module GameConditions
    Public Function checkForWin(playerBoard As Array, computerBoard As Array)
        Dim playerWin As Boolean = True
        Dim computerWin As Boolean = True

        For i = 0 To 8
            For v = 0 To 8
                If computerBoard(i, v) = "B" Or computerBoard(i, v) = "C" Then
                    playerWin = False
                End If
            Next
        Next

        For _i = 0 To 8
            For _v = 0 To 8
                If playerBoard(_i, _v) = "B" Or playerBoard(_i, _v) = "C" Then
                    computerWin = False
                End If
            Next
        Next

        If playerWin = True Then
            Return "player"
        ElseIf computerWin = True Then
            Return "computer"
        Else
            Return "undetermined"
        End If
    End Function


    'to-do:  --------------------------------------------
    '   -save and load functions/subroutines
End Module
