extends Node2D

class_name Manolo
# Called when the node enters the scene tree for the first time.
var dir : int
signal on_stop
signal on_move
@export var speed : float = 10
func _ready() -> void:
	modulate = Color.GRAY;
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	global_position.x += (dir * speed);
	pass

func _input(event: InputEvent) -> void:
	if event.is_action_pressed("move"):
		dir = 1
		print("aaa");
		on_move.emit()
	else: 
		on_stop.emit()
		dir = 0
	pass