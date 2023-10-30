#load "Vehicle.fsx"
#load "CircleList.fsx"
open System.IO
open CircleList
open Vehicle

type Command<'T> =
    abstract member Execute : list:CircularLinkedList * parameters:string[] -> unit

type AddCommand() =
    interface Command<Vehicle> with
        override this.Execute(list, parameters) =
            let objectType = parameters.[1]
            let power = int parameters.[2]
            let manufacturer = parameters.[3]

            match objectType with
            | "Truck" ->
                let capacity = int parameters.[4]
                list.Push(Truck(power, manufacturer, capacity))
            | "Bus" ->
                let capacity = int parameters.[4]
                list.Push(Bus(power, manufacturer, capacity))
            | "Car" ->
                let nDoors = int parameters.[4]
                let maxSpeed = int parameters.[5]
                list.Push(Car(power, manufacturer, nDoors, maxSpeed))
            | _ -> failwith "Неизвестный объект"


type CommandProcessor<'T>() =
    let commands = System.Collections.Generic.Dictionary<string, Command<'T>>()
    member this.Register(name:string, command:Command<'T>) = commands.Add(name, command)
    member this.Process(lst:CircularLinkedList, line:string) =
        let parameters = line.Split(' ')
        let commandName = parameters.[0]
        match commands.TryGetValue(commandName) with
        | true, command -> 
            try
                command.Execute(lst, parameters)
            with e -> printfn "Ошибка при выполнении команды: %s" e.Message
        | _ -> failwithf "Неизвестная команда: %s" commandName

type CommandReader<'T>(commandProcessor: CommandProcessor<'T>) =
    member this.ReadFile (list:CircularLinkedList, fileName:string) =
        let commands = ref []
        use file = new StreamReader(fileName)
        while not file.EndOfStream do
            let line = file.ReadLine()
            commandProcessor.Process(list, line)

type PrintCommand<'T>() =
    interface Command<'T> with
        override this.Execute(list, _) =
            list.Print()

type RemoveCommand<'T>() =
    interface Command<'T> with
        override this.Execute(list, parameters) =
            if parameters.Length <> 2 then
                failwith "Некорректное количество параметров для команды REM"

            let manufacturer = parameters.[1]
            list.Remove(manufacturer)