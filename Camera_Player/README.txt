include Player_Move
input manager
managers

Art UnityChan

Controller PlayerController => utilize OnCollisionEnter func
=> divide keyinputs callback func OnKeyBoardMove to OnKeyBoardMove, OnKeyBoardIdle

Controller CameraController => Add MapInfo struct

Define add cameramode



change gravity edit>project settings>physics>gravity -9.81 to -15

bug : Idle state is not appear

debug : KeyUp(wasd) to OnKeyBoardIdle, not OnKeyBoardMove
KeyAction+=OnKeyBoardIdle

KeyAction is more sensitive than MoveKeyAction.
Maybe need CameraManager to get map info at outside.
HumanView needs mapinfo

Define script
24/10/14 1840
improve PlayerState, implement QuaterView
