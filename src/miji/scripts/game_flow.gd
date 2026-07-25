extends Node
# Autoload: GameFlow
# 진행 흐름 총괄 — 사망 → 재화 드롭 → 체크포인트 리스폰, 체크포인트 활성화 순서를 정한다.
# RunState는 데이터만 들고 있고, "어떤 순서로 무엇을 한다"는 결정은 전부 여기에 있다.

const RESPAWN_SPACING := 20.0


func _ready() -> void:
	Medium.robot_registered.connect(_connect_robot)


func _connect_robot(robot: Robot) -> void:
	robot.died.connect(_on_robot_died)


# 체크포인트 프롭은 접촉 사실만 알리고, 순서는 이 함수가 정한다.
func activate_checkpoint(id: String, checkpoint_position: Vector2) -> void:
	RunState.try_recover_drop(checkpoint_position)
	RunState.set_checkpoint(id, checkpoint_position)
	Medium.refill_all()
	RunState.save_game()


func _on_robot_died(robot: Robot) -> void:
	RunState.on_death(robot.global_position)
	await get_tree().process_frame
	_respawn_all()


func _respawn_all() -> void:
	# 로봇 두 개를 같은 좌표에 겹쳐 놓으면 서로 물리 충돌로 밀려나므로 살짝 간격을 둔다.
	for i in Medium.robots.size():
		Medium.robots[i].global_position = RunState.last_checkpoint_position + Vector2(i * RESPAWN_SPACING, 0)
	Medium.refill_all()
	Medium.reset_active(0)
