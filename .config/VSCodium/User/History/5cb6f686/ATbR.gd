extends Node

@export var image : TextureRect
@export var animator : AnimationPlayer
@export var idle_animation : Animation
@export var selected_animation : Animation
@export var btn_tex_array: Array[Texture]

var rng = RandomNumberGenerator.new()
func  _ready() -> void:
func init_btn(image_index : int)-> void:
	image.texture = btn_tex_array[image_index]
	animator.play_section(idle_animation.resource_name,rng.randf_range(0,idle_animation.length-0.1))
	animator.loop_mode = true
