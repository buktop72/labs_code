module Module2
open System

type Lab3(minLength: int, maxLength: int, p: float, count: int) =
    inherit Module1.Basic(count)

    let mutable messageLengths : int array = [||]

    member this.GenerateMessageLengths() =
        let rand = new Random()
        messageLengths <- Array.init this.k (fun _ -> 
            let rec generateLength() =
                let length = rand.Next(minLength, maxLength + 1)
                let probability = rand.NextDouble()
                if probability <= p then
                    length
                else
                    generateLength()
            generateLength()
        )

    member this.PrintMessageLengths() =
        printfn "Message Lengths:"
        printfn "%A" messageLengths
        let res1 = Array.min messageLengths
        let res2 = Array.max messageLengths
        printfn "%A %A" res1 res2



