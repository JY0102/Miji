extends Node
# Autoload: RunState
# 능력 해금, 재화, 체크포인트, 세이브/로드 담당.
# 데이터 저장소 역할만 한다 — 사망/체크포인트 진행 흐름은 GameFlow가 총괄한다.

signal currency_changed(amount: int)
signal ability_unlocked(ability: String)

const SAVE_PATH := "user://save.json"

# 능력 키는 robot.gd가 매 프레임 조회하므로 문자열 리터럴 대신 상수로 공유한다.
const ABILITY_DASH := "dash"
const ABILITY_DOUBLE_JUMP := "double_jump"
const ABILITY_WALL_CLIMB := "wall_climb"
const ABILITY_MEDIUM_STABILIZATION := "medium_stabilization"
const CORE_ABILITIES := [
	ABILITY_DASH,
	ABILITY_DOUBLE_JUMP,
	ABILITY_WALL_CLIMB,
	ABILITY_MEDIUM_STABILIZATION,
]

const DEFAULT_SPAWN := Vector2(120, 152)
const RECOVER_RADIUS := 32.0

var unlocked_abilities: Dictionary = {}
var currency: int = 0
var last_checkpoint_id: String = ""
var last_checkpoint_position: Vector2 = DEFAULT_SPAWN
var dropped_currency: int = 0
var dropped_currency_position: Vector2 = Vector2.ZERO
var dropped_currency_valid: bool = false


func _ready() -> void:
	for ability in CORE_ABILITIES:
		# TEMP: 실제 게이트 콘텐츠(Phase 10+)가 나오기 전까지 전부 해금 상태로 테스트.
		# 실제 세이브는 전부 false로 시작해서 보스/탐색 보상으로 unlock_ability() 호출해야 함.
		unlocked_abilities[ability] = true


func has_ability(ability: String) -> bool:
	return unlocked_abilities.get(ability, false)


func unlock_ability(ability: String) -> void:
	if has_ability(ability):
		return
	unlocked_abilities[ability] = true
	ability_unlocked.emit(ability)


func add_currency(amount: int) -> void:
	currency += amount
	currency_changed.emit(currency)


func spend_currency(amount: int) -> bool:
	if currency < amount:
		return false
	currency -= amount
	currency_changed.emit(currency)
	return true


func on_death(death_position: Vector2) -> void:
	dropped_currency = currency
	dropped_currency_position = death_position
	dropped_currency_valid = true
	currency = 0
	currency_changed.emit(currency)


func try_recover_drop(at_position: Vector2, recover_radius: float = RECOVER_RADIUS) -> void:
	if not dropped_currency_valid:
		return
	if at_position.distance_to(dropped_currency_position) <= recover_radius:
		add_currency(dropped_currency)
		dropped_currency_valid = false


func set_checkpoint(id: String, position: Vector2) -> void:
	last_checkpoint_id = id
	last_checkpoint_position = position


func save_game() -> void:
	var data := {
		"currency": currency,
		"unlocked_abilities": unlocked_abilities,
		"last_checkpoint_id": last_checkpoint_id,
		"last_checkpoint_position": _pack_vec(last_checkpoint_position),
		"dropped_currency": dropped_currency,
		"dropped_currency_position": _pack_vec(dropped_currency_position),
		"dropped_currency_valid": dropped_currency_valid,
	}
	var file := FileAccess.open(SAVE_PATH, FileAccess.WRITE)
	if file == null:
		push_error("세이브 파일을 열 수 없습니다 (%s): %s" % [SAVE_PATH, error_string(FileAccess.get_open_error())])
		return
	file.store_string(JSON.stringify(data))
	file.close()


func load_game() -> bool:
	if not FileAccess.file_exists(SAVE_PATH):
		return false
	var file := FileAccess.open(SAVE_PATH, FileAccess.READ)
	if file == null:
		push_error("세이브 파일을 읽을 수 없습니다 (%s): %s" % [SAVE_PATH, error_string(FileAccess.get_open_error())])
		return false
	var text := file.get_as_text()
	file.close()
	var parsed = JSON.parse_string(text)
	if typeof(parsed) != TYPE_DICTIONARY:
		return false
	# JSON은 모든 수를 float으로 되돌리므로 int 필드는 명시적으로 변환한다.
	currency = int(parsed.get("currency", 0))
	var loaded_abilities: Dictionary = parsed.get("unlocked_abilities", {})
	for key in loaded_abilities:
		unlocked_abilities[key] = loaded_abilities[key]
	last_checkpoint_id = parsed.get("last_checkpoint_id", "")
	last_checkpoint_position = _unpack_vec(parsed.get("last_checkpoint_position"), DEFAULT_SPAWN)
	dropped_currency = int(parsed.get("dropped_currency", 0))
	dropped_currency_position = _unpack_vec(parsed.get("dropped_currency_position"), Vector2.ZERO)
	dropped_currency_valid = parsed.get("dropped_currency_valid", false)
	currency_changed.emit(currency)
	return true


static func _pack_vec(v: Vector2) -> Dictionary:
	return {"x": v.x, "y": v.y}


static func _unpack_vec(raw, fallback: Vector2) -> Vector2:
	if raw is Dictionary and raw.has("x") and raw.has("y"):
		return Vector2(raw["x"], raw["y"])
	return fallback
