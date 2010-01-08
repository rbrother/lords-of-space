require 'test/unit'

def d10
    rand(10)+1    
end

class Unit

    def initialize(required_to_hit, fire_count = 1)
        @required_to_hit = required_to_hit    
        @fire_count = fire_count
    end
  
    def fire
        # TODO: Use inject here? eliminates local variable
        hits = 0
        @fire_count.times { hits += 1 if d10 >= @required_to_hit }
        hits
    end

    def to_s
        self.class.to_s
    end
    
end

# Generic ship
class Ship < Unit
    
    def initialize(required_to_hit, fire_count = 1, hit_points = 1)
        super required_to_hit, fire_count
        @current_hit_points  = @max_hit_points = hit_points 
    end
    
    attr_reader :current_hit_points 
    
    def take_hit
        @current_hit_points -= 1
    end
    
    def destroyed?
        @current_hit_points <= 0
    end
        
    def repair
        raise "Cannot repair destroyed ship" if destroyed?
        @current_hit_points = @max_hit_points
    end
    
    def cost_of_taking_hit
        case 
            when @current_hit_points == 1 : self.class.production_cost
            # even if ship can sustaini damage, assign some small value to damage cost because of possibility of "direct hit" card 
            when @current_hit_points > 1 : self.class.production_cost * 0.01
            else raise "unexpected @current_hit_points"
        end
    end
        
    def to_s
        self.class.to_s + (@current_hit_points < @max_hit_points ? " (damaged)" : "")
    end
    
    def self.sort_order; -production_cost; end
            
end

class Fighter < Ship
    def self.production_cost; 0.5; end
    def initialize
        super 9
    end
end

class Destroyer < Ship
    def self.production_cost; 1; end
    def initialize
        super 9
    end
end

class Carrier < Ship
    def self.production_cost; 3; end
    def initialize
        super 9
    end
end

class Cruiser < Ship
    def self.production_cost; 2; end  
    def initialize
        super 7
    end
end

class Dreadnought < Ship
    def self.production_cost; 5; end  
    def initialize
        super 5, 2, 2
    end
end

class WarSun < Ship
    def self.production_cost; 12; end  
    def initialize
        super 3, 3, 3
    end
end

class PDS < Unit
  def self.sort_order; 1000; end
  def self.production_cost; 2; end
  def initialize
    super 6
  end
end


#**********************  TESTS  ******************************

class TestShips < Test::Unit::TestCase

    def testFighter
        f = Fighter.new        
        assert_equal( 1, f.current_hit_points )
        assert_equal( false, f.destroyed? )
        assert_equal( 0.5, f.cost_of_taking_hit )
        f.take_hit
        assert_equal( true, f.destroyed? )
    end

    def testDreadnought
        f = Dreadnought.new
        assert_equal( 0.05, f.cost_of_taking_hit )
        f.take_hit
        assert_equal( false, f.destroyed? )
        assert_equal( "Dreadnought (damaged)", f.to_s)
        f.repair
        assert_equal( false, f.destroyed? )
        assert_equal( 2, f.current_hit_points )
    end
    
end
