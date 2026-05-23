extends Node

@export var image : TextureRect
@export var btn_tex_arrat: Array[Texture]

func init_btn(image_index : int)-> void:
	image.texture = btn_tex_array