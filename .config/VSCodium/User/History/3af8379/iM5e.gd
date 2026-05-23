extends Node

@export var bite_anim : Animation
@export var buoy_idle : Animation
@export var playerturn : bool
@export var animator : AnimationPlayer

@export var bait_array : Array[Animation]

var numbaits : int
signal idle_finish;
signal  truebite;

func initround(_numbaits : int) -> void:
	numbaits = _numbaits

func _ready() -> void:
	animator = get_node()
	idle_finish.connect(bitetime);
	initround(4)
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
		animator.play(bait_array[randi_range(0,bait_array.size())])

