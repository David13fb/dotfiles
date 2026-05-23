extends Node

@export var canvas : CanvasLayer
@export var dialogue_name = "NONE"
@export var dialogue_manager : Control
@export var text_right : Control
signal end_talk
signal fail_talk
signal show_btn
signal can_interact
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	dialogue_manager.show_dialogue(dialogue_name);
	set_active(false);
	
	pass # Replace with function body.

func set_active(b : bool) -> void:
	dialogue_manager.set_process(b)
	canvas.set_process(b)
	canvas.visible = b
	if b:
		dialogue_manager.show_ops_btn()
	

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass



func _on_dialogue_dialog_end() -> void:
	end_talk.emit()
	pass # Replace with function body.

func _on_dialogue_options_display() -> void:
	
	pass # Replace with function body.


func _on_dialogue_fail() -> void:
	fail_talk.emit()
	pass # Replace with function body.


func _on_dialogue_show_dialogue_battle() -> void:
	show_btn.emit()
	pass # Replace with function body.


func _on_dialogue_can_interact() -> void:
	can_interact.emit()
	pass # Replace with function body.
