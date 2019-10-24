namespace Models

module User =
  type UserId = UserId of int32
  type User = {
    Id: UserId
    Name: string
  }
  
  let makeUser id name = {
    Id = id
    Name = name
  }

  let users: User list = [
    (makeUser (UserId 1) "John")
    (makeUser (UserId 2) "Jean")
    (makeUser (UserId 3) "Pierre")
    (makeUser (UserId 4) "Brian")
  ]