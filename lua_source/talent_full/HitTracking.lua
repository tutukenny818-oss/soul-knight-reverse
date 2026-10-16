local TalentBase = require 'app.talent.TalentBase'
local HitTracking = TalentBase.create(CS.emBuff.HitTracking)
local Buffs = ire 'app.buff.init'

function HitTracking.on_add(player)
    Buffs.add(player, CS.emBuff.HitTracking, "app.buff.HitTracking", {
        ~