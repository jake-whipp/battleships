Module Module1
    Dim publicBoard(8, 8) As String ' board that the user will be seeing

    Dim computer_privateBoard(8, 8) As String ' where the boats are held for the computer
    Dim player_privateBoard(8, 8) As String ' where the boats are held for the player

    Dim attacker As Integer ' who is currently attacking identified by "1" for computer and "2" for player
    Dim gameWon As String = "undetermined"

    Sub Main()
        Console.Title = "Battleships - Main menu"

        Dim validAnswer As Boolean = False

        Do Until validAnswer = True
            Console.Clear()

            Console.ForegroundColor = ConsoleColor.Cyan
            Console.WriteLine("==================== Welcome to battleships! ====================")
            Console.ForegroundColor = ConsoleColor.Gray
            Console.WriteLine(Environment.NewLine & "[1]: Begin new game" & Environment.NewLine & "[2]: Information" & Environment.NewLine & "[3]: Exit game")
            Dim userInput As String = CStr(Console.ReadLine())

            If userInput.Length() = 1 Then
                If userInput = "1" Then
                    validAnswer = True
                    Game() ' send user to the game
                ElseIf userInput = "2" Then
                    ' do not satisfy the condition of the "do" loop here, so that when the user presses enter, they are returned to the menu automatically
                    Console.Clear()

                    Console.ForegroundColor = ConsoleColor.Cyan
                    Console.WriteLine("==================== Welcome to battleships! ====================")

                    Console.ForegroundColor = ConsoleColor.DarkCyan
                    Console.WriteLine(Environment.NewLine & "How is the game played?:")
                    Console.ForegroundColor = ConsoleColor.Gray
                    Console.WriteLine("The game is played on an alternating-turn basis. The first person to move is randomly selected each game.")
                    Console.WriteLine("If it is your turn, you will be prompted to enter coordinates for X and Y, that are between 1 and 9. If it's")
                    Console.WriteLine("the computer's turn, all you have to do is watch your ships at the bottom, and see if the computer manages to")
                    Console.WriteLine("guess a location that your ship is in. If you manage to do so correctly, and guess all of their ship locations,")
                    Console.WriteLine("you will win the game! If the computer does it first, then they will win instead.")

                    Console.ForegroundColor = ConsoleColor.DarkCyan
                    Console.WriteLine(Environment.NewLine & "What do the different characters on the board represent?:")
                    Console.ForegroundColor = ConsoleColor.Gray
                    Console.WriteLine(" ~   Sea" & Environment.NewLine() & " B   Vertical Battleship" & Environment.NewLine & " C   Horizontal Battleship" & Environment.NewLine & " X   Hit Ship Location" & Environment.NewLine & " M   Missed Ship Location")

                    Console.WriteLine(Environment.NewLine & "Press ENTER to return to the menu")
                    Console.ReadLine()
                ElseIf userInput = "3" Then
                    validAnswer = True ' satisfy condition of "do" loop
                    End ' close the program
                Else
                    Console.WriteLine(Environment.NewLine & "Invalid answer. Please choose option 1, 2, or 3.")
                    Console.ReadLine()
                End If
            Else
                Console.WriteLine(Environment.NewLine & "Invalid answer. Please choose option 1, 2, or 3.")
                Console.ReadLine()
            End If
        Loop
    End Sub

    Sub Game()
        Board.SetupBoard(computer_privateBoard)
        Board.SetupBoard(player_privateBoard)

        Board.DrawBoards(computer_privateBoard, player_privateBoard) ' xray cheat
        Console.ReadLine()
        attacker = Attack.determineFirstAttacker()
        gameWon = "undetermined"

        Do Until gameWon <> "undetermined"
            If attacker = 1 Then ' if the computer is attacking:
                Console.Title = "Battleships - COMPUTER's turn"
                Dim coordinates = Attack.computerAttack()

                ' the function "computerAttack" will return a string in the format "x,y" so we must manipulate it for the values we need and then input it into a sub
                Board.ModifyBoard(player_privateBoard, CInt(coordinates.substring(0, 1)), CInt(coordinates.substring(2, 1)), "X")

                If Board.CheckForShip(player_privateBoard, CInt(coordinates.Substring(0, 1)), CInt(coordinates.Substring(2, 1))) Then
                    Board.ModifyBoard(player_privateBoard, CInt(coordinates.substring(0, 1)), CInt(coordinates.substring(2, 1)), "X")
                    Board.DrawBoards(publicBoard, player_privateBoard)
                Else ' if not:
                    Board.ModifyBoard(player_privateBoard, CInt(coordinates.substring(0, 1)), CInt(coordinates.substring(2, 1)), "M")
                    Board.DrawBoards(publicBoard, player_privateBoard)
                End If

                ' then update the boards
                Board.DrawBoards(publicBoard, player_privateBoard)

                ' tell the player where the computer shot
                Console.WriteLine("Computer attacked coordinate (" & (CInt(coordinates.substring(2, 1)) + 1) & "," & (CInt(coordinates.substring(0, 1)) + 1) & ")")
                ' and switch attackers
                attacker = 2

            Else ' if the player is attacking:
                Console.Title = "Battleships - YOUR turn"
                Dim coordinates As String = "invalid"

                Do Until coordinates <> "invalid" ' this line should be obvious what it means
                    Board.DrawBoards(publicBoard, player_privateBoard)
                    coordinates = playerAttack()
                Loop


                'if the computer's board has a ship in that spot:
                If Board.CheckForShip(computer_privateBoard, (CInt(coordinates.Substring(0, 1)) - 1), (CInt(coordinates.Substring(2, 1)) - 1)) Then
                    Board.ModifyBoard(publicBoard, (CInt(coordinates.Substring(0, 1)) - 1), (CInt(coordinates.Substring(2, 1)) - 1), "X") ' minus one for each coordinate because user is counting from 1 instead of 0
                    Board.DrawBoards(publicBoard, player_privateBoard)
                    Console.WriteLine(Environment.NewLine & "You Hit!") ' tell them they hit a ship
                Else ' if not:
                    Board.ModifyBoard(publicBoard, (CInt(coordinates.Substring(0, 1)) - 1), (CInt(coordinates.Substring(2, 1)) - 1), "M") ' minus one for each coordinate because user is counting from 1 instead of 0
                    Board.DrawBoards(publicBoard, player_privateBoard)
                    Console.WriteLine(Environment.NewLine & "You Missed!") ' tell them they missed
                End If

                ' manipulate the string again for the values and use them to update the boards

                Board.ModifyBoard(computer_privateBoard, (CInt(coordinates.Substring(0, 1)) - 1), (CInt(coordinates.Substring(2, 1)) - 1), "X")

                attacker = 1
            End If

            Console.ReadLine()

            gameWon = GameConditions.checkForWin(player_privateBoard, computer_privateBoard)
        Loop

        If gameWon = "player" Then
            Console.Title = "Battleships - YOUR Victory!"
            Dim validAnswer As Boolean = False
            Do Until validAnswer = True
                Console.Clear()

                Console.WriteLine("Congratulations! You hit all of the opponent's ships in their entirety and won!")
                Console.WriteLine(Environment.NewLine & "[1]: Return to menu" & Environment.NewLine & "[2]: Exit game")
                Dim userInput As String = CStr(Console.ReadLine())

                If userInput.Length() = 1 Then
                    If userInput = "1" Then
                        validAnswer = True
                        Main()
                    ElseIf userInput = "2" Then
                        validAnswer = True
                        End
                    Else
                        Console.WriteLine(Environment.NewLine & "Invalid answer. Please choose option 1 or 2.")
                        Console.ReadLine()
                    End If
                Else
                    Console.WriteLine(Environment.NewLine & "Invalid answer. Please choose option 1 or 2.")
                    Console.ReadLine()
                End If
            Loop

        ElseIf gameWon = "computer" Then
            Console.Title = "Battleships - COMPUTER'S Victory.."
            Dim validAnswer As Boolean = False
            Do Until validAnswer = True
                Console.Clear()

                Console.WriteLine("Oh no! Unfortunately the computer sunk your ships and has won. Maybe next time?")
                Console.WriteLine(Environment.NewLine & "[1]: Return to menu" & Environment.NewLine & "[2]: Exit game")
                Dim userInput As String = CStr(Console.ReadLine())

                If userInput.Length() = 1 Then
                    If userInput = "1" Then
                        validAnswer = True
                        Main()
                    ElseIf userInput = "2" Then
                        validAnswer = True
                        End
                    Else
                        Console.WriteLine(Environment.NewLine & "Invalid answer. Please choose option 1 or 2.")
                        Console.ReadLine()
                    End If
                Else
                    Console.WriteLine(Environment.NewLine & "Invalid answer. Please choose option 1 or 2.")
                    Console.ReadLine()
                End If
            Loop
        End If
    End Sub

End Module
