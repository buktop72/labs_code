module Module2
open System

type Lab3(minLength: int, maxLength: int, p: float, count: int) =
    inherit Module1.Basic(count)

    let mutable messageLengths : int array = [||]

    member this.GenerateMessageLengths() =                          // Моделирование длин сообщений 
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


    member this.MathExpect arr =                                      // а) математическое ожидание
        let sum = arr |> Array.sum
        decimal sum / decimal this.k
(*
    member this.ComputeMeanLength() =                               // а) математическое ожидание
        let sum = Array.sum messageLengths
        let mean = float sum / float messageLengths.Length
        mean
*)
    member this.ComputeMeanLength() =                               // а) математическое ожидание
        let sum = messageLengths |> Array.sum
        float sum / float messageLengths.Length

    member this.ComputeVariance() =                                 // Дисперсия
        let mean = this.ComputeMeanLength()
        let squaredDifferences = Array.map (fun length -> (float length - mean) ** 2.0) messageLengths
        let variance = Array.sum squaredDifferences / float messageLengths.Length
        variance

    member this.ComputeStandardDeviation() =
        let variance = this.ComputeVariance()
        sqrt variance


    member this.PrintMessageLengths() =
        printfn "Message Lengths:"
        printfn "%A" messageLengths
        let res1 = Array.min messageLengths
        let res2 = Array.max messageLengths
        printfn "%A %A" res1 res2
        let resultExpect = this.MathExpect messageLengths
        printfn "Математическое ожидание:";
        printfn "%A" resultExpect;
        let resultExpect2 = this.ComputeMeanLength() 
        resultExpect2
        printfn "Математическое ожидание2:";
        printfn "%A" resultExpect2;
        printfn "Дисперсия:";
        let result3 = this.ComputeVariance();
        printfn "%A" result3;
        printfn "среднеквадратичное отклонение:";
        let result4 = this.ComputeStandardDeviation();
        printfn "%A" result4;


