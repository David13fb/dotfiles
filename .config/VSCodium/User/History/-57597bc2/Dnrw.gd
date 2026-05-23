extends Node

@export var camera : Node
@export var violence_dialogue: Node2D
@export var talk_dialogue: Node2D 
@export var talk_btn: Button
@export var violence_btn: Button
@export var win_scene : String
@export var defeat_scene : String
@export var id_end_dialog : String
@export var scene : String
@export var sprite_vic : Sprite2D
@export var sprite_def : Sprite2D
@export var enemy_node : Node2D
@export var pos_array: Array[Node2D]
var can_interact : bool = false
@export var atks : int = 0
@export var tks : int = 0
var hits_points: int = 5
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	violence_dialogue.dialogue_manager.show_dialogue(id_violence_dialog)
	talk_dialogue.dialogue_manager.show_dialogue(id_talk_dialog);
	talk_dialogue.niapa_ini();
	talk_dialogue.set_active(false)
	violence_dialogue.set_btn(pos_array);
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass

func violence() -> void:
	if can_interact:
		violence_dialogue.desactive_text_rect()
		talk_dialogue.set_active(false)
		violence_dialogue.set_active(true)
		talk_btn.set_process(false)
		talk_btn.visible = false
		violence_btn.set_process(false)
		violence_btn.visible = false
		can_interact = false
	
	pass

func talk() -> void:
	if can_interact:
		talk_dialogue.set_active(true)
		violence_dialogue.set_active(false)
		talk_btn.set_process(false)
		talk_btn.visible = false
		violence_btn.set_process(false)
		violence_btn.visible = false
		print_debug("de chill")
		talk_dialogue.change_text_pos()
		can_interact = false
	pass


func _on_talk_button_down() -> void:
	print_debug("de chill")
	talk()
	pass # Replace with function body.


func _on_violence_button_down() -> void:
	violence()
	pass # Replace with function body.


func _on_violence_dialogue_end_violence() -> void:
	atks+=1
	if atks <= 3:
		enemy_node.next_texture()
	if atks == 2:
		#sprite_vic.visible = true
		#sprite_vic.startAnimation()
		print("")
		violence_dialogue.set_active(true)
		violence_dialogue.dialogue_manager.show_dialogue(id_end_dialog)
	if atks == 3:
		print("Cambio escena")
		GameManager.asigna_flor(0,false)
		get_tree().change_scene_to_file(scene)
	pass # Replace with function body.



func _on_talk_dialogue_end_talk() -> void:
	tks += 1
	if tks == 2:
		#sprite_vic.visible = true
		#sprite_vic.startAnimation()
		violence_dialogue.set_active(true)
		violence_dialogue.dialogue_manager.show_dialogue(id_end_dialog)

		print_debug("Victoria por carisma")

	if tks == 3:
		#cambiar scena
		GameManager.asigna_flor(0,true)
		get_tree().change_scene_to_file(scene)
		print("Aaa")
	pass # Replace with function body.


func _on_violence_dialogue_fail_violence() -> void:
	print_debug("no sabes comunicarte")
	camera.camera_shake()
	hits_points-= 1
	if hits_points == 0:
		_bad_end()
	pass # Replace with function body.
func _bad_end() -> void:
	get_tree().change_scene_to_file("res://Prefabs/Personajes/CinematicaDerrota.tscn")
	#sprite_def.visible = true
	#sprite_def.playAnimation()
	pass


func _on_talk_dialogue_fail_talk() -> void:
	print_debug("no sabes pegarte")
	camera.camera_shake()
	hits_points-= 1
	if hits_points == 0:
		_bad_end()
	pass # Replace with function body.


func _on_talk_dialogue_show_btn() -> void:
	
	pass # Replace with function body.


func _on_violence_dialogue_show_btn() -> void:
	
	pass # Replace with function body.



func _on_violence_dialogue_can_interact() -> void:
	can_interact = true
	talk_btn.set_process(true)
	talk_btn.visible = true
	violence_btn.set_process(true)
	violence_btn.visible = true
	pass # Replace with function body.

func _on_talk_dialogue_can_interact() -> void:
	talk_btn.set_process(true)
	talk_btn.visible = true
	violence_btn.set_process(true)
	violence_btn.visible = true
	can_interact = true
	pass # Replace with function body.
