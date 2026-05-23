extends Node

@export var violence_dialogue: Node2D
@export var talk_dialogue: Node2D 
var hits_points: int = 5
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	talk_dialogue.process_mode = Node.PROCESS_MODE_DISABLED
	talk_dialogue.visible = false
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func violence() -> void:
	talk_dialogue.process_mode = Node.PROCESS_MODE_DISABLED
	talk_dialogue.visible = false
	print("VIOLENSIA")

	violence_dialogue.process_mode = Node.PROCESS_MODE_INHERIT
	violence_dialogue.visible = true
	pass

func talk() -> void:
	talk_dialogue.process_mode = Node.PROCESS_MODE_INHERIT
	talk_dialogue.visible = true

	violence_dialogue.process_mode = Node.PROCESS_MODE_DISABLED
	violence_dialogue.visible = false
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
