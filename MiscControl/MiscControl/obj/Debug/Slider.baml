   M S B A M L   `   `   `  ����F  BMiscControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MiscControlMiscControl  5   	       MiscControl.SliderWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:MiscControl     5   	     � Title$  SliderAndProgressBar��5   	   $	��230��6&   $	��300��63   .��5	      �  � RowDefinitions 5
   
   �� 5      $
��131* �6   �� 5      $��2* �6   .+�5   
   �� -   5         ��Name$ SliderPanel��5      $��0��6+   $��5q�69   .+�5      ]� .+�5      �� $��10, 5, 5, 5q�6   $��Slider��61   6A   5      ]� 5      $��Center=�6   $��10q�63   .+�5      �� $
��Left=�6   $
��Bold"�6.   .��6@   R:6D   �� 5      $��Right=�6q    �� TextAlignment$ Right=�6�   $��40��6�   ��6   ��  �� ElementName$ slColorR�� �� Path$ Value� �� UpdateSourceTrigger$ PropertyChanged=�6�   �� 5      -   5        $��slColorR��5   p    � Maximum$	 255Q�6    �� TickPlacement$ BottomRight=�6'   	 �� TickFrequency$	 5Q�6C   
 �� IsSnapToTickEnabled
 . 6�   5      ]� 5      $��Center=�6   $��10q�63   .+�5      �� $
��Left=�6   $
��Bold"�6.   .��6@   G:6D   �� 5      $��Right=�6q   $ Right=�6�   $��40��6�   ��6   �� $ slColorG��$ Value�$ PropertyChanged=�6�   �� 5      -   5        $��slColorG��5   p   $	 255Q�6   $ BottomRight=�6'   $	 5Q�6C   
 . 6�   5      ]� 5       $��Center=�6   $��10q�63   .+�5!      �� $
��Left=�6   $
��Bold"�6.   .��6@   B:6D   �� 5"      $��Right=�6q   $ Right=�6�   $��40��6�   ��6   �� $ slColorB��$ Value�$ PropertyChanged=�6�   �� 5#      -   5        $��slColorB��5#   p   $	 255Q�6   $ BottomRight=�6'   $	 5Q�6C   
 . 6�   5$      5%      5&      5'      