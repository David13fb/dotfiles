extends Node3D

@export var SceneMenu : String = ""
@export var InitMiniGame : Node3D
@export var QTEminiGame :  Node3D
@export var FishManager : Node3D
@export var AudioPlayerSlow : AudioStreamPlayer
@export var AudioPlayerFast : AudioStreamPlayer
@export var timer_node : Control

@onready var Interfaz = $InterfazGame
@onready var GanadorFinal = $InterfazGame/GanadorFinal

@onready var puntosP1 = $InterfazGame/ColorRect
@onready var puntosP2 = $InterfazGame/ColorRect2
var monedasP1 = []
var monedasP2 = []

@export var sound_fail : AudioStream
@export var sound_start : AudioStream
@export var sound_success : AudioStream

@export var audio_p1 : AudioStreamPlayer
@export var audio_p2 : AudioStreamPlayer

var P1pulse = false
var P2pulse = false

var P1miss = false
var P2miss = false

@export var textTime = 3.0
var acttextTime = textTime

var slowSong
var fastSong

var volume : float
@export var speed_reduce_audio : float = 0.01
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	resetRound()

func resetRound() :
	
	timer_node.visible = false
	AudioPlayerSlow.stream_paused = false
	AudioPlayerSlow.volume_db = 10
	AudioPlayerFast.stream_paused = true
	QTEminiGame.reset()
	Game_Manager.resetTime()
	GanadorFinal.visible = false
	QTEminiGame.set_process(false) 
	QTEminiGame.visible = false
	InitMiniGame.visible = true
	InitMiniGame.initround()
	P1pulse = false
	P2pulse = false
	P1miss = false
	P2miss = false
	acttextTime = textTime
	audio_p1.stream = sound_start
	audio_p1.play()
	FishManager.start()
	
func startQTEP1() :
	timer_node.visible = true
	QTEminiGame.visible = true
	QTEminiGame.set_process(true) 
	QTEminiGame.startP1()

func startQTEP2() :
	timer_node.visible = true
	QTEminiGame.visible = true
	QTEminiGame.set_process(true) 
	QTEminiGame.startP2()

	
# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(_delta: float) -> void:
	volume = volume - speed_reduce_audio
	AudioPlayerSlow.volume_db = 
	if P1pulse and P2pulse:
		InitMiniGame.btn_input.visible = false
		InitMiniGame.visible = false

	if Game_Manager.getTime() <= 0 or P1miss and P2miss:
		#TODO seteo de imagenes y animcaciones chachis
		InitMiniGame.btn_input.visible = false
		GanadorFinal.text = "EL PEZ SE HA ESCAPADO"
		audio_p1.stream = sound_fail
		audio_p1.play()
		GanadorFinal.visible = true
		QTEminiGame.desactiveP1Images()
		QTEminiGame.desactiveP2Images()
		Game_Manager.setTime(0)

	elif Game_Manager.P1win:
		InitMiniGame.btn_input.visible = false
		GanadorFinal.text = "PEZ PARA: JUGADOR 1"
		for i in range(Game_Manager.PointsP1 - 1):

		audio_p1.stream = sound_success
		audio_p1.play()
		GanadorFinal.visible = true

	elif Game_Manager.P2win:
		InitMiniGame.btn_input.visible = false
		GanadorFinal.text = "PEZ PARA: JUGADOR 2" + str(Game_Manager.fishP2 + 1)
		audio_p1.stream = sound_success
		audio_p1.play()
		GanadorFinal.visible = true
		
	if Game_Manager.PointsP1 >= 5: 
		GanadorFinal.text = "GANADOR: JUGADOR 1"
		audio_p1.stream = sound_success
		audio_p1.play()
		GanadorFinal.visible = true

	elif Game_Manager.PointsP2 >= 5:
		GanadorFinal.text = "GANADOR: JUGADOR 2"
		audio_p1.stream = sound_success
		audio_p1.play()
		GanadorFinal.visible = true
			

	if Game_Manager.getTime() <= 0 or Game_Manager.P1win or Game_Manager.P2win:
		acttextTime -= _delta
		QTEminiGame.desactiveP1Images()
		QTEminiGame.desactiveP2Images()
		if acttextTime <= 0:
			if Game_Manager.PointsP1 >= 5 or Game_Manager.PointsP2 >= 5:
				Game_Manager.changeScene("res://Scenes/menu_scene.tscn")

			else:
				resetRound()

	elif QTEminiGame.visible == true and Game_Manager.actualTime >= 0:
		Game_Manager.actualTime -= _delta
		Game_Manager.setTime(Game_Manager.actualTime)

	

func _on_init_minigame_player_1_input(result: bool) -> void:
	P1pulse = true
	Input.stop_joy_vibration(0)
	if result == true:
		startQTEP1()
	else :
		P1miss = true
		QTEminiGame.P1loose = true


func _on_init_minigame_player_2_input(result: bool) -> void:
	P2pulse = true
	Input.stop_joy_vibration(1)
	if result == true:
		startQTEP2()
	else :
		P2miss = true
		QTEminiGame.P2loose = true


func _on_init_minigame_pico() -> void:
	AudioPlayerSlow.stream_paused = true
	AudioPlayerFast.stream_paused = false
	Input.start_joy_vibration(0, 0.4, 0.8) 
	Input.start_joy_vibration(1, 0.4, 0.8) 


func _on_canya_v_2_boya_start_short_vibration(player: Variant) -> void:
	Input.start_joy_vibration(player, 0.1, 0.2,0.5) 
