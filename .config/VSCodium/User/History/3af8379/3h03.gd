extends Node

@export var bite_anim : Animation
@export var buoy_idle : Animation
signal idle_finish;
@export var bait_array : Array[Animation]

@export var numbaits : int = 3
@export var nummaxbaits : int = 5

@export var playerturn : bool
var rng = RandomNumberGenerator.new()


func initround() -> void:
	numbaits = rng.randi_range(0,nummaxbaits)

func _ready() -> void:
	idle_finish.connect();
	pass

func _process(delta: float) -> void:
	pass


func _on_animation_player_animation_finished(anim_name: StringName) -> void:
	if buoy_idle.resource_name == anim_name:
		idle_finish.emit()
	pass # Replace with function body.


func bitetime() -> void :
	numbaits--
	if numbaits == 0
