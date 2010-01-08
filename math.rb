require 'test/unit'

class Numeric
    # calculates x^y where y is integer
    def intPower(n)
        (1..n).inject(1) { |product,i| product * self }
    end
end

class Float
    def roundDecimals(decimals = 1)
        m = 10.intPower(decimals).to_f
        (self * m).round / m
    end
    def percent(decimals = 1)
        (self * 100.0).roundDecimals(decimals) 
    end
end

# ********************* TESTS ***************************

class TestMath < Test::Unit::TestCase
    def testIntPower
        assert_equal( 81, 3.intPower(4) )
        assert_equal( 15.625, 2.5.intPower(3) )
    end
    def testRoundDecimals
        assert_equal( 625.3, 625.3456.roundDecimals(1) )
        assert_equal( 4.17, 4.1678.roundDecimals(2) )
    end
end

