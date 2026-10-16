local ExclusiveDivineWeapon = TalentBase.create(CS.emBuff.ExclusiveDivineWeapon)
local Buffs = require 'app.buff.init'

function ExclusiveDivineWeapon.on_add(player)
    Buffs.add(player, CS.emBuff.ExclusiveDivineWeapon, "app.buff.ExclusiveDivineWeapon", {
        events = {
            "RGBaseControllerCreateBulletEvent"
        },
        attack_speed = 0.25,
        weapon_damage_factor = 0.25,
        critical = 25,
        bullet_size_increase = 0.25,
        weapon_scale_factor = 1.25,
        fixed_back_weapon_count = 0,
        bullet_size_mark = "RuntimeLuaExclusiveDivineWeapon",
        weapon_star_prefab_path = "RGTexture/weapon/mythic/star.prefab",
        weapon_visual_path = "w",
        weapon_star_sorting_order_offset = 100
    })
end

function ExclusiveDivineWeapon.on_remove(player)
    Buffs.remove(player, CS.emBuff.ExclusiveDivineWeapon)
end

return ExclusiveDivineWeapon