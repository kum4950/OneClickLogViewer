   M S B A M L   `   `   `  ����L  HModernUI_practice, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:ModernUI_practiceModernUI_practice  5   	   S.clr-namespace:ModernUI_practice.MVVM.ViewModel ModernUI_practice.MVVM.ViewModel  5   	   "    ModernUI_practice.MainWindow   5   	   W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:ModernUI_practice     5   	   D	viewModel.clr-namespace:ModernUI_practice.MVVM.ViewModel     5   	   $	��600��5	   	   $	��920��6     � WindowStyle$
  None=�5
   	    � 
ResizeMode$ NoResize=�5   	   
������  � AllowsTransparency$
 True��5   	    � DataContext 5      4  .ModernUI_practice.MVVM.ViewModel.MainViewModel  5   
   .��5      �� ���#272537 �� CornerRadius$ 20��5      ._�5   
   �  � ColumnDefinitions 5      �� 5      $	��200 �6#   �� 5       � RowDefinitions 5      �� 5      $��75 �6    �� 5      .+�5      �� $��Elysian��6   $��Center=�5      $
��Left=�5       
�������$��22(�5"      $��20,0,0,0q�5#      �� 5%      $��1��6   .+�5&      	� $
��Home��6   $��50��5'      
�������$��14(�5)         MenuButtonTheme#��[  5*       d� 	IsChecked$
 TrueY�5+      ��5,      �� *HomeViewCommand+	� 5-      $��	Discovery��6   $��50��5.      
�������$��14(�50      #��[  51      ��52      �� *DiscoveryViewCommand+	� 53      $��Featured��6   $��50��54      
�������$��14(�56      #��[  57      58      �� 5:      $	��250��6   $��40��5;      $��Center=�5<      $
��Left=�5=      $��5q�5>      $��1��5?        ModernTextbox#��[ 5@      �� 5B      $��1��6   $��1��5C      $��10q�5D      ��5E      �� *CurrentView+5F      5I      5J      