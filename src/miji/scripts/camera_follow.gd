extends Camera2D

@export var smoothing: float = 8.0


func _process(delta: float) -> void:
	var target := Medium.get_active_robot()
	if target == null:
		return
	global_position = global_position.lerp(target.global_position, clampf(smoothing * delta, 0.0, 1.0))
