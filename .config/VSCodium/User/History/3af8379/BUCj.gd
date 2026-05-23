extends Node

@export var bite_anim : Animation
@export var buoy_idle : Animation
@export var playerturn : bool
@export var animator : AnimationPlayer
signal idle_finish;
signal  truebite;

@export var bait_array : Array[Animation]

func initround(var numbaits : int) -> void:
	

func _ready() -> void:
	idle_finish.connect(bitetime);
	pass

func _process(delta: float) -> void:
	pass


func _on_animation_player_animation_finished(anim_name: StringName) -> void:
	if buoy_idle.resource_name == anim_name:
		idle_finish.emit()
	else:
		animator.play(buoy_idle)


func bitetime() -> void :
	numbaits-= 1
	if numbaits == 0:
		truebite.emit()
	else 
		anima

