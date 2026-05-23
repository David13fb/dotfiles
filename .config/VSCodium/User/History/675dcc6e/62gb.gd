extends Node2D

var player : Node2D

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	var hijos = get_children();
	for hijo in hijos:
		if hijo is CharacterBody2D:
			player = hijo

func _read_input():
	var clickPressed = Input.is_action_just_pressed("Interact")
	if clickPressed:
		player._setSpeed(get_viewport().get_mouse_position()) 


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	_read_input()
