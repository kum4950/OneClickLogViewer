   M S B A M L   `   `   `  ����L  HOneClickLogViewer, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null5clr-namespace:OneClickLogViewerOneClickLogViewer  5   	   A%clr-namespace:OneClickLogViewer.ViewsOneClickLogViewer.Views  5   	   K*clr-namespace:OneClickLogViewer.ViewModelsOneClickLogViewer.ViewModels  5	   	   "    OneClickLogViewer.MainWindow   5   	   R NAvalonDock, Version=4.72.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4` \AvalonDock.Themes.VS2013, Version=4.72.0.0, Culture=neutral, PublicKeyToken=3e4669d2f30244f4W SPresentationCore, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=6.0.2.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35J 9http://schemas.microsoft.com/winfx/2006/xaml/presentation       5   	   R NSystem.Xaml, Version=6.0.2.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   1localclr-namespace:OneClickLogViewer     5   	   6view%clr-namespace:OneClickLogViewer.Views     5   	   @	viewmodel*clr-namespace:OneClickLogViewer.ViewModels     5	   	   D @ModernWpf, Version=0.9.5.0, Culture=neutral, PublicKeyToken=nullM	 IModernWpf.Controls, Version=0.9.5.0, Culture=neutral, PublicKeyToken=nullL
 HModernWpf.MahApps, Version=0.9.5.0, Culture=neutral, PublicKeyToken=null.ui!http://schemas.modernwpf.com/2019  	 
 5   	   -   5          � Title$  OneClickLogViewer��5   	   $	��450��6#   $	��800��60   
������  � WindowStyle$
 None=�5   	    � AllowsTransparency$
 True��5   	    � 
ResizeMode$ CanResizeWithGrip=�5   	    � DataContext 5      0  *OneClickLogViewer.ViewModels.MainViewModel  5   
    � 	Resources 5      ��%    5      .��5      ]� ���#1E1E1E$��7q�5      .+�5   
   �� $��28��6   ���#252525$	��Top=�5       J� InputBindings 5       d� 5!       d� MouseAction$ 	LeftClicke�6    �� Command 5"      �� *MoveWindowCommand+._�5$      � 	 � ColumnDefinitions	 5%      �� 5&      �� 5'      $	��100 �6'   .+�5)      �� $��OneClickLogViewer��6   
�������$��Consolas)�5+      $��Center=�5,      $��Center=�5-      $��	100,0,0,0q�5.      �� 5/      $��1��6   
 �� Orientation$
 
Horizontal=�50      $��Right=�51      $��0,0,4,0q�52      .+�53      �� $	��━��6   $��0,0,0,2q�54         TitlebarButtonStyle#��[  56      ��55      �� *MinimizeWindowCommand+56      �� 57      $	��⬜��6   $��0,0,0,3q�58      #��[  5:      ��59      �� *MaximizeWindowCommand+5:      �� 5;      $	��✕��6   $��0,0,0,2q�5<      #��[  5>      ��5=      �� *ShutdownWindowCommand+5>      5?      5@      5A      � 5C   
   $��Bottom=�6   ���#EEEEF2 � RowDefinitions 5G      �� 5H      $��21 �6    �� 5I      $��50 �6    �� 5J      $��* �6    .+�5M      o� $��0��6   .@�5N      m� $
��File��6   .@�5O      m� $	��New��6    m� Command 6,   �� *
NewCommand+m� 5P      $
��Open��6    6-   �� *OpenCommand+�� 5Q      m� 5R      $
��Save��6    6-   �� *ActiveDocument.SaveCommand+m� 5S      $��
Save As...��6    63   �� *ActiveDocument.SaveAsCommand+�� 5T      m� 5U      $��Close��6    6.   �� *ActiveDocument.CloseCommand+5V      m� 5X      $��Tools��6   .@�5Y      m� $��
Properties��6    m� IsCheckable$
 True��6j    m� 	IsChecked 63   �� *FileStats.IsVisible+ �� Mode$ TwoWay=�6j   5Z      m� 5\      $��Layout��6   .@�5]      m� $
��Load��6    6-   �� *LoadLayoutCommand+ �� ElementName$ 
mainWindow��m� 5^      $
��Save��6    6-   �� *SaveLayoutCommand+$ 
mainWindow��5`      m� 5b      $��Settings��6   .@�5c      m� $��Configurations��6    67   �� * Settings_Configuration_Command+5d      5f      �� 5h      $��1��6   $
 
Horizontal=�6&   $��50��6?   .+�5i      �� $��Date :��6   $��12(�5j      $��Center=�5k      $��Center=�5l      $��7,0,4,0q�5m      
���   �5n      (  "System.Windows.Controls.DatePicker  5p      $��5,0,0,0q�6   $��8(�5q      1 
 +ModernWpf.MahApps.Controls.SimpleTimePicker  5s      $��5,0,0,0q�6&   $��8(�5t   "   �� 5v      $��~��6   $��12(�5w      $��Center=�5x      $��Center=�5y      $��7,0,7,0q�5z      
���   �5{        5}      $��5,0,0,0q�6   $��8(�5~        5�      $��5,0,0,0q�6&   $��8(�5�   "   5�      �� 5�      $��2��6   ��6,   �� *CurrentView+5�      5�      5�      