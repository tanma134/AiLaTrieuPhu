   M S B A M L   `   `   `  ����J  FAdminManagement, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullE'clr-namespace:AdminManagement.ViewModelAdminManagement.ViewModel  5   	   ?$clr-namespace:AdminManagement.HelperAdminManagement.Helper  5   	   (    "AdminManagement.View.PlayerManager   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   6vm'clr-namespace:AdminManagement.ViewModel     5   	   7helper$clr-namespace:AdminManagement.Helper     5   	     � Title$5  /Quản lý người chơi - Ai Là Triệu Phú��5   	   $	��600��66   $	��800��6C    � WindowStartupLocation$ CenterScreen=�5   	    � 
ResizeMode$ NoResize=�6.   ���	#FF1A1A2E � 	Resources 5
         GoldGradient �� 
StartPoint �� EndPoint � Color � Offset  ButtonGradient  ModernButton �� 
TargetType	 �� Property
 �� Value �� 
TargetType �� CornerRadius �� Padding �� Triggers P� Property P� Value9   3AdminManagement.Helper.BooleanToLockStatusConverter   BooleanToLockStatusConverter9   3AdminManagement.Helper.BooleanToLockButtonConverter   BooleanToLockButtonConverter��%i  &	        &	 �     &	 ,    7  &	     &	      �� 5   
   $	 0,0&�63   $	 1,1&�6D   .��5      � $ #FFD700��6   $ 0Q�6+   � 5      $ #FFA500��6   $ 1Q�6+   5      �� 5   
   $	 0,0&�65   $	 1,1&�6F   .��5      � $ 	#FF4169E1��6   $ 0Q�6-   � 5      $ 	#FF1E90FF��6   $ 1Q�6-   5      �� 5   
   " ��6%   .�5      �� 	 � ��8
   6+   �� 5      	 � ��

 �����6+   �� 5      	 � ��$
 0q�60   �� 5      	 � ��$
 12(�6)   �� 5      	 � ��$

 Bold"�6+   �� 5      	 � ��$
 30��6'   �� 5      	 �@�Cursor$

 Hand��6'   �� 5      	 � ��$

 10,0q�6(   �� 5       	 � ��
 5!      �� 5"      " ��6&   .R�5#      �� #��z��6!   $ 8��5$   !   # z��5%   !   ._�5&      �� $��Center=�6/   $��Center=�6L   5'       5(      P� 5)       �@J�IsMouseOver$
 True��6=   . �5*   "   �� 	 � ��
 5+   &   �� 5,   *   $	 0,0&�6>   $	 1,1&�6O   .��5-   .   � $ 	#FF5A7FFF��6;   $ 0Q�6M   � 5.   .   $ 	#FF3A5FFF��6;   $ 1Q�6M   5/   +   51   #   �� 52   "   	 �@�Cursor$

 Hand��6;   53      P� 54       � {�$ False��6;   . �55   "   �� 	 �@J�Opacity$	
 0.5Q�6<   �� 56   "   	 �@�Cursor$
 Arrow��6;   57      59      5;      5<        5?   
   6.     5@   
   6.   5A       � DataContext 5C      6   0AdminManagement.ViewModel.PlayerManagerViewModel  5D   
   .��5G      � ��5H   
   �� 5I      $	 0,0&�6"   $	 1,1&�63   .��5J      � $ 	#FF1A1A2E��6   $ 0Q�61   � 5K      $ 	#FF0F0F1E��6   $ 1Q�61   5L      .+�5P   
   �� -   5        $��
QUAY LẠI��5P      $	��100��5Q      $��35��6   $��	0,20,30,0q�5R      $��Right=�5S      $	��Top=�6-   $
{�True��5U       K� ZIndex$ 1��5V      #��[ 5W      �� 5Z   
   ���	#22FFFFFF$ 20��6(   $��20q�6:   #��[  5[      $��2q�6=   ._�5\      � $��30q�6    � RowDefinitions 5]      �� 5^      $
��Auto �6$   �� 5_      $��* �6$   .+�5c      �� $��0��6   $��QUẢN LÝ NGƯỜI CHƠI��6)   $��20(�5d      $
��Bold"�6*   #��[  6<   $��Center=�5e      $��0,0,0,20q�69   &   System.Windows.Controls.DataGrid  5h      -   5        $��1��5h         AutoGenerateColumns$ False��6I      
IsReadOnly$
 True��5i         CanUserAddRows$ False��6-   $��0,0,0,10q�6D   ��5h   (   �� *
Accounts+   Columns 5k      0  *System.Windows.Controls.DataGridTextColumn  5l      ,  &System.Windows.Controls.DataGridColumn   Header$ Tên đăng nhập��6-      Width5  /System.Windows.Controls.DataGridLengthConverter$ * 6a   1  +System.Windows.Controls.DataGridBoundColumn   Binding 6D   �� *
Username+6a     5m      $ Email��6-   $ * 6V    6<   �� *Email+6V     5n      $ Vai trò��6-   $ * 6W    6>   �� *Role+6W     5o      $ Trạng thái��6-   $ * 6�    6A   �� *
IsLocked+ �� 	Converter# [ 6�   4	  .System.Windows.Controls.DataGridTemplateColumn	  5p      $ Hành động��61   $ * 6D    	  CellTemplate 5q      �� 5r   "   .R�5s   &   ��  �� Orientation$ 
Horizontal=�61   .+�5t   *   �� #��[ 5v   N   $	��5,0q�6t   ��5t   1   �� *
IsLocked+# [ ��5u   1   �� *DataContext.LockCommand+ �� RelativeSource ��  �� AncestorType DataGrid��5v   1   �� 6t   �� 5w   *   $
��Xóa��61   #��[ 5y   N   $	��5,0q�6t   ��5x   1   �� *DataContext.DeleteCommand+ ��  DataGrid��5y   1   �� 6t   5z   '   5{   #   5}      5      5�      5�      5�      5�      