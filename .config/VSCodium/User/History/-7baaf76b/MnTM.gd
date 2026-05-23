extends StaticBody2D

@export var scene : String = ""
@export var flip : bool = true
@onready var interactable: Area2D = $Interactable
@onready var sprite_2d: Sprite2D = $Sprite2D

func aparezco() ->void:
	set_process(true)
	visible = true

# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	interactable.interact = _on_interact #Añadido para interactuar con el objeto
	set_process(false)
	if flip:
		scale.x *=-1
	pass # Replace with function body.
	
#Comportamiento del objeto al interactuar con el
func _on_interact():
	if get_tree().change_scene_to_file(scene) == null:
		print("Error la escena",scene,"no se ha encontrado")
	if sprite_2d.frame == 0:
		#sprite_2d.frame = 1 #Para cambiar el sprite cuando ya hayamos interactuado
		interactable.is_interactable = false
		print("He interactuado")

# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	pass
