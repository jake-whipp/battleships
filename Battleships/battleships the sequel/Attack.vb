Module Attack
    Public Function determineFirstAttacker()
        Randomize()
        Return (Int((2) * Rnd() + 1))
    End Function

    Public Function computerAttack()
        Randomize()

        Dim xPos, yPos As Integer
        xPos = Int((8 - 0 + 1) * Rnd() + 0) ' random generation for x and y between 0 and 8 (or 1 and 9)
        yPos = Int((8 - 0 + 1) * Rnd() + 0)

        Return xPos & "," & yPos
    End Function

    Public Function playerAttack()
        Console.WriteLine(Environment.NewLine & "Please input an x-coordinate that is less than 10 and greater than 0.")
        Dim userInput As String = CStr(Console.ReadLine())
        Dim xPos, yPos As Integer

        Dim validOutput As Boolean = True

        If userInput.Length() = 1 Then
            If Asc(userInput) >= 49 And Asc(userInput) <= 57 Then
                yPos = CInt(userInput) ' visual basic inverts 2-dimensional coordinates of arrays so we will assign the values backwards. (y then x)

                Console.WriteLine(Environment.NewLine & "Now please input a y-coordinate that is less than 10 and greater than 0.")
                userInput = CStr(Console.ReadLine())

                If userInput.Length() = 1 Then
                    If Asc(userInput) >= 49 And Asc(userInput) <= 57 Then
                        xPos = CInt(userInput) ' visual basic inverts 2-dimensional coordinates of arrays so we will assign the values backwards. (y then x)
                    Else
                        validOutput = False
                        Console.WriteLine("This coordinate is not valid.")
                        Console.ReadLine()
                    End If
                Else
                    validOutput = False
                    Console.WriteLine("This coordinate is not valid.")
                    Console.ReadLine()
                End If
            Else
                validOutput = False
                Console.WriteLine("This coordinate is not valid.")
                Console.ReadLine()
            End If

        Else
            validOutput = False
            Console.WriteLine("This coordinate is not valid.")
            Console.ReadLine()
        End If



        If validOutput = True Then
            Return xPos & "," & yPos
        Else
            Return "invalid"
        End If
    End Function
End Module
