# Reference the WPF assemblies
require 'PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
require 'PresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'

# Initialization Constants
module Wpf
    MessageBox = System::Windows::MessageBox
    Window = System::Windows::Window
    Application = System::Windows::Application
    Button = System::Windows::Controls::Button
    StackPanel = System::Windows::Controls::StackPanel
    Label = System::Windows::Controls::Label
    Thickness = System::Windows::Thickness
    XamlReader = System::Windows::Markup::XamlReader
end
