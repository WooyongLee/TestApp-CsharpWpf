   M S B A M L   `   `   `  ����F  BMiscControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:MiscControlMiscControl  5   	       MiscControl.MainWindow   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:MiscControl     5   	     � Title$  
MainWindow��5   	   $	��340��6   $	��300��6)    � 	Resources 5	       �� 
TargetType   CircularButtonTemplate �� CornerRadius �� 
TargetType  CircularButtonStyle �� Property �� Value  DynamicResourceButtonStyle  	PinkBrush  StaticResourceButtonStyle��%  &	        &	 �     7   &	 v    &	 �    �� 5
   
   " ��6   .R�5      �� 
�������$ 15��6'   ._�5      �� #��z��5      #��z��5      5      5      �� 5   
   " ��6   .�5      ��  � ��$ Center=�64   6E   �� 5       � ��$ Center=�62   6C   �� 5       � ��$ 30��6'   64   �� 5       � ��$	 200��6&   64   �� 5       � ��8   6)   5      �� 5   
   " ��6   .�5      ��  � ��# �  6+   5      5      �� 5   
   " ��6   5"      5#      .��5%      � $��10q�6   .+�5&   
   �� 
����  �$��1,3,1,5q�6$   $ 8,8,3,3��6>   ��5'      �� 5(       �� 
StartPoint$ 0.5,0&�6&    �� EndPoint$ 0.5,1&�69   .��5)      � 	 � Color$	 	LightCyan��6#   
 � Offset$	
 0.0Q�65   � 5*      $	 	LightBlue��6#   $
 0.598Q�65   � 5+      $	 	#FF359B9C��6#   $	
 1.0Q�65   � 5,      $	 	#FFBDE4ED��6#   $
 0.333Q�65   5-      ._�5/      �� $��10q�6   .+�50      �� -   5         ��Name$ SliderButton��50      $��Slider ��6/   #��[ 6A   6�   �� 51      -   5        $ ProgressBarButton��51      $��ProgressBar��64   $
��0,10q�6J   #��[ 6X   6�   �� 52      -   5        $ WinFormsHostButton��52      $��WinFormsHost��65   $	��0,0q�6L   #��[ 6Y   6�   �� 53      -   5        $ CircularButton��53      $��ContentTemplateButton��61   $	��200��6R   $��30��6^   $
��0,10q�6j   ��54      �� 55      .R�56      � .+�57   "   �� $ 15��6)   
��� � �$	��200��6N   $��30��6Z   59   #   �� 5:   "   $ 10��6)   
��� ���$	��150��6O   $��20��6[   5<   #   �� 5=   "   $��ContentTemplateButton��6(   $��Center=�6I   $��Center=�6f   6�   5>      5?      5A      �� 5B      -   5        $��DynamicResourceButton��5B      $��DynamicResourceButton��66   #���  6V   6�   �� 5C      -   5        $��StaticResourceButton��5C      $��StaticResourceButton��65   #��[ 6T   6�   5D      5E      5F      5G      