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

def build(aclass, attrs = {})
    instance = aclass.new.attr(attrs)
    if block_given?
        # to-do: add contents of block to contents of widget
    end
    return instance
end

window = build(Wpf::Window, :width => 400, :height => 300, :title => "Ti3 battle calculator")

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