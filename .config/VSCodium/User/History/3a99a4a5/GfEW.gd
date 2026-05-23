extends Node

@export var violence_dialogue: Node2D
@export var talk_dialogue: Node2D 
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

	violence_dialogue.process_mode = Node.PROCESS_MODE_INHERIT
	violence_dialogue.visible = true
	pass

func talk() -> void:
	talk_dialogue.process_mode = Node.PROCESS_MODE_INHERIT
	talk_dialogue.visible = true

	violence_dialogue.process_mode = Node.PROCESS_MODE_DISABLED
	violence_dialogue.visible = false
	pass