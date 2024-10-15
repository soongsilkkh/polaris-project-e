include Player_Move
input manager
managers

Art UnityChan => Change box collider to thin capsule collider
=> Adjust no-friction physics to capsule collide to get smooth during jump collision

Scene StartTriggerCube GameObject trigger => for entering new map and store mapinfo
Scene EndTriggerCube GameObject trigger => for store next mapinfo and entering prev map
Scene additional maps

Controller PlayerController => Add PlayerMovements Enum, PlayerMovements variables, Camera variable, CameraController variable for HumanViewCamera Move and later
=> Add isEntered variable, prevX variable for store proper MapInfo
=> Add OnTriggerEnter func for record player's prevX position
=> Add OnTriggerExit func for check player entering new map and store proper MapInfo, use CameraController's StoreMapInfo func
=> MapInfo Input is hard codeing rn, needs to define map infos properly later

Controller CameraController => Add more MapInfo class member
=> Implement VerticalHumanView, use stored MapInfo with no Z rotation
=> Add VerticalHumanViewFuncs


Define Script

change gravity edit>project settings>physics>gravity -9.81 to -15


bug : not appear



24/10/15 2000
improve PlayerMove, Implement HumanView, Add new trigger and mapinfo rules
