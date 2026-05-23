extends Node


@export var P1ImagesCombination : HFlowContainer
@export var P2ImagesCombination : HFlowContainer

@export var p1_audio : AudioStreamPlayer
@export var p2_audio : AudioStreamPlayer
@export var sound_fail : AudioStream
@export var sound_success : AudioStream

@onready var btn = preload("res://prefabs/btnInput.tscn")
#Array de tamaños
var tams = [7,7,8,8]
#Array de imagenes

#Array de imagenes de la combinacion P1 y P2
#Combinaciones de P1 y P2
var P1Combination = []
var P2Combination = []

#Serie de botones pulsados de P1 y P2
var P1Inputs = []
var P2Inputs = []

#Posición actual del PInputs de cada jugador
var P1Index = 0
var P2Index = 0

#Indica si P1 y P2 pueden jugar
var P1canPlay = false
var P2canPlay = false

#Indica si los jugadores han perdido la ronda
var P1loose = false
var P2loose = false

#Indica si la tecla pulsada no corresponde con la que te toca 
var P1miss = false
var P2miss = false

#Para la ronda
var stop = false
var st = false

var firstFrameP1 = true
var firstFrameP2 = true
func startP1():
	st = true
	P1canPlay = true
	setRandP1()
func startP2():
	st = true
	P2canPlay = true
	setRandP2()
#Comprueba el input de los dos jugadores y lo agrega al array de combinaciones de cada uno
func addInputP1():

	if !stop and P1canPlay and !firstFrameP1: 
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
			elif Input.is_action_just_pressed("P1Button5"):
				P1Inputs.push_back(5)
			elif Input.is_action_just_pressed("P1Button6"):
				P1Inputs.push_back(6)
			elif Input.is_action_just_pressed("P1Button7"):
				P1Inputs.push_back(7)
			elif Input.is_action_just_pressed("P1Button8"):
				P1Inputs.push_back(8)
			elif Input.is_action_just_pressed("P1Button9"):
				P1Inputs.push_back(9)
			elif Input.is_action_just_pressed("P1Button10"):
				P1Inputs.push_back(10)
			elif Input.is_action_just_pressed("P1Button11"):
				P1Inputs.push_back(11)
			elif Input.is_action_just_pressed("P1Button12"):
				P1Inputs.push_back(12)

func addInputP2():
	if !stop and P2canPlay and !firstFrameP2:
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
			elif Input.is_action_just_pressed("P2Button5"):
				P2Inputs.push_back(5)
			elif Input.is_action_just_pressed("P2Button6"):
				P2Inputs.push_back(6)
			elif Input.is_action_just_pressed("P2Button7"):
				P2Inputs.push_back(7)
			elif Input.is_action_just_pressed("P2Button8"):
				P2Inputs.push_back(8)
			elif Input.is_action_just_pressed("P2Button9"):
				P2Inputs.push_back(9)
			elif Input.is_action_just_pressed("P2Button10"):
				P2Inputs.push_back(10)
			elif Input.is_action_just_pressed("P2Button11"):
				P2Inputs.push_back(11)
			elif Input.is_action_just_pressed("P2Button12"):
				P2Inputs.push_back(12)

#Comprueba que la última tecla pulsada es correcta o no
func checkInputP1(): 

	if !stop and P1canPlay:
		if !P1loose:
			if P1Index < P1Inputs.size() :
				if P1Inputs[P1Index] == P1Combination[P1Index] :
					P1ImagesCombination.get_child(P1Index).visible = false
					#TODO Sonidito de acierto
					p1_audio.steam = sound_success
					
					P1Index+=1
				else :
					#TODO Sonidito de fallo
					P1miss = true

	

func checkInputP2():
	if !stop and P2canPlay:
		if !P2loose:
			if P2Index < P2Inputs.size() :
				if P2Inputs[P2Index] == P2Combination[P2Index] :
					P2ImagesCombination.get_child(P2Index).visible = false
					#TODO Sonidito de acierto
					P2Index+=1
				else :
					#TODO Sonidito de fallo
					P2miss = true
#Genera la combinación aleatoria para cada jugador
func setRandP1():

	var tamP1 = tams[Game_Manager.fishP1]
	while tamP1 > 0:
		#Player1
		var rndNum1 = randi_range(1,12)
		P1Combination.push_back(rndNum1)
		var new_btn = btn.instantiate()
		new_btn.init_btn(rndNum1 - 1)
		P1ImagesCombination.add_child(new_btn)
		tamP1-=1


func setRandP2():
	var tamP2 = tams[Game_Manager.fishP2]
	while tamP2 > 0:
		#Player2
		var rndNum2 = randi_range(1,12)
		P2Combination.push_back(rndNum2)
		var new_btn2 = btn.instantiate()
		new_btn2.init_btn(rndNum2 - 1)
		P2ImagesCombination.add_child(new_btn2)
		tamP2-=1
	
#Resetea los valores de la clase a su estado original
func reset():
	st = false
	for node in P1ImagesCombination.get_children():
		node.queue_free()
	for node in P2ImagesCombination.get_children():
		node.queue_free()

	P1Combination.clear()
	P2Combination.clear()
	P1Inputs.clear()
	P2Inputs.clear()
	P1Index = 0
	P2Index = 0
	P1loose = false
	P2loose = false
	P1miss = false
	P2miss = false
	P1canPlay = false
	P2canPlay = false
	stop = false
	firstFrameP1 = true
	firstFrameP2 = true
	
func desactiveP1Images():
	for node in P1ImagesCombination.get_children():
		node.visible = false
func desactiveP2Images():
	for node in P2ImagesCombination.get_children():
		node.visible = false
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if st:
		if P1loose and P2loose:
			stop = true
			Game_Manager.setTime(0)

		if !stop:
			if !P1loose and P1canPlay:
				addInputP1()
				checkInputP1()
				firstFrameP1 = false
				if !P1miss:
					if P1Inputs.size() >= P1Combination.size():
						Game_Manager.addPointsP1()
						stop = true
					
				else :
					P1loose = true
					desactiveP1Images()

		if !stop:
			if !P2loose and P2canPlay:
				addInputP2()	
				checkInputP2()
				firstFrameP2 = false
				if !P2miss:
					if P2Inputs.size() >= P2Combination.size():
						Game_Manager.addPointsP2()
						stop = true
				else :	
					P2loose = true
					desactiveP2Images()
				
