require 'test/unit'
require 'ships'
require 'set'

# Define generic sum function for arrays
class Array
    def sum; inject(0) { |sum,x| sum ? sum+x : x }; end; 
    def class_counts
        counts = Hash.new(0)
        self.each { |u| counts[u.class] += 1 }   
        counts      
    end
end

class Hash
    def inspect_sorted(&block)        
        self.keys.sort_by(&block).map { |k| "#{k} => #{self[k]}" }.join(', ')
    end
end

class Fleet

    attr_reader :race  
    
    def initialize(race, unit_counts)
        @race = race
        @units = []
        unit_counts.each_pair do | unit, count |
            count.times { @units << unit.new }
        end 
    end

    def fire
        ships.map { |ship| ship.fire }.sum
    end
      
    def pds_fire
        pds_units.map { |pds| pds.fire }.sum
    end
      
    def take_damage(points)
        return if points == 0
        ship = ship_to_take_damage()
        ship.take_hit
        @units.delete(ship) if ship.destroyed?
        return if self.destroyed?
        take_damage(points-1) # recursively take remaining damage
    end
            
    def destroyed?
        ships.empty?        
    end
    
    def to_s
        "Fleet #{@race}: #{unit_counts_str} (cost #{production_cost})"
    end
            
    def unit_counts
      @units.class_counts
    end

    def unit_counts_str
        unit_counts.inspect_sorted { |cls| cls.sort_order }
    end

    def size
        ships.length
    end
    
    def production_cost
        @units.map { |ship| ship.class.production_cost }.sum
    end
           
    def ships
      @units.select { |s| s.respond_to?(:take_hit) }
    end
    
    def pds_units 
      @units.select { |u| u.kind_of?(PDS) }
    end
       
    def ship_to_take_damage
      ships.min { |a,b| a.cost_of_taking_hit <=> b.cost_of_taking_hit }
    end    
        
end

class TestFleet < Test::Unit::TestCase
    def testCreateFleet
        fleet = Fleet.new("Roope", { Fighter => 1, Carrier => 1, Cruiser => 2, Dreadnought => 1, PDS => 2 } )
        assert_equal( 5, fleet.size )
        assert_equal( 2, fleet.pds_units.size )
        assert_equal( "Fleet Roope: Dreadnought => 1, Carrier => 1, Cruiser => 2, Fighter => 1, PDS => 2 (cost 16.5)", fleet.to_s)
        assert fleet.ship_to_take_damage.kind_of?(Dreadnought)
        fleet.take_damage(3)
        assert_equal( "Fleet Roope: Dreadnought => 1, Carrier => 1, Cruiser => 1, PDS => 2 (cost 14)", fleet.to_s)        
    end
end

