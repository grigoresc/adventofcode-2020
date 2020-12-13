//module aoc.day13

let input="""1005595
41,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,37,x,x,x,x,x,557,x,29,x,x,x,x,x,x,x,x,x,x,13,x,x,x,17,x,x,x,x,x,23,x,x,x,x,x,x,x,419,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,x,19"""
// let input="""939
// 7,13,x,x,59,x,31,19"""

let parse (text:string)=
    let v=text.Split '\n'
    v.[0]|>int64,v.[1].Split ','

let (no,els) = parse input

let elsi = Array.filter (fun o -> o<>"x") els |> Array.map int64



let (time,bus) = elsi |> Array.map (fun o -> if(no%o=0L)then (no,o) else (no-no%o+o,o)) |> Array.min 

let Solve1 = (time-no)*bus