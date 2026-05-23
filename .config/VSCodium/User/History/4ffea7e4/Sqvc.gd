extends Control

const FONT_REGULAR = preload("res://Assets/Fonts/lucidagrande.ttf")
const FONT_ITALIC = preload("res://Assets/Fonts/lucidagrande-italic.otf")

# --- GAME STATE ---
var game_state: Node

# --- JSONS ---
var json_data: Dictionary
var json_file_path: String

# --- CAJA NPCS ---
@export var npc_s_rect: Panel
@export var npc_label_text: Label

# --- CAJA CLOVER ---
@export var clover_rect: Panel
@export var label_text: Label

# BOTONES OPCIONES
@export var options_rect: ColorRect
@export var response_buttons: Array[Button]

# --- VARIABLES DE ESTADO ---
var name_of_the_event: String
var actual_line: int
var dialog_array: Array
var is_dialogue_showing: bool = false
var is_waiting_for_response: bool = false
var is_end_of_dialogue: bool = false
var response_next_event: Array = []
var visible_option_count: int = 0

# Manejo de opciones
var selected_option_index: int = 0
var last_input_was_mouse: bool = false

# Manejo de UI dialogo
var current_npc: Node3D
var current_portrait_texture: Texture2D = null
var current_npc_name: String = ""
var current_clover_portrait_texture: Texture2D = null
var current_clover_name: String = ""
var canvas: CanvasLayer

#Signals
signal options_display
signal dialog_end
signal fail
var num_ops : int = 0
var last_node_id = ""
func _ready():
	game_state = get_node_or_null("/root/GameState")

	var locale = TranslationServer.get_locale()
	if locale.begins_with("es"): 
		json_file_path = "res://Assets/Json/text.json"
	else:
		json_file_path = "res://Assets/Json/text.json"

	json_data = load_dialogue(json_file_path)

	if response_buttons.size() >= 3:
		response_next_event.resize(3)
		for i in range(response_buttons.size()):
			var btn = response_buttons[i]
			btn.pressed.connect(func():
				last_input_was_mouse = true
				selected_option_index = i
				_on_response_button_pressed(response_next_event[i])
			)
			btn.mouse_entered.connect(func():
				last_input_was_mouse = true
				selected_option_index = i
				update_option_selection()
			)
	else:
		push_error("Error: Los botones de respuesta no están asignados en el Inspector.")

	canvas = get_node("CanvasLayer")
	canvas.visible = false
	hide_options()

# --- INPUT GENERAL ---
func _input(event):

	if event.is_action_pressed("Interact"):
		if is_end_of_dialogue:
			var last_line_data = dialog_array[dialog_array.size() - 1] as Dictionary

			# --- GESTIÓN DE 'NEXT' (DIÁLOGO O CINEMÁTICA) ---
			if last_line_data.has("next"):
				var next_event_name = str(last_line_data["next"])
				
				# 1. Si está en el JSON, es otro diálogo
				if json_data.has(next_event_name):
					is_end_of_dialogue = false
					is_dialogue_showing = false
					if last_node_id == next_event_name:
						fail.emit()
					if last_node_id == "":
						last_node_id = next_event_name
					show_dialogue(next_event_name)
					return 
				else:
					# 2. Si NO está, es una CINEMÁTICA (Evento de código)
					print("Dialog: Disparando evento externo -> ", next_event_name)
					
					# Cerramos la UI
					canvas.visible = false
					is_end_of_dialogue = false
					is_dialogue_showing = false
					
					# Avisamos al NPC
					if current_npc and current_npc.has_method("set_next_dialog"):
						current_npc.set_next_dialog(next_event_name)
					else:
						push_error("Dialog: El NPC no tiene método set_next_dialog para: " + next_event_name)
					
					return # IMPORTANTE: Salimos aquí para no reabrir el diálogo
			# --------------------------------------------------
			
			is_end_of_dialogue = false
			is_dialogue_showing = false

			var next_event = json_data[name_of_the_event] as Dictionary
			var options_array: Array = []
			
			if next_event.has("options"):
				options_array = next_event["options"]
			
			if options_array != null and options_array.size() > 0:
				show_options(options_array)
			else: 
				# FIN DEL DIÁLOGO (CERRAR)

				canvas.visible = false
				get_viewport().set_input_as_handled()

				if game_state:
					game_state.set("can_interact", true)
					game_state.set("can_move", true)
					game_state.set("ignore_next_interact", true)

				if current_npc:
					print("toca cerrar")
					print("currentnpc: ", current_npc)
					if current_npc.has_method("close_shop_ui"):
						print("toca cerrar")
						current_npc.call("close_shop_ui")
					if current_npc.has_method("close_house_ui"):
						current_npc.call("close_house_ui")
					if current_npc.has_method("set_next_dialog"):
						current_npc.call("set_next_dialog", name_of_the_event)
		else:
			show_next_dialogue()


