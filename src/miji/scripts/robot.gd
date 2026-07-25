class_name Robot
extends CharacterBody2D

enum State { ACTIVE, TRANSITIONING_OUT, FROZEN }

@export var base_speed: float = 120.0
@export var jump_velocity: float = -300.0
@export var base_max_hp: int = 100
@export var base_max_energy: int = 100
@export var base_attack_damage: int = 10
@export var base_attack_cooldown: float = 0.35
@export var robot_color: Color = Color.WHITE

const DASH_SPEED := 320.0
const DASH_DURATION := 0.18
const DASH_COOLDOWN := 0.6
const WALL_SLIDE_SPEED := 40.0
const ATTACK_DURATION := 0.15
const ENERGY_PER_HIT := 15

# 업그레이드 1레벨당 증가폭
const STAT_STEP := 0.15
const HP_PER_LEVEL := 20
const ATTACK_DAMAGE_PER_LEVEL := 5

# 업그레이드 가능한 스탯 목록. upgrade_station이 이 목록을 그대로 UI/입력에 쓴다.
const STAT_ATTACK := "attack"
const STAT_ATTACK_SPEED := "attack_speed"
const STAT_MOVE_SPEED := "move_speed"
const STAT_MAX_HP := "max_hp"
const STAT_KEYS := [STAT_ATTACK, STAT_ATTACK_SPEED, STAT_MOVE_SPEED, STAT_MAX_HP]

signal died(robot: Robot)
signal hp_changed(robot: Robot, current: int, max_value: int)
signal energy_changed(robot: Robot, current: int, max_value: int)

var state: State = State.FROZEN
var facing: int = 1

var stat_levels: Dictionary = {
	STAT_ATTACK: 0,
	STAT_ATTACK_SPEED: 0,
	STAT_MOVE_SPEED: 0,
	STAT_MAX_HP: 0,
}

var current_hp: int = 0
var current_energy: int = 0

# 최대치는 스탯 레벨에서 파생된다 — 별도로 들고 있으면 갱신을 놓칠 수 있다.
var max_hp: int:
	get:
		return base_max_hp + stat_levels[STAT_MAX_HP] * HP_PER_LEVEL

var max_energy: int:
	get:
		return base_max_energy

var active_skills: Array = []
var passive_skills: Array = []
var active_slot_count: int = 1
var passive_slot_count: int = 1

var _jumps_used: int = 0
var _dash_time_left: float = 0.0
var _dash_cooldown_left: float = 0.0
var _attack_cooldown_left: float = 0.0
var _gravity: float

@onready var sprite: ColorRect = $Sprite
@onready var hurtbox: Hurtbox = $Hurtbox
@onready var attack_hitbox: AttackHitbox = $AttackHitbox


func _ready() -> void:
	_gravity = ProjectSettings.get_setting("physics/2d/default_gravity")
	sprite.color = robot_color
	current_hp = max_hp
	hurtbox.hurt.connect(take_damage)
	attack_hitbox.hit_landed.connect(_on_hit_landed)
	Medium.register_robot(self)


func _physics_process(delta: float) -> void:
	_dash_cooldown_left = maxf(_dash_cooldown_left - delta, 0.0)
	_attack_cooldown_left = maxf(_attack_cooldown_left - delta, 0.0)

	if state == State.ACTIVE:
		_step_active(delta)
	else:
		_step_inactive(delta)

	move_and_slide()


# 조작권이 없는 상태. FROZEN이고 땅에 있으면 완전 정지해 발판 역할을 한다.
func _step_inactive(delta: float) -> void:
	velocity.x = 0.0
	if state == State.FROZEN and is_on_floor():
		velocity.y = 0.0
	else:
		velocity.y += _gravity * delta


func _step_active(delta: float) -> void:
	if is_on_floor():
		_jumps_used = 0

	var direction := Input.get_axis("move_left", "move_right")
	if direction != 0.0:
		_set_facing(int(sign(direction)))

	# 대쉬 중에는 중력/입력을 무시하고 설정된 속도를 유지한다.
	if _dash_time_left > 0.0:
		_dash_time_left -= delta
		return
	if _try_start_dash():
		return

	velocity.x = direction * current_speed()

	var on_wall := RunState.has_ability(RunState.ABILITY_WALL_CLIMB) and is_on_wall() and not is_on_floor()

	if Input.is_action_just_pressed("jump"):
		_try_jump(on_wall)
	if Input.is_action_just_pressed("attack"):
		_try_attack()

	velocity.y += _gravity * delta
	if on_wall and velocity.y > WALL_SLIDE_SPEED:
		velocity.y = WALL_SLIDE_SPEED


