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
    
  let private borrow (device: Device) (user: User): Result<string, Device> =
    notImplemented ()

  let private updateDevices (devices: Device list) (deviceToReplace: Device) =
    replace (fun device device2 -> device.Id = device2.Id) devices deviceToReplace
  
  let private checkSameBorrower (user: User) (borrower: UserId): Result<string, UserId> =
    notImplemented ()

  let private returnBack (device: Device) (user: User): Result<string, Device> =
    notImplemented ()

  let private findDevice (deviceToFind: Device) (devices: Device list): Result<string, Device> =
    notImplemented ()

  let returnBackToDevices (device: Device) (user: User) (devices: Device list): Result<string, list<Device>> =
    notImplemented ()

  let borrowFromDevices (deviceToBorrow: Device) (user: User) (devices: list<Device>): Result<string, list<Device>> =
    notImplemented ()
      
  let available (device: Device) = device.BorrowedBy.IsNone
  
  let allAvailableDevices (devices: list<Device>): list<Device> =
    notImplemented ()
    
  let allBorrowedDevices (devices: list<Device>): list<Device> =
    notImplemented ()

  