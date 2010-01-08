# Reference the WPF assemblies
require 'PresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'
require 'PresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35'

# Initialization Constants
module Wpf
    Application = System::Windows::Application
    Button = System::Windows::Controls::Button
    Colors = System::Windows::Media::Colors
    FrameworkElement = System::Windows::FrameworkElement
    MessageBox = System::Windows::MessageBox
    Window = System::Windows::Window
    StackPanel = System::Windows::Controls::StackPanel
    Label = System::Windows::Controls::Label
    Thickness = System::Windows::Thickness
    XamlReader = System::Windows::Markup::XamlReader
    SolidColorBrush = System::Windows::Media::SolidColorBrush
    SizeToContent = System::Windows::SizeToContent
end
