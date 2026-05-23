extends Node

@export var bar_node : Control 

func change_scale(timeact : float) -> void:
	bar_node.scale.x = timeact/10.0

func _process(delta: float) -> void:
	Game_Manager.actualTime