include 
input manager => add MoveKeyAction variable, add OnMoveUpdate func
managers => utilize FixedUpdate function

Art UnityChan => change capsule collider to box collider, transform

Controller PlayerController => utilize OnCollisionEnter func
=> add PlayerAt, Player PlayerMode enum, isPlayerOn Raycast func,
=> divide keyinputs callback func OnKeyBoard to OnKeyBoardMove, Jump
=> analyze ground check and edit _at at collision, not always update

change gravity edit>project settings>physics>gravity -9.81 to -15

bug : if rigidbody.AddForce func called '
but target go's transform not actually jump(stick on ground)
( cause of short roof, side friction, etc,,,)
_at isOnAir but no collision comes, no transform changed
leads to infinit OnAir with no collision func, isPlayerOn func
=>
debug : check actual go's transform jumped
utilize isPlayerOn func at jump KeyDown (OnKeyBoardJump func)

Define script
24/10/14 3pm done
improve jump, improve movement
