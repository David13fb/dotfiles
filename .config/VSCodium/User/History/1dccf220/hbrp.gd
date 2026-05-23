extends Area2D
class_name InteractableObject

@export var interact_label: Label 

enum InteractType {
	NONE,
	CHANGE_SCENE,
	OBJECT
}

@export var interact_name: String = ""
@export var interact_type: InteractType = InteractType.NONE
@export var is_interactable: bool = true

#var interact: Callable = func():
#	pass
	
func _ready() -> void:
	set_process(true)
	input_pickable = true
	print("Interactable READY")
	mouse_entered.connect(_on_mouse_entered)
	mouse_exited.connect(_on_mouse_exited)

func _on_mouse_entered():
	print("Mouse dentro del interactuable")
	interact_label.show()

func _on_mouse_exited():
	print("Mouse fuera del interactuable")
	interact_label.hide()
	
	
func _process(delta: float) -> void:
	if abs(get_global_mouse_position() - global_position) < Vector2(100,100):
		is_interactable = true
	else: is_interactable = false

func active_Area(MousePosition: Vector2) -> bool:
	if abs(MousePosition - global_position) < Vector2(100,100):
		print("Entramos en el area de efecto")
		#set_process(true)
		return true
	else:
		#set_process(false) 
		return false
