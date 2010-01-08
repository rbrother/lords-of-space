# Calculator for battles in twilight imperium

require 'test/unit'
require 'math'
require 'fleet'

class SpaceBattle
    
    def initialize(fleetA, fleetB)
        @fleets = [fleetA, fleetB]
    end
        
    def fight
        pre_combat_effects
        fight_round until @fleets.any? { |f| f.destroyed? }  
    end
    
    def fight_round
        apply_hits( @fleets.map{ |fleet| fleet.fire } )
    end
      
    def apply_hits(hits_scored)
        damages_taken = hits_scored.reverse
        @fleets.zip(damages_taken) do |fleet,damage|
            fleet.take_damage(damage)
        end
    end
    
    def ship_counts; @fleets.map { |f| f.size }; end
    
    def to_s
        "\nBattle Status:\n" +
        "    #{@fleets.first}\n" +
        "    #{@fleets.last}\n"
    end
        
    def pre_combat_effects
        apply_hits( @fleets.map{ |fleet| fleet.pds_fire } )
    end
          
end

class BattleIterator

    def self.simulate(race1, fleet_spec1, race2, fleet_spec2)
      iterator = BattleIterator.new(race1, fleet_spec1, race2, fleet_spec2) 
      iterator.simulate_multiple_fights
      iterator.print_statistics      
    end
    
    def initialize(nameA, fleetSpecA, nameB, fleetSpecB)
        @names = [nameA, nameB]
        @fleetSpecA = fleetSpecA
        @fleetSpecB = fleetSpecB
        @statuses = Hash.new(0.0)
        @battle_count = 1000
        puts createBattle
    end
    
    def simulate_multiple_fights
        @battle_count.times do 
            battle = createBattle
            battle.fight
            @statuses[battle.ship_counts] += 1.0
        end        
    end
    
    def print_statistics
        puts "\nResults for ships remaining for #{@names.inspect}"
        @statuses.keys.sort {|a,b| a[0]-a[1] <=> b[0]-b[1]} .each do |result|
            percent = (@statuses[result] / @battle_count).percent
            puts "Result #{result.inspect}: #{"*" * percent.to_i} #{percent} %" 
        end        
        aWinsP = total_winnings_percent(0)
        bWinsP = total_winnings_percent(1)
        puts "\nWinnings: #{@names[0]}: #{aWinsP}%, #{@names[1]}: #{bWinsP}% (both destroyed #{(100.0-aWinsP-bWinsP).roundDecimals}%)"
    end
    
    protected
    
    def total_winnings_percent(player)
        wins = @statuses.keys.find_all { |sk| sk[player] > 0 } .inject(0.0) { |sum,key| sum + @statuses[key] }
        (wins / @battle_count).percent
    end
        
    def createBattle
        SpaceBattle.new( Fleet.new(@names.first, @fleetSpecA ), Fleet.new(@names.last, @fleetSpecB ) )
    end    
    
end

