extends CanvasLayer
# 로봇별 HP/자원 바 + 재화 표시. 슬롯은 Medium.robots 순서에 대응한다.
# 씬의 패널 개수만큼만 표시하므로, 로봇이 늘어나면 HUD.tscn에 패널을 추가하면 된다.

@onready var currency_label: Label = $Currency
@onready var _panels: Array[Node] = [$RobotA, $RobotB]


func _ready() -> void:
	RunState.currency_changed.connect(_on_currency_changed)
	_on_currency_changed(RunState.currency)

	for robot in Medium.robots:
		_connect_robot(robot)
	Medium.robot_registered.connect(_connect_robot)
	Medium.swap_completed.connect(_on_swap_completed)
	_update_active_highlight()


func _connect_robot(robot: Robot) -> void:
	var slot := Medium.robots.find(robot)
	if slot < 0 or slot >= _panels.size():
		return
	# 슬롯 번호를 연결 시점에 고정한다 — 시그널마다 배열을 훑지 않아도 된다.
	robot.hp_changed.connect(_on_hp_changed.bind(slot))
	robot.energy_changed.connect(_on_energy_changed.bind(slot))
	_set_bar(slot, "HPBar", robot.current_hp, robot.max_hp)
	_set_bar(slot, "EnergyBar", robot.current_energy, robot.max_energy)


func _on_currency_changed(amount: int) -> void:
	currency_label.text = "재화: %d" % amount


func _on_hp_changed(_robot: Robot, current: int, max_value: int, slot: int) -> void:
	_set_bar(slot, "HPBar", current, max_value)


func _on_energy_changed(_robot: Robot, current: int, max_value: int, slot: int) -> void:
	_set_bar(slot, "EnergyBar", current, max_value)


func _set_bar(slot: int, bar_name: String, current: int, max_value: int) -> void:
	var bar: ProgressBar = _panels[slot].get_node(bar_name)
	bar.max_value = max_value
	bar.value = current


func _on_swap_completed(_active_robot: Robot) -> void:
	_update_active_highlight()


func _update_active_highlight() -> void:
	for i in _panels.size():
		var name_label: Label = _panels[i].get_node("NameLabel")
		name_label.modulate.a = 1.0 if i == Medium.active_index else 0.4
