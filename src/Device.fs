namespace Models

module Device =
  open User
  open Utils.Commons

  type DeviceId = DeviceId of int32

  type Device = {
    Id: DeviceId
    Name: string
    Manufacturer: string
    BorrowedBy: UserId option
  }

  let makeDevice id name manufacturer = {
      Id = id;
      Name = name;
      Manufacturer = manufacturer;
      BorrowedBy = None
  }

  let devices: Device list =
    [
        (makeDevice (DeviceId 1) "Dell XPS 1923" "DELL");
        (makeDevice (DeviceId 2) "Dell XPS 1930" "DELL");
        (makeDevice (DeviceId 3) "Dell XPS 2000" "DELL");
        (makeDevice (DeviceId 4) "Sony XHD 120Hz" "Sony")
    ]
    
  let private borrow (device: Device) (user: User): Result<Device, string> =
    match device.BorrowedBy with
      | Some _ -> Error "Already booked"
      | None -> Ok ({device with BorrowedBy=(Some user.Id)})

  let private updateDevices (devices: Device list) (deviceToReplace: Device) =
    replace (fun device device2 -> device.Id = device2.Id) devices deviceToReplace
  
  let private checkSameBorrower (user: User) (borrower: UserId): Result<UserId, string> =
    if user.Id = borrower then Ok borrower else Error "User is not the same as borrower" 

  let private returnBack (device: Device) (user: User): Result<Device, string> =
    match device.BorrowedBy with
      | Some userId -> (checkSameBorrower user userId) |> Result.map (fun _ -> {device with BorrowedBy=None})
      | None -> Error "Nobody has borrowed this book"

  let private findDevice (deviceToFind: Device) (devices: Device list) =
    devices 
      |> List.tryFind (fun device -> device.Id = deviceToFind.Id)
      |> orNotFound

  let returnBackToDevices (deviceToReturn: Device) (user: User) (devices: Device list): Result<list<Device>, string> =
    devices 
      |> findDevice deviceToReturn
      |> Result.bind (fun device -> returnBack device user)
      |> Result.map (fun device -> updateDevices devices device)

  let borrowFromDevices (deviceToBorrow: Device) (user: User) (devices: list<Device>): Result<list<Device>, string> =
    devices 
      |> findDevice deviceToBorrow
      |> Result.bind (fun device -> borrow device user)
      |> Result.map (fun device -> updateDevices devices device)

      
  let available (device: Device) = device.BorrowedBy.IsNone
  
  let allAvailableDevices = List.filter available
    
  let allBorrowedDevices = List.filter (fun device -> not(available device))

  