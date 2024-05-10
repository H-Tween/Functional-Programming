open System
open Account

Console.WriteLine(" Select: ")
Console.WriteLine(" 1. Task 1 ")
Console.WriteLine(" 2. Task 2 ")
Console.WriteLine(" 3. Task 3 ")
Console.WriteLine(" 3. Task 4 ")
Console.WriteLine(" 4. Exit ")
Console.WriteLine("")

let rec getUserInput () =
    Console.Write("Enter your choice: ")
    let input = Console.ReadLine()
    match Int32.TryParse(input) with
    | (true, choice) when choice >= 1 && choice <= 5 -> choice
    | _ ->
        Console.WriteLine("Invalid input, try again")
        Console.WriteLine("")
        getUserInput ()

let runChoice (choice) =
    match choice with
        | 1 -> task1()
        | 2 -> task2()
        | 3 -> task3()
        | 4 -> exit 0
        | 5 -> exit 0
        | _ -> failwith "Invalid choice."

let choice = getUserInput()

if choice = 4 then
    task4()

printfn ""
let runProgram = runChoice(choice)
