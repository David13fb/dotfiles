extends Node

@export var player1_rod : Node 
@export var player1_buoy : buoy 
@export var player2_rod : Node 
@export var player2_buoy : Node 
@export var numbaits : int = 3
@export var numminbaits : int = 3
@export var nummaxbaits : int = 5

var rng = RandomNumberGenerator.new()
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	numbaits = rng.randi_range(numminbaits,nummaxbaits)

	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
