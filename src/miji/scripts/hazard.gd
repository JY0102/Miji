extends Area2D
# 임시 테스트용 즉사 트랩 — 사망/재화 드롭/체크포인트 리스폰 흐름 검증용

@export var damage: int = 999


func _ready() -> void:
	body_entered.connect(_on_body_entered)


func _on_body_entered(body: Node) -> void:
	if body is Robot:
		body.take_damage(damage)
