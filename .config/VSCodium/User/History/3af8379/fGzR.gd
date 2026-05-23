extends Node

@export var bite_anim : Animation
@export var bait_array : Array[Animation]
@export var numbaits : int = 3
@export var nummaxbaits : int = 5

@export var playerturn : bool
var rng = RandomNumberGenerator.new()
func initround() -> void:
	numbaits = rng.randi_range(0,nummaxbaits)
	pass

func _on_animation_finished(anim_name):
    if anim_name == "StartJump":
        # haz otras cosa

func _process(delta: float) -> void:
	pass
