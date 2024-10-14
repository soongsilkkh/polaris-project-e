include 
input manager => add MoveKeyAction variable, add OnMoveUpdate func
managers => utilize FixedUpdate function

Art UnityChan => change capsule collider to box collider, transform

Controller PlayerController => utilize OnCollisionEnter func
=> add PlayerAt, Player PlayerMode enum, isPlayerOn Raycast func,
=> divide keyinputs callback func OnKeyBoard to OnKeyBoardMove, Jump
=> analyze ground check and edit _at at collision, not always update

change gravity edit>project settings>physics>gravity -9.81 to -15

bug : double jump, KeyDown(space) can't catch bug

debug : check actual go's position at KeyUp(space) not KeyDown(space)

Define script
24/10/14 1600 done
improve jump
