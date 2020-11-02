Module Board
    Public Sub SetupBoard(a As Array)

        Dim shipSize As Integer = 5
        For i = 1 To 3 ' 3 big boats
            Randomize()


            Dim xPos, yPos As Integer
            Dim breakLoop As Boolean = True ' two booleans are required for this loop to function
            Dim takenSpace As Boolean = False

            Do Until breakLoop = False
                xPos = Int((8 - 0 + 1) * Rnd() + 0) '' repeatedly generate random coordinates until they are both inbounds and not overlapping a ship
                yPos = Int((8 - 0 + 1) * Rnd() + 0)
                takenSpace = False '' redefine the boolean as false every time the coordinates refresh to prevent it being stuck as "true"

                For takenChecker = 0 To shipSize - 1 '' indexing is at 0 so minus 1
                    If xPos + shipSize > 8 Then '' if out of bounds:
                        takenSpace = True '' we don't want to exit the do loop so we will pretend it is "taken" although being out of bounds
                        Exit For
                    Else
                        If a(xPos + takenChecker, yPos) = "B" Then '' if spot is taken by a battleship:
                            takenSpace = True
                            Exit For
                        End If
                    End If
                Next

                If takenSpace = False Then
                    breakLoop = False
                    Exit Do '' a catch incase the "breakloop = false" does not exit the "do" loop
                End If
                'repeat until takenspace is false
            Loop

            'place ships:
            For v = 0 To shipSize - 1
                a(xPos + v, yPos) = "B"
            Next

            shipSize = shipSize - 1
        Next


        shipSize = 2 ' change the shipsize to 2
        For i = 1 To 2 ' 2 small boats
            For v = 1 To 2 ' we need to generate them twice for each size of small boat. so a nested for loop works. it will generate 2 ships of size 2, followed by 2 ships of size 1.
                Randomize()

                ' same code as before but on a different axis
                Dim xPos, yPos As Integer
                Dim breakLoop As Boolean = True
                Dim takenSpace As Boolean = False

                Do Until breakLoop = False
                    xPos = Int((8 - 0 + 1) * Rnd() + 0)
                    yPos = Int((8 - 0 + 1) * Rnd() + 0)
                    takenSpace = False

                    For takenChecker = 0 To shipSize - 1
                        If yPos + shipSize > 8 Then
                            takenSpace = True
                            Exit For
                        Else
                            If a(xPos, yPos + takenChecker) = "B" Then
                                takenSpace = True
                                Exit For
                            End If
                        End If
                    Next

                    If takenSpace = False Then
                        breakLoop = False
                        Exit Do
                    End If
                Loop


                'place ships:
                For _v = 0 To shipSize - 1
                    a(xPos, yPos + _v) = "C" ' "C" will represent a sideways ship and "B" will represent a downwards ship
                Next

            Next
            shipSize = shipSize - 1
        Next
    End Sub

    Public Sub ModifyBoard(a As Array, x As Integer, y As Integer, content As String)
        If x <= 8 And y <= 8 Then
            a(x, y) = content
        End If
    End Sub

    Public Function CheckForShip(boardToScan As Array, x As Integer, y As Integer)
        If boardToScan(x, y) = "B" Or boardToScan(x, y) = "C" Or boardToScan(x, y) = "X" Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Sub DrawBoards(a As Array, b As Array)
        Console.Clear()
        Console.WriteLine("Enemy's ships:")
        For i = 0 To 8
            For v = 0 To 8
                If a(i, v) = "" Then
                    Console.Write("~")
                Else
                    Console.Write(a(i, v))
                End If

            Next
            Console.WriteLine()
        Next


        Console.WriteLine(Environment.NewLine & "Your ships:")
        For i = 0 To 8
            For v = 0 To 8
                If b(i, v) = "" Then
                    Console.Write("~")
                Else
                    Console.Write(b(i, v))
                End If

            Next
            Console.WriteLine()
        Next
    End Sub
End Module
