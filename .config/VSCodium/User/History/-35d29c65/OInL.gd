extends Node


# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	pass # Replace with function body.


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	# Obtener datos del acelerómetro
	var accel = Input.get_accelerometer()
	print(accel)
	pass
