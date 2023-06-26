module Module2
open System

//Создать свойство (тип – запись) для всех теоретических характеристик. 
type IdealCharacteristics =                 // тип данных - запись
    {
    a: float32;                             // записи (record) с двумя полями a и b
    b: float32;
    }
    //Осуществлять подсчёт этих характеристик в свойствах самой записи.
    member this.getMathExpection =        // математическое ожидание
        (this.a + this.b) / 2.0f

    member this.getDispersion =            // вычисляет дисперсию, которая является средним квадратичным отклонением между a и b (для равномерного распределения)
        ((this.b - this.a) * (this.b - this.a)) / 12.0f

    member this.getDeviation =             // вычисляет стандартное отклонение
        (this.b - this.a) / 2.0f * float32(Math.Sqrt(3.0))

let Ideal: IdealCharacteristics = {
    a = 41f;
    b = 64f;
}

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

    member this.ComputeStandardDeviation() =                        // среднеквадратичное отклонение
        let variance = this.ComputeVariance()
        sqrt variance
(*
    member this.ComputeCorrelationCoefficient() =
        let mean = this.ComputeMeanLength()                         // Вычислить среднее значение для последовательности чисел
        printfn "mean:";
        printfn "%A" mean;              // 52.46493333
                                                                    // Вычислить сумму квадратов разностей между элементами и средним значением
        let sumSquaredDeviations = Array.sumBy (fun x -> (float x - mean) ** 2.0) messageLengths
        printfn "sumSquaredDeviations:";
        printfn "%A" sumSquaredDeviations;

        let variance = Array.map (fun length -> float (length - int mean) ** 2.0) messageLengths
                    |> Array.sum
                    |> (/) (float (messageLengths.Length - 1))
        printfn "variance:";
        printfn "%A" variance;

        let standardDeviation = sqrt variance
        printfn "standardDeviation:";
        printfn "%A" standardDeviation; // 0.1439710515
        let normalizedLengths = Array.map (fun length -> float (float length - mean) / standardDeviation) messageLengths
        printfn "normalizedLengths:";
        printfn "%A" normalizedLengths;
        let squaredNormalizedLengths = Array.map (fun length -> length * length) normalizedLengths

        printfn "squaredNormalizedLengths:";
        printfn "%A" squaredNormalizedLengths;
        let sumProduct = Array.map2 (*) normalizedLengths squaredNormalizedLengths
                         |> Array.sum

        let correlationCoefficient = sumProduct / float (messageLengths.Length - 1)

        correlationCoefficient
*)

    member this.CorrelationCoefficient() =
        let lengthProductSum = 
            let lastIndex = Array.length messageLengths - 1
            Array.fold (fun acc i -> acc + messageLengths.[i] * messageLengths.[(i + 1) % lastIndex]) 0 messageLengths
    
        let sumSquared = Array.sum messageLengths |> float |> fun sum -> sum ** 2.0
        let sumSquaredValues = float (Array.sumBy (fun x -> x * x) messageLengths)
        let n = float this.k
        //let n = float messageLengths.Length
        printfn "%A %A %A %A" n lengthProductSum sumSquared sumSquaredValues
        let res = (n * (float lengthProductSum) - sumSquared) / (n * sumSquaredValues - sumSquared)
        res

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
        printfn "Математическое ожидание2:";
        printfn "%A" resultExpect2;
        printfn "Дисперсия:";
        let result3 = this.ComputeVariance();
        printfn "%A" result3;
        printfn "среднеквадратичное отклонение:";
        let result4 = this.ComputeStandardDeviation();
        printfn "%A" result4;
        printfn "Идеальные характеристики:"
        printfn "Математическое ожидание:"
        printfn "%A" Ideal.getMathExpection;
        printfn "Дисперсия:"
        printfn "%A" Ideal.getDispersion;
        printfn "Стандартное отклонение:"
        printfn "%A" Ideal.getDeviation;
        printfn ""
        printfn "к-т корреляции:"
        printfn "%A" (this.CorrelationCoefficient())