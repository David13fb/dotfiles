extends Node

@export var bite_anim : Animation
@export var bait_array : Array[Animation]
@export var numbaits : int = 3
@export var playerturn : bool
var rng = RandomNumberGenerator.new()
func initround() -> void:
	numbaits = rng.randi_range(0,bait_array.size())
	pass



func _process(delta: float) -> void:
	pass
