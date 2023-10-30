type Vehicle(power: int, manufacturer: string) =
    do
        if not (System.Char.IsUpper(manufacturer.[0]) && manufacturer |> Seq.forall System.Char.IsLetter) then
            failwith "Некорректный параметр manufacturer"

    member val Power = power
    member val Manufacturer = manufacturer

    member this.GetManufacturer() = manufacturer

type Car(power: int, manufacturer: string, nDoors: int, maxSpeed: int) =
    inherit Vehicle(power, manufacturer)

    do
        if not (nDoors >= 2 && nDoors <= 5) then
            failwith "Некорректный параметр nDoors, параметр должен быть в диапозоне от 2 до 5"

    member val NDoors = nDoors
    member val MaxSpeed = maxSpeed

    override this.ToString() =
        sprintf
            "Car { nDoors = %d, maxSpeed = %d, power = %d, manufacturer = %s }"
            this.NDoors
            this.MaxSpeed
            this.Power
            this.Manufacturer

type Bus(power: int, manufacturer: string, capacity: int) =
    inherit Vehicle(power, manufacturer)
    member val Capacity = capacity

    override this.ToString() =
        sprintf "Bus { capacity = %d, power = %d, manufacturer = %s }" this.Capacity this.Power this.Manufacturer

type Truck(power: int, manufacturer: string, capacity: int) =
    inherit Vehicle(power, manufacturer)
    member val Capacity = capacity

    override this.ToString() =
        sprintf "Truck { capacity = %d, power = %d, manufacturer = %s }" this.Capacity this.Power this.Manufacturer