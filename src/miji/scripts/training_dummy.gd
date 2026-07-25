extends StaticBody2D
# 임시 테스트용 더미 — 전투/자원 충전 검증용. 실제 적 AI는 Phase 11에서 별도 구현.

const BASE_COLOR := Color(0.6, 0.2, 0.2)
const MIN_ALPHA := 0.3

@export var max_hp: int = 50
@export var currency_reward: int = 5

var current_hp: int

@onready var sprite: ColorRect = $Sprite
@onready var hurtbox: Hurtbox = $Hurtbox


func _ready() -> void:
	current_hp = max_hp
	hurtbox.hurt.connect(_on_hurt)


func _on_hurt(amount: int) -> void:
	current_hp = maxi(current_hp - amount, 0)
	if current_hp == 0:
		RunState.add_currency(currency_reward)
		current_hp = max_hp
	sprite.color = Color(BASE_COLOR, float(current_hp) / max_hp * (1.0 - MIN_ALPHA) + MIN_ALPHA)
