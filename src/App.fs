module App

(**
 The famous Increment/Decrement ported from Elm.
 You can find more info about Elmish architecture and samples at https://elmish.github.io/
*)

open Elmish
open Elmish.React
open Fable.React
open Fable.React.Props
open Models.Device
open Models.User

// MODEL

type Model = {
  devices: Device list
  users: User list
  filter: (Device -> bool)
}

type Msg =
  | BookDevice of Device * User
  | ReturnDevice of Device * User
  | Filter of (Device -> bool)

let init() : Model = {
  devices = devices;
  users = users
  filter = (fun _ -> true) // all
}

// UPDATE

let update (msg:Msg) (model:Model) =
    match msg with
      | BookDevice (device, user) ->
        match borrowFromDevices device user model.devices with
          | Ok devices -> {model with devices=devices}
          | Error err ->
            printf "%s" err
            model
      | ReturnDevice (device, user) ->
        match returnBackToDevices device user model.devices with
          | Ok devices -> {model with devices=devices}
          | Error err ->
            printf "%s" err
            model
      | Filter filter -> {model with filter=filter}



// VIEW (rendered with React)

let private deviceView dispatch (device: Device) (user: User) =
  p [] [
    str device.Name
    button [
      Disabled (not (available device))
      OnClick (fun _ -> dispatch (BookDevice (device, user)))
    ] [str "Book"]
    button [
      Disabled (available device)
      OnClick (fun _ -> dispatch (ReturnDevice (device, user)))
    ] [str "Return"]
  ]


let private deviceListView dispatch model =
  model.devices 
    |> List.filter model.filter
    |> List.map (fun device -> li [Key device.Name] [deviceView dispatch device model.users.Head])

let view (model: Model) dispatch =
  div [Id "Test"] [
    ul [] (deviceListView dispatch model)
    button [
      OnClick (fun _ -> dispatch (Filter available))
    ] [str "All available devices"]
    button [
      OnClick (fun _ -> dispatch (Filter (available >> not)))
    ] [str "All borrowed devices"]
    button [
      OnClick (fun _ -> dispatch (Filter (fun _ -> true)))
    ] [str "All"]
  ]
  // div []
  //     [ button [ OnClick (fun _ -> dispatch Increment) ] [ str "+" ]
  //       div [] [ str (string model) ]
  //       button [ OnClick (fun _ -> dispatch Decrement) ] [ str "-" ] ]

// App
Program.mkSimple init update view
  |> Program.withReactSynchronous "elmish-app"
  |> Program.withConsoleTrace
  |> Program.run