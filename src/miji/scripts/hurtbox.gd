class_name Hurtbox
extends Area2D
# 피격 판정. 콜리전 레이어: hurtbox(2)에 속하고 hitbox(3)만 감지한다.

signal hurt(amount: int)

var _owner_body: Node


func _ready() -> void:
	_owner_body = get_parent()
	area_entered.connect(_on_area_entered)


func _on_area_entered(area: Area2D) -> void:
	if area is AttackHitbox and area.get_parent() != _owner_body:
		hurt.emit((area as AttackHitbox).damage)
