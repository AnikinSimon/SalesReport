   M S B A M L   `   `   `  ����F  BSalesReport, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null)clr-namespace:SalesReportSalesReport  5   	       SalesReport.ReportWindow   5   	   W SPresentationCore, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35e aSystem.Windows.Controls.Ribbon, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089R NWindowsBase, Version=8.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=8.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   @d2http://schemas.microsoft.com/expression/blend/2008     5   	   Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5   	   +localclr-namespace:SalesReport     5   	     � Title$  ReportWindow��5   	   $	��450��6   $	��800��6+   .��5	      � $��10q�6    � RowDefinitions 5
   
   �� 5      $
��Auto �6   �� 5      $
��Auto �6   �� 5      $��* �6   �� 5      $
��Auto �6   .+�5   
   �� -   5         ��Name$ tbReportTitle��5   !   $��0��6   $��16(�68   $
��Bold"�6F   $��Center=�5      $��0,0,0,10q�61   �� 5   
   $��1��6    �� Orientation$ 
Horizontal=�6"   $��Right=�6;   $��0,0,0,10q�6W   .+�5      �� -   5         ��Name$ 	cbArticle��5      $	��250��6*   $��0,0,10,0q�66    �� DisplayMemberPath$ Article��5       �� SelectedValuePath$ Article��63   �� 5      -   5         ��Name$ btnShowPriceHistory��5      $,��&Показать историю цен��62   $	��150��6Q   5      &   System.Windows.Controls.DataGrid  5   
   -   5          Name$ 	dgDevices��5       $��2��6   	   AutoGenerateColumns$	 False��63   
   
IsReadOnly$

 True��6O      Columns 5      0  *System.Windows.Controls.DataGridTextColumn  5      ,  &System.Windows.Controls.DataGridColumn   Header$ Артикул��6%      Width5  /System.Windows.Controls.DataGridLengthConverter$	 100 6R   1  +System.Windows.Controls.DataGridBoundColumn   Binding 66   �� *	Article+6R     5       $ 
Бренд��6%   $	 120 6N    64   �� *Brand+6N     5!      $ Модель��6%   $	 150 6O    65   �� *Model+6O     5"      $ Тип��6%   $	 100 6K    62   �� *Type+6K     5#      $ Цена, рубл.��6%   $	 100 6k    6:   �� *Price+ �� StringFormat$ {0:N2}��6k     5$      $ Дата продажи��6%   $	 120 6h    6;   �� *
SaleDate+$ d��6h   5&      �� 5)   
   $��3��6   $ 
Horizontal=�6"   $��Right=�6;   $��0,10,0,0q�6W   .+�5*      	� -   5         	�Name$ 	rbSortAsc��5*      $!��По возрастанию��6-    	� 	GroupName$ Sorting��6F    d� 	IsChecked$
 TrueY�6Z   	� 5+      -   5        $ 
rbSortDesc��5+      $��По убыванию��6.   $ Sorting��6D   $��10,0,0,0q�6X   5,      5-      5.      