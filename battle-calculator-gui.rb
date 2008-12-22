require 'mscorlib'
require 'wpf'
require 'test/unit'

class Object
    def attr(hash)
        hash.each_pair do |key,value|
            self.send(key.to_s + "=", value)
        end
        return self
    end
end


window = Wpf::Window.new.attr(:width => 400, :height => 300)

app = Wpf::Application.new
app.run(window)


class ObjectExtensionTest < Test::Unit::TestCase
    
    class Mike
        attr_accessor :cat, :dog
    end

    def test_attr
        m = Mike.new.attr(:cat => "joo", :dog => :nice)
        assert_equal('#<ObjectExtensionTest::Mike:0x00000c4 @cat="joo", @dog=:nice>', m.inspect)
        assert_equal("joo", m.cat)
    end
    
end