func get_screen_position_from_world(world_pos: Vector3) -> Vector2:
	var camera = get_viewport().get_camera_3d()
	if camera == null:
		return Vector2.ZERO
	var screen_pos = camera.unproject_position(world_pos)
	return screen_pos

func show_dialogue(event_name: String):
	if current_npc != null and current_npc.has_method(event_name):
		print("Interceptando '", event_name, "' y llamando al método del NPC.")
		current_npc.call(event_name)
		return

	if is_dialogue_showing: return
	
	if json_data == null:
		json_data = load_dialogue(json_file_path)

	if not json_data.has(event_name):
		# INTENTO DE EVENTO DIRECTO (Cinemática)
		if current_npc and current_npc.has_method("set_next_dialog"):
			print("Dialog: Evento directo a NPC -> ", event_name)
			current_npc.set_next_dialog(event_name)
			canvas.visible = false
			is_dialogue_showing = false
			return
		
		push_error("showDialogue: Evento '" + event_name + "' no encontrado en JSON ni como método en el NPC.")
		return

	var dialogue_event = json_data[event_name] as Dictionary
	is_dialogue_showing = true
	is_end_of_dialogue = false
	is_waiting_for_response = false
	actual_line = 0
	name_of_the_event = event_name

	

	dialog_array = dialogue_event["dialogs"]
	canvas.visible = true
	show_next_dialogue()

func show_next_dialogue():
	if not is_dialogue_showing: return

	if dialog_array.size() == 0:
		is_end_of_dialogue = true
		print_debug("Mellamo")
		dialog_end.emit()
		return

	var line_data = dialog_array[actual_line] as Dictionary

	if str(line_data["author"]) == "npc":
		npc_s_rect.visible = true
		clover_rect.visible = false

		if line_data.has("text"):
			npc_label_text.text = str(line_data["text"])
			var anim_player = npc_s_rect.get_node("AnimationPlayer")
			anim_player.play("showText")


	else: # Clover
		npc_s_rect.visible = false
		clover_rect.visible = true

		var text_to_display = ""
		if line_data.has("quadrant_thought"):
			var quadrant_data = line_data["quadrant_thought"] as Dictionary
			text_to_display = get_quadrant_thought(quadrant_data)
		elif line_data.has("text"):
			text_to_display = str(line_data["text"])
		
		if not text_to_display.is_empty():
			var final_text = text_to_display
			
			if label_text.label_settings:
				if final_text.begins_with("*") and final_text.ends_with("*"):
					label_text.label_settings.font = FONT_ITALIC
					final_text = final_text.trim_prefix("*").trim_suffix("*")
				else:
					label_text.label_settings.font = FONT_REGULAR
			else:
				if final_text.begins_with("*") and final_text.ends_with("*"):
					label_text.add_theme_font_override("font", FONT_ITALIC)
					final_text = final_text.trim_prefix("*").trim_suffix("*")
				else:
					label_text.add_theme_font_override("font", FONT_REGULAR)
			
			label_text.text = final_text
			clover_rect.get_node("AnimationPlayer").play("showText")

	is_end_of_dialogue = actual_line >= dialog_array.size() - 1
	if not is_end_of_dialogue:
		actual_line += 1
	else: 
		print_debug(last_node_id)
		dialog_end.emit()

