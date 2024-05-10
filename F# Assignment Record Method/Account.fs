module Account

open System
open System.Threading

type CustomerAccount = 
    {
        name : string
        accountNumber : string
        mutable balance : float 
    }

    // Print Customer
    member this.Print() = 
        Console.WriteLine($" Name: {this.name}")
        Console.WriteLine($" Account Number: {this.accountNumber}")
        Console.WriteLine($" Balance: {this.balance}")
        Console.WriteLine(" ")


    // Update balance of Customer
    member this.UpdateBalance newBalance = {this with balance = newBalance}

    // Withdraw from account
    member this.Withdraw(amount: float) =
        if amount <= 0.0 then
            failwith "Withdrawal amount must be greater than zero"
        elif amount > this.balance then
            failwith "Insufficient funds"
        else
            this.UpdateBalance(this.balance - amount)

    // Deposit into account
    member this.Deposit(amount: float) =
        if amount <= 0.0 then
            failwith "Deposit amount must be greater than zero"
        else
            this.UpdateBalance(this.balance + amount)
    
// Check customer balance
let CheckBalance (balance: float) =
    match balance with
    | b when b < 10.0 -> "Balance is low"
    | b when b >= 10.0 && b <= 100.0 -> "Balance is OK"
    | _ -> "Balance is high"

// Create customer records
let customer = {name = "Kim"; accountNumber = "0007"; balance = 1000}
let customer1 = {name = "John"; accountNumber = "0001"; balance = 0.0}        
let customer2 = {name = "Alice"; accountNumber = "0002"; balance = 51.0}        
let customer3 = {name = "Harry"; accountNumber = "0003"; balance = 5.0}        
let customer4 = {name = "Jacob"; accountNumber = "0004"; balance = 70.0}       
let customer5 = {name = "Freddie"; accountNumber = "0005"; balance = 55.0}        
let customer6 = {name = "Lewis"; accountNumber = "0006"; balance = 100.0} 

let task1() = 

    customer.Print()

    let customer = customer.Withdraw(300.0)
    printfn "After Withdraw:"
    customer.Print()

    let customer = customer.Deposit(500.0)
    printfn "After Deposit:"
    customer.Print()
    0

let task2() =
    
    Console.WriteLine($" Customer: {customer1.accountNumber} {CheckBalance customer1.balance}")
    Console.WriteLine($" Customer: {customer2.accountNumber} {CheckBalance customer2.balance}")
    Console.WriteLine($" Customer: {customer3.accountNumber} {CheckBalance customer3.balance}")
    Console.WriteLine($" Customer: {customer4.accountNumber} {CheckBalance customer4.balance}")
    Console.WriteLine($" Customer: {customer5.accountNumber} {CheckBalance customer5.balance}")
    Console.WriteLine($" Customer: {customer6.accountNumber} {CheckBalance customer6.balance}")
    0


let task3() =

    let accounts =[customer1; customer2; customer3; customer4; customer5; customer6]
    let below50, above50 = List.partition (fun acc -> acc.balance < 50.0) accounts

    Console.WriteLine("Accounts with balance between 0 and 50:")
    for item in below50 do
        Console.WriteLine($"Name: {item.name}, balance: {item.balance}")
    printfn ""
    Console.WriteLine("Accounts with balance over 50:")
    for item in above50 do
        Console.WriteLine($"Name: {item.name}, balance: {item.balance}")
    0

type Ticket = {seat:int; customer:string}

let mutable tickets = [for n in 1..10 -> {Ticket.seat = n; Ticket.customer = ""}]
let lockObj = new Object() // object for record locking

let displayTickets () =
    printfn ""
    for item in tickets do
        Console.WriteLine($"Customer: {item.customer}, Seat: {item.seat}")
    Console.WriteLine(" ")

let bookSeat (customerName:string) (seatNumber:int) =
    lock lockObj (fun() ->
    if seatNumber >= 1 && seatNumber <= 10 then
        let updatedTickets =
            tickets |> List.mapi (fun i ticket ->
                if i = seatNumber - 1 && ticket.customer = "" then
                    { ticket with customer = customerName }
                else
                    ticket
            )
        if updatedTickets = tickets then
            Console.WriteLine($"Seat: {seatNumber} is already booked or invalid")
        else
            Console.WriteLine($"Seat: {seatNumber} booked for {customerName}")
            tickets <- updatedTickets
    else
        Console.WriteLine($"Seat: {seatNumber} is invalid")
    )
    Thread.Sleep(1000) // simulates work being done


let task4() =
    displayTickets()
    let thread1 = new Thread(fun() -> bookSeat "Harrison" 3) // assinging a thread
    thread1.Start()                                          // starting thread
    let thread2 = new Thread(fun() -> bookSeat "Alice" 5)
    thread2.Start()
    let thread3 = new Thread(fun() -> bookSeat "Jack" 11)
    thread3.Start()

    thread1.Join()
    thread2.Join()
    thread3.Join()

    displayTickets()