extends Sprite2D

var cinematica : AnimatedSprite2D
var play_anim: bool = false
# Called when the node enters the scene tree for the first time.
func _ready() -> void:
	cinematica = get_children()[0]


func play_animation() ->void:
	play_anim = true
	pass

func _process(delta: float) -> void:
	if play_anim:
		cinematica.play()
		play_anim = false
	

