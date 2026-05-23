extends Node

@export var player1_rod : Node 
@export var player1_buoy : Node
@export var player2_rod : Node 
@export var player2_buoy : Node 
@export var numbaits : int = 3
@export var numminbaits : int = 3
@export var nummaxbaits : int = 5

var rng = RandomNumberGenerator.new()
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	numbaits = rng.randi_range(numminbaits,nummaxbaits)
	print(numbaits)
	player2_buoy.initround(numbaits)
	player1_buoy.initround(numbaits)
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass



func _on_canya_v_2_boya_truebite() -> void:
	pass # Replace with function body.
