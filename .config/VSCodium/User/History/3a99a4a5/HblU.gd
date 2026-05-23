extends Node

@export var violence_dialogue: Node2D
@export var talk_dialogue: Node2D 
var hits_points: int = 5
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	talk_dialogue.set_active(false)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func violence() -> void:
	talk_dialogue.set_active(false)
	violence_dialogue.get_script().set_active(true)
	pass

func talk() -> void:
	talk_dialogue.set_active(true)

	violence_dialogue.set_active(false)
	print_debug("de chill")
	pass


func _on_talk_button_down() -> void:
	print_debug("de chill")
	talk()
	pass # Replace with function body.


func _on_violence_button_down() -> void:
	print_debug("violence")
	violence()
	pass # Replace with function body.
