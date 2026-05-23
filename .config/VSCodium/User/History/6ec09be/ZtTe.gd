extends Node

@export var player1_buoy : Node
@export var player2_buoy : Node 
@export var btn_input : Node 
@export var numbaits : int = 3
@export var numminbaits : int = 3
@export var nummaxbaits : int = 5
@export var btn_array : Array[Node]

var player1canplay : bool = false
var player2canplay :bool = false
signal player1input
signal player2input
var random_key : int
var rng = RandomNumberGenerator.new()
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	initround()
	pass

func initround():
	numbaits = rng.randi_range(numminbaits,nummaxbaits)
	print(numbaits)
	player2_buoy.initround(numbaits)
	player1_buoy.initround(numbaits)

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if player1canplay and player2canplay:
		addinput()



func _on_canya_v_2_boya_truebite() -> void:
	print("PICO")
	btn_input.visible = true
	btn_input.init_btn(random_key)
	player1canplay = true
	player1canplay = true

func addinput()-> void:

	if player1canplay:
			#PLAYER1
		if Input.is_action_just_pressed("P1Button1"):
			player1canplay = false
			if random_key == 1:
				player1input.emit(true)
		elif Input.is_action_just_pressed("P1Button2"):
			player1canplay = false
			if random_key == 2:
				player1input.emit(true)
		elif Input.is_action_just_pressed("P1Button3"):
			player1canplay = false
			if random_key == 3:
				player1input.emit(true)
		elif Input.is_action_just_pressed("P1Button4"):
			player1canplay = false
			if random_key == 4:
				player1input.emit(true)
		if player1canplay== false:
				player1input.emit(false)
	if player2canplay:
		#PLAYER 2
		if Input.is_action_just_pressed("P2Button1"):
			player2canplay = false
			if random_key == 1:
				player2input.emit(true)
		elif Input.is_action_just_pressed("P2Button2"):
			player2canplay = false
			if random_key == 2:
				player2input.emit(true)
		elif Input.is_action_just_pressed("P2Button3"):
			player2canplay = false
			if random_key == 3:
				player2input.emit(true)
		elif Input.is_action_just_pressed("P2Button4"):
			player2canplay = false
			if random_key == 4:
				player2input.emit(true)
		if player2canplay== false:
				player2input.emit(false)
			
