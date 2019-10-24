namespace Models

module Device =
  open User
  open Utils.Commons

  type DeviceId = DeviceId of int32

  type Device = {
    Id: DeviceId
    Name: string
    Manufacturer: string
    BorrowedBy: option<UserId>
  }

  let makeDevice id name manufacturer = {
      Id = id;
      Name = name;
      Manufacturer = manufacturer;
      BorrowedBy = None
  }

  let devices: list<Device> =
    [
        (makeDevice (DeviceId 1) "Dell XPS 1923" "DELL");
        (makeDevice (DeviceId 2) "Dell XPS 1930" "DELL");
        (makeDevice (DeviceId 3) "Dell XPS 2000" "DELL");
        (makeDevice (DeviceId 4) "Sony XHD 120Hz" "Sony")
    ]
    
  let private borrow (device: Device) (user: User) =
    device.BorrowedBy
      |> Option.map (fun _ -> Error "Already booked")
      |> Option.orElse (Some (Ok {device with BorrowedBy=(Some user.Id)}))
      |> Option.get

  let private updateDevices =
    replace (fun device device2 -> device.Id = device2.Id)
  
  let private checkSameBorrower (user: User) (borrower: UserId) =
    if user.Id = borrower then Ok borrower else Error "User is not the same as the borrower" 

  let private returnBack device user =
    device.BorrowedBy
      |> orError "Nobody has borrowed this book"
      |> Result.bind (fun userId -> checkSameBorrower user userId)
      |> Result.map (fun _ -> {device with BorrowedBy=None})

  let private findDevice deviceToFind devices =
    devices
      |> List.tryFind (fun device -> device.Id = deviceToFind.Id)
      |> orNotFound

  let returnBackToDevices (deviceToReturn: Device) (user: User) =
    findDevice deviceToReturn
      >> Result.bind (fun device -> returnBack device user)
      >> Result.map (fun device -> updateDevices device devices)

  let borrowFromDevices (deviceToBorrow: Device) (user: User) =
    findDevice deviceToBorrow
      >> Result.bind (fun device -> borrow device user)
      >> Result.map (fun device -> updateDevices device devices)
      
  let available (device: Device): bool = device.BorrowedBy.IsNone
  
  let allAvailableDevices: list<Device> -> list<Device> = List.filter available
    
  let allBorrowedDevices: list<Device> -> list<Device> = List.filter (available >> not)

  