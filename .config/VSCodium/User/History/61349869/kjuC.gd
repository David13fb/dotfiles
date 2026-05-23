extends Node


@export var canvas : CanvasLayer
@export var dialogue_name = "NONE"
@export var dialogue_manager : Control
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	dialogue_manager.show_dialogue(dialogue_name);
	pass # Replace with function body.

func set_active(b : bool) -> void:
	canvas.set_process(b)
	dialogue_manager.set_process(b)
	canvas.visible = b
	if b:
		dialogue_manager.show_ops_btn()
	pass

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