func show_options(options_array: Array):
	is_waiting_for_response = true
	hide_options()
	options_rect.visible = true
	npc_s_rect.visible = false
	var aux :bool = clover_rect.visible
	if not aux:
		#JUGADOR NO VISIBLE
		clover_rect.visible = true
		label_text.text = npc_label_text.text;
	
	visible_option_count = options_array.size()
	num_ops = options_array.size()
	for i in range(options_array.size()):
		if i >= response_buttons.size(): break
		var option = options_array[i] as Dictionary

		response_buttons[i].text = str(option["response"])
		response_buttons[i].visible = false

		var gain_val = 0
		if option.has("gain"):
			gain_val = int(option["gain"])

		response_next_event[i] = {
			"nextEvent": str(option["next"]),
			"answerToDisplay": str(option["response"]),
			"gain": gain_val
		}

	selected_option_index = 0
	last_input_was_mouse = false
	options_display.emit();
	update_option_selection()

func _on_response_button_pressed(response: Dictionary):
	is_waiting_for_response = false
	hide_options()
	npc_label_text.text = ""

	var choice_text = str(response["answerToDisplay"])
	var gain_val = response["gain"]
	var analytics_data = {}

	var game = get_node_or_null("/root/GameState")
	if game:
		if response["gain"] == 0:
			game.call("changeActivity", -1)
			analytics_data["stat_change"] = "activity -1"
		else:
			var gain = response["gain"]
			if gain == 1 or gain == -1:
				game.call("changeMoral", gain)
				analytics_data["stat_change"] = "moral " + str(gain_val)

				var pos = int(game.get("empujarPos"))
				var neg = int(game.get("empujarNeg"))

				if gain == -1: pos += 1
				elif gain == 1: neg += 1

				var diff = pos - neg
				if diff > 0: game.set("empujarPos", diff)
				elif diff < 0: game.set("empujarNeg", diff)

				game.call("update_population_opinion")

	var analytics = get_node_or_null("/root/Analytics")
	if analytics:
		analytics.get_alternative(name_of_the_event).selected(choice_text, analytics_data)

	await show_dialogue(response["nextEvent"])

func hide_options():
	for button in response_buttons:
		if button: button.visible = false
	options_rect.visible = false

func load_dialogue(file_path: String) -> Dictionary:
	var file = FileAccess.open(file_path, FileAccess.READ)
	if file == null:
		push_error("Could not open file: " + file_path)
		return {}

	var json_text = file.get_as_text()
	file.close()
	
	var parsed_result = JSON.parse_string(json_text)
	if parsed_result is Dictionary:
		return parsed_result
	return {}

func update_option_selection():
	if response_buttons.is_empty():
		return
	selected_option_index = clamp(selected_option_index, 0, response_buttons.size() - 1)
	for i in range(response_buttons.size()):
		if response_buttons[i] == null: continue
		if not last_input_was_mouse and i == selected_option_index and response_buttons[i].visible:
			response_buttons[i].grab_focus()
		else:
			response_buttons[i].release_focus()

func get_quadrant_thought(quadrant_data: Dictionary) -> String:
	var moral = int(game_state.get("moral"))
	var activity = int(game_state.get("activity"))
	
	var key: String
	if activity > 0:
		key = "pos_act_pos_mor" if moral > 0 else "pos_act_neg_mor"
	else: 
		key = "neg_act_pos_mor" if moral > 0 else "neg_act_neg_mor"

	if quadrant_data.has(key):
		return str(quadrant_data[key])
	else:
		push_error("GetQuadrantThought: Clave '" + key + "' no encontrada en JSON!")
		return "Error: Falta diálogo para este cuadrante."

func show_ops_btn() -> void:
	for i in range(num_ops):
		response_buttons[i].visible = true
	pass