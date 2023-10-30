#load "Vehicle.fsx"
#load "CircleList.fsx"
#load "Command.fsx"

open Vehicle
open CircleList
open Command

let commandProcessor = CommandProcessor<Vehicle>()
commandProcessor.Register("ADD", AddCommand())
commandProcessor.Register("REM", RemoveCommand<Vehicle>())
commandProcessor.Register("PRINT", PrintCommand<Vehicle>())

let commandReader = CommandReader<Vehicle>(commandProcessor)

let list = CircularLinkedList()

commandReader.ReadFile(list, "commands.txt")
