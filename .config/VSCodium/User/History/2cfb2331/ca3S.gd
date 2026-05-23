extends  CharacterBody2D

@export var vel = 10.0; 
func _ready():
	pass

func  _process(delta: float):
	var inputResult =  Vector2(0,0);
	if Input.is_action_pressed("moveRight"):
		inputResult += Vector2(vel,0);

	if Input.is_action_pressed("moveLeft"):
		inputResult += Vector2(-vel,0);
	
	if Input.is_action_pressed("moveDown"):
		inputResult += Vector2(0,vel);
		
	if Input.is_action_pressed("moveUp"):
		inputResult += Vector2(0,-vel);
	
	velocity = inputResult;
	
	   

	move_and_slide();
	pass
