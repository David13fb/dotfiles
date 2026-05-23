extends Node2D
@var texture : Texture
@var texture_array : Array[Texture]
@var act_texture : int = 0

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.

func next_texture() -> void:
	texture.Texture = texture_array[act_texture]
	act_texture+=1
	pass
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
