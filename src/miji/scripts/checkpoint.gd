extends Area2D

@export var checkpoint_id: String = ""


func _ready() -> void:
	body_entered.connect(_on_body_entered)


func _on_body_entered(body: Node) -> void:
	if body is Robot:
		GameFlow.activate_checkpoint(checkpoint_id, global_position)
