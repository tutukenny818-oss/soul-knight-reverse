local TalentBase = require 'app.talent.TalentBase'
local SkillHaste = TalentBase.create(CS.emBuff.SkillHaste)
local Buffs = require 'app.buff.init'

function SkillHaste.on_add(player)
    Buffs.add(player, CS.emBuff.SkillHaste, "app.buff.SkillHaste", {
        skill_haste = 25
    })
end

function SkillHaste.on_remove(player)
    Buffs.remove(player, CS.emBuff.SkillHaste)
end

return SkillHaste