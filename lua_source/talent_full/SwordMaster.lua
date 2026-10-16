local SwordMaster = TalentBase.create(CS.emBuff.SwordMaster)
local Buffs = require 'app.buff.init'

function SwordMaster.on_add(player)
    Buffs.add(player, CS.emBuff.SwordMaster, "app.buff.SwordMaster", {
        events = {
            "RGBaseControllerCreateBulletEvent"
        },
        melee_size_factor = 0.25,
        melee_size_mark = "RuntimeLuaSwordMaster"
    })
end

function SwordMaster.on_remove(player)
    Buffs.remove(player, CS.emBuff.SwordMaster)
end

return SwordMaster