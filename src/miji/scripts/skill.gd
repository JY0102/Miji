class_name Skill
extends Resource
# 스킬 슬롯 프레임워크 — 구체적 스킬 내용은 미확정, 장착/발동 구조만 정의

@export var skill_name: String = ""
@export var energy_cost: int = 0
@export var is_passive: bool = false


func activate(_robot: Robot) -> void:
	pass
