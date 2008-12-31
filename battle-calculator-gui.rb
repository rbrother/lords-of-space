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

FileInfo = System::IO::FileInfo

window = Wpf::XamlReader.load(FileInfo.new("gui/BattleCalculator.xaml").open_read)

attacker_props = Wpf::XamlReader.load(FileInfo.new("gui/RaceProperties.xaml").open_read)
defender_props = Wpf::XamlReader.load(FileInfo.new("gui/RaceProperties.xaml").open_read)

window.find_name("Attacker").parent.content = attacker_props
window.find_name("Defender").parent.content = defender_props

window.size_to_content = Wpf::SizeToContent.width_and_height

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