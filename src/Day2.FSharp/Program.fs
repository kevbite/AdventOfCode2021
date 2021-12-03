open System
open System.IO

type Aim = {
    AimUnits: int
    Horizontal: int
    Depth: int
}

type Position = {
    Horizontal: int
    Depth: int
}

let lines = File.ReadLines("example.txt")
                |> Seq.map (fun line -> line.Split(' '))
                |> Seq.map (fun line -> (line[0], Int32.Parse line[1]))
                |> Seq.toList

let part1 = 
      
    let position : Position = lines |> List.fold (fun current (direction, distance) ->
                                            match direction with
                                            | "forward" ->  {current with Horizontal = current.Horizontal + distance}
                                            | "down" ->  {current with Depth = current.Depth + distance}
                                            | "up" ->  {current with Depth = current.Depth - distance}
                                            | _ -> failwith "Unknown direction"
                                       ) { Horizontal= 0; Depth= 0}

    printf $"Horizontal position of %d{position.Horizontal} and a depth of %d{position.Depth}\n"
    
let part2 =       
    let aim : Aim = lines |> List.fold (fun current (direction, distance) ->
                                            match direction with
                                            | "forward" -> { current with Horizontal = current.Horizontal + distance; Depth = current.Depth + current.AimUnits * distance }
                                            | "down" ->  {current with AimUnits = current.AimUnits + distance}
                                            | "up" ->  {current with AimUnits = current.AimUnits - distance}
                                            | _ -> failwith "Unknown direction"
                                       ) { Horizontal= 0; Depth= 0; AimUnits = 0; } 

    printf $"Horizontal position of %d{aim.Horizontal} and a depth of %d{aim.Depth}\n"
    
    
    
part1
part2