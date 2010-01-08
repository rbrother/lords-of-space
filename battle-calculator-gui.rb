require 'mscorlib'
require 'wpf'
require 'test/unit'

require 'gui/bin/LordsOfSpace.dll'

FileInfo = System::IO::FileInfo

class Object
    def attr(hash)
        hash.each_pair do |key,value|
            self.send(key.to_s + "=", value)
        end
        return self
    end
    
    def enumerator_to_a
        Net::Brotherus::EnumeratorUtil.enumerator_to_list(self)
    end
    
end

class Wpf::XamlReader
    def self.load_file(file_name)
        file = FileInfo.new(file_name).open_read
        begin
            xaml = load(file)
            xaml.load_xaml_references if xaml.respond_to?(:load_xaml_references)
            return xaml
        ensure
            file.close
        end
    end
end

class Wpf::FrameworkElement

    def load_xaml_references
        descendant_xaml_references.each do | element |
            new_xaml =  Wpf::XamlReader.load_file(element.tag)
            new_xaml.name = element.name # preserve name
            element.parent.content = new_xaml
        end
    end
    
    def descendant_xaml_references
        descendants.select { |d| d.instance_of? Wpf::FrameworkElement }
    end
    
    def descendants
        [self, child_elements.map { |child| child.descendants } ].flatten
    end
    
    def child_elements
        logical_children_array.select { |child| child.kind_of?(Wpf::FrameworkElement) }
    end
    
    def logical_children_array
        self.logical_children.enumerator_to_a
    end
    
end


app = Wpf::Application.new
# Must load the theme to app.resources before loading any Xaml - then theme is applied at 
app.resources = Wpf::XamlReader.load_file "gui/Themes/Generic.xaml"


window = Wpf::XamlReader.load_file "gui/BattleCalculator.xaml"
window.size_to_content = Wpf::SizeToContent.width_and_height
app.run(window)


class ObjectExtensionTest < Test::Unit::TestCase
    
    class Mike
        attr_accessor :cat, :dog
    end

    def test_attr
        m = Mike.new.attr(:cat => "joo", :dog => :nice)
        assert_match(/#<ObjectExtensionTest::Mike:0x.+ @cat="joo", @dog=:nice>/, m.inspect)
        assert_equal("joo", m.cat)
    end
    
end