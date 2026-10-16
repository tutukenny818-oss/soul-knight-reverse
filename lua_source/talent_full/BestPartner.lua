local Buffs = require 'app.buff.init'

function BestPartner.on_add(player)
    Buffs.add(player, CS.emBuff.BestPartner, "app.buff.BestPartner", Values)
end

function BestPartner.on_remove(player)
    Buffs.remove(player, CS.emBuff.BestPartner)
end

return BestPartner