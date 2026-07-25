extends Area2D
# 업그레이드 스테이션. UI는 기능 검증용 그레이박스(Label 텍스트) — 아트/디자인 확정 전 임시.
# 스탯 목록은 Robot.STAT_KEYS를 그대로 따르고, 입력 액션 이름은 "upgrade_<스탯>" 규칙으로 파생한다.

const COST_BASE := 20
const COST_PER_LEVEL := 15
const ACTION_PREFIX := "upgrade_"

const STAT_LABELS := {
	Robot.STAT_ATTACK: "[1] 공격력",
	Robot.STAT_ATTACK_SPEED: "[2] 공격속도",
	Robot.STAT_MOVE_SPEED: "[3] 이동속도",
	Robot.STAT_MAX_HP: "[4] 최대체력",
}

@onready var ui: CanvasLayer = $UpgradeUI
@onready var label: Label = $UpgradeUI/Panel/Label

var _player_inside: bool = false


func _ready() -> void:
	ui.visible = false
	body_entered.connect(_on_body_entered)
	body_exited.connect(_on_body_exited)
	# 표시 내용은 재화/스탯/활성 로봇이 바뀔 때만 달라진다 — 매 프레임 다시 만들 이유가 없다.
	RunState.currency_changed.connect(_on_currency_changed)
	Medium.swap_completed.connect(_on_swap_completed)


func _unhandled_input(event: InputEvent) -> void:
	if not _player_inside or not event is InputEventKey:
		return
	for stat in Robot.STAT_KEYS:
		if event.is_action_pressed(ACTION_PREFIX + stat):
			_try_upgrade(stat)
			return


func _on_body_entered(body: Node) -> void:
	if body is Robot:
		_player_inside = true
		ui.visible = true
		_refresh()


func _on_body_exited(body: Node) -> void:
	if body is Robot:
		_player_inside = false
		ui.visible = false


func _on_currency_changed(_amount: int) -> void:
	_refresh()


func _on_swap_completed(_active_robot: Robot) -> void:
	_refresh()


func _refresh() -> void:
	if not _player_inside:
		return
	var robot := Medium.get_active_robot()
	if robot == null:
		return
	label.text = _build_text(robot)


func _build_text(robot: Robot) -> String:
	var lines := ["업그레이드 스테이션 — 재화: %d" % RunState.currency]
	for stat in Robot.STAT_KEYS:
		var level := robot.get_stat_level(stat)
		lines.append("%s (Lv.%d, 비용 %d)" % [STAT_LABELS[stat], level, _cost(level)])
	return "\n".join(lines)


func _cost(level: int) -> int:
	return COST_BASE + level * COST_PER_LEVEL


func _try_upgrade(stat: String) -> void:
	var robot := Medium.get_active_robot()
	if robot == null:
		return
	if RunState.spend_currency(_cost(robot.get_stat_level(stat))):
		robot.upgrade_stat(stat)
		_refresh()
