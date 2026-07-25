class_name AttackHitbox
extends Area2D
# 공격 판정. 스윙 중에만 콜라이더를 켜는 책임을 스스로 가진다.
# 콜리전 레이어: hitbox(3)에 속하고 hurtbox(2)만 감지한다.

signal hit_landed(target: Node)

@export var damage: int = 10

var _owner_body: Node
var _active_left: float = 0.0

@onready var _shape: CollisionShape2D = $CollisionShape2D


func _ready() -> void:
	_owner_body = get_parent()
	_shape.disabled = true
	area_entered.connect(_on_area_entered)
	set_physics_process(false)


# 지정 시간만큼 판정을 켠다. 타이머 객체를 만들지 않고 물리 프레임에서 직접 센다.
func strike(strike_damage: int, duration: float) -> void:
	damage = strike_damage
	_active_left = duration
	_shape.set_deferred("disabled", false)
	set_physics_process(true)


func _physics_process(delta: float) -> void:
	_active_left -= delta
	if _active_left <= 0.0:
		_shape.set_deferred("disabled", true)
		set_physics_process(false)


func _on_area_entered(area: Area2D) -> void:
	# 레이어 분리로 hitbox끼리는 애초에 매칭되지 않지만,
	# 자기 자신의 hurtbox는 같은 레이어 쌍에 걸리므로 소유자 비교로 걸러낸다.
	if area is Hurtbox and area.get_parent() != _owner_body:
		hit_landed.emit(area.get_parent())
