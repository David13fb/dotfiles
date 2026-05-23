extends Node


@export var canvas : CanvasLayer
@export var dialogue_name = "NONE"
@export var dialogue_manager : Control
@export var player_rect : Control
signal end_violence
signal fail_violence
signal show_btn
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


func _on_dialogue_dialog_end() -> void:
	pass # Replace with function body.
	end_violence.emit()


func _on_dialogue_options_display() -> void:
	pass # Replace with function body.


func _on_dialogue_fail() -> void:
	fail_violence.emit()
	pass # Replace with function body.


func _on_dialogue_show_dialogue_battle() -> void:
	show_btn.emit()
	pass # Replace with function body.
