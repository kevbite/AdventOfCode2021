open System.IO

let lines = File.ReadLines("example.txt")
                |> Seq.map System.Int32.Parse
                |> Seq.toList

let part1 =
    let zipped = lines |> Seq.zip (lines |> Seq.skip(1))

    let measurementIncreases = zipped
                                    |> Seq.where (fun (item1, item2) -> item2 > item1) 

    printf $"There are %d{Seq.length measurementIncreases} measurements that increased\n"

let part2 =
    let windows = lines |> Seq.windowed 3
                           |> Seq.map (fun item -> item |> Seq.sum)
                           |> Seq.toList

    let measurementIncreases = windows
                                    |> Seq.zip (windows |> Seq.skip(1))
                                    |> Seq.where (fun (item1, item2) -> item2 > item1) 

    printf $"There are %d{Seq.length measurementIncreases} measurements that increased\n"


part1
part2
