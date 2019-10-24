namespace Utils

module Commons =
  let notImplemented () =
    raise (System.Exception  "Not implemented")

  let replace (eq: 'a -> 'a -> bool) (list: list<'a>) (a: 'a): list<'a> =
    notImplemented ()
  
  let orError (error: 'e) (optVal: option<'a>): Result<'e, 'a> =
    notImplemented ()

  let orNotFound (optVal: option<'a>) =
    orError "Device not found" optVal
