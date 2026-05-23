extends Node

@export var move_factor : float = 10.0

@export var act_move_factor : float

@export var friction : float = 1

@export var can_move : bool = false

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if can_move:
		print("se mueve")
	pass
