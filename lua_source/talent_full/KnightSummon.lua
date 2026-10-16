local KnightSummon = TalentBase.create(CS.emBuff.KnightSummon)
local Buffs = require 'app.buff.init'

function KnightSummon.on_add(player)
    Buffs.add(player, CS.emBuff.KnightSummon, "app.buff.KnightSummon", {
        events = {
            "HeroUseSkillEvent"
        },
        display = {
            icon_buff = CS.emBuff.KnightSummon,
            desc_key = CS.emBuff.KnightSummon,
            show_counter = false
        },
        cooldown = 12,
        prefab_path = "RGPrefab/Pet/npc_knight_01.prefab",
        summon_effect_path = "RGPrefab/Effect/fx_obj_smoke"
    })
end

function KnightSummon.on_remove(player)
    Buffs.remove(player, CS.emBuff.KnightSummon)
end

return KnightSummon