extends Node

@export var image : TextureRect
@export var animator : AnimatorPlayer
@export var btn_tex_array: Array[Texture]

func init_btn(image_index : int)-> void:
	image.texture = btn_tex_array[image_index]
