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

func _process(delta: float) -> void:
	pass


func _on_animation_player_animation_finished(anim_name: StringName) -> void:
	if()
	pass # Replace with function body.
