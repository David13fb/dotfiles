extends Node

@export var player1_rod : Node 
@export var player1_buoy : Node
@export var player2_rod : Node 
@export var player2_buoy : Node 
@export var numbaits : int = 3
@export var numminbaits : int = 3
@export var nummaxbaits : int = 5
@export var btn_array : Array[Node]

var player1canplay : bool = false
var player2canplay
signal player1input
signal player2input
var random_key : int
var rng = RandomNumberGenerator.new()
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass

func initround():
	numbaits = rng.randi_range(numminbaits,nummaxbaits)
	print(numbaits)
	player2_buoy.initround(numbaits)
	player1_buoy.initround(numbaits)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass



func _on_canya_v_2_boya_truebite() -> void:
	print("PICO")
	
func addInput():

	if !stop: 
		if !P1loose:
			#PLAYER1
			if Input.is_action_just_pressed("P1Button1"):
				P1Inputs.push_back(1)
			elif Input.is_action_just_pressed("P1Button2"):
				P1Inputs.push_back(2)
			elif Input.is_action_just_pressed("P1Button3"):
				P1Inputs.push_back(3)
			elif Input.is_action_just_pressed("P1Button4"):
				P1Inputs.push_back(4)
	if !stop:
		#PLAYER 2
		if !P2loose:
			if Input.is_action_just_pressed("P2Button1"):
				P2Inputs.push_back(1)
			elif Input.is_action_just_pressed("P2Button2"):
				P2Inputs.push_back(2)
			elif Input.is_action_just_pressed("P2Button3"):
				P2Inputs.push_back(3)
			elif Input.is_action_just_pressed("P2Button4"):
				P2Inputs.push_back(4)
			
