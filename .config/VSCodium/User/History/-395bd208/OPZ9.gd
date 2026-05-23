extends Node

@export var canvas : CanvasLayer
@export var dialogue_name = ""
@export var dialogue_manager : Node2D
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.

func set_active(b : bool) -> void:
	
	canvas.set_process(b)
	canvas.visible = b
	dialogue_manager.show_dialogue(dialogue_name);

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
