Dwarf

//Iron shot
while button action1:
   //fire rate
   cooldown _ironshot_cddur _ironshot_cdremaining?
   //mana
   spend _ironshot_cost?
   //shoot projectile
   _ironshot = spawn "ironshot" dir mousedir _ironshotRange, shoot dir mousedir
   //insert _ironshot @shot
   _shootDir = mousedir


//Wall
while button action3:
   charge rate _wallChargeRate _wallCharge max _wallChargeMax
   
on buttonup action3:
   consume _wallCharge min _wallChargeMin?
   _wall = spawn "wall" pos range mousepos _wallRange
   rampstats _wall $wallStats _wallCharge zero _wallChargeMax

//Turret
while button action2:
   charge rate _turretChargeRate _turretCharge max _turretChargeMax

on buttonup action2:
   consume _turretCharge min _turretChargeMin?
   _turret = spawn "turret" pos range mousepos _turretRange
   rampstats _turret $turretStats _turretCharge zero _turretChargeMax

on variable _shootDir, foreach "turret" _turret:
   _ironshot = spawn "ironshot" dir _shootDir _ironshotRange, shoot dir _shootDir
   //insert _ironshot @shot

projectile "ironshot":
   hp: 1
   duration: 1
   limit: hpdrain
   damage: 1
   speed: 3
   prefab: [prefab ironshot]

# while tick, foreach "ironshot" _ironshot:
#    move _ironshot_cddur

while tick:
   _lookDir = mousedir

# while tick, foreach "turret" _turret:
#    _turret _lookDir = _lookDir

object "wall":
   hp: 100
   damage: 10
   prefab: [prefab wall]

object "turret":
   hp: 10
   damage: 1
   prefab: [prefab wall]





//Reload
on buttondown reload:
   lock mana
   enable @reloading
   enable @main false

on mana empty:
   lock mana
   enable @reloading
   enable @main false

@reloading{
   while tick:
      locked mana?
      recharge rate _manaRechargeRate

   on mana full:
      locked mana?
      lock mana false
      enable @reloading false
      enable @main
}



@shot{
   on hit player _target:
      stun _target _stunDuration
      destroy this

   on hit spawn _shot:
      damage _shot _damage
}