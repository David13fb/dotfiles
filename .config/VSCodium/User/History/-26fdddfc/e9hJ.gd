extends Node
class_name GameManager

#Scenes
enum GameScenes {
	Menu,
	Play,
	Tutorial
}
var MenuScene : String = "res://Scenes/menu_scene.tscn"
var PlayScene : String = "res://Scenes/game_scene.tscn"
var TutorialScene : String = "res://Scenes/tutorial_scene.tscn"

#ActualScene
var GameScene:GameScenes = GameScenes.Menu

#TimerFromPlayScene
var actualTime : float = 0.0
var maxTime : float = 120.0

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	_changeScene(MenuScene)
	GameScene = GameScenes.Menu # Empezamos en el menu principal
	pass # Replace with function body.

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if GameScene == GameScenes.Menu: _MenuState(delta)
	elif GameScene == GameScenes.Play: _PlayState(delta)
	elif GameScene == GameScenes.Tutorial: _TutorialState(delta)
	else: _MenuState(delta)

func _changeScene(scene: String) -> void:
	if (get_tree().change_scene_to_file(scene) == null):
		print("Error en cambio de escena")

func _MenuState(_delta: float) -> void:
	print("ESTADO MENU")
	if (Input.is_anything_pressed()):
		_changeScene(PlayScene)

func _PlayState(_delta: float) -> void:
	print("ESTADO JUGAR")
	actualTime += _delta
	if actualTime >= maxTime:
		if (Input.is_anything_pressed()):
			actualTime = 0
			_changeScene(MenuScene)

func _TutorialState(_delta: float) -> void:
	pass
