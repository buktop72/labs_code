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
let recivers : numberReciver array = Array.zeroCreate !k
// printfn "%A" recivers

(*вероятности и число поступлений сообщений данного типа к каждому приёмнику
((кол-во 1 типа в адр1, вероятность), (кол-во 1 типа в адр2, вероятность))
...
((кол-во 4 типа в адр1, вероятность), (кол-во 4 типа в адр2, вероятность))
*)
let countReciverProbability : ((int * float) * (int * float)) array = Array.zeroCreate 4
for i = 0 to 3 do
    countReciverProbability.[i] <- ((0, 0.0), (0, 0.0))
//printfn "%A" countReciverProbability  // [|((0, 0.0), (0, 0.0)); ((0, 0.0), (0, 0.0)); ((0, 0.0), (0, 0.0)); ((0, 0.0), (0, 0.0))|]

(*Создать функцию Modeling, в которой выполнить моделирование потока сообщений
1.тип сообщения(1,2,3,4)
2.адрес приёмника(1,2)
распределение по типам:
1 - temp <= 0.55
2 - temp > 0.55 && temp <= 0.63
3 - temp > 0.63 && temp <= 0.75
4 - temp > 0.75
распределение по адресам:
адр1:
1 - 0.88
2 - 0.06
3 - 0.3
4 - 0.54
адр2:
1 - 0.12
2 - 0.94
3 - 0.7
4 - 0.46*)
let Modeling(task: bool) = 
    let rand = new Random()
    if task then
        for i = 0 to !k-1 do
            let mutable probability = rand.NextDouble()
            // printfn "%f" probability
            if probability <= 0.55 then
                stream.[i] <- 1
            elif probability <= 0.63 then
                stream.[i] <- 2
            elif probability <= 0.75 then
                stream.[i] <- 3
            else
                stream.[i] <- 4
        //printfn "Types messeges:"    
        //printfn "%A" stream

    else
        let mutable i = 0
        while i < !k do
            let mutable probablity = rand.NextDouble()
            if stream.[i] = 1 then
                if probablity <= 0.88 then
                   recivers.[i] <- One (1, "one")
                else
                    recivers.[i] <- Two (2, "two")
            if stream.[i] = 2 then
                if probablity <= 0.06 then
                   recivers.[i] <- One (1, "one")
                else
                    recivers.[i] <- Two (2, "two")           
            if stream.[i] = 3 then
                if probablity <= 0.3 then
                   recivers.[i] <- One (1, "one")
                else
                    recivers.[i] <- Two (2, "two")
            if stream.[i] = 4 then
                if probablity <= 0.54 then
                   recivers.[i] <- One (1, "one")
                else
                    recivers.[i] <- Two (2, "two")
            i <- i + 1
        //printfn "%A" recivers

(*Создать функцию Calculating, в которой для сообщения каждого типа определить 
    а) вероятность и количество появления сообщений, 
    б) вероятности и число поступлений сообщений данного типа к каждому приёмнику. Использовать при этом операцию сопоставления с образцом*)
(*количество сообщений и вероятность для каждого типа*)   
let countProbability : (int * float) array = [|(0, 0.0); (0, 0.0); (0, 0.0); (0, 0.0)|] 

(*считаем вероятности и число поступлений сообщений данного типа к каждому приёмнику*)
let incrementCount(i, x, y) =
    match y with
    | One (1,"one") -> countReciverProbability.[x - 1] <- ((fst (fst countReciverProbability.[x - 1]) + 1, float (fst (fst countReciverProbability.[x - 1]) + 1) / float (fst countProbability.[x - 1])), (fst (snd countReciverProbability.[x - 1]),    snd (snd countReciverProbability.[x - 1]))) 
    | Two (2,"two") -> countReciverProbability.[x - 1] <- ((fst (fst countReciverProbability.[x - 1]), snd (fst countReciverProbability.[x - 1])    ), (fst (snd countReciverProbability.[x - 1]) + 1, float (fst (snd countReciverProbability.[x - 1]) + 1) / float (fst countProbability.[x - 1])))

(*Создать функцию Calculating*)
let Calculating(task: bool) =
    if task then
        let num1 = (Array.filter (fun elem -> elem  = 1) stream).Length
        let probablity1 = float num1 / float !k
        countProbability.[0] <- (num1, probablity1)
        let num2 = (Array.filter (fun elem -> elem  = 2) stream).Length
        let probablity2 = float num2 / float !k
        countProbability.[1] <- (num2, probablity2)
        let num3 = (Array.filter (fun elem -> elem  = 3) stream).Length
        let probablity3 = float num3 / float !k
        countProbability.[2] <- (num3, probablity3)
        let num4 = (Array.filter (fun elem -> elem  = 4) stream).Length
        let probablity4 = float num4 / float !k
        countProbability.[3] <- (num4, probablity4)
        
        Array.iteri2 (fun i x y -> incrementCount(i, x, y)) stream recivers

(*Создать функцию Printing для вывода потока сообщений и всех полученных характеристик на консоль*)
let Printing() =
    printfn "count messeges: %i" !k
    printfn "Types messeges:" 
    let mutable i = 0
    while i < !k do
        //printfn "%i - %A - %A" i stream.[i] recivers.[i]
        printfn $"{i + 1} - type: {stream.[i]}; reciver: {recivers.[i]}"
        i <- i + 1
    //Array.iter2 (printfn "%A %A") stream, recivers
    printfn "types probability:"
    printfn "%A" countProbability
    printfn "recivers probability:"
    printfn "%A" countReciverProbability

[<EntryPoint>]
let main argv =
    let basic = new Module1.Basic(1);
    //basic.NormalTimeGenerator();
    //basic.ExpoTimeGenerator();
    //basic.BoxMullerGenerator();
    basic.BoxMullerGenerator2();
    basic.Modeling(true);
    basic.Modeling(false);
    basic.Calculating(true);
    basic.Printing();

    0 // return an integer exit code
