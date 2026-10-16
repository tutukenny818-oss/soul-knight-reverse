// ==============================================================================
// Soul Knight 8.6.0 — 核心战斗/天赋/加密 类型摘录
//
// 来源：Il2CppDumper 从解密后的 libil2cpp.so + global-metadata.dat 提取
// 原始：47,503 个类型（25.4 MB）
// 本文：346 个核心类型
//
// 完整 dump 见 Release
// ==============================================================================

// --------------------------------------------------------------------
// NewMechaTalentCardPickup  [class]
// --------------------------------------------------------------------
public sealed class NewMechaTalentCardPickup : MonoBehaviour // TypeDefIndex: 460
{
	// Fields
	private const string FloatingVisualName = "Img";
	private const string FloatingVisualNameLower = "img";
	private Transform floatingVisual; // 0x20
	private float floatingAmplitude; // 0x28
	private float floatingSpeed; // 0x2C
	private bool _picked; // 0x30
	private Vector3 _floatingVisualBaseLocalPosition; // 0x34
}

// --------------------------------------------------------------------
// NewMechaTalentConfig  [class]
// --------------------------------------------------------------------
public sealed class NewMechaTalentConfig.TalentConfig // TypeDefIndex: 461
{
	// Fields
	public string TalentId; // 0x10
	public bool IsCommon; // 0x18
	public string[] Params; // 0x20
}

// --------------------------------------------------------------------
// NewMechaTalentConfig  [class]
// --------------------------------------------------------------------
public static class NewMechaTalentConfig // TypeDefIndex: 462
{
	// Fields
	private const int ParamCount = 4;
	private static readonly Dictionary<string, NewMechaTalentConfig.TalentConfig> Talents; // 0x0
	private static bool _loaded; // 0x8
}

// --------------------------------------------------------------------
// NewMechaTalents  [struct]
// --------------------------------------------------------------------
public struct NewMechaTalents.AutoRefreshCooldownState // TypeDefIndex: 463
{
	// Fields
	public static readonly NewMechaTalents.AutoRefreshCooldownState Hidden; // 0x0
	public readonly NewMechaCombatPilot Pilot; // 0x0
	public readonly bool Visible; // 0x8
	public readonly float Interval; // 0xC
	public readonly float Remaining; // 0x10
}

// --------------------------------------------------------------------
// NewMechaTalents  [struct]
// --------------------------------------------------------------------
private struct NewMechaTalents.NewMechaTalentContext // TypeDefIndex: 483
{
	// Fields
	public readonly float DeltaTime; // 0x0
	public readonly Mecha2GameModeProcess Process; // 0x8
	public readonly RGController Controller; // 0x10
	public readonly NewMechaCombatPilot Pilot; // 0x18
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.EngineOverheatTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase // TypeDefIndex: 485
{
	// Fields
	private const int DamageRatioParamIndex = 0;
	private const int RadiusParamIndex = 1;
	private const int DamageIntervalParamIndex = 2;
	private const float DefaultDamageRatio = 0.01;
	private const float DefaultRadius = 3;
	private const float DefaultDamageInterval = 1;
	private const string FireRingPrefabResourcePath = "Activities/Mecha2/Prefabs/Talent/engine_overheat_fire_ring.prefab";
	private readonly List<NewMechaEnemyInstance> _enemyBuffer; // 0x10
	private NewMechaCombatPilot _activePilot; // 0x18
	private Transform _followTarget; // 0x20
	private GameObject _fireRingInstance; // 0x28
	private float _damageRatio; // 0x30
	private float _baseRadius; // 0x34
	private float _damageInterval; // 0x38
	private float _damageTimer; // 0x3C
	private float _lastAppliedRadius; // 0x40
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.MechaCoreTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaAttackDamageDeltaPercentageTalentRuntime // TypeDefIndex: 486
{
	// Fields
	private const int AttackDamageBonusParamIndex = 0;
	private const float DefaultAttackDamageBonus = 0.3;
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.HumanHeartTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaAttackDamageDeltaPercentageTalentRuntime // TypeDefIndex: 487
{
	// Fields
	private const int AttackDamageBonusParamIndex = 0;
	private const float DefaultAttackDamageBonus = 0.3;
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.GlassCannonTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaAttackDamageDeltaPercentageTalentRuntime, NewMechaTalents.INewMechaMaxHpDeltaPercentageTalentRuntime // TypeDefIndex: 488
{
	// Fields
	private const int AttackDamageBonusParamIndex = 0;
	private const int MaxHpReductionParamIndex = 1;
	private const float DefaultAttackDamageBonus = 0.4;
	private const float DefaultMaxHpReduction = 0.3;
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.EquivalentExchangeTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaAttackDamageDeltaPercentageTalentRuntime // TypeDefIndex: 489
{
	// Fields
	private const int HumanDamageReductionParamIndex = 0;
	private const int MountedDamageBonusParamIndex = 1;
	private const float DefaultHumanDamageReduction = 0.4;
	private const float DefaultMountedDamageBonus = 0.4;
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.HeavyHitterTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaAttackDamageDeltaPercentageTalentRuntime, NewMechaTalents.INewMechaCooldownReductionDeltaFlatValueTalentRuntime // TypeDefIndex: 490
{
	// Fields
	private const int AttackDamageBonusParamIndex = 0;
	private const int CooldownReductionLossParamIndex = 1;
	private const float DefaultAttackDamageBonus = 0.5;
	private const float DefaultCooldownReductionLoss = 30;
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.HeavyMechaTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaMaxHpDeltaPercentageTalentRuntime, NewMechaTalents.INewMechaMoveSpeedDeltaPercentageTalentRuntime, NewMechaTalents.INewMechaBodyScaleMultiplierTalentRuntime // TypeDefIndex: 491
{
	// Fields
	private const int BodyScaleBonusParamIndex = 0;
	private const int MaxHpBonusParamIndex = 1;
	private const int MoveSpeedLossParamIndex = 2;
	private const float DefaultBodyScaleBonus = 0.3;
	private const float DefaultMaxHpBonus = 0.3;
	private const float DefaultMoveSpeedLoss = 0.15;
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.LightMechaTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaMaxHpDeltaPercentageTalentRuntime, NewMechaTalents.INewMechaMoveSpeedDeltaPercentageTalentRuntime, NewMechaTalents.INewMechaCooldownReductionDeltaFlatValueTalentRuntime, NewMechaTalents.INewMechaBodyScaleMultiplierTalentRuntime // TypeDefIndex: 492
{
	// Fields
	private const int BodyScaleReductionParamIndex = 0;
	private const int MaxHpReductionParamIndex = 1;
	private const int MoveSpeedBonusParamIndex = 2;
	private const int CooldownReductionBonusParamIndex = 3;
	private const float DefaultBodyScaleReduction = 0.3;
	private const float DefaultMaxHpReduction = 0.3;
	private const float DefaultMoveSpeedBonus = 0.3;
	private const float DefaultCooldownReductionBonus = 30;
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.UltimateSkillTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaMountedUltimateCooldownReductionBonusTalentRuntime // TypeDefIndex: 493
{
	// Fields
	private const int CooldownReductionBonusParamIndex = 0;
	private const float DefaultCooldownReductionBonus = 100;
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.SolidBioBatteryTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaMountedEnergyConsumeReductionTalentRuntime // TypeDefIndex: 494
{
	// Fields
	private const int EnergyConsumeReductionParamIndex = 0;
	private const float DefaultEnergyConsumeReduction = 0.5;
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.MechaSurgeTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaMountedFromHumanTalentRuntime // TypeDefIndex: 495
{
	// Fields
	private const int DamageRatioParamIndex = 0;
	private const float DefaultDamageRatio = 0.3;
	private const string ShockwaveBulletId = "mechaSurgeShockwave";
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.SuperGiantTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaBodyScaleMultiplierTalentRuntime, NewMechaTalents.INewMechaProjectileSizeDeltaPercentageTalentRuntime // TypeDefIndex: 496
{
	// Fields
	private const int ProjectileSizeBonusParamIndex = 0;
	private const int BodyScaleBonusParamIndex = 1;
	private const float DefaultProjectileSizeBonus = 0.2;
	private const float DefaultBodyScaleBonus = 0.2;
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.WorkaholicTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaAttackDamageDeltaPercentageTalentRuntime, NewMechaTalents.INewMechaMissionCompletedTalentRuntime // TypeDefIndex: 497
{
	// Fields
	private const int AttackDamageBonusPerMissionParamIndex = 0;
	private const float DefaultAttackDamageBonusPerMission = 0.05;
	private int _completedMissionCount; // 0x10
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.DisplacementMagicTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaDirectionalFastDisplacementCompletedTalentRuntime // TypeDefIndex: 498
{
	// Fields
	private const int DamageParamIndex = 0;
	private const int RadiusParamIndex = 1;
	private const int CooldownParamIndex = 2;
	private const int BulletIdParamIndex = 3;
	private const float DefaultDamage = 50;
	private const float DefaultRadius = 3;
	private const float DefaultCooldown = 0;
	private const string DefaultBulletId = "displacement_magic_explosion";
	private const int InfinitePenetrateCount = -1;
	private NewMechaCombatPilot _lastTriggerPilot; // 0x10
	private float _lastTriggerTime; // 0x18
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.HardArmorTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaMountedFromHumanTalentRuntime // TypeDefIndex: 499
{
	// Fields
	private const int DurationParamIndex = 0;
	private const float DefaultDuration = 5;
	private const string ShieldPrefabResourcePath = "Activities/Mecha2/Prefabs/Talent/invulnerable_shield.prefab";
	private static readonly Vector3 ShieldLocalPosition; // 0x0
	private NewMechaCombatPilot _activePilot; // 0x10
	private Transform _followTarget; // 0x18
	private GameObject _shieldInstance; // 0x20
	private float _duration; // 0x28
	private float _remainingTime; // 0x2C
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.KeyCriticalTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaCriticalRateDeltaFlatValueTalentRuntime, NewMechaTalents.INewMechaCriticalDamageBonusTalentRuntime, NewMechaTalents.INewMechaNonCriticalDamageReductionTalentRuntime // TypeDefIndex: 500
{
	// Fields
	private const int CriticalRateBonusParamIndex = 0;
	private const int CriticalDamageBonusParamIndex = 1;
	private const int NonCriticalDamageReductionParamIndex = 2;
	private const float DefaultCriticalRateBonus = 0.1;
	private const float DefaultCriticalDamageBonus = 0.25;
	private const float DefaultNonCriticalDamageReduction = 0.2;
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.MoreBulletsTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaProjectileCountDeltaFlatValueTalentRuntime, NewMechaTalents.INewMechaAttackDamageDeltaPercentageTalentRuntime // TypeDefIndex: 501
{
	// Fields
	private const int ProjectileCountBonusParamIndex = 0;
	private const int AttackDamageReductionParamIndex = 1;
	private const float DefaultProjectileCountBonus = 4;
	private const float DefaultAttackDamageReduction = 0.35;
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.MultiWeaponCarryTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaAdditionalWeaponMaxCountTalentRuntime // TypeDefIndex: 502
{
	// Fields
	private const int AdditionalWeaponMaxCountBonusParamIndex = 0;
	private const float DefaultAdditionalWeaponMaxCountBonus = 1;
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.OffRoadTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaAttackDamageDeltaPercentageTalentRuntime, NewMechaTalents.INewMechaMaxHpDeltaPercentageTalentRuntime // TypeDefIndex: 503
{
	// Fields
	private const int DistancePerStackParamIndex = 0;
	private const int AttackDamageBonusPerStackParamIndex = 1;
	private const int MaxHpBonusPerStackParamIndex = 2;
	private const float DefaultDistancePerStack = 1000;
	private const float DefaultAttackDamageBonusPerStack = 0.03;
	private const float DefaultMaxHpBonusPerStack = 0.03;
	private NewMechaCombatPilot _activePilot; // 0x10
	private Vector3 _lastPosition; // 0x18
	private bool _hasLastPosition; // 0x24
	private float _accumulatedDistance; // 0x28
	private int _stackCount; // 0x2C
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.MetabolismTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaMaxHpDeltaPercentageTalentRuntime // TypeDefIndex: 504
{
	// Fields
	private const int HealRatioParamIndex = 0;
	private const int MaxHpReductionParamIndex = 1;
	private const float DefaultHealRatio = 0.2;
	private const float DefaultMaxHpReduction = 0.6;
	private const float HealInterval = 1;
	private NewMechaCombatPilot _activePilot; // 0x10
	private float _healTimer; // 0x18
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.BioEnergyOverloadTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase, NewMechaTalents.INewMechaEnergyRecoveryBonusTalentRuntime, NewMechaTalents.INewMechaMaxEnergyMultiplierTalentRuntime // TypeDefIndex: 505
{
	// Fields
	private const int EnergyRecoveryBonusParamIndex = 0;
	private const int MaxEnergyMultiplierParamIndex = 1;
	private const float DefaultEnergyRecoveryBonus = 0.5;
	private const float DefaultMaxEnergyMultiplier = 0.3;
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.AutoRefreshTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase // TypeDefIndex: 506
{
	// Fields
	private const int IntervalParamIndex = 0;
	private const int RecoverPercentParamIndex = 1;
	private const float DefaultInterval = 60;
	private const float DefaultRecoverPercent = 100;
	private NewMechaCombatPilot _activePilot; // 0x10
	private float _timer; // 0x18
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.KillEnergyRecoveryTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase // TypeDefIndex: 507
{
	// Fields
	private const int ProbabilityParamIndex = 0;
	private const int RecoveryAmountParamIndex = 1;
	private const float DefaultProbability = 0.25;
	private const float DefaultRecoveryAmount = 1;
	private static RGRandom _fallbackRandom; // 0x0
	private bool _isListening; // 0x10
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.KillHealPackTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase // TypeDefIndex: 508
{
	// Fields
	private const string HealingBallPrefabPath = "Activities/Mecha2/Prefabs/healing_ball.prefab";
	private const int ProbabilityParamIndex = 0;
	private const int HealPercentParamIndex = 1;
	private const float DefaultProbability = 0.25;
	private static RGRandom _fallbackRandom; // 0x0
	private bool _isListening; // 0x10
	private GameObject _healingBallPrefab; // 0x18
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalents.CorpseExplosionTalentRuntime : NewMechaTalents.NewMechaTalentRuntimeBase // TypeDefIndex: 509
{
	// Fields
	private const int ProbabilityParamIndex = 0;
	private const int DamageParamIndex = 1;
	private const int RadiusParamIndex = 2;
	private const int BulletIdParamIndex = 3;
	private const float DefaultProbability = 0.25;
	private const float DefaultDamage = 10;
	private const float DefaultRadius = 2;
	private const string DefaultBulletId = "mecha2_corpse_explosion";
	private static RGRandom _fallbackRandom; // 0x0
	private bool _isListening; // 0x10
}

// --------------------------------------------------------------------
// NewMechaTalents  [class]
// --------------------------------------------------------------------
public static class NewMechaTalents // TypeDefIndex: 510
{
	// Fields
	public const string EngineOverheatTalentId = "engineOverheat";
	public const string MechaCoreTalentId = "mechaCore";
	public const string HumanHeartTalentId = "humanHeart";
	public const string GlassCannonTalentId = "glassCannon";
	public const string EquivalentExchangeTalentId = "equivalentExchange";
	public const string HeavyHitterTalentId = "heavyHitter";
	public const string HeavyMechaTalentId = "heavyMecha";
	public const string LightMechaTalentId = "lightMecha";
	public const string UltimateSkillTalentId = "ultimateSkill";
	public const string SolidBioBatteryTalentId = "solidBioBattery";
	public const string MechaSurgeTalentId = "mechaSurge";
	public const string SuperGiantTalentId = "superGiant";
	public const string WorkaholicTalentId = "workaholic";
	public const string HardArmorTalentId = "hardArmor";
	public const string KeyCriticalTalentId = "keyCritical";
	public const string MoreBulletsTalentId = "moreBullets";
	public const string MetabolismTalentId = "metabolism";
	public const string BioEnergyOverloadTalentId = "bioEnergyOverload";
	public const string KillEnergyRecoveryTalentId = "killEnergyRecovery";
	public const string KillHealPackTalentId = "killHealPack";
	public const string CorpseExplosionTalentId = "corpseExplosion";
	public const string MultiWeaponCarryTalentId = "multiWeaponCarry";
	public const string OffRoadTalentId = "offRoad";
	public const string AutoRefreshTalentId = "autoRefresh";
	public const string DisplacementMagicTalentId = "displacementMagic";
	private static readonly NewMechaTalents.INewMechaTalentRuntime[] TalentRuntimes; // 0x0
	private static NewMechaAttributeRuntime _lastAttackDamageTalentRuntime; // 0x8
	private static float _lastAttackDamageTalentDeltaPercentage; // 0x10
	private static NewMechaAttributeRuntime _lastMaxHpTalentRuntime; // 0x18
	private static float _lastMaxHpTalentDeltaPercentage; // 0x20
	private static NewMechaAttributeRuntime _lastCooldownReductionTalentRuntime; // 0x28
	private static float _lastCooldownReductionTalentDeltaFlatValue; // 0x30
	private static NewMechaAttributeRuntime _lastMoveSpeedTalentRuntime; // 0x38
	private static float _lastMoveSpeedTalentDeltaPercentage; // 0x40
	private static NewMechaAttributeRuntime _lastCriticalRateTalentRuntime; // 0x48
	private static float _lastCriticalRateTalentDeltaFlatValue; // 0x50
	private static NewMechaAttributeRuntime _lastProjectileCountTalentRuntime; // 0x58
	private static float _lastProjectileCountTalentDeltaFlatValue; // 0x60
	private static NewMechaAttributeRuntime _lastProjectileSizeTalentRuntime; // 0x68
	private static float _lastProjectileSizeTalentDeltaFlatValue; // 0x70
	private static Transform _lastBodyScaleTarget; // 0x78
	private static NewMechaTalents.AutoRefreshCooldownState _autoRefreshCooldownState; // 0x80
}

// --------------------------------------------------------------------
// NewMechaRoleAttributeSet  [class]
// --------------------------------------------------------------------
public sealed class NewMechaRoleAttributeSet // TypeDefIndex: 575
{
	// Fields
	private readonly Dictionary<NewMechaAttributeType, float> _values; // 0x10
}

// --------------------------------------------------------------------
// Mecha2TalentRewardRuntime  [struct]
// --------------------------------------------------------------------
private struct Mecha2TalentRewardRuntime.PendingExternalReward // TypeDefIndex: 582
{
	// Fields
	public RGController Controller; // 0x0
	public Mecha2GameModeProcess Process; // 0x8
	public RGRandom Random; // 0x10
	public NewMechaUpgradeCategoryMask CategoryMask; // 0x18
	public string RewardName; // 0x20
}

// --------------------------------------------------------------------
// Mecha2TalentRewardRuntime  [class]
// --------------------------------------------------------------------
public static class Mecha2TalentRewardRuntime // TypeDefIndex: 583
{
	// Fields
	private const int MaxOptionCount = 3;
	private static readonly Queue<Mecha2TalentRewardRuntime.PendingExternalReward> PendingRewards; // 0x0
	private static UIMecha2LevelReward _window; // 0x8
	private static RGController _currentController; // 0x10
	private static Mecha2GameModeProcess _currentProcess; // 0x18
	private static bool _registered; // 0x20
	private static bool _missionResultUiHiddenRegistered; // 0x21
	private static bool _showing; // 0x22
	private static bool _pausedByReward; // 0x23
	private static RGRandom _fallbackRandom; // 0x28
	private static Action RewardFlowIdle; // 0x30
}

// --------------------------------------------------------------------
// NewMechaTalentBar  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalentBar.SlotView // TypeDefIndex: 604
{
	// Fields
	private readonly Transform _slotTransform; // 0x10
	private readonly GameObject _iconObject; // 0x18
	private readonly GameObject _frontObject; // 0x20
	private readonly Image _iconImage; // 0x28
	private readonly Sprite _defaultIconSprite; // 0x30
}

// --------------------------------------------------------------------
// NewMechaTalentBar  [class]
// --------------------------------------------------------------------
private sealed class NewMechaTalentBar.<>c // TypeDefIndex: 605
{
	// Fields
	public static readonly NewMechaTalentBar.<>c <>9; // 0x0
	public static Comparison<KeyValuePair<string, int>> <>9__33_0; // 0x8
}

// --------------------------------------------------------------------
// NewMechaTalentBar  [class]
// --------------------------------------------------------------------
public class NewMechaTalentBar : MonoBehaviour // TypeDefIndex: 606
{
	// Fields
	public const string TalentBarPrefabAssetPath = "Assets/Activities/Mecha2/Prefabs/UI/TalentBar.prefab";
	public const string TalentBarPrefabResourcePath = "Activities/Mecha2/Prefabs/UI/TalentBar.prefab";
	private const string TalentSlotsRootName = "TalentSlots";
	private const string TalentSlotNamePrefix = "TalentSlot_";
	private const string TalentIconName = "TalentIcon";
	private const string TalentFrontName = "Front";
	private const string SceneName = "TalentBar";
	private const string TalentRowNamePrefix = "TalentSlotsRow_";
	private const int SlotsPerRow = 5;
	private readonly List<NewMechaTalentBar.SlotView> _slots; // 0x20
	private readonly List<Transform> _slotRows; // 0x28
	private MechaPlayerData _playerData; // 0x30
	private MechaPlayerData _subscribedPlayerData; // 0x38
	private Transform _slotsRoot; // 0x40
	private Transform _slotTemplate; // 0x48
}

// --------------------------------------------------------------------
// EYaoqiWitch  [class]
// --------------------------------------------------------------------
public class EYaoqiWitch : RGEController // TypeDefIndex: 696
{
	// Fields
	public float functionInterval; // 0x488
	private float _cooldown; // 0x48C
}

// --------------------------------------------------------------------
// SummonOwlTalentRuntime  [class]
// --------------------------------------------------------------------
public class SummonOwlTalentRuntime : MonoBehaviour // TypeDefIndex: 1141
{
	// Fields
	private const string OwlPrefabPath = "RGPrefab/Pet/pet_beheaded_owl.prefab";
	private const float DefaultHideDuration = 3;
	private RGController _controller; // 0x20
	private BeheadedOwlController _owl; // 0x28
	private bool _listeningHurtHpEvent; // 0x30
}

// --------------------------------------------------------------------
// BulletGasBuffBase  [class]
// --------------------------------------------------------------------
private sealed class BulletGasBuffBase.<DelayBeforeEnd>d__21 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 1318
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public BulletGasBuffBase <>4__this; // 0x20
}

// --------------------------------------------------------------------
// BulletGasBuffBase  [class]
// --------------------------------------------------------------------
public class BulletGasBuffBase : RGBuff, OffensiveInterface // TypeDefIndex: 1319
{
	// Fields
	public float buffLeftTime; // 0xC0
	public float damageInterval; // 0xC4
	private float _damageElapsedTime; // 0xC8
	public int damage; // 0xCC
	private int _extraDamage; // 0xD0
	public bool canHurt; // 0xD4
	public Color poisonColor; // 0xD8
	public float poisoningTime; // 0xE8
	public bool isDestroying; // 0xEC
	public List<string> master_gas_ids; // 0xF0
	public Material poisonMaterial; // 0xF8
	private Material instancePoisonMaterial; // 0x100
	private List<SpriteRenderer> bodyRenderers; // 0x108
	protected GameObject source_object; // 0x110
	private RGEController enemyCtrl; // 0x118
	private int i; // 0x120
}

// --------------------------------------------------------------------
// BulletInfo  [enum]
// --------------------------------------------------------------------
public enum BulletInfo.emBulletTag // TypeDefIndex: 1493
{
	// Fields
	public int value__; // 0x518
	public const BulletInfo.emBulletTag None = 0;
	public const BulletInfo.emBulletTag HurtTeammateZero = 1;
	public const BulletInfo.emBulletTag CanReboundByTeammateSword = 2;
	public const BulletInfo.emBulletTag CanReboundTeammateBullet = 3;
	public const BulletInfo.emBulletTag CantSplitBySkill = 4;
	public const BulletInfo.emBulletTag CanReboundBySword = 5;
	public const BulletInfo.emBulletTag CanReboundLaserBySword = 6;
	public const BulletInfo.emBulletTag NoReboundBySword = 7;
	public const BulletInfo.emBulletTag NoModifySpeed = 8;
	public const BulletInfo.emBulletTag HurtTeammate = 9;
}

// --------------------------------------------------------------------
// BulletInfo  [struct]
// --------------------------------------------------------------------
public struct BulletInfo // TypeDefIndex: 1494
{
	// Fields
	public GameObject bulletProtoBeforeSplit; // 0x560
	public GameObject bulletProto; // 0x568
	public GameObject sourceObject; // 0x570
	public RGWeapon sourceWeapon; // 0x574
	public string sourceWeaponName; // 0x578
	public Vector2 createPosition; // 0x57C
	public Transform targetObj; // 0x580
	public Vector2 initVelocity; // 0xB0
	public float speed; // 0xB8
	public float size; // 0xC0
	public float directionAngle; // 0xC8
	public int reflectCount; // 0xD0
	public int extraReflectCount; // 0xD8
	public float duration; // 0x0
	public int camp; // 0x8
	public float effectDeltaSize; // 0xE0
	public float subEffectDeltaSize; // 0xE8
	public object customData; // 0xEC
	public string customTag; // 0xF0
	public List<int> ignoreGetHurtGameObjects; // 0x10
	public bool isCritical; // 0x14
	public bool isPenetrate; // 0x18
	public int damage; // 0x20
	public IBulletEmitter.EmitInfo emitInfo; // 0x0
	public IBulletEmitter sourceBulletEmitter; // 0x8
	public HashSet<BulletInfo.emBulletTag> bulletTags; // 0x0
	public float resultModifiedDamageFactor; // 0x10
	public BulletInfoInspector bulletInfoInspector; // 0x18
	public int effectGeneration; // 0x1C
	public ActionInfo actionInfo; // 0x1D
	public Dictionary<string, float> CustomDict; // 0x20
	public float chargeAmount; // 0x28
	public WeaponChargeState chargeState; // 0x30
	public int endAttackSequence; // 0x38
	public int attackSequence; // 0x0
	public int siblings; // 0x8
	public int siblingIdx; // 0x0
	private BattleData _battleData; // 0x8
	private GameObject _sourceObject; // 0x0
}

// --------------------------------------------------------------------
// DamageInfo  [struct]
// --------------------------------------------------------------------
public struct DamageInfo // TypeDefIndex: 1495
{
	// Fields
	public emDamageType damageType; // 0x10
	public DamageType newDamageType; // 0x0
	public DamageSubtype newDamageSubtype; // 0x8
	public int damage; // 0x10
	public int throughCount; // 0x0
	public int critic; // 0xFFFFFFF8
	public float criticFactor; // 0x0
	public int repel; // 0x8
	public float damageInterval; // 0x0
	public int camp; // 0x8
}

// --------------------------------------------------------------------
// BulletInfoInspector  [enum]
// --------------------------------------------------------------------
public enum BulletInfoInspector.AttachOwnerMode // TypeDefIndex: 1498
{
	// Fields
	public int value__; // 0x3C
	public const BulletInfoInspector.AttachOwnerMode None = 0;
	public const BulletInfoInspector.AttachOwnerMode Root = 1;
	public const BulletInfoInspector.AttachOwnerMode Body = 2;
}

// --------------------------------------------------------------------
// BulletInfoInspector  [class]
// --------------------------------------------------------------------
public class BulletInfoInspector : ICloneable // TypeDefIndex: 1499
{
	// Fields
	public GameObject bulletProto; // 0xC4
	public float speed; // 0xC8
	public float size; // 0xCC
	public int damage; // 0xD0
	public int critic; // 0xD4
	public int repel; // 0xD8
	public int throughCount; // 0xDC
	public bool useGunpointAngle; // 0xE0
	public BulletInfoInspector.AttachOwnerMode attachOwnerMode; // 0xE4
	public List<BulletInfo.emBulletTag> bulletTags; // 0xE8
}

// --------------------------------------------------------------------
// emDamageType  [enum]
// --------------------------------------------------------------------
public enum emDamageType // TypeDefIndex: 1582
{
	// Fields
	public int value__; // 0x10
	public const emDamageType None = 0;
	public const emDamageType Physics = 2;
	public const emDamageType Magic = 4;
	public const emDamageType Laser = 8;
	public const emDamageType Fire = 16;
	public const emDamageType Explode = 32;
	public const emDamageType Ice = 64;
	public const emDamageType Gas = 128;
	public const emDamageType Energy = 256;
	public const emDamageType Metal = 512;
	public const emDamageType Arrow = 1024;
	public const emDamageType Electric = 2048;
	public const emDamageType Throw = 4096;
	public const emDamageType Create = 8192;
	public const emDamageType Divide = 16384;
	public const emDamageType MultiTrigger = 32768;
	public const emDamageType Upgradable = 65536;
	public const emDamageType Melle = 131072;
	public const emDamageType Dart = 262144;
	public const emDamageType MelleAngle = 524288;
	public const emDamageType IgnoreBox = 1048576;
	public const emDamageType IgnoreWall = 2097152;
	public const emDamageType LastingRange = 4194304;
	public const emDamageType Enemy = 8388608;
	public const emDamageType Transform = 16777216;
	public const emDamageType SuddenKill = 33554432;
	public const emDamageType Skill = 67108864;
	public const emDamageType Teleport = 134217728;
	public const emDamageType HandCut = 268435456;
	public const emDamageType OnlyIgnoreWall = 536870912;
}

// --------------------------------------------------------------------
// RGEController  [class]
// --------------------------------------------------------------------
public class RGEController.DefaultDeadBodyActionState : RGEController.IDeadBodyActionState // TypeDefIndex: 2166
{
	// Fields
	public float wait; // 0x10
}

// --------------------------------------------------------------------
// RGEController  [class]
// --------------------------------------------------------------------
public class RGEController.ActionCoolDown.Data // TypeDefIndex: 2167
{
	// Fields
	public float cooldown; // 0x10
	private float <remain>k__BackingField; // 0x14
	private int <actionTimes>k__BackingField; // 0x18
	public float silenceAfterAction; // 0x1C
	public float latestActionTime; // 0x20
}

// --------------------------------------------------------------------
// RGEController  [class]
// --------------------------------------------------------------------
private sealed class RGEController.<>c // TypeDefIndex: 2169
{
	// Fields
	public static readonly RGEController.<>c <>9; // 0x0
	public static Func<SpriteRenderer, bool> <>9__491_0; // 0x8
}

// --------------------------------------------------------------------
// RGEController  [class]
// --------------------------------------------------------------------
private sealed class RGEController.<>c__430<T> // TypeDefIndex: 2170
{
	// Fields
	public static readonly RGEController.<>c__430<T> <>9; // 0x0
	public static Func<RGBuff, bool> <>9__430_0; // 0x0
}

// --------------------------------------------------------------------
// RGEController  [class]
// --------------------------------------------------------------------
private sealed class RGEController.<>c__DisplayClass365_0 // TypeDefIndex: 2171
{
	// Fields
	public HurtInfo hurtInfo; // 0x10
	public int actualDamage; // 0x60
	public RGEController <>4__this; // 0x68
	public bool die; // 0x70
	public GameObject source_object; // 0x78
}

// --------------------------------------------------------------------
// RGEController  [class]
// --------------------------------------------------------------------
private sealed class RGEController.<>c__DisplayClass395_0 // TypeDefIndex: 2172
{
	// Fields
	public RGEController <>4__this; // 0x10
	public RGBaseController eater; // 0x18
}

// --------------------------------------------------------------------
// RGEController  [class]
// --------------------------------------------------------------------
private sealed class RGEController.<>c__DisplayClass431_0 // TypeDefIndex: 2173
{
	// Fields
	public Rigidbody2D rigidBody; // 0x10
}

// --------------------------------------------------------------------
// RGEController  [class]
// --------------------------------------------------------------------
private sealed class RGEController.<>c__DisplayClass499_0 // TypeDefIndex: 2174
{
	// Fields
	public RGEController <>4__this; // 0x10
	public Vector3 pos; // 0x18
	public Action<float> move; // 0x28
	public Action finished; // 0x30
}

// --------------------------------------------------------------------
// RGEController  [class]
// --------------------------------------------------------------------
private sealed class RGEController.<CheckPositionValid>d__468 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 2175
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RGEController <>4__this; // 0x20
	private WaitForSeconds <wait>5__2; // 0x28
	private RGRoomX <room>5__3; // 0x30
}

// --------------------------------------------------------------------
// RGEController  [class]
// --------------------------------------------------------------------
private sealed class RGEController.<DizzyScout>d__308 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 2176
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RGEController <>4__this; // 0x20
}

// --------------------------------------------------------------------
// RGEController  [class]
// --------------------------------------------------------------------
private sealed class RGEController.<DoDelayFade>d__500 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 2177
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RGEController <>4__this; // 0x20
	public bool fade; // 0x28
}

// --------------------------------------------------------------------
// RGEController  [class]
// --------------------------------------------------------------------
private sealed class RGEController.<SendingPosition>d__471 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 2178
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RGEController <>4__this; // 0x20
	private WaitForSeconds <wait>5__2; // 0x28
}

// --------------------------------------------------------------------
// RGEController  [class]
// --------------------------------------------------------------------
public class RGEController : RandomBehaviour, ILockableObject, IUnit, IHitable, IRoomObject // TypeDefIndex: 2179
{
	// Fields
	public object summoner; // 0x28
	public RGEController noumenon; // 0x30
	public bool useAIController; // 0x38
	public bool needShowDamage; // 0x39
	private AIController <AIController>k__BackingField; // 0x40
	protected RaycastHit2D[] detect_list; // 0x48
	public bool awake; // 0x50
	public bool temp_enemy; // 0x51
	public bool start_awake; // 0x52
	public emLevelScene levelScene; // 0x54
	public emEnemyTag enemyTag; // 0x58
	public emDamageType immuneEffectType; // 0x5C
	public emEnemyTagRunTime enemyTagRunTime; // 0x60
	public bool destory_on_dead; // 0x64
	public bool intensive; // 0x65
	public GameObject exEnemy; // 0x68
	private GameObject <originPrefab>k__BackingField; // 0x70
	public bool weapon_lock_target; // 0x78
	public string enemy_id; // 0x80
	public string enemy_statistic_id; // 0x88
	public bool customAI; // 0x90
	public PlayMakerFSM fsm; // 0x98
	public bool onCar; // 0xA0
	public int ai_level; // 0xA4
	public int consume; // 0xA8
	public int e_size; // 0xAC
	public int reward_rate; // 0xB0
	public int[] reward_value; // 0xB8
	internal bool dead; // 0xC0
	public float shoot_cd; // 0xC4
	public float shoot_prob; // 0xC8
	public bool random_shoot; // 0xCC
	protected bool _can_shoot; // 0xCD
	private bool <can_move>k__BackingField; // 0xCE
	public bool canGetForce; // 0xCF
	public float friction; // 0xD0
	public int facing; // 0xD4
	public bool kinematic; // 0xD8
	public int findTargetRange; // 0xDC
	private bool <ignoreObstacleWhenTargeting>k__BackingField; // 0xE0
	public Color shadowColor; // 0xE4
	public bool lowPriorityForPetTarget; // 0xF4
	private bool <NotPartOfWave>k__BackingField; // 0xF5
	private bool <DontSortingLayerWhenDie>k__BackingField; // 0xF6
	private bool <UnnaturalDead>k__BackingField; // 0xF7
	private HurtInfo <DeadHurtInfo>k__BackingField; // 0xF8
	public AudioClip clip_dead; // 0x148
	public BuffMgr buffMgr; // 0x150
	public bool autoChanageMaterial; // 0x158
	public bool autoTeleportIntoRoom; // 0x159
	public bool autoStartAI; // 0x15A
	public bool disableReplaceMat; // 0x15B
	public bool lockable; // 0x15C
	public bool move_default; // 0x15D
	public int enemy_level; // 0x160
	public int wave_index; // 0x164
	public int DefenceRewardValue; // 0x168
	public string enemyName; // 0x170
	public bool freeInPart; // 0x178
	private bool _isMutated; // 0x179
	private bool _isCharmed; // 0x17A
	internal float dead_destory_delay_time; // 0x17C
	protected Animator anim; // 0x180
	public Rigidbody2D rigibody; // 0x188
	protected Collider2D _bodyCol; // 0x190
	protected SpriteRenderer _body_renderer; // 0x198
	protected Transform transform_img; // 0x1A0
	protected Transform hand_tf; // 0x1A8
	public RGEHand hand; // 0x1B0
	private RoleAttributeProxy _attributeProxy; // 0x1B8
	public RoleAttribute role_attribute; // 0x1C0
	protected Vector2 _move_direction; // 0x1C8
	protected Vector2 _default_direction; // 0x1D0
	public UIHpBar HpBar; // 0x1D8
	internal bool shooting; // 0x1E0
	internal float fixed_angle; // 0x1E4
	protected float min_distance; // 0x1E8
	public string modeID; // 0x1F0
	public object modeCustomData; // 0x1F8
	private Transform _target_obj; // 0x200
	public Transform HatredTarget; // 0x208
	private Transform _fixedTarget; // 0x210
	protected Vector2 target_point; // 0x218
	private EnemyMaker _theMaker; // 0x220
	private float scoutRateConfig; // 0x228
	public float fixedScoutRate; // 0x22C
	protected SpriteRenderer shadow_lock; // 0x230
	public bool is_lock; // 0x238
	public float buff_immune_factor; // 0x23C
	private float _buff_immune; // 0x240
	public bool immune_to_dizzy; // 0x244
	protected float seach_time; // 0x248
	internal int netId; // 0x24C
	public bool ImmunSting; // 0x250
	public bool invincible_mode; // 0x251
	public bool can_award; // 0x252
	protected static string dead_sorting_layer_name; // 0x0
	private Transform _defaultTarget; // 0x258
	protected int _atkAddition; // 0x260
	public bool isDemonstrationCharacter; // 0x264
	private ObjectZPos _objZPos; // 0x268
	private bool <CanDropItem>k__BackingField; // 0x270
	public List<GameObject> mustDropItems; // 0x278
	public Action OnEnemyDestroy; // 0x280
	private Action<HurtInfo> onEnemyHurted; // 0x288
	private Action afterEnemyGetHurt; // 0x290
	private Action<HurtInfo> AfterEnemyGotHurt; // 0x298
	public Action<HurtInfo> afterEnemyGetHurtWithInfo; // 0x2A0
	private Action onEnemyAttack; // 0x2A8
	private Action<RGEController> onSetupAttribute; // 0x2B0
	public Action<Action> onBeforeEnemyDead; // 0x2B8
	private Action<RGEController> onEnemyDead; // 0x2C0
	private Action<RGEController> OnEnemyEaten; // 0x2C8
	private Action<int> onGameStateChanged; // 0x2D0
	private Action<IUnit> onUnitDead; // 0x2D8
	private Action<RGEController> onEnemyDeadAnim; // 0x2E0
	protected List<Func<GameObject, HurtInfo, bool, int>> addHurtCallbacks; // 0x2E8
	private Action<RGEController> onReborn; // 0x2F0
	public RGEController.DeadCallback DeadEvents; // 0x2F8
	public readonly HashSet<string> customFlagSet; // 0x300
	private Action<float> customUpdateEvent; // 0x308
	private static Material _sample_material; // 0x8
	private const string BodyRendererPath = "img/body";
	private const string BodyColliderPath = "collider";
	protected Vector2 additionVelocity; // 0x310
	protected Vector2 fixedVelocity; // 0x318
	public int fixedDamage; // 0x320
	protected RGEController.IDeadBodyActionState _deadBodyActionState; // 0x328
	public AttributeValue damageFactor; // 0x330
	public AttributeValue speedFactor; // 0x338
	private int scoutMask; // 0x340
	private Action onScoutStart; // 0x348
	private Action onScoutEnd; // 0x350
	public bool enable_linecast; // 0x358
	private readonly RaycastHit2D[] _linecastCache; // 0x360
	public EnemyMoveType MoveType; // 0x368
	public EnemyTargetingType TargetingType; // 0x36C
	private Action onStopMove; // 0x370
	public Action<RGEController> OnDeadBeforeMakerTriggering; // 0x378
	private Transform <damageDisplayTransform>k__BackingField; // 0x380
	private bool _canGetHurt; // 0x388
	public float hurtCd; // 0x38C
	protected Timer HurtCdTimer; // 0x390
	private bool _hasRaiseKillHurtEvent; // 0x398
	protected MovementInput MoveInput; // 0x39C
	protected CircleCollider2D _bodyCollider; // 0x3D0
	public bool stopAutoAiming; // 0x3D8
	public Func<bool> onFixedRotation; // 0x3E0
	public GameObject killer; // 0x3E8
	internal bool justDead; // 0x3F0
	public bool reborned; // 0x3F1
	private Action BeforeShowDeadAnimation; // 0x3F8
	private Transform <rewardDropPosition>k__BackingField; // 0x400
	private Vector3 <itemDropWorldPosition>k__BackingField; // 0x408
	public Vector2 dropOffset; // 0x414
	private static readonly string[] buff_list; // 0x10
	public Vector2 colliderCenterOffset; // 0x41C
	private PositionProvider _positionProvider; // 0x428
	public RGRoomX enemyRoom; // 0x430
	protected bool can_hit; // 0x438
	public float HpBarOffsetFactor; // 0x43C
	public Vector3 hpBarOffset; // 0x440
	private float destroy_alpha; // 0x44C
	public bool destroy_fade_out; // 0x450
	private HashSet<string> shadowSet; // 0x458
	protected RGEController.ActionCoolDown actionCooldowns; // 0x460
	private static readonly int Run; // 0x18
	private static readonly int Atk; // 0x1C
	protected bool _isGameObjectDestroyed; // 0x468
	private RoomGrid <OwnerRoomGrid>k__BackingField; // 0x470
	public ObjectGridSize gridSize; // 0x478
	public ObjectLayoutOffset layoutOffset; // 0x480
}

// --------------------------------------------------------------------
// DamageType  [enum]
// --------------------------------------------------------------------
public enum DamageType // TypeDefIndex: 2192
{
	// Fields
	public int value__; // 0x0
	public const DamageType None = 0;
	public const DamageType Melee = 1;
	public const DamageType Ranged = 2;
	public const DamageType Weapon = 4;
	public const DamageType Skill = 8;
	public const DamageType Pet = 16;
	public const DamageType Periodic = 32;
	public const DamageType Proc = 64;
	public const DamageType Aoe = 128;
	public const DamageType ActIce = 256;
	public const DamageType ActFire = 512;
	public const DamageType ActElectric = 1024;
	public const DamageType ActPoison = 2048;
	public const DamageType CanHitFlyEnemy = 4096;
}

// --------------------------------------------------------------------
// emRoleAttribute  [enum]
// --------------------------------------------------------------------
public enum emRoleAttribute // TypeDefIndex: 2638
{
	// Fields
	public int value__; // 0x0
	public const emRoleAttribute MaxHp = 0;
	public const emRoleAttribute MaxEnergy = 1;
	public const emRoleAttribute MaxArmor = 2;
	public const emRoleAttribute Critic = 3;
	public const emRoleAttribute Defence = 4;
	public const emRoleAttribute MoveSpeed = 5;
	public const emRoleAttribute AttackSpeed = 6;
	public const emRoleAttribute Deviation = 7;
	public const emRoleAttribute ExtAddEnergy = 8;
	public const emRoleAttribute SkillHaste = 9;
	public const emRoleAttribute BulletSpeedRatio = 10;
	public const emRoleAttribute MeleeDamage = 11;
	public const emRoleAttribute RangedDamage = 12;
	public const emRoleAttribute WeaponDamageFactor = 13;
	public const emRoleAttribute SkillLevel = 14;
	public const emRoleAttribute ElementalDamage = 15;
	public const emRoleAttribute AoeRange = 16;
	public const emRoleAttribute Lifesteal = 17;
	public const emRoleAttribute EnergyRegen = 18;
	public const emRoleAttribute Salary = 19;
	public const emRoleAttribute Luck = 20;
	public const emRoleAttribute PetPower = 21;
	public const emRoleAttribute HealthRegen = 22;
	public const emRoleAttribute CriticDamage = 23;
	public const emRoleAttribute SkillDamageFactor = 24;
	public const emRoleAttribute AttackDamageFactor = 25;
	public const emRoleAttribute SkillRechargeFactor = 26;
	public const emRoleAttribute WeaponDamage = 27;
	public const emRoleAttribute SkillRealHaste = 28;
	public const emRoleAttribute DefenceLevel = 29;
}

// --------------------------------------------------------------------
// RGBaseController  [struct]
// --------------------------------------------------------------------
private struct RGBaseController.RendererAlpha // TypeDefIndex: 2727
{
	// Fields
	public int tag; // 0x0
	public float value; // 0x4
}

// --------------------------------------------------------------------
// RGBaseController  [struct]
// --------------------------------------------------------------------
public struct RGBaseController.FindLandingPosResult // TypeDefIndex: 2728
{
	// Fields
	public bool Success; // 0x0
	public Vector3 LandingPos; // 0x4
}

// --------------------------------------------------------------------
// RGBaseController  [struct]
// --------------------------------------------------------------------
public struct RGBaseController.DetectSurroundingObjectsResult // TypeDefIndex: 2729
{
	// Fields
	public int HitCount; // 0x0
	public RaycastHit2D[] RaycastHits; // 0x8
}

// --------------------------------------------------------------------
// RGBaseController  [class]
// --------------------------------------------------------------------
private sealed class RGBaseController.<>c // TypeDefIndex: 2730
{
	// Fields
	public static readonly RGBaseController.<>c <>9; // 0x0
	public static Func<RGBaseController.RendererAlpha, float> <>9__160_1; // 0x8
	public static Func<SpriteRenderer, bool> <>9__160_2; // 0x10
}

// --------------------------------------------------------------------
// RGBaseController  [class]
// --------------------------------------------------------------------
private sealed class RGBaseController.<>c__DisplayClass160_0 // TypeDefIndex: 2731
{
	// Fields
	public int tag; // 0x10
}

// --------------------------------------------------------------------
// RGBaseController  [class]
// --------------------------------------------------------------------
public class RGBaseController : MonoBehaviour, IUnit // TypeDefIndex: 2732
{
	// Fields
	protected static readonly int AnimatorParameterRun; // 0x0
	protected static readonly int AnimatorParameterHideHand; // 0x4
	public bool awake; // 0x20
	public bool dead; // 0x21
	public int p_index; // 0x24
	public Animator anim; // 0x28
	public Rigidbody2D rigibody; // 0x30
	public Transform target_obj; // 0x38
	protected Collider2D _targetCollider; // 0x40
	private float _fixedAngle; // 0x48
	protected Transform transform_img; // 0x50
	protected SpriteRenderer bodyRenderer; // 0x58
	internal float holdSpeedFactor; // 0x60
	internal List<BuffShield> oneshot_shields; // 0x68
	private float <DefaultSpeed>k__BackingField; // 0x70
	private float <DefaultSpeedWithRate>k__BackingField; // 0x74
	private Action<MovementInput> onRoleMove; // 0x78
	private Action<MovementInput> onRoleMoveIgnoreAttrDisable; // 0x80
	public Vector2 latestMoveDir; // 0x88
	public Vector2 latestAimDir; // 0x90
	public float findTargetRange; // 0x98
	public RGRandom rg_random; // 0xA0
	public RGHand hand; // 0xA8
	public BuffMgr buffMgr; // 0xB0
	private Vector2 <move_dir>k__BackingField; // 0xB8
	private bool _weaponSelfForceActive; // 0xC0
	protected Vector2 additionVelocity; // 0xC4
	protected Vector2 fixedVelocity; // 0xCC
	protected string BodyRendererPath; // 0xD8
	private Vector2 <FixedDirection>k__BackingField; // 0xE0
	public float lockBulletAngle; // 0xE8
	public bool isLockBulletAngle; // 0xEC
	private readonly FloatProperty _frictionProperty; // 0xF0
	protected readonly FloatProperty extraSpeedFactor; // 0xF8
	public bool isPassive; // 0x100
	public bool movable; // 0x101
	public bool isFly; // 0x102
	public static Quaternion RotLeft; // 0x8
	public static Quaternion RotRight; // 0x18
	public bool keepFacing; // 0x103
	private int <facing>k__BackingField; // 0x104
	private bool <has_target>k__BackingField; // 0x108
	public float forceLerp; // 0x10C
	protected RoleAttributeProxy _attributeProxy; // 0x110
	private static Material _sample_material; // 0x28
	private static Material _hit_material; // 0x30
	public Action<bool> OnChangeRunState; // 0x118
	private static RaycastHit2D[] _hitCache; // 0x38
	protected readonly RaycastHit2D[] _linecastCache; // 0x120
	private readonly string[] buff_list; // 0x128
	private Action onColorBack; // 0x130
	protected readonly HashSet<SpriteRenderer> _exclusiveChangeColorRenderer; // 0x138
	protected readonly Dictionary<SpriteRenderer, float> _customAlphaDic; // 0x140
	public float fadeDurTime; // 0x148
	private List<RGBaseController.RendererAlpha> _bodyRendererAlphaRecords; // 0x150
	private Sequence _bodyRendererDOFadeSeq; // 0x158
	private readonly HashSet<object> _ignoreMapMoveEffectTags; // 0x160
	private Action<GameObject> onBulletCreate; // 0x168
	private Action<GameObject> onDelayBulletRelease; // 0x170
	private ColliderOffsetHandler _colliderOffsetHandler; // 0x178
	private ColliderOffsetHandler _touchColliderOffsetHandler; // 0x180
	private static Vector3[] _colliderDetectOffset; // 0x40
	private static RaycastHit2D[] _cachedRaycastHit; // 0x48
	private static int _cachedRaycastHitCount; // 0x50
	private HashSet<string> shadowSet; // 0x188
	public Color shadowColor; // 0x190
	private HashSet<string> shadowSet_SpriteShader; // 0x1A0
	protected bool _isGameObjectDestroyed; // 0x1A8
	private CircleCollider2D _bodyCollider; // 0x1B0
	protected MovementInput movementInput; // 0x1B8
	private bool <ignoreMoveState>k__BackingField; // 0x1E8
	protected MovementInput preMoveInput; // 0x1EC
	private bool <SimpleMoving>k__BackingField; // 0x21C
	protected float processSpeedFactor; // 0x220
	public bool disableProcessSpeedFactor; // 0x224
}

// --------------------------------------------------------------------
// RGController  [class]
// --------------------------------------------------------------------
public class RGController.KillSomeOneReturnData // TypeDefIndex: 2738
{
	// Fields
	public bool triggerRestoreHealth; // 0x10
	public bool triggerRestoreEnergy; // 0x11
}

// --------------------------------------------------------------------
// RGController  [enum]
// --------------------------------------------------------------------
public enum RGController.RebornType // TypeDefIndex: 2739
{
	// Fields
	public int value__; // 0x0
	public const RGController.RebornType Default = 0;
	public const RGController.RebornType GameMachanism = 1;
	public const RGController.RebornType Redemption = 2;
	public const RGController.RebornType AdTwice = 3;
	public const RGController.RebornType NewPlayerReborn = 4;
	public const RGController.RebornType PriestSkillAuto = 5;
	public const RGController.RebornType ReturnPlayerReborn = 6;
}

// --------------------------------------------------------------------
// RGController  [class]
// --------------------------------------------------------------------
private sealed class RGController.<>c // TypeDefIndex: 2740
{
	// Fields
	public static readonly RGController.<>c <>9; // 0x0
	public static Func<RGBuff, bool> <>9__276_0; // 0x8
	public static Func<RGBuff, bool> <>9__276_1; // 0x10
}

// --------------------------------------------------------------------
// RGController  [class]
// --------------------------------------------------------------------
private sealed class RGController.<>c__DisplayClass223_0 // TypeDefIndex: 2741
{
	// Fields
	public RGController <>4__this; // 0x10
	public Action onCompleted; // 0x18
}

// --------------------------------------------------------------------
// RGController  [class]
// --------------------------------------------------------------------
private sealed class RGController.<>c__DisplayClass226_0 // TypeDefIndex: 2742
{
	// Fields
	public RGWeapon[] createdWeapons; // 0x10
	public RGController <>4__this; // 0x18
	public List<BattleData.WeaponInBattle> weapons; // 0x20
}

// --------------------------------------------------------------------
// RGController  [class]
// --------------------------------------------------------------------
private sealed class RGController.<>c__DisplayClass268_0 // TypeDefIndex: 2743
{
	// Fields
	public RGController <>4__this; // 0x10
	public int restoreEnergy; // 0x18
}

// --------------------------------------------------------------------
// RGController  [class]
// --------------------------------------------------------------------
private sealed class RGController.<>c__DisplayClass277_0 // TypeDefIndex: 2744
{
	// Fields
	public string sourceObjName; // 0x10
}

// --------------------------------------------------------------------
// RGController  [class]
// --------------------------------------------------------------------
private sealed class RGController.<>c__DisplayClass524_0 // TypeDefIndex: 2745
{
	// Fields
	public RGController <>4__this; // 0x10
	public Transform value; // 0x18
	public Sprite sprite; // 0x20
}

// --------------------------------------------------------------------
// RGController  [class]
// --------------------------------------------------------------------
private sealed class RGController.<>c__DisplayClass528_0 // TypeDefIndex: 2746
{
	// Fields
	public Transform trans; // 0x10
	public Sprite specifiedSprite; // 0x18
	public RGController <>4__this; // 0x20
}

// --------------------------------------------------------------------
// RGController  [class]
// --------------------------------------------------------------------
private sealed class RGController.<>c__DisplayClass587_0 // TypeDefIndex: 2747
{
	// Fields
	public RGController <>4__this; // 0x10
	public float t; // 0x18
	public Transform bodyTransform; // 0x20
}

// --------------------------------------------------------------------
// RGController  [class]
// --------------------------------------------------------------------
private sealed class RGController.<BuffChangingHoldSpeed>d__539 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 2748
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RGController <>4__this; // 0x20
	public float factor; // 0x28
	public float duration; // 0x2C
}

// --------------------------------------------------------------------
// RGController  [class]
// --------------------------------------------------------------------
private sealed class RGController.<ReEnableWeapon>d__719 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 2749
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RGController <>4__this; // 0x20
}

// --------------------------------------------------------------------
// RGController  [class]
// --------------------------------------------------------------------
public class RGController : RGBaseController, OffensiveInterface, MasterInterface // TypeDefIndex: 2750
{
	// Fields
	public RGController attributeDelegate; // 0x228
	public bool customAI; // 0x230
	private bool <disableInteractWeapon>k__BackingField; // 0x231
	private bool <disableInteractItem>k__BackingField; // 0x232
	private bool <suppressCharacterRunAnimation>k__BackingField; // 0x233
	private Action onAfterMount; // 0x238
	private Action onLand; // 0x240
	private Action onAfterPickUpItem; // 0x248
	private Action onUseSpecialButton; // 0x250
	private Action<float> customUpdateEvent; // 0x258
	private Action<RGWeapon> onForceDropWeapon; // 0x260
	public float hitInterval; // 0x268
	private const float Tier3RestoreInvulnerableDuration = 0.5;
	private const float Tier3RestoreExpertPotInvulnerableBonus = 0.5;
	private const float VoidShelterHitIntervalBonus = 0.25;
	private Color _skillLevelUpColor; // 0x26C
	protected int _facing; // 0x27C
	private Action onMountOff; // 0x280
	private Collider2D _bodyCol; // 0x288
	private Vector2 <RigidBodyPosition>k__BackingField; // 0x290
	private static SkinAudioConfig _skinAudioConfig; // 0x0
	public ISpecialItem special_item; // 0x298
	public Jewelry jewelry; // 0x2A0
	private RGMountController <mount>k__BackingField; // 0x2A8
	private NewMechaCombatPilot _newMechaCombatPilot; // 0x2B0
	public AudioClip clip_skill; // 0x2B8
	public AudioClip clip_hit; // 0x2C0
	private List<SkillInfo> _skills; // 0x2C8
	public bool in_battle; // 0x2D0
	internal bool move_invers; // 0x2D1
	internal SpriteRenderer shadow_lock; // 0x2D8
	protected Transform item_tf; // 0x2E0
	private Action OnInteractingItemChange; // 0x2E8
	public UsableItemInterface usableItem; // 0x2F0
	internal Transform camera_focus; // 0x2F8
	private bool _hasCameraFollowSmoothnessOverride; // 0x300
	private float _cameraHorizontalFollowSmoothness; // 0x304
	private float _cameraVerticalFollowSmoothness; // 0x308
	private float seach_time; // 0x30C
	internal bool canMount; // 0x310
	private Action OnRoleAttack; // 0x318
	private Action OnRoleAttackStop; // 0x320
	private Action OnWeaponAttack; // 0x328
	private Action OnWeaponStop; // 0x330
	private Action OnRoleSkillStart; // 0x338
	private Action afterSkillStart; // 0x340
	private Action OnMountAttack; // 0x348
	private Action OnRoleSpecial; // 0x350
	private Action<MovementInput> onRoleAim; // 0x358
	private Func<bool, bool> onBtnAttackClick; // 0x360
	private Action onArmorBreak; // 0x368
	private Action onRestoreArmorFromZero; // 0x370
	private Action onAtkCritical; // 0x378
	public bool can_hurt; // 0x380
	private bool _can_hurt; // 0x381
	private bool _hasMoved; // 0x382
	private readonly HashSet<object> _immuneFireTags; // 0x388
	private readonly HashSet<object> _immuneGasTags; // 0x390
	public readonly LinkedList<GameObject> Statues; // 0x398
	protected float scale; // 0x3A0
	public Action OnHeroSelectAnimEnabled; // 0x3A8
	private bool _isExceptionFactorInCd; // 0x3B0
	private static string[] randomBuffs; // 0x8
	public GameObject servant; // 0x3B8
	public GameObject summonGunServant; // 0x3C0
	public StateSyncPlayer StateSyncPlayer; // 0x3C8
	private Transform <Back>k__BackingField; // 0x3D0
	private string _lastClearRoom; // 0x3D8
	private float _lastClearRoomTime; // 0x3E0
	public Vector2 skillDir; // 0x3E4
	public int fixedSkinIndex; // 0x3EC
	private bool <IsSetUp>k__BackingField; // 0x3F0
	private bool <BindToCameraOnStart>k__BackingField; // 0x3F1
	private int _loadedSkinIndex; // 0x3F4
	private CreationRequest _creationRequest; // 0x3F8
	private ProductSkin _productSkin; // 0x400
	public Action playerSetUpEndAct; // 0x408
	private bool <SkinLoaded>k__BackingField; // 0x410
	public bool setupWeapon; // 0x411
	private PlayerBuffController _playerBuffController; // 0x418
	public bool disableSaveWeaponData; // 0x420
	private Action<int, GameObject> AfterGetHurt; // 0x428
	private Action<HurtInfo> OnGetHurt; // 0x430
	private Action AfterGetHurtNonParam; // 0x438
	public GameObject shieldBlastProto; // 0x440
	private Action onCharacterDead; // 0x448
	private float <CurrentSpeed>k__BackingField; // 0x450
	private HashSet<object> _cantMoveSetter; // 0x458
	private HashSet<object> _cantUseSkillSetter; // 0x460
	private Action<Vector2> callbackTryToMove; // 0x468
	private Vector2 <aimDirAccordingToMove>k__BackingField; // 0x470
	private float <cameraFocusOffsetFactor>k__BackingField; // 0x478
	private bool <IsCameraFocusFixed>k__BackingField; // 0x47C
	private Vector3 _cameraFocusOffset; // 0x480
	protected Vector3 temp_lock_dir; // 0x48C
	public bool stopAutoAiming; // 0x498
	public bool lockFacing; // 0x499
	public Action<bool> OverrideWeaponSpecial; // 0x4A0
	protected bool btnAtkPressed; // 0x4A8
	public Func<bool> preAttack; // 0x4B0
	private HandAbilityInfo _currentHandAbility; // 0x4B8
	public Func<GameObject> HandAbilityProxy; // 0x4C0
	protected bool can_cut; // 0x4C8
	private int _skillIndex; // 0x4CC
	public int fixedSkillIndex; // 0x4D0
	private bool <SkillExtraUpdateStateChanged>k__BackingField; // 0x4D4
	private bool _hasSkillExtraUpdate; // 0x4D5
	private int _skillCastState; // 0x4D8
	private bool _skillDisplacementDuringCast; // 0x4DC
	public bool MultiStatePreparing; // 0x4DD
	public bool MultiStateCanPrepare; // 0x4DE
	internal float skill_time; // 0x4E0
	protected Vector3 DragSkillWorldOffset; // 0x4E4
	protected MovementInput DragInput; // 0x4F0
	private DragSkillData <DragSkillData>k__BackingField; // 0x520
	private bool <SkillEnable>k__BackingField; // 0x528
	private bool _heroUseSkillEventRaisedInCurrentSkillInput; // 0x529
	private int <ChanceToTriggerVampireHp>k__BackingField; // 0x52C
	private int <ChanceToTriggerVampireEnergy>k__BackingField; // 0x530
	private readonly Dictionary<int, float> _canHitDic; // 0x538
	private TimerPool _hitTriggerTimerPool; // 0x540
	private readonly List<int> _needDelList; // 0x548
	public const float MaxHitTriggerEnableTime = 99;
	private readonly GameObjectSingleCache<Collider2D> _hitColliderCache; // 0x550
	private bool <CanRebornTwice>k__BackingField; // 0x558
	private bool <CanAdRebornTwice>k__BackingField; // 0x559
	private Action onCharacterReborn; // 0x560
	private InteractItems _interactItems; // 0x568
	public int skinIndex; // 0x570
	private ObjectZPos _objZPos; // 0x578
	private int flyHeight; // 0x580
	private HashSet<string> _flyFlags; // 0x588
	private HashSet<string> _flyRunAnimationFlags; // 0x590
	private Vector3 _handPositionOrigin; // 0x598
	private Vector3 _hand2PositionOrigin; // 0x5A4
	private Tween bodyScaleTween; // 0x5B0
	private Tween shadowScaleTween; // 0x5B8
	private Sequence bodyScaleSequence; // 0x5C0
	private Action<float> chageScaleEvent; // 0x5C8
	private Action onMount; // 0x5D0
	protected int _level; // 0x5D8
	protected int LastSkillLevel; // 0x5DC
	public Action onDestoryCb; // 0x5E0
	private Action onStopMove; // 0x5E8
	private int spriteMaskInteractionCount; // 0x5F0
	private Timer shadowTimer; // 0x5F8
	private Action<PlayerBulletHitEnemyEvent> HitEnemyEvent; // 0x600
	private Action HitEnemyNonParam; // 0x608
	private Action<RGController, object> KillEnemyEvent; // 0x610
	private Action<PlayerBulletPenetrateEvent> BulletPenetrateEvent; // 0x618
	private Action<PlayerBulletPreHitEnemyEvent> PreHitEnemyEvent; // 0x620
	public float moveDistance; // 0x628
	private Vector2 _lastPos; // 0x62C
	protected int[] longIdleSkinIndexes; // 0x638
	private bool _isLongIdleDisable; // 0x640
	public bool muteLongIdleAudio; // 0x641
	protected float WaitTime; // 0x644
	protected float IdleLongWaitTime; // 0x648
	private bool _needIgnoreVacantWallCheck; // 0x64C
	private Sprite _currentSkillSprite; // 0x650
}

// --------------------------------------------------------------------
// RoleAttribute  [class]
// --------------------------------------------------------------------
public class RoleAttribute : MonoBehaviour // TypeDefIndex: 2787
{
	// Fields
	public readonly AttributeValue MeleeDamage; // 0x20
	public readonly AttributeValue RangedDamage; // 0x28
	public readonly AttributeValue WeaponDamageFactor; // 0x30
	public readonly AttributeValue AddWeaponDamage; // 0x38
	public readonly AttributeValue SkillLevel; // 0x40
	public readonly AttributeValue ElementalDamage; // 0x48
	public readonly AttributeValue ElementalDamageFactor; // 0x50
	public readonly RoleAttributeValue<float> AddFireDamageFactor; // 0x58
	public readonly AttributeValue AoeRange; // 0x60
	public readonly AttributeValue Lifesteal; // 0x68
	public readonly AttributeValue EnergyRengen; // 0x70
	public readonly AttributeValue Salary; // 0x78
	public readonly AttributeValue Luck; // 0x80
	public readonly AttributeValue PetPower; // 0x88
	public readonly AttributeValue HealthRegen; // 0x90
	public readonly RoleAttributeValue<int> ShieldRegen; // 0x98
	public readonly RoleAttributeValue<float> AbilityDamageFactor; // 0xA0
	public int camp; // 0xA8
	public float speed; // 0xAC
	public float speed_rate; // 0xB0
	public RoleAttributeValue<float> addSpeedRate; // 0xB8
	public RoleAttributeValue<float> addAtkSpeed; // 0xC0
	public bool attacking; // 0xC8
	public float attackingSpeedRate; // 0xCC
	public int max_hp; // 0xD0
	public int hp; // 0xD4
	public float hpRestoreSpeed; // 0xD8
	public float hpRestoreTime; // 0xDC
	protected float _hpRestoreTime; // 0xE0
	private float <dynamicAddSpeedFactor>k__BackingField; // 0xE4
	internal float deviation; // 0xE8
	public int critical; // 0xEC
	public readonly AttributeValue FinalDamage; // 0xF0
	public readonly AttributeValue CriticDamage; // 0xF8
	private string _speedChangeID; // 0x100
	private float _speedChangeValue; // 0x108
	internal float atk_speed; // 0x10C
	public int add_energy_ball_recover; // 0x110
	internal float easyCriticFactor; // 0x114
	public float atchDelayFactor; // 0x118
	public float reloadDurationFactor; // 0x11C
	public float addSkillMaxTime; // 0x120
	public float skillReloadSpeedFactor; // 0x124
	public int skillExtraUseTimes; // 0x128
	public RoleAttributeValue<float> SkillDamageFactor; // 0x130
	private UIHpBar.HpChanged OnHpChanged; // 0x138
	protected RGBaseController _rgBaseController; // 0x140
}

// --------------------------------------------------------------------
// RoleAttributePet  [class]
// --------------------------------------------------------------------
private sealed class RoleAttributePet.<ChangingAtkSpeed>d__32 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 2789
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RoleAttributePet <>4__this; // 0x20
	public int change_val; // 0x28
	public float duration; // 0x2C
}

// --------------------------------------------------------------------
// RoleAttributePet  [class]
// --------------------------------------------------------------------
public class RoleAttributePet : RoleAttribute // TypeDefIndex: 2790
{
	// Fields
	private RGBaseController _baseController; // 0x148
	private RGPetWithSkillController _rgPetController; // 0x150
	public int atk; // 0x158
	internal float this_skill_time; // 0x15C
	public uint skill_release_count; // 0x160
	public Action OnSkillReady; // 0x168
}

// --------------------------------------------------------------------
// RoleAttributePlayer  [enum]
// --------------------------------------------------------------------
public enum RoleAttributePlayer.RestoreEnergySource // TypeDefIndex: 2791
{
	// Fields
	public int value__; // 0x0
	public const RoleAttributePlayer.RestoreEnergySource Normal = 0;
	public const RoleAttributePlayer.RestoreEnergySource ItemStats = 1;
}

// --------------------------------------------------------------------
// RoleAttributePlayer  [class]
// --------------------------------------------------------------------
private sealed class RoleAttributePlayer.<>c__DisplayClass96_0 // TypeDefIndex: 2792
{
	// Fields
	public RoleAttributePlayer <>4__this; // 0x10
	public int restoreVal; // 0x18
}

// --------------------------------------------------------------------
// RoleAttributePlayer  [class]
// --------------------------------------------------------------------
private sealed class RoleAttributePlayer.<ChangeChargeTimeFactor>d__113 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 2793
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RoleAttributePlayer <>4__this; // 0x20
	public string key; // 0x28
	public float value; // 0x30
	public float duration; // 0x34
	private RoleAttributeProxy <proxy>5__2; // 0x38
}

// --------------------------------------------------------------------
// RoleAttributePlayer  [class]
// --------------------------------------------------------------------
private sealed class RoleAttributePlayer.<ChangingAtkSpeed>d__111 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 2794
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RoleAttributePlayer <>4__this; // 0x20
	public int change_val; // 0x28
	public float duration; // 0x2C
}

// --------------------------------------------------------------------
// RoleAttributePlayer  [class]
// --------------------------------------------------------------------
private sealed class RoleAttributePlayer.<ChangingDefence>d__109 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 2795
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RoleAttributePlayer <>4__this; // 0x20
	public int defence; // 0x28
	public float duration; // 0x2C
}

// --------------------------------------------------------------------
// RoleAttributePlayer  [class]
// --------------------------------------------------------------------
private sealed class RoleAttributePlayer.<RestoringEnergy>d__100 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 2796
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public float interval; // 0x20
	public RoleAttributePlayer <>4__this; // 0x28
	public int delta; // 0x30
	public bool stopWhenFull; // 0x34
	private WaitForSeconds <wait>5__2; // 0x38
}

// --------------------------------------------------------------------
// RoleAttributePlayer  [class]
// --------------------------------------------------------------------
public class RoleAttributePlayer : RoleAttribute // TypeDefIndex: 2797
{
	// Fields
	public int _max_armor; // 0x148
	public int armor; // 0x14C
	public int max_energy; // 0x150
	public int _energy; // 0x154
	private RGController _rgController; // 0x158
	public int Atk; // 0x160
	public int c_index; // 0x164
	public int c_level; // 0x168
	public emBuff default_buff; // 0x16C
	internal int defence; // 0x170
	internal float bullet_speed_ratio; // 0x174
	internal float this_skill_time; // 0x178
	private float armor_load; // 0x17C
	private float armor_rate; // 0x180
	public RoleAttributeValue<float> armorRestoreSpeed; // 0x188
	public uint skill_release_count; // 0x190
	private bool <IsDemonstrationCharacter>k__BackingField; // 0x194
	private float armor_time; // 0x198
	private float energy_time; // 0x19C
	public Action OnSkillReady; // 0x1A0
	private GameObject <hpBarObj>k__BackingField; // 0x1A8
	private Dictionary<emRoleAttribute, int> additionAttr; // 0x1B0
	private float clampTime; // 0x1B8
	private float clampEnergyTime; // 0x1BC
	private bool reloading; // 0x1C0
	private float <LastBuffArmorBrokenRestoreArmorTime>k__BackingField; // 0x1C4
	private bool lowEnergy; // 0x1C8
}

// --------------------------------------------------------------------
// BuffInfo  [class]
// --------------------------------------------------------------------
public class BuffInfo // TypeDefIndex: 2936
{
	// Fields
	public string buffDesc; // 0x10
	public Sprite icon; // 0x18
	public emBuff buff; // 0x20
}

// --------------------------------------------------------------------
// BuffInfoTable  [class]
// --------------------------------------------------------------------
public class BuffInfoTable : SerializedScriptableObject // TypeDefIndex: 2937
{
	// Fields
	public List<BuffInfo> infos; // 0x58
	private static BuffInfoTable _info; // 0x0
	private Dictionary<emBuff, BuffInfo> _buffDic; // 0x60
}

// --------------------------------------------------------------------
// ActivityTalentTaskDailyRecord  [class]
// --------------------------------------------------------------------
public class ActivityTalentTaskDailyRecord // TypeDefIndex: 3083
{
	// Fields
	public Dictionary<int, int> taskProgress; // 0x10
	public Dictionary<int, int> taskCompletedCounts; // 0x18
	public Dictionary<int, int> taskClaimedCounts; // 0x20
	public List<int> selectedTalentIds; // 0x28
}

// --------------------------------------------------------------------
// NpcTalentUpgrade  [class]
// --------------------------------------------------------------------
private sealed class NpcTalentUpgrade.<DisplayPrice>d__20 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 3534
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public float time; // 0x20
	public NpcTalentUpgrade <>4__this; // 0x28
	public string currencyString; // 0x30
}

// --------------------------------------------------------------------
// NpcTalentUpgrade  [class]
// --------------------------------------------------------------------
private sealed class NpcTalentUpgrade.<DoService>d__21 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 3535
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public int serviceIdx; // 0x20
	public List<emBuff> candidateTalents; // 0x28
	public int price; // 0x30
	public NpcTalentUpgrade <>4__this; // 0x38
	public RGController controller; // 0x40
	public NpcTalentUpgrade npc; // 0x48
	private UITalentUpgrade <uiView>5__2; // 0x50
	private WaitForFixedUpdate <wait>5__3; // 0x58
}

// --------------------------------------------------------------------
// NpcTalentUpgrade  [class]
// --------------------------------------------------------------------
private sealed class NpcTalentUpgrade.<TryShowMasterPracticeTutorDrop>d__22 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 3536
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RGController controller; // 0x20
	public NpcTalentUpgrade <>4__this; // 0x28
	private List<PickableInfo> <rewardInfos>5__2; // 0x30
}

// --------------------------------------------------------------------
// NpcTalentUpgrade  [class]
// --------------------------------------------------------------------
public class NpcTalentUpgrade : RGStateItem, NormalGameModeProcess.IMapCreateCompleteHandler // TypeDefIndex: 3537
{
	// Fields
	private const string MasterPracticeTutorDropTalkKey = "master_ascension/material/mentor_guidance/talk";
	private const string MasterPracticeTutorDropTalkFallback = "这个你也收下吧，祝你一路顺利";
	private const float MasterPracticeTutorDropTalkDelay = 1.1;
	public bool dontShare; // 0x110
	public int[] servicePrices; // 0x118
	public int[] serviceWeights; // 0x120
	private int _serviceIndex; // 0x128
	private Coroutine _displayPriceCoroutone; // 0x130
	private bool _hasUsed; // 0x138
	private bool _isProcessingService; // 0x139
	public static HashSet<emBuff> Talents; // 0x0
	public static HashSet<emBuff> Tier3Talents; // 0x8
}

// --------------------------------------------------------------------
// RGCharacterInfo  [class]
// --------------------------------------------------------------------
public class RGCharacterInfo // TypeDefIndex: 4379
{
	// Fields
	public string char_name; // 0x10
	public bool unlock; // 0x18
	public int level; // 0x1C
	public int unlock_gem; // 0x20
	public int skillCount; // 0x24
	public int[] skin_list; // 0x28
	public int[] origin_skin_list; // 0x30
}

// --------------------------------------------------------------------
// UITalentUpgrade  [class]
// --------------------------------------------------------------------
private sealed class UITalentUpgrade.<UpgradeTagBlink>d__10 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 5210
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public Image img; // 0x20
	private Color <imgColor>5__2; // 0x28
	private CommandDuration <imgCmd1>5__3; // 0x38
	private CommandDuration <imgCmd2>5__4; // 0x40
	private WaitForEndOfFrame <wait>5__5; // 0x48
}

// --------------------------------------------------------------------
// UITalentUpgrade  [class]
// --------------------------------------------------------------------
public class UITalentUpgrade : BaseUIView // TypeDefIndex: 5211
{
	// Fields
	public static emBuff selectedBuff; // 0x0
	private emBuff _currentSelectedBuf; // 0xD8
	private GameObject lastSelectedObj; // 0xE0
	private Coroutine upgradeTagBlinkCoroutine; // 0xE8
}

// --------------------------------------------------------------------
// UITalentUpgradeResult  [class]
// --------------------------------------------------------------------
private sealed class UITalentUpgradeResult.<DoAnimation>d__3 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 5212
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public UITalentUpgradeResult <>4__this; // 0x20
	private RectTransform <rectTransform>5__2; // 0x28
}

// --------------------------------------------------------------------
// RGBaseControllerCreateBulletEvent  [class]
// --------------------------------------------------------------------
public class RGBaseControllerCreateBulletEvent : EventBase // TypeDefIndex: 5607
{
	// Fields
	public object owner; // 0x10
	public GameObject bullet; // 0x18
}

// --------------------------------------------------------------------
// GetTalentBuffEvent  [class]
// --------------------------------------------------------------------
public class GetTalentBuffEvent : EventBase // TypeDefIndex: 5701
{
	// Fields
	public emBuff buff; // 0x10
}

// --------------------------------------------------------------------
// TalentBuffChanageEvent  [class]
// --------------------------------------------------------------------
public class TalentBuffChanageEvent : EventBase // TypeDefIndex: 5715
{
	// Fields
	public emBuff buff; // 0x10
	public bool isGet; // 0x14
}

// --------------------------------------------------------------------
// RGWeapon  [struct]
// --------------------------------------------------------------------
public struct RGWeapon.WeaponEvoluteSkin // TypeDefIndex: 6743
{
	// Fields
	public int Level; // 0x0
	public int SkinIndex; // 0x4
	public int NetId; // 0x8
	public string WeaponName; // 0x10
}

// --------------------------------------------------------------------
// RGWeapon  [class]
// --------------------------------------------------------------------
private sealed class RGWeapon.<>c // TypeDefIndex: 6744
{
	// Fields
	public static readonly RGWeapon.<>c <>9; // 0x0
	public static Func<TalentConfig, bool> <>9__153_0; // 0x8
	public static Action<WeaponAffixBase> <>9__176_0; // 0x10
	public static Action<WeaponAffixBase> <>9__198_0; // 0x18
	public static Action<WeaponAffixBase> <>9__214_0; // 0x20
	public static Action<WeaponAffixBase> <>9__214_1; // 0x28
	public static Action<WeaponAffixBase> <>9__256_0; // 0x30
	public static Action<WeaponAffixBase> <>9__283_0; // 0x38
	public static Action<WeaponAffixBase> <>9__314_0; // 0x40
	public static Action<WeaponAffixBase> <>9__415_0; // 0x48
	public static Action<WeaponAffixBase> <>9__421_0; // 0x50
	public static Action<WeaponAffixBase> <>9__428_0; // 0x58
	public static Func<MethodInfo, bool> <>9__570_0; // 0x60
	public static Func<WeaponAffixBase, Sprite> <>9__588_0; // 0x68
	public static Func<Sprite, bool> <>9__588_1; // 0x70
	public static Action<WeaponAffixBase> <>9__589_0; // 0x78
	public static Func<WeaponAffixBase, WeaponAffixSyncData> <>9__591_0; // 0x80
}

// --------------------------------------------------------------------
// RGWeapon  [class]
// --------------------------------------------------------------------
private sealed class RGWeapon.<>c__DisplayClass270_0 // TypeDefIndex: 6745
{
	// Fields
	public RGWeapon <>4__this; // 0x10
	public RGController rgController; // 0x18
}

// --------------------------------------------------------------------
// RGWeapon  [class]
// --------------------------------------------------------------------
private sealed class RGWeapon.<>c__DisplayClass403_0 // TypeDefIndex: 6746
{
	// Fields
	public GameObject bullet; // 0x10
}

// --------------------------------------------------------------------
// RGWeapon  [class]
// --------------------------------------------------------------------
private sealed class RGWeapon.<>c__DisplayClass468_0 // TypeDefIndex: 6747
{
	// Fields
	public GameObject bulletGo; // 0x10
}

// --------------------------------------------------------------------
// RGWeapon  [class]
// --------------------------------------------------------------------
private sealed class RGWeapon.<>c__DisplayClass562_0 // TypeDefIndex: 6748
{
	// Fields
	public RGWeapon <>4__this; // 0x10
	public int skinIndex; // 0x18
	public bool isAtk; // 0x1C
	public WeaponSkin weaponSkinConfig; // 0x20
	public string weaponId; // 0x28
}

// --------------------------------------------------------------------
// RGWeapon  [class]
// --------------------------------------------------------------------
private sealed class RGWeapon.<AutoAttacking>d__252 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 6749
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public string tag; // 0x20
	public RGWeapon <>4__this; // 0x28
	public float speedFactor; // 0x30
	public float duration; // 0x34
	private float <dt>5__2; // 0x38
	private WaitForSeconds <wait>5__3; // 0x40
}

// --------------------------------------------------------------------
// RGWeapon  [class]
// --------------------------------------------------------------------
private sealed class RGWeapon.<BuffChangingSpeed>d__243 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 6750
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RGWeapon <>4__this; // 0x20
	public float speedFactor; // 0x28
	public float duration; // 0x2C
}

// --------------------------------------------------------------------
// RGWeapon  [class]
// --------------------------------------------------------------------
private sealed class RGWeapon.<ChangingSpeed>d__242 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 6751
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RGWeapon <>4__this; // 0x20
	public float speedFactor; // 0x28
	public float duration; // 0x2C
}

// --------------------------------------------------------------------
// RGWeapon  [class]
// --------------------------------------------------------------------
private sealed class RGWeapon.<RotateBodyZ>d__205 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 6752
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public Transform cBody; // 0x20
	public RGWeapon <>4__this; // 0x28
}

// --------------------------------------------------------------------
// RGWeapon  [class]
// --------------------------------------------------------------------
public class RGWeapon : MonoBehaviour, ItemInterface, IConsumeRecord, IForgeItem, IBackItem // TypeDefIndex: 6753
{
	// Fields
	public bool isBossWeapon; // 0x20
	public bool isCreateFromSkill; // 0x21
	private RGWeapon <ParentWeaponSource>k__BackingField; // 0x28
	public emWeaponType weapon_type; // 0x30
	public List<emWeaponTag> weapon_tags; // 0x38
	public AudioClip audio_clip; // 0x40
	public bool activate; // 0x48
	public bool overrideHandCut; // 0x49
	public int item_level; // 0x4C
	public int item_value; // 0x50
	public Vector2 grabLocalOffset; // 0x54
	public List<BulletInfoInspector> bulletsInfo; // 0x60
	public int deviation; // 0x68
	private bool _runtimeDeviationInit; // 0x6C
	private int _runtimeDeviation; // 0x70
	public int consume; // 0x74
	public int consumeChange; // 0x78
	public float dmgMultiFactor; // 0x7C
	public emWeaponSource weaponSource; // 0x80
	protected float _specialEnergy; // 0x84
	public FuncObj OnCreateBulletInfo; // 0x88
	public FuncObj OnCreateDamageInfo; // 0x90
	public float holdSpeedFactor; // 0x98
	public ReloadClipSource reloadClipSource; // 0x9C
	public bool ignoreControllerMovePenalty; // 0xA0
	public Transform targetObj; // 0xA8
	internal emOwnerType OwnerType; // 0xB0
	public float weapon_speed; // 0xB4
	public float atk_move_speed; // 0xB8
	internal Transform gun_point; // 0xC0
	public RGBaseController controller; // 0xC8
	public RGBaseController owner; // 0xD0
	public bool suppressEscapeTalismanTalentQuery; // 0xD8
	public Animator anim; // 0xE0
	public bool need_lock; // 0xE8
	public bool has_effect; // 0xE9
	public bool is_melee; // 0xEA
	public bool not_forgeable; // 0xEB
	internal bool has_picked; // 0xEC
	public bool can_use; // 0xED
	internal RGRandom rg_random; // 0xF0
	private WeaponItemBase <weapon_item>k__BackingField; // 0xF8
	internal WeaponItemBase weapon_item_activity; // 0x100
	private bool <forbidSwitchWeapon>k__BackingField; // 0x108
	public readonly AttributeValue FinalDamage; // 0x110
	public readonly AttributeValue BulletSizeFactor; // 0x118
	private StateSyncRgWeapon _stateSyncRgWeapon; // 0x120
	private HashSet<emBuff> _talentList; // 0x128
	private bool <handCutWhenOutOfEnergy>k__BackingField; // 0x130
	public Action onPickup; // 0x138
	private Action OnStartUseWeapon; // 0x140
	private Action OnStopWeapon; // 0x148
	private Action<RGWeapon> OnDropWeapon; // 0x150
	internal int attack_count; // 0x158
	public bool canDualWielding; // 0x15C
	protected List<SpriteRenderer> _cachedWeaponSpr; // 0x160
	public Action<BulletSplitConfig> onBulletStartSplit; // 0x168
	public Action onBulletEndSplit; // 0x170
	public Action<GameObject> onSplitBullet; // 0x178
	protected bool EnergyNotEnoughStopAtk; // 0x180
	private readonly List<emBuff> _talentRecord; // 0x188
	protected int _pressureLv; // 0x190
	public Action PreCheckAttack; // 0x198
	private bool <attackPressed>k__BackingField; // 0x1A0
	private Action OnAttackKeyDown; // 0x1A8
	private Action OnAttackKeyUp; // 0x1B0
	public bool ImmuneSpeedChange; // 0x1B8
	protected bool hasAddSpeed; // 0x1B9
	private Coroutine _rotateCor; // 0x1C0
	public Action<RGWeapon> OnSwitch2Front; // 0x1C8
	protected float _buffAtkSpeedFactor; // 0x1D0
	protected float _speedFactor; // 0x1D4
	private AttributeValue _attackSpeedFactor; // 0x1D8
	private float _atkFactor; // 0x1E0
	private float _appliedShooterAtkFactor; // 0x1E4
	private float _criticFactor; // 0x1E8
	private HashSet<string> _autoAttackTags; // 0x1F0
	private bool autoAtk; // 0x1F8
	protected const float MinWeaponSpeed = 0.1;
	protected const float MaxWeaponSpeed = 10;
	private Action<RGWeapon> OnCopyFrom; // 0x200
	private bool energyWarn; // 0x208
	private bool energyWarnTrigger; // 0x209
	public bool ignoreConsume; // 0x20A
	public Func<bool> onConsumeEnergy; // 0x210
	public Action onBeforeConsume; // 0x218
	protected int[] _icons; // 0x220
	protected string[] _texts; // 0x228
	private Action OnFlushIcon; // 0x230
	private IWeaponSpecial _weaponSpecial; // 0x238
	private bool hasSpecialInit; // 0x240
	protected Vector3 DragSpecialWorldOffset; // 0x244
	protected MovementInput DragSpecialInput; // 0x250
	private DragSkillData <DragSpecialData>k__BackingField; // 0x280
	private bool _dragSpecialHasInput; // 0x288
	private Action<GameObject> onBulletCreate; // 0x290
	public GameObject latestCreatedBulletObject; // 0x298
	public List<Func<GameObject, GameObject>> onBulletReplacement; // 0x2A0
	private Action afterBulletCreate; // 0x2A8
	private Action afterAttack; // 0x2B0
	private Action<RGWeapon> afterAttackWithWeapon; // 0x2B8
	private Action onAttackStart; // 0x2C0
	public float AtkTimeDuration; // 0x2C8
	private float _tmpAtkStartTime; // 0x2CC
	private Action onAttackStop; // 0x2D0
	public FuncObj overridedFireAngle; // 0x2D8
	private Dictionary<GameObject, int> bullet2DmgType; // 0x2E0
	internal int additionLevel; // 0x2E8
	private int _level; // 0x2EC
	private DefenceWeapon _defence_weapon; // 0x2F0
	public Action<int, int> OnLevelChanged; // 0x2F8
	private bool <NeedRecord>k__BackingField; // 0x300
	private ConsumeInfo _consumeInfo; // 0x308
	public bool limitUseInMultiRoom; // 0x310
	private bool <funnelMode>k__BackingField; // 0x311
	private GameObject target_point; // 0x318
	private float detect_radius; // 0x320
	protected bool IsGunEchoWeapon; // 0x324
	protected GameObject ExternalSourceObject; // 0x328
	private bool _isEvolvedWeapon; // 0x330
	private int _weaponSkinId; // 0x334
	private bool _weaponSkinPreviewMode; // 0x338
	private int _weaponSkinPreviewReadySkinId; // 0x33C
	private bool _weaponSkinResourcesReady; // 0x340
	private Action<RGWeapon> OnWeaponSkinResourcesApplied; // 0x348
	private bool <LockEvolute>k__BackingField; // 0x350
	private bool <LockSkin>k__BackingField; // 0x351
	protected List<float> EvolvedWeaponParameters; // 0x358
	private CreationRequest _creationRequest; // 0x360
	private ProductSkin _productSkin; // 0x368
	private static readonly Dictionary<Type, MethodInfo> LoadMethodCache; // 0x0
	private readonly List<WeaponAffixBase> _weaponAffixes; // 0x370
}

// --------------------------------------------------------------------
// ActivityTalentStatistics  [class]
// --------------------------------------------------------------------
public static class ActivityTalentStatistics // TypeDefIndex: 8078
{
	// Fields
	private const string ActivityName = "ActivityTalentManager";
	private const string EventGameStart = "activity_talent_game_start";
	private const string EventGameOver = "activity_talent_game_over";
	private const string EventTalentOffTaskRewardClaim = "talent_off_task_reward_claim";
	private const string EventTalentOffTaskPointRewardClaim = "talent_off_task_point_reward_claim";
	private static bool _gameStartTracked; // 0x0
	private static bool _gameOverTracked; // 0x1
}

// --------------------------------------------------------------------
// VoidShieldForRGEController  [class]
// --------------------------------------------------------------------
private sealed class VoidShieldForRGEController.<DelayBreak>d__31 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 8825
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public VoidShieldForRGEController <>4__this; // 0x20
}

// --------------------------------------------------------------------
// VoidShieldForRGEController  [class]
// --------------------------------------------------------------------
public class VoidShieldForRGEController : MonoBehaviour // TypeDefIndex: 8826
{
	// Fields
	public const string VoidShieldName = "VoidShield";
	public AudioClip clip; // 0x20
	public const int DefaultHp = 30;
	private const int DifficultyHpStep = 20;
	private const int Damage = 1;
	public const string VoidShieldPath = "Level/difficulty/LevelObject/Other/void_shield/VoidShield_RGEController.prefab";
	private RoleResourceValue<int> _hp; // 0x28
	public Action<VoidShieldForRGEController> OnShieldBreak; // 0x30
	private RGEController _ctrl; // 0x38
	private Animator _animator; // 0x40
	private static readonly int BreakHash; // 0x0
	private static readonly int HideHash; // 0x4
	private bool _broken; // 0x48
	private bool <ShieldHard>k__BackingField; // 0x49
	private float _shieldHardTime; // 0x4C
	private Timer _shieldHardTimer; // 0x50
	private Sequence _shieldHardSequence; // 0x58
	private SpriteRenderer _shieldImg; // 0x60
}

// --------------------------------------------------------------------
// BuffManager  [class]
// --------------------------------------------------------------------
public class BuffManager : FComponent // TypeDefIndex: 11269
{
	// Fields
	public BooleanCounter counter_debuffImmune; // 0x20
	public readonly List<FBuff> buffs; // 0x28
	private readonly Queue<FBuff> _buffAddQueue; // 0x30
	private readonly Queue<FBuff> _buffRemoveQueue; // 0x38
	private readonly Dictionary<BuffName, FBuff> _name2Buff; // 0x40
	private Action<BuffInflictInfo> OnAddBuff; // 0x48
	private Action<BuffInflictInfo> OnRemoveBuff; // 0x50
}

// --------------------------------------------------------------------
// DamageType  [enum]
// --------------------------------------------------------------------
public enum DamageType // TypeDefIndex: 11296
{
	// Fields
	public int value__; // 0x0
	public const DamageType Poisoned = 0;
	public const DamageType Ignited = 1;
	public const DamageType Bleeding = 2;
}

// --------------------------------------------------------------------
// DamageInfo  [enum]
// --------------------------------------------------------------------
public enum DamageInfo.Flags // TypeDefIndex: 11298
{
	// Fields
	public int value__; // 0x0
	public const DamageInfo.Flags Crit = 1;
	public const DamageInfo.Flags Dodged = 2;
	public const DamageInfo.Flags Blocked = 4;
}

// --------------------------------------------------------------------
// DamageInfo  [struct]
// --------------------------------------------------------------------
public struct DamageInfo // TypeDefIndex: 11299
{
	// Fields
	public DamageType type; // 0x0
	public CombatActionInfo action; // 0x8
	public DamageSubtype damageSubtype; // 0x28
	public FixedNumber rawDamage; // 0x30
	public FixedNumber damage; // 0x38
	public FixedNumber notClampedDamage; // 0x40
	public FixedNumber realDamage; // 0x48
	public int directSource; // 0x50
	public IHitSource source; // 0x58
	public IHitTarget target; // 0x60
	public DamageInfo.Flags flags; // 0x68
	public int currentPermission; // 0x6C
}

// --------------------------------------------------------------------
// DamageInfoModifierPermission  [enum]
// --------------------------------------------------------------------
public enum DamageInfoModifierPermission // TypeDefIndex: 11300
{
	// Fields
	public int value__; // 0x0
	public const DamageInfoModifierPermission Dodge = 10;
}

// --------------------------------------------------------------------
// TalentConfig  [class]
// --------------------------------------------------------------------
public class TalentConfig // TypeDefIndex: 11816
{
	// Fields
	public string talentName; // 0x10
	public string bindingScript; // 0x18
	public List<FixedKeyValue> keyValueList; // 0x20
	public string customData; // 0x28
	public bool autoBindScript; // 0x30
}

// --------------------------------------------------------------------
// DebugLuaManager  [class]
// --------------------------------------------------------------------
private sealed class DebugLuaManager.<>c // TypeDefIndex: 11875
{
	// Fields
	public static readonly DebugLuaManager.<>c <>9; // 0x0
	public static Action <>9__11_0; // 0x8
	public static Action <>9__11_1; // 0x10
}

// --------------------------------------------------------------------
// DebugLuaManager  [class]
// --------------------------------------------------------------------
private sealed class DebugLuaManager.<>c__DisplayClass8_0 // TypeDefIndex: 11876
{
	// Fields
	public string commandKey; // 0x10
	public DebugLuaManager <>4__this; // 0x18
}

// --------------------------------------------------------------------
// DebugLuaManager  [struct]
// --------------------------------------------------------------------
private struct DebugLuaManager.<DownloadLuaScripts>d__14 : IAsyncStateMachine // TypeDefIndex: 11877
{
	// Fields
	public int <>1__state; // 0x0
	public AsyncTaskMethodBuilder <>t__builder; // 0x8
	public DebugLuaManager <>4__this; // 0x20
	private string <dataSavePath>5__2; // 0x28
	private TaskAwaiter<byte[]> <>u__1; // 0x30
	private TaskAwaiter <>u__2; // 0x38
}

// --------------------------------------------------------------------
// DebugLuaManager  [struct]
// --------------------------------------------------------------------
private struct DebugLuaManager.<Execute>d__19 : IAsyncStateMachine // TypeDefIndex: 11878
{
	// Fields
	public int <>1__state; // 0x0
	public AsyncVoidMethodBuilder <>t__builder; // 0x8
	public DebugLuaManager <>4__this; // 0x28
	public string command; // 0x30
	private TaskAwaiter <>u__1; // 0x38
}

// --------------------------------------------------------------------
// DebugLuaManager  [struct]
// --------------------------------------------------------------------
private struct DebugLuaManager.<GmStart>d__18 : IAsyncStateMachine // TypeDefIndex: 11879
{
	// Fields
	public int <>1__state; // 0x0
	public AsyncVoidMethodBuilder <>t__builder; // 0x8
	public DebugLuaManager <>4__this; // 0x28
	private TaskAwaiter<bool> <>u__1; // 0x30
	private TaskAwaiter <>u__2; // 0x38
}

// --------------------------------------------------------------------
// DebugLuaManager  [class]
// --------------------------------------------------------------------
public class DebugLuaManager : Singleton<DebugLuaManager> // TypeDefIndex: 11880
{
	// Fields
	private LuaEnv _luaEnv; // 0x20
	private LuaTable _gameTable; // 0x28
	public readonly HashSet<string> LuaKeySet; // 0x30
	private readonly SafeCheckClient SafeCheckClient; // 0x38
}

// --------------------------------------------------------------------
// RuntimeLuaSourceLoader  [class]
// --------------------------------------------------------------------
public class RuntimeLuaSourceLoader // TypeDefIndex: 11891
{
	// Fields
	private const string SourceRoot = "SoulKnight/Runtime/Lua/Src";
	private const string EncryptedSourceRoot = "SoulKnight/Runtime/Lua/EncryptedSrc";
}

// --------------------------------------------------------------------
// CsvLoader  [struct]
// --------------------------------------------------------------------
public struct CsvLoader.LoadSource<T> // TypeDefIndex: 11925
{
	// Fields
	public readonly string Text; // 0x0
	public readonly bool Encrypted; // 0x0
}

// --------------------------------------------------------------------
// CsvLoader  [class]
// --------------------------------------------------------------------
public sealed class CsvLoader.LoadResult<T> // TypeDefIndex: 11926
{
	// Fields
	private readonly Dictionary<string, T> _data; // 0x0
	private readonly List<string> _errors; // 0x0
}

// --------------------------------------------------------------------
// CsvLoader  [class]
// --------------------------------------------------------------------
public class CsvLoader<T> // TypeDefIndex: 11927
{
	// Fields
	private const string SecretKey = "soulKnight";
	private Dictionary<string, T> _dataDict; // 0x0
	private readonly Type _tType; // 0x0
}

// --------------------------------------------------------------------
// AngryFrighterTalent  [class]
// --------------------------------------------------------------------
public class AngryFrighterTalent : Troop2Talent // TypeDefIndex: 12287
{
	// Fields
	private float lastPercent; // 0x18
	private float atkSpeed; // 0x1C
}

// --------------------------------------------------------------------
// AngryRabbiTalent  [class]
// --------------------------------------------------------------------
public class AngryRabbiTalent : Troop2Talent // TypeDefIndex: 12288
{
	// Fields
	private float lastPercent; // 0x18
}

// --------------------------------------------------------------------
// AttackSpeedTalent  [class]
// --------------------------------------------------------------------
public class AttackSpeedTalent : Troop2Talent // TypeDefIndex: 12289
{
	// Fields
	private float atkSpeed; // 0x18
}

// --------------------------------------------------------------------
// ContinuousCountTalent  [class]
// --------------------------------------------------------------------
public class ContinuousCountTalent : Troop2Talent // TypeDefIndex: 12292
{
	// Fields
	private readonly HashSet<RGWeapon> _modifyWeapons; // 0x18
}

// --------------------------------------------------------------------
// DefenseTalent  [class]
// --------------------------------------------------------------------
public class DefenseTalent : Troop2Talent // TypeDefIndex: 12294
{
	// Fields
	private int defence; // 0x18
}

// --------------------------------------------------------------------
// FindHitTalent  [struct]
// --------------------------------------------------------------------
private struct FindHitTalent.<Dashing>d__8 : IAsyncStateMachine // TypeDefIndex: 12296
{
	// Fields
	public int <>1__state; // 0x0
	public AsyncUniTaskVoidMethodBuilder <>t__builder; // 0x8
	public FindHitTalent <>4__this; // 0x10
	private Transform <nowTarget>5__2; // 0x18
	private UniTask.Awaiter <>u__1; // 0x20
}

// --------------------------------------------------------------------
// FindHitTalent  [class]
// --------------------------------------------------------------------
public class FindHitTalent : Troop2Talent // TypeDefIndex: 12297
{
	// Fields
	private Transform target; // 0x18
	private GameObject theBullet; // 0x20
	private RGEController rgeController; // 0x28
	private CancellationTokenSource _ctSource; // 0x30
	private readonly float velocity; // 0x38
}

// --------------------------------------------------------------------
// HealthRegenTalent  [class]
// --------------------------------------------------------------------
public class HealthRegenTalent : Troop2Talent // TypeDefIndex: 12298
{
	// Fields
	private float time; // 0x18
	private float rhp; // 0x1C
}

// --------------------------------------------------------------------
// HitRocketTalent  [class]
// --------------------------------------------------------------------
public class HitRocketTalent : Troop2Talent // TypeDefIndex: 12299
{
	// Fields
	private readonly HashSet<RGWeapon> _modifyWeapons; // 0x18
}

// --------------------------------------------------------------------
// LaserRainTalent  [struct]
// --------------------------------------------------------------------
private struct LaserRainTalent.<OnDoSkillUniTaskVoid>d__4 : IAsyncStateMachine // TypeDefIndex: 12300
{
	// Fields
	public int <>1__state; // 0x0
	public AsyncUniTaskVoidMethodBuilder <>t__builder; // 0x8
	public SkillBase obj; // 0x10
	public LaserRainTalent <>4__this; // 0x18
	private Transform <target>5__2; // 0x20
	private float[] <descParams>5__3; // 0x28
	private int <i>5__4; // 0x30
	private UniTask.Awaiter <>u__1; // 0x38
}

// --------------------------------------------------------------------
// LaserRainTalent  [class]
// --------------------------------------------------------------------
public class LaserRainTalent : Troop2Talent // TypeDefIndex: 12301
{
	// Fields
	private CancellationTokenSource _ctSource; // 0x18
}

// --------------------------------------------------------------------
// MultiCountTalent  [class]
// --------------------------------------------------------------------
public class MultiCountTalent : Troop2Talent // TypeDefIndex: 12302
{
	// Fields
	private readonly HashSet<RGWeapon> _modifyWeapons; // 0x18
}

// --------------------------------------------------------------------
// RandomWeaponTalent  [class]
// --------------------------------------------------------------------
private sealed class RandomWeaponTalent.<>c // TypeDefIndex: 12304
{
	// Fields
	public static readonly RandomWeaponTalent.<>c <>9; // 0x0
	public static Func<T2Good, bool> <>9__2_0; // 0x8
}

// --------------------------------------------------------------------
// RandomWeaponTalent  [class]
// --------------------------------------------------------------------
private sealed class RandomWeaponTalent.<>c__DisplayClass2_0 // TypeDefIndex: 12305
{
	// Fields
	public T2Good[] tbGoods; // 0x10
	public int level; // 0x18
	public RandomWeaponTalent <>4__this; // 0x20
}

// --------------------------------------------------------------------
// RandomWeaponTalent  [class]
// --------------------------------------------------------------------
public class RandomWeaponTalent : Troop2Talent // TypeDefIndex: 12306
{
	// Fields
	private bool changed; // 0x18
}

// --------------------------------------------------------------------
// SuperWeapon3Talent  [class]
// --------------------------------------------------------------------
private sealed class SuperWeapon3Talent.<>c // TypeDefIndex: 12312
{
	// Fields
	public static readonly SuperWeapon3Talent.<>c <>9; // 0x0
	public static Func<Troop2Talent, bool> <>9__2_0; // 0x8
	public static Func<Troop2Talent, bool> <>9__2_1; // 0x10
}

// --------------------------------------------------------------------
// SuperWeapon3Talent  [class]
// --------------------------------------------------------------------
public class SuperWeapon3Talent : SuperWeaponTalent // TypeDefIndex: 12313
{
	// Fields
	private bool changed; // 0x18
}

// --------------------------------------------------------------------
// Troop2TalentName  [enum]
// --------------------------------------------------------------------
public enum Troop2TalentName // TypeDefIndex: 12314
{
	// Fields
	public int value__; // 0x0
	public const Troop2TalentName None = 0;
	public const Troop2TalentName CriticDamage = 1;
	public const Troop2TalentName BulletThrough = 2;
	public const Troop2TalentName LaserRain = 3;
	public const Troop2TalentName EnergyRegen = 4;
	public const Troop2TalentName HealthRegen = 5;
	public const Troop2TalentName AttackSpeed = 6;
	public const Troop2TalentName Defense = 7;
	public const Troop2TalentName ShieldBlue = 8;
	public const Troop2TalentName RandomWeapon = 9;
	public const Troop2TalentName ShieldBlast = 10;
	public const Troop2TalentName BulletSize = 11;
	public const Troop2TalentName MultiCount = 12;
	public const Troop2TalentName ContinuousCount = 13;
	public const Troop2TalentName FindHit = 14;
	public const Troop2TalentName HitRocket = 15;
	public const Troop2TalentName Vampire = 16;
	public const Troop2TalentName SuperWeapon1 = 17;
	public const Troop2TalentName SuperWeapon2 = 18;
	public const Troop2TalentName SuperWeapon3 = 19;
	public const Troop2TalentName AngryFrighter = 20;
	public const Troop2TalentName AngryRabbi = 21;
	public const Troop2TalentName NoDied = 22;
}

// --------------------------------------------------------------------
// Troop2Talent  [class]
// --------------------------------------------------------------------
public abstract class Troop2Talent // TypeDefIndex: 12315
{
	// Fields
	public Troop2Mercenary mercenary; // 0x10
}

// --------------------------------------------------------------------
// CGBuffManager  [class]
// --------------------------------------------------------------------
private sealed class CGBuffManager.<>c // TypeDefIndex: 12890
{
	// Fields
	public static readonly CGBuffManager.<>c <>9; // 0x0
	public static Predicate<CGBuff> <>9__5_0; // 0x0
}

// --------------------------------------------------------------------
// CGBuffManager  [class]
// --------------------------------------------------------------------
private sealed class CGBuffManager.<>c__DisplayClass2_0 // TypeDefIndex: 12891
{
	// Fields
	public string buffID; // 0x0
}

// --------------------------------------------------------------------
// CGBuffManager  [class]
// --------------------------------------------------------------------
private sealed class CGBuffManager.<>c__DisplayClass4_0 // TypeDefIndex: 12892
{
	// Fields
	public string buffID; // 0x0
}

// --------------------------------------------------------------------
// CGBuffManager  [class]
// --------------------------------------------------------------------
public class CGBuffManager : MonoBehaviour // TypeDefIndex: 12893
{
	// Fields
	public static CGBuffManager BuffManager; // 0x0
	private readonly Dictionary<GameObject, List<CGBuff>> _buffs; // 0x0
}

// --------------------------------------------------------------------
// CGDamageInfo  [struct]
// --------------------------------------------------------------------
public struct CGDamageInfo // TypeDefIndex: 13005
{
	// Fields
	public int damage; // 0x0
	public int critic; // 0x0
	public CGWeapon sourceWeapon; // 0x0
	public RGController sourceController; // 0x0
	public CGProjectile sourceProj; // 0x0
	public GameObject sourceObject; // 0x0
}

// --------------------------------------------------------------------
// ArtifactDamageInfo  [enum]
// --------------------------------------------------------------------
public enum ArtifactDamageInfo.StateFlag // TypeDefIndex: 13369
{
	// Fields
	public int value__; // 0x0
	public const ArtifactDamageInfo.StateFlag None = 0;
	public const ArtifactDamageInfo.StateFlag ProcessedHitWall = 1;
}

// --------------------------------------------------------------------
// ArtifactDamageInfo  [class]
// --------------------------------------------------------------------
public class ArtifactDamageInfo // TypeDefIndex: 13370
{
	// Fields
	public WeaponComponentBulletEmitter bulletEmitter; // 0x10
	public bool isGodPower; // 0x18
	public float godpowerRechargeFactor; // 0x1C
	public int atkSequence; // 0x20
	public float chargeAmount; // 0x24
	public ArtifactDamageInfo.StateFlag customFlag; // 0x28
	public int customTag; // 0x2C
}

// --------------------------------------------------------------------
// RGWeaponArtifactBody  [enum]
// --------------------------------------------------------------------
public enum RGWeaponArtifactBody.AttackType // TypeDefIndex: 13557
{
	// Fields
	public int value__; // 0x0
	public const RGWeaponArtifactBody.AttackType Normal = 0;
	public const RGWeaponArtifactBody.AttackType SpecialSkill = 1;
}

// --------------------------------------------------------------------
// RGWeaponArtifactBody  [class]
// --------------------------------------------------------------------
private class RGWeaponArtifactBody.PartInfo // TypeDefIndex: 13558
{
	// Fields
	public ArtifactComponentPart part; // 0x10
	public List<ArtifactWeaponComponent> nativeComponents; // 0x18
	public List<ArtifactWeaponComponent> additionComponents; // 0x20
}

// --------------------------------------------------------------------
// RGWeaponArtifactBody  [class]
// --------------------------------------------------------------------
private class RGWeaponArtifactBody.ExtraPartEffectInfo // TypeDefIndex: 13559
{
	// Fields
	public int tier; // 0x10
	public int level; // 0x14
}

// --------------------------------------------------------------------
// RGWeaponArtifactBody  [class]
// --------------------------------------------------------------------
private sealed class RGWeaponArtifactBody.<>c // TypeDefIndex: 13560
{
	// Fields
	public static readonly RGWeaponArtifactBody.<>c <>9; // 0x0
	public static Func<ArtifactWeaponComponent, ArtifactWeaponComponent.Command> <>9__231_0; // 0x8
	public static Comparison<RGWeaponArtifactBody.PartInfo> <>9__252_0; // 0x10
	public static Func<ArtifactWeaponComponent, ArtifactWeaponComponent.Command> <>9__266_0; // 0x18
	public static Func<ArtifactWeaponComponent, ArtifactWeaponComponent.Command> <>9__266_1; // 0x20
	public static Func<ArtifactWeaponComponent, ArtifactWeaponComponent.Command> <>9__266_2; // 0x28
	public static Func<ArtifactWeaponComponent, ArtifactWeaponComponent.Command> <>9__267_0; // 0x30
	public static Func<ArtifactWeaponComponent, ArtifactWeaponComponent.Command> <>9__271_0; // 0x38
	public static Func<ArtifactWeaponComponent, ArtifactWeaponComponent.Command> <>9__271_1; // 0x40
	public static Comparison<ArtifactWeaponComponent.Command> <>9__286_0; // 0x48
	public static Func<ArtifactWeaponComponent, ArtifactWeaponComponent.Command> <>9__288_0; // 0x50
	public static Func<ArtifactWeaponComponent, ArtifactWeaponComponent.Command> <>9__295_0; // 0x58
	public static Func<ArtifactWeaponComponent, ArtifactWeaponComponent.Command> <>9__299_0; // 0x60
	public static Comparison<ArtifactComponentPart> <>9__313_0; // 0x68
	public static Comparison<SpriteRenderer> <>9__313_1; // 0x70
	public static Comparison<ArtifactComponentPart> <>9__316_0; // 0x78
	public static Action <>9__336_0; // 0x80
}

// --------------------------------------------------------------------
// RGWeaponArtifactBody  [class]
// --------------------------------------------------------------------
private sealed class RGWeaponArtifactBody.<>c__DisplayClass272_0 // TypeDefIndex: 13561
{
	// Fields
	public RGWeaponArtifactBody.AttackType attackType; // 0x10
}

// --------------------------------------------------------------------
// RGWeaponArtifactBody  [class]
// --------------------------------------------------------------------
private sealed class RGWeaponArtifactBody.<>c__DisplayClass273_0 // TypeDefIndex: 13562
{
	// Fields
	public RGWeaponArtifactBody.AttackType attackType; // 0x10
	public int attackTypeParam; // 0x14
	public int atkSequence; // 0x18
}

// --------------------------------------------------------------------
// RGWeaponArtifactBody  [class]
// --------------------------------------------------------------------
private sealed class RGWeaponArtifactBody.<>c__DisplayClass274_0 // TypeDefIndex: 13563
{
	// Fields
	public RGWeaponArtifactBody.AttackType attackType; // 0x10
}

// --------------------------------------------------------------------
// RGWeaponArtifactBody  [class]
// --------------------------------------------------------------------
private sealed class RGWeaponArtifactBody.<>c__DisplayClass275_0 // TypeDefIndex: 13564
{
	// Fields
	public RGWeaponArtifactBody.AttackType atkType; // 0x10
	public int attackTypeParam; // 0x14
}

// --------------------------------------------------------------------
// RGWeaponArtifactBody  [class]
// --------------------------------------------------------------------
private sealed class RGWeaponArtifactBody.<>c__DisplayClass276_0 // TypeDefIndex: 13565
{
	// Fields
	public RGWeaponArtifactBody.AttackType atkType; // 0x10
	public int attackTypeParam; // 0x14
}

// --------------------------------------------------------------------
// RGWeaponArtifactBody  [class]
// --------------------------------------------------------------------
private sealed class RGWeaponArtifactBody.<>c__DisplayClass287_0 // TypeDefIndex: 13566
{
	// Fields
	public Func<ArtifactWeaponComponent, ArtifactWeaponComponent.Command> todo; // 0x10
}

// --------------------------------------------------------------------
// RGWeaponArtifactBody  [class]
// --------------------------------------------------------------------
private sealed class RGWeaponArtifactBody.<>c__DisplayClass295_0 // TypeDefIndex: 13567
{
	// Fields
	public bool toFront; // 0x10
}

// --------------------------------------------------------------------
// RGWeaponArtifactBody  [class]
// --------------------------------------------------------------------
private sealed class RGWeaponArtifactBody.<>c__DisplayClass327_0 // TypeDefIndex: 13568
{
	// Fields
	public RGWeaponArtifactBody <>4__this; // 0x10
	public RectTransform parent; // 0x18
}

// --------------------------------------------------------------------
// RGWeaponArtifactBody  [class]
// --------------------------------------------------------------------
public class RGWeaponArtifactBody : RGWeapon, IWeaponCustomDisplay, IDoubleWeapon, ChargeWeaponInterface // TypeDefIndex: 13569
{
	// Fields
	public RGWeaponArtifactBody.AttackType currentAttackType; // 0x378
	private float <recharge>k__BackingField; // 0x37C
	private float <addRechargeFactor>k__BackingField; // 0x380
	public AudioClip attachComponentClip; // 0x388
	private List<RGWeaponArtifactBody.PartInfo> partList; // 0x390
	protected ArtifactComponentPart[] _cachedPartArray; // 0x398
	private List<ArtifactsModeData.ArtifactsAttributeInfo> _partAttributes; // 0x3A0
	private WeaponComActionAttack <actionComponent>k__BackingField; // 0x3A8
	public GameObject weaponBody; // 0x3B0
	public Vector3 equipedOffset; // 0x3B8
	private Vector3 <_equipedRotation>k__BackingField; // 0x3C4
	public GameObject specialButtonPrefab; // 0x3D0
	private GameObject _weaponBodyInner; // 0x3D8
	protected List<ArtifactWeaponComponent> _extraComponents; // 0x3E0
	protected SortedDictionary<string, ArtifactPartEffect> _partEffectsDict; // 0x3E8
	private SortedDictionary<string, RGWeaponArtifactBody.ExtraPartEffectInfo> extraPartEffects; // 0x3F0
	private bool extraPartEffectChanged; // 0x3F8
	public List<ArtifactGodPower> activeGodpowers; // 0x400
	private float <godPowerDuration>k__BackingField; // 0x408
	private int <godPowerDamage>k__BackingField; // 0x40C
	private float <inscriptionAdditionalPower>k__BackingField; // 0x410
	public List<float> inscriptionAdditionalPowerOfRanks; // 0x418
	private List<ArtifactInscriptionEffect> _inscriptionEffects; // 0x420
	public List<ArtifactInscriptionEffect> tempInscriptionEffects; // 0x428
	private ArtifactsModeData _modeData; // 0x430
	private List<ArtifactWeaponComponent.Command> _additionalCmds; // 0x438
	public Dictionary<string, object> componentShareData; // 0x440
	private Func<int, int> overrideConsume; // 0x448
	private Action<RGWeaponArtifactBody.AttackType> onAttackActionStart; // 0x450
	private Action<RGWeaponArtifactBody.AttackType> onAttackActionEvent; // 0x458
	private ArtifactWeaponData _artifactWeaponData; // 0x460
	private int _oldSkillHaste; // 0x468
	private int _addConsume; // 0x46C
	private int _addRepel; // 0x470
	private int _addDeviation; // 0x474
	private int _throughCount; // 0x478
	private int _modifiedDeltaHP; // 0x47C
	public float addGodPower; // 0x480
	public float addBulletSpeed; // 0x484
	public int defaultEmitterExtraTimes; // 0x488
	public float bulletAddScale; // 0x48C
	private bool <attackActionComModified>k__BackingField; // 0x490
	public string attackActionComModifyKey; // 0x498
	private bool <modifyAsChargeAttack>k__BackingField; // 0x4A0
	private bool <modifyAsChargeAttack_NoEvent>k__BackingField; // 0x4A1
	private float <modifyAsChargeAttack_MaxChargeTime>k__BackingField; // 0x4A4
	private bool <isCopied>k__BackingField; // 0x4A8
	private bool <isAttacking>k__BackingField; // 0x4A9
	private SortingGroup _sortingGroup; // 0x4B0
	private ICommand _artifactUpdateCommand; // 0x4B8
	private bool _atk_b; // 0x4C0
	private bool _atkPressed; // 0x4C1
	protected bool _atkValid; // 0x4C2
	private UIArtifactsGodPower _specialButtonObj; // 0x4C8
	private Action onClickSpecialSkillButton; // 0x4D0
	private Dictionary<string, object> listenEventFlag; // 0x4D8
	private bool _picked; // 0x4E0
	private string _sortingLayer; // 0x4E8
	private Action<int> onRecharge; // 0x4F0
	private Action _cleanCustomDisplayer; // 0x4F8
	private GameObject _displayObj; // 0x500
	private Action _updateCustomDisplay; // 0x508
	private static GameObject _InfoPanelObj; // 0x0
}

// --------------------------------------------------------------------
// ArtifactBulletInfoInspector  [class]
// --------------------------------------------------------------------
public class ArtifactBulletInfoInspector : BulletInfoInspector // TypeDefIndex: 13599
{
	// Fields
	public string path; // 0x40
	public emDamageType damageType; // 0x48
	public float damageFactor; // 0x4C
	public float critFactor; // 0x50
	public float repelFactor; // 0x54
	public float throughCountFactor; // 0x58
	public float chargeAddSpeed; // 0x5C
	public float chargeAddCrit; // 0x60
	public float chargeAddDamage; // 0x64
	public float chargeAddSize; // 0x68
	public float chargeAddRepel; // 0x6C
	public int chargeAddThroughCount; // 0x70
	public float godpowerRechargeFactor; // 0x74
}

// --------------------------------------------------------------------
// SpecialBulletInfoIndices  [class]
// --------------------------------------------------------------------
public class SpecialBulletInfoIndices // TypeDefIndex: 13609
{
	// Fields
	public int[] indices; // 0x10
}

// --------------------------------------------------------------------
// YaoQiAttrType  [enum]
// --------------------------------------------------------------------
public enum YaoQiAttrType // TypeDefIndex: 13702
{
	// Fields
	public int value__; // 0x0
	public const YaoQiAttrType Vigor = 0;
	public const YaoQiAttrType Agility = 1;
	public const YaoQiAttrType Stability = 2;
	public const YaoQiAttrType Resourceful = 3;
	public const YaoQiAttrType Impact = 4;
}

// --------------------------------------------------------------------
// GetYaoQiItemEvent  [class]
// --------------------------------------------------------------------
public class GetYaoQiItemEvent // TypeDefIndex: 13704
{
	// Fields
	public RGController controller; // 0x10
	public ARAMProjData data; // 0x18
}

// --------------------------------------------------------------------
// YaoQiStabilityEffectEvent  [class]
// --------------------------------------------------------------------
public class YaoQiStabilityEffectEvent // TypeDefIndex: 13706
{
	// Fields
	public RGController controller; // 0x10
}

// --------------------------------------------------------------------
// ARAMYaoQiEffects  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffects // TypeDefIndex: 13708
{
	// Fields
	public static string EffectColor; // 0x0
	public static string AttrColor; // 0x8
	private static Dictionary<int, List<Aram2YaoQiLevels>> YaoQiLevelsList; // 0x10
}

// --------------------------------------------------------------------
// YaoQiEffectList  [class]
// --------------------------------------------------------------------
public class YaoQiEffectList : CommonUpdateMono.ICustomData // TypeDefIndex: 13709
{
	// Fields
	public List<List<ARAMYaoQiEffect>> effects; // 0x10
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect // TypeDefIndex: 13710
{
	// Fields
	public int effectIndex; // 0x10
	public int duplicateIndex; // 0x14
	public RGController controller; // 0x18
	public ARAMModeData modeData; // 0x20
	public int effectLevel; // 0x28
	private RGRandom _rng; // 0x30
}

// --------------------------------------------------------------------
// ARAMYaoQiEffectWithTrigger  [class]
// --------------------------------------------------------------------
public abstract class ARAMYaoQiEffectWithTrigger : ARAMYaoQiEffect // TypeDefIndex: 13716
{
	// Fields
	protected EffectTrigger _trigger; // 0x38
	protected EffectTrigger _resetTrigger; // 0x40
}

// --------------------------------------------------------------------
// ARAMYaoQiEffectChangeValue  [class]
// --------------------------------------------------------------------
public abstract class ARAMYaoQiEffectChangeValue : ARAMYaoQiEffectWithTrigger // TypeDefIndex: 13717
{
	// Fields
	public int addedValue; // 0x48
	protected FuncObj attrValueGetter; // 0x50
	protected FuncObj attrValueLimitGetter; // 0x58
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect4  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect4 : ARAMYaoQiEffectChangeValue // TypeDefIndex: 13722
{
	// Fields
	private EffectTriggerByTime timer; // 0x60
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect8  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect8 : ARAMYaoQiEffectWithTrigger // TypeDefIndex: 13726
{
	// Fields
	public int times; // 0x48
	public int timesLimit; // 0x4C
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect10  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect10 : ARAMYaoQiEffectWithTrigger // TypeDefIndex: 13728
{
	// Fields
	private bool active; // 0x48
	private float elapsedTime; // 0x4C
	private float lifetimeRemain; // 0x50
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect11  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect11 : ARAMYaoQiEffectWithTrigger // TypeDefIndex: 13729
{
	// Fields
	private int accumulatedEnergy; // 0x48
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect13  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect13 : ARAMYaoQiEffectWithTrigger // TypeDefIndex: 13731
{
	// Fields
	private int currentStacks; // 0x48
	private float remainingDuration; // 0x4C
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect18  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect18 : ARAMYaoQiEffectChangeYaoQiAttribute // TypeDefIndex: 13736
{
	// Fields
	private EffectTriggerByTime timerTrigger; // 0x60
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect19  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect19 : ARAMYaoQiEffect // TypeDefIndex: 13737
{
	// Fields
	private float originalPriceFactor; // 0x38
	private EffectTrigger trigger; // 0x40
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect25  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect25 : ARAMYaoQiEffect // TypeDefIndex: 13744
{
	// Fields
	private DisorderList<TempWeaponManager.MimicWeaponAttackCmd> weaponCmds; // 0x38
	private float cooldownRemain; // 0x40
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect26  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect26 : ARAMYaoQiEffectWithTrigger // TypeDefIndex: 13745
{
	// Fields
	private int damageCount; // 0x48
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect27  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect27 : ARAMYaoQiEffectWithTrigger // TypeDefIndex: 13746
{
	// Fields
	private int killCount; // 0x48
	private int currentDefenseBonus; // 0x4C
	private float remainingDuration; // 0x50
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect29  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect29 : ARAMYaoQiEffectChangeYaoQiAttribute // TypeDefIndex: 13748
{
	// Fields
	private float accumulatedEnergy; // 0x60
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect30  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect30 : ARAMYaoQiEffect // TypeDefIndex: 13749
{
	// Fields
	private int capturedCount; // 0x38
	private List<DeadBodyController> minions; // 0x40
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect31  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect31 : ARAMYaoQiEffect // TypeDefIndex: 13750
{
	// Fields
	private List<float> timers; // 0x38
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect32  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect32 : ARAMYaoQiEffect // TypeDefIndex: 13751
{
	// Fields
	private float moveTimer; // 0x38
	private float addedAgility; // 0x3C
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect33  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect33 : ARAMYaoQiEffect // TypeDefIndex: 13752
{
	// Fields
	private const float BOMBARDMENT_RADIUS = 4;
	private float _lastTime; // 0x38
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect34  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect34 : ARAMYaoQiEffect // TypeDefIndex: 13753
{
	// Fields
	public bool turretCreated; // 0x38
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect36  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect36 : ARAMYaoQiEffect // TypeDefIndex: 13755
{
	// Fields
	private Type typeTag; // 0x38
	private float _lastTime; // 0x40
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect37  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect37 : ARAMYaoQiEffect // TypeDefIndex: 13756
{
	// Fields
	private float currentLifeStealBonus; // 0x38
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect39  [class]
// --------------------------------------------------------------------
private sealed class ARAMYaoQiEffect39.<>c__DisplayClass15_0 // TypeDefIndex: 13758
{
	// Fields
	public GameObject marker; // 0x10
	public ARAMYaoQiEffect39 <>4__this; // 0x18
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect39  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect39 : ARAMYaoQiEffect // TypeDefIndex: 13759
{
	// Fields
	private int storedMissiles; // 0x38
	private const float MISSILE_GAIN_INTERVAL = 0.5;
	private const float MISSILE_FIRE_INTERVAL = 0.1;
	private float missileTimer; // 0x3C
	private int missilesToFire; // 0x40
	private float fireTimer; // 0x44
	private List<GameObject> missileMarkers; // 0x48
	private const float MARKER_RADIUS = 1.15;
	private const float MARKER_MOVE_DURATION = 0.3;
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect40  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect40 : ARAMYaoQiEffect // TypeDefIndex: 13760
{
	// Fields
	private List<NpcSummon01> treants; // 0x38
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect41  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect41 : ARAMYaoQiEffectWithTrigger // TypeDefIndex: 13761
{
	// Fields
	private float cooldownTimer; // 0x48
	private DisorderList<TempWeaponManager.MimicWeaponAttackCmd> weaponCmds; // 0x50
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect42  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect42 : ARAMYaoQiEffect, AreaDamageCarrier.IDamageInfoGetter // TypeDefIndex: 13762
{
	// Fields
	private GameObject shieldObj; // 0x38
	private CircleCollider2D collider; // 0x40
	private bool isActive; // 0x48
	private int shieldAmount; // 0x4C
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect43  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect43 : ARAMYaoQiEffect // TypeDefIndex: 13763
{
	// Fields
	private float attackTimer; // 0x38
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect44  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect44 : ARAMYaoQiEffect // TypeDefIndex: 13764
{
	// Fields
	private int storedStacks; // 0x38
	private int accumulatedEnergy; // 0x3C
	private LookUpRecordList<DamageCarrier, int> enhancedBullets; // 0x40
	private int typeTag; // 0x48
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect45  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect45 : ARAMYaoQiEffect // TypeDefIndex: 13765
{
	// Fields
	private float addedCriticalDamage; // 0x38
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect46  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect46 : ARAMYaoQiEffect // TypeDefIndex: 13766
{
	// Fields
	private float _lastTime; // 0x38
	private const float FROST_SPIKE_INTERVAL = 0.08;
	private float cooldownTimer; // 0x3C
	private int typeTag; // 0x40
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect47  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect47 : ARAMYaoQiEffect // TypeDefIndex: 13767
{
	// Fields
	private int addedArmor; // 0x38
	private DisorderList<TempWeaponManager.MimicWeaponAttackCmd> weaponCmds; // 0x40
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect49  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect49 : ARAMYaoQiEffect // TypeDefIndex: 13769
{
	// Fields
	private float addedSkillHaste; // 0x38
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect50  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect50 : ARAMYaoQiEffect // TypeDefIndex: 13770
{
	// Fields
	private Dictionary<YaoQiAttrType, int> addedAttributes; // 0x38
}

// --------------------------------------------------------------------
// ARAMYaoQiEffect51  [class]
// --------------------------------------------------------------------
public class ARAMYaoQiEffect51 : ARAMYaoQiEffect // TypeDefIndex: 13771
{
	// Fields
	private int addedDefense; // 0x38
}

// --------------------------------------------------------------------
// T2Talents  [class]
// --------------------------------------------------------------------
public sealed class T2Talents : BeanBase // TypeDefIndex: 14051
{
	// Fields
	private string <TalentName>k__BackingField; // 0x10
	private string <NameKey>k__BackingField; // 0x18
	private string <IconName>k__BackingField; // 0x20
	private string <DescKey>k__BackingField; // 0x28
	private string <DefaultText>k__BackingField; // 0x30
	private string <DescParams>k__BackingField; // 0x38
	public const int __ID__ = -1329322085;
}

// --------------------------------------------------------------------
// TbT2Talents  [class]
// --------------------------------------------------------------------
public class TbT2Talents // TypeDefIndex: 14069
{
	// Fields
	private readonly Dictionary<string, T2Talents> _dataMap; // 0x10
	private readonly List<T2Talents> _dataList; // 0x18
}

// --------------------------------------------------------------------
// TalentGroups  [class]
// --------------------------------------------------------------------
public sealed class TalentGroups : BeanBase // TypeDefIndex: 14142
{
	// Fields
	private string <TalentGroupId>k__BackingField; // 0x10
	private List<TalentUnit> <TalentUnit>k__BackingField; // 0x18
	public const int __ID__ = 1476028045;
}

// --------------------------------------------------------------------
// TalentRandomWeightBonus  [class]
// --------------------------------------------------------------------
public sealed class TalentRandomWeightBonus : BeanBase // TypeDefIndex: 14143
{
	// Fields
	private string <Id>k__BackingField; // 0x10
	private int <WeightBonus>k__BackingField; // 0x18
	public const int __ID__ = -1333171861;
}

// --------------------------------------------------------------------
// TalentUnit  [class]
// --------------------------------------------------------------------
public sealed class TalentUnit : BeanBase // TypeDefIndex: 14144
{
	// Fields
	private string <Id>k__BackingField; // 0x10
	private int <Weight>k__BackingField; // 0x18
	public const int __ID__ = 1727086717;
}

// --------------------------------------------------------------------
// TbTalentGroups  [class]
// --------------------------------------------------------------------
public class TbTalentGroups // TypeDefIndex: 14147
{
	// Fields
	private readonly Dictionary<string, TalentGroups> _dataMap; // 0x10
	private readonly List<TalentGroups> _dataList; // 0x18
}

// --------------------------------------------------------------------
// MasterTalentDropConfig  [class]
// --------------------------------------------------------------------
public sealed class MasterTalentDropConfig : BeanBase // TypeDefIndex: 14207
{
	// Fields
	private int <Id>k__BackingField; // 0x10
	private int <GameMode>k__BackingField; // 0x14
	private int <DifficultyGroup>k__BackingField; // 0x18
	private int <BigLevel>k__BackingField; // 0x1C
	private int <SmallLevel>k__BackingField; // 0x20
	private int <RatePermyriad>k__BackingField; // 0x24
	private int <DropCount>k__BackingField; // 0x28
	private int <Enabled>k__BackingField; // 0x2C
	private string <Comment>k__BackingField; // 0x30
	public const int __ID__ = -2123125073;
}

// --------------------------------------------------------------------
// MasterTalentHeroPoolOverrideConfig  [class]
// --------------------------------------------------------------------
public sealed class MasterTalentHeroPoolOverrideConfig : BeanBase // TypeDefIndex: 14208
{
	// Fields
	private int <Id>k__BackingField; // 0x10
	private int <HeroId>k__BackingField; // 0x14
	private int <BuffId>k__BackingField; // 0x18
	private string <BuffName>k__BackingField; // 0x20
	private int <RuleType>k__BackingField; // 0x28
	private int <Weight>k__BackingField; // 0x2C
	private int <Enabled>k__BackingField; // 0x30
	private string <Comment>k__BackingField; // 0x38
	public const int __ID__ = 1122404770;
}

// --------------------------------------------------------------------
// MasterTalentPoolConfig  [class]
// --------------------------------------------------------------------
public sealed class MasterTalentPoolConfig : BeanBase // TypeDefIndex: 14209
{
	// Fields
	private int <Id>k__BackingField; // 0x10
	private int <BuffId>k__BackingField; // 0x14
	private string <BuffName>k__BackingField; // 0x18
	private int <Weight>k__BackingField; // 0x20
	private int <Enabled>k__BackingField; // 0x24
	private string <Comment>k__BackingField; // 0x28
	public const int __ID__ = 1686028828;
}

// --------------------------------------------------------------------
// TbMasterTalentDropConfig  [class]
// --------------------------------------------------------------------
public class TbMasterTalentDropConfig // TypeDefIndex: 14214
{
	// Fields
	private readonly Dictionary<int, MasterTalentDropConfig> _dataMap; // 0x10
	private readonly List<MasterTalentDropConfig> _dataList; // 0x18
}

// --------------------------------------------------------------------
// TbMasterTalentHeroPoolOverrideConfig  [class]
// --------------------------------------------------------------------
public class TbMasterTalentHeroPoolOverrideConfig // TypeDefIndex: 14215
{
	// Fields
	private readonly Dictionary<int, MasterTalentHeroPoolOverrideConfig> _dataMap; // 0x10
	private readonly List<MasterTalentHeroPoolOverrideConfig> _dataList; // 0x18
}

// --------------------------------------------------------------------
// TbMasterTalentPoolConfig  [class]
// --------------------------------------------------------------------
public class TbMasterTalentPoolConfig // TypeDefIndex: 14216
{
	// Fields
	private readonly Dictionary<int, MasterTalentPoolConfig> _dataMap; // 0x10
	private readonly List<MasterTalentPoolConfig> _dataList; // 0x18
}

// --------------------------------------------------------------------
// Aram2YaoQiEffects  [class]
// --------------------------------------------------------------------
public sealed class Aram2YaoQiEffects : BeanBase // TypeDefIndex: 14452
{
	// Fields
	private int <Id>k__BackingField; // 0x10
	private int <MaxStack>k__BackingField; // 0x14
	private int <Rarity>k__BackingField; // 0x18
	public const int __ID__ = -1517053465;
}

// --------------------------------------------------------------------
// Aram2YaoQiLevels  [class]
// --------------------------------------------------------------------
public sealed class Aram2YaoQiLevels : BeanBase // TypeDefIndex: 14453
{
	// Fields
	private int <Level>k__BackingField; // 0x10
	private int <Value>k__BackingField; // 0x14
	private int <EnhanceProbability>k__BackingField; // 0x18
	private int <Speed>k__BackingField; // 0x1C
	private int <Damage>k__BackingField; // 0x20
	private int <RandomGene>k__BackingField; // 0x24
	private int <Amount>k__BackingField; // 0x28
	private List<int> <EffectRarityWeights>k__BackingField; // 0x30
	private string <DescKey>k__BackingField; // 0x38
	public const int __ID__ = 428114922;
}

// --------------------------------------------------------------------
// TbAram2YaoQiEffects  [class]
// --------------------------------------------------------------------
public class TbAram2YaoQiEffects // TypeDefIndex: 14476
{
	// Fields
	private readonly Dictionary<int, Aram2YaoQiEffects> _dataMap; // 0x10
	private readonly List<Aram2YaoQiEffects> _dataList; // 0x18
}

// --------------------------------------------------------------------
// TbAram2YaoQiLevels  [class]
// --------------------------------------------------------------------
public class TbAram2YaoQiLevels // TypeDefIndex: 14477
{
	// Fields
	private readonly Dictionary<int, Aram2YaoQiLevels> _dataMap; // 0x10
	private readonly List<Aram2YaoQiLevels> _dataList; // 0x18
}

// --------------------------------------------------------------------
// TalentBuffTagConfig  [class]
// --------------------------------------------------------------------
public sealed class TalentBuffTagConfig : BeanBase // TypeDefIndex: 14501
{
	// Fields
	private int <RowID>k__BackingField; // 0x10
	private string <BuffID>k__BackingField; // 0x18
	private string <Name>k__BackingField; // 0x20
	private int <Weight>k__BackingField; // 0x28
	private List<int> <TagID>k__BackingField; // 0x30
	private string <BuffType>k__BackingField; // 0x38
	private List<string> <ExclusiveBuffID>k__BackingField; // 0x40
	private string <Key>k__BackingField; // 0x48
	public const int __ID__ = -1166211766;
}

// --------------------------------------------------------------------
// TalentConstConfig  [class]
// --------------------------------------------------------------------
public sealed class TalentConstConfig : BeanBase // TypeDefIndex: 14502
{
	// Fields
	private string <Name>k__BackingField; // 0x10
	private string <Type>k__BackingField; // 0x18
	private string <Value>k__BackingField; // 0x20
	private List<float> <ArrayValue>k__BackingField; // 0x28
	public const int __ID__ = -246543962;
}

// --------------------------------------------------------------------
// TalentRewardConfig  [class]
// --------------------------------------------------------------------
public sealed class TalentRewardConfig : BeanBase // TypeDefIndex: 14503
{
	// Fields
	private int <TrophyLootID>k__BackingField; // 0x10
	private int <TrophyLevel>k__BackingField; // 0x14
	private int <LevelExp>k__BackingField; // 0x18
	private string <LootId>k__BackingField; // 0x20
	private int <LimitTime>k__BackingField; // 0x28
	private int <RefreshExp>k__BackingField; // 0x2C
	public const int __ID__ = -1147549104;
}

// --------------------------------------------------------------------
// TalentTagConfig  [class]
// --------------------------------------------------------------------
public sealed class TalentTagConfig : BeanBase // TypeDefIndex: 14504
{
	// Fields
	private string <RowID>k__BackingField; // 0x10
	private string <Name>k__BackingField; // 0x18
	private string <TagID>k__BackingField; // 0x20
	public const int __ID__ = -1777917731;
}

// --------------------------------------------------------------------
// TalentTaskConfig  [class]
// --------------------------------------------------------------------
public sealed class TalentTaskConfig : BeanBase // TypeDefIndex: 14505
{
	// Fields
	private int <TaskID>k__BackingField; // 0x10
	private string <DefaultDesc>k__BackingField; // 0x18
	private string <LocalKey>k__BackingField; // 0x20
	private int <TaskCheckerParam0>k__BackingField; // 0x28
	private int <TaskAward>k__BackingField; // 0x2C
	public const int __ID__ = 39898086;
}

// --------------------------------------------------------------------
// TbTalentConstConfig  [class]
// --------------------------------------------------------------------
public class TbTalentConstConfig // TypeDefIndex: 14511
{
	// Fields
	private readonly Dictionary<string, TalentConstConfig> _dataMap; // 0x10
	private readonly List<TalentConstConfig> _dataList; // 0x18
}

// --------------------------------------------------------------------
// TbTalentTagConfig  [class]
// --------------------------------------------------------------------
public class TbTalentTagConfig // TypeDefIndex: 14512
{
	// Fields
	private readonly Dictionary<int, TalentBuffTagConfig> _dataMap; // 0x10
	private readonly List<TalentBuffTagConfig> _dataList; // 0x18
}

// --------------------------------------------------------------------
// NewMechaRoleAttributesConfig  [class]
// --------------------------------------------------------------------
public sealed class NewMechaRoleAttributesConfig : BeanBase // TypeDefIndex: 14569
{
	// Fields
	private string <AttributeId>k__BackingField; // 0x10
	private string <DisplayName>k__BackingField; // 0x18
	private string <DefaultValue>k__BackingField; // 0x20
	private string <Description>k__BackingField; // 0x28
	public const int __ID__ = 2058204196;
}

// --------------------------------------------------------------------
// NewMechaTalentsConfig  [class]
// --------------------------------------------------------------------
public sealed class NewMechaTalentsConfig : BeanBase // TypeDefIndex: 14573
{
	// Fields
	private string <TalentId>k__BackingField; // 0x10
	private string <IsCommon>k__BackingField; // 0x18
	private string <Param0>k__BackingField; // 0x20
	private string <Param1>k__BackingField; // 0x28
	private string <Param2>k__BackingField; // 0x30
	private string <Param3>k__BackingField; // 0x38
	public const int __ID__ = -541894476;
}

// --------------------------------------------------------------------
// TbNewMechaRoleAttributesConfig  [class]
// --------------------------------------------------------------------
public class TbNewMechaRoleAttributesConfig // TypeDefIndex: 14591
{
	// Fields
	private readonly List<NewMechaRoleAttributesConfig> _dataList; // 0x10
}

// --------------------------------------------------------------------
// TbNewMechaTalentsConfig  [class]
// --------------------------------------------------------------------
public class TbNewMechaTalentsConfig // TypeDefIndex: 14595
{
	// Fields
	private readonly List<NewMechaTalentsConfig> _dataList; // 0x10
}

// --------------------------------------------------------------------
// ActivityGunMasterTalentConfig  [class]
// --------------------------------------------------------------------
public sealed class ActivityGunMasterTalentConfig : BeanBase // TypeDefIndex: 14647
{
	// Fields
	private int <Id>k__BackingField; // 0x10
	private string <Type>k__BackingField; // 0x18
	private string <TitleFallback>k__BackingField; // 0x20
	private int <Rarity>k__BackingField; // 0x28
	private bool <Overlay>k__BackingField; // 0x2C
	private string <Param>k__BackingField; // 0x30
	private string <RealEffect>k__BackingField; // 0x38
	public const int __ID__ = -364698958;
}

// --------------------------------------------------------------------
// TbActivityGunMasterTalentConfig  [class]
// --------------------------------------------------------------------
public class TbActivityGunMasterTalentConfig // TypeDefIndex: 14648
{
	// Fields
	private readonly Dictionary<int, ActivityGunMasterTalentConfig> _dataMap; // 0x10
	private readonly List<ActivityGunMasterTalentConfig> _dataList; // 0x18
}

// --------------------------------------------------------------------
// ForcePatchConst  [class]
// --------------------------------------------------------------------
public static class ForcePatchConst // TypeDefIndex: 16163
{
	// Fields
	public const string CodePatchFileName = "code.bytes";
	public const string LubanPatchZipFileName = "patch_luban_config.zip";
	public const string LocalizeZipFileName = "localize.zip";
	public const string RuntimeLuaZipFileName = "runtime_lua.zip";
	public const string LocalizeFileName = "localize.csv";
	public const string LocalizeName = "localize";
	public const string LocalizeFolderName = "loc";
	public const string ForcePatchAnnouncementConfigFileName = "ForcePatchAnnouncementInfoConfig.json";
	public const string DynamicConfigFileName = "DynamicConfig.json";
	public const string ManifestFileName = "manifest.json";
	public const long Byte2Kb = 1024;
	public const long Byte2Mb = 1048576;
	public static readonly string LocalFileRoot; // 0x0
	public static readonly string PatchLubanLocalRoot; // 0x8
	public static readonly string LubanManifestPath; // 0x10
	public static readonly string PatchLocalizeLocalRoot; // 0x18
	public static readonly string LocalizeManifestPath; // 0x20
	public const string EncryptKey = "_@sT0r3SFif95";
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.Candidate // TypeDefIndex: 16330
{
	// Fields
	public emBuff Talent; // 0x10
	public int PoolType; // 0x14
	public float BaseWeight; // 0x18
	public List<int> TagIds; // 0x20
	public List<emBuff> ExclusiveTalents; // 0x28
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
public sealed class NewTalentRandomManager.TagValue // TypeDefIndex: 16331
{
	// Fields
	public int TagId; // 0x10
	public float Value; // 0x14
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
public sealed class NewTalentRandomManager.TalentWeightDebugEntry // TypeDefIndex: 16332
{
	// Fields
	public emBuff Talent; // 0x10
	public float BaseWeight; // 0x14
	public float EarlyCoefficient; // 0x18
	public float CorrelationWeight; // 0x1C
	public float ProgressWeight; // 0x20
	public float RecentWeight; // 0x24
	public float Weight; // 0x28
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
public sealed class NewTalentRandomManager.TalentSelectionDebugInfo // TypeDefIndex: 16333
{
	// Fields
	public int Round; // 0x10
	public string Pool; // 0x18
	public int CandidateCount; // 0x20
	public float TotalWeight; // 0x24
	public float RandomTarget; // 0x28
	public emBuff SelectedTalent; // 0x2C
	public float SelectedWeight; // 0x30
	public NewTalentRandomManager.TalentWeightDebugEntry SelectedWeightDetails; // 0x38
	public List<NewTalentRandomManager.TagValue> ContextTags; // 0x40
	public Dictionary<int, int> OwnedTagCounts; // 0x48
	public List<NewTalentRandomManager.TalentWeightDebugEntry> TopCandidates; // 0x50
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c // TypeDefIndex: 16334
{
	// Fields
	public static readonly NewTalentRandomManager.<>c <>9; // 0x0
	public static Func<NewTalentRandomManager.Candidate, emBuff> <>9__24_0; // 0x8
	public static Func<NewTalentRandomManager.TagValue, NewTalentRandomManager.TagValue> <>9__24_7; // 0x10
	public static Func<emBuff, bool> <>9__26_0; // 0x18
	public static Func<int, emBuff> <>9__28_0; // 0x20
	public static Func<int, emBuff> <>9__28_1; // 0x28
	public static Func<WeaponTagConfig, NewTalentRandomManager.TagValue> <>9__29_5; // 0x30
	public static Func<WeaponTypeTagConfig, NewTalentRandomManager.TagValue> <>9__29_9; // 0x38
	public static Func<WeaponTypeTagConfig, NewTalentRandomManager.TagValue> <>9__29_2; // 0x40
	public static Func<int, emBuff> <>9__31_0; // 0x48
	public static Func<ValueTuple<NewTalentRandomManager.Candidate, float>, float> <>9__34_0; // 0x50
	public static Func<ValueTuple<NewTalentRandomManager.Candidate, float>, float> <>9__34_1; // 0x58
	public static Func<ValueTuple<NewTalentRandomManager.Candidate, float>, int> <>9__34_2; // 0x60
	public static Func<KeyValuePair<int, int>, int> <>9__39_0; // 0x68
	public static Func<KeyValuePair<int, int>, string> <>9__39_1; // 0x70
	public static Func<NewTalentRandomManager.TagValue, float> <>9__44_1; // 0x78
	public static Func<emBuff, int> <>9__53_0; // 0x80
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass24_0 // TypeDefIndex: 16335
{
	// Fields
	public HashSet<emBuff> selectedSet; // 0x10
	public Dictionary<emBuff, NewTalentRandomManager.Candidate> candidateMap; // 0x18
	public List<NewTalentRandomManager.TagValue> contextTags; // 0x20
	public Func<NewTalentRandomManager.Candidate, bool> <>9__2; // 0x28
	public Func<NewTalentRandomManager.Candidate, bool> <>9__3; // 0x30
	public Func<NewTalentRandomManager.Candidate, bool> <>9__5; // 0x38
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass24_1 // TypeDefIndex: 16336
{
	// Fields
	public HashSet<emBuff> excludedTalentSet; // 0x10
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass24_2 // TypeDefIndex: 16337
{
	// Fields
	public HashSet<int> pool; // 0x10
	public NewTalentRandomManager.<>c__DisplayClass24_0 CS$<>8__locals1; // 0x18
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass24_3 // TypeDefIndex: 16338
{
	// Fields
	public NewTalentRandomManager.Candidate candidate; // 0x10
	public NewTalentRandomManager.<>c__DisplayClass24_0 CS$<>8__locals2; // 0x18
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass26_0 // TypeDefIndex: 16339
{
	// Fields
	public Dictionary<emBuff, NewTalentRandomManager.Candidate> candidateMap; // 0x10
	public List<NewTalentRandomManager.TagValue> contextTags; // 0x18
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass27_0 // TypeDefIndex: 16340
{
	// Fields
	public HashSet<int> validTagIds; // 0x10
	public Func<int, bool> <>9__0; // 0x18
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass28_0 // TypeDefIndex: 16341
{
	// Fields
	public HashSet<int> validTagIds; // 0x10
	public Dictionary<emBuff, List<emBuff>> exclusivityMap; // 0x18
	public Func<int, bool> <>9__2; // 0x20
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass28_1 // TypeDefIndex: 16342
{
	// Fields
	public emBuff talent; // 0x10
	public NewTalentRandomManager.<>c__DisplayClass28_0 CS$<>8__locals1; // 0x18
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass29_0 // TypeDefIndex: 16343
{
	// Fields
	public HashSet<int> validTagIds; // 0x10
	public string skillId; // 0x18
	public Func<WeaponTagConfig, bool> <>9__4; // 0x20
	public Func<WeaponTypeTagConfig, bool> <>9__8; // 0x28
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass29_1 // TypeDefIndex: 16344
{
	// Fields
	public string weaponId; // 0x10
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass29_2 // TypeDefIndex: 16345
{
	// Fields
	public string typeName; // 0x10
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass33_0 // TypeDefIndex: 16346
{
	// Fields
	public bool buildPool; // 0x10
	public bool relatedOnly; // 0x11
	public List<NewTalentRandomManager.TagValue> contextTags; // 0x18
	public BattleData data; // 0x20
	public Dictionary<int, int> ownedTagCounts; // 0x28
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass34_0 // TypeDefIndex: 16347
{
	// Fields
	public BattleData data; // 0x10
	public List<NewTalentRandomManager.TagValue> contextTags; // 0x18
	public Dictionary<int, int> ownedTagCounts; // 0x20
	public bool ignoreRecent; // 0x28
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass42_0 // TypeDefIndex: 16348
{
	// Fields
	public int tagId; // 0x10
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass44_0 // TypeDefIndex: 16349
{
	// Fields
	public NewTalentRandomManager.Candidate candidate; // 0x10
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass48_0 // TypeDefIndex: 16350
{
	// Fields
	public List<NewTalentRandomManager.TagValue> contextTags; // 0x10
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
private sealed class NewTalentRandomManager.<>c__DisplayClass49_0 // TypeDefIndex: 16351
{
	// Fields
	public NewTalentRandomManager.Candidate candidate; // 0x10
}

// --------------------------------------------------------------------
// NewTalentRandomManager  [class]
// --------------------------------------------------------------------
public static class NewTalentRandomManager // TypeDefIndex: 16352
{
	// Fields
	private const int CommonType = 1;
	private const int EarlyCommonType = 2;
	private const int BuildType = 0;
	private const int WeaponTargetType = 0;
	private const int SkillTargetType = 1;
	private const string DebugLog = "NewTalentRandomManager>";
	private const float DefaultTalentWeight = 10;
	private const int DebugWeightPreviewCount = 10;
	private static bool <EnableWeightDebugLogging>k__BackingField; // 0x0
	private static string <LastWeightDebugInfo>k__BackingField; // 0x8
	private static IReadOnlyList<NewTalentRandomManager.TalentSelectionDebugInfo> <LastWeightDebugSelections>k__BackingField; // 0x10
}

// --------------------------------------------------------------------
// TalentRandomPool  [class]
// --------------------------------------------------------------------
private sealed class TalentRandomPool.<>c // TypeDefIndex: 16354
{
	// Fields
	public static readonly TalentRandomPool.<>c <>9; // 0x0
	public static Func<emBuff, ValueTuple<emBuff, bool>> <>9__7_0; // 0x8
}

// --------------------------------------------------------------------
// TalentRandomPool  [class]
// --------------------------------------------------------------------
public class TalentRandomPool : RandomPool // TypeDefIndex: 16355
{
	// Fields
	private const string DebugLog = "PseudoRandomSystem>TalentRandomPool>";
	public const string NextLevelTalentSelectionContext = "NextLevelTalentSelection";
}

// --------------------------------------------------------------------
// TalentRandomPoolNewPlayerLevelRandomStrategy  [class]
// --------------------------------------------------------------------
private sealed class TalentRandomPoolNewPlayerLevelRandomStrategy.<>c__DisplayClass7_0 // TypeDefIndex: 16362
{
	// Fields
	public Random rng; // 0x10
}

// --------------------------------------------------------------------
// TalentRandomPoolNewPlayerLevelRandomStrategy  [class]
// --------------------------------------------------------------------
internal class TalentRandomPoolNewPlayerLevelRandomStrategy : IRandomStrategy // TypeDefIndex: 16363
{
	// Fields
	private const string DebugLog = "PseudoRandomSystem>TalentPseudoRandomPool>NewPlayerLevelRandomStrategy>";
}

// --------------------------------------------------------------------
// TalentRandomPoolNoobRandomStrategy  [class]
// --------------------------------------------------------------------
internal class TalentRandomPoolNoobRandomStrategy : IRandomStrategy // TypeDefIndex: 16365
{
	// Fields
	private Dictionary<emBuff, int> _talentRandomPool; // 0x10
	private const string DebugLog = "PseudoRandomSystem>TalentPseudoRandomPool>NoobPseudoRandomStrategy>";
}

// --------------------------------------------------------------------
// TalentRandomPoolOldRandomStrategy  [class]
// --------------------------------------------------------------------
internal class TalentRandomPoolOldRandomStrategy : IRandomStrategy // TypeDefIndex: 16367
{
	// Fields
	private const string DebugLog = "PseudoRandomSystem>TalentPseudoRandomPool>OldRandomStrategy>";
}

// --------------------------------------------------------------------
// TalentRandomPoolVoidInvasion2RandomStrategy  [class]
// --------------------------------------------------------------------
internal class TalentRandomPoolVoidInvasion2RandomStrategy : IRandomStrategy // TypeDefIndex: 16368
{
	// Fields
	private const string DebugLog = "PseudoRandomSystem>TalentPseudoRandomPool>VoidInvasion2RandomStrategy>";
	private const float VoidUpgradeBuffProb = 0.1;
}

// --------------------------------------------------------------------
// BuffInfo  [struct]
// --------------------------------------------------------------------
internal struct BuffInfo // TypeDefIndex: 16421
{
	// Fields
	public emBuff Buff; // 0x0
	public BasePlayerBuff PlayerBuff; // 0x8
}

// --------------------------------------------------------------------
// GlobalizeManager  [class]
// --------------------------------------------------------------------
private sealed class GlobalizeManager.<>c // TypeDefIndex: 16602
{
	// Fields
	public static readonly GlobalizeManager.<>c <>9; // 0x0
	public static Func<IGlobalize, string> <>9__11_0; // 0x8
	public static Func<IGlobalize, IGlobalize> <>9__11_1; // 0x10
	public static Func<IGlobalize, string> <>9__11_2; // 0x18
	public static Func<IGlobalize, IGlobalize> <>9__11_3; // 0x20
	public static Func<string, string> <>9__24_0; // 0x28
}

// --------------------------------------------------------------------
// GlobalizeManager  [class]
// --------------------------------------------------------------------
private sealed class GlobalizeManager.<>c__DisplayClass12_0 // TypeDefIndex: 16603
{
	// Fields
	public string name; // 0x10
}

// --------------------------------------------------------------------
// GlobalizeManager  [class]
// --------------------------------------------------------------------
public class GlobalizeManager : Singleton<GlobalizeManager>, Init // TypeDefIndex: 16604
{
	// Fields
	private bool <IsInit>k__BackingField; // 0x10
	private const string path = "SoulKnight/Runtime/Config2Code/EncryptedCsv/globalize.csv";
	private const string SecretKey = "soulKnight";
	private List<IGlobalize> _data; // 0x18
	private const string LanguageRegex = "^language_(.+)";
	private const string LanguagesPrefix = "languages";
	private const string LanguagesRegex = "^languages_[(.+)]";
	private const string PackageRegex = "^package_(.+)";
	private const string PackagesPrefix = "packages";
	private const string PackagesRegex = "^packages_[(.+)]";
	private const string ChannelRegex = "^channel_(.+)";
	private const string ChannelsPrefix = "channels";
	private const string ChannelsRegex = "^channels_[(.+)]";
	private const string ArraysRegex = "^(.+)_[(.+)]";
}

// --------------------------------------------------------------------
// StateSyncRgWeapon  [struct]
// --------------------------------------------------------------------
private struct StateSyncRgWeapon.PickUpWeaponData // TypeDefIndex: 16684
{
	// Fields
	public bool CanEvolute; // 0x0
	public bool CanChangeSkin; // 0x1
	public bool CanPickUp; // 0x2
	public int SkinIndex; // 0x4
}

// --------------------------------------------------------------------
// StateSyncRgWeapon  [struct]
// --------------------------------------------------------------------
private struct StateSyncRgWeapon.FusionWeaponData // TypeDefIndex: 16685
{
	// Fields
	public bool CanFusion; // 0x0
	public uint StateSyncId; // 0x4
}

// --------------------------------------------------------------------
// StateSyncRgWeapon  [class]
// --------------------------------------------------------------------
private sealed class StateSyncRgWeapon.<>c__DisplayClass12_0 // TypeDefIndex: 16686
{
	// Fields
	public StateSyncRgWeapon <>4__this; // 0x10
	public RGController sourcePlayer; // 0x18
	public StateSyncRgWeapon.FusionWeaponData fusionWeaponData; // 0x20
}

// --------------------------------------------------------------------
// StateSyncRgWeapon  [class]
// --------------------------------------------------------------------
public class StateSyncRgWeapon : StateSyncObject // TypeDefIndex: 16687
{
	// Fields
	private RGWeapon _rgWeapon; // 0x30
	private Dictionary<StateEnum, StateSyncStrategy> <StateSyncStrategyConfig>k__BackingField; // 0x38
}

// --------------------------------------------------------------------
// RgWeaponObjectData  [struct]
// --------------------------------------------------------------------
public struct RgWeaponObjectData : IObjectData // TypeDefIndex: 16703
{
	// Fields
	public string WeaponId; // 0x0
	public emWeaponSource WeaponSource; // 0x8
	private string <Name>k__BackingField; // 0x10
	private string <ParentPath>k__BackingField; // 0x18
	private Vector3Data <Position>k__BackingField; // 0x20
	private StateEnum <State>k__BackingField; // 0x2C
}

// --------------------------------------------------------------------
// GameConfigManager  [class]
// --------------------------------------------------------------------
public class GameConfigManager // TypeDefIndex: 17368
{
	// Fields
	private static GameConfigManager _instance; // 0x0
	private GameConfig <Config>k__BackingField; // 0x10
	public const string UseDebugConfigInEditorKey = "useDebugConfigInEditor";
	public const string UseDebugConfigInBuildKey = "useDebugConfigInBuild";
	public const string OpenDebugConsoleKey = "OpenDebugConsole";
}

// --------------------------------------------------------------------
// MultiLayerBuffBase  [class]
// --------------------------------------------------------------------
private sealed class MultiLayerBuffBase.<ReduceLayerCoroutine>d__11 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 17534
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public MultiLayerBuffBase <>4__this; // 0x20
}

// --------------------------------------------------------------------
// MultiLayerBuffBase  [class]
// --------------------------------------------------------------------
public abstract class MultiLayerBuffBase : RGBuff // TypeDefIndex: 17535
{
	// Fields
	protected int maxLayers; // 0xC0
	protected int currentLayers; // 0xC4
	protected float layerDuration; // 0xC8
	protected RGEController AttachEnemy; // 0xD0
	private Coroutine _reduceLayerCoroutine; // 0xD8
}

// --------------------------------------------------------------------
// RGBuffStackSpatter  [class]
// --------------------------------------------------------------------
public class RGBuffStackSpatter : RGBuff, OffensiveInterface // TypeDefIndex: 17540
{
	// Fields
	public float spacing; // 0xC0
	public float bulletSpeed; // 0xC4
	public float stackExtraBulletPosY; // 0xC8
	public float stackExtraBulletSize; // 0xCC
	public float spatterBulletSize; // 0xD0
	private int _maxStackCount; // 0xD4
	private int _maxSpatterBulletCount; // 0xD8
	private int _stackCount; // 0xDC
	private RGEController _enemy; // 0xE0
	private GameObject _bulletProto; // 0xE8
	private List<SpriteRenderer> _sortSpriteRenderers; // 0xF0
	private List<SpriteRenderer> _allSpriteRenderers; // 0xF8
	private float _iconYOffset; // 0x100
	private int _stackExtraDmg; // 0x104
	private int _spatterDmg; // 0x108
	private int _spatterBulletLockRadius; // 0x10C
	private int _critical; // 0x110
	private GameObject sourceObj; // 0x118
}

// --------------------------------------------------------------------
// BuffStackCfgItem  [class]
// --------------------------------------------------------------------
public abstract class BuffStackCfgItem<TItem, TParam> : IBuffStackCfgItem // TypeDefIndex: 17653
{
	// Fields
	public List<TParam> paramList; // 0x0
}

// --------------------------------------------------------------------
// BuffStackConfigList  [class]
// --------------------------------------------------------------------
public class BuffStackConfigList // TypeDefIndex: 17654
{
	// Fields
	public List<IBuffStackCfgItem> list; // 0x10
}

// --------------------------------------------------------------------
// BuffStackCfgExpertForce  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgExpertForce.Param // TypeDefIndex: 17656
{
	// Fields
	public float addRange; // 0x0
	public float bulletSpeed; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgExpertForce  [class]
// --------------------------------------------------------------------
public class BuffStackCfgExpertForce : BuffStackCfgItem<BuffStackCfgExpertForce, BuffStackCfgExpertForce.Param> // TypeDefIndex: 17657
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMasterShotgun  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgMasterShotgun.Param // TypeDefIndex: 17658
{
	// Fields
	public int addCount; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMasterShotgun  [class]
// --------------------------------------------------------------------
public class BuffStackCfgMasterShotgun : BuffStackCfgItem<BuffStackCfgMasterShotgun, BuffStackCfgMasterShotgun.Param> // TypeDefIndex: 17659
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMasterContinuous  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgMasterContinuous.Param // TypeDefIndex: 17660
{
	// Fields
	public int addCount; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMasterContinuous  [class]
// --------------------------------------------------------------------
public class BuffStackCfgMasterContinuous : BuffStackCfgItem<BuffStackCfgMasterContinuous, BuffStackCfgMasterContinuous.Param> // TypeDefIndex: 17661
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMasterSwordRange  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgMasterSwordRange.Param // TypeDefIndex: 17662
{
	// Fields
	public float addRangeFactor; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMasterSwordRange  [class]
// --------------------------------------------------------------------
public class BuffStackCfgMasterSwordRange : BuffStackCfgItem<BuffStackCfgMasterSwordRange, BuffStackCfgMasterSwordRange.Param> // TypeDefIndex: 17663
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgShopDiscount  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgShopDiscount.Param // TypeDefIndex: 17664
{
	// Fields
	public float discount; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgShopDiscount  [class]
// --------------------------------------------------------------------
public class BuffStackCfgShopDiscount : BuffStackCfgItem<BuffStackCfgShopDiscount, BuffStackCfgShopDiscount.Param> // TypeDefIndex: 17665
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgWhirlWindSlash  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgWhirlWindSlash.Param // TypeDefIndex: 17666
{
	// Fields
	public float cooldown; // 0x0
	public float size; // 0x4
	public int damage; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgWhirlWindSlash  [class]
// --------------------------------------------------------------------
public class BuffStackCfgWhirlWindSlash : BuffStackCfgItem<BuffStackCfgWhirlWindSlash, BuffStackCfgWhirlWindSlash.Param> // TypeDefIndex: 17667
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgExtraEmitBullet  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgExtraEmitBullet.Param // TypeDefIndex: 17668
{
	// Fields
	public float probability; // 0x0
	public float sizeFactor; // 0x4
	public float damageFactor; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgExtraEmitBullet  [class]
// --------------------------------------------------------------------
public class BuffStackCfgExtraEmitBullet : BuffStackCfgItem<BuffStackCfgExtraEmitBullet, BuffStackCfgExtraEmitBullet.Param> // TypeDefIndex: 17669
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgCyclicElementEnhance  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgCyclicElementEnhance.Param // TypeDefIndex: 17670
{
	// Fields
	public float duration; // 0x0
	public float addDamagePercent; // 0x4
	public float missileTriggerRate; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgCyclicElementEnhance  [class]
// --------------------------------------------------------------------
public class BuffStackCfgCyclicElementEnhance : BuffStackCfgItem<BuffStackCfgCyclicElementEnhance, BuffStackCfgCyclicElementEnhance.Param> // TypeDefIndex: 17671
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMoveEnhanceAttack  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgMoveEnhanceAttack.Param // TypeDefIndex: 17672
{
	// Fields
	public float distance; // 0x0
	public int addDamage; // 0x4
	public int addCritic; // 0x8
	public float addBulletScale; // 0xC
	public float dizzyTime; // 0x10
}

// --------------------------------------------------------------------
// BuffStackCfgMoveEnhanceAttack  [class]
// --------------------------------------------------------------------
public class BuffStackCfgMoveEnhanceAttack : BuffStackCfgItem<BuffStackCfgMoveEnhanceAttack, BuffStackCfgMoveEnhanceAttack.Param> // TypeDefIndex: 17673
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgExEnergyForExDamage  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgExEnergyForExDamage.Param // TypeDefIndex: 17674
{
	// Fields
	public float extraEnergy; // 0x0
	public float extraDamage; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgExEnergyForExDamage  [class]
// --------------------------------------------------------------------
public class BuffStackCfgExEnergyForExDamage : BuffStackCfgItem<BuffStackCfgExEnergyForExDamage, BuffStackCfgExEnergyForExDamage.Param> // TypeDefIndex: 17675
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgPetCharge  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgPetCharge.Param // TypeDefIndex: 17676
{
	// Fields
	public float addAttackSpeedFactor; // 0x0
	public float addDamageFactor; // 0x4
	public float addMoveSpeedFactor; // 0x8
	public float duration; // 0xC
}

// --------------------------------------------------------------------
// BuffStackCfgPetCharge  [class]
// --------------------------------------------------------------------
public class BuffStackCfgPetCharge : BuffStackCfgItem<BuffStackCfgPetCharge, BuffStackCfgPetCharge.Param> // TypeDefIndex: 17677
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgBullyTheWeak  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgBullyTheWeak.Param // TypeDefIndex: 17678
{
	// Fields
	public float addDuration; // 0x0
	public float addDamageFactor; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgBullyTheWeak  [class]
// --------------------------------------------------------------------
public class BuffStackCfgBullyTheWeak : BuffStackCfgItem<BuffStackCfgBullyTheWeak, BuffStackCfgBullyTheWeak.Param> // TypeDefIndex: 17679
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgPremeditate  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgPremeditate.Param // TypeDefIndex: 17680
{
	// Fields
	public int addSkillLevel; // 0x0
	public float addAtkSpdFactor; // 0x4
	public int addCrit; // 0x8
	public float addWeaponDmgFactor; // 0xC
	public float duration; // 0x10
	public float atkAddCounter; // 0x14
	public float moveAddCounter; // 0x18
	public int maxCount; // 0x1C
}

// --------------------------------------------------------------------
// BuffStackCfgPremeditate  [class]
// --------------------------------------------------------------------
public class BuffStackCfgPremeditate : BuffStackCfgItem<BuffStackCfgPremeditate, BuffStackCfgPremeditate.Param> // TypeDefIndex: 17681
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgConsumeRecycle  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgConsumeRecycle.Param // TypeDefIndex: 17682
{
	// Fields
	public int energy; // 0x0
	public float addDmgFactor; // 0x4
	public float addAtkSpdFactor; // 0x8
	public float addAoeRange; // 0xC
	public int maxCount; // 0x10
	public float duration; // 0x14
}

// --------------------------------------------------------------------
// BuffStackCfgConsumeRecycle  [class]
// --------------------------------------------------------------------
public class BuffStackCfgConsumeRecycle : BuffStackCfgItem<BuffStackCfgConsumeRecycle, BuffStackCfgConsumeRecycle.Param> // TypeDefIndex: 17683
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgAddDefWithHighEnergy  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgAddDefWithHighEnergy.Param // TypeDefIndex: 17684
{
	// Fields
	public float energyThreshold; // 0x0
	public int addDefence; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgAddDefWithHighEnergy  [class]
// --------------------------------------------------------------------
public class BuffStackCfgAddDefWithHighEnergy : BuffStackCfgItem<BuffStackCfgAddDefWithHighEnergy, BuffStackCfgAddDefWithHighEnergy.Param> // TypeDefIndex: 17685
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgAddHealthRegen  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgAddHealthRegen.Param // TypeDefIndex: 17686
{
	// Fields
	public float addHealthRegen; // 0x0
	public int addEnergyBallRecover; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgAddHealthRegen  [class]
// --------------------------------------------------------------------
public class BuffStackCfgAddHealthRegen : BuffStackCfgItem<BuffStackCfgAddHealthRegen, BuffStackCfgAddHealthRegen.Param> // TypeDefIndex: 17687
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgBattleGainCoin  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgBattleGainCoin.Param // TypeDefIndex: 17688
{
	// Fields
	public float addCoinPercent; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgBattleGainCoin  [class]
// --------------------------------------------------------------------
public class BuffStackCfgBattleGainCoin : BuffStackCfgItem<BuffStackCfgBattleGainCoin, BuffStackCfgBattleGainCoin.Param> // TypeDefIndex: 17689
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgPeriodicImmuneElement  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgPeriodicImmuneElement.Param // TypeDefIndex: 17690
{
	// Fields
	public float period; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgPeriodicImmuneElement  [class]
// --------------------------------------------------------------------
public class BuffStackCfgPeriodicImmuneElement : BuffStackCfgItem<BuffStackCfgPeriodicImmuneElement, BuffStackCfgPeriodicImmuneElement.Param> // TypeDefIndex: 17691
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgRecoverArmorOnInflictDebuff  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgRecoverArmorOnInflictDebuff.Param // TypeDefIndex: 17692
{
	// Fields
	public float probability; // 0x0
	public int armor; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgRecoverArmorOnInflictDebuff  [class]
// --------------------------------------------------------------------
public class BuffStackCfgRecoverArmorOnInflictDebuff : BuffStackCfgItem<BuffStackCfgRecoverArmorOnInflictDebuff, BuffStackCfgRecoverArmorOnInflictDebuff.Param> // TypeDefIndex: 17693
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgGainCoinOnCriticalKill  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgGainCoinOnCriticalKill.Param // TypeDefIndex: 17694
{
	// Fields
	public float probability; // 0x0
	public int coin; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgGainCoinOnCriticalKill  [class]
// --------------------------------------------------------------------
public class BuffStackCfgGainCoinOnCriticalKill : BuffStackCfgItem<BuffStackCfgGainCoinOnCriticalKill, BuffStackCfgGainCoinOnCriticalKill.Param> // TypeDefIndex: 17695
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgAddEnBallRecvAndEnemy  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgAddEnBallRecvAndEnemy.Param // TypeDefIndex: 17696
{
	// Fields
	public int addEnergyBallRecv; // 0x0
	public float addEnemyAmount; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgAddEnBallRecvAndEnemy  [class]
// --------------------------------------------------------------------
public class BuffStackCfgAddEnBallRecvAndEnemy : BuffStackCfgItem<BuffStackCfgAddEnBallRecvAndEnemy, BuffStackCfgAddEnBallRecvAndEnemy.Param> // TypeDefIndex: 17697
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgWalkStandTrick  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgWalkStandTrick.Param // TypeDefIndex: 17698
{
	// Fields
	public float addAtkSpdFactor; // 0x0
	public int addDefence; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgWalkStandTrick  [class]
// --------------------------------------------------------------------
public class BuffStackCfgWalkStandTrick : BuffStackCfgItem<BuffStackCfgWalkStandTrick, BuffStackCfgWalkStandTrick.Param> // TypeDefIndex: 17699
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgFreeRefreshShop  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgFreeRefreshShop.Param // TypeDefIndex: 17700
{
	// Fields
	public int times; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgFreeRefreshShop  [class]
// --------------------------------------------------------------------
public class BuffStackCfgFreeRefreshShop : BuffStackCfgItem<BuffStackCfgFreeRefreshShop, BuffStackCfgFreeRefreshShop.Param> // TypeDefIndex: 17701
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMorePlants  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgMorePlants.Param // TypeDefIndex: 17702
{
	// Fields
	public int addPlantAmount; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMorePlants  [class]
// --------------------------------------------------------------------
public class BuffStackCfgMorePlants : BuffStackCfgItem<BuffStackCfgMorePlants, BuffStackCfgMorePlants.Param> // TypeDefIndex: 17703
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgStingRevenge  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgStingRevenge.Param // TypeDefIndex: 17704
{
	// Fields
	public int count; // 0x0
	public int addDamage; // 0x4
	public float cooldown; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgStingRevenge  [class]
// --------------------------------------------------------------------
public class BuffStackCfgStingRevenge : BuffStackCfgItem<BuffStackCfgStingRevenge, BuffStackCfgStingRevenge.Param> // TypeDefIndex: 17705
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgHolyNovaOnDying  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgHolyNovaOnDying.Param // TypeDefIndex: 17706
{
	// Fields
	public int cooldown; // 0x0
	public int damageToBoss; // 0x4
	public float radius; // 0x8
	public float healHpPercent; // 0xC
	public float healHp; // 0x10
}

// --------------------------------------------------------------------
// BuffStackCfgHolyNovaOnDying  [class]
// --------------------------------------------------------------------
public class BuffStackCfgHolyNovaOnDying : BuffStackCfgItem<BuffStackCfgHolyNovaOnDying, BuffStackCfgHolyNovaOnDying.Param> // TypeDefIndex: 17707
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgSoulStrike  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgSoulStrike.Param // TypeDefIndex: 17708
{
	// Fields
	public int dropSoulMin; // 0x0
	public int dropSoulMax; // 0x4
	public int fullSoul; // 0x8
	public int mainDamage; // 0xC
	public float radius; // 0x10
	public int AOEDamage; // 0x14
	public float addDamageFactor; // 0x18
}

// --------------------------------------------------------------------
// BuffStackCfgSoulStrike  [class]
// --------------------------------------------------------------------
public class BuffStackCfgSoulStrike : BuffStackCfgItem<BuffStackCfgSoulStrike, BuffStackCfgSoulStrike.Param> // TypeDefIndex: 17709
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgFrostNovaOnShieldHit  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgFrostNovaOnShieldHit.Param // TypeDefIndex: 17710
{
	// Fields
	public float interval; // 0x0
	public int max; // 0x4
	public float freezingTime; // 0x8
	public float radius; // 0xC
}

// --------------------------------------------------------------------
// BuffStackCfgFrostNovaOnShieldHit  [class]
// --------------------------------------------------------------------
public class BuffStackCfgFrostNovaOnShieldHit : BuffStackCfgItem<BuffStackCfgFrostNovaOnShieldHit, BuffStackCfgFrostNovaOnShieldHit.Param> // TypeDefIndex: 17711
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgRegularFireBreath  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgRegularFireBreath.Param // TypeDefIndex: 17712
{
	// Fields
	public float time; // 0x0
	public int damage; // 0x4
	public int explodeDamage; // 0x8
	public float scale; // 0xC
	public float interval; // 0x10
}

// --------------------------------------------------------------------
// BuffStackCfgRegularFireBreath  [class]
// --------------------------------------------------------------------
public class BuffStackCfgRegularFireBreath : BuffStackCfgItem<BuffStackCfgRegularFireBreath, BuffStackCfgRegularFireBreath.Param> // TypeDefIndex: 17713
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMoveReleaseHurricane  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgMoveReleaseHurricane.Param // TypeDefIndex: 17714
{
	// Fields
	public float distance; // 0x0
	public float destroyDelay; // 0x4
	public float size; // 0x8
	public int damage; // 0xC
	public float lifeTime; // 0x10
	public float seekEnemyDelay; // 0x14
}

// --------------------------------------------------------------------
// BuffStackCfgMoveReleaseHurricane  [class]
// --------------------------------------------------------------------
public class BuffStackCfgMoveReleaseHurricane : BuffStackCfgItem<BuffStackCfgMoveReleaseHurricane, BuffStackCfgMoveReleaseHurricane.Param> // TypeDefIndex: 17715
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgChaosStrike  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgChaosStrike.Param // TypeDefIndex: 17716
{
	// Fields
	public float addDmgFactor; // 0x0
	public float addBulletSize; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgChaosStrike  [class]
// --------------------------------------------------------------------
public class BuffStackCfgChaosStrike : BuffStackCfgItem<BuffStackCfgChaosStrike, BuffStackCfgChaosStrike.Param> // TypeDefIndex: 17717
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgAirReinforcement  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgAirReinforcement.MercaneryInfo // TypeDefIndex: 17718
{
	// Fields
	public string name; // 0x0
	public string weapon; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgAirReinforcement  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgAirReinforcement.Param // TypeDefIndex: 17719
{
	// Fields
	public float addAtkSpeed; // 0x0
	public float addMoveSpeedFactor; // 0x4
	public float duration; // 0x8
	public float cooldown; // 0xC
	public BuffStackCfgAirReinforcement.MercaneryInfo[] mercenaries; // 0x10
}

// --------------------------------------------------------------------
// BuffStackCfgAirReinforcement  [class]
// --------------------------------------------------------------------
public class BuffStackCfgAirReinforcement : BuffStackCfgItem<BuffStackCfgAirReinforcement, BuffStackCfgAirReinforcement.Param> // TypeDefIndex: 17720
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgPetMimic  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgPetMimic.Param // TypeDefIndex: 17721
{
	// Fields
	public float cooldown; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgPetMimic  [class]
// --------------------------------------------------------------------
public class BuffStackCfgPetMimic : BuffStackCfgItem<BuffStackCfgPetMimic, BuffStackCfgPetMimic.Param> // TypeDefIndex: 17722
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgSummonPet  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgSummonPet.Param // TypeDefIndex: 17723
{
	// Fields
	public int energy; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgSummonPet  [class]
// --------------------------------------------------------------------
public class BuffStackCfgSummonPet : BuffStackCfgItem<BuffStackCfgSummonPet, BuffStackCfgSummonPet.Param> // TypeDefIndex: 17724
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgSwordCore  [class]
// --------------------------------------------------------------------
public class BuffStackCfgSwordCore : BuffStackCfgItem<BuffStackCfgSwordCore, BuffStackCfgSwordCore.Param> // TypeDefIndex: 17726
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgElementalAreaEnhance  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgElementalAreaEnhance.Param // TypeDefIndex: 17727
{
	// Fields
	public float addDuration; // 0x0
	public float durationMax; // 0x4
	public float addRadius; // 0x8
	public float radiusMax; // 0xC
	public float addDamageFactor; // 0x10
	public float damageFactorMax; // 0x14
}

// --------------------------------------------------------------------
// BuffStackCfgElementalAreaEnhance  [class]
// --------------------------------------------------------------------
public class BuffStackCfgElementalAreaEnhance : BuffStackCfgItem<BuffStackCfgElementalAreaEnhance, BuffStackCfgElementalAreaEnhance.Param> // TypeDefIndex: 17728
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgHeavyStrike  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgHeavyStrike.Param // TypeDefIndex: 17729
{
	// Fields
	public float damageFactor; // 0x0
	public float size; // 0x4
	public int attackCount; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgHeavyStrike  [class]
// --------------------------------------------------------------------
public class BuffStackCfgHeavyStrike : BuffStackCfgItem<BuffStackCfgHeavyStrike, BuffStackCfgHeavyStrike.Param> // TypeDefIndex: 17730
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgSiphon  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgSiphon.Param // TypeDefIndex: 17731
{
	// Fields
	public int lossDefence; // 0x0
	public float range; // 0x4
	public float damageHpFactor; // 0x8
	public int damageMax; // 0xC
	public float cooldown; // 0x10
	public int restoreHit; // 0x14
}

// --------------------------------------------------------------------
// BuffStackCfgSiphon  [class]
// --------------------------------------------------------------------
public class BuffStackCfgSiphon : BuffStackCfgItem<BuffStackCfgSiphon, BuffStackCfgSiphon.Param> // TypeDefIndex: 17732
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMoreSalary  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgMoreSalary.Param // TypeDefIndex: 17733
{
	// Fields
	public int salary; // 0x0
	public float damageCooldown; // 0x4
	public int damage; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgMoreSalary  [class]
// --------------------------------------------------------------------
public class BuffStackCfgMoreSalary : BuffStackCfgItem<BuffStackCfgMoreSalary, BuffStackCfgMoreSalary.Param> // TypeDefIndex: 17734
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgGiftPackage  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgGiftPackage.Param // TypeDefIndex: 17735
{
	// Fields
	public int salary; // 0x0
	public int maxHp; // 0x4
	public int maxEnergy; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgGiftPackage  [class]
// --------------------------------------------------------------------
public class BuffStackCfgGiftPackage : BuffStackCfgItem<BuffStackCfgGiftPackage, BuffStackCfgGiftPackage.Param> // TypeDefIndex: 17736
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgRandomDodge  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgRandomDodge.Param // TypeDefIndex: 17737
{
	// Fields
	public float probability; // 0x0
	public float teleportDistance; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgRandomDodge  [class]
// --------------------------------------------------------------------
public class BuffStackCfgRandomDodge : BuffStackCfgItem<BuffStackCfgRandomDodge, BuffStackCfgRandomDodge.Param> // TypeDefIndex: 17738
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgSkillPowStack  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgSkillPowStack.Param // TypeDefIndex: 17739
{
	// Fields
	public float damageFactor; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgSkillPowStack  [class]
// --------------------------------------------------------------------
public class BuffStackCfgSkillPowStack : BuffStackCfgItem<BuffStackCfgSkillPowStack, BuffStackCfgSkillPowStack.Param> // TypeDefIndex: 17740
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgFireWork  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgFireWork.Param // TypeDefIndex: 17741
{
	// Fields
	public float time; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgFireWork  [class]
// --------------------------------------------------------------------
public class BuffStackCfgFireWork : BuffStackCfgItem<BuffStackCfgFireWork, BuffStackCfgFireWork.Param> // TypeDefIndex: 17742
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgPotionOnKill  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgPotionOnKill.Portion // TypeDefIndex: 17743
{
	// Fields
	public float weight; // 0x0
	public string name; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgPotionOnKill  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgPotionOnKill.Param // TypeDefIndex: 17744
{
	// Fields
	public float addEffectFactor; // 0x0
	public BuffStackCfgPotionOnKill.Portion[] portions; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgPotionOnKill  [class]
// --------------------------------------------------------------------
public class BuffStackCfgPotionOnKill : BuffStackCfgItem<BuffStackCfgPotionOnKill, BuffStackCfgPotionOnKill.Param> // TypeDefIndex: 17745
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgLaserRefraction  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgLaserRefraction.Param // TypeDefIndex: 17746
{
	// Fields
	public float probability; // 0x0
	public int count; // 0x4
	public float damageFactor; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgLaserRefraction  [class]
// --------------------------------------------------------------------
public class BuffStackCfgLaserRefraction : BuffStackCfgItem<BuffStackCfgLaserRefraction, BuffStackCfgLaserRefraction.Param> // TypeDefIndex: 17747
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgEnemyHpUpSpeedDown  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgEnemyHpUpSpeedDown.Param // TypeDefIndex: 17748
{
	// Fields
	public float hpFactor; // 0x0
	public float speedFactor; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgEnemyHpUpSpeedDown  [class]
// --------------------------------------------------------------------
public class BuffStackCfgEnemyHpUpSpeedDown : BuffStackCfgItem<BuffStackCfgEnemyHpUpSpeedDown, BuffStackCfgEnemyHpUpSpeedDown.Param> // TypeDefIndex: 17749
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMissionKillPlant  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgMissionKillPlant.Param // TypeDefIndex: 17750
{
	// Fields
	public int count; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMissionKillPlant  [class]
// --------------------------------------------------------------------
public class BuffStackCfgMissionKillPlant : BuffStackCfgItem<BuffStackCfgMissionKillPlant, BuffStackCfgMissionKillPlant.Param> // TypeDefIndex: 17751
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgGetCoinAddEnergy  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgGetCoinAddEnergy.Param // TypeDefIndex: 17752
{
	// Fields
	public float probability; // 0x0
	public int energy; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgGetCoinAddEnergy  [class]
// --------------------------------------------------------------------
public class BuffStackCfgGetCoinAddEnergy : BuffStackCfgItem<BuffStackCfgGetCoinAddEnergy, BuffStackCfgGetCoinAddEnergy.Param> // TypeDefIndex: 17753
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgSkillAddDamage  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgSkillAddDamage.Param // TypeDefIndex: 17754
{
	// Fields
	public float damageFactor; // 0x0
	public float period; // 0x4
	public int maxCount; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgSkillAddDamage  [class]
// --------------------------------------------------------------------
public class BuffStackCfgSkillAddDamage : BuffStackCfgItem<BuffStackCfgSkillAddDamage, BuffStackCfgSkillAddDamage.Param> // TypeDefIndex: 17755
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgTenCritical  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgTenCritical.Param // TypeDefIndex: 17756
{
	// Fields
	public int CriticalCount; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgTenCritical  [class]
// --------------------------------------------------------------------
public class BuffStackCfgTenCritical : BuffStackCfgItem<BuffStackCfgTenCritical, BuffStackCfgTenCritical.Param> // TypeDefIndex: 17757
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgFocusOn  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgFocusOn.Param // TypeDefIndex: 17758
{
	// Fields
	public int Count; // 0x0
	public int MaxCount; // 0x4
	public float Duration; // 0x8
	public int AttackSpeedAddition; // 0xC
	public int CriticalAddition; // 0x10
	public int DeviationAddition; // 0x14
}

// --------------------------------------------------------------------
// BuffStackCfgFocusOn  [class]
// --------------------------------------------------------------------
public class BuffStackCfgFocusOn : BuffStackCfgItem<BuffStackCfgFocusOn, BuffStackCfgFocusOn.Param> // TypeDefIndex: 17759
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgIncreaseVolume  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgIncreaseVolume.Param // TypeDefIndex: 17760
{
	// Fields
	public int HitCount; // 0x0
	public int DeltaBulletSize; // 0x4
	public int DeltaMoveSpeed; // 0x8
	public int Duration; // 0xC
	public int Layer; // 0x10
}

// --------------------------------------------------------------------
// BuffStackCfgIncreaseVolume  [class]
// --------------------------------------------------------------------
public class BuffStackCfgIncreaseVolume : BuffStackCfgItem<BuffStackCfgIncreaseVolume, BuffStackCfgIncreaseVolume.Param> // TypeDefIndex: 17761
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgGunEcho  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgGunEcho.Param // TypeDefIndex: 17762
{
	// Fields
	public float triggerInterval; // 0x0
	public float cooldownReducePerWeapon; // 0x4
	public float minInterval; // 0x8
	public BuffImplGunEcho implGunEcho; // 0x10
}

// --------------------------------------------------------------------
// BuffStackCfgGunEcho  [class]
// --------------------------------------------------------------------
public class BuffStackCfgGunEcho : BuffStackCfgItem<BuffStackCfgGunEcho, BuffStackCfgGunEcho.Param> // TypeDefIndex: 17763
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgRecoverArmorOnKillEnemy  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgRecoverArmorOnKillEnemy.Param // TypeDefIndex: 17764
{
	// Fields
	public int armor; // 0x0
	public float cooldown; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgRecoverArmorOnKillEnemy  [class]
// --------------------------------------------------------------------
public class BuffStackCfgRecoverArmorOnKillEnemy : BuffStackCfgItem<BuffStackCfgRecoverArmorOnKillEnemy, BuffStackCfgRecoverArmorOnKillEnemy.Param> // TypeDefIndex: 17765
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgCreatePhantomOnChargeAttack  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgCreatePhantomOnChargeAttack.Param // TypeDefIndex: 17766
{
	// Fields
	public float duration; // 0x0
	public int count; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgCreatePhantomOnChargeAttack  [class]
// --------------------------------------------------------------------
public class BuffStackCfgCreatePhantomOnChargeAttack : BuffStackCfgItem<BuffStackCfgCreatePhantomOnChargeAttack, BuffStackCfgCreatePhantomOnChargeAttack.Param> // TypeDefIndex: 17767
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgElementalFollower  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgElementalFollower.Param // TypeDefIndex: 17768
{
	// Fields
	public float probability; // 0x0
	public float duration; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgElementalFollower  [class]
// --------------------------------------------------------------------
public class BuffStackCfgElementalFollower : BuffStackCfgItem<BuffStackCfgElementalFollower, BuffStackCfgElementalFollower.Param> // TypeDefIndex: 17769
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgSkillCooldownExtend  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgSkillCooldownExtend.Param // TypeDefIndex: 17770
{
	// Fields
	public float addCooldown; // 0x0
	public float reduceCooldownPercent; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgSkillCooldownExtend  [class]
// --------------------------------------------------------------------
public class BuffStackCfgSkillCooldownExtend : BuffStackCfgItem<BuffStackCfgSkillCooldownExtend, BuffStackCfgSkillCooldownExtend.Param> // TypeDefIndex: 17771
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgArmorDamageConversion  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgArmorDamageConversion.Param // TypeDefIndex: 17772
{
	// Fields
	public float armorRecoveryReduceFactor; // 0x0
	public float armorPerDamage; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgArmorDamageConversion  [class]
// --------------------------------------------------------------------
public class BuffStackCfgArmorDamageConversion : BuffStackCfgItem<BuffStackCfgArmorDamageConversion, BuffStackCfgArmorDamageConversion.Param> // TypeDefIndex: 17773
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgSwitchWeaponDamage  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgSwitchWeaponDamage.Param // TypeDefIndex: 17774
{
	// Fields
	public float damageFactor; // 0x0
	public float duration; // 0x4
	public int maxStack; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgSwitchWeaponDamage  [class]
// --------------------------------------------------------------------
public class BuffStackCfgSwitchWeaponDamage : BuffStackCfgItem<BuffStackCfgSwitchWeaponDamage, BuffStackCfgSwitchWeaponDamage.Param> // TypeDefIndex: 17775
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgThunderStrikeNearPet  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgThunderStrikeNearPet.Param // TypeDefIndex: 17776
{
	// Fields
	public float radius; // 0x0
	public float thunderRadius; // 0x4
	public float interval; // 0x8
	public int damage; // 0xC
}

// --------------------------------------------------------------------
// BuffStackCfgThunderStrikeNearPet  [class]
// --------------------------------------------------------------------
public class BuffStackCfgThunderStrikeNearPet : BuffStackCfgItem<BuffStackCfgThunderStrikeNearPet, BuffStackCfgThunderStrikeNearPet.Param> // TypeDefIndex: 17777
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgAttackMoveSpeedStack  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgAttackMoveSpeedStack.Param // TypeDefIndex: 17778
{
	// Fields
	public float duration; // 0x0
	public float moveSpeed; // 0x4
	public int maxStack; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgAttackMoveSpeedStack  [class]
// --------------------------------------------------------------------
public class BuffStackCfgAttackMoveSpeedStack : BuffStackCfgItem<BuffStackCfgAttackMoveSpeedStack, BuffStackCfgAttackMoveSpeedStack.Param> // TypeDefIndex: 17779
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgAutoAimPenetration  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgAutoAimPenetration.Param // TypeDefIndex: 17780
{
	// Fields
	public float damageFactor; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgAutoAimPenetration  [class]
// --------------------------------------------------------------------
public class BuffStackCfgAutoAimPenetration : BuffStackCfgItem<BuffStackCfgAutoAimPenetration, BuffStackCfgAutoAimPenetration.Param> // TypeDefIndex: 17781
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgFreeEnergyOnAttack  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgFreeEnergyOnAttack.Param // TypeDefIndex: 17782
{
	// Fields
	public float probability; // 0x0
	public int addDamage; // 0x4
	public float energyPercent; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgFreeEnergyOnAttack  [class]
// --------------------------------------------------------------------
public class BuffStackCfgFreeEnergyOnAttack : BuffStackCfgItem<BuffStackCfgFreeEnergyOnAttack, BuffStackCfgFreeEnergyOnAttack.Param> // TypeDefIndex: 17783
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgElementalBottleOnAttack  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgElementalBottleOnAttack.Param // TypeDefIndex: 17784
{
	// Fields
	public float checkDuration; // 0x0
	public int energyThreshold; // 0x4
	public float cooldown; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgElementalBottleOnAttack  [class]
// --------------------------------------------------------------------
public class BuffStackCfgElementalBottleOnAttack : BuffStackCfgItem<BuffStackCfgElementalBottleOnAttack, BuffStackCfgElementalBottleOnAttack.Param> // TypeDefIndex: 17785
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgEnergyBallMovement  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgEnergyBallMovement.Param // TypeDefIndex: 17786
{
	// Fields
	public float recoveryReducePercent; // 0x0
	public float spawnProbability; // 0x4
	public float checkDistance; // 0x8
	public float cooldown; // 0xC
}

// --------------------------------------------------------------------
// BuffStackCfgEnergyBallMovement  [class]
// --------------------------------------------------------------------
public class BuffStackCfgEnergyBallMovement : BuffStackCfgItem<BuffStackCfgEnergyBallMovement, BuffStackCfgEnergyBallMovement.Param> // TypeDefIndex: 17787
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgArcaneEnergyBall  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgArcaneEnergyBall.Param // TypeDefIndex: 17788
{
	// Fields
	public int bulletCount; // 0x0
	public int maxStore; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgArcaneEnergyBall  [class]
// --------------------------------------------------------------------
public class BuffStackCfgArcaneEnergyBall : BuffStackCfgItem<BuffStackCfgArcaneEnergyBall, BuffStackCfgArcaneEnergyBall.Param> // TypeDefIndex: 17789
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgBlackRoseCurse  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgBlackRoseCurse.Param // TypeDefIndex: 17790
{
	// Fields
	public float damageFactor; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgBlackRoseCurse  [class]
// --------------------------------------------------------------------
public class BuffStackCfgBlackRoseCurse : BuffStackCfgItem<BuffStackCfgBlackRoseCurse, BuffStackCfgBlackRoseCurse.Param> // TypeDefIndex: 17791
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgNepenthes  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgNepenthes.Param // TypeDefIndex: 17792
{
	// Fields
	public float absorptionTime; // 0x0
	public float duration; // 0x4
	public float damagePercentage; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgNepenthes  [class]
// --------------------------------------------------------------------
public class BuffStackCfgNepenthes : BuffStackCfgItem<BuffStackCfgNepenthes, BuffStackCfgNepenthes.Param> // TypeDefIndex: 17793
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMimicBackWeapon  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgMimicBackWeapon.Param // TypeDefIndex: 17794
{
	// Fields
	public float mimicDuration; // 0x0
	public float cooldownTime; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgMimicBackWeapon  [class]
// --------------------------------------------------------------------
public class BuffStackCfgMimicBackWeapon : BuffStackCfgItem<BuffStackCfgMimicBackWeapon, BuffStackCfgMimicBackWeapon.Param> // TypeDefIndex: 17795
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgToughPlant  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgToughPlant.Param // TypeDefIndex: 17796
{
	// Fields
	public float hpFactor; // 0x0
	public float coinFactor; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgToughPlant  [class]
// --------------------------------------------------------------------
public class BuffStackCfgToughPlant : BuffStackCfgItem<BuffStackCfgToughPlant, BuffStackCfgToughPlant.Param> // TypeDefIndex: 17797
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgPlantExplode  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgPlantExplode.Param // TypeDefIndex: 17798
{
	// Fields
	public float explodeRadius; // 0x0
	public int explodeDamage; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgPlantExplode  [class]
// --------------------------------------------------------------------
public class BuffStackCfgPlantExplode : BuffStackCfgItem<BuffStackCfgPlantExplode, BuffStackCfgPlantExplode.Param> // TypeDefIndex: 17799
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgFirePath  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgFirePath.Param // TypeDefIndex: 17800
{
	// Fields
	public int damage; // 0x0
	public float duration; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgFirePath  [class]
// --------------------------------------------------------------------
public class BuffStackCfgFirePath : BuffStackCfgItem<BuffStackCfgFirePath, BuffStackCfgFirePath.Param> // TypeDefIndex: 17801
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgShortCircuit  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgShortCircuit.Param // TypeDefIndex: 17802
{
	// Fields
	public int damage; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgShortCircuit  [class]
// --------------------------------------------------------------------
public class BuffStackCfgShortCircuit : BuffStackCfgItem<BuffStackCfgShortCircuit, BuffStackCfgShortCircuit.Param> // TypeDefIndex: 17803
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgLockHpAddDamage  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgLockHpAddDamage.Param // TypeDefIndex: 17804
{
	// Fields
	public int addDamage; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgLockHpAddDamage  [class]
// --------------------------------------------------------------------
public class BuffStackCfgLockHpAddDamage : BuffStackCfgItem<BuffStackCfgLockHpAddDamage, BuffStackCfgLockHpAddDamage.Param> // TypeDefIndex: 17805
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMoreExplosion  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgMoreExplosion.Param // TypeDefIndex: 17806
{
	// Fields
	public float reduceDamagePercent; // 0x0
	public float addRange; // 0x4
}

// --------------------------------------------------------------------
// BuffStackCfgMoreExplosion  [class]
// --------------------------------------------------------------------
public class BuffStackCfgMoreExplosion : BuffStackCfgItem<BuffStackCfgMoreExplosion, BuffStackCfgMoreExplosion.Param> // TypeDefIndex: 17807
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgMonkeySummon  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgMonkeySummon.Param // TypeDefIndex: 17808
{
	// Fields
	public int count; // 0x0
	public int cooldown; // 0x4
	public int hp; // 0x8
}

// --------------------------------------------------------------------
// BuffStackCfgMonkeySummon  [class]
// --------------------------------------------------------------------
public class BuffStackCfgMonkeySummon : BuffStackCfgItem<BuffStackCfgMonkeySummon, BuffStackCfgMonkeySummon.Param> // TypeDefIndex: 17809
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// BuffStackCfgDropCaptainBox  [struct]
// --------------------------------------------------------------------
public struct BuffStackCfgDropCaptainBox.Param // TypeDefIndex: 17810
{
	// Fields
	public int Count; // 0x0
	public int MaxCount; // 0x4
	public float Interval; // 0x8
	public int Radious; // 0xC
}

// --------------------------------------------------------------------
// BuffStackCfgDropCaptainBox  [class]
// --------------------------------------------------------------------
public class BuffStackCfgDropCaptainBox : BuffStackCfgItem<BuffStackCfgDropCaptainBox, BuffStackCfgDropCaptainBox.Param> // TypeDefIndex: 17811
{
	// Fields
	private static readonly emBuff _buff; // 0x0
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.BuffHolyNovaOnDying.<>c // TypeDefIndex: 17839
{
	// Fields
	public static readonly TalentBuff.BuffHolyNovaOnDying.<>c <>9; // 0x0
	public static Action<BuffComEmitBullet, GameObject> <>9__7_0; // 0x8
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.BuffHolyNovaOnDying // TypeDefIndex: 17840
{
	// Fields
	private RGController controller; // 0x10
	private emBuff buff; // 0x18
	private int netId; // 0x1C
	private BuffStackCfgHolyNovaOnDying.Param param; // 0x20
	private BuffFuncNode buffFuncNode; // 0x38
}

// --------------------------------------------------------------------
// TalentBuff  [struct]
// --------------------------------------------------------------------
private struct TalentBuff.SoulStrikeDamageInfoGetter : AreaDamageCarrier.IDamageInfoGetter // TypeDefIndex: 17841
{
	// Fields
	public BuffStackCfgSoulStrike.Param param; // 0x0
	public RGEController hitEnemy; // 0x20
	public int netId; // 0x28
}

// --------------------------------------------------------------------
// TalentBuff  [struct]
// --------------------------------------------------------------------
private struct TalentBuff.FrostNovaDamageInfoGetter : AreaDamageCarrier.IDamageInfoGetter // TypeDefIndex: 17842
{
	// Fields
	public BuffStackCfgFrostNovaOnShieldHit.Param param; // 0x0
	public IBuffFuncTarget owner; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.RegularFireBreathDamageInfoGetter.<>c__DisplayClass3_0 // TypeDefIndex: 17843
{
	// Fields
	public Vector3 position; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [struct]
// --------------------------------------------------------------------
private struct TalentBuff.RegularFireBreathDamageInfoGetter : AreaDamageCarrier.IDamageInfoGetter // TypeDefIndex: 17844
{
	// Fields
	public BuffStackCfgRegularFireBreath.Param param; // 0x0
	public IBuffFuncTarget owner; // 0x18
	public RGBaseController controller; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [struct]
// --------------------------------------------------------------------
private struct TalentBuff.SiphonDamageInfoGetter : AreaDamageCarrier.IDamageInfoGetter // TypeDefIndex: 17846
{
	// Fields
	public BuffStackCfgSiphon.Param param; // 0x0
}

// --------------------------------------------------------------------
// TalentBuff  [struct]
// --------------------------------------------------------------------
private struct TalentBuff.MechaAttributeConfig // TypeDefIndex: 17847
{
	// Fields
	public float weight; // 0x0
	public string name; // 0x8
	public float value; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.BuffSkillCooldownExtend // TypeDefIndex: 17848
{
	// Fields
	private BuffStackCfgSkillCooldownExtend.Param param; // 0x10
	private RGController controller; // 0x18
	private float skillCdDecrease; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.BuffExpertPet // TypeDefIndex: 17849
{
	// Fields
	private RGController _controller; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.BuffExtraEmitBullet // TypeDefIndex: 17850
{
	// Fields
	private RGController controller; // 0x10
	private BuffStackCfgExtraEmitBullet.Param param; // 0x18
	private RGRandom _RNG; // 0x28
	private LookUpRecordList<long, DamageCarrier> records; // 0x30
	private bool isUpgraded; // 0x38
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
public class TalentBuff.BuffFocusOn // TypeDefIndex: 17851
{
	// Fields
	private BuffStackCfgFocusOn.Param param; // 0x10
	private RGController controller; // 0x28
	private float lifetimeRemain; // 0x30
	private const string BuffEffectName = "buff_focus_on_effect";
	private const string BuffEffectPrefabPath = "RGPrefab/Effect/buff_focus_on_effect/buff_focus_on_effect.prefab";
	private int stack; // 0x34
	private LookUpRecordList<RGWeapon, int> weaponDeviationInfos; // 0x38
	private BuffComAttribute attributeNode; // 0x40
	private RGBuff infoDisplayer; // 0x48
	private emBuff buff; // 0x50
	private bool upgraded; // 0x54
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.BuffHurtBeCrazy // TypeDefIndex: 17852
{
	// Fields
	private const float NormalMaxCriticalChanceBonus = 70;
	private const float UpgradedMaxCriticalChanceBonus = 100;
	private const float NormalMaxAttackSpeedBonus = 0.3;
	private const float UpgradedMaxAttackSpeedBonus = 0.5;
	private const float MinDangerPercent = 1;
	private const float MaxDangerPercent = 100;
	private const float MaxDangerEffectThreshold = 0.9;
	private const float MinEmissionRate = 0;
	private const float MaxEmissionRate = 10;
	private const string DangerEffectPrefabPath = "RGPrefab/Bullet/Buff/buff_hurt_critic.prefab";
	private const string DangerEffectObjectName = "buff_hurt_critic";
	private const string MaxDangerEffectNodeName = "particle_max";
	public RGController controller; // 0x10
	private int addedCriticalChance; // 0x18
	private float addedAttackSpeed; // 0x1C
	private float currentDangerLevel; // 0x20
	private GameObject dangerEffect; // 0x28
	private ParticleSystem dangerParticle; // 0x30
	private GameObject maxDangerEffect; // 0x38
	private bool isUpgraded; // 0x40
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.BuffMasterSpeedAtk // TypeDefIndex: 17853
{
	// Fields
	private const int StackGainPerAttack = 1;
	private const float AttackSpeedPerStack = 0.02;
	private const float UpgradedAttackSpeedPerStack = 0.04;
	private const float FullStackAttackSpeedBonus = 0.1;
	private const float StackDuration = 3;
	private const float StackDecayInterval = 0.25;
	private const int MaxStack = 5;
	private const string FullStackEffectPrefabPath = "RGPrefab/Bullet/Buff/buff_master_atk_speed_effect.prefab";
	private const string FullStackEffectObjectName = "buff_master_atk_speed_effect";
	private RGController controller; // 0x10
	private emBuff buffId; // 0x18
	private int stack; // 0x1C
	private float addedAttackSpeed; // 0x20
	private float stackRemainTime; // 0x24
	private bool isDecaying; // 0x28
	private bool isUpgraded; // 0x29
	private BuffDisplayBasicInfo displayInfo; // 0x30
	private UtilBuff infoDisplayer; // 0x38
	private GameObject fullStackEffect; // 0x40
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.BuffMoveEnhanceAttack // TypeDefIndex: 17854
{
	// Fields
	private HashSet<long> _modifiedBulletHashSet; // 0x10
	private RGController controller; // 0x18
	private bool isUpgraded; // 0x20
	private BuffStackCfgMoveEnhanceAttack.Param param; // 0x24
	private float accumulatedDistance; // 0x38
	private Vector2 lastPos; // 0x3C
	private emBuff buffId; // 0x44
	private float _lastFastChargeTime; // 0x48
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.BuffSkillAddDamage // TypeDefIndex: 17855
{
	// Fields
	private RGController controller; // 0x10
	private bool isUpgraded; // 0x18
	private BuffStackCfgSkillAddDamage.Param param; // 0x1C
	private emBuff buffId; // 0x28
	private float _lifetime; // 0x2C
	private float _totalLifetime; // 0x30
	private int _stack; // 0x34
	private BuffFuncNodeMoniter monitor; // 0x38
	private UtilBuff infoDisplayer; // 0x40
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.BuffSkillCd // TypeDefIndex: 17856
{
	// Fields
	public RGController controller; // 0x10
	private RGRandom _RNG; // 0x18
	private bool upgraded; // 0x20
	private bool checkerActive; // 0x21
	private float cooldownRemain; // 0x24
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
public class TalentBuff.BuffImplTenCritical // TypeDefIndex: 17857
{
	// Fields
	private const float FightingSpiritDuration = 5;
	private const float BurstDuration = 5;
	private const int NormalMaxFightingSpiritStacks = 10;
	private const int UpgradedMaxFightingSpiritStacks = 5;
	private const int BurstCriticalChanceBonus = 50;
	private const float BurstMeleeVolumeMultiplier = 1.2;
	private const string BurstMeleeVolumeModifyMark = "TenCriticalBurst";
	private const string DefaultBurstEffectPath = "RGPrefab/Characters/Fighter/skin_0/skill_1/angry/fighter_0_angry_effect.prefab";
	private const string BurstEffectObjectName = "ten_critical_angry_effect";
	private RGController controller; // 0x10
	private emBuff buffId; // 0x18
	private BuffStackCfgTenCritical.Param param; // 0x1C
	private int fightingSpirit; // 0x20
	private float fightingSpiritRemainTime; // 0x24
	private float burstRemainTime; // 0x28
	private bool burstActive; // 0x2C
	private bool isUpgraded; // 0x2D
	private GameObject burstEffect; // 0x30
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.BuffWhirlWindSlash // TypeDefIndex: 17858
{
	// Fields
	private RGController controller; // 0x10
	private BuffStackCfgWhirlWindSlash.Param param; // 0x18
	private bool isUpgraded; // 0x24
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.BuffArmorBrokenRestoreArmor // TypeDefIndex: 17859
{
	// Fields
	public RGController controller; // 0x10
	private int _killCount; // 0x18
	private bool upgraded; // 0x1C
	private RGBuff utilBuff; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeCoinAddArmor // TypeDefIndex: 17860
{
	// Fields
	public RGController controller; // 0x10
	private bool coinChanged; // 0x18
	private int oldCoin; // 0x1C
	private int compensation; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeElementalStaff // TypeDefIndex: 17861
{
	// Fields
	public RGController controller; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeEnemyDeadBoom // TypeDefIndex: 17862
{
	// Fields
	public RGController controller; // 0x10
	private RGRandom _RNG; // 0x18
	private GameObject[] bulletList; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeExpertBoxPot // TypeDefIndex: 17863
{
	// Fields
	public RGController controller; // 0x10
	private RGRandom _RNG; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeExpertForce // TypeDefIndex: 17864
{
	// Fields
	public RGController controller; // 0x10
	private double _lastBulletStopTime; // 0x18
	private GameObject checker; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.UpgradeExpertPet.<ShieldUpdate>d__3 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 17865
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public float duration; // 0x20
	public GameObject shieldObj; // 0x28
	private CommandDuration <fadeOut>5__2; // 0x30
	private float <dt>5__3; // 0x38
	private WaitForSeconds <wait>5__4; // 0x40
	private float <t>5__5; // 0x48
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeExpertPet // TypeDefIndex: 17866
{
	// Fields
	public RGController controller; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeExpertShop // TypeDefIndex: 17867
{
	// Fields
	public RGController controller; // 0x10
	private RGRandom _RNG; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeExpertStatus // TypeDefIndex: 17868
{
	// Fields
	private RGRandom _RNG; // 0x10
	public RGController controller; // 0x18
	private float _lastTime; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeHitFullHpEnemyCritical // TypeDefIndex: 17869
{
	// Fields
	public RGController controller; // 0x10
	private float _critTimeRemain; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeMasterAccurate // TypeDefIndex: 17870
{
	// Fields
	private int stack; // 0x10
	public RGController controller; // 0x18
	private DamageCarrier stunDmgCarrier; // 0x20
	private float _expireTime; // 0x28
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeMasterContinuous // TypeDefIndex: 17871
{
	// Fields
	public RGController controller; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeMasterHold // TypeDefIndex: 17872
{
	// Fields
	public RGController controller; // 0x10
	private bool _nextChargeInstant; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeMasterLaser // TypeDefIndex: 17873
{
	// Fields
	public RGController controller; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeMasterReflection // TypeDefIndex: 17874
{
	// Fields
	public RGController controller; // 0x10
	private LookUpRecordList<DamageCarrier, DamageCarrier> damageCarrierList; // 0x18
	private RGRandom _RNG; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeMasterShotgun // TypeDefIndex: 17875
{
	// Fields
	public RGController controller; // 0x10
	private float _lastTime; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeMasterSwordRange // TypeDefIndex: 17876
{
	// Fields
	public RGController controller; // 0x10
	private GameObject lightPrefab; // 0x18
	private int hitCount; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeMasterSwordRebound // TypeDefIndex: 17877
{
	// Fields
	public RGController controller; // 0x10
	private float _lastTime; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeMasterThough // TypeDefIndex: 17878
{
	// Fields
	public RGController controller; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeMasterWeapon // TypeDefIndex: 17879
{
	// Fields
	public RGController controller; // 0x10
	private int weaponCount; // 0x18
	private float interval; // 0x1C
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeMaxEnergy // TypeDefIndex: 17881
{
	// Fields
	private const int ExtraMaxEnergy = 100;
	private const int ConsumeEnergyToRestoreShield = 200;
	private RGController controller; // 0x10
	private int consumedEnergy; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeMaxHp // TypeDefIndex: 17882
{
	// Fields
	public RGController controller; // 0x10
	private int _lastHp; // 0x18
	private float cooldownRemain; // 0x1C
	private string dataKey; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.BuffPetCharge // TypeDefIndex: 17883
{
	// Fields
	public RGController controller; // 0x10
	private emBuff buffId; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
public class TalentBuff.TalentUpgradeQigongWave // TypeDefIndex: 17884
{
	// Fields
	private RGController controller; // 0x10
	private int _hitCount; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.BuffRecoverArmorOnInflictDebuff // TypeDefIndex: 17885
{
	// Fields
	public RGController controller; // 0x10
	private RGRandom _RNG; // 0x18
	private bool upgraded; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeShieldBlast // TypeDefIndex: 17886
{
	// Fields
	private bool active; // 0x10
	private float bulletSize; // 0x14
	public RGController controller; // 0x18
	private GameObject defaultShieldBlastPrefab; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeShieldBlue // TypeDefIndex: 17887
{
	// Fields
	public RGController controller; // 0x10
	private float _speedBoostEndTime; // 0x18
	private GameObject fxObj; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeShieldFire // TypeDefIndex: 17888
{
	// Fields
	public RGController controller; // 0x10
	private float _lastTime; // 0x18
	private RGRandom _RNG; // 0x20
	private List<GameObject> fireList; // 0x28
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeShieldGas // TypeDefIndex: 17889
{
	// Fields
	public RGController controller; // 0x10
	private float _addSpeedTime; // 0x18
	private GameObject fxObj; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeShieldIce // TypeDefIndex: 17890
{
	// Fields
	private RGRandom _RNG; // 0x10
	public RGController controller; // 0x18
	private float _lastTime; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeShieldSting // TypeDefIndex: 17891
{
	// Fields
	public RGController controller; // 0x10
	private float _lastTime; // 0x18
	private int _lostArmor; // 0x1C
	private float _cooldownRemain; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.BuffSpeedUpWithCritic // TypeDefIndex: 17892
{
	// Fields
	public RGController controller; // 0x10
	private float _speedBoostEndTime; // 0x18
	private bool addedSpeed; // 0x1C
	private Vector2 lastControllerPos; // 0x20
	private float accumulatedDistance; // 0x28
	private bool upgraded; // 0x2C
	private RGRandom rng; // 0x30
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeVampireEnergy // TypeDefIndex: 17893
{
	// Fields
	public RGController controller; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private class TalentBuff.UpgradeVampireHp // TypeDefIndex: 17894
{
	// Fields
	public RGController controller; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c // TypeDefIndex: 17895
{
	// Fields
	public static readonly TalentBuff.<>c <>9; // 0x0
	public static Func<int, RGBuff, int> <>9__6_2; // 0x8
	public static Action<BuffFuncNode> <>9__11_1; // 0x10
	public static Action<Buff> <>9__15_0; // 0x18
	public static Func<float, BuffStackCfgPotionOnKill.Portion, float> <>9__51_1; // 0x20
	public static Func<float, TalentBuff.MechaAttributeConfig, float> <>9__57_4; // 0x28
	public static Predicate<emBuff> <>9__59_1; // 0x30
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass10_0 // TypeDefIndex: 17896
{
	// Fields
	public bool active; // 0x10
	public RGController controller; // 0x18
	public float counter; // 0x20
	public BuffStackCfgPremeditate.Param param; // 0x24
	public int netId; // 0x44
	public Vector2 lastPos; // 0x48
	public emBuff buff; // 0x50
	public string[] descParams; // 0x58
	public BuffFuncNodeMoniter monitor; // 0x60
	public Func<BuffState> <>9__5; // 0x68
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass11_0 // TypeDefIndex: 17897
{
	// Fields
	public RGController controller; // 0x10
	public int consumed; // 0x18
	public BuffStackCfgConsumeRecycle.Param param; // 0x1C
	public BuffFuncNodeMoniter monitor; // 0x38
	public emBuff buff; // 0x40
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass127_0 // TypeDefIndex: 17898
{
	// Fields
	public SummonOwlTalentRuntime runtime; // 0x10
	public RGController controller; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass12_0 // TypeDefIndex: 17899
{
	// Fields
	public int coinAmount; // 0x10
	public BuffComAttribute attributeNode; // 0x18
	public BuffFuncNodeMoniter monitor; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass17_0 // TypeDefIndex: 17900
{
	// Fields
	public BuffFuncNodeMoniter monitor; // 0x10
	public bool coolingDown; // 0x18
	public float cooldownRemain; // 0x1C
	public BuffStackCfgPeriodicImmuneElement.Param param; // 0x20
	public int netId; // 0x24
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass18_0 // TypeDefIndex: 17901
{
	// Fields
	public RGController controller; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass220_0 // TypeDefIndex: 17902
{
	// Fields
	public RGController controller; // 0x10
	public emBuff buffId; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass224_0 // TypeDefIndex: 17903
{
	// Fields
	public GameObject effect; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass225_0 // TypeDefIndex: 17904
{
	// Fields
	public PetChargeBattleTextEffect battleTextEffect; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass23_0 // TypeDefIndex: 17905
{
	// Fields
	public RGController controller; // 0x10
	public BuffStackCfgStingRevenge.Param param; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass27_0 // TypeDefIndex: 17906
{
	// Fields
	public int netId; // 0x10
	public BuffStackCfgSoulStrike.Param param; // 0x14
	public BuffFuncTR_Semaphore.Semaphore semaphore; // 0x30
	public RGController controller; // 0x38
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass29_0 // TypeDefIndex: 17907
{
	// Fields
	public RGController controller; // 0x10
	public int barrierCounter; // 0x18
	public BuffStackCfgFrostNovaOnShieldHit.Param param; // 0x1C
	public BuffFuncNode buffFuncNode; // 0x30
	public int netId; // 0x38
	public int delayElapse; // 0x3C
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass2_0 // TypeDefIndex: 17908
{
	// Fields
	public emBuff hostBuffId; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass32_0 // TypeDefIndex: 17909
{
	// Fields
	public BuffFuncNode buffFuncNode; // 0x10
	public BuffStackCfgRegularFireBreath.Param param; // 0x18
	public RGController controller; // 0x30
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass33_0 // TypeDefIndex: 17910
{
	// Fields
	public BuffFuncNodeWithTrigger buffFuncNode; // 0x10
	public BuffStackCfgMoveReleaseHurricane.Param param; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass34_0 // TypeDefIndex: 17911
{
	// Fields
	public RGController controller; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass35_0 // TypeDefIndex: 17912
{
	// Fields
	public RGController controller; // 0x10
	public BuffStackCfgAirReinforcement.Param param; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass38_0 // TypeDefIndex: 17913
{
	// Fields
	public RGController controller; // 0x10
	public int sputterDamage; // 0x18
	public string[] descParams; // 0x20
	public int netId; // 0x28
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass39_0 // TypeDefIndex: 17914
{
	// Fields
	public RGController controller; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass40_0 // TypeDefIndex: 17915
{
	// Fields
	public RGController controller; // 0x10
	public bool seedInit; // 0x18
	public int randomSeed; // 0x1C
	public RGRandom rgRandom; // 0x20
	public Dictionary<int, int> overPetIndexes; // 0x28
	public List<int> candidatesPetIdx; // 0x30
	public BuffStackCfgSummonPet.Param param; // 0x38
	public int currentPetCount; // 0x3C
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass41_0 // TypeDefIndex: 17916
{
	// Fields
	public float elapsedTime; // 0x10
	public float checkInterval; // 0x14
	public RGController controller; // 0x18
	public Dictionary<IAreaEffect, float> addSizeFactors; // 0x20
	public BuffStackCfgElementalAreaEnhance.Param param; // 0x28
	public Dictionary<IAreaEffect, float> addDuration; // 0x40
	public Dictionary<IAreaEffect, float> addDamageFactors; // 0x48
	public BuffFuncNodeMoniter root; // 0x50
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass42_0 // TypeDefIndex: 17917
{
	// Fields
	public RGController controller; // 0x10
	public int attackCount; // 0x18
	public BuffStackCfgHeavyStrike.Param param; // 0x1C
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass42_1 // TypeDefIndex: 17918
{
	// Fields
	public BulletInfo bulletInfo; // 0x10
	public Vector3 controllerPos; // 0x150
	public DamageInfo damageInfo; // 0x15C
	public float angles; // 0x184
	public RGWeapon weapon; // 0x188
	public TalentBuff.<>c__DisplayClass42_0 CS$<>8__locals1; // 0x190
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass43_0 // TypeDefIndex: 17919
{
	// Fields
	public RGController controller; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass45_0 // TypeDefIndex: 17920
{
	// Fields
	public float timeElapsed; // 0x10
	public BuffStackCfgSiphon.Param param; // 0x14
	public RGController controller; // 0x30
	public int accumulate; // 0x38
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass45_1 // TypeDefIndex: 17921
{
	// Fields
	public GameObject trailFxPrefab; // 0x10
	public CircleDamageCarrier damageCarrier; // 0x18
	public GameObject fxPrefab; // 0x20
	public TalentBuff.<>c__DisplayClass45_0 CS$<>8__locals1; // 0x28
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass45_2 // TypeDefIndex: 17922
{
	// Fields
	public CommonUpdateMono updateMono; // 0x10
	public GameObject fxobj; // 0x18
	public TalentBuff.<>c__DisplayClass45_1 CS$<>8__locals2; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass46_0 // TypeDefIndex: 17923
{
	// Fields
	public RGController controller; // 0x10
	public BuffStackCfgRandomDodge.Param param; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass47_0 // TypeDefIndex: 17924
{
	// Fields
	public float elapsedTime; // 0x10
	public BuffStackCfgMoreSalary.Param param; // 0x14
	public RGController controller; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass48_0 // TypeDefIndex: 17925
{
	// Fields
	public RGController controller; // 0x10
	public BuffStackCfgGiftPackage.Param param; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass49_0 // TypeDefIndex: 17926
{
	// Fields
	public int stack; // 0x10
	public RGController controller; // 0x18
	public BuffComAttribute currentAttrNode; // 0x20
	public BuffStackCfgSkillPowStack.Param param; // 0x28
	public IBuffFuncTarget buffTarget; // 0x30
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass50_0.<<InitBuffFireWork>g__CreateEther|0>d : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 17927
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public float minWait; // 0x20
	public float maxWait; // 0x24
	public float time; // 0x28
	public RGController rgController; // 0x30
	public TalentBuff.<>c__DisplayClass50_0 <>4__this; // 0x38
	private GameObject <obj>5__2; // 0x40
	private float <wait>5__3; // 0x48
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass50_0 // TypeDefIndex: 17928
{
	// Fields
	public RGController controller; // 0x10
	public BuffStackCfgFireWork.Param param; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass51_0 // TypeDefIndex: 17929
{
	// Fields
	public RGController controller; // 0x10
	public BuffStackCfgPotionOnKill.Param param; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass53_0 // TypeDefIndex: 17930
{
	// Fields
	public RGController controller; // 0x10
	public BuffStackCfgLaserRefraction.Param param; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass57_0 // TypeDefIndex: 17931
{
	// Fields
	public int netId; // 0x10
	public BuffFuncComposite root; // 0x18
	public int tempHp; // 0x20
	public RGController controller; // 0x28
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass59_0 // TypeDefIndex: 17932
{
	// Fields
	public RGController controller; // 0x10
	public int accumulation; // 0x18
	public BuffStackCfgMissionKillPlant.Param param; // 0x1C
	public int netId; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass5_0 // TypeDefIndex: 17933
{
	// Fields
	public RGController controller; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass60_0 // TypeDefIndex: 17934
{
	// Fields
	public RGController controller; // 0x10
	public BuffStackCfgGetCoinAddEnergy.Param param; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass65_0 // TypeDefIndex: 17935
{
	// Fields
	public int Count; // 0x10
	public RGController controller; // 0x18
	public BuffStackCfgIncreaseVolume.Param param; // 0x20
	public int netId; // 0x34
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass66_0 // TypeDefIndex: 17936
{
	// Fields
	public emBuff buff; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass67_0 // TypeDefIndex: 17937
{
	// Fields
	public emBuff buff; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass68_0 // TypeDefIndex: 17938
{
	// Fields
	public emBuff buff; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass6_0 // TypeDefIndex: 17939
{
	// Fields
	public BuffFuncNode buffFuncNode; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass71_0 // TypeDefIndex: 17940
{
	// Fields
	public RGController controller; // 0x10
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass76_0 // TypeDefIndex: 17941
{
	// Fields
	public RGController controller; // 0x10
	public BuffStackCfgArmorDamageConversion.Param param; // 0x18
	public emBuff buff; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass79_0 // TypeDefIndex: 17942
{
	// Fields
	public RGController controller; // 0x10
	public float lastAttackTime; // 0x18
	public int currentStack; // 0x1C
	public BuffStackCfgAttackMoveSpeedStack.Param param; // 0x20
	public float cooldownTimer; // 0x2C
	public string key; // 0x30
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass7_0.<<InitBuffCyclicElementEnhance>g__FlyElementMissile|7>d : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 17943
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public Vector3 targetPosition; // 0x20
	public Vector3 startPosition; // 0x2C
	public float initialAngleOffset; // 0x38
	public TalentBuff.<>c__DisplayClass7_0 <>4__this; // 0x40
	public float duration; // 0x48
	public GameObject missileObject; // 0x50
	public Func<bool> isFlying; // 0x58
	private Vector3 <startControlPosition>5__2; // 0x60
	private Vector3 <endControlPosition>5__3; // 0x6C
	private float <elapsed>5__4; // 0x78
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass7_0 // TypeDefIndex: 17944
{
	// Fields
	public RGController controller; // 0x10
	public BuffStackCfgCyclicElementEnhance.Param param; // 0x18
	public float _elapsedTime; // 0x24
	public string[] elementCirclePaths; // 0x28
	public string[] missilePaths; // 0x30
	public float _nextMissileCreateTime; // 0x38
	public int currentIdx; // 0x3C
	public BuffDisplayBasicInfo buffDisplayInfo; // 0x40
	public Tuple<ElementalType, string, string, string>[] list; // 0x48
	public BuffFuncNodeMoniter monitor; // 0x50
	public emBuff buff; // 0x58
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass7_1 // TypeDefIndex: 17945
{
	// Fields
	public bool isFlying; // 0x10
	public bool hasTriggered; // 0x11
	public int elementIdx; // 0x14
	public GameObject missileObject; // 0x18
	public RGWeapon sourceWeapon; // 0x20
	public TalentBuff.<>c__DisplayClass7_0 CS$<>8__locals1; // 0x28
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass8_0 // TypeDefIndex: 17946
{
	// Fields
	public RGController controller; // 0x10
	public BuffStackCfgExEnergyForExDamage.Param param; // 0x18
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
private sealed class TalentBuff.<>c__DisplayClass96_0 // TypeDefIndex: 17947
{
	// Fields
	public BuffStackCfgDropCaptainBox.Param param; // 0x10
	public RGRoomX lastRoom; // 0x20
}

// --------------------------------------------------------------------
// TalentBuff  [class]
// --------------------------------------------------------------------
public class TalentBuff // TypeDefIndex: 17948
{
	// Fields
	private static Color[] LaserRandomColor; // 0x0
	private static string[] RandomMechaName; // 0x8
	private static TalentBuff.MechaAttributeConfig[] RandomMechaAttrs; // 0x10
	private static emBuff[] killPlantRandomBuff; // 0x18
	private const float PetChargeDefaultAttackSpeedFactor = 0.7;
	private const float PetChargeDefaultDamageFactor = 0;
	private const float PetChargeDefaultMoveSpeedFactor = 0.6;
	private const float PetChargePermanentDuration = -1;
	private const string PetChargeBuffPrefabPath = "RGPrefab/Bullet/Buff/buff_pet_charge.prefab";
	private const string PetChargeBuffObjectName = "buff_pet_charge";
	private const string PetChargeEffectPrefabPath = "RGPrefab/Bullet/Buff/buff_pet_charge_effect.prefab";
	private const string PetChargeEffectObjectName = "buff_pet_charge_effect";
}

// --------------------------------------------------------------------
// ExpertPetTalentBaseRotator  [class]
// --------------------------------------------------------------------
public class ExpertPetTalentBaseRotator : MonoBehaviour // TypeDefIndex: 17950
{
	// Fields
	private float rotateSpeed; // 0x20
}

// --------------------------------------------------------------------
// ExpertPetTalentEffect  [class]
// --------------------------------------------------------------------
public static class ExpertPetTalentEffect // TypeDefIndex: 17951
{
	// Fields
	public const float DefaultSizeAndHpAddFactor = 0.5;
	public const float DefaultDamageAddFactor = 1;
	private static float _sizeAndHpAddFactor; // 0x0
	private static float _damageAddFactor; // 0x4
}

// --------------------------------------------------------------------
// ExpertPetTalentEffectState  [struct]
// --------------------------------------------------------------------
private struct ExpertPetTalentEffectState.Modifier // TypeDefIndex: 17952
{
	// Fields
	public float sizeMultiplier; // 0x0
	public float maxHpMultiplier; // 0x4
	public float damageAddFactor; // 0x8
	public bool showBaseEffect; // 0xC
}

// --------------------------------------------------------------------
// ExpertPetTalentEffectState  [class]
// --------------------------------------------------------------------
public class ExpertPetTalentEffectState : MonoBehaviour // TypeDefIndex: 17953
{
	// Fields
	private const string BaseEffectPrefabPath = "RGPrefab/Effect/expert_pet_talent_base.prefab";
	private const string ExpertPetModifierKey = "BuffExpertPet";
	private readonly Dictionary<string, ExpertPetTalentEffectState.Modifier> _modifiers; // 0x20
	private RGBaseController _controller; // 0x28
	private GameObject _baseEffect; // 0x30
	private bool _applied; // 0x38
	private float _appliedScaleMultiplier; // 0x3C
	private Vector3 _baseScale; // 0x40
	private Vector3 _appliedScale; // 0x4C
	private float _appliedDamageAddFactor; // 0x58
}

// --------------------------------------------------------------------
// RuntimeLuaTalent  [class]
// --------------------------------------------------------------------
public class RuntimeLuaTalent // TypeDefIndex: 17973
{
	// Fields
	private readonly LuaTable _handler; // 0x10
	private readonly LuaFunction _descArgsGetter; // 0x18
	private readonly LuaFunction _nameGetter; // 0x20
	private readonly LuaFunction _descGetter; // 0x28
	private readonly LuaFunction _randomCondition; // 0x30
	private readonly emBuff <Id>k__BackingField; // 0x38
}

// --------------------------------------------------------------------
// RuntimeLuaTalentRegistry  [class]
// --------------------------------------------------------------------
public static class RuntimeLuaTalentRegistry // TypeDefIndex: 17974
{
	// Fields
	private static readonly Dictionary<emBuff, RuntimeLuaTalent> Talents; // 0x0
	private static readonly List<emBuff> TalentIds; // 0x8
	private static readonly HashSet<emBuff> TalentIdSet; // 0x10
	private static LuaTable _talentModule; // 0x18
	private static LuaFunction _allIdsGetter; // 0x20
	private static LuaFunction _handlerGetter; // 0x28
	private static bool _isInitialized; // 0x30
}

// --------------------------------------------------------------------
// RgControllerTest  [class]
// --------------------------------------------------------------------
private sealed class RgControllerTest.<>c // TypeDefIndex: 18004
{
	// Fields
	public static readonly RgControllerTest.<>c <>9; // 0x0
	public static TweenCallback <>9__19_0; // 0x8
	public static Action <>9__22_0; // 0x10
	public static Action <>9__22_3; // 0x18
}

// --------------------------------------------------------------------
// RgControllerTest  [class]
// --------------------------------------------------------------------
private sealed class RgControllerTest.<>c__DisplayClass20_0 // TypeDefIndex: 18005
{
	// Fields
	public RgControllerTest <>4__this; // 0x10
	public int index; // 0x18
}

// --------------------------------------------------------------------
// RgControllerTest  [class]
// --------------------------------------------------------------------
private sealed class RgControllerTest.<>c__DisplayClass22_0 // TypeDefIndex: 18006
{
	// Fields
	public RgControllerTest <>4__this; // 0x10
	public RGRoomX roomX; // 0x18
	public int nextIndex; // 0x20
}

// --------------------------------------------------------------------
// RgControllerTest  [class]
// --------------------------------------------------------------------
private sealed class RgControllerTest.<>c__DisplayClass27_0 // TypeDefIndex: 18007
{
	// Fields
	public RGRoomX roomX; // 0x10
	public RgControllerTest <>4__this; // 0x18
	public int nextIndex; // 0x20
}

// --------------------------------------------------------------------
// RgControllerTest  [class]
// --------------------------------------------------------------------
private sealed class RgControllerTest.<>c__DisplayClass32_0 // TypeDefIndex: 18008
{
	// Fields
	public RgControllerTest <>4__this; // 0x10
	public RGRoomX roomX; // 0x18
}

// --------------------------------------------------------------------
// RgControllerTest  [class]
// --------------------------------------------------------------------
private sealed class RgControllerTest.<>c__DisplayClass32_1 // TypeDefIndex: 18009
{
	// Fields
	public Transform transferGate; // 0x10
	public RgControllerTest.<>c__DisplayClass32_0 CS$<>8__locals1; // 0x18
}

// --------------------------------------------------------------------
// RgControllerTest  [class]
// --------------------------------------------------------------------
private sealed class RgControllerTest.<KillTheTestSequence>d__39 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 18010
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RgControllerTest <>4__this; // 0x20
}

// --------------------------------------------------------------------
// RgControllerTest  [class]
// --------------------------------------------------------------------
private sealed class RgControllerTest.<MonitorEnemyHealth>d__30 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 18011
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RgControllerTest <>4__this; // 0x20
	public RGEController enemyController; // 0x28
	public Action<RGRoomX, int> onComplete; // 0x30
	public RGRoomX roomX; // 0x38
	public int nextIndex; // 0x40
}

// --------------------------------------------------------------------
// RgControllerTest  [class]
// --------------------------------------------------------------------
private sealed class RgControllerTest.<WaitForEnemiesAndStartBehaviour>d__25 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 18012
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public RGRoomX roomX; // 0x20
	public RgControllerTest <>4__this; // 0x28
	public int nextIndex; // 0x30
}

// --------------------------------------------------------------------
// RgControllerTest  [class]
// --------------------------------------------------------------------
public class RgControllerTest : MonoBehaviour // TypeDefIndex: 18013
{
	// Fields
	private Vector3 targetPosition; // 0x20
	private bool isMoving; // 0x2C
	private Coroutine monitorEnemyCoroutine; // 0x30
	private Path<RGRoomX> startToEndShortestPath; // 0x38
	private readonly Queue<Action> moveCompleteCallbacks; // 0x40
	private AutoTestConfig testConfig; // 0x48
	private ProfilerReport _profilerReport; // 0x50
	public int retryCount; // 0x58
	private float roomStartTime; // 0x5C
	private const float RoomTimeout = 300;
	private bool isSingleBehaviourExecuting; // 0x60
	private int _allBranchIndex; // 0x64
	private static readonly int[] StartIndexes; // 0x0
	private static readonly int[] EndIndexes; // 0x8
}

// --------------------------------------------------------------------
// RoleAttributeProxy  [class]
// --------------------------------------------------------------------
private sealed class RoleAttributeProxy.<>c // TypeDefIndex: 18277
{
	// Fields
	public static readonly RoleAttributeProxy.<>c <>9; // 0x0
	public static Func<float, float> <>9__23_0; // 0x8
	public static Func<float, float> <>9__23_1; // 0x10
	public static Func<float, float> <>9__23_2; // 0x18
}

// --------------------------------------------------------------------
// RoleAttributeProxy  [class]
// --------------------------------------------------------------------
public class RoleAttributeProxy // TypeDefIndex: 18278
{
	// Fields
	private readonly RoleAttribute <Client>k__BackingField; // 0x10
	private RoleAttributeValue<float> _speedValue; // 0x18
	private HashSet<object> _immuneMoveSpeedSlowDownTags; // 0x20
	private RoleAttributeValue<int> _maxHpValue; // 0x28
	private HashSet<object> _criticalChance100; // 0x30
	private RoleAttributeValue<int> _maxArmorValue; // 0x38
	public int MaxArmor_Sync; // 0x40
	private RoleAttributeValue<int> _maxEnergyValue; // 0x48
	public float atk_damage_factor; // 0x50
	public RoleAttributeValue<float> potionEffectFactor; // 0x58
	public RoleAttributeValue<float> HealPowerFactor; // 0x60
	public RoleAttributeValue<float> chargeTimeFactor; // 0x68
	public RoleAttributeValue<float> defenceLevel; // 0x70
	public float takeDamageFactor; // 0x78
	public float inflictDamagFactor; // 0x7C
	private Action onAtkSpeedChanged; // 0x80
	private static AttributeValue defaultValue; // 0x0
	private static AttributeValue defaultFactor; // 0x8
	private float _hpRengenTimer; // 0x88
	private float _enRegenTimer; // 0x8C
	private float _healthRegen; // 0x90
	private float _energyRegen; // 0x94
	public RoleAttributeValue<float> hpRegenSpeedFactor; // 0x98
	public RoleAttributeValue<float> enRegenSpeedFactor; // 0xA0
	private Action<bool> OnInvincibleStateChanged; // 0xA8
	private HashSet<object> _protectedFromDamageCounter; // 0xB0
	public Func<bool> immuneDizzyChecker; // 0xB8
	private Action<bool, object> OnDisableStateChanged; // 0xC0
	private HashSet<object> _disableCounter; // 0xC8
	public float inertial_vel; // 0xD0
	public Vector2 force_direction; // 0xD4
}

// --------------------------------------------------------------------
// RoleAttributeValue  [class]
// --------------------------------------------------------------------
private sealed class RoleAttributeValue.<>c<T> // TypeDefIndex: 18283
{
	// Fields
	public static readonly RoleAttributeValue.<>c<T> <>9; // 0x0
	public static Func<KeyValuePair<object, string>, bool> <>9__41_0; // 0x0
	public static Func<T, T, T> <>9__59_0; // 0x0
}

// --------------------------------------------------------------------
// RoleAttributeValue  [class]
// --------------------------------------------------------------------
private sealed class RoleAttributeValue.<>c__DisplayClass48_0<T> // TypeDefIndex: 18284
{
	// Fields
	public string key; // 0x0
	public Func<Tuple<string, T>, bool> <>9__0; // 0x0
}

// --------------------------------------------------------------------
// RoleAttributeValue  [class]
// --------------------------------------------------------------------
public class RoleAttributeValue<T> // TypeDefIndex: 18285
{
	// Fields
	private T _originalValue; // 0x0
	private T _baseValue; // 0x0
	private readonly T _minimum; // 0x0
	private readonly T _maximum; // 0x0
	private Dictionary<string, T> _fixedValues; // 0x0
	private T _finalValue; // 0x0
	private T _finalValueWithoutTemp; // 0x0
	private T _lastFinalValue; // 0x0
	private bool _dirty; // 0x0
	private List<Tuple<string, T>> _permanentAdditionValues; // 0x0
	private Dictionary<string, T> _additionValues; // 0x0
	private Dictionary<string, T> _tempAdditionValues; // 0x0
	private Dictionary<string, float> _accumulateMultiplicationValues; // 0x0
	private Dictionary<string, float> _multiplicationValues; // 0x0
	private Dictionary<object, string> _references; // 0x0
	private int _referenceCount; // 0x0
	public Func<T, T> _addValueFilter; // 0x0
	public Func<float, float> _accumulateMultiplierFilter; // 0x0
	public Func<float, float> _multiplierValueFilter; // 0x0
}

// --------------------------------------------------------------------
// RoleAttributeValueEnum  [class]
// --------------------------------------------------------------------
public static class RoleAttributeValueEnum // TypeDefIndex: 18286
{
	// Fields
	public const string MaxHpUp = "MaxHpUp";
	public const string MercenaryHpx2 = "MercenaryHpx2";
	public const string PhantomSplit = "PhantomSplit";
	public const string AssassinPhantom = "AssassinPhantom";
	public const string OnAdditionLevelChanged = "OnAdditionLevelChanged";
	public const string VampireImmortal = "VampireImmortal";
	public const string WarliegeSpriteSurging = "WarliegeSpriteSurging";
	public const string PetFood = "PetFood";
	public const string OnDigestLevelChanged = "OnDigestLevelChanged";
	public const string ChangeAttribute = "ChangeAttribute";
	public const string FurniturePlant = "FurniturePlant";
	public const string WeaponSpeedDown = "WeaponSpeedDown";
	public const string LancerSkill2SpeedUp = "LancerSkill2SpeedUp";
	public const string JokerSkill1SpeedUp = "JokerSkill1SpeedUp";
	public const string WarliegeSkill2 = "WarliegeSkill2";
	public const string WarlockSkill0SpeedUp = "WarlockSkill0SpeedUp";
	public const string YinYangSkill0SpeedUp = "YinYangSkill0SpeedUp";
	public const string LevelUpMaxHp = "LevelUpMaxHp";
	public const string LevelUpMaxArmor = "LevelUpMaxArmor";
	public const string LevelUpMaxEnergy = "LevelUpMaxEnergy";
	public const string BuffMaxHp = "BuffMaxHp";
	public const string BuffMaxArmor = "BuffMaxArmor";
	public const string BuffMaxEnergy = "BuffMaxEnergy";
	public const string BuffMaxEnergyUpgrade = "BuffMaxEnergyUpgrade";
	public const string BuffMaxEnergyLoop = "BuffMaxEnergyLoop";
	public const string BuffTutor = "BuffTutor";
	public const string BuffExpertPet = "BuffExpertPet";
	public const string BuffCoinAddArmor = "BuffCoinAddArmor";
	public const string BuffMasterHold = "BuffMasterHold";
	public const string BuffC11Skill0MaxArmor = "BuffC11Skill0MaxArmor";
	public const string BuffExpertPotHealPower = "BuffExpertPotHealPower";
	public const string BattleFactorHuge = "BattleFactorHuge";
	public const string BattleFactorTiny = "BattleFactorTiny";
	public const string BattleFactorLackHp = "BattleFactorLackHp";
	public const string BattleFactorLackEnergy = "BattleFactorLackEnergy";
	public const string BattleFactorZongziStyle = "BattleFactorZongziStyle";
	public const string ArtifactPartsImproveMaxHp = "ArtifactPartsImproveMaxHp";
	public const string ArtifactPartsFixMaxHp = "ArtifactPartsFixMaxHp";
	public const string ArtifactPartsImproveMaxArmor = "ArtifactPartsImproveMaxArmor";
	public const string ArtifactPartsImproveMaxEnergy = "ArtifactPartsImproveMaxEnergy";
	public const string DefenceTowerStarRatingMaxHp = "DefenceTowerStarRatingMaxHp";
	public const string DefenceSkillLevel = "DefenceSkillLevel";
	public const string RobotPawnReduceSpeed = "RobotPawnReduceSpeed";
}

// --------------------------------------------------------------------
// C39Skill2TalentSelectionPanel  [class]
// --------------------------------------------------------------------
private sealed class C39Skill2TalentSelectionPanel.OptionView // TypeDefIndex: 18448
{
	// Fields
	public RectTransform Root; // 0x10
	public Button Button; // 0x18
	public UIC39BuffCardInfo Card; // 0x20
	public CaptainSkillBoatModuleConfig Config; // 0x28
}

// --------------------------------------------------------------------
// C39Skill2TalentSelectionPanel  [class]
// --------------------------------------------------------------------
private sealed class C39Skill2TalentSelectionPanel.<>c__DisplayClass34_0 // TypeDefIndex: 18449
{
	// Fields
	public C39Skill2TalentSelectionPanel <>4__this; // 0x10
	public int optionIndex; // 0x18
}

// --------------------------------------------------------------------
// C39Skill2TalentSelectionPanel  [class]
// --------------------------------------------------------------------
private sealed class C39Skill2TalentSelectionPanel.<>c__DisplayClass38_0 // TypeDefIndex: 18450
{
	// Fields
	public C39Skill2TalentSelectionPanel <>4__this; // 0x10
	public int optionIndex; // 0x18
	public TweenCallback <>9__0; // 0x20
}

// --------------------------------------------------------------------
// C39Skill2TalentSelectionPanel  [class]
// --------------------------------------------------------------------
public class C39Skill2TalentSelectionPanel : MonoBehaviour // TypeDefIndex: 18451
{
	// Fields
	private const string Skill2PanelPrefabPath = "RGPrefab/Characters/Captain/skin_0/skill_2/UI/CaptaionSkill2Panel.prefab";
	private const string DamageCardPrefabPath = "RGPrefab/Characters/Captain/skin_0/skill_2/UI/ui_buff_card_red.prefab";
	private const string SkillCardPrefabPath = "RGPrefab/Characters/Captain/skin_0/skill_2/UI/ui_buff_card_blue.prefab";
	private const string BehaviorCardPrefabPath = "RGPrefab/Characters/Captain/skin_0/skill_2/UI/ui_buff_card_green.prefab";
	private static readonly Color RefreshButtonDisabledColor; // 0x0
	private float panelVisibleAlpha; // 0x20
	private float panelFadeInDuration; // 0x24
	private float panelFadeOutDuration; // 0x28
	private AudioClip showUpAudioClip; // 0x30
	private AudioClip refreshAudioClip; // 0x38
	private AudioClip confirmSuccessAudioClip; // 0x40
	private readonly List<C39Skill2TalentSelectionPanel.OptionView> _optionViews; // 0x48
	private C39Controller _controller; // 0x50
	private Graphic _panelGraphic; // 0x58
	private Button _maskButton; // 0x60
	private Button _checkButton; // 0x68
	private Button _refreshButton; // 0x70
	private RectTransform _cardsRoot; // 0x78
	private int _selectedOptionIndex; // 0x80
	private bool _isConfirmingSelection; // 0x84
	private bool _interactionEnabled; // 0x85
	private bool _refreshUsed; // 0x86
	private Tween _panelFadeTween; // 0x88
	private Color _panelBaseColor; // 0x90
}

// --------------------------------------------------------------------
// CaptainSkillBoatTalentSynergyHelper  [class]
// --------------------------------------------------------------------
public static class CaptainSkillBoatTalentSynergyHelper // TypeDefIndex: 18622
{
	// Fields
	private const float MasterSpeedAtkFireRateMultiplier = 1.5;
	private const float MasterSwordRangeSailorRaidSlashSizeMultiplier = 1.4;
	private const int ExpertPetBirdCountMultiplier = 2;
	private const int HitThunderStormStrikeCountMultiplier = 2;
	private const float MinAdjustedInterval = 0.01;
}

// --------------------------------------------------------------------
// MasterTalentBackpackItemViewData  [class]
// --------------------------------------------------------------------
public sealed class MasterTalentBackpackItemViewData // TypeDefIndex: 19314
{
	// Fields
	public emBuff Buff; // 0x10
	public string Name; // 0x18
	public string Desc; // 0x20
	public Sprite Icon; // 0x28
	public bool IsCollected; // 0x30
	public bool IsEquipped; // 0x31
	public bool IsFreeEquipTarget; // 0x32
}

// --------------------------------------------------------------------
// MasterTalentWishGachaCharacterPlayer  [class]
// --------------------------------------------------------------------
public sealed class MasterTalentWishGachaCharacterPlayer : MonoBehaviour // TypeDefIndex: 19315
{
	// Fields
	private const string CharacterSpritePathFormat = "RGTexture/ui/master_advancement/gacha_character/character{0:D2}.png";
	private const string CharacterRootNameFormat = "character{0:D2}";
	private const string FallbackCharacterSpritePath = "RGTexture/ui/master_advancement/gacha_character/character20.png";
	private const string AnimationCharacterRootName = "character20";
	private const string BGPath = "root/BG";
	private const string CharacterPrefix = "character";
	private const string UnusedCharacterPrefix = "unused_master_talent_wish_character_";
	private const string SmallName = "char small";
	private const string MaskName = "mask";
	private const string Big1Name = "char big1";
	private const string Big2Name = "char big2";
	private const string Big3Name = "char big3";
	private const int RequiredFrameCount = 3;
	private SpriteRenderer _smallRenderer; // 0x20
	private SpriteRenderer _big1Renderer; // 0x28
	private SpriteRenderer _big2Renderer; // 0x30
	private SpriteRenderer _big3Renderer; // 0x38
}

// --------------------------------------------------------------------
// UIMasterTalentBackpackItemWidget  [class]
// --------------------------------------------------------------------
public class UIMasterTalentBackpackItemWidget : MonoBehaviour // TypeDefIndex: 19332
{
	// Fields
	private const string IconPath = "icon";
	private const string FramePath = "frame";
	private const string SelectPath = "select";
	private const string EquippedTagPath = "equipped_tag";
	private const string FreeTagPath = "free_tag";
	private const string LockedOverlayPath = "locked_overlay";
	private const string LockedOverlayLockPath = "lock";
	private const string GrayMaterialPath = "RGMaterial/UI_Grayscale.mat";
	private static readonly Color LockedGraphicColor; // 0x0
	private static readonly Color UncollectedFallbackIconColor; // 0x10
	private static readonly Color CollectedFallbackIconColor; // 0x20
	private static readonly Color TransparentColor; // 0x30
	private static Material _grayMaterial; // 0x40
	private Button _button; // 0x20
	private Image _iconImage; // 0x28
	private Image _frameImage; // 0x30
	private GameObject _select; // 0x38
	private GameObject _equippedTag; // 0x40
	private GameObject _freeTag; // 0x48
	private GameObject _lockedOverlay; // 0x50
	private Image _lockedOverlayImage; // 0x58
	private GameObject _lockedOverlayLock; // 0x60
	private Image _lockedOverlayLockImage; // 0x68
	private Color _frameDefaultColor; // 0x70
	private bool _frameDefaultColorCached; // 0x80
	private MasterTalentBackpackItemViewData _data; // 0x88
	private Action<emBuff> _onClick; // 0x90
}

// --------------------------------------------------------------------
// UIMasterTalentBackpackListWidget  [class]
// --------------------------------------------------------------------
public class UIMasterTalentBackpackListWidget : BaseUIWidget, IEnhancedScrollerDelegate // TypeDefIndex: 19333
{
	// Fields
	private const string ScrollerPath = "Scroller";
	private const string RowTemplatePath = "Templates/RowCellTemplate";
	private const float RowHeight = 86;
	private EnhancedScroller _scroller; // 0x48
	private ScrollRect _scrollRect; // 0x50
	private UIMasterTalentBackpackRowCellView _rowTemplate; // 0x58
	private readonly List<MasterTalentBackpackItemViewData> _items; // 0x60
	private emBuff _selectedBuff; // 0x68
	private Action<emBuff> _onClickBuff; // 0x70
}

// --------------------------------------------------------------------
// UIMasterTalentBackpackRowCellView  [class]
// --------------------------------------------------------------------
public class UIMasterTalentBackpackRowCellView : EnhancedScrollerCellView // TypeDefIndex: 19334
{
	// Fields
	public const int ColumnCount = 6;
	private const string ItemsRootPath = "items";
	private const string ItemNamePrefix = "item_";
	private const string RuntimeItemNamePrefix = "runtime_item_";
	private const float ItemSpacing = 14;
	private readonly List<UIMasterTalentBackpackItemWidget> _items; // 0x38
	private IReadOnlyList<MasterTalentBackpackItemViewData> _sourceItems; // 0x40
	private emBuff _selectedBuff; // 0x48
	private Action<emBuff> _onClickBuff; // 0x50
}

// --------------------------------------------------------------------
// UIMasterTalentConfirmWindowData  [class]
// --------------------------------------------------------------------
public sealed class UIMasterTalentConfirmWindowData // TypeDefIndex: 19335
{
	// Fields
	public emHero Hero; // 0x10
	public MasterPracticeDisplayData DisplayData; // 0x18
	public Func<MasterPracticeDisplayData, bool> OnConfirm; // 0x20
	public Action OnClose; // 0x28
}

// --------------------------------------------------------------------
// UIMasterTalentConfirmWindowBase  [class]
// --------------------------------------------------------------------
public abstract class UIMasterTalentConfirmWindowBase : BaseUIView // TypeDefIndex: 19336
{
	// Fields
	private const string BackgroundPath = "Root";
	private const string CloseButtonPath = "Root/panel/btn_close";
	private const string TitlePath = "Root/panel/title";
	private const string NoticeTextPath = "Root/panel/content/notice_text";
	private const string CurrentRootPath = "Root/panel/content/current";
	private const string CurrentIconRootPath = "Root/panel/content/current/icon_bg/item_0";
	private const string CurrentIconPath = "Root/panel/content/current/icon_bg/item_0/icon";
	private const string CurrentIconTextPath = "Root/panel/content/current/icon_bg/item_0/text";
	private const string CurrentNamePath = "Root/panel/content/current/name";
	private const string ArrowPath = "Root/panel/content/arrow";
	private const string TargetRootPath = "Root/panel/content/target";
	private const string TargetIconRootPath = "Root/panel/content/target/icon_bg/item_0";
	private const string TargetIconPath = "Root/panel/content/target/icon_bg/item_0/icon";
	private const string TargetIconTextPath = "Root/panel/content/target/icon_bg/item_0/text";
	private const string TargetNamePath = "Root/panel/content/target/name";
	private const string ConfirmButtonPath = "Root/panel/btn_confirm";
	private const string ConfirmButtonTextPath = "Root/panel/btn_confirm/text";
	private const string ButtonCostRootPath = "Root/panel/btn_confirm/button_cost_root";
	private const string ButtonCostPrefixTextName = "prefix_text";
	private const string ButtonPrimaryCostName = "item_0";
	private const string ButtonSecondaryCostName = "item_1";
	private const string ButtonCostSuffixTextName = "suffix_text";
	private const string CostPlaceholder = "{0}";
	private const float ButtonCostTextPadding = 8;
	private const float ButtonCostTextMaxWidth = 320;
	private const float ButtonCostTextMinWidth = 24;
	private const int ButtonCostTextMinFontSize = 18;
	private Image _backgroundImage; // 0xD8
	private Button _closeButton; // 0xE0
	private Text _titleText; // 0xE8
	private Text _noticeText; // 0xF0
	private RectTransform _currentRootRect; // 0xF8
	private GameObject _currentRoot; // 0x100
	private Image _currentIconImage; // 0x108
	private Text _currentIconText; // 0x110
	private Text _currentNameText; // 0x118
	private GameObject _arrow; // 0x120
	private RectTransform _targetRootRect; // 0x128
	private GameObject _targetRoot; // 0x130
	private Image _targetIconImage; // 0x138
	private Text _targetIconText; // 0x140
	private Text _targetNameText; // 0x148
	private Button _confirmButton; // 0x150
	private Text _confirmButtonText; // 0x158
	private GameObject _buttonCostRoot; // 0x160
	private RectTransform _buttonCostRootRect; // 0x168
	private HorizontalLayoutGroup _buttonCostLayoutGroup; // 0x170
	private Text _buttonCostPrefixText; // 0x178
	private UIMasterAdvancementCostItemWidget _buttonPrimaryCostWidget; // 0x180
	private UIMasterAdvancementCostItemWidget _buttonSecondaryCostWidget; // 0x188
	private Text _buttonCostSuffixText; // 0x190
	private UIMasterTalentConfirmWindowData _data; // 0x198
	private bool _isConfirming; // 0x1A0
	private bool _closeCallbackInvoked; // 0x1A1
}

// --------------------------------------------------------------------
// UIMasterTalentReplaceConfirmWindow  [class]
// --------------------------------------------------------------------
public sealed class UIMasterTalentReplaceConfirmWindow : UIMasterTalentConfirmWindowBase // TypeDefIndex: 19337
{
	// Fields
	private const string PrefabPath = "MasterAdvancement/window_master_talent_replace_confirm";
}

// --------------------------------------------------------------------
// UIMasterTalentWishConfirmWindow  [class]
// --------------------------------------------------------------------
public sealed class UIMasterTalentWishConfirmWindow : UIMasterTalentConfirmWindowBase // TypeDefIndex: 19338
{
	// Fields
	private const string PrefabPath = "MasterAdvancement/window_master_talent_wish_confirm";
}

// --------------------------------------------------------------------
// UIMasterTalentWishGachaWindowData  [class]
// --------------------------------------------------------------------
public sealed class UIMasterTalentWishGachaWindowData // TypeDefIndex: 19339
{
	// Fields
	public emHero Hero; // 0x10
	public emBuff WishedBuff; // 0x14
	public Action<emBuff> OnFinished; // 0x18
	public bool KeepOpenAfterPlay; // 0x20
}

// --------------------------------------------------------------------
// UIMasterTalentWishGachaWindow  [class]
// --------------------------------------------------------------------
private sealed class UIMasterTalentWishGachaWindow.<WaitGachaFinishRoutine>d__54 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 19340
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public float duration; // 0x20
	public UIMasterTalentWishGachaWindow <>4__this; // 0x28
	private float <freezeWaitTime>5__2; // 0x30
}

// --------------------------------------------------------------------
// UIMasterTalentWishGachaWindow  [class]
// --------------------------------------------------------------------
public sealed class UIMasterTalentWishGachaWindow : BaseUIView // TypeDefIndex: 19341
{
	// Fields
	private const string PrefabPath = "MasterAdvancement/window_master_talent_wish_gacha";
	private const string EffectPrefabPath = "RGPrefab/UI/MasterAdvancement/MasterGacha/MasterGacha.prefab";
	private const string ClipMaskName = "gacha_clip_mask";
	private const string RawImageName = "gacha_raw_image";
	private const string CharacterClipMaskName = "master_talent_wish_gacha_character_mask";
	private const string CharacterAnimatorStateName = "character20";
	private const string CharacterAnimatorStateFullName = "Base Layer.character20";
	private const string RenderLayerName = "UI";
	private const string EffectSortingLayerName = "UI";
	private const int EffectSortingOrderOffset = 300;
	private const int RenderTextureWidth = 960;
	private const int RenderTextureHeight = 540;
	private const int RenderWorldBase = 120000;
	private const int RenderSlotSize = 300;
	private const int RenderSlotsPerRow = 32;
	private const float CharacterSkinScale = 3;
	private const float ClipMaskAspect = 3.0769231;
	private const float CharacterClipMaskWidth = 40;
	private const float CharacterClipMaskHeight = 13;
	private const float CharacterClipMaskLocalY = 0.04;
	private const float DefaultCameraOrthographicSize = 4;
	private const float CameraFitPadding = 1.15;
	private const float MinCameraOrthographicSize = 2.2;
	private const float MaxCameraOrthographicSize = 8;
	private const float CameraZOffset = -10;
	private const float DefaultPlayDuration = 2.2;
	private const float MinPlayDuration = 1.2;
	private const float MaxPlayDuration = 4;
	private const float EndFrameFreezeLeadTime = 0.03;
	private const float EndFrameNormalizedTime = 0.999;
	private const float SpineEndFrameTimeOffset = 0.001;
	private Image _inputBlocker; // 0xD8
	private RectTransform _clipMaskRect; // 0xE0
	private RawImage _gachaRawImage; // 0xE8
	private UIMasterTalentWishGachaWindowData _data; // 0xF0
	private GameObject _renderRoot; // 0xF8
	private Transform _renderDrawingRoot; // 0x100
	private Camera _renderCamera; // 0x108
	private RenderTexture _renderTexture; // 0x110
	private GameObject _effectInstance; // 0x118
	private Coroutine _playCoroutine; // 0x120
	private bool _finished; // 0x128
}

// --------------------------------------------------------------------
// LevelBuffInfo  [class]
// --------------------------------------------------------------------
public class LevelBuffInfo // TypeDefIndex: 19635
{
	// Fields
	public int id; // 0x10
	public LevelBuffType levelBuffType; // 0x14
	public Sprite icon; // 0x18
}

// --------------------------------------------------------------------
// LevelBuffInfos  [class]
// --------------------------------------------------------------------
private sealed class LevelBuffInfos.<>c__DisplayClass9_0 // TypeDefIndex: 19636
{
	// Fields
	public string spriteName; // 0x10
}

// --------------------------------------------------------------------
// LevelBuffInfos  [class]
// --------------------------------------------------------------------
public class LevelBuffInfos : ScriptableObject // TypeDefIndex: 19637
{
	// Fields
	public List<LevelBuffInfo> levelBuffInfos; // 0x18
	public Dictionary<LevelBuffType, LevelBuffInfo> levelBuffInfoDict; // 0x20
	private bool _hasLoad; // 0x28
	private const string FileName = "Data/level_buff_infos.asset";
	private static LevelBuffInfos _info; // 0x0
}

// --------------------------------------------------------------------
// GiantSwordTalentBulletVisual  [class]
// --------------------------------------------------------------------
internal class GiantSwordTalentBulletVisual : DefaultBulletVisual // TypeDefIndex: 19780
{
	// Fields
	private const string GiantSwordSpawnAudioPath = "RGSound/effect/fx_laser_explode.mp3";
	private GameObject _prefab; // 0x18
	private Vector2 _targetPosition; // 0x20
	private bool _effectShown; // 0x28
}

// --------------------------------------------------------------------
// PvpTalentUiResolver  [class]
// --------------------------------------------------------------------
public static class PvpTalentUiResolver // TypeDefIndex: 19896
{
	// Fields
	private static SpriteAtlas _talentUiAtlas; // 0x0
	private static SpriteAtlas _uiAtlas; // 0x8
}

// --------------------------------------------------------------------
// PvpUITutorialFirstGameTalent  [class]
// --------------------------------------------------------------------
public class PvpUITutorialFirstGameTalent : PvpUITutorial // TypeDefIndex: 19909
{
	// Fields
	private const string FingerPrefabPath = "ModeSeason/PVP/Prefabs/Tutorial/finger_right.prefab";
	private static readonly Vector2 DialogOffset; // 0x0
	private UISquareInputBlocker _inputBlocker; // 0x30
	private UIRectFocusMask _focusMask; // 0x38
	private UITalentSelectionPanel _talentPanel; // 0x40
	private RectTransform _optionsRoot; // 0x48
	private GameObject _dialogGo; // 0x50
	private GameObject _fingerGo; // 0x58
}

// --------------------------------------------------------------------
// UITalentIconButton  [class]
// --------------------------------------------------------------------
public class UITalentIconButton : MonoBehaviour // TypeDefIndex: 19995
{
	// Fields
	private Button _button; // 0x20
	private Image _rarityFrame; // 0x28
	private Image _iconBg; // 0x30
	private Image _icon; // 0x38
	private Action _onClick; // 0x40
}

// --------------------------------------------------------------------
// UITalentSelectionOptionButton  [class]
// --------------------------------------------------------------------
public class UITalentSelectionOptionButton : MonoBehaviour // TypeDefIndex: 19996
{
	// Fields
	private Button _button; // 0x20
	private Image _rarityFrame; // 0x28
	private Image _iconBg; // 0x30
	private Image _icon; // 0x38
	private Image _characterIcon; // 0x40
	private TMP_Text _effectText; // 0x48
	private TMP_Text _nameText; // 0x50
	private int _index; // 0x58
	private Action<int> _onClicked; // 0x60
}

// --------------------------------------------------------------------
// UITalentSelectionPanel  [class]
// --------------------------------------------------------------------
public class UITalentSelectionPanel : MonoBehaviour // TypeDefIndex: 19997
{
	// Fields
	private RectTransform _optionsRoot; // 0x20
	private UITalentSelectionOptionButton _optionPrefab; // 0x28
	private Button _hideButton; // 0x30
	private PlayerInputController _playerInputController; // 0x38
	private bool _startHidden; // 0x40
	private readonly List<UITalentSelectionOptionButton> _buttons; // 0x48
	private int _currentSelectionId; // 0x50
	private bool _hasOffer; // 0x54
	private TalentOfferKind _currentKind; // 0x58
	private Transform _hideButtonImageTrans; // 0x60
	private Transform _hideButtonIconTrans; // 0x68
}

// --------------------------------------------------------------------
// PvpNumericTalentConfigLoader  [struct]
// --------------------------------------------------------------------
public struct PvpNumericTalentConfigLoader.NumericTalentData // TypeDefIndex: 20172
{
	// Fields
	public int PctOffset; // 0x0
	public int Offset; // 0x4
	public int Scale; // 0x8
	public string Icon; // 0x10
}

// --------------------------------------------------------------------
// PvpNumericTalentConfigLoader  [class]
// --------------------------------------------------------------------
public class PvpNumericTalentConfigLoader // TypeDefIndex: 20173
{
	// Fields
	public static Dictionary<string, PvpNumericTalentConfigLoader.NumericTalentData> TalentConfigs; // 0x0
}

// --------------------------------------------------------------------
// PvpTalentConfigLoader  [class]
// --------------------------------------------------------------------
public class PvpTalentConfigLoader // TypeDefIndex: 20176
{
	// Fields
	public static Dictionary<string, Dictionary<string, object>> TalentConfigs; // 0x0
}

// --------------------------------------------------------------------
// TalentBuffStateChanged  [struct]
// --------------------------------------------------------------------
public struct TalentBuffStateChanged : IMessage // TypeDefIndex: 20265
{
	// Fields
	public long EntityId; // 0x0
	public PvpTalentId TalentId; // 0x8
	public long BuffInstanceId; // 0x10
	public bool add; // 0x18
}

// --------------------------------------------------------------------
// TalentRarity  [enum]
// --------------------------------------------------------------------
public enum TalentRarity // TypeDefIndex: 20278
{
	// Fields
	public int value__; // 0x0
	public const TalentRarity Rare = 0;
	public const TalentRarity Epic = 1;
	public const TalentRarity Legendary = 2;
}

// --------------------------------------------------------------------
// TalentOfferKind  [enum]
// --------------------------------------------------------------------
public enum TalentOfferKind // TypeDefIndex: 20279
{
	// Fields
	public int value__; // 0x0
	public const TalentOfferKind Numeric = 0;
	public const TalentOfferKind Functional = 1;
	public const TalentOfferKind RandomAttribute = 2;
	public const TalentOfferKind Mixed = 3;
}

// --------------------------------------------------------------------
// PvpTalentId  [enum]
// --------------------------------------------------------------------
public enum PvpTalentId // TypeDefIndex: 20280
{
	// Fields
	public int value__; // 0x0
	public const PvpTalentId None = 0;
	public const PvpTalentId AttackSpeed = 100;
	public const PvpTalentId MaxHp = 101;
	public const PvpTalentId MoveSpeed = 102;
	public const PvpTalentId BulletSize = 103;
	public const PvpTalentId BulletSpeed = 104;
	public const PvpTalentId RoleDamage = 105;
	public const PvpTalentId WeaponDamage = 106;
	public const PvpTalentId SkillCooldown = 107;
	public const PvpTalentId EnergyRegen = 108;
	public const PvpTalentId KillHeal = 1000;
	public const PvpTalentId KillAddEnergy = 1001;
	public const PvpTalentId IncMaxEnergy = 1002;
	public const PvpTalentId InjurySpike = 1003;
	public const PvpTalentId HitSpeedUp = 1004;
	public const PvpTalentId BareHand = 1005;
	public const PvpTalentId ZulanPower = 1006;
	public const PvpTalentId CrystalSpike = 1007;
	public const PvpTalentId BloodLust = 1008;
	public const PvpTalentId GiantKiller = 1009;
	public const PvpTalentId KillGrow = 1010;
	public const PvpTalentId FullThrottle = 1011;
	public const PvpTalentId SoulStrike = 1012;
	public const PvpTalentId FrostNova = 1013;
	public const PvpTalentId SkillStartInvincible = 2000;
	public const PvpTalentId KnightReduceSkillCooldown = 2001;
	public const PvpTalentId KnightExplosionBullet = 2002;
	public const PvpTalentId KnightTrippleFire = 2003;
	public const PvpTalentId RangerAddSkillTimes = 2004;
	public const PvpTalentId RangerRegenHpOnSkill = 2005;
	public const PvpTalentId RangerAttackOnSkill = 2006;
	public const PvpTalentId EngineerLaserTurret = 2007;
	public const PvpTalentId EngineerHeavyTurret = 2008;
	public const PvpTalentId EngineerBigTurret = 2009;
	public const PvpTalentId WarliegeStun = 2010;
	public const PvpTalentId WarliegeEmergencyHealing = 2011;
	public const PvpTalentId WarliegeTough = 2012;
	public const PvpTalentId SwordHeart = 2013;
	public const PvpTalentId SwordQiSlash = 2014;
	public const PvpTalentId GiantSword = 2015;
	public const PvpTalentId LifeSiphon = 2016;
	public const PvpTalentId BatWaves = 2017;
	public const PvpTalentId ImmortalBody = 2018;
	public const PvpTalentId ShadowAttack = 2019;
	public const PvpTalentId HideInShadow = 2020;
	public const PvpTalentId PriestHolyBlessing = 2021;
	public const PvpTalentId PriestHolyMarch = 2022;
	public const PvpTalentId PriestMoonShadowBreath = 2023;
	public const PvpTalentId AlchemistSluggishPoisonZone = 2024;
	public const PvpTalentId AlchemistWeakeningPoison = 2025;
	public const PvpTalentId AlchemistMasterPoison = 2026;
	public const PvpTalentId Phantom = 2027;
}

// --------------------------------------------------------------------
// TalentOfferOption  [struct]
// --------------------------------------------------------------------
public struct TalentOfferOption // TypeDefIndex: 20281
{
	// Fields
	public TalentRarity Rarity; // 0x0
	public PvpTalentId TalentId; // 0x4
	public bool IsCharacterSpecific; // 0x8
}

// --------------------------------------------------------------------
// TalentOfferChanged  [struct]
// --------------------------------------------------------------------
public struct TalentOfferChanged : IMessage // TypeDefIndex: 20282
{
	// Fields
	public long EntityId; // 0x0
	public int SelectionId; // 0x8
	public TalentOfferKind Kind; // 0xC
	public bool AllLegendary; // 0x10
	public List<TalentOfferOption> Options; // 0x18
}

// --------------------------------------------------------------------
// TalentOfferCleared  [struct]
// --------------------------------------------------------------------
public struct TalentOfferCleared : IMessage // TypeDefIndex: 20283
{
	// Fields
	public long EntityId; // 0x0
	public int SelectionId; // 0x8
}

// --------------------------------------------------------------------
// TalentChosen  [struct]
// --------------------------------------------------------------------
public struct TalentChosen : IMessage // TypeDefIndex: 20284
{
	// Fields
	public long EntityId; // 0x0
	public int SelectionId; // 0x8
	public int ChoiceIndex; // 0xC
	public TalentOfferOption Option; // 0x10
}

// --------------------------------------------------------------------
// TalentOwnedTalentsChanged  [struct]
// --------------------------------------------------------------------
public struct TalentOwnedTalentsChanged : IMessage // TypeDefIndex: 20285
{
	// Fields
	public long EntityId; // 0x0
	public List<TalentOfferOption> OwnedTalents; // 0x8
}

// --------------------------------------------------------------------
// TalentStackChanged  [struct]
// --------------------------------------------------------------------
public struct TalentStackChanged : IMessage // TypeDefIndex: 20302
{
	// Fields
	public long EntityId; // 0x0
	public PvpTalentId TalentId; // 0x8
	public int StackCount; // 0xC
}

// --------------------------------------------------------------------
// PvpTalentKind  [enum]
// --------------------------------------------------------------------
public enum PvpTalentKind // TypeDefIndex: 20352
{
	// Fields
	public int value__; // 0x0
	public const PvpTalentKind Numeric = 0;
	public const PvpTalentKind Functional = 1;
}

// --------------------------------------------------------------------
// TalentBuffAttachment  [class]
// --------------------------------------------------------------------
public class TalentBuffAttachment : IBuffAttachment // TypeDefIndex: 20353
{
	// Fields
	private readonly PvpTalentId _id; // 0x10
}

// --------------------------------------------------------------------
// PvpTalentDefinition  [class]
// --------------------------------------------------------------------
public sealed class PvpTalentDefinition // TypeDefIndex: 20354
{
	// Fields
	public readonly PvpTalentId Id; // 0x10
	public readonly PvpTalentKind Kind; // 0x14
	public readonly bool IsCharacterSpecific; // 0x18
	public readonly Func<TalentRarity, List<Buff>> CreateBuffs; // 0x20
}

// --------------------------------------------------------------------
// PvpTalentCatalog  [class]
// --------------------------------------------------------------------
private sealed class PvpTalentCatalog.<>c // TypeDefIndex: 20355
{
	// Fields
	public static readonly PvpTalentCatalog.<>c <>9; // 0x0
}

// --------------------------------------------------------------------
// PvpTalentCatalog  [class]
// --------------------------------------------------------------------
public static class PvpTalentCatalog // TypeDefIndex: 20356
{
	// Fields
	private static readonly Dictionary<PvpTalentId, PvpTalentDefinition> Dict; // 0x0
}

// --------------------------------------------------------------------
// PvpDamageType  [enum]
// --------------------------------------------------------------------
public enum PvpDamageType // TypeDefIndex: 20454
{
	// Fields
	public int value__; // 0x0
	public const PvpDamageType None = 0;
	public const PvpDamageType InitWeapon = 1;
	public const PvpDamageType Weapon = 2;
	public const PvpDamageType Skill = 4;
	public const PvpDamageType ExtraEffect = 8;
	public const PvpDamageType Projectile = 16;
	public const PvpDamageType Melee = 32;
	public const PvpDamageType Fire = 64;
	public const PvpDamageType Ice = 128;
	public const PvpDamageType Electric = 256;
	public const PvpDamageType Poison = 512;
}

// --------------------------------------------------------------------
// TalentComponent  [class]
// --------------------------------------------------------------------
private sealed class TalentComponent.Snapshot : IComponentSnapshotData // TypeDefIndex: 20591
{
	// Fields
	public int PendingSelections; // 0x10
	public int SelectionId; // 0x14
	public bool AwaitingPick; // 0x18
	public TalentOfferKind CurrentKind; // 0x1C
	public bool CurrentAllLegendary; // 0x20
	public List<TalentOfferOption> CurrentOptions; // 0x28
	public int FunctionalSlotsUsed; // 0x30
	public int AttributeLegendaryPity; // 0x34
	public int FunctionalCharSpecificPity; // 0x38
	public List<TalentOfferOption> OwnedTalents; // 0x40
	public ulong RngSeed; // 0x48
}

// --------------------------------------------------------------------
// TalentComponent  [class]
// --------------------------------------------------------------------
private sealed class TalentComponent.FunctionalOfferContext // TypeDefIndex: 20592
{
	// Fields
	public List<PvpTalentId> UniversalPool; // 0x10
	public List<PvpTalentId> CharacterPool; // 0x18
}

// --------------------------------------------------------------------
// TalentComponent  [class]
// --------------------------------------------------------------------
private sealed class TalentComponent.<>c__DisplayClass31_0 // TypeDefIndex: 20593
{
	// Fields
	public PvpTalentId talentId; // 0x10
}

// --------------------------------------------------------------------
// TalentComponent  [class]
// --------------------------------------------------------------------
public class TalentComponent : EntityComponent, IComponentSnapshottable // TypeDefIndex: 20594
{
	// Fields
	private FixedRandNumGen _rng; // 0x18
	private int _pendingSelections; // 0x20
	private int _selectionId; // 0x24
	private bool _awaitingPick; // 0x28
	private TalentOfferKind _currentKind; // 0x2C
	private bool _currentAllLegendary; // 0x30
	private readonly List<TalentOfferOption> _currentOptions; // 0x38
	private int _functionalSlotsUsed; // 0x40
	private int _attributeLegendaryPity; // 0x44
	private int _functionalCharSpecificPity; // 0x48
	private PlayerComponent _player; // 0x50
	private BuffComponent _buffs; // 0x58
	private readonly List<TalentOfferOption> _ownedTalents; // 0x60
	public List<TalentOfferOption> OverrideOptions; // 0x68
}

// --------------------------------------------------------------------
// AlchemistWeakeningPoisonTalentBuff  [class]
// --------------------------------------------------------------------
public class AlchemistWeakeningPoisonTalentBuff : Buff // TypeDefIndex: 20630
{
	// Fields
	private readonly FixedPoint _damageReducePct; // 0x68
	private readonly FixedPoint _poisonDamageReducePct; // 0x70
	private MessageSystem.EventHandle<BuffStateChanged> _buffStateChangedHandler; // 0x78
}

// --------------------------------------------------------------------
// BareHandTalentBuff  [class]
// --------------------------------------------------------------------
public class BareHandTalentBuff : Buff // TypeDefIndex: 20633
{
	// Fields
	private int _stackCount; // 0x68
	private int _damage; // 0x6C
	private FixedPoint _cooldown; // 0x70
	private ComponentGetter<PlayerComponent> _playerComponent; // 0x78
}

// --------------------------------------------------------------------
// BatWavesTalentBuff  [class]
// --------------------------------------------------------------------
public class BatWavesTalentBuff : Buff // TypeDefIndex: 20634
{
	// Fields
	private readonly int _batCountAdd; // 0x68
	private readonly FixedPoint _batSpeedScale; // 0x70
	private readonly FixedPoint _rangeScale; // 0x78
	private ComponentGetter<SkillComponent> _skillComponent; // 0x80
	private Skill7 _skill; // 0x90
}

// --------------------------------------------------------------------
// BloodLustTalentBuff  [class]
// --------------------------------------------------------------------
public class BloodLustTalentBuff : Buff // TypeDefIndex: 20635
{
	// Fields
	private readonly FixedPoint _regenEfficiencyScale; // 0x68
	private readonly FixedPoint _lifeStealPct; // 0x70
	private ComponentGetter<UnitStateComponent> _unitState; // 0x78
}

// --------------------------------------------------------------------
// BuffStackLogic  [enum]
// --------------------------------------------------------------------
public enum BuffStackLogic // TypeDefIndex: 20640
{
	// Fields
	public int value__; // 0x0
	public const BuffStackLogic discardNew = 0;
	public const BuffStackLogic discardOld = 1;
	public const BuffStackLogic keep = 2;
}

// --------------------------------------------------------------------
// CrystalSpikeTalentBuff  [class]
// --------------------------------------------------------------------
public class CrystalSpikeTalentBuff : Buff // TypeDefIndex: 20644
{
	// Fields
	private readonly int _spikeCount; // 0x68
	private readonly FixedPoint _spikeScale; // 0x70
	private readonly FixedPoint _scatterRadius; // 0x78
	private readonly FixedPoint _distancePerPatch; // 0x80
	private readonly FixedPoint _spikeLifetime; // 0x88
	private readonly int _damage; // 0x90
	private readonly FixedPoint _damageInterval; // 0x98
	private FixedPoint _travelledDistance; // 0xA0
	private FixedPointVector2 _lastPosition; // 0xA8
	private bool _hasLastPosition; // 0xB8
	private ulong _rngSeed; // 0xC0
	private ComponentGetter<UnitStateComponent> _unitState; // 0xC8
}

// --------------------------------------------------------------------
// FrostNovaTalentBuff  [class]
// --------------------------------------------------------------------
public class FrostNovaTalentBuff : Buff // TypeDefIndex: 20651
{
	// Fields
	private readonly FixedPoint _interval; // 0x68
	private readonly FixedPoint _radius; // 0x70
	private readonly FrostNovaColdDebuff _coldDebuffPrefab; // 0x78
	private readonly FrostNovaFrozenDebuff _frozenDebuffPrefab; // 0x80
	private readonly List<FixedPointCollider> _overlapResults; // 0x88
	private readonly HashSet<long> _processedTargets; // 0x90
	private ComponentGetter<UnitStateComponent> _ownerUnitState; // 0x98
	private FixedPoint _elapsed; // 0xA8
}

// --------------------------------------------------------------------
// FullThrottleTalentBuff  [class]
// --------------------------------------------------------------------
public class FullThrottleTalentBuff : Buff // TypeDefIndex: 20652
{
	// Fields
	private readonly FixedPoint _attackSpeedToEnergyRegenPct; // 0x68
	private ComponentGetter<UnitStateComponent> _unitStateComponent; // 0x70
	private FixedPoint _appliedEnergyRegenPct; // 0x80
}

// --------------------------------------------------------------------
// GiantKillerTalentBuff  [class]
// --------------------------------------------------------------------
public class GiantKillerTalentBuff : Buff // TypeDefIndex: 20654
{
	// Fields
	private readonly FixedPoint _bonusPctPer100; // 0x68
	private bool _applied; // 0x70
	private ComponentGetter<UnitStateComponent> _unitState; // 0x78
}

// --------------------------------------------------------------------
// GiantSwordTalentBuff  [class]
// --------------------------------------------------------------------
public class GiantSwordTalentBuff : Buff // TypeDefIndex: 20655
{
	// Fields
	private const string GiantSwordVisualPrefab = "SwordMasterGiantSwordTalent";
	private static readonly FixedPoint GiantSwordSpawnDistance; // 0x0
	private readonly int _damage; // 0x68
	private readonly FixedPoint _radius; // 0x70
	private readonly FixedPoint _stunDuration; // 0x78
	private readonly FixedPoint _strikeDelay; // 0x80
	private ComponentGetter<UnitStateComponent> _unitStateComponent; // 0x88
	private ComponentGetter<SkillComponent> _skillComponent; // 0x98
	private ComponentGetter<MoveComponent> _moveComponent; // 0xA8
	private MessageSystem.EventHandle<SkillStateChanged> _skillStateChangedHandler; // 0xB8
}

// --------------------------------------------------------------------
// HideInShadowTalentBuff  [class]
// --------------------------------------------------------------------
public class HideInShadowTalentBuff : Buff // TypeDefIndex: 20658
{
	// Fields
	private readonly FixedPoint _healPercentPerSecond; // 0x68
	private readonly FixedPoint _tickInterval; // 0x70
	private FixedPoint _tickTimer; // 0x78
	private bool _isInvisible; // 0x80
	private ComponentGetter<UnitStateComponent> _unitState; // 0x88
	private MessageSystem.EventHandle<UnitStateInvisibleChanged> _invisibleHandler; // 0x98
}

// --------------------------------------------------------------------
// HitSpeedUpTalentBuff  [class]
// --------------------------------------------------------------------
public class HitSpeedUpTalentBuff : Buff // TypeDefIndex: 20659
{
	// Fields
	private ComponentGetter<BuffComponent> _buffComp; // 0x68
	private Hastebuff _hastebuffPrefab; // 0x78
	private FixedPoint _hastebuffSpeedPctOffset; // 0x80
	private MessageSystem.EventHandle<DamageResolved> _damageResolvedHandler; // 0x88
}

// --------------------------------------------------------------------
// ImmortalBodyTalentBuff  [class]
// --------------------------------------------------------------------
public class ImmortalBodyTalentBuff : Buff // TypeDefIndex: 20661
{
	// Fields
	private readonly FixedPoint _hpThreshold; // 0x68
	private readonly ImmortalBodyStateBuff _stateBuffPrefab; // 0x70
	private ComponentGetter<BuffComponent> _buffComponent; // 0x78
	private ComponentGetter<UnitStateComponent> _unitState; // 0x88
	private MessageSystem.EventHandle<UnitStateHPChanged> _hpChangedHandler; // 0x98
	private bool _isStateActive; // 0xA0
	private long _stateBuffInstanceId; // 0xA8
}

// --------------------------------------------------------------------
// IncMaxEnergyTalentBuff  [class]
// --------------------------------------------------------------------
public class IncMaxEnergyTalentBuff : Buff // TypeDefIndex: 20662
{
	// Fields
	private int _maxEnergyInc; // 0x68
	private FixedPoint _energyRcvSpeedScale; // 0x70
	private ComponentGetter<UnitStateComponent> _unitStateComponent; // 0x78
	private ComponentGetter<WeaponComponent> _weaponComponent; // 0x88
}

// --------------------------------------------------------------------
// InjurySpikeTalentBuff  [class]
// --------------------------------------------------------------------
public class InjurySpikeTalentBuff : Buff // TypeDefIndex: 20663
{
	// Fields
	private int _spikeCount; // 0x68
	private int _damage; // 0x6C
	private int _extraHpStep; // 0x70
	private int _damagePerExtraHpStep; // 0x74
	private FixedPoint _coolDown; // 0x78
	private FixedPoint spikeTimestamp; // 0x80
	private ComponentGetter<UnitStateComponent> _unitState; // 0x88
	private ComponentGetter<WeaponComponent> _weaponComp; // 0x98
	private MessageSystem.EventHandle<DamageResolved> _damageResolvedHandler; // 0xA8
}

// --------------------------------------------------------------------
// KillAddEnergyTalentBuff  [class]
// --------------------------------------------------------------------
public class KillAddEnergyTalentBuff : Buff // TypeDefIndex: 20665
{
	// Fields
	private int _energyRcvOnKill; // 0x68
	private ComponentGetter<UnitStateComponent> _unitState; // 0x70
	private ComponentGetter<WeaponComponent> _weaponComp; // 0x80
	private MessageSystem.EventHandle<DamageResolved> _damageResolvedHandler; // 0x90
}

// --------------------------------------------------------------------
// KillGrowTalentBuff  [class]
// --------------------------------------------------------------------
public class KillGrowTalentBuff : Buff // TypeDefIndex: 20666
{
	// Fields
	private readonly FixedPoint _scalePerStack; // 0x68
	private readonly int _maxHpPerStack; // 0x70
	private readonly int _loseStackOnDeath; // 0x74
	private int _currentStacks; // 0x78
	private int _appliedStacks; // 0x7C
	private ComponentGetter<UnitStateComponent> _unitState; // 0x80
	private ComponentGetter<UnitScaleComponent> _unitScale; // 0x90
	private MessageSystem.EventHandle<DamageResolved> _damageResolvedHandler; // 0xA0
	private MessageSystem.EventHandle<GameUnitDie> _unitDieHandler; // 0xA8
}

// --------------------------------------------------------------------
// KillHealTalentBuff  [class]
// --------------------------------------------------------------------
public class KillHealTalentBuff : Buff // TypeDefIndex: 20667
{
	// Fields
	private int _healOnKill; // 0x68
	private ComponentGetter<UnitStateComponent> _unitState; // 0x70
	private MessageSystem.EventHandle<DamageResolved> _damageResolvedHandler; // 0x80
}

// --------------------------------------------------------------------
// LifeSiphonTalentBuff  [class]
// --------------------------------------------------------------------
public class LifeSiphonTalentBuff : Buff // TypeDefIndex: 20669
{
	// Fields
	private readonly FixedPoint _healPct; // 0x68
	private ComponentGetter<UnitStateComponent> _unitState; // 0x70
	private MessageSystem.EventHandle<DamageResolved> _damageResolvedHandler; // 0x80
}

// --------------------------------------------------------------------
// PhantomTalentBuff  [class]
// --------------------------------------------------------------------
public class PhantomTalentBuff : Buff // TypeDefIndex: 20670
{
	// Fields
	private readonly FixedPoint _chargeTimeIncreasePct; // 0x68
	private readonly FixedPoint _duration; // 0x70
	private readonly int _maxPhantomCount; // 0x78
	private readonly int _spawnCount; // 0x7C
	private readonly FixedPoint _spawnRadius; // 0x80
	private readonly int _phantomHp; // 0x88
	private readonly FixedPoint _moveSpeedScale; // 0x90
	private readonly FixedPoint _phantomRoleDamagePctOffset; // 0x98
	private readonly FixedPoint _followDistance; // 0xA0
	private readonly FixedPoint _maxFollowDistance; // 0xA8
	private readonly FixedPoint _seekDistance; // 0xB0
	private readonly FixedPoint _attackDistance; // 0xB8
	private readonly FixedPoint _attackCooldown; // 0xC0
	private readonly FixedPoint _forwardProbability; // 0xC8
	private readonly FixedPoint _retreatProbability; // 0xD0
	private readonly FixedPoint _closeForwardProbability; // 0xD8
	private readonly FixedPoint _closeRetreatProbability; // 0xE0
	private ComponentGetter<UnitStateComponent> _unitStateComponent; // 0xE8
	private ComponentGetter<WeaponComponent> _weaponComponent; // 0xF8
	private MessageSystem.EventHandle<WeaponChargeProcessChanged> _weaponChargeHandler; // 0x108
	private long _chargingWeaponInstanceId; // 0x110
	private bool _spawnedThisCharge; // 0x118
	private ValueList<long> _phantomEntityIds; // 0x120
}

// --------------------------------------------------------------------
// PriestMoonShadowBreathTalentBuff  [class]
// --------------------------------------------------------------------
public class PriestMoonShadowBreathTalentBuff : Buff // TypeDefIndex: 20673
{
	// Fields
	private readonly int _healThreshold; // 0x68
	private readonly int _spawnCount; // 0x6C
	private readonly int _damage; // 0x70
	private readonly FixedPoint _orbitRadius; // 0x78
	private readonly FixedPoint _orbitAngularSpeed; // 0x80
	private readonly FixedPoint _acquireRange; // 0x88
	private readonly FixedPoint _moveSpeed; // 0x90
	private readonly FixedPoint _turnRateDeg; // 0x98
	private readonly FixedPoint _hitRadius; // 0xA0
	private readonly int _initialBiasMaxDeg; // 0xA8
	private readonly FixedPoint _searchInterval; // 0xB0
	private MessageSystem.EventHandle<HealApplied> _healAppliedHandler; // 0xB8
	private int _accumulatedHeal; // 0xC0
}

// --------------------------------------------------------------------
// ShadowAttackTalentBuff  [class]
// --------------------------------------------------------------------
public class ShadowAttackTalentBuff : Buff // TypeDefIndex: 20679
{
	// Fields
	private readonly ShadowAttackStateBuff _stateBuffPrefab; // 0x68
	private ComponentGetter<BuffComponent> _buffComponent; // 0x70
	private ComponentGetter<UnitStateComponent> _unitState; // 0x80
	private MessageSystem.EventHandle<UnitStateInvisibleChanged> _invisibleHandler; // 0x90
	private bool _wasInvisible; // 0x98
}

// --------------------------------------------------------------------
// SkillStartInvincibleTalentBuff  [class]
// --------------------------------------------------------------------
public class SkillStartInvincibleTalentBuff : Buff // TypeDefIndex: 20681
{
	// Fields
	private FixedPoint _duration; // 0x68
	private ComponentGetter<BuffComponent> _buffComponent; // 0x70
	private MessageSystem.EventHandle<SkillStateChanged> _skillStateChangedHandler; // 0x80
}

// --------------------------------------------------------------------
// SoulStrikeTalentBuff  [class]
// --------------------------------------------------------------------
public class SoulStrikeTalentBuff : Buff // TypeDefIndex: 20683
{
	// Fields
	private readonly FixedPoint _dropRange; // 0x68
	private readonly int _dropSoulCount; // 0x70
	private readonly int _extraSoulDropOnPlayerKill; // 0x74
	private readonly int _fullSoul; // 0x78
	private readonly int _baseDamage; // 0x7C
	private readonly int _damageGrowthPerCast; // 0x80
	private readonly FixedPoint _hitRadius; // 0x88
	private int _currentSoul; // 0x90
	private int _releasedCount; // 0x94
	private ComponentGetter<UnitStateComponent> _ownerUnitState; // 0x98
	private MessageSystem.EventHandle<DamageResolved> _damageResolvedHandler; // 0xA8
	private MessageSystem.EventHandle<SoulOrbCollected> _soulCollectedHandler; // 0xB0
}

// --------------------------------------------------------------------
// SwordHeartTalentBuff  [class]
// --------------------------------------------------------------------
public class SwordHeartTalentBuff : Buff // TypeDefIndex: 20688
{
	// Fields
	private readonly FixedPoint _duration; // 0x68
	private readonly FixedPoint _moveSpeedPct; // 0x70
	private readonly FixedPoint _energyRegenPct; // 0x78
	private ComponentGetter<UnitStateComponent> _unitState; // 0x80
	private MessageSystem.EventHandle<DamageResolved> _damageResolvedHandler; // 0x90
	private MessageSystem.EventHandle<PlayerKillStreakChanged> _playerKillHandler; // 0x98
	private int _lastTriggerFrame; // 0xA0
	private long _lastTriggerTargetId; // 0xA8
}

// --------------------------------------------------------------------
// SwordQiSlashTalentBuff  [class]
// --------------------------------------------------------------------
public class SwordQiSlashTalentBuff : Buff // TypeDefIndex: 20689
{
	// Fields
	private readonly int _damage; // 0x68
	private readonly FixedPoint _range; // 0x70
	private readonly FixedPoint _bulletSpeed; // 0x78
	private readonly FixedPoint _bulletSize; // 0x80
	private readonly FixedPoint _critRate; // 0x88
	private readonly FixedPoint _critDamage; // 0x90
	private ComponentGetter<UnitStateComponent> _unitStateComponent; // 0x98
	private ComponentGetter<MoveComponent> _moveComponent; // 0xA8
	private MessageSystem.EventHandle<WeaponPreFire> _weaponPreFireHandler; // 0xB8
	private MessageSystem.EventHandle<SkillStateChanged> _skillStateChangedHandler; // 0xC0
	private long _trackedWeaponInstanceId; // 0xC8
	private int _attackSequenceLength; // 0xD0
	private int _attackSequenceCounter; // 0xD4
}

// --------------------------------------------------------------------
// TalentStatModifierBuff  [class]
// --------------------------------------------------------------------
public class TalentStatModifierBuff : Buff // TypeDefIndex: 20690
{
	// Fields
	private ComponentGetter<UnitStateComponent> _unitState; // 0x68
	private ComponentGetter<SkillComponent> _skill; // 0x78
	private FixedPoint _pctOffset; // 0x88
	private FixedPoint _offset; // 0x90
	private FixedPoint _scale; // 0x98
	private PvpTalentId _talentId; // 0xA0
	private object[] _vals; // 0xA8
}

// --------------------------------------------------------------------
// ZulanPowerTalentBuff  [class]
// --------------------------------------------------------------------
public class ZulanPowerTalentBuff : Buff // TypeDefIndex: 20691
{
	// Fields
	private readonly int _cannonCount; // 0x68
	private readonly FixedPoint _followRadius; // 0x70
	private readonly FixedPoint _followSpeed; // 0x78
	private readonly int _laserDamage; // 0x80
	private readonly FixedPoint _laserDistance; // 0x88
	private readonly FixedPoint _laserHitRadius; // 0x90
	private ValueList<long> _cannonIds; // 0x98
	private ValueList<FixedPointVector2> _cannonOffsets; // 0xA8
	private MessageSystem.EventHandle<WeaponAttackActionStart> _attackActionHandler; // 0xB8
	private MessageSystem.EventHandle<FaceDirectionChanged> _faceDirectionHandler; // 0xC0
	private MessageSystem.EventHandle<GameUnitRevive> _reviveHandler; // 0xC8
}

// --------------------------------------------------------------------
// AutoPickTalentAction  [class]
// --------------------------------------------------------------------
public class AutoPickTalentAction : BehaviorTreeNode<AIContext> // TypeDefIndex: 20707
{
	// Fields
	private const string TalentPickSentIdKey = "TalentPickSentId";
}

// --------------------------------------------------------------------
// MasterTalentHeroPoolRuleType  [enum]
// --------------------------------------------------------------------
public enum MasterTalentHeroPoolRuleType // TypeDefIndex: 21312
{
	// Fields
	public int value__; // 0x0
	public const MasterTalentHeroPoolRuleType None = 0;
	public const MasterTalentHeroPoolRuleType Blacklist = 1;
	public const MasterTalentHeroPoolRuleType Whitelist = 2;
}

// --------------------------------------------------------------------
// MasterTalentPoolRuntimeConfig  [class]
// --------------------------------------------------------------------
public sealed class MasterTalentPoolRuntimeConfig // TypeDefIndex: 21321
{
	// Fields
	public int Id; // 0x0
	public emBuff Buff; // 0x0
	public string BuffName; // 0x0
	public int Weight; // 0x0
	public bool Enabled; // 0x0
}

// --------------------------------------------------------------------
// MasterTalentHeroPoolOverrideRuntimeConfig  [class]
// --------------------------------------------------------------------
public sealed class MasterTalentHeroPoolOverrideRuntimeConfig // TypeDefIndex: 21322
{
	// Fields
	public int Id; // 0x0
	public emHero Hero; // 0x0
	public emBuff Buff; // 0x0
	public string BuffName; // 0x0
	public MasterTalentHeroPoolRuleType RuleType; // 0x0
	public int Weight; // 0x0
	public bool Enabled; // 0x0
}

// --------------------------------------------------------------------
// MasterTalentDropRuntimeConfig  [class]
// --------------------------------------------------------------------
public sealed class MasterTalentDropRuntimeConfig // TypeDefIndex: 21323
{
	// Fields
	public int Id; // 0x0
	public int GameMode; // 0x0
	public int DifficultyGroup; // 0x0
	public int BigLevel; // 0x0
	public int SmallLevel; // 0x0
	public int RatePermyriad; // 0x0
	public int DropCount; // 0x0
	public bool Enabled; // 0x0
}

// --------------------------------------------------------------------
// MasterTalentCandidateData  [class]
// --------------------------------------------------------------------
public sealed class MasterTalentCandidateData // TypeDefIndex: 21324
{
	// Fields
	public emBuff Buff; // 0x0
	public string BuffName; // 0x0
	public int Weight; // 0x0
}

// --------------------------------------------------------------------
// NewTalentConfigLoader  [class]
// --------------------------------------------------------------------
public static class NewTalentConfigLoader // TypeDefIndex: 21328
{
	// Fields
	private const string TalentRefreshLevelIndexesKey = "talent_refresh_level_indexes";
	private const string BraveTrialTalentRefreshLevelIndexesKey = "EX_talent_refresh_level_indexes";
	private const string BraveTrialFirstLevelExtraChoicesKey = "extra_buff_levelindex_0";
	private const string ExtraTalentCountKey = "EX_extra_buff_count";
	private const string RefreshCountKey = "refresh_count";
	private const string CommonBaseWeightKey = "common_pool_base_weight";
	private const string BuildBaseWeightKey = "build_pool_base_weight";
	private const string CommonOptionCountKey = "common_pool_option_count";
	private const string CommonGuaranteeCountKey = "common_pool_guarantee_count";
	private const string EarlyCoefficientKey = "common_pool_previous_talent_probability_bonus";
	private const string RecentCoefficientKey = "recent_duplicate_appearance_coefficient";
	private const string CorrelationCoefficientKey = "correlation_coefficient_k";
	private const string ProgressCoefficientKey = "build_progress_weight_coefficient";
	private const string BuildGuaranteeCountKey = "build_pool_guarantee_count";
}

// --------------------------------------------------------------------
// TalentChoiceResultData  [class]
// --------------------------------------------------------------------
public class TalentChoiceResultData // TypeDefIndex: 21329
{
	// Fields
	public string battleSaveId; // 0x0
	public string choiceId; // 0x0
	public int choiceIndex; // 0x0
	public List<int> initialSlots; // 0x0
	public List<int> finalSlots; // 0x0
	public int selectedTalentId; // 0x0
	public int selectedSlotId; // 0x0
	public List<int> currentTalentIds; // 0x0
	public string levelIndex; // 0x0
	public string characterId; // 0x0
	public string weaponId; // 0x0
	public bool completed; // 0x0
}

// --------------------------------------------------------------------
// NewTalentData  [class]
// --------------------------------------------------------------------
public class NewTalentData // TypeDefIndex: 21330
{
	// Fields
	public bool isBraveTrial; // 0x0
	public int slotCapacity; // 0x0
	public List<int> ownedTalentIds; // 0x0
	public List<int> extraTalentList; // 0x0
	public int selectionCredits; // 0x0
	public int selectedSkillId; // 0x0
	public int randomSeed; // 0x0
	public int selectionCount; // 0x0
	public int commonPoolMissCount; // 0x0
	public int buildPoolMissCount; // 0x0
	public List<int> previousSelectionTalentIds; // 0x0
	public List<int> twoRoundsAgoTalentIds; // 0x0
	public int lastSelectionLevelIndex; // 0x0
	public Dictionary<int, bool> oneShotTalentStates; // 0x0
	public List<TalentChoiceResultData> talentChoiceResults; // 0x0
}

// --------------------------------------------------------------------
// TalentDevilFire  [class]
// --------------------------------------------------------------------
public class TalentDevilFire // TypeDefIndex: 21644
{
	// Fields
	private const string PrefabPath = "Activities/Fire/Prefabs/buff_devil_fire.prefab";
}

// --------------------------------------------------------------------
// TalentRewardDataChangeType  [enum]
// --------------------------------------------------------------------
public enum TalentRewardDataChangeType // TypeDefIndex: 21773
{
	// Fields
	public int value__; // 0x0
	public const TalentRewardDataChangeType State = 0;
	public const TalentRewardDataChangeType Structure = 1;
}

// --------------------------------------------------------------------
// TalentRewardItem  [class]
// --------------------------------------------------------------------
public sealed class TalentRewardItem // TypeDefIndex: 21774
{
	// Fields
	private int <RewardId>k__BackingField; // 0x10
	private int <TrophyLevel>k__BackingField; // 0x14
	private int <RequiredExp>k__BackingField; // 0x18
	private string <Key>k__BackingField; // 0x20
	private int <Count>k__BackingField; // 0x28
	private int <LimitTime>k__BackingField; // 0x2C
	private int <RefreshExp>k__BackingField; // 0x30
	private int <ClaimedCount>k__BackingField; // 0x34
	private int <ClaimableCount>k__BackingField; // 0x38
	private bool <HasClaimed>k__BackingField; // 0x3C
}

// --------------------------------------------------------------------
// ActivityTalentData  [class]
// --------------------------------------------------------------------
public class ActivityTalentData : BaseData // TypeDefIndex: 21775
{
	// Fields
	private const string RewardSource = "activity_talent";
	private const string CacheLastSelectDiff = "talent_last_select_diff";
	private const string CacheDifficultIntroShown = "talent_difficult_intro_shown";
	private const int UnlimitedRewardClaimLimit = -1;
	public const int MaxDifficultyLevel = 1;
	private readonly List<TalentRewardItem> _rewardConfigs; // 0x10
	private ActivityTalentTaskManager <TaskManager>k__BackingField; // 0x18
	private Action<TalentRewardDataChangeType, int> RewardDataChanged; // 0x20
}

// --------------------------------------------------------------------
// ActivityTalentEnemyGeneratorHandle  [class]
// --------------------------------------------------------------------
private sealed class ActivityTalentEnemyGeneratorHandle.<>c // TypeDefIndex: 21776
{
	// Fields
	public static readonly ActivityTalentEnemyGeneratorHandle.<>c <>9; // 0x0
	public static Comparison<EnemyConfig> <>9__14_0; // 0x8
}

// --------------------------------------------------------------------
// ActivityTalentEnemyGeneratorHandle  [class]
// --------------------------------------------------------------------
public class ActivityTalentEnemyGeneratorHandle : IEnterExitSceneHandler // TypeDefIndex: 21777
{
	// Fields
	private const int DefaultDifficulty = 1;
	private static bool _enabledByTalent; // 0x0
}

// --------------------------------------------------------------------
// ActivityTalentManager  [class]
// --------------------------------------------------------------------
private sealed class ActivityTalentManager.<>c // TypeDefIndex: 21779
{
	// Fields
	public static readonly ActivityTalentManager.<>c <>9; // 0x0
	public static Action <>9__28_0; // 0x8
	public static Action <>9__29_0; // 0x10
	public static Action <>9__35_0; // 0x18
	public static Action <>9__36_0; // 0x20
}

// --------------------------------------------------------------------
// ActivityTalentManager  [class]
// --------------------------------------------------------------------
public class ActivityTalentManager // TypeDefIndex: 21780
{
	// Fields
	private const int MaxAutoPopCount = 2;
	private const string AutoPopShownCountKey = "activity_talent_auto_pop_shown_count";
	private const string AutoPopShownDateKeyPrefix = "activity_talent_auto_pop_shown_date_";
	private const string AutoPopParticipatedKey = "activity_talent_auto_pop_participated";
	public static string TAG; // 0x0
	public const string NPC_PATH = "Activities/Talent/Prefabs/UI/npc.prefab";
	public static string MatName; // 0x8
	public static string GUIDE_VIEW; // 0x10
	public static string DIFFICULT_INTRO_VIEW; // 0x18
	public static string TASK_VIEW; // 0x20
	public static string POP_VIEW; // 0x28
	public static string WidgetPath; // 0x30
	public static string BtnPath; // 0x38
	private const string StakeEnemyId = "stake";
	public const int DailyDoubleCount = 3;
	private const float HeroicTrialScoreFactor = 1.5;
	private static readonly int[] NormalBigLevelScoreTable; // 0x40
	private static bool _hasSettledGameScore; // 0x48
	public static bool Inited; // 0x49
}

// --------------------------------------------------------------------
// TalentTaskStatus  [enum]
// --------------------------------------------------------------------
public enum TalentTaskStatus // TypeDefIndex: 21781
{
	// Fields
	public int value__; // 0x0
	public const TalentTaskStatus InProgress = 0;
	public const TalentTaskStatus Completed = 1;
	public const TalentTaskStatus Claimed = 2;
}

// --------------------------------------------------------------------
// TalentTaskItem  [class]
// --------------------------------------------------------------------
public sealed class TalentTaskItem // TypeDefIndex: 21782
{
	// Fields
	private int <TaskId>k__BackingField; // 0x10
	private string <DefaultDesc>k__BackingField; // 0x18
	private string <LocalKey>k__BackingField; // 0x20
	private int <Progress>k__BackingField; // 0x28
	private int <TargetProgress>k__BackingField; // 0x2C
	private int <CompletedCount>k__BackingField; // 0x30
	private int <ClaimedCount>k__BackingField; // 0x34
	private int <Score>k__BackingField; // 0x38
	private TalentTaskStatus <Status>k__BackingField; // 0x3C
}

// --------------------------------------------------------------------
// ActivityTalentTaskManager  [class]
// --------------------------------------------------------------------
public sealed class ActivityTalentTaskManager : IDisposable // TypeDefIndex: 21783
{
	// Fields
	private const int DailyLoginTaskId = 1;
	private const int NewTalentTaskId = 2;
	private const int DifferentTalentTaskId = 3;
	private const int HighestDifficultyTaskId = 4;
	private bool _disposed; // 0x10
	private string _taskDayKey; // 0x18
	private Action DataChanged; // 0x20
}

// --------------------------------------------------------------------
// TalentDifficultIntroView  [class]
// --------------------------------------------------------------------
public class TalentDifficultIntroView : BaseUIView // TypeDefIndex: 21784
{
	// Fields
	private const string EnemyKey = "activity/talent/difficulty/heroic_trial/enemy";
	private const string ClearExternalItemsKey = "activity/talent/difficulty/heroic_trial/clear_external_items";
	private const string RandomSkillKey = "activity/talent/difficulty/heroic_trial/random_skill";
	private const string ExtraSlotsKey = "activity/talent/difficulty/heroic_trial/extra_slots";
	private const string RewardBonusKey = "activity/talent/difficulty/heroic_trial/reward_bonus";
}

// --------------------------------------------------------------------
// TalentHandbookRowCellView  [class]
// --------------------------------------------------------------------
private sealed class TalentHandbookRowCellView.<>c__DisplayClass16_0 // TypeDefIndex: 21786
{
	// Fields
	public TalentHandbookRowCellView <>4__this; // 0x10
	public RuntimeLuaTalent talent; // 0x18
}

// --------------------------------------------------------------------
// TalentHandbookRowCellView  [class]
// --------------------------------------------------------------------
public sealed class TalentHandbookRowCellView : EnhancedScrollerCellView // TypeDefIndex: 21787
{
	// Fields
	internal const int SlotCount = 5;
	private const float TalentIconSizeRatio = 0.6;
	private const float DefaultCellHeight = 120;
	private readonly Transform[] _slots; // 0x38
	private IReadOnlyList<RuntimeLuaTalent> _talents; // 0x40
	private int _startIndex; // 0x48
	private TalentHandbookView.TalentCategory _category; // 0x4C
	private Func<RuntimeLuaTalent> _getSelectedTalent; // 0x50
	private Action<RuntimeLuaTalent> _onSelectTalent; // 0x58
	private bool _bound; // 0x60
}

// --------------------------------------------------------------------
// TalentHandbookView  [enum]
// --------------------------------------------------------------------
internal enum TalentHandbookView.TalentCategory // TypeDefIndex: 21788
{
	// Fields
	public int value__; // 0x0
	public const TalentHandbookView.TalentCategory Current = 0;
	public const TalentHandbookView.TalentCategory Total = 1;
	public const TalentHandbookView.TalentCategory Critical = 2;
	public const TalentHandbookView.TalentCategory Classic = 3;
	public const TalentHandbookView.TalentCategory Skill = 4;
	public const TalentHandbookView.TalentCategory Explosion = 5;
	public const TalentHandbookView.TalentCategory HeroPower = 6;
	public const TalentHandbookView.TalentCategory RiskReward = 7;
	public const TalentHandbookView.TalentCategory Movement = 8;
	public const TalentHandbookView.TalentCategory Powerful = 9;
	public const TalentHandbookView.TalentCategory Throwing = 10;
	public const TalentHandbookView.TalentCategory Laser = 11;
	public const TalentHandbookView.TalentCategory Charge = 12;
	public const TalentHandbookView.TalentCategory Melee = 13;
	public const TalentHandbookView.TalentCategory Recovery = 14;
	public const TalentHandbookView.TalentCategory Summon = 15;
	public const TalentHandbookView.TalentCategory Missile = 16;
	public const TalentHandbookView.TalentCategory Element = 17;
	public const TalentHandbookView.TalentCategory Luck = 18;
}

// --------------------------------------------------------------------
// TalentHandbookView  [class]
// --------------------------------------------------------------------
private sealed class TalentHandbookView.TalentTabConfig // TypeDefIndex: 21789
{
	// Fields
	public readonly TalentHandbookView.TalentCategory Category; // 0x10
	public readonly string Key; // 0x18
	public readonly string DefaultText; // 0x20
	public readonly bool InGameOnly; // 0x28
}

// --------------------------------------------------------------------
// TalentHandbookView  [class]
// --------------------------------------------------------------------
private sealed class TalentHandbookView.TalentTabItem // TypeDefIndex: 21790
{
	// Fields
	public TalentHandbookView.TalentTabConfig Config; // 0x10
	public Toggle Toggle; // 0x18
}

// --------------------------------------------------------------------
// TalentHandbookView  [class]
// --------------------------------------------------------------------
private sealed class TalentHandbookView.<>c__DisplayClass31_0 // TypeDefIndex: 21791
{
	// Fields
	public TalentHandbookView <>4__this; // 0x10
	public TalentHandbookView.TalentTabItem tabItem; // 0x18
}

// --------------------------------------------------------------------
// TalentHandbookView  [class]
// --------------------------------------------------------------------
public sealed class TalentHandbookView : BaseUIView, IEnhancedScrollerDelegate // TypeDefIndex: 21792
{
	// Fields
	private static readonly TalentHandbookView.TalentTabConfig[] TabConfigs; // 0x0
	private readonly List<RuntimeLuaTalent> _talentItems; // 0xD8
	private readonly List<TalentHandbookView.TalentTabItem> _tabItems; // 0xE0
	private EnhancedScroller _scroller; // 0xE8
	private TalentHandbookRowCellView _rowTemplate; // 0xF0
	private Transform _tabContent; // 0xF8
	private Transform _tabTemplate; // 0x100
	private ScrollRect _tabScrollRect; // 0x108
	private ToggleGroup _tabGroup; // 0x110
	private GameObject _cardPanel; // 0x118
	private GameObject _textPanel; // 0x120
	private Image _cardIcon; // 0x128
	private TMP_Text _cardNameText; // 0x130
	private TMP_Text _cardDescriptionText; // 0x138
	private TMP_Text _cardCategoryText; // 0x140
	private TMP_Text _textNameText; // 0x148
	private TMP_Text _textDescriptionText; // 0x150
	private TMP_Text _useTimeText; // 0x158
	private Button _cardSwitcher; // 0x160
	private Button _textSwitcher; // 0x168
	private RuntimeLuaTalent _selectedTalent; // 0x170
	private TalentHandbookView.TalentCategory _talentCategory; // 0x178
	private bool _showTextDetail; // 0x17C
}

// --------------------------------------------------------------------
// TalentCategoryLocalization  [class]
// --------------------------------------------------------------------
private sealed class TalentCategoryLocalization.<>c__DisplayClass3_0 // TypeDefIndex: 21793
{
	// Fields
	public string buffId; // 0x10
}

// --------------------------------------------------------------------
// TalentInGameButton  [class]
// --------------------------------------------------------------------
public class TalentInGameButton : MonoBehaviour // TypeDefIndex: 21795
{
	// Fields
	private const string TalentHandbookViewPath = "Activities/Talent/Prefabs/UI/window_talent.prefab";
}

// --------------------------------------------------------------------
// TalentPopView  [class]
// --------------------------------------------------------------------
public sealed class TalentPopView : BaseUIView // TypeDefIndex: 21797
{
	// Fields
	public Sprite img_title_cn; // 0xD8
	public Sprite img_title_en; // 0xE0
	public Sprite btn_cn; // 0xE8
	public Sprite btn_en; // 0xF0
}

// --------------------------------------------------------------------
// TalentTaskView  [class]
// --------------------------------------------------------------------
private sealed class TalentTaskView.<>c__DisplayClass10_0 // TypeDefIndex: 21798
{
	// Fields
	public TalentTaskView <>4__this; // 0x10
	public int taskId; // 0x18
}

// --------------------------------------------------------------------
// TalentTaskView  [class]
// --------------------------------------------------------------------
public class TalentTaskView : BaseUIView // TypeDefIndex: 21799
{
	// Fields
	private readonly List<GameObject> _taskItems; // 0xD8
	private Transform _content; // 0xE0
	private Transform _itemTemplate; // 0xE8
	private bool _isRefreshing; // 0xF0
}

// --------------------------------------------------------------------
// UIPanelTalentSelection  [enum]
// --------------------------------------------------------------------
private enum UIPanelTalentSelection.SelectionAction // TypeDefIndex: 21800
{
	// Fields
	public int value__; // 0x0
	public const UIPanelTalentSelection.SelectionAction Add = 0;
	public const UIPanelTalentSelection.SelectionAction AddExtra = 1;
	public const UIPanelTalentSelection.SelectionAction Forget = 2;
}

// --------------------------------------------------------------------
// UIPanelTalentSelection  [class]
// --------------------------------------------------------------------
public class UIPanelTalentSelection : MonoBehaviour // TypeDefIndex: 21801
{
	// Fields
	private const int ChoiceCount = 3;
	private const string PrefabPath = "Activities/Talent/Prefabs/UI/UIPanelTalentSelection.prefab";
	private const string DefaultTitleLocalizationKey = "I_tip_00";
	private const string ForgetTalentTitleLocalizationKey = "activity/talent/forget_talent_title";
	private static UIPanelTalentSelection _activePanel; // 0x0
	private float panelVisibleAlpha; // 0x20
	private float panelFadeInDuration; // 0x24
	private float panelFadeOutDuration; // 0x28
	private AudioClip showUpAudioClip; // 0x30
	private AudioClip confirmSuccessAudioClip; // 0x38
	private Graphic _dimGraphic; // 0x40
	private UITextLocalization _titleLocalization; // 0x48
	private TMP_Text _titleText; // 0x50
	private Tween _panelFadeTween; // 0x58
	private Color _dimBaseColor; // 0x60
	private UITalentSelectionCard[] _cards; // 0x70
	private UITalentSelectionCard _selectedCard; // 0x78
	private BattleData _battleData; // 0x80
	private RGRandom _random; // 0x88
	private emBuff[] _presentedTalents; // 0x90
	private TalentChoiceResultData _pendingTalentChoice; // 0x98
	private List<emBuff> _forgetUnpresentedTalents; // 0xA0
	private int _forgetRefreshRemainingCount; // 0xA8
	private int _forgetRefreshMaxCount; // 0xAC
	private UIPanelTalentSelection.SelectionAction _selectionAction; // 0xB0
	private int _remainingChoiceTimes; // 0xB4
	private bool _openNextChoiceAfterDisappear; // 0xB8
	private BattleData _nextChoiceBattleData; // 0xC0
	private RGRandom _nextChoiceRandom; // 0xC8
	private UIPanelTalentSelection.SelectionAction _nextChoiceAction; // 0xD0
	private int _nextChoiceTimes; // 0xD4
	private bool _isConfirmingSelection; // 0xD8
	private bool _pauseAppliedByThisPanel; // 0xD9
}

// --------------------------------------------------------------------
// UITalentActivityInfoWidget  [class]
// --------------------------------------------------------------------
private sealed class UITalentActivityInfoWidget.<>c__DisplayClass34_0 // TypeDefIndex: 21802
{
	// Fields
	public int rewardId; // 0x10
}

// --------------------------------------------------------------------
// UITalentActivityInfoWidget  [class]
// --------------------------------------------------------------------
private sealed class UITalentActivityInfoWidget.<CountingDown>d__49 : IEnumerator<object>, IEnumerator, IDisposable // TypeDefIndex: 21803
{
	// Fields
	private int <>1__state; // 0x10
	private object <>2__current; // 0x18
	public UITalentActivityInfoWidget <>4__this; // 0x20
	private DateTime <endDate>5__2; // 0x28
	private WaitForSecondsRealtime <wait>5__3; // 0x30
}

// --------------------------------------------------------------------
// UITalentActivityInfoWidget  [class]
// --------------------------------------------------------------------
public class UITalentActivityInfoWidget : BaseUIWidget // TypeDefIndex: 21804
{
	// Fields
	private const string TalentHandbookViewPath = "Activities/Talent/Prefabs/UI/window_talent.prefab";
	private Button _openOrCloseActivityBtn; // 0x48
	private Button _introBtn; // 0x50
	private Button _previewBtn; // 0x58
	private Button _taskBtn; // 0x60
	private Toggle _difficultyToggle; // 0x68
	private GameObject _difficultyImage0; // 0x70
	private GameObject _difficultyImage1; // 0x78
	private Button _difficultyImage0Button; // 0x80
	private Button _difficultyImage1Button; // 0x88
	private TMP_Text _doubleText; // 0x90
	private TMP_Text _leftText; // 0x98
	private TMP_Text _scoreText; // 0xA0
	private Coroutine _coroutine; // 0xA8
	private Transform _rewards; // 0xB0
	private Transform _progressBackgroundLayer; // 0xB8
	private Transform _progressFillLayer; // 0xC0
	private Transform _progressStateLayer; // 0xC8
	private UITalentProgressItemReward _rewardTemplate; // 0xD0
	private readonly List<UITalentProgressItemReward> _rewardItems; // 0xD8
	private readonly Dictionary<int, List<UITalentProgressItemReward>> _rewardItemsById; // 0xE0
	public Sprite btnOpen; // 0xE8
	public Sprite btnClose; // 0xF0
}

// --------------------------------------------------------------------
// UITalentActivityWidget  [class]
// --------------------------------------------------------------------
public class UITalentActivityWidget : UIActivityEntryDetailWidgetBase // TypeDefIndex: 21805
{
	// Fields
	private Transform _groups; // 0x48
	private UITalentActivityInfoWidget _introWidget; // 0x50
}

// --------------------------------------------------------------------
// UITalentProgressItemRewardData  [class]
// --------------------------------------------------------------------
public sealed class UITalentProgressItemRewardData // TypeDefIndex: 21806
{
	// Fields
	private int <RewardId>k__BackingField; // 0x10
	private string <ItemName>k__BackingField; // 0x18
	private int <Count>k__BackingField; // 0x20
	private int <RequireExp>k__BackingField; // 0x24
	private int <LimitTime>k__BackingField; // 0x28
	private int <ClaimedCount>k__BackingField; // 0x2C
	private bool <CanClaim>k__BackingField; // 0x30
	private bool <HasClaimed>k__BackingField; // 0x31
	private Action <ClaimReward>k__BackingField; // 0x38
}

// --------------------------------------------------------------------
// UITalentProgressItemReward  [struct]
// --------------------------------------------------------------------
private struct UITalentProgressItemReward.ProgressVisualState // TypeDefIndex: 21807
{
	// Fields
	public Transform Parent; // 0x0
	public int SiblingIndex; // 0x8
	public Vector2 AnchorMin; // 0xC
	public Vector2 AnchorMax; // 0x14
	public Vector2 Pivot; // 0x1C
	public Vector3 AnchoredPosition3D; // 0x24
	public Vector2 SizeDelta; // 0x30
	public Vector3 LocalScale; // 0x38
	public Quaternion LocalRotation; // 0x44
	public bool IsValid; // 0x54
}

// --------------------------------------------------------------------
// UITalentProgressItemReward  [class]
// --------------------------------------------------------------------
public class UITalentProgressItemReward : BaseUIWidget // TypeDefIndex: 21808
{
	// Fields
	private const float ClaimClickInterval = 0.5;
	private Button _rewardButton; // 0x48
	private Button _backgroundButton; // 0x50
	private Button _itemButton; // 0x58
	private Transform _reward; // 0x60
	private Item _item; // 0x68
	private TMP_Text _countText; // 0x70
	private TMP_Text _feeText; // 0x78
	private GameObject _outline; // 0x80
	private GameObject _finishIcon; // 0x88
	private GameObject _disableBackground; // 0x90
	private GameObject _defaultStateIcon; // 0x98
	private GameObject _availableStateIcon; // 0xA0
	private GameObject _claimedStateIcon; // 0xA8
	private GameObject _feeIcon; // 0xB0
	private Transform _progress; // 0xB8
	private Transform _progressBackground; // 0xC0
	private Transform _progressFill; // 0xC8
	private Transform _sliderCircle; // 0xD0
	private UITalentProgressItemReward.ProgressVisualState _progressBackgroundState; // 0xD8
	private UITalentProgressItemReward.ProgressVisualState _progressFillState; // 0x130
	private UITalentProgressItemReward.ProgressVisualState _sliderCircleState; // 0x188
	private UITalentProgressItemRewardData _data; // 0x1E0
	private GameObject _packageItem; // 0x1E8
	private string _packageItemName; // 0x1F0
	private int _packageItemCount; // 0x1F8
	private float _lastClaimClickTime; // 0x1FC
}

// --------------------------------------------------------------------
// UITalentSelectionCard  [class]
// --------------------------------------------------------------------
private sealed class UITalentSelectionCard.<>c__DisplayClass55_0 // TypeDefIndex: 21809
{
	// Fields
	public TweenCallback onComplete; // 0x10
}

// --------------------------------------------------------------------
// UITalentSelectionCard  [class]
// --------------------------------------------------------------------
public class UITalentSelectionCard : MonoBehaviour // TypeDefIndex: 21810
{
	// Fields
	public CanvasGroup canvasGroup; // 0x20
	public ParticleSystem vfx; // 0x28
	private Action<UITalentSelectionCard> Clicked; // 0x30
	private Action<UITalentSelectionCard> RefreshClicked; // 0x38
	private float selectedScale; // 0x40
	private float selectTweenDuration; // 0x44
	private float fadeInStartAlpha; // 0x48
	private float fadeInDuration; // 0x4C
	private float confirmSelectedScale; // 0x50
	private float confirmSelectedFadeDuration; // 0x54
	private float confirmOtherScale; // 0x58
	private float confirmOtherFadeDuration; // 0x5C
	private const string RefreshAudioPath = "Activities/Plant/Audio/Effect/fx_shop_item_reroll.mp3";
	private Button _button; // 0x60
	private Button _refreshButton; // 0x68
	private Transform _visualRoot; // 0x70
	private RectTransform _visualRectTransform; // 0x78
	private GameObject _background; // 0x80
	private GameObject _selectedBackground; // 0x88
	private Image _iconImage; // 0x90
	private TMP_Text _nameText; // 0x98
	private TMP_Text _descriptionText; // 0xA0
	private TMP_Text _categoryText; // 0xA8
	private TMP_Text _refreshCountText; // 0xB0
	private int _refreshRemainingCount; // 0xB8
	private int _refreshMaxCount; // 0xBC
	private bool _refreshCountInitialized; // 0xC0
	private Tween _scaleTween; // 0xC8
	private Tween _fadeTween; // 0xD0
	private Vector3 _defaultScale; // 0xD8
	private bool _referencesCached; // 0xE4
	private emBuff <Talent>k__BackingField; // 0xE8
}

// --------------------------------------------------------------------
// UIWindowBuffInfo  [class]
// --------------------------------------------------------------------
public class UIWindowBuffInfo : BaseUIView // TypeDefIndex: 21899
{
	// Fields
	private Button _closeBtn; // 0xD8
	private Transform _contentRoot; // 0xE0
	private GameObject _buffProto; // 0xE8
	public Sprite[] buffSprites; // 0xF0
}

// --------------------------------------------------------------------
// PlantBuffManager  [class]
// --------------------------------------------------------------------
private sealed class PlantBuffManager.<>c // TypeDefIndex: 22071
{
	// Fields
	public static readonly PlantBuffManager.<>c <>9; // 0x0
	public static Func<KeyValuePair<string, int>, string> <>9__75_1; // 0x8
}

// --------------------------------------------------------------------
// PlantBuffManager  [class]
// --------------------------------------------------------------------
private sealed class PlantBuffManager.<>c__DisplayClass152_0 // TypeDefIndex: 22072
{
	// Fields
	public ActivityPlantBuffType buffType; // 0x10
}

// --------------------------------------------------------------------
// PlantBuffManager  [class]
// --------------------------------------------------------------------
private sealed class PlantBuffManager.<>c__DisplayClass153_0 // TypeDefIndex: 22073
{
	// Fields
	public int level; // 0x10
}

// --------------------------------------------------------------------
// PlantBuffManager  [class]
// --------------------------------------------------------------------
private sealed class PlantBuffManager.<>c__DisplayClass161_0 // TypeDefIndex: 22074
{
	// Fields
	public PlantBuffManager <>4__this; // 0x10
	public ActivityPlantBuffType buffType; // 0x18
}

// --------------------------------------------------------------------
// PlantBuffManager  [class]
// --------------------------------------------------------------------
private sealed class PlantBuffManager.<>c__DisplayClass162_0 // TypeDefIndex: 22075
{
	// Fields
	public PlantBuffManager <>4__this; // 0x10
	public int level; // 0x18
}

// --------------------------------------------------------------------
// PlantBuffManager  [class]
// --------------------------------------------------------------------
private sealed class PlantBuffManager.<>c__DisplayClass173_0 // TypeDefIndex: 22076
{
	// Fields
	public List<ActivityPlantBuffConfig> result; // 0x10
	public Func<ActivityPlantBuffConfig, bool> <>9__0; // 0x18
}

// --------------------------------------------------------------------
// PlantBuffManager  [class]
// --------------------------------------------------------------------
private sealed class PlantBuffManager.<>c__DisplayClass173_1 // TypeDefIndex: 22077
{
	// Fields
	public ActivityPlantBuffConfig buff; // 0x10
}

// --------------------------------------------------------------------
// PlantBuffManager  [class]
// --------------------------------------------------------------------
private sealed class PlantBuffManager.<>c__DisplayClass175_0 // TypeDefIndex: 22078
{
	// Fields
	public HashSet<string> ownedBuffKeys; // 0x10
}

// --------------------------------------------------------------------
// PlantBuffManager  [class]
// --------------------------------------------------------------------
private sealed class PlantBuffManager.<>c__DisplayClass179_0 // TypeDefIndex: 22079
{
	// Fields
	public int buffLevel; // 0x10
}

// --------------------------------------------------------------------
// PlantBuffManager  [class]
// --------------------------------------------------------------------
public class PlantBuffManager // TypeDefIndex: 22080
{
	// Fields
	private readonly Dictionary<string, PlantBattleBuff> activeBuffs; // 0x10
	private readonly Dictionary<string, long> buffAcquisitionTimes; // 0x18
	public const string AttackSpeedBuffKey = "buff_plant_fast";
	public const string AttackStackBuffKey = "buff_plant_atk";
	public const string BuffDurationBuffKey = "buff_plant_buffTime";
	public const string EnergyIntervalBuffKey = "buff_plant_energy";
	public const string FertilizerOffenseBuffKey = "buff_plant_fertilizer1";
	public const string FertilizerSupportBuffKey = "buff_plant_fertilizer2";
	public const string FertilizerDefenseBuffKey = "buff_plant_fertilizer3";
	public const string ThroughBuffKey = "buff_plant_through";
	public const string ThreeShotBuffKey = "buff_plant_three";
	public const string OneShotBuffKey = "buff_plant_one";
	public const string TwoShotBuffKey = "buff_plant_two";
	public const string LuckyBuffKey = "buff_plant_lucky";
	public const string ElementBuffKey = "buff_plant_element";
	public const string FireBuffKey = "buff_plant_fire";
	public const string IceBuffKey = "buff_plant_ice";
	public const string PoisonBuffKey = "buff_plant_posion";
	public const string LightningBuffKey = "buff_plant_lighting";
	public const string SuperElementBuffKey = "buff_plant_superElement";
	public const string DoubleElementBuffKey = "buff_plant_doubleElement";
	public const string PlantDeadBuffKey = "buff_plant_dead";
	public const string PlantProtectBuffKey = "buff_plant_protect";
	public const string PlantReflectBuffKey = "buff_plant_reflect";
	public const string BreedBuffKey = "buff_plant_breed";
	public const string WeedKillerBuffKey = "buff_plant_weedKiller";
	public const string GrowthPotionBuffKey = "buff_plant_growthPotion";
	public const string MoreMoreBuffKey = "buff_plant_moreMore";
	public const string GambleSymphonyBuffKey = "buff_plant_gambleSymphony";
	public const string DefenseToOffenseBuffKey = "buff_plant_defenseToOffense";
	public const string SacrificeBuffKey = "buff_plant_sacrifice";
	public const string SacrificeGoldBuffKey = "buff_plant_sacrificeGold";
	private static ActivityPlantBuffConfig s_attackSpeedBuffConfig; // 0x0
	private static ActivityPlantBuffConfig s_attackStackBuffConfig; // 0x8
	private static ActivityPlantBuffConfig s_buffDurationBuffConfig; // 0x10
	private static ActivityPlantBuffConfig s_throughBuffConfig; // 0x18
	private static ActivityPlantBuffConfig s_threeShotBuffConfig; // 0x20
	private static ActivityPlantBuffConfig s_oneShotBuffConfig; // 0x28
	private static ActivityPlantBuffConfig s_twoShotBuffConfig; // 0x30
	private static ActivityPlantBuffConfig s_buffIntervalBuffConfig; // 0x38
	private static ActivityPlantBuffConfig s_fertilizerOffenseBuffConfig; // 0x40
	private static ActivityPlantBuffConfig s_fertilizerSupportBuffConfig; // 0x48
	private static ActivityPlantBuffConfig s_fertilizerDefenseBuffConfig; // 0x50
	private static ActivityPlantBuffConfig s_elementBuffConfig; // 0x58
	private static ActivityPlantBuffConfig s_fireBuffConfig; // 0x60
	private static ActivityPlantBuffConfig s_iceBuffConfig; // 0x68
	private static ActivityPlantBuffConfig s_poisonBuffConfig; // 0x70
	private static ActivityPlantBuffConfig s_lightningBuffConfig; // 0x78
	private static ActivityPlantBuffConfig s_superElementBuffConfig; // 0x80
	private static ActivityPlantBuffConfig s_doubleElementBuffConfig; // 0x88
	private static ActivityPlantBuffConfig s_plantProtectBuffConfig; // 0x90
	private static ActivityPlantBuffConfig s_plantReflectBuffConfig; // 0x98
	private static bool s_attackSpeedBuffConfigLoaded; // 0xA0
	private static bool s_attackStackBuffConfigLoaded; // 0xA1
	private static bool s_buffDurationBuffConfigLoaded; // 0xA2
	private static bool s_throughBuffConfigLoaded; // 0xA3
	private static bool s_threeShotBuffConfigLoaded; // 0xA4
	private static bool s_oneShotBuffConfigLoaded; // 0xA5
	private static bool s_twoShotBuffConfigLoaded; // 0xA6
	private static bool s_buffIntervalBuffConfigLoaded; // 0xA7
	private static bool s_fertilizerOffenseBuffConfigLoaded; // 0xA8
	private static bool s_fertilizerSupportBuffConfigLoaded; // 0xA9
	private static bool s_fertilizerDefenseBuffConfigLoaded; // 0xAA
	private static bool s_elementBuffConfigLoaded; // 0xAB
	private static bool s_fireBuffConfigLoaded; // 0xAC
	private static bool s_iceBuffConfigLoaded; // 0xAD
	private static bool s_poisonBuffConfigLoaded; // 0xAE
	private static bool s_lightningBuffConfigLoaded; // 0xAF
	private static bool s_superElementBuffConfigLoaded; // 0xB0
	private static bool s_doubleElementBuffConfigLoaded; // 0xB1
	private static bool s_plantProtectBuffConfigLoaded; // 0xB2
	private static bool s_plantReflectBuffConfigLoaded; // 0xB3
	private Action<PlantBattleBuff> BuffAdded; // 0x20
	private Action<PlantBattleBuff> BuffRemoved; // 0x28
}

// --------------------------------------------------------------------
// PlayerDamageTypeModifier  [class]
// --------------------------------------------------------------------
private class PlayerDamageTypeModifier.DamageTypeModification // TypeDefIndex: 22081
{
	// Fields
	public ElementalType damageType; // 0x10
	public float remainingDuration; // 0x14
	public string id; // 0x18
}

// --------------------------------------------------------------------
// PlayerDamageTypeModifier  [class]
// --------------------------------------------------------------------
private sealed class PlayerDamageTypeModifier.<>c__DisplayClass12_0 // TypeDefIndex: 22082
{
	// Fields
	public string id; // 0x10
}

// --------------------------------------------------------------------
// PlayerDamageTypeModifier  [class]
// --------------------------------------------------------------------
private sealed class PlayerDamageTypeModifier.<>c__DisplayClass13_0 // TypeDefIndex: 22083
{
	// Fields
	public string id; // 0x10
}

// --------------------------------------------------------------------
// PlayerDamageTypeModifier  [class]
// --------------------------------------------------------------------
public class PlayerDamageTypeModifier : MonoBehaviour // TypeDefIndex: 22084
{
	// Fields
	private GameObject defaultHitEffectPrefab; // 0x20
	private const float EffectCooldownDuration = 3;
	private float spawnChance; // 0x28
	private RGBaseController controller; // 0x30
	private float _lastEffectTime; // 0x38
	private List<PlayerDamageTypeModifier.DamageTypeModification> activeModifications; // 0x40
	private Nullable<ElementalType> currentOverrideDamageType; // 0x48
	private HashSet<DamageTrigger> subscribedTriggers; // 0x50
}

// --------------------------------------------------------------------
// HeroBallBuffInfo  [struct]
// --------------------------------------------------------------------
public struct HeroBallBuffInfo // TypeDefIndex: 22346
{
	// Fields
	private string buffId; // 0x0
	private string displayName; // 0x8
	private int level; // 0x10
}

// --------------------------------------------------------------------
// HeroBallBuffManager  [struct]
// --------------------------------------------------------------------
private struct HeroBallBuffManager.DebugBuffPreset // TypeDefIndex: 22561
{
	// Fields
	public string buffId; // 0x0
	public int level; // 0x8
}

// --------------------------------------------------------------------
// HeroBallBuffManager  [class]
// --------------------------------------------------------------------
public sealed class HeroBallBuffManager : MonoBehaviour // TypeDefIndex: 22562
{
	// Fields
	private HeroBallRuntimeContext context; // 0x20
	private bool applyDebugBuffsOnStart; // 0x28
	private List<HeroBallBuffManager.DebugBuffPreset> debugBuffs; // 0x30
	private int maxBuffSlots; // 0x38
	private readonly Dictionary<string, HeroBallBuffState> _stateLookup; // 0x40
	private readonly List<HeroBallBuffState> _activeStates; // 0x48
	private readonly List<string> _buffOrder; // 0x50
	private readonly List<HeroBallBuffDefinition> _runtimeDefinitions; // 0x58
	private readonly Dictionary<string, HeroBallBuffDefinition> _definitionLookup; // 0x60
	private readonly Dictionary<object, int> _buffSlotModifiers; // 0x68
	private Action<IReadOnlyList<HeroBallBuffState>> BuffsChanged; // 0x70
}

// --------------------------------------------------------------------
// BuildTalentBuffEffect  [class]
// --------------------------------------------------------------------
public sealed class BuildTalentBuffEffect : MonoBehaviour // TypeDefIndex: 22571
{
	// Fields
	private const string BuffId = "heroBall_buff_buildTalent";
	private HeroBallRuntimeContext context; // 0x20
	private HeroBallBuffManager buffManager; // 0x28
	private float defaultBonusChance; // 0x30
	private HeroBallBuffState _state; // 0x38
	private float _bonus; // 0x40
}

// --------------------------------------------------------------------
// TalentListItemData  [class]
// --------------------------------------------------------------------
public class TalentListItemData // TypeDefIndex: 22671
{
	// Fields
	private int <TalentId>k__BackingField; // 0x10
	private string <Type>k__BackingField; // 0x18
	private string <Title>k__BackingField; // 0x20
	private string <Desc>k__BackingField; // 0x28
	private int <Rarity>k__BackingField; // 0x30
	private string <RarityText>k__BackingField; // 0x38
	private bool <Overlay>k__BackingField; // 0x40
	private string <Param>k__BackingField; // 0x48
	private int <SortOrder>k__BackingField; // 0x50
}

// --------------------------------------------------------------------
// TalentLevelGroupData  [class]
// --------------------------------------------------------------------
public class TalentLevelGroupData // TypeDefIndex: 22672
{
	// Fields
	private int <Level>k__BackingField; // 0x10
	private string <LevelText>k__BackingField; // 0x18
	private List<TalentListItemData> <TalentList>k__BackingField; // 0x20
}

// --------------------------------------------------------------------
// TalentDetailData  [class]
// --------------------------------------------------------------------
public class TalentDetailData // TypeDefIndex: 22673
{
	// Fields
	private int <TalentId>k__BackingField; // 0x10
	private string <Type>k__BackingField; // 0x18
	private string <Title>k__BackingField; // 0x20
	private string <Desc>k__BackingField; // 0x28
	private int <Rarity>k__BackingField; // 0x30
	private string <RarityText>k__BackingField; // 0x38
	private bool <Overlay>k__BackingField; // 0x40
	private string <Param>k__BackingField; // 0x48
}

// --------------------------------------------------------------------
// GunMasterTalentAcquiredEvent  [class]
// --------------------------------------------------------------------
public class GunMasterTalentAcquiredEvent : EventBase // TypeDefIndex: 22696
{
	// Fields
	public int talentId; // 0x10
	private static GunMasterTalentAcquiredEvent _cache; // 0x0
}

// --------------------------------------------------------------------
// UIGunMasterTalentDetail  [class]
// --------------------------------------------------------------------
public class UIGunMasterTalentDetail : MonoBehaviour // TypeDefIndex: 22740
{
	// Fields
	public Image img; // 0x20
	public Text nameText; // 0x28
	public Text descText; // 0x30
}

// --------------------------------------------------------------------
// UIGunMasterTalentItem  [class]
// --------------------------------------------------------------------
public class UIGunMasterTalentItem : MonoBehaviour // TypeDefIndex: 22741
{
	// Fields
	public Image img; // 0x20
	public Text nameText; // 0x28
	public Image colorImg; // 0x30
	private TalentListItemData <Data>k__BackingField; // 0x38
	private Button _button; // 0x40
	public Action<UIGunMasterTalentItem> onClickAct; // 0x48
}

// --------------------------------------------------------------------
// UIGunMasterTalentListView  [class]
// --------------------------------------------------------------------
public class UIGunMasterTalentListView : MonoBehaviour // TypeDefIndex: 22742
{
	// Fields
	private const float DetailMinBottomPadding = 20;
	public float detailOffsetY; // 0x20
	public Transform root; // 0x28
	private Transform _tip; // 0x30
	private UIGunMasterTalentDetail _detail; // 0x38
	private Button _closeDetailBtn; // 0x40
	private int _selectedTalentId; // 0x48
}

// --------------------------------------------------------------------
// UIWindowElementTalent  [struct]
// --------------------------------------------------------------------
public struct UIWindowElementTalent.ElementHeaderInfo // TypeDefIndex: 23025
{
	// Fields
	public EmActElementType elementType; // 0x0
	public bool isElementHeader; // 0x4
}

// --------------------------------------------------------------------
// UIWindowElementTalent  [class]
// --------------------------------------------------------------------
private sealed class UIWindowElementTalent.<>c__DisplayClass36_0 // TypeDefIndex: 23026
{
	// Fields
	public UIWindowElementTalent <>4__this; // 0x10
	public int theIndex; // 0x18
	public ActivityElementBuffConfig theTalent; // 0x20
}

// --------------------------------------------------------------------
// UIWindowElementTalent  [class]
// --------------------------------------------------------------------
private sealed class UIWindowElementTalent.<>c__DisplayClass42_0 // TypeDefIndex: 23027
{
	// Fields
	public EmActElementType elementType; // 0x10
}

// --------------------------------------------------------------------
// UIWindowElementTalent  [class]
// --------------------------------------------------------------------
private sealed class UIWindowElementTalent.<>c__DisplayClass43_0 // TypeDefIndex: 23028
{
	// Fields
	public EmActElementType elementType; // 0x10
}

// --------------------------------------------------------------------
// UIWindowElementTalent  [class]
// --------------------------------------------------------------------
private sealed class UIWindowElementTalent.<>c__DisplayClass46_0 // TypeDefIndex: 23029
{
	// Fields
	public int elementId; // 0x10
	public UIWindowElementTalent <>4__this; // 0x18
	public int index; // 0x20
	public UIWindowElementTalent.ElementHeaderInfo elementHeader; // 0x24
}

// --------------------------------------------------------------------
// UIWindowElementTalent  [class]
// --------------------------------------------------------------------
public class UIWindowElementTalent : BaseUIView // TypeDefIndex: 23030
{
	// Fields
	private Button btnAll; // 0xD8
	private Button btnFire; // 0xE0
	private Button btnIce; // 0xE8
	private Button btnElectric; // 0xF0
	private Button btnPoison; // 0xF8
	private Transform talentListParent; // 0x100
	private GameObject talentGridPrefab; // 0x108
	private Button closeButton; // 0x110
	private EmActElementType currentSelectedElement; // 0x118
	private List<ElementBuffGrid> talentGrids; // 0x120
	private Transform panel; // 0x128
	private Text _disableTip; // 0x130
	private List<ValueTuple<ActivityElementBuffConfig, int>> allPurchasedTalents; // 0x138
	private const int MIN_TALENT_GRID_COUNT = 8;
	private int selectedTalentIndex; // 0x140
	private Color _grayColor; // 0x144
	private float _itemActivedOffset; // 0x154
	private float _itemUnActivedOffset; // 0x158
	private Transform _buffInfoTR; // 0x160
	private Text _buffInfoText; // 0x168
	private Image _buffInfoImage; // 0x170
	private Text _titleText; // 0x178
}

// --------------------------------------------------------------------
// UISelectIceTalent  [class]
// --------------------------------------------------------------------
public class UISelectIceTalent : BaseUIView // TypeDefIndex: 23083
{
	// Fields
	private RGRandom _random; // 0xD8
	private Button _ingredientLeftBtn; // 0xE0
	private Button _ingredientRightBtn; // 0xE8
	private Button _confirmBtn; // 0xF0
	private Transform _ingredientDesLeft; // 0xF8
	private Transform _ingredientDesRight; // 0x100
	private Transform _styleLeft; // 0x108
	private Transform _styleRight; // 0x110
	private Transform _selectLeft; // 0x118
	private Transform _selectRight; // 0x120
	private List<emBuff> buffList; // 0x128
	private int selectIndex; // 0x130
	private uint _npcStateSyncId; // 0x134
}

// --------------------------------------------------------------------
// Rfc2898DeriveBytes  [class]
// --------------------------------------------------------------------
public class Rfc2898DeriveBytes : DeriveBytes // TypeDefIndex: 24005
{
	// Fields
	private readonly byte[] _password; // 0x0
	private byte[] _salt; // 0x0
	private uint _iterations; // 0x10
	private HMAC _hmac; // 0x14
	private int _blockSize; // 0x18
	private byte[] _buffer; // 0x10
	private uint _block; // 0x10
	private int _startIndex; // 0x0
	private int _endIndex; // 0x0
	private readonly HashAlgorithmName <HashAlgorithm>k__BackingField; // 0x90
}

// --------------------------------------------------------------------
// XmlCharacterData  [class]
// --------------------------------------------------------------------
public abstract class XmlCharacterData : XmlLinkedNode // TypeDefIndex: 28919
{
	// Fields
	private string data; // 0x20
}

// --------------------------------------------------------------------
// GameConfigParse  [class]
// --------------------------------------------------------------------
public class GameConfigParse // TypeDefIndex: 36964
{
	// Fields
	private const string secret = "PASSWORD";
	private const string cypherText = "1\"8ku|";
	private static string m_currentPath; // 0x0
	private static JObject m_gameConfigJObject; // 0x8
}

// --------------------------------------------------------------------
// CreateRoleAttributes  [struct]
// --------------------------------------------------------------------
public struct CreateRoleAttributes // TypeDefIndex: 44586
{
	// Fields
	public int reportingToTencentSdk; // 0x0
	public string mpRoleName; // 0x8
	public Dictionary<string, object> customProperties; // 0x10
}
