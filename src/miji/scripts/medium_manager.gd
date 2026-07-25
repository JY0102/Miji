extends Node
# Autoload: Medium
# 매개체 상태 관리 — 활성 로봇 추적, 스왑 연출 처리.
# 입력 액션은 project.godot의 [input] 섹션에 선언되어 있다.

signal swap_started(from_robot: Robot, to_robot: Robot)
signal swap_completed(active_robot: Robot)
signal robot_registered(robot: Robot)

const SWAP_DURATION := 0.5
const ORB_SIZE := Vector2(6, 6)

var robots: Array[Robot] = []
var active_index: int = 0
var swap_in_progress: bool = false

# 스왑 연출용 임시 placeholder (아트 확정 전까지 흰 점).
# 스왑마다 새로 만들지 않고 하나를 숨겨뒀다 재사용한다.
var _orb: ColorRect
var _orb_tween: Tween


func _ready() -> void:
	_orb = ColorRect.new()
	_orb.color = Color.WHITE
	_orb.size = ORB_SIZE
	_orb.visible = false
	add_child(_orb)


func _unhandled_input(event: InputEvent) -> void:
	if not event is InputEventKey:
		return
	if event.is_action_pressed("swap"):
		_try_swap()


func get_active_robot() -> Robot:
	if robots.is_empty():
		return null
	return robots[active_index]


func register_robot(robot: Robot) -> void:
	robots.append(robot)
	_apply_active_state()
	robot_registered.emit(robot)


func reset_active(index: int) -> void:
	if robots.is_empty():
		return
	active_index = clampi(index, 0, robots.size() - 1)
	_apply_active_state()
	swap_in_progress = false


func refill_all() -> void:
	for robot in robots:
		robot.refill()


# "정확히 한 대만 활성" 규칙은 이 함수에만 존재한다.
func _apply_active_state() -> void:
	for i in robots.size():
		if i == active_index:
			robots[i].activate()
		else:
			robots[i].freeze()


func _try_swap() -> void:
	if swap_in_progress or robots.size() < 2:
		return
	swap_in_progress = true

	var from_robot := robots[active_index]
	var to_index := (active_index + 1) % robots.size()
	var to_robot := robots[to_index]

	from_robot.set_transitioning_out()
	swap_started.emit(from_robot, to_robot)

	await _play_orb_travel(from_robot.global_position, to_robot.global_position)

	from_robot.freeze()
	active_index = to_index
	to_robot.activate()
	swap_in_progress = false
	swap_completed.emit(to_robot)


func _play_orb_travel(from_position: Vector2, to_position: Vector2) -> void:
	var offset := ORB_SIZE / 2.0
	_orb.global_position = from_position - offset
	_orb.visible = true

	if _orb_tween:
		_orb_tween.kill()
	_orb_tween = create_tween()
	_orb_tween.tween_property(_orb, "global_position", to_position - offset, SWAP_DURATION)
	await _orb_tween.finished

	_orb.visible = false
