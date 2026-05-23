extends Node2D
@export var texture : Sprite2D
@export var texture_array : Array[Texture]
@export var act_texture : int = 0

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.

func next_texture() -> void:
	act_texture+=1
	texture.texture = texture_array[act_texture]
	
	pass
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
