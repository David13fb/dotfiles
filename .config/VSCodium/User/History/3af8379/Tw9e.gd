extends Node

@export var bite_anim : Animation
@export var bait_array : Array[Animation]
@export var numbaits : int = 3
@export var playerturn : bool
var rng = RandomNumberGenerator.new()
func initround() -> void:
	var rnd : RandomNumberGenerator = new RandomNumberGenerator()
	pass



func _process(delta: float) -> void:
	pass
