require 'battle-calculator'

BattleIterator.simulate(
    :winnu, { Dreadnought => 2, Cruiser => 2, Carrier => 2, Destroyer => 2, Fighter => 2 },
    :mentak, { Dreadnought => 1, Cruiser => 3, Fighter => 2, PDS => 1 } 
) 
