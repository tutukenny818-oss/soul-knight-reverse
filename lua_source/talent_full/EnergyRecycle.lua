local EnergyRecycle = TalentBase.create(CS.emBuff.EnergyRecycle)
local Buffs = require 'app.buff.init'

function EnergyRecycle.on_add(player)
    Buffs.add(player, CS.emBuff.EnergyRecycle, "app.buff.EnergyRecycle", {
        events = {
            "EnergyChangedEvent"
        },
        display = {
            icon_buff = CS.emBuff.EnergyRecycle,
            desc_key = CS.emBuff.EnergyRecycle
        },
        energy_per_stack = 10,
        damage_factor_per_stack = 0.05,
        attack_speed_per_stack = 0.05,
        aoe_range_per_stack = 0.1,
        stack_duration = 12,
        max_stack = 5
    })
end

function EnergyRecycle.on_remove(player)
    Buffs.remove(player, CS.emBuff.EnergyRecycle)
end

return EnergyRecycle