func _try_start_dash() -> bool:
	if _dash_cooldown_left > 0.0:
		return false
	if not RunState.has_ability(RunState.ABILITY_DASH):
		return false
	if not Input.is_action_just_pressed("dash"):
		return false
	_dash_time_left = DASH_DURATION
	_dash_cooldown_left = DASH_COOLDOWN
	velocity = Vector2(facing * DASH_SPEED, 0.0)
	return true


func _try_jump(on_wall: bool) -> void:
	if is_on_floor():
		velocity.y = jump_velocity
		_jumps_used = 1
	elif on_wall:
		velocity.y = jump_velocity
		velocity.x = -facing * current_speed()
		_jumps_used = 1
	elif RunState.has_ability(RunState.ABILITY_DOUBLE_JUMP) and _jumps_used < 2:
		velocity.y = jump_velocity
		_jumps_used += 1


func _set_facing(new_facing: int) -> void:
	if new_facing == facing:
		return
	facing = new_facing
	attack_hitbox.position.x = absf(attack_hitbox.position.x) * facing


func _try_attack() -> void:
	if _attack_cooldown_left > 0.0:
		return
	_attack_cooldown_left = current_attack_cooldown()
	attack_hitbox.strike(current_attack_damage(), ATTACK_DURATION)


func _on_hit_landed(_target: Node) -> void:
	_set_energy(current_energy + ENERGY_PER_HIT)


func take_damage(amount: int) -> void:
	if current_hp <= 0:
		return
	_set_hp(current_hp - amount)
	if current_hp <= 0:
		died.emit(self)


func refill() -> void:
	_set_hp(max_hp)
	_set_energy(max_energy)


func spend_energy(amount: int) -> bool:
	if current_energy < amount:
		return false
	_set_energy(current_energy - amount)
	return true


# 체력/자원 변경은 이 두 함수만 통과한다 — 클램프와 시그널을 한 곳에 모아둔다.
func _set_hp(value: int) -> void:
	current_hp = clampi(value, 0, max_hp)
	hp_changed.emit(self, current_hp, max_hp)


func _set_energy(value: int) -> void:
	current_energy = clampi(value, 0, max_energy)
	energy_changed.emit(self, current_energy, max_energy)


func get_stat_level(stat: String) -> int:
	return stat_levels.get(stat, 0)


func upgrade_stat(stat: String) -> void:
	if not stat_levels.has(stat):
		return
	stat_levels[stat] += 1
	if stat == STAT_MAX_HP:
		# max_hp는 위 레벨 증가로 이미 올라갔으므로 현재 체력만 같이 올려준다.
		_set_hp(current_hp + HP_PER_LEVEL)


func equip_active(skill: Skill, slot: int) -> bool:
	return _equip(active_skills, active_slot_count, skill, slot)


func equip_passive(skill: Skill, slot: int) -> bool:
	return _equip(passive_skills, passive_slot_count, skill, slot)


func _equip(slots: Array, slot_count: int, skill: Skill, slot: int) -> bool:
	if slot >= slot_count:
		return false
	slots.resize(slot_count)
	slots[slot] = skill
	return true


func use_active(slot: int) -> void:
	if slot >= active_skills.size():
		return
	var skill: Skill = active_skills[slot]
	if skill and spend_energy(skill.energy_cost):
		skill.activate(self)


func current_speed() -> float:
	return base_speed * (1.0 + stat_levels[STAT_MOVE_SPEED] * STAT_STEP)


func current_attack_cooldown() -> float:
	return base_attack_cooldown / (1.0 + stat_levels[STAT_ATTACK_SPEED] * STAT_STEP)


func current_attack_damage() -> int:
	return base_attack_damage + stat_levels[STAT_ATTACK] * ATTACK_DAMAGE_PER_LEVEL


func activate() -> void:
	state = State.ACTIVE


func set_transitioning_out() -> void:
	state = State.TRANSITIONING_OUT


func freeze() -> void:
	state = State.FROZEN
	velocity.x = 0.0
