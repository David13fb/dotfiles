extends Node


@export var canvas : CanvasLayer
@export var dialogue_name = "NONE"
@export var dialogue_manager : Control
@export var player_rect : Control
@export var btn_array : Array[Button]
signal end_violence
signal fail_violence
signal show_btn
signal can_interact

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	dialogue_manager.show_dialogue(dialogue_name);
	

	pass # Replace with function body.

func set_active(b : bool) -> void:
	canvas.set_process(b)
	dialogue_manager.set_process(b)
	canvas.visible = b
	dialogue_manager.visible = false
	if b:
		dialogue_manager.show_ops_btn()
	pass

func desactive_text_rect()-> void:
	player_rect.set_process(false)
	player_rect.visible = false
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


func _on_dialogue_can_interact() -> void:
	can_interact.emit()
	pass # Replace with function body.
