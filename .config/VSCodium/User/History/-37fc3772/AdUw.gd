extends Node2D

class_name Manolo
# Called when the node enters the scene tree for the first time.
var dir : Vector2
@export var speed : float = 10
func _ready() -> void:
	modulate = Color.GRAY;
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	global_position += Vector2(dir * speed);
	pass

func _input(event: InputEvent) -> void:
	if event.is_action_pressed("move"):
		dir = Vector2(1,0)
	else: 
		dir = Vector2(0,0)
	pass