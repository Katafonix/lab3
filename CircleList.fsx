#load "Vehicle.fsx"

open Vehicle

type Node(data: Vehicle, next: Node option) =
    member val Data = data with get, set
    member val Next = next with get, set

type CircularLinkedList() =
    let mutable head = None
    let mutable tail = None
    let mutable size = 0

    member this.Push(data: Vehicle) =
        let node = new Node(data, None)

        match tail with
        | None ->
            head <- Some node
            tail <- Some node
            node.Next <- Some node
            size <- size + 1
        | Some t ->
            node.Next <- head
            t.Next <- Some node
            tail <- Some node
            size <- size + 1

    member this.Size = size

    member this.Remove (data: string) =
        if (size = 0) then 
            failwith "Список пуст, нельзя удалить элемент"
        
        let mutable previous = None
        let mutable current = head
        let mutable countList = size

        for i in 1 .. size do
            if (current.Value.Data.Manufacturer = data) then
                if current = head && current = tail then
                    head <- None
                    tail <-  None
                elif current = head then
                    head <- current.Value.Next
                    tail.Value.Next <- head
                elif current = tail then
                    tail <- previous
                    tail.Value.Next <- head
                else
                    previous.Value.Next <- current.Value.Next
                    current <- previous
                size <- size - 1
            previous <- current
            current <- current.Value.Next
            countList <- countList - 1

    member this.Print() =
            let rec print (node: Node) =
                printfn "%A" node.Data

                if not (obj.ReferenceEquals(node.Next.Value, head.Value)) then
                    print node.Next.Value

            print head.Value