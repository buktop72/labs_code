module Module1
open System

(*  1 - temp <= 0.55
    2 - temp > 0.55 && temp <= 0.63
    3 - temp > 0.63 && temp <= 0.75
    4 - temp ? 0.75 *)
    
(*Объявить переменную k как ссылочную ячейку и инициализировать её значением, указанным в варианте. Далее умножить значение в этой переменной на 100*)
let k = ref 6
k := !k * 100
// printfn "count messeges: %i" !k

(*Объявить и инициализировать пустыми значениями два массива для хранения типов сообщений ..*)
(*для представления типов использовать числа*)
let stream : int array = Array.zeroCreate !k
//printfn "%A" stream


(*для представления приёмников – размеченное объединение, каждый элемент которого – кортеж, состоящий из числа и строки*)
type numberReciver =
    | One of int * string
    | Two of int * string
    
(*..и номеров приёмников*)
//let recivers : numberReciver array = Array.zeroCreate !k
// printfn "%A" recivers

(*вероятности и число поступлений сообщений данного типа к каждому приёмнику
((кол-во 1 типа в адр1, вероятность), (кол-во 1 типа в адр2, вероятность))
...
((кол-во 4 типа в адр1, вероятность), (кол-во 4 типа в адр2, вероятность))
*)
//let countReciverProbability : ((int * float) * (int * float)) array = Array.zeroCreate 4
//for i = 0 to 3 do
//    countReciverProbability.[i] <- ((0, 0.0), (0, 0.0))
//printfn "%A" countReciverProbability  // [|((0, 0.0), (0, 0.0)); ((0, 0.0), (0, 0.0)); ((0, 0.0), (0, 0.0)); ((0, 0.0), (0, 0.0))|]

let sigma = 1.8
let mu = 5.6
let maxCountMessage = 1000
//let rand = System.Random()
//Неявный конструктор класса
type Basic =
    val mutable k: int
    val stream: int array
    val recivers: numberReciver array 
    val countReciverProbability : ((int * float) * (int * float)) array
    val countProbability : (int * float) array
    val messagesTime: float array
    new (count: int) =
        {
            k = count * 100
            stream = Array.zeroCreate (count * 100)
            recivers = Array.zeroCreate (count * 100)
            countReciverProbability = [|((0, 0.0), (0, 0.0)); ((0, 0.0), (0, 0.0)); ((0, 0.0), (0, 0.0)); ((0, 0.0), (0, 0.0))|]
            countProbability = [|(0, 0.0); (0, 0.0); (0, 0.0); (0, 0.0)|]
            messagesTime = Array.zeroCreate count
        }
    (*метод Modeling, в котором выполняется моделирование потока сообщений*)
    member this.Modeling(task: bool) =
        let rand = new Random()
        if task then
            for i = 0 to !k-1 do               
                let mutable probability = rand.NextDouble()
                //printfn $"probablity: {probability}"
                if probability <= 0.55 then
                    this.stream.[i] <- 1
                elif probability <= 0.63 then
                    this.stream.[i] <- 2
                elif probability <= 0.75 then
                    this.stream.[i] <- 3
                else
                    this.stream.[i] <- 4
            //printfn "Types messeges:"    
            //printfn "%A" stream

        else
            let mutable i = 0
            while i < !k do
                let mutable probablity = rand.NextDouble()
                if this.stream.[i] = 1 then
                    if probablity <= 0.88 then
                       this.recivers.[i] <- One (1, "one")
                    else
                       this.recivers.[i] <- Two (2, "two")
                if this.stream.[i] = 2 then
                    if probablity <= 0.06 then
                       this.recivers.[i] <- One (1, "one")
                    else
                       this.recivers.[i] <- Two (2, "two")           
                if this.stream.[i] = 3 then
                    if probablity <= 0.3 then
                       this.recivers.[i] <- One (1, "one")
                    else
                       this.recivers.[i] <- Two (2, "two")
                if this.stream.[i] = 4 then
                    if probablity <= 0.54 then
                       this.recivers.[i] <- One (1, "one")
                    else
                       this.recivers.[i] <- Two (2, "two")
                i <- i + 1
            //printfn "%A" this.recivers

    (*считаем вероятности и число поступлений сообщений данного типа к каждому приёмнику*)
    member this.incrementCount(x, y) =
        match y with
        | One (1,"one") -> this.countReciverProbability.[x - 1] <- ((fst (fst this.countReciverProbability.[x - 1]) + 1, float (fst (fst this.countReciverProbability.[x - 1]) + 1) / float (fst this.countProbability.[x - 1])), (fst (snd this.countReciverProbability.[x - 1]),    snd (snd this.countReciverProbability.[x - 1]))) 
        | Two (2,"two") -> this.countReciverProbability.[x - 1] <- ((fst (fst this.countReciverProbability.[x - 1]), snd (fst this.countReciverProbability.[x - 1])    ), (fst (snd this.countReciverProbability.[x - 1]) + 1, float (fst (snd this.countReciverProbability.[x - 1]) + 1) / float (fst this.countProbability.[x - 1])))

    (*Создать метод Calculating*)
    member this.Calculating(task: bool) =
        if task then
            let num1 = (Array.filter (fun elem -> elem  = 1) this.stream).Length
            let probablity1 = float num1 / float !k
            this.countProbability.[0] <- (num1, probablity1)
            let num2 = (Array.filter (fun elem -> elem  = 2) this.stream).Length
            let probablity2 = float num2 / float !k
            this.countProbability.[1] <- (num2, probablity2)
            let num3 = (Array.filter (fun elem -> elem  = 3) this.stream).Length
            let probablity3 = float num3 / float !k
            this.countProbability.[2] <- (num3, probablity3)
            let num4 = (Array.filter (fun elem -> elem  = 4) this.stream).Length
            let probablity4 = float num4 / float !k
            this.countProbability.[3] <- (num4, probablity4)
        
            Array.iter2 (fun x y -> this.incrementCount(x, y)) this.stream this.recivers


    member this.Printing() =
        printfn "count messeges: %i" this.k
        printfn "Types messeges:" 
        let mutable i = 0
        while i < !k do
            printfn $"{i + 1} - type: {this.stream.[i]}; reciver: {this.recivers.[i]}"
            i <- i + 1
        //Array.iter2 (printfn "%A %A") stream, recivers
        
        printfn "types probability:"
        printfn "%A" this.countProbability
        printfn "recivers probability:"
        printfn "%A" this.countReciverProbability
        printfn "Время:"
        (*i <- 0
        while i < this.k do
            printfn $"{i + 1} сообщение: {this.messagesTime.[i]} сек"
            i <- i + 1*)
