extends Sprite2D

@export var animation : AnimationPlayer
@export var segundos_en_negro : float = 2.0
var animStart : bool = false

func startAnimation() ->void:
	animation.play("MascaraTransición")

func startAnimationBackwards() -> void:
	animation.play_backwards("MascaraTransición")

func selectFinal() ->void:
	match GameManager.devuelve_mascara(): #LA CHULETA EN EL GAMEMANAGER
		0:
			texture = load("res://Assets/Máscaras/MáscaraFlor1.png")
		1:
			texture = load("res://Assets/Máscaras/MáscaraFlor1Marchita.png")
		2:
			texture = load("res://Assets/Máscaras/MáscaraFlor1y2.png")
		3:
			texture = load("res://Assets/Máscaras/MáscaraFlor1y2marchita.png")
		4:
			texture = load("res://Assets/Máscaras/MáscaraFlor1marchitay2.png")
		5:
			texture = load("res://Assets/Máscaras/MáscaraFlor1marchita,2marchita.png")
		6:
			texture = load("res://Assets/Máscaras/MáscaraFlor1,2y3.png")
		7:
			texture = load("res://Assets/Máscaras/MáscaraFlor1marchita,2y3.png")			
		8:
			texture = load("res://Assets/Máscaras/MáscaraFlor1,2marchita,3.png")
		9:
			texture = load("res://Assets/Máscaras/MáscaraFlor1marchita,2marchitay3.png")
		10:
			texture = load("res://Assets/Máscaras/MáscaraFlor1,2y3marchita.png")
		11:
			texture = load("res://Assets/Máscaras/MáscaraFlor1marchita,2y3marchita.png")
		12:
			texture = load("res://Assets/Máscaras/MáscaraFlor1,2marchita,3marchita.png")
		13:
			texture = load("res://Assets/Máscaras/MáscaraFlor1marchita,2marchitay3marchita.png")


# Called every frame. 'delta' is the elapsed time since the previous frame.
func _process(delta: float) -> void:
	if animStart and segundos_en_negro > 0:
		segundos_en_negro -= delta

	elif segundos_en_negro <= 0:
		animStart = false
		selectFinal()
		startAnimationBackwards()

	pass
