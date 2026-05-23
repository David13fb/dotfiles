extends Node2D

class_name Manolo
# Called when the node enters the scene tree for the first time.
@export var speed : float = 1
func _ready() -> void:
	modulate = Color(0);
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	global_position += Vector2(speed,0);
	pass

func _input(event: InputEvent) -> void:
	if event.is_action_pressed("move"):
		_process();
	pass