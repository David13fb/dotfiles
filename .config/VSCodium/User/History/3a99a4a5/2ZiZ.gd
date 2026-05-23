extends Node

@export var fight_dialogue: Node2D
@export var talk_dialogue: Node2D 
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	talk_dialogue.process_mode = Node.PROCESS_MODE_DISABLED
	talk_dialogue.visible = false
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
