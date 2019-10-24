namespace Utils

module Commons =
  let notImplemented () =
    raise (System.Exception  "Not implemented")

  let replace (eq: 'a -> 'a -> bool) (list: list<'a>) (a: 'a): list<'a> =
    list |> List.map (fun elem ->
      if eq elem a then a else elem
    )
  
  let orError error =
    Option.map Ok >> Option.orElse (Some (Error error)) >> Option.get

  let orNotFound (optVal: option<'a>) =
    orError "Device not found" optVal
