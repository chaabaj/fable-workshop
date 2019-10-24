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
}

type Msg =
  | BookDevice of Device * User
  | ReturnDevice of Device * User

let init() : Model = {
  devices = devices;
  users = users
}

// UPDATE

let update (msg:Msg) (model:Model) =
    match msg with
      | BookDevice (device, user) -> model
      | ReturnDevice (device, user) -> model


// VIEW (rendered with React)


let view (model:Model) dispatch =
  h3 [] [str "Hello world"]
  // div []
  //     [ button [ OnClick (fun _ -> dispatch Increment) ] [ str "+" ]
  //       div [] [ str (string model) ]
  //       button [ OnClick (fun _ -> dispatch Decrement) ] [ str "-" ] ]

// App
Program.mkSimple init update view
  |> Program.withReactSynchronous "elmish-app"
  |> Program.withConsoleTrace
  |> Program.run